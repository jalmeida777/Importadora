using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class AperturaCaja : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            lblFecha.Text = DateTime.Now.ToShortDateString();
            ListarSucursal();
            if (ValidarApertura() == true)
            {
                ibEstablecerSucursal.Visible = true;
            }
            else 
            {
                ibEstablecerSucursal.Visible = false;
                ddlTienda.Enabled = false;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'La caja ya está abierta para el día de hoy' });</script>", false);
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
                ibEstablecerSucursal.Visible = true;
            }
            else if (dtAlmacen.Rows.Count == 1) 
            {
                ddlTienda.Enabled = false;
                ibEstablecerSucursal.Visible = false;
            }
            else if (dtAlmacen.Rows.Count == 0)
            {
                ddlTienda.Enabled = false;
                ibEstablecerSucursal.Visible = false;
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Su sesión ha caducado. Vuelva a ingresar al sistema.' });</script>", false);
        }
    }

    bool ValidarApertura() 
    {
        string Almacen = ddlTienda.SelectedValue;
        string Año = DateTime.Now.Year.ToString();
        string Mes = DateTime.Now.Month.ToString();
        string Dia = DateTime.Now.Day.ToString();
        DataTable dtCaja = new DataTable();
        SqlDataAdapter daCaja = new SqlDataAdapter("Play_Valida_Caja " + Almacen + "," + Año + "," + Mes + "," + Dia, conexion);
        daCaja.Fill(dtCaja);
        int Existe = int.Parse(dtCaja.Rows[0]["Existe"].ToString());
        if (Existe == 0)
        {
            return true;
        }
        else if (Existe == 1)
        {
            return false;
        }
        else { return false; }
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["dtUsuario"] == null)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Su sesion ha caducado, vuelva a ingresar al sistema.' });</script>", false);
            return;
        }

        if (ValidarApertura() == false)
        {
            ibEstablecerSucursal.Visible = false;
            btnGuardar.Enabled = false;
            txtmonto.Enabled = false;
            ddlTienda.Enabled = false;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'La caja ya está abierta para el día de hoy' });</script>", false);
            return;
        }

        try
        {
            DataTable dtUsuario = new DataTable();
            dtUsuario = (DataTable)Session["dtUsuario"];
            string n_IdUsuario = dtUsuario.Rows[0]["n_IdUsuario"].ToString();

            string i_IdCaja = "";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Play_CajaHistorica_Registrar";
            cmd.Parameters.AddWithValue("@n_IdAlmacen", ddlTienda.SelectedValue);
            cmd.Parameters.AddWithValue("@f_CajaInicial", double.Parse(txtmonto.Text));
            cmd.Parameters.AddWithValue("@f_CajaFinal", double.Parse(txtmonto.Text));
            cmd.Parameters.AddWithValue("@n_IdUsuarioApertura", n_IdUsuario);

            conexion.Open();
            i_IdCaja = cmd.ExecuteScalar().ToString();
            conexion.Close();
            cmd.Dispose();

            btnGuardar.Enabled = false;
            txtmonto.Enabled = false;

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Caja Aperturada.' });</script>", false);


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.error({ message: '" + ex.Message + "' });</script>", false);
        }
    }

    protected void btnSalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Principal.aspx");
    }

    protected void ibEstablecerSucursal_Click(object sender, ImageClickEventArgs e)
    {
        ibEstablecerSucursal.Visible = false;
        ddlTienda.Enabled = false;
        btnGuardar.Enabled = true;
        txtmonto.Enabled = true;
    }
}