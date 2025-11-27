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
                Usuario usuario = Session["usuario"] as Usuario;

                if (usuario == null)
                {
                    string urlActual = Request.RawUrl;
                    Response.Redirect("Login.aspx?returnUrl=" + Server.UrlEncode(urlActual), false);
                    return;
                }

                lblUsuario.Text = "Pedidos de: <span style='color:blue'>" + usuario.Email + "</span>";

                cargarHistorial(usuario.Id);
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
    }
}