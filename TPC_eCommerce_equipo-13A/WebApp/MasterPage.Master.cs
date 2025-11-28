using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bool logueado = Session["usuario"] != null;

                phLogin.Visible = !logueado;
                phRegistrar.Visible = !logueado;
                phPedidosHistorial.Visible = logueado;
                phCambiarContrasenia.Visible = logueado;
                phLogout.Visible = logueado;
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("/");
        }

        protected void btnPedidosHistorial_Click(object sender, EventArgs e)
        {
            Response.Redirect("PedidosHistorial.aspx", false);
        }
    }
}