using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class CrearAlmacen : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {

            if (Request.QueryString["n_IdAlmacen"] != null)
            {
                int n_IdAlmacen = int.Parse(Request.QueryString["n_IdAlmacen"].ToString());
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("Play_Almacen_Seleccionar " + n_IdAlmacen.ToString(), conexion);
                da.Fill(dt);
                lblCodigo.Text = n_IdAlmacen.ToString();
                txtCodigoInterno.Text = dt.Rows[0]["c_Codigo"].ToString();
                txtDescripcion.Text = dt.Rows[0]["v_Descripcion"].ToString();
                chkEstado.Checked = bool.Parse(dt.Rows[0]["b_Estado"].ToString());
                txtDireccion.Text = dt.Rows[0]["v_Direccion"].ToString();
                txtTelefono.Text = dt.Rows[0]["v_Telefono"].ToString();
            }

            txtDescripcion.Focus();
        }
    }
    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        if (txtCodigoInterno.Text == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar el código interno' });</script>", false);
            txtCodigoInterno.Focus();
            return;
        }
        if (txtDescripcion.Text == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar la descripción' });</script>", false);
            txtDescripcion.Focus();
            return;
        }
        if (txtDireccion.Text == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar la dirección' });</script>", false);
            txtDireccion.Focus();
            return;
        }
        try
        {
            if (lblCodigo.Text.Trim() != "")
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Play_Almacen_Actualizar";
                cmd.Parameters.AddWithValue("@n_IdAlmacen", lblCodigo.Text);
                cmd.Parameters.AddWithValue("@c_Codigo", txtCodigoInterno.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Descripcion", txtDescripcion.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@b_Estado", chkEstado.Checked);
                cmd.Parameters.AddWithValue("@v_Direccion", txtDireccion.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Telefono", txtTelefono.Text.Trim().ToUpper());
                conexion.Open();
                cmd.ExecuteNonQuery();
                conexion.Close();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Sucursal actualizada satisfactoriamente' });</script>", false);
            }
            else 
            {
                //Crear Sucursal
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Play_Almacen_Registrar";
                cmd.Parameters.AddWithValue("@c_Codigo", txtCodigoInterno.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Descripcion", txtDescripcion.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Direccion", txtDireccion.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Telefono", txtTelefono.Text.Trim().ToUpper());
                conexion.Open();
                cmd.ExecuteNonQuery();
                conexion.Close();
                btnGuardar.Enabled = false;
                txtCodigoInterno.Enabled = false;
                txtDescripcion.Enabled = false;
                chkEstado.Enabled = false;

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Sucursal registrada satisfactoriamente' });</script>", false);
            }
        }
        catch (Exception)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.error({ message: 'El código interno ya se encuentra en uso!' });</script>", false);
        }


    }
    protected void btnSalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("ListarAlmacen.aspx");
    }
}