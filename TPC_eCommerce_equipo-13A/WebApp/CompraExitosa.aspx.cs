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
    public partial class CompraExitosa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            string status = Request.QueryString["status"];
            string idVentaStr = Request.QueryString["idVenta"];

            if (string.IsNullOrEmpty(idVentaStr) && Session["idVenta"] != null)
                idVentaStr = Session["idVenta"].ToString();

            Venta ultimaVenta = Session["ultimaVenta"] as Venta;

            if (!string.IsNullOrEmpty(idVentaStr) &&
                ultimaVenta != null &&
                !string.IsNullOrEmpty(status))
            {
                int idVenta;
                if (int.TryParse(idVentaStr, out idVenta))
                {
                    ProductoNegocio productoNegocio = new ProductoNegocio();
                    VentaNegocio ventaNegocio = new VentaNegocio();

                    if (status == "approved")
                    {
                        try
                        {
                            foreach (var prod in ultimaVenta.ListaProductos)
                            {
                                Producto prodBD = productoNegocio.buscarPorId(prod.Id);

                                if (prodBD == null)
                                    continue;

                                int nuevoStock = prodBD.Stock - prod.Stock;
                                productoNegocio.actualizarStock(prod.Id, nuevoStock);
                            }

                            EmailService emailService = new EmailService();
                            string email = ultimaVenta.Usuario.Email;

                            emailService.armarCorreo(email, "¡Compra exitosa!", armarBodyEmail(ultimaVenta));
                            try
                            {
                                emailService.enviarEmail();
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }

                            lblMensaje.Text = "El pedido N° " + idVentaStr + " se ha pagado correctamente.";
                        }
                        catch
                        {
                            lblMensaje.Text = "Tu pago fue aprobado, pero ocurrió un error al confirmar el pedido. Nos contactaremos contigo.";
                        }
                    }
                    else if (status == "pending")
                    {
                        lblMensaje.Text = "Tu pago está pendiente. Te avisaremos cuando se acredite.";
                    }
                    else
                    {
                        ventaNegocio.ActualizarEstadoPedido(ultimaVenta.Id, OrderStatus.Rechazado);
                        lblMensaje.Text = "Tu pago fue rechazado o cancelado. Podés intentar nuevamente.";
                    }
                }
                else
                {
                    lblMensaje.Text = "Tu compra se ha registrado correctamente.";
                }
            }
            else
            {
                if (string.IsNullOrEmpty(idVentaStr))
                    lblMensaje.Text = "Tu compra se ha registrado correctamente.";
                else
                    lblMensaje.Text = "El pedido N° " + idVentaStr + " se ha registrado correctamente.";
            }

            if (ultimaVenta != null && ultimaVenta.ListaProductos != null && ultimaVenta.ListaProductos.Any())
            {
                panelResumen.Visible = true;

                RepeaterResumen.DataSource = ultimaVenta.ListaProductos;
                RepeaterResumen.DataBind();

                decimal total = ultimaVenta.ListaProductos.Sum(p => p.Precio * p.Stock);
                lblTotal.Text = "Total: " + string.Format("{0:C}", total);
            }
            else
            {
                panelResumen.Visible = false;
            }

            Session["listaCarrito"] = new List<Producto>();
            Session["ultimaVenta"] = null;
            Session["idVenta"] = null;

        }

        protected string armarBodyEmail(Venta ultimaVenta)
        {
            VentaNegocio ventaNegocio = new VentaNegocio();
            List<Producto> listaProductos = ventaNegocio.listarProductosPorVenta(ultimaVenta.Id);

            string cuerpo = $@"
            <!DOCTYPE html>
            <html lang=""es"">
            	<head>
            		<meta charset=""UTF-8""/>
            		<title>Detalle del pedido</title>
            	</head>
            	<body style=""font-family: Arial, sans-serif; background-color:#f6f6f6; margin:0; padding:20px;"">
            		<div style=""max-width:800px; background:#ffffff; margin:auto; padding:30px; border-radius:6px; border:1px solid #ddd;"">
            			<h1 style=""margin-bottom:35px; font-size:26px;"">¡Su compra se ha procesado exitosamente!</h1>
                        <h3 style=""margin-bottom:35px; font-size:26px;"">Detalle del pedido</h3>
            			<p style=""margin:6px 0; font-size:18px;"">
            				<strong>Pedido N° {ultimaVenta.Id}</strong>
            			</p>
            			<p style=""margin:4px 0;"">Fecha de venta: {ultimaVenta.FechaHoraVenta.ToString("dd/MM/yyyy HH:mm")}hs</p>
            			<p style=""margin:4px 0;"">Cliente: {ultimaVenta.Usuario.Nombre} {ultimaVenta.Usuario.Apellido} ({ultimaVenta.Usuario.Email})</p>
            			<p style=""margin:4px 0;"">Forma de pago: {GetEnumDisplayName(ultimaVenta.MetodoPago)}</p>
            			<p style=""margin:4px 0;"">Método de envío: {GetEnumDisplayName(ultimaVenta.MetodoEnvio)}</p>
                        <p style=""margin:4px 0;"">Fecha de entrega estimada: {(ultimaVenta.FechaHoraEntrega?.ToString("dd/MM/yyyy") ?? "Sin fecha")}</p>
                        <p style=""margin:4px 0;"">Estado actual: {GetEnumDisplayName(ultimaVenta.EstadoPedido)}</p>
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
            				<strong>Total: {string.Format("{0:C}", ultimaVenta.MontoTotal)}</strong>
            			</p>
            		</div>
            	</body>
            </html>";

            return cuerpo;
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
    }
}