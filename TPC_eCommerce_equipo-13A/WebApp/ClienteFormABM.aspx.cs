using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace WebApp
{
    public partial class ClienteFormABM : System.Web.UI.Page
    {
        public ClienteNegocio clienteNegocio = new ClienteNegocio();
        public UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
        public Cliente cliente = new Cliente();
        protected void Page_Load(object sender, EventArgs e)
        {
            /*if (!(Session["usuario"] != null && (((Dominio.Usuario)Session["usuario"]).TipoUsuario == Dominio.UserType.EMPLEADO ||
                                                 ((Dominio.Usuario)Session["usuario"]).TipoUsuario == Dominio.UserType.ADMIN)))
            {
                Session.Add("error", "No tienes permisos para ingreasar a esta pantalla. ¡Necesitas ser EMPLEADO!");
                Response.Redirect("Error.aspx", false);
            }*/

            string idStr = Request.QueryString["id"];
            int idCliente = idStr != null ? int.Parse(idStr) : 0;
            if (!IsPostBack)
            {
                //Si viene de un click al "editar", te traés el ID
                if (!string.IsNullOrEmpty(idStr))
                {
                    cliente = clienteNegocio.buscarPorId(idCliente);

                    txtDocumento.Text = cliente.Usuario.Documento.ToString();
                    txtNombre.Text = cliente.Usuario.Nombre;
                    txtApellido.Text = cliente.Usuario.Apellido;
                    txtFechaNacimiento.Text = cliente.Usuario.FechaNacimiento.ToString("yyyy-MM-dd");
                    txtTelefono.Text = cliente.Usuario.Telefono.ToString();
                    txtDireccion.Text = cliente.Usuario.Direccion;
                    txtCodigoPostal.Text = cliente.Usuario.CodigoPostal;
                    txtEmail.Text = cliente.Usuario.Email;
                    //txtFechaAlta.Text = cliente.Usuario.FechaAlta.ToString("yyyy-MM-dd");

                    Titulo.InnerText = "Modificar cliente";
                    btnAgregar.Text = "💾 Guardar";
                }
            }
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                FuncionesAdicionales fa = new FuncionesAdicionales();

                string documento = txtDocumento.Text.Trim();
                string nombre = txtNombre.Text.Trim();
                string apellido = txtApellido.Text.Trim();
                DateTime fechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text);
                long telefono = long.Parse(txtTelefono.Text);
                string direccion = txtDireccion.Text.Trim();
                string codigoPostal = txtCodigoPostal.Text.Trim();
                string email = txtEmail.Text.Trim();
                string contrasenia = fa.generarContrasenia(nombre, documento);
                //DateTime fechaAlta = DateTime.Parse(txtFechaAlta.Text);


                string idStr = Request.QueryString["id"];
                if (!string.IsNullOrEmpty(idStr))
                {
                    int idCliente = int.Parse(idStr);
                    clienteNegocio.modificar(idCliente, documento, nombre, apellido, fechaNacimiento, telefono, direccion, codigoPostal, email);
                }
                else if (!usuarioNegocio.mailExiste(email))
                {
                    clienteNegocio.agregar(documento, nombre, apellido, fechaNacimiento, telefono, direccion, codigoPostal, email, contrasenia);
                    EmailService emailService = new EmailService();

                    string cuerpo = $@"
                                    <!DOCTYPE html>
                                    <html lang=""es"">
                                    	<head>
                                    		<meta charset=""UTF-8""/>
                                    		<title>Usuario Creado</title>
                                    	</head>
                                    	<body style=""font-family: Arial, sans-serif; background-color:#f6f6f6; margin:0; padding:20px;"">
                                    		<div style=""max-width:800px; background:#ffffff; margin:auto; padding:30px; border-radius:6px; border:1px solid #ddd;"">
                                    			<h1 style=""margin-bottom:35px; font-size:26px;"">Usuario creado exitosamente</h1>
                                    			<p style=""margin:6px 0; font-size:18px;"">
                                    				<strong>¡Gracias por registrate en nuestro sitio!</strong>
                                    			</p>
                                    			<p style=""margin:4px 0;"">Se ha generado tu contraseña: {contrasenia}</p>
                                    		</div>
                                    	</body>
                                    </html>
                                    ";

                    emailService.armarCorreo(email, "¡Usuario creado!", cuerpo);
                    try
                    {
                        emailService.enviarEmail();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }


                Response.Redirect("Productos.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
            }
        }
    }
}