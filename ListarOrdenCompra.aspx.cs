using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
public partial class ListarOrdenCompra : System.Web.UI.Page
{

    SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            txtFechaInicial.Text = DateTime.Now.ToShortDateString();
            txtFechaFinal.Text = DateTime.Now.ToShortDateString();
            ListarEstados();
            Listar();
        }
    }

    void ListarEstados() 
    {
        DataTable dtEstados = new DataTable();
        SqlDataAdapter daEstados = new SqlDataAdapter("Play_OrdenCompraEstado_Combo", conexion);
        daEstados.Fill(dtEstados);
        ddlEstado.DataSource = dtEstados;
        ddlEstado.DataTextField = "v_DescripcionEstado";
        ddlEstado.DataValueField = "i_IdOrdenCompraEstado";
        ddlEstado.DataBind();
        ddlEstado.Items.Insert(0, "TODOS");
        ddlEstado.SelectedIndex = 0;
    }

    void Listar()
    {
        try
        {
            string FechaInicial = DateTime.Parse(txtFechaInicial.Text).Year.ToString("0000") + DateTime.Parse(txtFechaInicial.Text).Month.ToString("00") + DateTime.Parse(txtFechaInicial.Text).Day.ToString("00");
            string FechaFinal = DateTime.Parse(txtFechaFinal.Text).Year.ToString("0000") + DateTime.Parse(txtFechaFinal.Text).Month.ToString("00") + DateTime.Parse(txtFechaFinal.Text).Day.ToString("00");
            string Estado = "%";
            if (ddlEstado.SelectedIndex == 0)
            {
                Estado = "%";
            }
            else 
            {
                Estado = ddlEstado.SelectedItem.Text;
            }

            SqlDataAdapter da = new SqlDataAdapter("Play_OrdenCompra_Listar '" + FechaInicial + "','" + FechaFinal + "','" + Estado + "'", conexion);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvOrdenCompra.DataSource = dt;
            gvOrdenCompra.DataBind();
        }
        catch (Exception)
        {

        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Listar();
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("CrearOrdenCompra.aspx");
    }

    protected void btnSalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Principal.aspx");
    }

    protected void gvOrdenCompra_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int i_IdOrdenCompra = int.Parse(gvOrdenCompra.DataKeys[e.Row.RowIndex].Value.ToString());
            LinkButton LinkButton1 = e.Row.FindControl("LinkButton1") as LinkButton;

            if (LinkButton1 != null) 
            {
                LinkButton1.PostBackUrl = "CrearOrdenCompra.aspx?i_IdOrdenCompra=" + i_IdOrdenCompra;
            }
        }
    }
}