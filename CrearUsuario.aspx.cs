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

public partial class CrearUsuario : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            ListarRol();
            ListarSucursal();

            if (Request.QueryString["n_IdUsuario"] != null)
            {
                string n_IdUsuario = Request.QueryString["n_IdUsuario"];
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("Play_Usuario_Seleccionar " + n_IdUsuario, conexion);
                da.Fill(dt);
                lblCodigo.Text = n_IdUsuario;
                txtNombre.Text = dt.Rows[0]["v_Nombre"].ToString();
                txtUsuario.Text = dt.Rows[0]["v_Usuario"].ToString();
                txtPwd.Text = dt.Rows[0]["v_Pwd"].ToString();

                if (dt.Rows[0]["v_RutaFoto"].ToString().Trim() == "")
                {
                    lblRuta.Text = "~/images/Prev.jpg";
                }
                else
                {
                    lblRuta.Text = dt.Rows[0]["v_RutaFoto"].ToString();
                }
                ibImagen.ImageUrl = lblRuta.Text;       

                if (dt.Rows[0]["i_IdRol"].ToString() != "") { ddlRol.SelectedValue = dt.Rows[0]["i_IdRol"].ToString(); }
                
                chkEstado.Checked = bool.Parse(dt.Rows[0]["b_Estado"].ToString());
                filaContraseña.Visible = false;
                lbContraseña.Visible = true;

                SqlDataAdapter daAlmacen = new SqlDataAdapter("Play_UsuarioAlmacen_Seleccionar " + n_IdUsuario, conexion);
                DataTable dtAlmacen = new DataTable();
                daAlmacen.Fill(dtAlmacen);

                for (int i = 0; i < chkSucursal.Items.Count; i++)
                {
                    for (int x = 0; x < dtAlmacen.Rows.Count; x++)
                    {
                        if (dtAlmacen.Rows[x]["n_IdAlmacen"].ToString().Trim() == chkSucursal.Items[i].Value) 
                        {
                            chkSucursal.Items[i].Selected = true;
                        }       
                    }
                }

            }
            else
            {
                filaContraseña.Visible = true;
                lbContraseña.Visible = false;
            }
            
            txtNombre.Focus();
        }
    }

    void ListarRol()
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter("Play_Rol_Combo", conexion);
        da.Fill(dt);
        ddlRol.DataSource = dt;
        ddlRol.DataTextField = "v_Nombrerol";
        ddlRol.DataValueField = "i_IdRol";
        ddlRol.DataBind();
        ddlRol.SelectedIndex = 0;
    }

    void ListarSucursal() 
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter("Play_Almacen_combo 1", conexion);
        da.Fill(dt);
        chkSucursal.DataSource = dt;
        chkSucursal.DataTextField = "v_Descripcion";
        chkSucursal.DataValueField = "n_IdAlmacen";
        chkSucursal.DataBind();
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        if (txtNombre.Text == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar el nombre' });</script>", false);
            txtNombre.Focus();
            return;
        }
        if ( txtUsuario.Text == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar el Usuario' });</script>", false);
            txtUsuario.Focus();
            return;
        }


        try
        {
            if (lblCodigo.Text.Trim() != "")
            {
                if (lbContraseña.Text.Trim() == "Cambiar Contraseña")
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conexion;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Play_Usuario_Actualizar_Parcial";
                    cmd.Parameters.AddWithValue("@n_IdUsuario", lblCodigo.Text);
                    cmd.Parameters.AddWithValue("@i_IdRol", ddlRol.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@v_Usuario", txtUsuario.Text.Trim().ToUpper());
                    cmd.Parameters.AddWithValue("@v_RutaFoto", lblRuta.Text.Trim().ToUpper());
                    cmd.Parameters.AddWithValue("@b_Estado", chkEstado.Checked);
                    cmd.Parameters.AddWithValue("@v_Nombre", txtNombre.Text.Trim().ToUpper());
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                    conexion.Close();
                    cmd.Dispose();

                    SqlCommand cmdEliminar = new SqlCommand();
                    cmdEliminar.Connection = conexion;
                    cmdEliminar.CommandType = CommandType.StoredProcedure;
                    cmdEliminar.CommandText = "Play_UsuarioAlmacen_Eliminar";
                    cmdEliminar.Parameters.AddWithValue("@n_IdUsuario", lblCodigo.Text);
                    conexion.Open();
                    cmdEliminar.ExecuteNonQuery();
                    conexion.Close();
                    cmdEliminar.Dispose();

                    for (int i = 0; i < chkSucursal.Items.Count; i++)
                    {
                        if (chkSucursal.Items[i].Selected == true)
                        {
                            SqlCommand cmdAlmacen = new SqlCommand();
                            cmdAlmacen.Connection = conexion;
                            cmdAlmacen.CommandType = CommandType.StoredProcedure;
                            cmdAlmacen.CommandText = "Play_UsuarioAlmacen_Registrar";
                            cmdAlmacen.Parameters.AddWithValue("@n_IdUsuario", lblCodigo.Text);
                            cmdAlmacen.Parameters.AddWithValue("@n_IdAlmacen", chkSucursal.Items[i].Value);
                            conexion.Open();
                            cmdAlmacen.ExecuteNonQuery();
                            conexion.Close();
                            cmdAlmacen.Dispose();
                        }
                    }
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Usuario actualizado.' });</script>", false);
                }
                else if (lbContraseña.Text.Trim() == "Ocultar Contraseña") 
                {
                    if (txtPwd.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar el Contraseña' });</script>", false);
                        txtPwd.Focus();
                        return;
                    }
                    if (txtPwd.Text.Trim() == txtPwd2.Text.Trim())
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conexion;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Play_Usuario_Actualizar_Completo";
                        cmd.Parameters.AddWithValue("@n_IdUsuario", lblCodigo.Text);
                        cmd.Parameters.AddWithValue("@i_IdRol", ddlRol.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@v_Usuario", txtUsuario.Text.Trim().ToUpper());
                        cmd.Parameters.AddWithValue("@v_Pwd", txtPwd.Text.Trim().ToUpper());
                        cmd.Parameters.AddWithValue("@v_RutaFoto", lblRuta.Text.Trim().ToUpper());
                        cmd.Parameters.AddWithValue("@b_Estado", chkEstado.Checked);
                        cmd.Parameters.AddWithValue("@v_Nombre", txtNombre.Text.Trim().ToUpper());
                        conexion.Open();
                        cmd.ExecuteNonQuery();
                        conexion.Close();
                        cmd.Dispose();

                        SqlCommand cmdEliminar = new SqlCommand();
                        cmdEliminar.Connection = conexion;
                        cmdEliminar.CommandType = CommandType.StoredProcedure;
                        cmdEliminar.CommandText = "Play_UsuarioAlmacen_Eliminar";
                        cmdEliminar.Parameters.AddWithValue("@n_IdUsuario", lblCodigo.Text);
                        conexion.Open();
                        cmdEliminar.ExecuteNonQuery();
                        conexion.Close();
                        cmdEliminar.Dispose();

                        for (int i = 0; i < chkSucursal.Items.Count; i++)
                        {
                            if (chkSucursal.Items[i].Selected == true)
                            {
                                SqlCommand cmdAlmacen = new SqlCommand();
                                cmdAlmacen.Connection = conexion;
                                cmdAlmacen.CommandType = CommandType.StoredProcedure;
                                cmdAlmacen.CommandText = "Play_UsuarioAlmacen_Registrar";
                                cmdAlmacen.Parameters.AddWithValue("@n_IdUsuario", lblCodigo.Text);
                                cmdAlmacen.Parameters.AddWithValue("@n_IdAlmacen", chkSucursal.Items[i].Value);
                                conexion.Open();
                                cmdAlmacen.ExecuteNonQuery();
                                conexion.Close();
                                cmdAlmacen.Dispose();
                            }
                        }
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Usuario actualizado.' });</script>", false);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Las contraseñas no son iguales.' });</script>", false);
                    }

                }
            }
            else
            {
                if (txtPwd.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar el Contraseña' });</script>", false);
                    txtPwd.Focus();
                    return;
                }
                if (txtPwd.Text.Trim() == txtPwd2.Text.Trim())
                {

                    string n_IdUsuario = "";
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conexion;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Play_Usuario_Registrar";
                    cmd.Parameters.AddWithValue("@i_IdRol", ddlRol.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@v_Usuario", txtUsuario.Text.Trim().ToUpper());
                    cmd.Parameters.AddWithValue("@v_Pwd", txtPwd.Text.Trim().ToUpper());
                    cmd.Parameters.AddWithValue("@v_RutaFoto", lblRuta.Text.Trim().ToUpper());
                    cmd.Parameters.AddWithValue("@b_Estado", chkEstado.Checked);
                    cmd.Parameters.AddWithValue("@v_Nombre", txtNombre.Text.Trim().ToUpper());
                    conexion.Open();
                    n_IdUsuario = cmd.ExecuteScalar().ToString();
                    conexion.Close();
                    lblCodigo.Text = n_IdUsuario;

                    SqlCommand cmdEliminar = new SqlCommand();
                    cmdEliminar.Connection = conexion;
                    cmdEliminar.CommandType = CommandType.StoredProcedure;
                    cmdEliminar.CommandText = "Play_UsuarioAlmacen_Eliminar";
                    cmdEliminar.Parameters.AddWithValue("@n_IdUsuario", n_IdUsuario);
                    conexion.Open();
                    cmdEliminar.ExecuteNonQuery();
                    conexion.Close();
                    cmdEliminar.Dispose();

                    for (int i = 0; i < chkSucursal.Items.Count; i++)
                    {
                        if (chkSucursal.Items[i].Selected == true)
                        {
                            SqlCommand cmdAlmacen = new SqlCommand();
                            cmdAlmacen.Connection = conexion;
                            cmdAlmacen.CommandType = CommandType.StoredProcedure;
                            cmdAlmacen.CommandText = "Play_UsuarioAlmacen_Registrar";
                            cmdAlmacen.Parameters.AddWithValue("@n_IdUsuario", n_IdUsuario);
                            cmdAlmacen.Parameters.AddWithValue("@n_IdAlmacen", chkSucursal.Items[i].Value);
                            conexion.Open();
                            cmdAlmacen.ExecuteNonQuery();
                            conexion.Close();
                            cmdAlmacen.Dispose();
                        }
                    }
                    BloquearUsuario();

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Usuario registrado.' });</script>", false);
                }
                else 
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Las contraseñas no son iguales.' });</script>", false);
                }
                
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.error({ message: '" + ex.Message + "' });</script>", false);
        }
    }

    void BloquearUsuario() 
    {
        txtNombre.Enabled = false;
        txtUsuario.Enabled = false;
        lbContraseña.Enabled = false;
        txtPwd.Enabled = false;
        txtPwd2.Enabled = false;
        ddlRol.Enabled = false;
        chkSucursal.Enabled = false;
        chkEstado.Enabled = false;
        ibImagen.Enabled = false;
        fu1.Enabled = false;
        ibUpload.Enabled = false;
    }

    protected void btnSalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("ListarUsuario.aspx");
    }

    protected void ibUpload_Click(object sender, ImageClickEventArgs e)
    {
        string filename = Path.GetFileName(fu1.FileName);
        fu1.SaveAs(Server.MapPath("~/Usuarios/") + filename);
        ibImagen.ImageUrl = "~/Usuarios/" + filename;
        lblRuta.Text = "~/Usuarios/" + filename;
    }

    protected void lbContraseña_Click(object sender, EventArgs e)
    {
        if (lbContraseña.Text.Trim() == "Cambiar Contraseña") 
        {
            lbContraseña.Text = "Ocultar Contraseña";
            filaContraseña.Visible = true;
        }
        else if (lbContraseña.Text.Trim() == "Ocultar Contraseña") 
        {
            lbContraseña.Text = "Cambiar Contraseña";
            filaContraseña.Visible = false;
        }
        
    }
}