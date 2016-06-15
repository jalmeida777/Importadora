using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class FlujoCajaDiaria : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            txtFecha.Text = DateTime.Now.ToShortDateString();
            ListarSucursal();

        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["dtUsuario"] != null)
        {
            DataTable dtUsuario = new DataTable();
            dtUsuario = (DataTable)Session["dtUsuario"];
            string n_IdUsuario = dtUsuario.Rows[0]["n_IdUsuario"].ToString();

            string idAlmacen = ddlTienda.SelectedValue;

            string anio, mes, dia;

            anio = DateTime.Parse(txtFecha.Text).Year.ToString("0000");
            mes = DateTime.Parse(txtFecha.Text).Month.ToString("00");
            dia = DateTime.Parse(txtFecha.Text).Day.ToString("00");

            DataTable dtIdCaja = new DataTable();
            SqlDataAdapter daIdCaja = new SqlDataAdapter("Play_CajaHistorica_SelectID " + idAlmacen + "," + anio + "," + mes + "," + dia, conexion);
            daIdCaja.Fill(dtIdCaja);

            if (dtIdCaja.Rows.Count > 0)
            {
                string idCaja = dtIdCaja.Rows[0]["i_IdCaja"].ToString();

                SqlDataAdapter da = new SqlDataAdapter("Play_FlujoCaja_Listar " + idCaja, conexion);
                DataTable dt = new DataTable();
                da.Fill(dt);

                lblCajaInicial.Text = dt.Rows[0]["f_CajaInicial"].ToString();
                lblTotalIngresos.Text = dt.Rows[0]["f_IngresoAdicional"].ToString();
                lblTotalSalidas.Text = dt.Rows[0]["f_SalidaAdicional"].ToString();
                lblTotalVentas.Text = dt.Rows[0]["f_TotalVenta"].ToString();
                lblCajaFinal.Text = dt.Rows[0]["f_CajaFinal"].ToString();
                lblCajaReal.Text = dt.Rows[0]["f_CajaReal"].ToString();


                DataTable dt2 = new DataTable();
                SqlDataAdapter da2 = new SqlDataAdapter("Play_CajaMovimiento_Listar " + idCaja, conexion);
                da2.Fill(dt2);
                gvReciboCaja.DataSource = dt2;
                gvReciboCaja.DataBind();

            }
            else 
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'La caja no está aperturada para el día " + txtFecha.Text + "' });</script>", false);

            }
        }
    }

    void ListarSucursal()
    {
        if (Session["dtAlmacenes"] != null)
        {
            DataTable dtAlmacen = new DataTable();
            dtAlmacen = (DataTable)Session["dtAlmacenes"];
            ddlTienda.DataSource = dtAlmacen;
            ddlTienda.DataTextField = "v_Descripcion";
            ddlTienda.DataValueField = "n_IdAlmacen";
            ddlTienda.DataBind();
            ddlTienda.SelectedIndex = 0;
            if (dtAlmacen.Rows.Count > 1)
            {
                ddlTienda.Enabled = true;
            }
            else
            {
                ddlTienda.Enabled = false;
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Su sesión ha caducado. Vuelva a ingresar al sistema.' });</script>", false);
        }
    }

    protected void btnSalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Principal.aspx");
    }
}