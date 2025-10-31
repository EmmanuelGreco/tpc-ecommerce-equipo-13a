using Negocio;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp
{
    public partial class ProductoFormABM : System.Web.UI.Page
    {
        ProductoNegocio productoNegocio = new ProductoNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

            if (!IsPostBack)
            {
                ddlMarca.DataSource = marcaNegocio.listar();
                ddlMarca.DataTextField = "Nombre";
                ddlMarca.DataValueField = "Id";
                ddlMarca.DataBind();

                ddlCategoria.DataSource = categoriaNegocio.listar();
                ddlCategoria.DataTextField = "Nombre";
                ddlCategoria.DataValueField = "Id";
                ddlCategoria.DataBind();
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

                productoNegocio.agregar(codigo, nombre, descripcion, idMarca, idCategoria, origen, precio);

                errorNombre.IsValid = false;
                errorNombre.ForeColor = Color.Green;
                errorNombre.ErrorMessage = "✅ Producto agregada exitosamente.";

                txtCodigo.Text = "";
                txtNombre.Text = "";
                txtDescripcion.Text = "";
                txtOrigen.Text = "";
                txtPrecio.Text = "";
            }
            catch (Exception)
            {
                errorNombre.IsValid = false;
                errorNombre.ForeColor = Color.Red;
                errorNombre.ErrorMessage = "❌ Ocurrió un error al agregar la producto. Intenta nuevamente.";
            }
        }
    }
}