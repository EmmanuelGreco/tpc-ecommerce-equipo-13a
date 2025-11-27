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
        public List<Producto> listaProducto { get; set; }
        ProductoNegocio productoNegocio = new ProductoNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    listaProducto = productoNegocio.listar(1);
                    Session["listaProducto"] = listaProducto;

                    foreach (Producto art in listaProducto)
                    {
                        if (art.ListaImagen == null)
                            art.ListaImagen = new List<ProductoImagen>();

                        if (art.ListaImagen.Count < 1)
                        {
                            art.ListaImagen.Add(new ProductoImagen());
                            art.ListaImagen[0].ImagenUrl = "https://www.svgrepo.com/show/508699/landscape-placeholder.svg";
                        }
                    }

                    RepeaterProductos.DataSource = listaProducto;
                    RepeaterProductos.DataBind();
                }
            }
            catch (Exception)
            {
                //lblError.Text = "Error! No se pudieron cargar los productos! Intente nuevamente.";
            }
        }

        protected void btnAgregarCarrito_Command(object sender, CommandEventArgs e)
        {
            //Se obtiene el id del producto
            int idProducto = int.Parse(e.CommandArgument.ToString());

            //Se buscan los datos del producto a agregar
            Producto productoAgregado = productoNegocio.buscarPorId(idProducto);

            //Se crea localmente la lista del carrito
            List<Producto> listaCarrito = new List<Producto>();
            //Si ya existe en la Session, traerla
            if ((List<Producto>)Session["listaCarrito"] != null)
                listaCarrito = (List<Producto>)Session["listaCarrito"];
            //Si no, se utiliza la vacia creada arriba.

            //Se busca la cantidad seleccionada para el item del repeater
            RepeaterItem item = (RepeaterItem)((Button)sender).NamingContainer;
            TextBox cantidadTxt = (TextBox)item.FindControl("cantidadElegida");
            int cantidadAgregada = int.Parse(cantidadTxt.Text);

            CustomValidator cvStock = (CustomValidator)item.FindControl("cvStock");


            if (listaCarrito.Any(prod => prod.Id == idProducto))
            {
                Producto aux = listaCarrito.FirstOrDefault(prod => prod.Id == idProducto);
                if (productoAgregado.Stock < cantidadAgregada + aux.Stock)
                {
                    cvStock.IsValid = false;
                    cvStock.ErrorMessage = "La cantidad escogida y la cantidad en el carrito exceden el stock disponible.";
                    return; 
                }
                cvStock.IsValid = true;

                aux.Stock += cantidadAgregada;
            }
            else
            {
                productoAgregado.Stock = cantidadAgregada;
                listaCarrito.Add(productoAgregado);
            }

            //Acá se chequea si 

            Session["listaCarrito"] = listaCarrito;
        }

        protected void RepeaterProductos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var producto = (Producto)e.Item.DataItem;

                TextBox cantidad = (TextBox)e.Item.FindControl("cantidadElegida");
                Button agregar = (Button)e.Item.FindControl("btnAgregarCarrito");
                if (producto.Stock > 0)
                {
                    cantidad.Attributes["min"] = "1";
                    cantidad.Attributes["max"] = producto.Stock.ToString();
                }
                else
                {
                    cantidad.Text = "0";
                    cantidad.Enabled = false;
                    agregar.Enabled = false;
                    agregar.Text = "Sin stock";
                    //agregar.Attributes["Style"] = "background-color: #ffcccc; color: #990000; border-color";
                    agregar.CssClass = "btn btn-danger w-100";
                }
            }
        }

        protected void filtro_TextChanged(object sender, EventArgs e)
        {
            if (Session["listaProducto"] == null)
                return;

            List<Producto> lista = (List<Producto>)Session["listaProducto"];

            if (string.IsNullOrWhiteSpace(txtFiltro.Text))
            {
                RepeaterProductos.DataSource = lista;
                RepeaterProductos.DataBind();
                return;
            }

            List<Producto> listaFiltrada = lista.FindAll(x => 
                x.Codigo.ToUpper().Contains(txtFiltro.Text.ToUpper()) ||
                x.Nombre.ToUpper().Contains(txtFiltro.Text.ToUpper())
            );

            RepeaterProductos.DataSource = listaFiltrada;
            RepeaterProductos.DataBind();
        }
    }
}