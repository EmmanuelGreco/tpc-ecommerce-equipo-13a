using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp
{
    public partial class PedidoDetalle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Usuario usuario = Session["usuario"] as Usuario;

                if (usuario == null)
                {
                    string urlActual = Request.RawUrl;
                    Response.Redirect("Login.aspx?returnUrl=" + Server.UrlEncode(urlActual), false);
                    return;
                }

                string idVentaStr = Request.QueryString["idVenta"];
                int idVenta;

                if (string.IsNullOrEmpty(idVentaStr) || !int.TryParse(idVentaStr, out idVenta))
                {
                    lblError.Text = "No se encontró el pedido solicitado.";
                    gvProductos.Visible = false;
                    return;
                }

                cargarDetalle(usuario, idVenta);

                configurarEdicionEstado(usuario);

                string idUsuarioClienteStr = Request.QueryString["idUsuarioCliente"];
                if (!string.IsNullOrEmpty(idUsuarioClienteStr))
                    lnkVolverHistorial.NavigateUrl = "PedidosHistorial.aspx?idUsuarioCliente=" + idUsuarioClienteStr;
                else
                    lnkVolverHistorial.NavigateUrl = "PedidosHistorial.aspx";
            }
        }

        private void cargarDetalle(Usuario usuarioLogueado, int idVenta)
        {
            VentaNegocio ventaNegocio = new VentaNegocio();
            Venta venta = ventaNegocio.obtenerPorId(idVenta);

            if (venta == null)
            {
                lblError.Text = "El pedido no existe.";
                gvProductos.Visible = false;
                return;
            }

            if (usuarioLogueado.TipoUsuario == UserType.CLIENTE &&
                (venta.Usuario == null || venta.Usuario.Id != usuarioLogueado.Id))
            {
                lblError.Text = "No tiene permisos para ver este pedido.";
                gvProductos.Visible = false;
                return;
            }

            lblPedido.Text = "Pedido N° " + venta.Id;
            lblFechaVenta.Text = "Fecha de venta: " + venta.FechaHoraVenta.ToString("dd/MM/yyyy HH:mm") + "hs";
            lblUsuario.Text = "Cliente: " + venta.Usuario.Nombre + " " + venta.Usuario.Apellido + " (" + venta.Usuario.Email + ")";

            lblMetodoPago.Text = "Forma de pago: " + GetEnumDisplayName(venta.MetodoPago);
            lblMetodoEnvio.Text = "Método de envío: " + GetEnumDisplayName(venta.MetodoEnvio);
            lblFechaEntrega.Text = "Fecha de entrega estimada: " + venta.FechaHoraEntrega?.ToString("dd/MM/yyyy") ?? "Sin fecha";

            lblEstado.Text = "Estado actual: " + GetEnumDisplayName(venta.EstadoPedido);

            List<Producto> productos = ventaNegocio.listarProductosPorVenta(idVenta);

            if (productos == null || !productos.Any())
            {
                lblError.Text = "No se encontraron productos asociados a este pedido.";
                gvProductos.Visible = false;
                return;
            }

            gvProductos.Visible = true;
            gvProductos.DataSource = productos;
            gvProductos.DataBind();

            lblTotal.Text = "Total: " + string.Format("{0:C}", venta.MontoTotal);
        }

        protected string GetEnumDisplayName(Enum value)
        {
            if (value == null)
                return string.Empty;

            var member = value.GetType().GetMember(value.ToString());
            if (member.Length > 0)
            {
                var attr = member[0].GetCustomAttribute<DisplayAttribute>();
                if (attr != null)
                    return attr.Name;
            }

            return value.ToString();
        }

        private void configurarEdicionEstado(Usuario usuarioLogueado)
        {
            if (usuarioLogueado.TipoUsuario == UserType.CLIENTE)
            {
                panelCambiarEstado.Visible = false;
                return;
            }

            panelCambiarEstado.Visible = true;

            if (ddlEstadoPedido.Items.Count == 0)
            {
                ddlEstadoPedido.Items.Add(new ListItem("Pendiente", ((int)OrderStatus.Pendiente).ToString()));
                ddlEstadoPedido.Items.Add(new ListItem("Rechazado", ((int)OrderStatus.Rechazado).ToString()));
                ddlEstadoPedido.Items.Add(new ListItem("En preparación", ((int)OrderStatus.EnPreparacion).ToString()));
                ddlEstadoPedido.Items.Add(new ListItem("Enviado", ((int)OrderStatus.Enviado).ToString()));
                ddlEstadoPedido.Items.Add(new ListItem("Listo para retirar", ((int)OrderStatus.ListoRetira).ToString()));
                ddlEstadoPedido.Items.Add(new ListItem("Entregado", ((int)OrderStatus.Entregado).ToString()));
            }

            string idVentaStr = Request.QueryString["idVenta"];
            int idVenta;
            if (!int.TryParse(idVentaStr, out idVenta))
                return;

            VentaNegocio ventaNegocio = new VentaNegocio();
            Venta venta = ventaNegocio.obtenerPorId(idVenta);

            if (venta != null)
            {
                ddlEstadoPedido.SelectedValue = ((int)venta.EstadoPedido).ToString();
            }
        }

        protected void btnActualizarEstado_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuarioLogueado = Session["usuario"] as Usuario;

                if (usuarioLogueado == null || usuarioLogueado.TipoUsuario == UserType.CLIENTE)
                {
                    lblError.Text = "No tiene permisos para modificar el estado del pedido.";
                    return;
                }

                string idVentaStr = Request.QueryString["idVenta"];
                int idVenta;
                if (!int.TryParse(idVentaStr, out idVenta))
                {
                    lblError.Text = "Pedido inválido.";
                    return;
                }

                int estadoInt = int.Parse(ddlEstadoPedido.SelectedValue);
                OrderStatus nuevoEstado = (OrderStatus)estadoInt;

                VentaNegocio ventaNegocio = new VentaNegocio();
                ventaNegocio.ActualizarEstadoPedido(idVenta, nuevoEstado);

                Venta venta = ventaNegocio.obtenerPorId(idVenta);

                if (venta == null || venta.Usuario == null)
                {
                    lblError.Text = "Se actualizó el estado, pero no se pudo recuperar la información del pedido para enviar el email.";
                }
                else
                {
                    EmailService emailService = new EmailService();
                    string email = venta.Usuario.Email;

                    string asunto = "Actualización del estado de su pedido";
                    string cuerpo = armarBodyEmail(venta);

                    emailService.armarCorreo(email, asunto, cuerpo);

                    try
                    {
                        emailService.enviarEmail();
                    }
                    catch (Exception ex)
                    {
                        lblError.Text = "El estado del pedido se actualizó, pero ocurrió un error al enviar el email.";
                    }
                }

                lblError.Text = "";
                cargarDetalle(usuarioLogueado, idVenta);
                configurarEdicionEstado(usuarioLogueado);
            }
            catch (Exception ex)
            {
                lblError.Text = "Ocurrió un error al actualizar el estado del pedido.";
            }
        }

        protected string armarBodyEmail(Venta venta)
        {
            VentaNegocio ventaNegocio = new VentaNegocio();
            List<Producto> listaProductos = ventaNegocio.listarProductosPorVenta(venta.Id);

            string cuerpo = $@"
            <!DOCTYPE html>
            <html lang=""es"">
                <head>
                    <meta charset=""UTF-8""/>
                    <title>Detalle del pedido</title>
                </head>
                <body style=""font-family: Arial, sans-serif; background-color:#f6f6f6; margin:0; padding:20px;"">
                    <div style=""max-width:800px; background:#ffffff; margin:auto; padding:30px; border-radius:6px; border:1px solid #ddd;"">
                        <h1 style=""margin-bottom:35px; font-size:26px;"">Se actualizó el estado de su pedido</h1>
                        <h3 style=""margin-bottom:35px; font-size:26px;"">Detalle del pedido</h3>
                        <p style=""margin:6px 0; font-size:18px;"">
                            <strong>Pedido N° {venta.Id}</strong>
                        </p>
                        <p style=""margin:4px 0;"">Fecha de venta: {venta.FechaHoraVenta.ToString("dd/MM/yyyy HH:mm")}hs</p>
                        <p style=""margin:4px 0;"">Cliente: {venta.Usuario.Nombre} {venta.Usuario.Apellido} ({venta.Usuario.Email})</p>
                        <p style=""margin:4px 0;"">Forma de pago: {GetEnumDisplayName(venta.MetodoPago)}</p>
                        <p style=""margin:4px 0;"">Método de envío: {GetEnumDisplayName(venta.MetodoEnvio)}</p>
                        <p style=""margin:4px 0;"">Fecha de entrega estimada: {(venta.FechaHoraEntrega?.ToString("dd/MM/yyyy") ?? "Sin fecha")}</p>
                        <p style=""margin:4px 0;"">Estado actual: {GetEnumDisplayName(venta.EstadoPedido)}</p>
                        <hr style=""margin:25px 0; border:none; border-top:1px solid #ccc;""/>
                        <h3 style=""font-size:22px; margin-bottom:15px;"">Productos</h3>
                        <table cellpadding=""8"" cellspacing=""0"" style=""width:100%; border-collapse:collapse; font-size:14px;"">
                            <tr style=""background:#f0f0f0; text-align:left;"">
                                <th style=""border:1px solid #ccc;"">Producto</th>
                                <th style=""border:1px solid #ccc;"">Descripción</th>
                                <th style=""border:1px solid #ccc;"">Cantidad</th>
                                <th style=""border:1px solid #ccc; width: 15%"">Precio unitario</th>
                                <th style=""border:1px solid #ccc; width: 15%"">Subtotal</th>
                            </tr>";

                    foreach (Producto prod in listaProductos)
                    {
                        cuerpo += $@"
                            <tr>
                                <td style=""border:1px solid #ccc;"">{prod.Nombre}</td>
                                <td style=""border:1px solid #ccc;"">{prod.Descripcion}</td>
                                <td style=""border:1px solid #ccc; text-align:center;"">{prod.Stock}</td>
                                <td style=""border:1px solid #ccc;"">{string.Format("{0:C}", prod.Precio)}</td>
                                <td style=""border:1px solid #ccc;"">{string.Format("{0:C}", (prod.Precio * prod.Stock))}</td>
                            </tr>";
                    }

                    cuerpo += $@"
                        </table>
                        <p style=""font-size:18px; margin-top:25px;"">
                            <strong>Total: {string.Format("{0:C}", venta.MontoTotal)}</strong>
                        </p>
                    </div>
                </body>
            </html>";

            return cuerpo;
        }
    }
}