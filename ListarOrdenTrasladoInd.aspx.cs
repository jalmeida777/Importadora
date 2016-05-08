using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DevExpress.Web.ASPxGridView;

public partial class ListarOrdenTrasladoInd : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            txtFechaInicial.Text = DateTime.Now.ToShortDateString();
            txtFechaFinal.Text = DateTime.Now.ToShortDateString();

            lblFechaInicial.Text = DateTime.Parse(txtFechaInicial.Text).Year.ToString("0000") + DateTime.Parse(txtFechaInicial.Text).Month.ToString("00") + DateTime.Parse(txtFechaInicial.Text).Day.ToString("00");
            lblFechaFinal.Text = DateTime.Parse(txtFechaFinal.Text).Year.ToString("0000") + DateTime.Parse(txtFechaFinal.Text).Month.ToString("00") + DateTime.Parse(txtFechaFinal.Text).Day.ToString("00");
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("CrearOrdenTrasladoInd.aspx");
    }
    protected void btnSalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Principal.aspx");
    }
    protected void gvDocumento_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["i_IdOrdenTraslado"] = (sender as ASPxGridView).GetMasterRowKeyValue();
    }
    protected void gvProducto_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["i_IdOrdenTraslado"] = (sender as ASPxGridView).GetMasterRowKeyValue();
    }
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {

    }
}