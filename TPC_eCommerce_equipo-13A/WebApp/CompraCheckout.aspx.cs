using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    MetodoEnvio = (ShippingMethod)int.Parse(ddlMetodoPago.SelectedValue),
                    FechaHoraVenta = DateTime.Now,
                    // Fecha de entrega: +3 días
                    FechaHoraEntrega = DateTime.Now.AddDays(3),

                    MontoTotal = total
                };

                VentaNegocio ventaNegocio = new VentaNegocio();
                int idVenta = ventaNegocio.agregar(venta);

                venta.Id = idVenta;

                foreach (var prodCarrito in listaCarrito)
                {
                    Producto prodBD = productoNegocio.buscarPorId(prodCarrito.Id);
                    int nuevoStock = prodBD.Stock - prodCarrito.Stock;

                    productoNegocio.actualizarStock(prodCarrito.Id, nuevoStock);
                }

                Session["listaCarrito"] = new List<Producto>();
                Session["ultimaVenta"] = venta;

                Response.Redirect("CompraExitosa.aspx?idVenta=" + idVenta, false);
            }

            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
                //lblErrorCompra.Text = "Ocurrió un error al registrar la compra. Intente nuevamente.";
            }
        }
    }
}