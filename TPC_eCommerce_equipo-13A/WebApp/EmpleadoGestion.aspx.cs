using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp
{
    public partial class EmpleadoGestion : System.Web.UI.Page
    {
        EmpleadoNegocio empleadoNegocio = new EmpleadoNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                listarEmpleados();
        }

        private void listarEmpleados()
        {
            dgvEmpleados.DataSource = empleadoNegocio.listar();
            dgvEmpleados.DataBind();
        }

        protected void dgvEmpleados_RowDataBound(object sender, GridViewRowEventArgs e)
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
            }
        }
        protected void btnInactivar_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                GridViewRow row = (GridViewRow)btn.NamingContainer;
                int id = Convert.ToInt32(dgvEmpleados.DataKeys[row.RowIndex].Value);

                empleadoNegocio.alternarEstado(id);

                listarEmpleados();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
            }
        }
    }
}