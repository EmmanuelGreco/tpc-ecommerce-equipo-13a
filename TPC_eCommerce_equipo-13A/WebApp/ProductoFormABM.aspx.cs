using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using static System.Net.WebRequestMethods;

namespace WebApp
{
    public partial class ProductoFormABM : System.Web.UI.Page
    {
        ProductoImagenNegocio imagenNegocio = new ProductoImagenNegocio();
        ProductoNegocio productoNegocio = new ProductoNegocio();

        public List<ProductoImagen> listaImagenes = new List<ProductoImagen>();
        public int idImagen = 1;

        public Producto producto = new Producto();
        protected void Page_Load(object sender, EventArgs e)
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

            string idStr = Request.QueryString["id"];
            int idProducto = idStr != null ? int.Parse(idStr) : 0;
            if (!IsPostBack)
            {
                //Siempre carga las ddl
                ddlMarca.DataSource = marcaNegocio.listar();
                ddlMarca.DataTextField = "Nombre";
                ddlMarca.DataValueField = "Id";
                ddlMarca.DataBind();

                ddlCategoria.DataSource = categoriaNegocio.listar();
                ddlCategoria.DataTextField = "Nombre";
                ddlCategoria.DataValueField = "Id";
                ddlCategoria.DataBind();

                //Si viene de un click al "editar", te traés el ID
                if (!string.IsNullOrEmpty(idStr))
                {
                    //Te traés el producto correspondiente
                    producto = productoNegocio.buscarPorId(idProducto);

                    //Cargas en los campos de texto los datos del producto
                    txtNombre.Text = producto.Nombre;
                    txtCodigo.Text = producto.Codigo;
                    txtDescripcion.Text = producto.Descripcion;
                    txtOrigen.Text = producto.Origen;
                    txtStock.Text = producto.Stock.ToString();
                    txtPrecio.Text = producto.Precio.ToString("0.00");

                    //Cargar las imágenes
                    listaImagenes = imagenNegocio.listarPorIdProducto(idProducto);
                    txtURLImagen.Text = listaImagenes[0].ImagenUrl;

                    Titulo.InnerText = "Modificar producto";
                    btnAgregar.Text = "💾 Guardar";

                    //Cargas en las ddl la marca y cat. correspondiente
                    ddlMarca.ClearSelection();
                    ddlMarca.Items.FindByValue(producto.Marca.Id.ToString()).Selected = true;

                    ddlCategoria.ClearSelection();
                    ddlCategoria.Items.FindByValue(producto.Categoria.Id.ToString()).Selected = true;

                }
                else
                {
                    listaImagenes.Add(new ProductoImagen()
                    {
                        IdProducto = idProducto,
                        ImagenUrl = "https://www.svgrepo.com/show/508699/landscape-placeholder.svg"
                    });
                }
                Session.Add("sessionListaImagenes", listaImagenes);
            }
            else
            {
                listaImagenes = (List<ProductoImagen>)Session["sessionListaImagenes"];
                idImagen = (int)Session["sessionIdImagen"];
            }
            Session.Add("sessionIdImagen", idImagen);
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                errorNombre.IsValid = true;

                string codigo = txtCodigo.Text.Trim();
                string nombre = txtNombre.Text.Trim();
                string descripcion = txtDescripcion.Text.Trim();
                int idMarca = int.Parse(ddlMarca.SelectedItem.Value);
                int idCategoria = int.Parse(ddlCategoria.SelectedItem.Value);
                string origen = txtOrigen.Text.Trim();
                int stock = int.Parse(txtStock.Text.Trim());
                decimal precio = decimal.Parse(txtPrecio.Text.Trim());

                //if (string.IsNullOrWhiteSpace(nombre))
                //{
                //    errorNombre.IsValid = false;
                //    errorNombre.ForeColor = Color.Red;
                //    errorNombre.ErrorMessage = "¡El nombre es requerido!";
                //    return;
                //}

                listaImagenes = (List<ProductoImagen>)Session["sessionListaImagenes"];
                if (listaImagenes.All(img => img.ImagenUrl == "https://www.svgrepo.com/show/508699/landscape-placeholder.svg"))
                {
                    errorNombre.IsValid = false;
                    errorNombre.ForeColor = Color.Red;
                    errorNombre.ErrorMessage = "¡Debes agregar al menos una imagen!";
                    return;
                }

                string idStr = Request.QueryString["id"];
                if (!string.IsNullOrEmpty(idStr))
                {
                    int idProducto = int.Parse(idStr);
                    productoNegocio.modificar(idProducto, codigo, nombre, descripcion, idMarca, idCategoria, origen, stock, precio);

                    List<ProductoImagen> imagenesExistentes = imagenNegocio.listarPorIdProducto(idProducto);
                    foreach (ProductoImagen img in imagenesExistentes)
                        imagenNegocio.eliminar(img.Id);

                    foreach (ProductoImagen img in listaImagenes)
                        if (img.ImagenUrl != "https://www.svgrepo.com/show/508699/landscape-placeholder.svg")
                            imagenNegocio.agregar(idProducto, img.ImagenUrl);
                }
                else
                {
                    int idProductoNuevo = productoNegocio.agregar(codigo, nombre, descripcion, idMarca, idCategoria, origen, stock, precio);

                    foreach (ProductoImagen img in listaImagenes)
                    {
                        if (img.ImagenUrl != "https://www.svgrepo.com/show/508699/landscape-placeholder.svg")
                            imagenNegocio.agregar(idProductoNuevo, img.ImagenUrl);
                    }
                }


                Response.Redirect("ProductoGestion.aspx", false);
            }
            catch (Exception)
            {
                errorNombre.IsValid = false;
                errorNombre.ForeColor = Color.Red;
                errorNombre.ErrorMessage = "❌ Ocurrió un error al agregar el producto. Intenta nuevamente.";
                return;
            }
        }

        protected void btnAnterior_Click(object sender, EventArgs e)
        {
            listaImagenes = (List<ProductoImagen>)Session["sessionListaImagenes"];
            if (idImagen <= 1)
                idImagen = listaImagenes.Count;
            else
                idImagen = idImagen - 1;

            txtURLImagen.Text = listaImagenes[idImagen - 1].ImagenUrl != "https://www.svgrepo.com/show/508699/landscape-placeholder.svg" ? listaImagenes[idImagen - 1].ImagenUrl : "";
            Session.Add("sessionIdImagen", idImagen);
            //CarouselUP.Update();
        }

        protected void btnSiguiente_Click(object sender, EventArgs e)
        {
            listaImagenes = (List<ProductoImagen>)Session["sessionListaImagenes"];

            if (idImagen >= listaImagenes.Count)
                idImagen = 1;
            else
                idImagen = idImagen + 1;

            txtURLImagen.Text = listaImagenes[idImagen - 1].ImagenUrl != "https://www.svgrepo.com/show/508699/landscape-placeholder.svg" ? listaImagenes[idImagen - 1].ImagenUrl : "";
            Session.Add("sessionIdImagen", idImagen);
            //CarouselUP.Update();
        }

        protected void btnConfirmarImagen_Click(object sender, EventArgs e)
        {
            if (txtURLImagen.Text != "https://www.svgrepo.com/show/508699/landscape-placeholder.svg" && txtURLImagen.Text != "")
            {
                listaImagenes = (List<ProductoImagen>)Session["sessionListaImagenes"];
                listaImagenes[idImagen - 1].ImagenUrl = txtURLImagen.Text;
                Session.Add("sessionListaImagenes", listaImagenes);
            }
        }

        protected void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            string idStr = Request.QueryString["id"];
            int idProducto = idStr != null ? int.Parse(idStr) : 0;

            listaImagenes = (List<ProductoImagen>)Session["sessionListaImagenes"];
            listaImagenes.Add(new ProductoImagen()
            {
                IdProducto = idProducto,
                ImagenUrl = "https://www.svgrepo.com/show/508699/landscape-placeholder.svg"
            });
            //txtURLImagen.Text = listaImagenes[idImagen - 1].ImagenUrl;
            txtURLImagen.Text = "";
            Session.Add("sessionIdImagen", listaImagenes.Count);
            Session.Add("sessionListaImagenes", listaImagenes);
        }

        protected void btnRemoverImagen_Click(object sender, EventArgs e)
        {
            listaImagenes = (List<ProductoImagen>)Session["sessionListaImagenes"];
            if (listaImagenes.Count > 1)
                listaImagenes.RemoveAt(idImagen - 1);
            //if (listaImagenes.Count == 0)
            //    txtURLImagen.Text = "";
            Session.Add("sessionIdImagen", listaImagenes.Count);
            txtURLImagen.Text = listaImagenes.Last().ImagenUrl != "https://www.svgrepo.com/show/508699/landscape-placeholder.svg" ? listaImagenes.Last().ImagenUrl : "";
            Session.Add("sessionListaImagenes", listaImagenes);
        }
    }
}