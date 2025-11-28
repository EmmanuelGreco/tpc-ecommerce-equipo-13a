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
        UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
        int idUsuario = -1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] != null)
                idUsuario = ((Usuario)Session["usuario"]).Id;
            else if (Session["usuarioAEditar"] != null)
                idUsuario = ((Usuario)Session["usuarioAEditar"]).Id;
            else
            {
                string queryEmail = Request.QueryString["email"];
                if (!string.IsNullOrEmpty(queryEmail))
                {
                    Usuario usuarioACambiar = new Usuario();
                    usuarioACambiar.Email = queryEmail;
                    usuarioACambiar = usuarioNegocio.ObtenerDatos(usuarioACambiar);
                    idUsuario = usuarioACambiar.Id;
                    txtContraseniaActual.Visible = false;
                    lblContraseniaActual.Visible = false;
                    rfvContraseniaActual.Enabled = false;
                    rfvContraseniaActual.Visible = false;
                    revContraseniaActual.Enabled = false;
                    revContraseniaActual.Visible = false;
                }
                else
                    Response.Redirect("Default.aspx", false);
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtContraseniaNueva.Text == txtContraseniaNuevaRepe.Text)
                usuarioNegocio.cambiarContrasenia(idUsuario, txtContraseniaNueva.Text);

            if (Session["usuario"] != null)
                Response.Redirect("Productos.aspx", false);
            else if (Session["usuarioAEditar"] != null)
                Session["usuarioAEditar"] = null;
                
            Response.Redirect("Login.aspx", false);
        }
    }
}
