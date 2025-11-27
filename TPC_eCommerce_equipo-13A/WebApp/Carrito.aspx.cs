using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace WebApp
{
    public partial class Carrito : System.Web.UI.Page
    {

        ProductoNegocio productoNegocio = new ProductoNegocio();
        public List<Producto> listaCarrito;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    listaCarrito = (List<Producto>)Session["listaCarrito"];

                    bindearRepeater(listaCarrito);
                }
            }
            catch (Exception)
            {
                //lblError.Text = "Error! No se pudieron cargar los productos! Intente nuevamente.";
            }
        }

        protected void RepeaterCarrito_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var producto = (Producto)e.Item.DataItem;

                HtmlGenericControl cantidad = (HtmlGenericControl)e.Item.FindControl("cantidadElegida");

                cantidad.InnerText = producto.Stock.ToString();


            }
        }

        protected void btnRestar_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idProducto = int.Parse(btn.CommandArgument);

            List<Producto> listaCarrito = (List<Producto>)Session["listaCarrito"];
            Producto prodActual = listaCarrito.FirstOrDefault(p => p.Id == idProducto);

            if (prodActual.Stock == 1)
            {
                listaCarrito.RemoveAll(prod => prod.Id == idProducto);
            }
            else
            {
                prodActual.Stock--;
            }

            bindearRepeater(listaCarrito);
        }

        protected void btnSumar_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idProducto = int.Parse(btn.CommandArgument);

            List<Producto> listaCarrito = (List<Producto>)Session["listaCarrito"];
            Producto prodActual = listaCarrito.FirstOrDefault(p => p.Id == idProducto);
            Producto prodEnBD = productoNegocio.buscarPorId(idProducto);

            RepeaterItem item = (RepeaterItem)((Button)sender).NamingContainer;
            CustomValidator cvStock = (CustomValidator)item.FindControl("cvStock");


            if (prodActual.Stock < prodEnBD.Stock)
                prodActual.Stock++;
            else
            {
                cvStock.IsValid = false;
                cvStock.ErrorMessage = "No puede agregarse una unidad mas, ya que excede el stock disponible.";
                return;
            }
            cvStock.IsValid = true;

            bindearRepeater(listaCarrito);
        }

        protected void bindearRepeater(List<Producto> lista)
        {
            if (lista == null || lista.Count == 0)
            {
                RepeaterCarrito.DataSource = null;
                RepeaterCarrito.DataBind();

                lblCarritoVacio.Visible = true;
                lblTotal.Text = string.Empty;

                btnCheckout.Visible = false;
                lblErrorCarrito.Text = string.Empty;
            }
            else
            {
                RepeaterCarrito.DataSource = lista;
                RepeaterCarrito.DataBind();

                lblCarritoVacio.Visible = false;

                decimal total = lista.Sum(p => p.Precio * p.Stock);
                lblTotal.Text = "Total: " + string.Format("{0:C}", total);

                btnCheckout.Visible = true;
                lblErrorCarrito.Text = string.Empty;
            }
        }


        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            List<Producto> listaCarrito = Session["listaCarrito"] as List<Producto>;

            if (listaCarrito == null || !listaCarrito.Any())
            {
                lblErrorCarrito.Text = "No hay productos en el carrito.";
                return;
            }

            Response.Redirect("CompraCheckout.aspx", false);
        }
    }
}