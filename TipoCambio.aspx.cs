using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;

public partial class TipoCambio : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false) 
        {
            lblFecha.Text = DateTime.Now.ToShortDateString();
            int año = DateTime.Now.Year;
            int mes = DateTime.Now.Month;
            int dia = DateTime.Now.Day;

            //1) Verificar que no exista tipo de cambio para este día.
            bool existe = false;
            SqlDataAdapter da = new SqlDataAdapter("Play_TC_Existencia " + año + "," + mes + "," + dia, conexion);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    existe = true;
                }
                else
                {
                    existe = false;
                }
            }
            else
            {
                existe = false;
            }

            if (existe == true)
            {
                txtTipoCambio.Text = dt.Rows[0]["f_TC"].ToString();
                txtTipoCambioPlay.Text = dt.Rows[0]["f_TCPlay"].ToString();
                txtTipoCambio.Enabled = false;
                txtTipoCambioPlay.Enabled = false;
                btnGuardar.Enabled = false;
            }
            else 
            {
                //2) Mostrar por defecto el último tipo de cambio registrado.
                existe = false;
                SqlDataAdapter da2 = new SqlDataAdapter("Play_TC_UltimoTC_Select", conexion);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                if (dt2 != null)
                {
                    if (dt2.Rows.Count > 0)
                    {
                        existe = true;
                    }
                    else
                    {
                        existe = false;
                    }
                }
                else
                {
                    existe = false;
                }

                if (existe == true)
                {
                    txtTipoCambio.Text = dt2.Rows[0]["f_TC"].ToString();
                    txtTipoCambioPlay.Text = dt2.Rows[0]["f_TCPlay"].ToString();
                    txtTipoCambio.Enabled = true;
                    txtTipoCambioPlay.Enabled = true;
                    btnGuardar.Enabled = true;
                }
                else
                {
                    txtTipoCambio.Text = "0.00";
                    txtTipoCambioPlay.Text = "0.00";
                    txtTipoCambio.Enabled = true;
                    txtTipoCambioPlay.Enabled = true;
                    btnGuardar.Enabled = true;
                    txtTipoCambio.Focus();
                }
            }

        }
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        if (txtTipoCambio.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar el tipo de cambio' });</script>", false);
            txtTipoCambio.Focus();
            return;
        }
        if (txtTipoCambioPlay.Text.Trim() == "") 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar el tipo de cambio play' });</script>", false);
            txtTipoCambioPlay.Focus();
            return;
        }
        //Validar que no registre ceros

        if (double.Parse(txtTipoCambio.Text.Trim()) == 0) 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'El tipo de cambio debe ser mayor a cero' });</script>", false);
            txtTipoCambio.Focus();
            return;
        }

        if (double.Parse(txtTipoCambioPlay.Text.Trim()) == 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'El tipo de cambio play debe ser mayor a cero' });</script>", false);
            txtTipoCambioPlay.Focus();
            return;
        }

        int año = DateTime.Now.Year;
        int mes = DateTime.Now.Month;
        int dia = DateTime.Now.Day;
        bool existe = false;

        //1) Verificar que no exista tipo de cambio para este día.
        SqlDataAdapter da = new SqlDataAdapter("Play_TC_Existencia " + año + "," + mes + "," + dia, conexion);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                existe = true;
            }
            else 
            {
                existe = false;
            }
        }
        else 
        {
            existe = false;
        }

        if (existe == true)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Ya existe el tipo de cambio para el " + DateTime.Now.ToShortDateString() + "' });</script>", false);
            return;
        }

        
        try 
	    {	        
		    SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Play_TC_Registrar";
            cmd.Parameters.AddWithValue("@i_Anio", año);
            cmd.Parameters.AddWithValue("@i_Mes", mes);
            cmd.Parameters.AddWithValue("@i_Dia", dia);
            cmd.Parameters.AddWithValue("@f_TC", txtTipoCambio.Text.Trim());
            cmd.Parameters.AddWithValue("@f_TCPlay", txtTipoCambioPlay.Text.Trim());
            conexion.Open();
            cmd.ExecuteNonQuery();
            conexion.Close();

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Tipo de Cambio Registrado' });</script>", false);
            DataTable dtUsuario = new DataTable();
            dtUsuario = (DataTable)Session["dtUsuario"];
            dtUsuario.Rows[0]["f_TC"] = double.Parse(txtTipoCambio.Text.Trim());
            dtUsuario.Rows[0]["f_TCPlay"] = double.Parse(txtTipoCambioPlay.Text.Trim());
            Session["dtUsuario"] = dtUsuario;
            string Usuario = dtUsuario.Rows[0]["v_Usuario"].ToString();
            FormsAuthentication.RedirectFromLoginPage(Usuario, false);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.error({ message: '" + ex.Message + "' });</script>", false);
        }

    }

    protected void btnSalir_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dtUsuario = new DataTable();
        dtUsuario = (DataTable)Session["dtUsuario"];
        string Usuario = dtUsuario.Rows[0]["v_Usuario"].ToString();
        FormsAuthentication.RedirectFromLoginPage(Usuario, false);
    }
}