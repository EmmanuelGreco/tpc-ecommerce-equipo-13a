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
    public partial class ClienteGestion : System.Web.UI.Page
    {
        ClienteNegocio clienteNegocio = new ClienteNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Session["usuario"] != null && (((Dominio.Usuario)Session["usuario"]).TipoUsuario == Dominio.UserType.EMPLEADO ||
                                                 ((Dominio.Usuario)Session["usuario"]).TipoUsuario == Dominio.UserType.ADMIN)))
            {
                Session.Add("error", "No tienes permisos para ingreasar a esta pantalla. ¡Necesitas ser EMPLEADO!");
                Response.Redirect("Error.aspx", false);
            }

            if (!IsPostBack)
                listarClientes();
        }

        private void listarClientes()
        {
            List<Cliente> lista = clienteNegocio.listar();

            Session["listaCliente"] = lista;

            dgvClientes.DataSource = lista;
            dgvClientes.DataBind();
        }

        protected void dgvClientes_RowDataBound(object sender, GridViewRowEventArgs e)
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
                int id = Convert.ToInt32(dgvClientes.DataKeys[row.RowIndex].Value);

                clienteNegocio.alternarEstado(id);

                listarClientes();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
            }
        }

        protected void filtro_TextChanged(object sender, EventArgs e)
        {
            if (Session["listaCliente"] == null)
                return;

            List<Cliente> lista = (List<Cliente>)Session["listaCliente"];

            string filtro = txtFiltro.Text.Trim().ToUpper();

            if (filtro == "")
            {
                dgvClientes.DataSource = lista;
            }
            else
            {
                List<Cliente> listaFiltrada = lista.FindAll(x =>
                    (
                        x.Usuario != null &&
                        (
                            (x.Usuario.Nombre != null && x.Usuario.Nombre.ToUpper().Contains(filtro)) ||
                            (x.Usuario.Apellido != null && x.Usuario.Apellido.ToUpper().Contains(filtro)) ||
                            (x.Usuario.Email != null && x.Usuario.Email.ToUpper().Contains(filtro)) ||
                            (x.Usuario.Documento != null && x.Usuario.Documento.ToUpper().Contains(filtro))
                        )
                    )
                );

                dgvClientes.DataSource = listaFiltrada;
            }

            dgvClientes.DataBind();
        }

        protected void btnVerPedidos_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idUsuarioCliente = int.Parse(btn.CommandArgument);

            Response.Redirect("PedidosHistorial.aspx?idUsuarioCliente=" + idUsuarioCliente, false);
        }
    }
}