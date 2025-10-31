using Dominio;
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
    public partial class ProductoGestion : System.Web.UI.Page
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
                ListarProductos();
            }
        }
        private void ListarProductos()
        {
            dgvProductos.DataSource = productoNegocio.listar();
            dgvProductos.DataBind();
        }

        // Este metodo es para que aparezca el cartel "Editar" al pasar el mouse por el ícono de Acción.
        protected void dgvProductos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                foreach (Control control in e.Row.Cells[1].Controls)
                {
                    if (control is LinkButton btn && btn.CommandName == "Edit")
                    {
                        btn.ToolTip = "Editar categoría";
                    }
                }
            }
        }

        protected void dgvProductos_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            dgvProductos.EditIndex = e.NewEditIndex;
            ListarProductos();
        }

        protected void dgvProductos_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(dgvProductos.DataKeys[e.RowIndex].Value);
                GridViewRow fila = dgvProductos.Rows[e.RowIndex];

                TextBox texto = (TextBox)fila.Cells[0].Controls[0];
                string nuevoNombre = texto.Text.Trim();

                if (string.IsNullOrEmpty(nuevoNombre))
                {
                    Response.Write("<script>alert('El nombre no puede estar vacío');</script>");
                    return;
                }

                //productoNegocio.modificar(id, nuevoNombre);

                dgvProductos.EditIndex = -1;
                ListarProductos();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message.Replace("'", "") + "');</script>");
            }
        }

        protected void dgvProductos_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            dgvProductos.EditIndex = -1;
            ListarProductos();
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
                errorNombre.ErrorMessage = "✅ Categoría agregada exitosamente.";

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
                errorNombre.ErrorMessage = "❌ Ocurrió un error al agregar la categoría. Intenta nuevamente.";
            }
        }
    }
}