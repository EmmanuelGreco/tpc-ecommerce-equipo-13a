using Dominio;
using negocio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp
{
    public partial class EmpleadoFormABM : System.Web.UI.Page
    {
        public EmpleadoNegocio empleadoNegocio = new EmpleadoNegocio();
        public UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
        public Empleado empleado = new Empleado();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Session["usuario"] != null && ((Dominio.Usuario)Session["usuario"]).TipoUsuario == Dominio.UserType.ADMIN))
            {
                Session.Add("error", "No tienes permisos para ingreasar a esta pantalla. ¡Necesitas ser ADMINISTRADOR!");
                Response.Redirect("Error.aspx", false);
            }

            string idStr = Request.QueryString["id"];
            int idEmpleado = idStr != null ? int.Parse(idStr) : 0;
            if (!IsPostBack)
            {
                //Si viene de un click al "editar", te traés el ID
                if (!string.IsNullOrEmpty(idStr))
                {
                    empleado = empleadoNegocio.buscarPorId(idEmpleado);

                    txtDocumento.Text = empleado.Usuario.Documento.ToString();
                    txtNombre.Text = empleado.Usuario.Nombre;
                    txtApellido.Text = empleado.Usuario.Apellido;
                    txtFechaNacimiento.Text = empleado.Usuario.FechaNacimiento.ToString("yyyy-MM-dd");
                    txtTelefono.Text = empleado.Usuario.Telefono.ToString();
                    txtDireccion.Text = empleado.Usuario.Direccion;
                    txtCodigoPostal.Text = empleado.Usuario.CodigoPostal;
                    txtEmail.Text = empleado.Usuario.Email;
                    //txtFechaAlta.Text = empleado.Usuario.FechaAlta.ToString("yyyy-MM-dd");

                    txtLegajo.Text = empleado.Legajo.ToString();
                    txtFechaIngreso.Text = empleado.FechaIngreso.ToString("yyyy-MM-dd");
                    txtFechaDespido.Text = empleado.FechaDespido?.ToString("yyyy-MM-dd");

                    Titulo.InnerText = "Modificar empleado";
                    btnAgregar.Text = "💾 Guardar";
                }
                else
                {
                    txtLegajo.Text = (empleadoNegocio.obtenerUltimoLegajo() + 1).ToString();
                    txtFechaDespido.Enabled = false;
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

                int legajo = int.Parse(txtLegajo.Text);
                DateTime fechaIngreso = DateTime.Parse(txtFechaIngreso.Text);
                DateTime? fechaDespido = txtFechaDespido.Text != "" ? (DateTime?)DateTime.Parse(txtFechaDespido.Text) : (DateTime?)null;


                string idStr = Request.QueryString["id"];
                if (!string.IsNullOrEmpty(idStr))
                {
                    int idEmpleado = int.Parse(idStr);
                    empleadoNegocio.modificar(idEmpleado, legajo, fechaIngreso, fechaDespido, documento, nombre, apellido, fechaNacimiento, telefono, direccion, codigoPostal, email);
                    if (fechaDespido != null)
                        empleadoNegocio.alternarEstado(idEmpleado);
                }
                else if (!usuarioNegocio.mailExiste(email))
                {
                    empleadoNegocio.agregar(legajo, fechaIngreso, fechaDespido, documento, nombre, apellido, fechaNacimiento, telefono, direccion, codigoPostal, email, contrasenia);
                    EmailService emailService = new EmailService();

                    string cuerpo = $@"
                                    <!DOCTYPE html>
                                    <html lang=""es"">
                                    	<head>
                                    		<meta charset=""UTF-8""/>
                                    		<title>Empleado Creado</title>
                                    	</head>
                                    	<body style=""font-family: Arial, sans-serif; background-color:#f6f6f6; margin:0; padding:20px;"">
                                    		<div style=""max-width:800px; background:#ffffff; margin:auto; padding:30px; border-radius:6px; border:1px solid #ddd;"">
                                    			<h1 style=""margin-bottom:35px; font-size:26px;"">Usuario de empleado creado exitosamente</h1>
                                    			<p style=""margin:6px 0; font-size:18px;"">
                                    				<strong>¡Ya podés comenzar a trabajar!</strong>
                                    			</p>
                                    			<p style=""margin:4px 0;"">Se ha generado tu contraseña: {contrasenia}</p>
                                    		</div>
                                    	</body>
                                    </html>
                                    ";

                    emailService.armarCorreo(email, "¡Usuario de empleado creado!", cuerpo);
                    try
                    {
                        emailService.enviarEmail();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }


                Response.Redirect("EmpleadoGestion.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
            }
        }
    }
}