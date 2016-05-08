using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class CrearMarca : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {

            if (Request.QueryString["n_IdMarca"] != null)
            {
                int n_IdMarca = int.Parse(Request.QueryString["n_IdMarca"].ToString());
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("Play_Marca_Seleccionar " + n_IdMarca.ToString(), conexion);
                da.Fill(dt);
                lblCodigo.Text = n_IdMarca.ToString();
                txtCodigoInterno.Text = dt.Rows[0]["c_Codigo"].ToString();
                txtDescripcion.Text = dt.Rows[0]["v_DescripcionMarca"].ToString();
                chkEstado.Checked = bool.Parse(dt.Rows[0]["b_Estado"].ToString());
            }
            else
            {
            }

            txtDescripcion.Focus();
        }
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        if (txtDescripcion.Text == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar la descripción' });</script>", false);
            txtDescripcion.Focus();
            return;
        }
        if (txtCodigoInterno.Text == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar el código interno' });</script>", false);
            txtCodigoInterno.Focus();
            return;
        }
        try
        {


            if (lblCodigo.Text.Trim() != "")
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Play_Marca_Actualizar";
                cmd.Parameters.AddWithValue("@n_IdMarca", lblCodigo.Text);
                cmd.Parameters.AddWithValue("@c_Codigo", txtCodigoInterno.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_DescripcionMarca", txtDescripcion.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@b_Estado", chkEstado.Checked);
                conexion.Open();
                cmd.ExecuteNonQuery();
                conexion.Close();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Marca actualizada.' });</script>", false);
            }
            else
            {
                string n_IdMarca = "";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Play_Marca_Registrar";
                cmd.Parameters.AddWithValue("@c_Codigo", txtCodigoInterno.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_DescripcionMarca", txtDescripcion.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@b_Estado", chkEstado.Checked);
                conexion.Open();
                n_IdMarca = cmd.ExecuteScalar().ToString();
                conexion.Close();
                lblCodigo.Text = n_IdMarca;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Marca registrada.' });</script>", false);
            }

        }
        catch (Exception)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.error({ message: 'El código interno ya se encuentra en uso!' });</script>", false);
        }
    }

    protected void btnSalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("ListarMarca.aspx");
    }
}