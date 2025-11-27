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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();

            try
            {
                usuario.Email = txtEmail.Text;
                usuario.Contrasenia = txtPassword.Text;

                if (usuarioNegocio.Loguear(usuario))
                {
                    Usuario usuarioLogueado = usuarioNegocio.ObtenerDatos(usuario);

                    FuncionesAdicionales fa = new FuncionesAdicionales();
                    if (usuario.Contrasenia == fa.generarContrasenia(usuarioLogueado.Nombre, usuarioLogueado.Documento))
                    {
                        Session["usuarioAEditar"] = usuarioLogueado;
                        Response.Redirect("CambiarContrasenia.aspx", false);
                    }
                    else
                    {
                        Session["usuario"] = usuarioLogueado;

                        string returnUrl = Request.QueryString["returnUrl"];

                        if (!string.IsNullOrEmpty(returnUrl))

                            Response.Redirect(Server.UrlDecode(returnUrl), false);
                        else
                            Response.Redirect("Default.aspx", false);
                    }
                }
                else
                {
                    Session.Add("error", "¡Email o Contraseña incorrectos!");
                    Response.Redirect("Error.aspx", false);
                }
            }
            catch (Exception ex)
            {

                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }
    }
}