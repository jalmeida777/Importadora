using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;

public partial class Empresa : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false) 
        {
            SqlDataAdapter da = new SqlDataAdapter("Play_Empresa_Seleccionar", conexion);
            DataTable dt = new DataTable();
            da.Fill(dt);
            txtRazonSocial.Text = dt.Rows[0]["v_RazonSocial"].ToString();
            txtRuc.Text = dt.Rows[0]["c_Ruc"].ToString();
            txtDireccion.Text = dt.Rows[0]["v_Direccion"].ToString();
            txtTelefono.Text = dt.Rows[0]["v_Telefono"].ToString();
            lblRuta.Text = dt.Rows[0]["v_RutaLogo"].ToString();
            ibImagen.ImageUrl= lblRuta.Text;
        }
    }

    protected void btnSalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Principal.aspx");
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        if (txtRazonSocial.Text == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar la razón social' });</script>", false);
            txtRazonSocial.Focus();
            return;
        }
        if (txtRuc.Text == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar el número de RUC' });</script>", false);
            txtRuc.Focus();
            return;
        }
        if (txtRuc.Text.Length < 11)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar un número de RUC válido' });</script>", false);
            txtRuc.Focus();
            return;
        }
        if (txtDireccion.Text == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar la direción' });</script>", false);
            txtDireccion.Focus();
            return;
        }
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Play_Empresa_Actualizar";
            cmd.Parameters.AddWithValue("@v_RazonSocial", txtRazonSocial.Text.Trim().ToUpper());
            cmd.Parameters.AddWithValue("@c_Ruc", txtRuc.Text);
            cmd.Parameters.AddWithValue("@v_RutaLogo", lblRuta.Text);
            cmd.Parameters.AddWithValue("@v_Direccion", txtDireccion.Text.Trim().ToUpper());
            cmd.Parameters.AddWithValue("@v_Telefono", txtTelefono.Text.Trim());
            conexion.Open();
            cmd.ExecuteNonQuery();
            conexion.Close();

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Empresa actualizada' });</script>", false);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.error({ message: '" + ex.Message + "' });</script>", false);
        }
    }

    protected void ibUpload_Click(object sender, ImageClickEventArgs e)
    {
        string filename = Path.GetFileName(fu1.FileName);
        fu1.SaveAs(Server.MapPath("~/Empresa/") + filename);
        ibImagen.ImageUrl = "~/Empresa/" + filename;
        lblRuta.Text = "~/Empresa/" + filename;
    }
}