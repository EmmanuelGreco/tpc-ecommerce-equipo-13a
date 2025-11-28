using Dominio;
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
                Usuario usuario = Session["usuario"] as Usuario;
                bool logueado = usuario != null;

                phLogin.Visible = !logueado;
                phRegistrar.Visible = !logueado;
                phPedidosHistorial.Visible = logueado;
                phCambiarContrasenia.Visible = logueado;
                phLogout.Visible = logueado;

                phUsuarioLogueado.Visible = logueado;

                if (logueado)
                {
                    //Email o Nombre + Apellido
                    lblUsuarioHeader.Text = usuario.Email;
                    //lblUsuarioHeader.Text = $"{usuario.Nombre}";
                    //lblUsuarioHeader.Text = $"{usuario.Nombre} {usuario.Apellido}";
                }
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