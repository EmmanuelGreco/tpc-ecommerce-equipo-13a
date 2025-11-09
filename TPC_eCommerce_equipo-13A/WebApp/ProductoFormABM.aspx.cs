using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace WebApp
{
    public partial class ProductoFormABM : System.Web.UI.Page
    {
        ProductoNegocio productoNegocio = new ProductoNegocio();
        public List<ProductoImagen> listaImagenes = new List<ProductoImagen>();
        //List<ProductoImagen> listaImagenes { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

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

                //Cargar las imágenes
                listaImagenes.Add(new ProductoImagen
                {
                    ImagenUrl = "https://www.svgrepo.com/show/508699/landscape-placeholder.svg"
                });


                //Si viene de un click al "editar", te traés el ID
                string idStr = Request.QueryString["id"];
                if (!string.IsNullOrEmpty(idStr))
                {
                    //Te traés el producto correspondiente
                    int id = int.Parse(idStr);
                    Producto producto = new Producto();
                    producto = productoNegocio.buscarPorId(id);

                    //Cargas en los campos de texto los datos del producto
                    txtNombre.Text = producto.Nombre;
                    txtCodigo.Text = producto.Codigo;
                    txtDescripcion.Text = producto.Descripcion;
                    txtOrigen.Text = producto.Origen;
                    txtPrecio.Text = producto.Precio.ToString("0.00");

                    btnAgregar.Text = "💾 Guardar";

                    //Cargas en las ddl la marca y cat. correspondiente
                    ddlMarca.ClearSelection();
                    ddlMarca.Items.FindByValue(producto.Marca.Id.ToString()).Selected = true;
                    
                    ddlCategoria.ClearSelection();
                    ddlCategoria.Items.FindByValue(producto.Categoria.Id.ToString()).Selected = true;

                }
            }
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
                decimal precio = decimal.Parse(txtPrecio.Text.Trim());

                if (string.IsNullOrWhiteSpace(nombre))
                {
                    errorNombre.IsValid = false;
                    errorNombre.ForeColor = Color.Red;
                    errorNombre.ErrorMessage = "¡El nombre es requerido!";
                    return;
                }

                string idStr = Request.QueryString["id"];
                if (!string.IsNullOrEmpty(idStr))
                {
                    int id = int.Parse(idStr);
                    productoNegocio.modificar(id, codigo, nombre, descripcion, idMarca, idCategoria, origen, precio);
                    errorNombre.IsValid = false;
                    errorNombre.ForeColor = Color.Green;
                    errorNombre.ErrorMessage = "✅ Producto modificado exitosamente.";
                }
                else
                { 
                    productoNegocio.agregar(codigo, nombre, descripcion, idMarca, idCategoria, origen, precio);
                    errorNombre.IsValid = false;
                    errorNombre.ForeColor = Color.Green;
                    errorNombre.ErrorMessage = "✅ Producto agregado exitosamente.";
                }


                txtCodigo.Text = "";
                txtNombre.Text = "";
                txtDescripcion.Text = "";
                txtOrigen.Text = "";
                txtPrecio.Text = "";


                //PODRIA USARSE PARA VOLVER AL INICIO, EN VEZ DE PONER LOS CAMPOS EN ""
                    //HtmlMeta meta = new HtmlMeta();
                    //meta.HttpEquiv = "refresh";
                    //meta.Content = "2;url=Default.aspx";
                    //Page.Header.Controls.Add(meta);
                    //return;
            }
            catch (Exception)
            {
                errorNombre.IsValid = false;
                errorNombre.ForeColor = Color.Red;
                errorNombre.ErrorMessage = "❌ Ocurrió un error al agregar el producto. Intenta nuevamente.";
            }
        }

        protected void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            try
            {
                listaImagenes.Add(new ProductoImagen
                {
                    ImagenUrl = URLImagen.Text
                });
                listaImagenes.Add(new ProductoImagen
                {
                    ImagenUrl = "https://www.svgrepo.com/show/508699/landscape-placeholder.svg"
                });

            }
            catch (Exception)
            {
                errorNombre.IsValid = false;
                errorNombre.ForeColor = Color.Red;
                errorNombre.ErrorMessage = "❌ Ocurrió un error al agregar el producto. Intenta nuevamente.";
            }
        }
    }
}