using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class ListarNotaSalida : System.Web.UI.Page
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
        gvNotaSalida.Enabled = false;
    }

    void Listar()
    {
        try
        {
            string FechaInicial = DateTime.Parse(txtFechaInicial.Text).Year.ToString("0000") + DateTime.Parse(txtFechaInicial.Text).Month.ToString("00") + DateTime.Parse(txtFechaInicial.Text).Day.ToString("00");
            string FechaFinal = DateTime.Parse(txtFechaFinal.Text).Year.ToString("0000") + DateTime.Parse(txtFechaFinal.Text).Month.ToString("00") + DateTime.Parse(txtFechaFinal.Text).Day.ToString("00");
            string Estado = "1";
            if (chkHabilitado.Checked) { Estado = "1"; } else { Estado = "0"; }
            string Almacen = ddlAlmacen.SelectedItem.Text;
            string Producto = txtProducto.Text.Trim();

            SqlDataAdapter da = new SqlDataAdapter("Play_NotaSalida_Listar '" + FechaInicial + "','" + FechaFinal + "','" + Almacen + "','" + Producto + "'," + Estado, conexion);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvNotaSalida.DataSource = dt;
            gvNotaSalida.DataBind();
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
        Response.Redirect("CrearNotaSalida.aspx");
    }

    protected void btnSalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Principal.aspx");
    }

    protected void gvNotaSalida_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string n_IdNotaSalida = gvNotaSalida.DataKeys[e.Row.RowIndex].Value.ToString();

            LinkButton lbIdNotaSalida = e.Row.FindControl("LinkButton1") as LinkButton;

            if (lbIdNotaSalida != null)
            {
                lbIdNotaSalida.PostBackUrl = "CrearNotaSalida.aspx?n_IdNotaSalida=" + n_IdNotaSalida;
            }
        }
    }


}