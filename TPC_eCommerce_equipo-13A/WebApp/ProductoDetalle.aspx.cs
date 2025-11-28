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
    public partial class ProductoDetalle : System.Web.UI.Page
    {
        private readonly ProductoNegocio productoNegocio = new ProductoNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string idStr = Request.QueryString["id"];
                if (string.IsNullOrEmpty(idStr))
                {
                    Response.Redirect("Productos.aspx", false);
                    return;
                }

                if (!int.TryParse(idStr, out int id))
                {
                    Response.Redirect("Productos.aspx", false);
                    return;
                }

                Producto productoActual = productoNegocio.buscarPorId(id);

                if (productoActual == null)
                {
                    Response.Redirect("Productos.aspx", false);
                    return;
                }

                if (productoActual.ListaImagen == null)
                    productoActual.ListaImagen = new List<ProductoImagen>();

                if (productoActual.ListaImagen.Count < 1)
                {
                    productoActual.ListaImagen.Add(new ProductoImagen());
                    productoActual.ListaImagen[0].ImagenUrl = "https://www.svgrepo.com/show/508699/landscape-placeholder.svg";
                }

                repIndicadores.DataSource = productoActual.ListaImagen;
                repIndicadores.DataBind();

                repImagenes.DataSource = productoActual.ListaImagen;
                repImagenes.DataBind();

                lblNombre.Text = productoActual.Nombre;
                lblCodigo.Text = productoActual.Codigo;
                lblDescripcion.Text = productoActual.Descripcion;
                lblOrigen.Text = productoActual.Origen;
                lblPrecio.Text = productoActual.Precio.ToString("C");
                lblStock.Text = productoActual.Stock.ToString();

                lblMarca.Text = productoActual.Marca != null ? productoActual.Marca.ToString() : "";
                lblCategoria.Text = productoActual.Categoria != null ? productoActual.Categoria.ToString() : "";

                txtCantidadDetalle.Attributes["min"] = "1";
                txtCantidadDetalle.Attributes["max"] = productoActual.Stock.ToString();

                if (productoActual.Stock <= 0)
                {
                    txtCantidadDetalle.Text = "0";
                    txtCantidadDetalle.Enabled = false;
                    btnAgregarCarritoDetalle.Enabled = false;
                    btnAgregarCarritoDetalle.Text = "Sin stock";
                    btnAgregarCarritoDetalle.CssClass = "btn btn-danger w-100";
                }
            }
        }

        protected void btnAgregarCarritoDetalle_Click(object sender, EventArgs e)
        {
            string idStr = Request.QueryString["id"];
            if (string.IsNullOrEmpty(idStr))
                return;

            int id = int.Parse(idStr);

            Producto productoBD = productoNegocio.buscarPorId(id);
            if (productoBD == null)
                return;

            int cantidadAgregada = int.Parse(txtCantidadDetalle.Text);

            List<Producto> listaCarrito = new List<Producto>();
            if (Session["listaCarrito"] != null)
                listaCarrito = (List<Producto>)Session["listaCarrito"];

            if (listaCarrito.Any(p => p.Id == productoBD.Id))
            {
                var aux = listaCarrito.First(p => p.Id == productoBD.Id);

                if (productoBD.Stock < aux.Stock + cantidadAgregada)
                {
                    cvStockDetalle.IsValid = false;
                    cvStockDetalle.ErrorMessage = "La cantidad escogida y la cantidad en el carrito exceden el stock disponible.";
                    return;
                }

                cvStockDetalle.IsValid = true;
                aux.Stock += cantidadAgregada;
            }
            else
            {
                if (productoBD.Stock < cantidadAgregada)
                {
                    cvStockDetalle.IsValid = false;
                    cvStockDetalle.ErrorMessage = "La cantidad escogida excede el stock disponible.";
                    return;
                }

                cvStockDetalle.IsValid = true;

                productoBD.Stock = cantidadAgregada;
                listaCarrito.Add(productoBD);
            }

            Session["listaCarrito"] = listaCarrito;
        }
    }
}

