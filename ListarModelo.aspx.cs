using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class ListarModelo : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            Label lblTitulo = (Label)Master.FindControl("lblTitulo");
            if (lblTitulo != null)
            {
                lblTitulo.Text = "Administración de Modelos";
            }
            ListarMarca();
            Listar();
        }
        txtBuscar.Focus();
    }

    void ListarMarca()
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter("Play_Marca_Combo", conexion);
        da.Fill(dt);
        ddlMarca.DataSource = dt;
        ddlMarca.DataTextField = "v_DescripcionMarca";
        ddlMarca.DataValueField = "n_IdMarca";
        ddlMarca.DataBind();
        ddlMarca.SelectedIndex = 0;
    }

    void Listar()
    {
        string Estado = "";
        if (chkEstado.Checked) { Estado = "1"; } else { Estado = "0"; }

        string n_IdMarca = "";
        n_IdMarca = ddlMarca.SelectedValue.ToString();

        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter("Play_Modelo_Listar " + n_IdMarca + ",'" + txtBuscar.Text.Trim() + "'," + Estado, conexion);
        da.Fill(dt);
        gvModelo.DataSource = dt;
        gvModelo.DataBind();
    }

    protected void btnSalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Principal.aspx");
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("CrearModelo.aspx");
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Listar();
    }

    protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
    {
        Listar();
    }

    protected void chkEstado_CheckedChanged(object sender, EventArgs e)
    {
        Listar();
    }

    protected void gvModelo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int n_IdModelo = int.Parse(gvModelo.DataKeys[e.Row.RowIndex].Value.ToString());
            ImageButton btnEditar = e.Row.FindControl("btnEditar") as ImageButton;

            if (btnEditar != null)
            {
                btnEditar.PostBackUrl = "CrearModelo.aspx?n_IdModelo=" + n_IdModelo;
            }
        }
    }

    protected void txtBuscar_TextChanged(object sender, EventArgs e)
    {
        Listar();
    }
}