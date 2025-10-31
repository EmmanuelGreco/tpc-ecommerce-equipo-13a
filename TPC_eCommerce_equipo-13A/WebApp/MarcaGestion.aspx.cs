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
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                foreach (Control control in e.Row.Cells[1].Controls)
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

        protected void dgvMarcas_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(dgvMarcas.DataKeys[e.RowIndex].Value);
                GridViewRow fila = dgvMarcas.Rows[e.RowIndex];

                TextBox texto = (TextBox)fila.Cells[0].Controls[0];
                string nuevoNombre = texto.Text.Trim();

                if (string.IsNullOrEmpty(nuevoNombre))
                {
                    Response.Write("<script>alert('El nombre no puede estar vacío');</script>");
                    return;
                }

                marcaNegocio.modificar(id, nuevoNombre);

                dgvMarcas.EditIndex = -1;
                listarMarcas();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message.Replace("'", "") + "');</script>");
            }
        }

        protected void dgvMarcas_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            dgvMarcas.EditIndex = -1;
            listarMarcas();
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