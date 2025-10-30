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
    public partial class CategoriaGestion : System.Web.UI.Page
    {
        CategoriaNegocio negocio = new CategoriaNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                ListarCategorias();
        }

        private void ListarCategorias()
        {
            dgvCategorias.DataSource = negocio.listar();
            dgvCategorias.DataBind();
        }

        // Este metodo es para que aparezca el cartel "Editar" al pasar el mouse por el ícono de Acción.
        protected void dgvCategorias_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void dgvCategorias_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            dgvCategorias.EditIndex = e.NewEditIndex;
            ListarCategorias();
        }

        protected void dgvCategorias_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(dgvCategorias.DataKeys[e.RowIndex].Value);
                GridViewRow fila = dgvCategorias.Rows[e.RowIndex];

                TextBox texto = (TextBox)fila.Cells[0].Controls[0];
                string nuevoNombre = texto.Text.Trim();

                if (string.IsNullOrEmpty(nuevoNombre))
                {
                    Response.Write("<script>alert('El nombre no puede estar vacío');</script>");
                    return;
                }

                negocio.modificar(id, nuevoNombre);

                dgvCategorias.EditIndex = -1;
                ListarCategorias();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message.Replace("'", "") + "');</script>");
            }
        }

        protected void dgvCategorias_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            dgvCategorias.EditIndex = -1;
            ListarCategorias();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                errorCategoriaCustom.IsValid = true;

                string nombre = txtNuevaCategoria.Text.Trim();

                if (string.IsNullOrWhiteSpace(nombre))
                {
                    errorCategoriaCustom.IsValid = false;
                    errorCategoriaCustom.ForeColor = Color.Red;
                    errorCategoriaCustom.ErrorMessage = "¡El nombre es requerido!";
                    return;
                }

                if (negocio.existe(nombre, 0))
                {
                    errorCategoriaCustom.IsValid = false;
                    errorCategoriaCustom.ForeColor = Color.Red;
                    errorCategoriaCustom.ErrorMessage = "¡Ya existe una categoría con ese nombre!";
                    return;
                }

                negocio.agregar(nombre);

                errorCategoriaCustom.IsValid = false;
                errorCategoriaCustom.ForeColor = Color.Green;
                errorCategoriaCustom.ErrorMessage = "✅ Categoría agregada exitosamente.";

                txtNuevaCategoria.Text = "";
                ListarCategorias();
            }
            catch (Exception)
            {
                errorCategoriaCustom.IsValid = false;
                errorCategoriaCustom.ForeColor = Color.Red;
                errorCategoriaCustom.ErrorMessage = "❌ Ocurrió un error al agregar la categoría. Intenta nuevamente.";
            }
        }
    }
}