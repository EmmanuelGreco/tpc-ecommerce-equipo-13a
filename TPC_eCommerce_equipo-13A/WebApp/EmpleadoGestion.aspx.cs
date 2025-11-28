using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp
{
    public partial class EmpleadoGestion : System.Web.UI.Page
    {
        EmpleadoNegocio empleadoNegocio = new EmpleadoNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Session["usuario"] != null && ((Dominio.Usuario)Session["usuario"]).TipoUsuario == Dominio.UserType.ADMIN))
            {
                Session.Add("error", "No tienes permisos para ingreasar a esta pantalla. ¡Necesitas ser ADMINISTRADOR!");
                Response.Redirect("Error.aspx", false);
            }

            if (!IsPostBack)
                listarEmpleados();
        }

        private void listarEmpleados()
        {
            List<Empleado> lista = empleadoNegocio.listar();

            Session["listaEmpleado"] = lista;

            dgvEmpleados.DataSource = lista;
            dgvEmpleados.DataBind();
        }

        protected void dgvEmpleados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                bool activo = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Activo"));
                Button btnInactivar = (Button)e.Row.FindControl("btnInactivar");

                if (btnInactivar != null)
                {
                    btnInactivar.Text = activo ? "Inactivar" : "Activar";
                    btnInactivar.CssClass = activo ? "btn btn-warning" : "btn btn-success";
                }
            }
        }
        protected void btnInactivar_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                GridViewRow row = (GridViewRow)btn.NamingContainer;
                int id = Convert.ToInt32(dgvEmpleados.DataKeys[row.RowIndex].Value);

                empleadoNegocio.alternarEstado(id);

                listarEmpleados();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
            }
        }

        protected void filtro_TextChanged(object sender, EventArgs e)
        {
            if (Session["listaEmpleado"] == null)
                return;

            List<Empleado> lista = (List<Empleado>)Session["listaEmpleado"];

            string filtro = txtFiltro.Text.Trim().ToUpper();

            if (filtro == "")
            {
                dgvEmpleados.DataSource = lista;
            }
            else
            {
                List<Empleado> listaFiltrada = lista.FindAll(x =>
                    (
                        x.Usuario != null &&
                        (
                            (x.Usuario.Nombre != null && x.Usuario.Nombre.ToUpper().Contains(filtro)) ||
                            (x.Usuario.Apellido != null && x.Usuario.Apellido.ToUpper().Contains(filtro)) ||
                            (x.Usuario.Email != null && x.Usuario.Email.ToUpper().Contains(filtro))
                        )
                    )
                    ||
                    x.Legajo.ToString().Contains(filtro)
                );

                dgvEmpleados.DataSource = listaFiltrada;
            }

            dgvEmpleados.DataBind();
        }

        protected string GetEnumDisplayName(object value)
        {
            if (value == null)
                return string.Empty;

            var enumValue = value as Enum;
            if (enumValue == null)
                return value.ToString();

            var member = enumValue.GetType().GetMember(enumValue.ToString());
            if (member.Length > 0)
            {
                var attr = member[0].GetCustomAttribute<DisplayAttribute>();
                if (attr != null)
                    return attr.Name;
            }

            return enumValue.ToString();
        }

        protected void btnVerPedidos_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idUsuarioCliente = int.Parse(btn.CommandArgument);

            Response.Redirect("PedidosHistorial.aspx?idUsuarioCliente=" + idUsuarioCliente, false);
        }
    }
}