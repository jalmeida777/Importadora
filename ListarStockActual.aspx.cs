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

public partial class Procesos_ListarStockActual : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dtUsuario"] == null) 
        {
            Response.Redirect("Login.aspx");
        }
        if (Page.IsPostBack == false)
        {
            ListarSucursal();
        }
    }

    void ListarSucursal()
    {
        if (Session["dtAlmacenes"] != null)
        {
            DataTable dtAlmacen = new DataTable();
            dtAlmacen = (DataTable)Session["dtAlmacenes"];
            ddlAlmacen.DataSource = dtAlmacen;
            ddlAlmacen.DataTextField = "v_Descripcion";
            ddlAlmacen.DataValueField = "n_IdAlmacen";
            ddlAlmacen.DataBind();
            ddlAlmacen.SelectedIndex = 0;
            if (dtAlmacen.Rows.Count >= 1)
            {
                ddlAlmacen.Enabled = true;
            }
            else
            {
                ddlAlmacen.Enabled = false;
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Su sesión ha caducado. Vuelva a ingresar al sistema.' });</script>", false);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
    }

    protected void btnSalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Principal.aspx");
    }

    protected void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/ReporteStockActual.aspx");
    }

    protected void ASPxGridView1_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == DevExpress.Web.ASPxGridView.GridViewRowType.Data)
        {
            string n_IdProducto = e.GetValue("n_IdProducto").ToString();

            LinkButton LinkButton1 = new LinkButton();
            LinkButton1 = (LinkButton)ASPxGridView1.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)(ASPxGridView1.Columns[4]), "LinkButton1");

            LinkButton1.PostBackUrl = "ListarKardex.aspx?n_IdProducto=" + n_IdProducto + "&n_IdAlmacen=" + ddlAlmacen.SelectedValue + "&origen=sucursal";

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

    protected void btnOrdenCompra_Click(object sender, EventArgs e)
    {
        Response.Redirect("ListarOrdenCompra.aspx");
    }
    protected void btnNotaIngreso_Click(object sender, EventArgs e)
    {
        Response.Redirect("ListarNotaIngreso.aspx");
    }
    protected void btnNotaSalida_Click(object sender, EventArgs e)
    {
        Response.Redirect("ListarNotaSalida.aspx");
    }
    protected void btnTraslado_Click(object sender, EventArgs e)
    {
        Response.Redirect("ListarOrdenTrasladoInd.aspx");
    }
}