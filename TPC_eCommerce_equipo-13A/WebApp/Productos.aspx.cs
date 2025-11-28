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

                    CargarFiltros(listaProducto);

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

        private void CargarFiltros(List<Producto> lista)
        {
            var categorias = lista
                .Where(p => p.Categoria != null)
                .Select(p => p.Categoria.ToString())
                .Distinct()
                .OrderBy(c => c)
                .ToList();

            cblCategorias.DataSource = categorias;
            cblCategorias.DataBind();

            var marcas = lista
                .Where(p => p.Marca != null)
                .Select(p => p.Marca.ToString())
                .Distinct()
                .OrderBy(m => m)
                .ToList();

            cblMarcas.DataSource = marcas;
            cblMarcas.DataBind();
        }

        private void AplicarFiltros()
        {
            if (Session["listaProducto"] == null)
                return;

            var lista = (List<Producto>)Session["listaProducto"];

            string texto = txtFiltroRapido.Text.Trim().ToUpper();

            var categoriasSeleccionadas = cblCategorias.Items.Cast<ListItem>()
                .Where(i => i.Selected)
                .Select(i => i.Value)
                .ToList();

            var marcasSeleccionadas = cblMarcas.Items.Cast<ListItem>()
                .Where(i => i.Selected)
                .Select(i => i.Value)
                .ToList();

            var listaFiltrada = lista.Where(p =>
            {
                bool coincideTexto = true;
                if (!string.IsNullOrEmpty(texto))
                {
                    coincideTexto =
                        (!string.IsNullOrEmpty(p.Codigo) && p.Codigo.ToUpper().Contains(texto)) ||
                        (!string.IsNullOrEmpty(p.Nombre) && p.Nombre.ToUpper().Contains(texto));
                }

                bool coincideCategoria = true;
                if (categoriasSeleccionadas.Any())
                {
                    var catTexto = p.Categoria != null ? p.Categoria.ToString() : "";
                    coincideCategoria = categoriasSeleccionadas.Contains(catTexto);
                }

                bool coincideMarca = true;
                if (marcasSeleccionadas.Any())
                {
                    var marcaTexto = p.Marca != null ? p.Marca.ToString() : "";
                    coincideMarca = marcasSeleccionadas.Contains(marcaTexto);
                }

                return coincideTexto && coincideCategoria && coincideMarca;
            }).ToList();

            RepeaterProductos.DataSource = listaFiltrada;
            RepeaterProductos.DataBind();
        }

        protected void filtroRapido_TextChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        protected void Filtros_Changed(object sender, EventArgs e)
        {
            AplicarFiltros();
        }
    }
}