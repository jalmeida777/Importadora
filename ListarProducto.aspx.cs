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

public partial class ListarProducto : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dtUsuario"] == null) 
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void btnSalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Principal.aspx");
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("CrearProducto.aspx");
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void ASPxGridView1_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == DevExpress.Web.ASPxGridView.GridViewRowType.Data)
        {
            string n_IdProducto = e.GetValue("n_IdProducto").ToString();

            LinkButton lbProducto = new LinkButton();
            lbProducto = (LinkButton)ASPxGridView1.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)(ASPxGridView1.Columns[2]), "lbProducto");

            lbProducto.PostBackUrl = "CrearProducto.aspx?n_IdProducto=" + n_IdProducto;
        }
    }

    protected void callbackPanel_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        int n_IdProducto = Convert.ToInt32(e.Parameter);
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter("select v_RutaImagen,v_CodigoInterno,v_Descripcion from producto where n_IdProducto=" + n_IdProducto, conexion);
        da.Fill(dt);
        lblCodigo.Text = dt.Rows[0]["v_CodigoInterno"].ToString();
        lblProducto.Text = dt.Rows[0]["v_Descripcion"].ToString();
        ImagenGrande.ImageUrl = dt.Rows[0]["v_RutaImagen"].ToString();
    }
}


