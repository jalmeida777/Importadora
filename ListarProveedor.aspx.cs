using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class ListarProveedor : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            Label lblTitulo = (Label)Master.FindControl("lblTitulo");
            if (lblTitulo != null)
            {
                lblTitulo.Text = "Administración de Proveedores";
            }
            Listar();
        }
        txtBuscar.Focus();
    }

    void Listar()
    {
        string Estado = "";
        if (chkEstado.Checked) { Estado = "1"; } else { Estado = "0"; }
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter("Play_Proveedor_Listar '" + txtBuscar.Text.Trim() + "'," + Estado, conexion);
        da.Fill(dt);
        gvProveedor.DataSource = dt;
        gvProveedor.DataBind();
    }

    protected void chkEstado_CheckedChanged(object sender, EventArgs e)
    {
        Listar();
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Listar();
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("CrearProveedor.aspx");
    }

    protected void btnSalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Principal.aspx");
    }

    protected void gvProveedor_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int n_IdProveedor = int.Parse(gvProveedor.DataKeys[e.Row.RowIndex].Value.ToString());
            ImageButton ibEditar = e.Row.FindControl("ibEditar") as ImageButton;

            if (ibEditar != null)
            {
                ibEditar.PostBackUrl = "CrearProveedor.aspx?n_IdProveedor=" + n_IdProveedor;
            }


        }
    }

    protected void txtBuscar_TextChanged(object sender, EventArgs e)
    {
        Listar();
    }
}