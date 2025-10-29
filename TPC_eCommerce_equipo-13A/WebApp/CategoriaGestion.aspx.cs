using Negocio;
using System;
using System.Collections.Generic;
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
    }
}