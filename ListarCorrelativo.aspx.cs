using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class ListarCorrelativo : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            Label lblTitulo = (Label)Master.FindControl("lblTitulo");
            if (lblTitulo != null)
            {
                lblTitulo.Text = "Administración de Correlativos";
            }
            ListarSucursal();
            Listar();
        }
    }

    void Listar()
    {
        string n_IdAlmacen = ddlSucursal.SelectedValue;
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter("Play_Correlativo_Listar " + n_IdAlmacen, conexion);
        da.Fill(dt);
        gv.DataSource = dt;
        gv.DataBind();
    }

    void ListarSucursal()
    {
        if (Session["dtAlmacenes"] != null)
        {
            DataTable dtAlmacen = new DataTable();
            dtAlmacen = (DataTable)Session["dtAlmacenes"];
            ddlSucursal.DataSource = dtAlmacen;
            ddlSucursal.DataTextField = "v_Descripcion";
            ddlSucursal.DataValueField = "n_IdAlmacen";
            ddlSucursal.DataBind();
            ddlSucursal.SelectedIndex = 0;
            if (dtAlmacen.Rows.Count > 1)
            {
                ddlSucursal.Enabled = true;
            }
            else
            {
                ddlSucursal.Enabled = false;
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Su sesión ha caducado. Vuelva a ingresar al sistema.' });</script>", false);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Listar();
    }
   
    protected void btnSalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Principal.aspx");
    }

    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int i_IdCorrelativo = int.Parse(gv.DataKeys[e.Row.RowIndex].Value.ToString());
            ImageButton btnEditar = e.Row.FindControl("btnEditar") as ImageButton;

            if (btnEditar != null)
            {
                btnEditar.PostBackUrl = "CrearCorrelativo.aspx?i_IdCorrelativo=" + i_IdCorrelativo;
            }
        }
    }

    protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
    {
        Listar();
    }
}