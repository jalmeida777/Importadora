using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class CanjearPuntos : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        if (txtDni.Text.Trim() == "") 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Ingrese el número de DNI' });</script>", false);
            txtDni.Focus();
            return;
        }
        SqlDataAdapter da = new SqlDataAdapter("POI_Cliente_ListarPuntos '" + txtDni.Text + "'", conexion);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            hfIdCliente.Value = dt.Rows[0]["n_IdCliente"].ToString();
            lblNombreCliente.Text = dt.Rows[0]["v_Nombre"].ToString();
            lblPuntos.Text = dt.Rows[0]["i_Puntos"].ToString();
        }
        else 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'El número de DNI ingresado no existe' });</script>", false);
        }
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        if (lblNombreCliente.Text.Trim() == "") 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Primero debe buscar un cliente' });</script>", false);
            txtDni.Focus();
            return;
        }
        if (txtCanjear.Text.Trim() == "") 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar los puntos a canjear' });</script>", false);
            txtCanjear.Focus();
            return;
        }
        if (int.Parse(txtCanjear.Text) <= 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar los puntos a canjear' });</script>", false);
            txtCanjear.Focus();
            return;
        }
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "POI_Cliente_RestarPuntos";
            cmd.Parameters.AddWithValue("@n_IdCliente", hfIdCliente.Value);
            cmd.Parameters.AddWithValue("@Cantidad", int.Parse(txtCanjear.Text));
            conexion.Open();
            cmd.ExecuteNonQuery();
            conexion.Close();

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Puntos Canjeados Satisfactoriamente' });</script>", false);
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
}