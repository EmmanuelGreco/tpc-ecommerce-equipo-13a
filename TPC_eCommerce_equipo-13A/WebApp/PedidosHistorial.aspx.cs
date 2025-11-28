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
    public partial class PedidosHistorial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Usuario usuarioLogueado = Session["usuario"] as Usuario;

                if (usuarioLogueado == null)
                {
                    string urlActual = Request.RawUrl;
                    Response.Redirect("Login.aspx?returnUrl=" + Server.UrlEncode(urlActual), false);
                    return;
                }

                string idUsuarioClienteStr = Request.QueryString["idUsuarioCliente"];
                int idUsuarioParaHistorial;

                if (!string.IsNullOrEmpty(idUsuarioClienteStr) && int.TryParse(idUsuarioClienteStr, out idUsuarioParaHistorial))
                {
                    if (usuarioLogueado.TipoUsuario == UserType.CLIENTE && idUsuarioParaHistorial != usuarioLogueado.Id)
                    {
                        lblTitulo.Text = "Mis pedidos";
                        lblUsuario.Text = "No tiene permisos para ver pedidos de otros usuarios.";
                        cargarHistorial(usuarioLogueado.Id);
                        return;
                    }

                    UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                    Usuario cliente = usuarioNegocio.obtenerPorId(idUsuarioParaHistorial);

                    lblTitulo.Text = "Pedidos del cliente";

                    if (cliente != null)
                        lblUsuario.Text = "Cliente: <span style='color:blue'>" + cliente.Email + "</span>";
                    else
                        lblUsuario.Text = "Cliente seleccionado";

                    cargarHistorial(idUsuarioParaHistorial);
                }
                else
                {
                    lblTitulo.Text = "Mis pedidos";
                    lblUsuario.Text = "Pedidos de: <span style='color:blue'>" + usuarioLogueado.Email + "</span>";
                    cargarHistorial(usuarioLogueado.Id);
                }
            }
        }

        private void cargarHistorial(int idUsuario)
        {
            VentaNegocio ventaNegocio = new VentaNegocio();
            List<Venta> lista = ventaNegocio.listarPorUsuario(idUsuario);

            if (lista != null && lista.Any())
            {
                gvPedidos.DataSource = lista;
                gvPedidos.DataBind();
                lblSinPedidos.Visible = false;
            }
            else
            {
                gvPedidos.DataSource = null;
                gvPedidos.DataBind();
                lblSinPedidos.Visible = true;
                lblSinPedidos.Text = "Todavía no tenés pedidos registrados.";
            }
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

        protected string GetDetalleUrl(object idVentaObj)
        {
            int idVenta = Convert.ToInt32(idVentaObj);
            string idUsuarioClienteStr = Request.QueryString["idUsuarioCliente"];

            if (!string.IsNullOrEmpty(idUsuarioClienteStr))
            {
                return $"PedidoDetalle.aspx?idVenta={idVenta}&idUsuarioCliente={idUsuarioClienteStr}";
            }

            return $"PedidoDetalle.aspx?idVenta={idVenta}";
        }
    }
}