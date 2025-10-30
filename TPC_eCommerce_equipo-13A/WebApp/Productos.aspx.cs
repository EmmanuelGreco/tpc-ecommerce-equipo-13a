using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp
{
    public partial class Productos : System.Web.UI.Page
    {
        public List<Producto> ListaProducto {  get; set; }
        ProductoNegocio productoNegocio = new ProductoNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ListaProducto = productoNegocio.listar();

                    foreach (Producto art in ListaProducto)
                    {
                        if (art.ListaImagen == null)
                            art.ListaImagen = new List<ProductoImagen>();

                        if (art.ListaImagen.Count < 1)
                        {
                            art.ListaImagen.Add(new ProductoImagen());
                            art.ListaImagen[0].ImagenUrl = "https://www.svgrepo.com/show/508699/landscape-placeholder.svg";
                        }
                    }

                    RepeaterProductos.DataSource = ListaProducto;
                    RepeaterProductos.DataBind();
                }
            }
            catch (Exception)
            {
                //lblError.Text = "Error! No se pudieron cargar los productos! Intente nuevamente.";
            }
        }
    }
}