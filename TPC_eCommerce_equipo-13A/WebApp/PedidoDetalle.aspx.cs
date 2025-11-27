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
    public partial class PedidoDetalle : System.Web.UI.Page
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

                string idVentaStr = Request.QueryString["idVenta"];
                int idVenta;

                if (string.IsNullOrEmpty(idVentaStr) || !int.TryParse(idVentaStr, out idVenta))
                {
                    lblError.Text = "No se encontró el pedido solicitado.";
                    gvProductos.Visible = false;
                    return;
                }

                cargarDetalle(usuario, idVenta);
            }
        }

        private void cargarDetalle(Usuario usuarioLogueado, int idVenta)
        {
            VentaNegocio ventaNegocio = new VentaNegocio();
            Venta venta = ventaNegocio.obtenerPorId(idVenta);

            if (venta == null)
            {
                lblError.Text = "El pedido no existe.";
                gvProductos.Visible = false;
                return;
            }

            if (venta.Usuario == null || venta.Usuario.Id != usuarioLogueado.Id)
            {
                lblError.Text = "No tiene permisos para ver este pedido.";
                gvProductos.Visible = false;
                return;
            }

            lblPedido.Text = "Pedido N° " + venta.Id;
            lblFecha.Text = "Fecha: " + venta.FechaHoraVenta.ToString("dd/MM/yyyy HH:mm") + "hs";
            lblUsuario.Text = "Cliente: " + venta.Usuario.Nombre + " " + venta.Usuario.Apellido + " (" + venta.Usuario.Email + ")";

            lblMetodoPago.Text = "Forma de pago: " + GetEnumDisplayName(venta.MetodoPago);
            lblMetodoEnvio.Text = "Método de envío: " + GetEnumDisplayName(venta.MetodoEnvio);

            List<Producto> productos = ventaNegocio.listarProductosPorVenta(idVenta);

            if (productos == null || !productos.Any())
            {
                lblError.Text = "No se encontraron productos asociados a este pedido.";
                gvProductos.Visible = false;
                return;
            }

            gvProductos.Visible = true;
            gvProductos.DataSource = productos;
            gvProductos.DataBind();

            lblTotal.Text = "Total: " + string.Format("{0:C}", venta.MontoTotal);
        }

        protected string GetEnumDisplayName(Enum value)
        {
            if (value == null)
                return string.Empty;

            var member = value.GetType().GetMember(value.ToString());
            if (member.Length > 0)
            {
                var attr = member[0].GetCustomAttribute<DisplayAttribute>();
                if (attr != null)
                    return attr.Name;
            }

            return value.ToString();
        }
    }
}