using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp
{
    public partial class Contacto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    string nombre = txtNombre.Text.Trim();
                    string email = txtEmail.Text.Trim();
                    string mensaje = txtMensaje.Text.Trim();

                    EmailService emailService = new EmailService();

                    string cuerpo = $@"
                                    <!DOCTYPE html>
                                    <html lang=""es"">
                                    	<head>
                                    		<meta charset=""UTF-8""/>
                                    		<title>Nueva pregunta</title>
                                    	</head>
                                    	<body style=""font-family: Arial, sans-serif; background-color:#f6f6f6; margin:0; padding:20px;"">
                                    		<div style=""max-width:800px; background:#ffffff; margin:auto; padding:30px; border-radius:6px; border:1px solid #ddd;"">
                                    			<h1 style=""margin-bottom:35px; font-size:26px;"">Se recibió una pregunta de {nombre}</h1>
                                    			<p style=""margin:6px 0; font-size:18px;"">
                                    				<strong>{email}</strong>
                                    			</p>
                                    			<p style=""margin:4px 0;"">{mensaje}</p>
                                    		</div>
                                    	</body>
                                    </html>
                                    ";

                    emailService.armarCorreo("ptaquino2003@gmail.com", $"Nueva pregunta - {nombre} - {email}", cuerpo);
                    try
                    {
                        emailService.enviarEmail();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    lblResultado.CssClass = "text-success mt-3 d-block";
                    lblResultado.Text = $"¡Gracias {nombre}! Tu mensaje fue enviado correctamente.";

                    txtNombre.Text = txtEmail.Text = txtMensaje.Text = "";
                }
                catch (Exception)
                {
                    lblResultado.CssClass = "text-danger mt-3 d-block";
                    lblResultado.Text = "Ocurrió un error al enviar el mensaje. Intentalo nuevamente.";
                }
            }
        }
    }
}