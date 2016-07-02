using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class ListarNotaIngreso : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            txtFechaInicial.Text = DateTime.Now.ToShortDateString();
            txtFechaFinal.Text = DateTime.Now.ToShortDateString();
            ListarAlmacen();
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
            if (chkHabilitado.Checked){Estado = "1";}else{Estado = "0";}
            string Almacen = ddlAlmacen.SelectedItem.Text;

            SqlDataAdapter da = new SqlDataAdapter("Play_NotaIngreso_Listar '" + FechaInicial + "','" + FechaFinal + "','" + Almacen + "'," + Estado, conexion);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvNotaIngreso.DataSource = dt;
            gvNotaIngreso.DataBind();
        }
        catch (Exception)
        {

        }
    }

    public void ListarAlmacen()
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
            if (dtAlmacen.Rows.Count > 1)
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
            BloquearTodo();
        }
    }

    void BloquearTodo()
    {
        txtFechaInicial.Enabled = false;
        txtFechaFinal.Enabled = false;
        ddlAlmacen.Enabled = false;
        chkHabilitado.Enabled = false;
        btnConsultar.Enabled = false;
        btnNuevo.Enabled = false;
        btnSalir.Enabled = false;
        gvNotaIngreso.Enabled = false;
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Listar();
    }
    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("CrearNotaIngreso.aspx");
    }
    protected void btnSalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("ListarStockActual.aspx");
    }
    protected void gvNotaIngreso_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string n_IdNotaIngreso = gvNotaIngreso.DataKeys[e.Row.RowIndex].Value.ToString();
                string motivo = "";
                if (e.Row.Cells[3].Text.Trim() == "INVENTARIO INICIAL")
                {
                    motivo = "II";
                }

                LinkButton lbIdNotaIngreso = e.Row.FindControl("LinkButton1") as LinkButton;

                if (lbIdNotaIngreso != null && motivo == "")
                {
                    lbIdNotaIngreso.PostBackUrl = "CrearNotaIngreso.aspx?n_IdNotaIngreso=" + n_IdNotaIngreso;
                }
                else if (lbIdNotaIngreso != null && motivo == "II")
                {
                    lbIdNotaIngreso.PostBackUrl = "CrearInventarioInicial.aspx?n_IdNotaIngreso=" + n_IdNotaIngreso;
                }
            }
        }
        catch (Exception)
        {
            
        }

    }

}