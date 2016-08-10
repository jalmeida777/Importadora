using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class CrearProveedor : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            if (Request.QueryString["n_IdProveedor"] != null)
            {
                int n_IdProveedor = int.Parse(Request.QueryString["n_IdProveedor"].ToString());
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("Play_Proveedor_Seleccionar " + n_IdProveedor.ToString(), conexion);
                da.Fill(dt);
                lblCodigo.Text = n_IdProveedor.ToString();
                txtRuc.Text = dt.Rows[0]["v_Ruc"].ToString();
                txtNombre.Text = dt.Rows[0]["v_Nombre"].ToString();
                txtTelefono.Text = dt.Rows[0]["v_Telefono"].ToString();
                txtDireccion.Text = dt.Rows[0]["v_Direccion"].ToString();
                txtContacto.Text = dt.Rows[0]["v_Contacto"].ToString();
                txtEmail.Text = dt.Rows[0]["v_Email"].ToString();
                chkEstado.Checked = bool.Parse(dt.Rows[0]["b_Estado"].ToString());
            }
            else
            {
            }

            txtNombre.Focus();
        }
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        if (txtNombre.Text == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar el nombre' });</script>", false);
            txtNombre.Focus();
            return;
        }

        try
        {
            if (lblCodigo.Text.Trim() != "")
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Play_Proveedor_Actualizar";
                cmd.Parameters.AddWithValue("@n_IdProveedor", lblCodigo.Text);
                cmd.Parameters.AddWithValue("@v_Ruc", txtRuc.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Nombre", txtNombre.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Telefono", txtTelefono.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Direccion", txtDireccion.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Contacto", txtContacto.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Email", txtEmail.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@b_Estado", chkEstado.Checked);
                conexion.Open();
                cmd.ExecuteNonQuery();
                conexion.Close();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Proveedor actualizado.' });</script>", false);
            }
            else
            {
                string n_IdProveedor = "";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Play_Proveedor_Registrar";
                cmd.Parameters.AddWithValue("@v_Ruc", txtRuc.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Nombre", txtNombre.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Telefono", txtTelefono.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Direccion", txtDireccion.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Contacto", txtContacto.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Email", txtEmail.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@b_Estado", chkEstado.Checked);
                conexion.Open();
                n_IdProveedor = cmd.ExecuteScalar().ToString();
                conexion.Close();
                lblCodigo.Text = n_IdProveedor;
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Proveedor registrado.' });</script>", false);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>window.parent.CerrarDialog('proveedores'," + n_IdProveedor.ToString() + ");</script>", false);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.error({ message: '"+ ex.Message +"' });</script>", false);
        }
    }

    protected void btnSalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("ListarProveedor.aspx");
    }
}