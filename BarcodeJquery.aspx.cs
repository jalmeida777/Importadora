using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BarcodeJquery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSalir_Click(object sender, EventArgs e)
    {
        string n_IdProducto = Request.QueryString["n_IdProducto"];
        Response.Redirect("CrearProducto.aspx?n_IdProducto=" + n_IdProducto);
    }
}