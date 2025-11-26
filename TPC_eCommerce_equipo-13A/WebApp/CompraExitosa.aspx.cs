using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp
{
    public partial class CompraExitosa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string idVentaStr = Request.QueryString["idVenta"];

                if (string.IsNullOrEmpty(idVentaStr))
                {
                    lblMensaje.Text = "Tu compra se ha registrado correctamente.";
                }
                else
                {
                    lblMensaje.Text = "El pedido N° " + idVentaStr + " se ha registrado correctamente.";
                }

                Venta ultimaVenta = Session["ultimaVenta"] as Venta;

                if (ultimaVenta != null && ultimaVenta.ListaProductos != null && ultimaVenta.ListaProductos.Any())
                {
                    panelResumen.Visible = true;

                    RepeaterResumen.DataSource = ultimaVenta.ListaProductos;
                    RepeaterResumen.DataBind();

                    decimal total = ultimaVenta.ListaProductos.Sum(p => p.Precio * p.Stock);
                    lblTotal.Text = "Total: " + string.Format("{0:C}", total);
                }
                else
                {
                    panelResumen.Visible = false;
                }

                Session["ultimaVenta"] = null;
            }
        }
    }
}