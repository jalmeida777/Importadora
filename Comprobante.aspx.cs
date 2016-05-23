using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Text;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.Drawing;

public partial class Reportes_Comprobante : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Principal.aspx");
    }


}