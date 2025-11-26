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
    public partial class EmpleadoFormABM : System.Web.UI.Page
    {
        public EmpleadoNegocio empleadoNegocio = new EmpleadoNegocio();
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
                } else
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
                string documento = txtDocumento.Text.Trim();
                string nombre = txtNombre.Text.Trim();
                string apellido = txtApellido.Text.Trim();
                DateTime fechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text);
                long telefono = long.Parse(txtTelefono.Text);
                string direccion = txtDireccion.Text.Trim();
                string codigoPostal = txtCodigoPostal.Text.Trim();
                string email = txtEmail.Text.Trim();
                string contrasenia = documento + nombre.Replace(" ", "");
                if (contrasenia.Length > 20)
                    contrasenia = contrasenia.Substring(0, 20);
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
                else
                {
                    empleadoNegocio.agregar(legajo, fechaIngreso, fechaDespido, documento, nombre, apellido, fechaNacimiento, telefono, direccion, codigoPostal, email, contrasenia);
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