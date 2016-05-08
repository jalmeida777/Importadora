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

public partial class Login : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        txtUsuario.Focus();
    }

    protected void btnEntrar_Click(object sender, ImageClickEventArgs e)
    {
        if (txtUsuario.Text == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar el usuario' });</script>", false);
            txtUsuario.Focus();
            return;
        }
        if (txtContraseña.Text == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar su contraseña' });</script>", false);
            txtContraseña.Focus();
            return;
        }

        DataTable dtUsuario = new DataTable();
        SqlDataAdapter daUsuario = new SqlDataAdapter("Play_Usuario_Select '" + txtUsuario.Text.Trim().ToUpper() + "','" + txtContraseña.Text.Trim() + "'", conexion);
        daUsuario.Fill(dtUsuario);
        if (dtUsuario != null) 
        {
            if (dtUsuario.Rows.Count == 1) 
            {
                //Guardar datos del Usuario en la sesión
                string Usuario = dtUsuario.Rows[0]["v_Usuario"].ToString();
                string n_IdUsuario = dtUsuario.Rows[0]["n_IdUsuario"].ToString();
                Session["dtUsuario"] = dtUsuario;

                //Guardar almacenes permitidos el usuario en la sesión
                DataTable dtAlmacenes = new DataTable();
                SqlDataAdapter daAlmacenes = new SqlDataAdapter("Play_UsuarioAlmacen_Listar " + n_IdUsuario, conexion);
                daAlmacenes.Fill(dtAlmacenes);
                if (dtAlmacenes.Rows.Count > 0)
                {
                    Session["dtAlmacenes"] = dtAlmacenes;
                }
                else 
                { 
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'No hay sucursales vinculadas con su usuario' });</script>", false);
                    return;
                }
                //Obtener Parámetros del sistema
                DataTable dtParametro = new DataTable();
                SqlDataAdapter daParametro = new SqlDataAdapter("select f_Igv, b_PedidoMueveStock from parametros", conexion);
                daParametro.Fill(dtParametro);
                Session["dtParametro"] = dtParametro;


                Session["dtUsuario"] = dtUsuario;
                FormsAuthentication.RedirectFromLoginPage(Usuario, false);

                
            }
        }

    }
}