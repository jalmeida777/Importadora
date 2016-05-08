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


public partial class AsignarPermisos : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            ListarRol();
            ddlRol_SelectedIndexChanged(null, null);
        }
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int i = 0;
            int idRol = int.Parse(ddlRol.SelectedValue);
            int idMenu = 0;
            int idMenu2 = 0;
            int idMenu3 = 0;
            bool Estado = false;
            bool Estado2 = false;
            bool Estado3 = false;

            foreach (TreeNode node in Arbol.Nodes)
            {
                Estado = false;
                if (node.Checked == true) { Estado = true; } else { Estado = false; }
                idMenu = int.Parse(node.Value);
                ActualizarMenu(idRol, idMenu, Estado);

                int x = 0;
                foreach (TreeNode node2 in Arbol.Nodes[i].ChildNodes)
                {
                    if (node2.Checked == true) { Estado2 = true; } else { Estado2 = false; }
                    idMenu2 = int.Parse(node2.Value);
                    ActualizarMenu(idRol, idMenu2, Estado2);

                    foreach (TreeNode node3 in Arbol.Nodes[i].ChildNodes[x].ChildNodes)
                    {
                        if (node3.Checked == true) { Estado3 = true; } else { Estado3 = false; }
                        idMenu3 = int.Parse(node3.Value);
                        ActualizarMenu(idRol, idMenu3, Estado3);
                    }

                    x++;
                }

                i++;
            }

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Producto actualizado.' });</script>", false);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.error({ message: '" + ex.Message + "' });</script>", false);
        }
        

    }

    void ActualizarMenu(int IdRol, int IdMenu, bool Estado) 
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conexion;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "CQ_VentanasRol_Actualizar";
        cmd.Parameters.AddWithValue("@i_IdRol", IdRol);
        cmd.Parameters.AddWithValue("@i_IdMenu", IdMenu);
        cmd.Parameters.AddWithValue("@b_Estado", Estado);
        conexion.Open();
        cmd.ExecuteNonQuery();
        conexion.Close();
    }

    protected void btnSalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Principal.aspx");
    }

    protected void ddlRol_SelectedIndexChanged(object sender, EventArgs e)
    {
        Arbol.Nodes.Clear();
        int i_IdRol;
        i_IdRol = int.Parse(ddlRol.SelectedValue);
        DataTable dtMenus = new DataTable();
        SqlDataAdapter daMenus = new SqlDataAdapter("CIP_VentanasRol_Listar " + i_IdRol,conexion);
        daMenus.Fill(dtMenus);
        for (int i = 0; i < dtMenus.Rows.Count; i++)
        {
            TreeNode tr = new TreeNode();
            tr.Text = dtMenus.Rows[i]["v_Nombre"].ToString();
            tr.Value = dtMenus.Rows[i]["i_IdMenu"].ToString();
            bool chk;
            chk = bool.Parse(dtMenus.Rows[i]["b_Estado"].ToString());
            tr.Checked = chk;
            Arbol.Nodes.Add(tr);
            DataTable dtSubMenu = new DataTable();
            SqlDataAdapter daSubMenu = new SqlDataAdapter("CIP_VentanasRolxPadre_Listar " + tr.Value + "," + i_IdRol, conexion);
            daSubMenu.Fill(dtSubMenu);

            for (int x = 0; x < dtSubMenu.Rows.Count; x++)
			{

			    TreeNode tr2 = new TreeNode();
                tr2.Text = dtSubMenu.Rows[x]["v_Nombre"].ToString();
                tr2.Value = dtSubMenu.Rows[x]["i_IdMenu"].ToString();
                bool chk2;
                chk2 = bool.Parse(dtSubMenu.Rows[x]["b_Estado"].ToString());
                tr2.Checked = chk2;
                tr2.ShowCheckBox = true;
                Arbol.Nodes[i].ChildNodes.Add(tr2);
                DataTable dtItemMenu = new DataTable();
                SqlDataAdapter daItemMenu = new SqlDataAdapter("CIP_VentanasRolxPadre_Listar " + tr2.Value + "," + i_IdRol,conexion);
                daItemMenu.Fill(dtItemMenu);
                for (int w = 0; w < dtItemMenu.Rows.Count; w++)
                {
                    TreeNode tr3 = new TreeNode();
                    tr3.Text = dtItemMenu.Rows[w]["v_Nombre"].ToString();
                    tr3.Value = dtItemMenu.Rows[w]["i_IdMenu"].ToString();
                    bool chk3;
                    chk3 = bool.Parse(dtItemMenu.Rows[w]["b_Estado"].ToString());
                    tr3.Checked = chk3;
                    tr3.ShowCheckBox = true;
                    Arbol.Nodes[i].ChildNodes[x].ChildNodes.Add(tr3);
                }
			}
        }

        Arbol.ExpandDepth = 1;
      
    }

    protected void Arbol_SelectedNodeChanged(object sender, EventArgs e)
    {
        int i_IdRol = int.Parse(ddlRol.SelectedValue);
        int i_IdMenu = int.Parse(Arbol.SelectedNode.Value.ToString());

        panelPermisos.Visible = true;
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter("Play_Permisos_Select " + i_IdRol + "," + i_IdMenu, conexion);
        da.Fill(dt);
        chkPermisos.DataSource = dt;
        chkPermisos.DataTextField = "v_NombreAccion";
        chkPermisos.DataValueField = "i_IdMenuAccionRol";
        chkPermisos.DataBind();

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            chkPermisos.Items[i].Selected = bool.Parse(dt.Rows[i]["b_Estado"].ToString());
        }
    }

    protected void chkPermisos_SelectedIndexChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < chkPermisos.Items.Count; i++)
        {
            int i_IdMenuAccionRol = int.Parse(chkPermisos.Items[i].Value);
            bool b_Estado = chkPermisos.Items[i].Selected;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Play_MenuAccionRol_Actualizar";
            cmd.Parameters.AddWithValue("@i_IdMenuAccionRol", i_IdMenuAccionRol);
            cmd.Parameters.AddWithValue("@b_Estado", b_Estado);
            conexion.Open();
            cmd.ExecuteNonQuery();
            conexion.Close();
        }

    }
}