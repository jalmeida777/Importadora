using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reportes_ReporteStockGlobal : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            ReportViewer1.LocalReport.Refresh();
        }
    }

    protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
    {
        ReportViewer1.LocalReport.Refresh();
    }

    protected void btnSalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Principal.aspx");
    }
}