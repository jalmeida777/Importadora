using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Reporting.WebForms;

public partial class Reportes_Facturas : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            ListarSucursal();
            txtFechaInicial.Text = DateTime.Now.ToShortDateString();
            txtFechaFinal.Text = DateTime.Now.ToShortDateString();
            lblFechaInicial.Text = DateTime.Parse(txtFechaInicial.Text).Year.ToString("0000") + DateTime.Parse(txtFechaInicial.Text).Month.ToString("00") + DateTime.Parse(txtFechaInicial.Text).Day.ToString("00");
            lblFechaFinal.Text = DateTime.Parse(txtFechaFinal.Text).Year.ToString("0000") + DateTime.Parse(txtFechaFinal.Text).Month.ToString("00") + DateTime.Parse(txtFechaFinal.Text).Day.ToString("00");
            if (ddlAlmacen.SelectedIndex == 0)
            {
                lblIdAlmacen.Text = "%";
            }
            else { lblIdAlmacen.Text = ddlAlmacen.SelectedItem.Text; }
            
            
            ReportViewer1.LocalReport.ReportPath = "Facturas.rdlc";
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DsFactura";
            SqlDataAdapter da = new SqlDataAdapter("play_factura_reporte '" + lblIdAlmacen.Text + "','" + lblFechaInicial.Text + "','" + lblFechaFinal.Text + "'", conexion);
            DataTable dt = new DataTable();
            da.Fill(dt);
            rds.Value = dt;
            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.LocalReport.Refresh();
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
            ddlAlmacen.Items.Insert(0, "Todos");
            ddlAlmacen.SelectedIndex = 0;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Su sesión ha caducado. Vuelva a ingresar al sistema.' });</script>", false);
        }
    }

    protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
    {
        lblFechaInicial.Text = DateTime.Parse(txtFechaInicial.Text).Year.ToString("0000") + DateTime.Parse(txtFechaInicial.Text).Month.ToString("00") + DateTime.Parse(txtFechaInicial.Text).Day.ToString("00");
        lblFechaFinal.Text = DateTime.Parse(txtFechaFinal.Text).Year.ToString("0000") + DateTime.Parse(txtFechaFinal.Text).Month.ToString("00") + DateTime.Parse(txtFechaFinal.Text).Day.ToString("00");
        if (ddlAlmacen.SelectedIndex == 0)
        {
            lblIdAlmacen.Text = "%";
        }
        else { lblIdAlmacen.Text = ddlAlmacen.SelectedItem.Text; }


        ReportViewer1.LocalReport.ReportPath = "Facturas.rdlc";
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportDataSource rds = new ReportDataSource();
        rds.Name = "DsFactura";
        SqlDataAdapter da = new SqlDataAdapter("play_factura_reporte '" + lblIdAlmacen.Text + "','" + lblFechaInicial.Text + "','" + lblFechaFinal.Text + "'", conexion);
        DataTable dt = new DataTable();
        da.Fill(dt);
        rds.Value = dt;
        ReportViewer1.LocalReport.DataSources.Add(rds);
        ReportViewer1.LocalReport.Refresh();
    }

    protected void btnSalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Principal.aspx");
    }
}