using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DevExpress.Web.ASPxNavBar;

public partial class Plantilla : System.Web.UI.MasterPage
{
    SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["MenuHTML"] != null)
        {
            litMenu.Text = Session["MenuHTML"].ToString();
        }
        else { ListarMenu(); }
       
    }



    protected void ListarMenu()
    {
        if (Session["dtUsuario"] != null)
        {
            DataTable dtUsuario = new DataTable();
            dtUsuario = (DataTable)Session["dtUsuario"];
            string i_IdRol = dtUsuario.Rows[0]["i_IdRol"].ToString();

            if (Session["MenuHTML"] == null)
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("Play_Menus_Menu " + i_IdRol, conexion);
                da.Fill(dt);
                litMenu.Text = "";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string i_IdMenu = dt.Rows[i]["i_IdMenu"].ToString();
                    string v_DescripcionMenu = dt.Rows[i]["v_Nombre"].ToString();
                    string v_RutaMenu = dt.Rows[i]["v_Url"].ToString();
                    string imagen = dt.Rows[i]["v_RutaImagen"].ToString();

                    litMenu.Text = litMenu.Text + "<li>";
                    

                    DataTable dtSubMenu = new DataTable();
                    SqlDataAdapter daSubMenu = new SqlDataAdapter("Play_Menus_Menu_Hijos " + i_IdRol + "," + i_IdMenu, conexion);
                    daSubMenu.Fill(dtSubMenu);
                    if (dtSubMenu.Rows.Count > 0)
                    {
                        litMenu.Text = litMenu.Text + "<span><i class='" + imagen + "'></i>&nbsp;&nbsp;&nbsp;" + v_DescripcionMenu + "</span>";
                        litMenu.Text = litMenu.Text + "<ul>";
                    }
                    else
                    {
                        litMenu.Text = litMenu.Text + "<a href='" + v_RutaMenu + "'><i class='" + imagen + "'></i>&nbsp;&nbsp;&nbsp;" + v_DescripcionMenu + "</a>";
                    }

                    for (int x = 0; x < dtSubMenu.Rows.Count; x++)
                    {
                        string i_IdSubMenu = dtSubMenu.Rows[x]["i_IdMenu"].ToString();
                        string v_DescripcionSubMenu = dtSubMenu.Rows[x]["v_Nombre"].ToString();
                        string v_Ruta = dtSubMenu.Rows[x]["v_Url"].ToString();
                        litMenu.Text = litMenu.Text + "<li>";

                        DataTable dtItemMenu = new DataTable();
                        SqlDataAdapter daItemMenu = new SqlDataAdapter("Play_Menus_Menu_Hijos " + i_IdRol + "," + i_IdSubMenu, conexion);
                        daItemMenu.Fill(dtItemMenu);
                        if (dtItemMenu.Rows.Count > 0)
                        {
                            litMenu.Text = litMenu.Text + "<span>" + v_DescripcionSubMenu + "</span>";
                            litMenu.Text = litMenu.Text + "<ul>";
                            for (int y = 0; y < dtItemMenu.Rows.Count; y++)
                            {
                                string v_DescripcionItem = dtItemMenu.Rows[y]["v_Nombre"].ToString();
                                string v_RutaItem = dtItemMenu.Rows[y]["v_Url"].ToString();
                                litMenu.Text = litMenu.Text + "<li>";
                                litMenu.Text = litMenu.Text + "<a href='" + v_RutaItem + "'>" + v_DescripcionItem + "</a>";
                                litMenu.Text = litMenu.Text + "</li>";
                            }
                            if (dtItemMenu.Rows.Count > 0)
                            {
                                litMenu.Text = litMenu.Text + "</ul>";
                            }
                        }
                        else
                        {
                            litMenu.Text = litMenu.Text + "<a href='" + v_Ruta + "'>" + v_DescripcionSubMenu + "</a>";
                        }
                        litMenu.Text = litMenu.Text + "</li>";
                    }
                    if (dtSubMenu.Rows.Count > 0)
                    {
                        litMenu.Text = litMenu.Text + "</ul>";
                    }
                    litMenu.Text = litMenu.Text + "</li>";
                }
                Session["MenuHTML"] = litMenu.Text;
            }
        }
    }

    protected void LoginStatus1_LoggingOut(object sender, LoginCancelEventArgs e)
    {
        try
        {
            if (Session["MenuHTML"] != null) { Session["MenuHTML"] = null; }
            if (Session["Detalle"] != null) { Session["Detalle"] = null; }
            if (Session["dtAlmacenes"] != null) { Session["dtAlmacenes"] = null; }
            if (Session["dtUsuario"] != null) { Session["dtUsuario"] = null; }
            if (Session["dtParametro"] != null) { Session["dtParametro"] = null; }
            Session.Abandon();
        }
        catch (Exception)
        {
            
        }

    }
}
