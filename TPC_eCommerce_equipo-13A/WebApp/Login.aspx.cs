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

                int statusLogueado = usuarioNegocio.Loguear(usuario);
                if (statusLogueado == 0)
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
                else if (statusLogueado == 1)
                {
                    Session.Add("error", "¡Email o contraseña incorrectos!");
                    Response.Redirect("Error.aspx", false);
                }
                else if (statusLogueado == 2)
                {
                    Session.Add("error", "¡Usuario inhabilitado!");
                    Response.Redirect("Error.aspx", false);
                }
            }
            catch (Exception ex)
            {

                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnOlvidada_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            EmailService emailService = new EmailService();

            string cuerpo = $@"
                                    <!DOCTYPE html>
                                    <html lang=""es"">
                                    	<head>
                                    		<meta charset=""UTF-8""/>
                                    		<title>Reestablecer contraseña</title>
                                    	</head>
                                    	<body style=""font-family: Arial, sans-serif; background-color:#f6f6f6; margin:0; padding:20px;"">
                                    		<div style=""max-width:800px; background:#ffffff; margin:auto; padding:30px; border-radius:6px; border:1px solid #ddd;"">
                                    			<h1 style=""margin-bottom:35px; font-size:26px;"">Solicitud de reestablecimiento de contraseña</h1>
                                    			<p style=""margin:6px 0; font-size:18px;"">Para reestablecer tu contraseña, debes clickear en el siguiente botón:</p>
                                                <a href=""https://localhost:44314/CambiarContrasenia.aspx?email={email}""
                                                                style=""color: #fff; background-color: #198754; border: 1px solid #198754; padding: 0.375rem 0.75rem; 
                                                                border-radius: 0.25rem; font-size: 1rem; cursor: pointer; text-decoration: none;"">
                                                  Reestablecer contraseña
                                                </a>
                                    		</div>
                                    	</body>
                                    </html>
                                    ";

            emailService.armarCorreo(email, "Reestablecer contraseña", cuerpo);
            try
            {
                emailService.enviarEmail();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            Response.Redirect("Productos.aspx", false);
        }
    }
}