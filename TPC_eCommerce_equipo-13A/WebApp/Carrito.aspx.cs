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
            if (lista?.Count == 0 || lista == null)
            {
                RepeaterCarrito.DataSource = null;
                RepeaterCarrito.DataBind();
                lblCarritoVacio.Visible = true;
            }
            else
            {
                RepeaterCarrito.DataSource = lista;
                RepeaterCarrito.DataBind();
                lblCarritoVacio.Visible = false;
            }
        }
    }
}