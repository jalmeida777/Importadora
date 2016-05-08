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
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

public partial class CrearInventarioInicial : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            txtFechaInicial.Text = DateTime.Now.ToShortDateString();
            ListarAlmacen();
            InicializarGrilla();

            if (Request.QueryString["n_IdNotaIngreso"] != null)
            {
                string n_IdNotaIngreso = Request.QueryString["n_IdNotaIngreso"];
                SqlDataAdapter da = new SqlDataAdapter("Play_NotaIngreso_Seleccionar " + n_IdNotaIngreso, conexion);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ddlAlmacen.SelectedValue = dt.Rows[0]["n_IdAlmacen"].ToString();
                txtFechaInicial.Text = DateTime.Parse(dt.Rows[0]["d_FechaEmision"].ToString()).ToShortDateString();
                txtObservacion.Text = dt.Rows[0]["t_Observacion"].ToString();
                lblNumero.Text = dt.Rows[0]["v_NumeroNotaIngreso"].ToString();

                lblUsuarioRegistro.Text = dt.Rows[0]["UsuarioRegistra"].ToString();
                lblFechaRegistro.Text = dt.Rows[0]["d_FechaCreacion"].ToString();
                if (dt.Rows[0]["UsuarioFotoRegistra"].ToString().Trim() != "")
                {
                    ibUsuarioRegistro.ImageUrl = dt.Rows[0]["UsuarioFotoRegistra"].ToString();
                }
                else
                {
                    ibUsuarioRegistro.ImageUrl = "~/images/face.jpg";
                }

                SqlDataAdapter daDetalle = new SqlDataAdapter("Play_NotaIngresoDetalle_Seleccionar " + n_IdNotaIngreso, conexion);
                DataTable dtDetalle = new DataTable();
                daDetalle.Fill(dtDetalle);
                gv.DataSource = dtDetalle;
                gv.DataBind();
                BloquearNotaIngreso();
            }
        }
    }

    public void ListarAlmacen()
    {
        if (Session["dtAlmacenes"] != null)
        {
            DataTable dtAlmacen = new DataTable();
            dtAlmacen = (DataTable)Session["dtAlmacenes"];
            ddlAlmacen.DataSource = dtAlmacen;
            ddlAlmacen.DataTextField = "v_Descripcion";
            ddlAlmacen.DataValueField = "n_IdAlmacen";
            ddlAlmacen.DataBind();
            ddlAlmacen.SelectedIndex = 0;
            if (dtAlmacen.Rows.Count > 1)
            {
                ddlAlmacen.Enabled = true;
            }
            else
            {
                ddlAlmacen.Enabled = false;
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Su sesión ha caducado. Vuelva a ingresar al sistema.' });</script>", false);
            BloquearNotaIngreso();
        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> BuscarProductos(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager
                .ConnectionStrings["conexion"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select v_Descripcion,n_IdProducto from Producto where v_CodigoInterno+v_Descripcion like '%' + @SearchText + '%' and b_Estado = 1 order by v_Descripcion";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Connection = conn;
                conn.Open();
                List<string> productos = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        productos.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(sdr["v_Descripcion"].ToString(), Convert.ToString(sdr["n_IdProducto"].ToString())));
                    }
                }
                conn.Close();
                return productos;
            }
        }
    }

    void InicializarGrilla()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("n_IdProducto");
        dt.Columns.Add("Producto");
        dt.Columns.Add("Cantidad");

        DataRow dr;
        dr = dt.NewRow();
        dr["n_IdProducto"] = 0;
        dr["Producto"] = "";
        dr["Cantidad"] = "0";
        dt.Rows.Add(dr);

        Session["Detalle"] = dt;
        gv.DataSource = dt;
        gv.DataBind();
    }

    protected void ibEstablecerSucursal_Click(object sender, ImageClickEventArgs e)
    {
        ddlAlmacen.Enabled = false;
        gv.Enabled = true;
        ibEstablecerSucursal.Visible = false;
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("CrearInventarioInicial.aspx");
    }

    protected void btnSalir_Click(object sender, ImageClickEventArgs e)
    {
        Session.Remove("Detalle");
        Response.Redirect("ListarNotaIngreso.aspx");
    }

    protected void hfIdProducto_ValueChanged(object sender, EventArgs e)
    {
        string selectedWidgetID = ((HiddenField)sender).Value;
        TextBox txtProducto = (TextBox)((HiddenField)sender).Parent.FindControl("txtProducto");

        ((HiddenField)sender).Value = selectedWidgetID;
    }

    void BloquearNotaIngreso()
    {
        btnGuardar.Enabled = false;
        ddlAlmacen.Enabled = false;
        txtFechaInicial.Enabled = false;
        gv.Enabled = false;
        txtObservacion.Enabled = false;
        ibEstablecerSucursal.Visible = false;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        if (txtFechaInicial.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar la Fecha' });</script>", false);
            txtFechaInicial.Focus();
            return;
        }

        TextBox tPro = new TextBox();
        TextBox tCan = new TextBox();
        HiddenField hf = new HiddenField();

        DataTable dt = new DataTable();
        dt = (DataTable)Session["Detalle"];

        //Pasar de la grilla a la tabla
        for (int i = 0; i < gv.Rows.Count; i++)
        {
            hf = (HiddenField)gv.Rows[i].FindControl("hfIdProducto");
            tPro = (TextBox)gv.Rows[i].Cells[0].FindControl("txtProducto");
            tCan = (TextBox)gv.Rows[i].Cells[1].FindControl("txtCantidad");
            dt.Rows[i]["n_IdProducto"] = hf.Value;
            dt.Rows[i]["Producto"] = tPro.Text.Trim();
            dt.Rows[i]["Cantidad"] = tCan.Text.Trim();
        }

        if (Session["dtUsuario"] != null)
        {
            DataTable dtUsuario = new DataTable();
            dtUsuario = (DataTable)Session["dtUsuario"];
            string n_IdUsuario = dtUsuario.Rows[0]["n_IdUsuario"].ToString();

            SqlTransaction tran;
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);
            cn.Open();
            tran = cn.BeginTransaction();

            try
            {
                //Registrar Cabecera
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = tran;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Play_NotaIngreso_Insertar";
                cmd.Parameters.AddWithValue("@n_IdAlmacen", ddlAlmacen.SelectedValue);
                cmd.Parameters.AddWithValue("@n_IdMotivoTraslado", 15);
                cmd.Parameters.AddWithValue("@d_FechaEmision", DateTime.Parse(txtFechaInicial.Text + " " + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00")));
                cmd.Parameters.AddWithValue("@v_Referencia", "");
                cmd.Parameters.AddWithValue("@t_Observacion", txtObservacion.Text);
                cmd.Parameters.AddWithValue("@n_IdUsuarioCreacion", n_IdUsuario);

                string n_IdNotaIngreso = cmd.ExecuteScalar().ToString();

                cmd.Dispose();

                if (n_IdNotaIngreso.Trim() == "0")
                {
                    tran.Rollback();
                    cn.Close();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'El correlativo de la Nota de Ingreso ha terminado' });</script>", false);
                    return;
                }

                SqlCommand cmd0 = new SqlCommand();
                cmd0.Connection = cn;
                cmd0.Transaction = tran;
                cmd0.CommandType = CommandType.Text;
                cmd0.CommandText = "select v_NumeroNotaIngreso from NotaIngreso where n_IdNotaIngreso = " + n_IdNotaIngreso;
                lblNumero.Text = cmd0.ExecuteScalar().ToString();


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //Obtener el id del producto por su nombre
                    string n_IdProducto = "";

                    n_IdProducto = dt.Rows[i]["n_IdProducto"].ToString();

                    //Registrar Detalle
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.Connection = cn;
                    cmd2.Transaction = tran;
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.CommandText = "Play_NotaIngresoDetalle_Insertar";
                    cmd2.Parameters.AddWithValue("@n_IdNotaIngreso", n_IdNotaIngreso);
                    cmd2.Parameters.AddWithValue("@n_IdProducto", n_IdProducto);
                    cmd2.Parameters.AddWithValue("@i_Cantidad", dt.Rows[i]["Cantidad"].ToString());
                    cmd2.ExecuteNonQuery();
                    cmd2.Dispose();

                    //Aumentar Stock
                    SqlCommand cmd3 = new SqlCommand();
                    cmd3.Connection = cn;
                    cmd3.Transaction = tran;
                    cmd3.CommandType = CommandType.StoredProcedure;
                    cmd3.CommandText = "Play_Stock_Inicial";
                    cmd3.Parameters.AddWithValue("@n_IdAlmacen", ddlAlmacen.SelectedValue);
                    cmd3.Parameters.AddWithValue("@n_IdProducto", n_IdProducto);
                    cmd3.Parameters.AddWithValue("@f_Cantidad", dt.Rows[i]["Cantidad"].ToString());
                    cmd3.ExecuteNonQuery();
                    cmd3.Dispose();

                    //Registrar Kardex
                    SqlCommand cmd4 = new SqlCommand();
                    cmd4.Connection = cn;
                    cmd4.Transaction = tran;
                    cmd4.CommandType = CommandType.StoredProcedure;
                    cmd4.CommandText = "Play_KardexInicial_Insertar";
                    cmd4.Parameters.AddWithValue("@c_TipoMovimiento", "I");
                    cmd4.Parameters.AddWithValue("@i_IdMotivoTraslado",15);
                    cmd4.Parameters.AddWithValue("@n_IdProducto", n_IdProducto);
                    cmd4.Parameters.AddWithValue("@n_IdAlmacen", ddlAlmacen.SelectedValue);
                    cmd4.Parameters.AddWithValue("@f_Cantidad", dt.Rows[i]["Cantidad"].ToString());
                    cmd4.Parameters.AddWithValue("@n_IdTipoDocumento", 8);
                    cmd4.Parameters.AddWithValue("@v_NumeroDocumento", lblNumero.Text.Trim());
                    cmd4.Parameters.AddWithValue("@n_IdCliente", DBNull.Value);
                    cmd4.Parameters.AddWithValue("@n_IdProveedor", DBNull.Value);
                    cmd4.ExecuteNonQuery();
                    cmd4.Dispose();
                }

                //Actualizar Correlativos
                SqlCommand cmd5 = new SqlCommand();
                cmd5.Connection = cn;
                cmd5.Transaction = tran;
                cmd5.CommandType = CommandType.StoredProcedure;
                cmd5.CommandText = "Play_Correlativo_Aumentar";
                cmd5.Parameters.AddWithValue("@n_IdTipoDocumento", 8);
                cmd5.Parameters.AddWithValue("@n_IdAlmacen", ddlAlmacen.SelectedValue);
                cmd5.ExecuteNonQuery();

                tran.Commit();


                BloquearNotaIngreso();

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Nota de Ingreso registrada Satisfactoriamente' });</script>", false);
            }
            catch (Exception ex)
            {
                tran.Rollback();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.error({ message: '" + ex.Message + "' });</script>", false);
            }
            finally
            {
                cn.Close();
            }
        }
    }


}