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
        CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                listarCategorias();
        }

        private void listarCategorias()
        {
            dgvCategorias.DataSource = categoriaNegocio.listar();
            dgvCategorias.DataBind();
        }

        // Este metodo es para que aparezca el cartel "Editar" al pasar el mouse por el ícono de Acción.
        protected void dgvCategorias_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                bool activo = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Activo"));
                Button btnInactivar = (Button)e.Row.FindControl("btnInactivar");

                if (btnInactivar != null)
                {
                    btnInactivar.Text = activo ? "Inactivar" : "Activar";
                    btnInactivar.CssClass = activo ? "btn btn-warning" : "btn btn-success";
                }

                foreach (TableCell cell in e.Row.Cells)
                {
                    foreach (Control control in cell.Controls)
                    {
                        if (control is LinkButton btn && btn.CommandName == "Edit")
                        {
                            btn.ToolTip = "Editar Categoría 📝";
                        }
                    }
                }
            }
        }

        /*protected void dgvCategorias_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            //BORRADO ACÁ
            int id = int.Parse(e.CommandArgument.ToString());

            categoriaNegocio.alternarEstado(id);

            listarCategorias();
        }*/

        protected void dgvCategorias_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            dgvCategorias.EditIndex = e.NewEditIndex;
            listarCategorias();
        }

        protected void dgvCategorias_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow fila = dgvCategorias.Rows[e.RowIndex];
            TextBox txtNombre = fila.FindControl("txtNombre") as TextBox;
            Label lblError = fila.FindControl("lblError") as Label;

            string nombre = txtNombre.Text.Trim();

            lblError.Visible = false;
            lblError.Text = "";

            try
            {
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    lblError.Text = "¡El nombre es requerido!";
                    lblError.Visible = true;
                    return;
                }

                int id = Convert.ToInt32(dgvCategorias.DataKeys[e.RowIndex].Value);

                if (categoriaNegocio.existe(nombre, id))
                {
                    lblError.Text = "¡Ya existe una categoría con ese nombre!";
                    lblError.Visible = true;
                    return;
                }

                categoriaNegocio.modificar(id, nombre);

                dgvCategorias.EditIndex = -1;
                listarCategorias();
            }
            catch (Exception ex)
            {
                lblGlobalError.Text = "Error al actualizar: " + ex.Message;
                lblGlobalError.Visible = true;
            }
        }

        protected void dgvCategorias_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            dgvCategorias.EditIndex = -1;
            listarCategorias();
        }

        protected void btnInactivar_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                GridViewRow row = (GridViewRow)btn.NamingContainer;
                int id = Convert.ToInt32(dgvCategorias.DataKeys[row.RowIndex].Value);

                categoriaNegocio.alternarEstado(id);

                listarCategorias();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
            }
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

                if (categoriaNegocio.existe(nombre, 0))
                {
                    errorCategoriaCustom.IsValid = false;
                    errorCategoriaCustom.ForeColor = Color.Red;
                    errorCategoriaCustom.ErrorMessage = "¡Ya existe una categoría con ese nombre!";
                    return;
                }

                categoriaNegocio.agregar(nombre);

                errorCategoriaCustom.IsValid = false;
                errorCategoriaCustom.ForeColor = Color.Green;
                errorCategoriaCustom.ErrorMessage = "✅ Categoría agregada exitosamente.";

                txtNuevaCategoria.Text = "";
                listarCategorias();
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