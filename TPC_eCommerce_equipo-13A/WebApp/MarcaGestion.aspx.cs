using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace WebApp
{
    public partial class MarcaGestion : System.Web.UI.Page
    {
        MarcaNegocio negocio = new MarcaNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                ListarMarcas();
        }

        private void ListarMarcas()
        {
            dgvMarcas.DataSource = negocio.listar();
            dgvMarcas.DataBind();
        }

        protected void dgvMarcas_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            dgvMarcas.EditIndex = e.NewEditIndex;
            ListarMarcas();
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

                negocio.modificar(id, nuevoNombre);

                dgvMarcas.EditIndex = -1;
                ListarMarcas();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message.Replace("'", "") + "');</script>");
            }
        }

        protected void dgvMarcas_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            dgvMarcas.EditIndex = -1;
            ListarMarcas();
        }
    }
}