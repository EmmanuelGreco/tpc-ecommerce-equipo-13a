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