using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;


public partial class CrearCliente : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            ListarDistrito();
            txtfecump.Text = DateTime.Now.ToShortDateString();

            if (Request.QueryString["n_IdCliente"] != null)
            {
                string n_IdCliente = Request.QueryString["n_IdCliente"];
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("Play_Cliente2_Seleccionar " + n_IdCliente, conexion);
                da.Fill(dt);
                lblCodigo.Text = n_IdCliente;
                txtNombre.Text = dt.Rows[0]["v_Nombre"].ToString();
                txtNumeroDocumento.Text = dt.Rows[0]["v_DocumentoIdentidad"].ToString();
                txtDireccion.Text = dt.Rows[0]["v_Direccion"].ToString();
                txtTelefono.Text = dt.Rows[0]["v_Telefono"].ToString();
                txtEmail.Text = dt.Rows[0]["v_Email"].ToString();
                txtFacebook.Text = dt.Rows[0]["v_Facebook"].ToString();
                txtContacto.Text = dt.Rows[0]["v_Contacto"].ToString();
                txtCelular.Text = dt.Rows[0]["v_Celular"].ToString();
                if (dt.Rows[0]["d_FechaNacimiento"].ToString() != "")
                {
                    txtfecump.Text = DateTime.Parse(dt.Rows[0]["d_FechaNacimiento"].ToString()).ToShortDateString();
                }
                rblSexo.SelectedValue = dt.Rows[0]["c_Genero"].ToString();
                ddlDistrito.SelectedValue = dt.Rows[0]["v_Distrito"].ToString();
                txtComentario.Text = dt.Rows[0]["t_Comentario"].ToString();
                chkEstado.Checked = bool.Parse(dt.Rows[0]["b_Estado"].ToString());

                txtNombre.Focus();
            }
        }
    }

    void ListarDistrito()
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter("Play_Distrito_Combo", conexion);
        da.Fill(dt);
        ddlDistrito.DataSource = dt;
        ddlDistrito.DataTextField = "v_NombreDistrito";
        ddlDistrito.DataValueField = "v_NombreDistrito";
        ddlDistrito.DataBind();
        ddlDistrito.SelectedIndex = 0;
    }

    protected void btnSalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("ListarCliente.aspx");
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        if (txtNombre.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar el nombre del cliente.' });</script>", false);
            txtNombre.Focus();
            return;
        }
        if (txtfecump.Text.Trim() == "") 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar una fecha de cumpleaños válida.' });</script>", false);
            txtfecump.Focus();
            return;
        }

        try
        {
            if (lblCodigo.Text.Trim() != "")
            {
                //Actualizar datos del cliente
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Play_Cliente2_Actualizar";
                cmd.Parameters.AddWithValue("@n_IdCliente", lblCodigo.Text);
                cmd.Parameters.AddWithValue("@v_Nombre", txtNombre.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_DocumentoIdentidad", txtNumeroDocumento.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Direccion", txtDireccion.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Telefono", txtTelefono.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Email", txtEmail.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Facebook", txtFacebook.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Contacto", txtContacto.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@t_Comentario", txtComentario.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@b_Estado", chkEstado.Checked);
                cmd.Parameters.AddWithValue("@d_FechaNacimiento", DateTime.Parse(txtfecump.Text));
                cmd.Parameters.AddWithValue("@c_Genero", rblSexo.SelectedValue);
                cmd.Parameters.AddWithValue("@v_Celular", txtCelular.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Distrito", ddlDistrito.Text.Trim().ToUpper());
                conexion.Open();
                cmd.ExecuteNonQuery();
                conexion.Close();
                cmd.Dispose();
                tblCliente.Visible = true;
                //tblGeneral.Visible = true;
                //toolbar.Visible = true;

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Cliente actualizado satisfactoriamente' });</script>", false);
                           
                                                                
            }
            else
            {
                //Registrar cliente nuevo
                string n_IdCliente = "";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Play_Cliente_Registrar2_Completo";
                cmd.Parameters.AddWithValue("@v_Nombre", txtNombre.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_DocumentoIdentidad", txtNumeroDocumento.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Direccion", txtDireccion.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Telefono", txtTelefono.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Email", txtEmail.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Facebook", txtEmail.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Contacto", txtContacto.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@t_Comentario", txtComentario.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@d_FechaNacimiento", DateTime.Parse(txtfecump.Text));
                cmd.Parameters.AddWithValue("@c_Genero", rblSexo.SelectedValue);
                cmd.Parameters.AddWithValue("@v_Celular", txtCelular.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Distrito", ddlDistrito.Text.Trim().ToUpper());
                conexion.Open();
                n_IdCliente = cmd.ExecuteScalar().ToString();
                conexion.Close();
                cmd.Dispose();
                tblCliente.Visible = true;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Cliente registrado satisfactoriamente' });</script>", false);

               
            }

           
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.error({ message: '" + ex.Message + "' });</script>", false);
        }
    }
}