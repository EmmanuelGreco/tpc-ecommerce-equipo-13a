using Dominio;
using negocio;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp
{
    public partial class CompraCheckout : System.Web.UI.Page
    {
        ProductoNegocio productoNegocio = new ProductoNegocio();

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

                lblUsuario.Text = "Comprando como: <span style='color:blue'>" + usuario.Email + "</span>";

                List<Producto> listaCarrito = Session["listaCarrito"] as List<Producto>;
                if (listaCarrito == null || !listaCarrito.Any())
                {
                    lblErrorCompra.Text = "No hay productos en el carrito.";
                    btnConfirmarCompra.Enabled = false;
                    return;
                }

                RepeaterResumen.DataSource = listaCarrito;
                RepeaterResumen.DataBind();

                decimal total = listaCarrito.Sum(p => p.Precio * p.Stock);
                lblTotal.Text = "Total: " + string.Format("{0:C}", total);

                CargarMetodosPago();

                CargarMetodosEnvio();
            }
        }

        private void CargarMetodosPago()
        {
            ddlMetodoPago.Items.Clear();
            ddlMetodoPago.Items.Add(new ListItem("Efectivo", "1"));
            ddlMetodoPago.Items.Add(new ListItem("Tarjeta de crédito", "2"));
            ddlMetodoPago.Items.Add(new ListItem("Tarjeta de débito", "3"));
            ddlMetodoPago.Items.Add(new ListItem("Transferencia bancaria", "4"));
            ddlMetodoPago.Items.Add(new ListItem("MercadoPago", "5"));
        }

        private void CargarMetodosEnvio()
        {
            ddlMetodoEnvio.Items.Clear();
            ddlMetodoEnvio.Items.Add(new ListItem("Retiro en sucursal", "1"));
            ddlMetodoEnvio.Items.Add(new ListItem("Envío a domicilio (3 días)", "2"));
        }


        protected void btnConfirmarCompra_Click(object sender, EventArgs e)
        {
            lblErrorCompra.Text = string.Empty;

            Usuario usuario = Session["usuario"] as Usuario;

            if (usuario == null)
            {
                lblErrorCompra.Text = "Debe iniciar sesión para completar la compra.";
                return;
            }

            List<Producto> listaCarrito = Session["listaCarrito"] as List<Producto>;
            if (listaCarrito == null || !listaCarrito.Any())
            {
                lblErrorCompra.Text = "No hay productos en el carrito.";
                return;
            }

            try
            {
                foreach (var prodCarrito in listaCarrito)
                {
                    Producto prodBD = productoNegocio.buscarPorId(prodCarrito.Id);

                    if (prodBD == null)
                    {
                        lblErrorCompra.Text = $"El producto '{prodCarrito.Nombre}' ya no existe.";
                        return;
                    }

                    if (prodCarrito.Stock > prodBD.Stock)
                    {
                        lblErrorCompra.Text =
                            $"No hay stock suficiente para el producto '{prodCarrito.Nombre}'. " +
                            $"Stock disponible: {prodBD.Stock}.";
                        return;
                    }
                }

                decimal total = listaCarrito.Sum(p => p.Precio * p.Stock);

                Venta venta = new Venta
                {
                    Usuario = usuario,
                    ListaProductos = listaCarrito,

                    MetodoPago = (PaymentMethod)int.Parse(ddlMetodoPago.SelectedValue),
                    MetodoEnvio = (ShippingMethod)int.Parse(ddlMetodoEnvio.SelectedValue),
                    FechaHoraVenta = DateTime.Now,
                    // Fecha de entrega: +3 días
                    FechaHoraEntrega = DateTime.Now.AddDays(3),

                    MontoTotal = total
                };

                VentaNegocio ventaNegocio = new VentaNegocio();
                int idVenta = ventaNegocio.agregar(venta);
                venta.Id = idVenta;

                ventaNegocio.agregarProductosDeVenta(idVenta, listaCarrito);

                foreach (var prodCarrito in listaCarrito)
                {
                    Producto prodBD = productoNegocio.buscarPorId(prodCarrito.Id);
                    int nuevoStock = prodBD.Stock - prodCarrito.Stock;

                    productoNegocio.actualizarStock(prodCarrito.Id, nuevoStock);
                }

                Session["listaCarrito"] = new List<Producto>();
                Session["ultimaVenta"] = venta;

                EmailService emailService = new EmailService();

                string email = venta.Usuario.Email;

                emailService.armarCorreo(email, "¡Compra exitosa!", armarBodyEmail(venta));
                try
                {
                    emailService.enviarEmail();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                Response.Redirect("CompraExitosa.aspx?idVenta=" + idVenta, false);
            }
            catch (Exception ex)
            {
                //Session.Add("error", ex.ToString());
                //Response.Redirect("Error.aspx");
                lblErrorCompra.Text = "Ocurrió un error al registrar la compra. Intente nuevamente.";
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
            			<h1 style=""margin-bottom:35px; font-size:26px;"">Detalle del pedido</h1>
            			<p style=""margin:6px 0; font-size:18px;"">
            				<strong>Pedido N° {venta.Id}</strong>
            			</p>
            			<p style=""margin:4px 0;"">Fecha: {venta.FechaHoraVenta.ToString("dd/MM/yyyy HH:mm")}hs</p>
            			<p style=""margin:4px 0;"">Cliente: {venta.Usuario.Nombre} {venta.Usuario.Apellido} ({venta.Usuario.Email})</p>
            			<p style=""margin:4px 0;"">Forma de pago: {GetEnumDisplayName(venta.MetodoPago)}</p>
            			<p style=""margin:4px 0;"">Método de envío: {GetEnumDisplayName(venta.MetodoEnvio)}</p>
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