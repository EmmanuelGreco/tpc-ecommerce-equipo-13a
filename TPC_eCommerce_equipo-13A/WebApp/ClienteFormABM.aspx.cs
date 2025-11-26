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


                string idStr = Request.QueryString["id"];
                if (!string.IsNullOrEmpty(idStr))
                {
                    int idCliente = int.Parse(idStr);
                    clienteNegocio.modificar(idCliente, documento, nombre, apellido, fechaNacimiento, telefono, direccion, codigoPostal, email);
                }
                else
                {
                    clienteNegocio.agregar(documento, nombre, apellido, fechaNacimiento, telefono, direccion, codigoPostal, email, contrasenia);
                }


                Response.Redirect("ClienteGestion.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
            }
        }
    }
}