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
    public partial class CompraError : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            string status = Request.QueryString["status"];
            string idVentaStr = Request.QueryString["idVenta"];

            if (string.IsNullOrEmpty(idVentaStr) && Session["idVenta"] != null)
                idVentaStr = Session["idVenta"].ToString();

            Venta venta = null;
            VentaNegocio ventaNegocio = new VentaNegocio();

            int idVenta;
            if (!string.IsNullOrEmpty(idVentaStr) && int.TryParse(idVentaStr, out idVenta))
            {
                venta = ventaNegocio.obtenerPorId(idVenta);
            }

            if (venta != null)
            {
                lblMensaje.Text = $"El pago del pedido N° {venta.Id} no pudo completarse.";
            }
            else if (!string.IsNullOrEmpty(idVentaStr))
            {
                lblMensaje.Text = $"El pago del pedido N° {idVentaStr} no pudo completarse.";
            }
            else
            {
                lblMensaje.Text = "El pago no pudo completarse o fue cancelado.";
            }

            if (!string.IsNullOrEmpty(status) && status.ToLower() != "null")
            {
                lblDetalleEstado.Text = $"Estado informado por el medio de pago: {status}.";
            }
            else
            {
                lblDetalleEstado.Text = "No se recibió información adicional del estado del pago.";
            }


            if (venta != null)
            {
                try
                {
                    ventaNegocio.ActualizarEstadoPedido(venta.Id, OrderStatus.Rechazado);

                    List<Producto> listaProductos = ventaNegocio.listarProductosPorVenta(venta.Id);

                    if (listaProductos != null && listaProductos.Any())
                    {
                        panelResumen.Visible = true;

                        RepeaterResumen.DataSource = listaProductos;
                        RepeaterResumen.DataBind();

                        decimal total = listaProductos.Sum(p => p.Precio * p.Stock);
                        lblTotal.Text = "Total del pedido: " + string.Format("{0:C}", total);
                    }
                    else
                    {
                        panelResumen.Visible = false;
                    }
                }
                catch
                {
                    panelResumen.Visible = false;
                }
            }
            else
            {
                panelResumen.Visible = false;
            }

            Session["ultimaVenta"] = null;
            Session["idVenta"] = null;
        }
    }
}