using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class ListarBoletas : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            txtFechaInicial.Text = DateTime.Now.ToShortDateString();
            txtFechaFinal.Text = DateTime.Now.ToShortDateString();
            ListarSucursal();
            ddlAlmacen_SelectedIndexChanged(null, null);
            Listar();
        }
    }

    void Listar()
    {
        try
        {
            string FechaInicial = DateTime.Parse(txtFechaInicial.Text).Year.ToString("0000") + DateTime.Parse(txtFechaInicial.Text).Month.ToString("00") + DateTime.Parse(txtFechaInicial.Text).Day.ToString("00");
            string FechaFinal = DateTime.Parse(txtFechaFinal.Text).Year.ToString("0000") + DateTime.Parse(txtFechaFinal.Text).Month.ToString("00") + DateTime.Parse(txtFechaFinal.Text).Day.ToString("00");
            string Estado = "1";
            string Almacen = "";
            //if (ddlAlmacen.SelectedIndex == 0) { Almacen = ""; } else { Almacen = ddlAlmacen.SelectedItem.Text; }
            Almacen = ddlAlmacen.SelectedItem.Text;
            if (chkHabilitado.Checked) { Estado = "1"; } else { Estado = "0"; }

            string Comprobante = txtBoleta.Text.Trim();

            SqlDataAdapter da = new SqlDataAdapter("Play_Comprobante_Listar '" + FechaInicial + "','" + FechaFinal + "'," + Estado + ",'" + Almacen + "',3,'" + Comprobante + "'", conexion);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvPedido.DataSource = dt;
            gvPedido.DataBind();
        }
        catch (Exception)
        {

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
            //ddlAlmacen.Items.Insert(0, "TODOS");
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
        Listar();
    }

    protected void btnSalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Principal.aspx");
    }

    protected void gvPedido_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string n_IdPedido = gvPedido.DataKeys[e.Row.RowIndex].Value.ToString();

            LinkButton lbIdPedido = e.Row.FindControl("LinkButton1") as LinkButton;

            if (lbIdPedido != null)
            {
                lbIdPedido.PostBackUrl = "CrearPedido.aspx?n_IdPedido=" + n_IdPedido + "&td=3";
            }
        }
    }

    protected void ddlAlmacen_SelectedIndexChanged(object sender, EventArgs e)
    {
        Listar();
    }

    protected void chkHabilitado_CheckedChanged(object sender, EventArgs e)
    {
        Listar();
    }

    protected void txtBoleta_TextChanged(object sender, EventArgs e)
    {
        Listar();
    }
}