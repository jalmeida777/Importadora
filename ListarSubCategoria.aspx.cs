using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class ListarSubCategoria : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            Label lblTitulo = (Label)Master.FindControl("lblTitulo");
            if (lblTitulo != null)
            {
                lblTitulo.Text = "Administración de SubFamilias";
            }
            ListarCategoria();
            Listar();
        }
        txtBuscar.Focus();
    }

    void ListarCategoria()
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter("Play_Categoria_Combo", conexion);
        da.Fill(dt);
        ddlCategoria.DataSource = dt;
        ddlCategoria.DataTextField = "v_Descripcion";
        ddlCategoria.DataValueField = "n_IdCategoria";
        ddlCategoria.DataBind();
        ddlCategoria.SelectedIndex = 0;
    }

    void Listar()
    {
        string Estado = "";
        if (chkEstado.Checked) { Estado = "1"; } else { Estado = "0"; }

        string categoria = "";
        categoria = ddlCategoria.SelectedValue.ToString();

        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter("Play_SubCategoria_Listar " + categoria + ",'" + txtBuscar.Text.Trim() + "'," + Estado, conexion);
        da.Fill(dt);
        gvSubCategoria.DataSource = dt;
        gvSubCategoria.DataBind();
    }

    protected void btnSalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Principal.aspx");
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("CrearSubCategoria.aspx");
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Listar();
    }

    protected void chkEstado_CheckedChanged(object sender, EventArgs e)
    {
        Listar();
    }

    protected void gvSubCategoria_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int n_IdSubCategoria = int.Parse(gvSubCategoria.DataKeys[e.Row.RowIndex].Value.ToString());
            ImageButton btnEditar = e.Row.FindControl("btnEditar") as ImageButton;

            if (btnEditar != null)
            {
                btnEditar.PostBackUrl = "CrearSubCategoria.aspx?n_IdSubCategoria=" + n_IdSubCategoria;
            }
        }
    }

    protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
    {
        Listar();
    }

    protected void txtBuscar_TextChanged(object sender, EventArgs e)
    {
        Listar();
    }
}