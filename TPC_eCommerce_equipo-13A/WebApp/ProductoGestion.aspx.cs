using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp
{
    public partial class ProductoGestion : System.Web.UI.Page
    {
        ProductoNegocio productoNegocio = new ProductoNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                listarProductos();
        }

        private void listarProductos()
        {
            dgvProductos.DataSource = productoNegocio.listar();
            dgvProductos.DataBind();
        }

        // Este metodo es para que aparezca el cartel "Editar" al pasar el mouse por el ícono de Acción.
        protected void dgvProductos_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
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

        protected void dgvProductos_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            //BORRADO ACÁ
            int id = int.Parse(e.CommandArgument.ToString());

            productoNegocio.alternarEstado(id);

            listarProductos();
        }

        protected void btnInactivar_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                GridViewRow row = (GridViewRow)btn.NamingContainer;
                int id = Convert.ToInt32(dgvProductos.DataKeys[row.RowIndex].Value);

                productoNegocio.alternarEstado(id);
                
                listarProductos();                
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
            }
        }


        //protected void dgvProductos_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        //{
        //    dgvProductos.EditIndex = e.NewEditIndex;
        //    listarProductos();
        //}

        //protected void dgvProductos_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        //{
        //    try
        //    {
        //        int id = Convert.ToInt32(dgvProductos.DataKeys[e.RowIndex].Value);
        //        GridViewRow fila = dgvProductos.Rows[e.RowIndex];

        //        TextBox texto = (TextBox)fila.Cells[0].Controls[0];
        //        string nuevoNombre = texto.Text.Trim();

        //        if (string.IsNullOrEmpty(nuevoNombre))
        //        {
        //            Response.Write("<script>alert('El nombre no puede estar vacío');</script>");
        //            return;
        //        }

        //        //productoNegocio.modificar(id, nuevoNombre);

        //        dgvProductos.EditIndex = -1;
        //        listarProductos();
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write("<script>alert('Error: " + ex.Message.Replace("'", "") + "');</script>");
        //    }
        //}

        //protected void dgvProductos_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        //{
        //    dgvProductos.EditIndex = -1;
        //    listarProductos();
        //}
    }
}