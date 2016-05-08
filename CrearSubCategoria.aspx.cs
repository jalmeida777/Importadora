using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class CrearSubCategoria : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {

            ListarCategoria();

            if (Request.QueryString["n_IdSubCategoria"] != null)
            {
                int n_IdSubCategoria = int.Parse(Request.QueryString["n_IdSubCategoria"].ToString());
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("Play_SubCategoria_Seleccionar " + n_IdSubCategoria.ToString(), conexion);
                da.Fill(dt);
                lblCodigo.Text = n_IdSubCategoria.ToString();
                txtCodigoInterno.Text = dt.Rows[0]["c_Codigo"].ToString();
                txtDescripcion.Text = dt.Rows[0]["v_Descripcion"].ToString();
                chkEstado.Checked = bool.Parse(dt.Rows[0]["b_Estado"].ToString());
                ddlCategoria.SelectedValue = dt.Rows[0]["n_IdCategoria"].ToString();
            }
            else
            {
            }

            txtDescripcion.Focus();
        }
    }

    void ListarCategoria()
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter("Play_Categoria_Combo", conexion);
        da.Fill(dt);
        ddlCategoria.DataSource = dt;
        ddlCategoria.DataTextField = "v_Descripcion";
        ddlCategoria.DataValueField = "n_IdCategoria";
        ddlCategoria.DataBind();
        ddlCategoria.SelectedIndex = 0;
    }

    protected void btnSalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("ListarSubCategoria.aspx");
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
                cmd.CommandText = "Play_SubCategoria_Actualizar";
                cmd.Parameters.AddWithValue("@n_IdSubCategoria", lblCodigo.Text);
                cmd.Parameters.AddWithValue("@n_IdCategoria", ddlCategoria.SelectedValue);
                cmd.Parameters.AddWithValue("@c_Codigo", txtCodigoInterno.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Descripcion", txtDescripcion.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@b_Estado", chkEstado.Checked);
                conexion.Open();
                cmd.ExecuteNonQuery();
                conexion.Close();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Subfamilia actualizada.' });</script>", false);
            }
            else
            {
                string n_IdSubCategoria = "";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Play_SubCategoria_Insertar";
                cmd.Parameters.AddWithValue("@n_IdCategoria", ddlCategoria.SelectedValue);
                cmd.Parameters.AddWithValue("@c_Codigo", txtCodigoInterno.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Descripcion", txtDescripcion.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@b_Estado", chkEstado.Checked);
                conexion.Open();
                n_IdSubCategoria = cmd.ExecuteScalar().ToString();
                conexion.Close();
                lblCodigo.Text = n_IdSubCategoria;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Subfamilia registrada.' });</script>", false);
            }

        }
        catch (Exception)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.error({ message: 'El código interno ya se encuentra en uso!' });</script>", false);
        }
    }
}