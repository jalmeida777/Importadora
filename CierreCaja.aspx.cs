using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class CierreCaja : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            lblFecha.Text = DateTime.Now.ToShortDateString();
            ListarSucursal();

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

        if (ValidarApertura() == true)
        {
            ibEstablecerSucursal.Visible = false;
            btnGuardar.Enabled = false;
            txtmonto.Enabled = false;
            ddlTienda.Enabled = false;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'La caja no se ha aperturado para el día de hoy.' });</script>", false);
            return;
        }

        try
        {
            DataTable dtUsuario = new DataTable();
            dtUsuario = (DataTable)Session["dtUsuario"];
            string n_IdUsuario = dtUsuario.Rows[0]["n_IdUsuario"].ToString();
            string idAlmacen = ddlTienda.SelectedValue;

            string anio, mes, dia;
            anio = DateTime.Now.Year.ToString();
            mes = DateTime.Now.Month.ToString();
            dia = DateTime.Now.Day.ToString();

            //Reconocer el id de la caja segun el almacen y la fecha
            DataTable dtIdCaja = new DataTable();
            SqlDataAdapter daIdCaja = new SqlDataAdapter("Play_CajaHistorica_SelectID " + idAlmacen + "," + anio + "," + mes + "," + dia, conexion);
            daIdCaja.Fill(dtIdCaja);
            string idCaja = dtIdCaja.Rows[0]["i_IdCaja"].ToString();
            if (idCaja == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.error({ message: 'Primero debe aperturar la caja.' });</script>", false);
                return;
            }

            
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Play_CajaHistorica_RegistrarCierre";
            cmd.Parameters.AddWithValue("@f_CajaReal", double.Parse(txtmonto.Text));
            cmd.Parameters.AddWithValue("@n_IdUsuarioCierre", n_IdUsuario);
            cmd.Parameters.AddWithValue("@n_IdAlmacen", idAlmacen);
            cmd.Parameters.AddWithValue("@i_IdCaja", idCaja);
            conexion.Open();
            cmd.ExecuteNonQuery();
            conexion.Close();
            cmd.Dispose();

            btnGuardar.Enabled = false;
            txtmonto.Enabled = false;
            
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Caja Cerrada.' });</script>", false);


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
        if (ValidarApertura() == true)
        {
            ibEstablecerSucursal.Visible = false;
            btnGuardar.Enabled = false;
            txtmonto.Enabled = false;
            ddlTienda.Enabled = false;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'La caja no se ha aperturado para el día de hoy.' });</script>", false);
            return;
        }
        else
        {
            ibEstablecerSucursal.Visible = false;
            ddlTienda.Enabled = false;
            btnGuardar.Enabled = true;
            txtmonto.Enabled = true;
        }
    }
}