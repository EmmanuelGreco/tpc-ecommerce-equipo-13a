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
    public partial class MarcaGestion : System.Web.UI.Page
    {
        MarcaNegocio marcaNegocio = new MarcaNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                listarMarcas();
        }

        private void listarMarcas()
        {
            dgvMarcas.DataSource = marcaNegocio.listar();
            dgvMarcas.DataBind();

        }

        // Este metodo es para que aparezca el cartel "Editar" al pasar el mouse por el ícono de Acción.
        protected void dgvMarcas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;

            foreach (TableCell cell in e.Row.Cells)
            {
                foreach (Control control in cell.Controls)
                {
                    if (control is LinkButton btn && btn.CommandName == "Edit")
                    {
                        btn.ToolTip = "Editar marca";
                    }
                }
            }
        }

        protected void dgvMarcas_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            dgvMarcas.EditIndex = e.NewEditIndex;
            listarMarcas();
        }

        protected void dgvMarcas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow fila = dgvMarcas.Rows[e.RowIndex];
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

                int id = Convert.ToInt32(dgvMarcas.DataKeys[e.RowIndex].Value);

                if (marcaNegocio.existe(nombre, id))
                {
                    lblError.Text = "¡Ya existe una marca con ese nombre!";
                    lblError.Visible = true;
                    return;
                }

                marcaNegocio.modificar(id, nombre);

                dgvMarcas.EditIndex = -1;
                listarMarcas();
            }
            catch (Exception ex)
            {
                lblGlobalError.Text = "Error al actualizar: " + ex.Message;
                lblGlobalError.Visible = true;
            }
        }

        protected void dgvMarcas_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            dgvMarcas.EditIndex = -1;
            listarMarcas();
        }

        protected void btnInactivar_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                GridViewRow row = (GridViewRow)btn.NamingContainer;
                int id = Convert.ToInt32(dgvMarcas.DataKeys[row.RowIndex].Value);

                marcaNegocio.eliminarLogico(id);

                listarMarcas();
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
                errorMarcaCustom.IsValid = true;

                string nombre = txtNuevaMarca.Text.Trim();

                if (string.IsNullOrWhiteSpace(nombre))
                {
                    errorMarcaCustom.IsValid = false;
                    errorMarcaCustom.ForeColor = Color.Red;
                    errorMarcaCustom.ErrorMessage = "¡El nombre es requerido!";
                    return;
                }

                if (marcaNegocio.existe(nombre, 0))
                {
                    errorMarcaCustom.IsValid = false;
                    errorMarcaCustom.ForeColor = Color.Red;
                    errorMarcaCustom.ErrorMessage = "¡Ya existe una marca con ese nombre!";
                    return;
                }

                marcaNegocio.agregar(nombre);

                errorMarcaCustom.IsValid = false;
                errorMarcaCustom.ForeColor = Color.Green;
                errorMarcaCustom.ErrorMessage = "✅ Marca agregada exitosamente.";

                txtNuevaMarca.Text = "";
                listarMarcas();
            }
            catch (Exception)
            {
                errorMarcaCustom.IsValid = false;
                errorMarcaCustom.ForeColor = Color.Red;
                errorMarcaCustom.ErrorMessage = "❌ Ocurrió un error al agregar la marca. Intenta nuevamente.";
            }
        }
    }
}