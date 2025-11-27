using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp
{
    public partial class CambiarContrasenia : System.Web.UI.Page
    {
        int idUsuario = -1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] != null)
                idUsuario = ((Usuario)Session["usuario"]).Id;
            else if (Session["usuarioAEditar"] != null)
                idUsuario = ((Usuario)Session["usuarioAEditar"]).Id;
            else
                Response.Redirect("Default.aspx", false);
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            if (txtContraseniaNueva.Text == txtContraseniaNuevaRepe.Text)
                usuarioNegocio.cambiarContrasenia(idUsuario, txtContraseniaNueva.Text);

            if (Session["usuario"] != null)
                Response.Redirect("Productos.aspx", false);
            else if (Session["usuarioAEditar"] != null)
            {
                Session["usuarioAEditar"] = null;
                Response.Redirect("Login.aspx", false);
            }
        }
    }
}
