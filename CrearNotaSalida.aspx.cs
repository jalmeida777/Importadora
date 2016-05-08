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

public partial class CrearNotaSalida : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false) 
        {
            ListarAlmacen();
            ListarMotivo();
            InicializarGrilla();
            lblEstado.Text = "Activo";
            lblEstado.ForeColor = System.Drawing.Color.Green;
            txtFechaInicial.Text = DateTime.Now.ToShortDateString();

            if (Request.QueryString["n_IdNotaSalida"] != null)
            {
                string n_IdNotaSalida = Request.QueryString["n_IdNotaSalida"];
                SqlDataAdapter da = new SqlDataAdapter("Play_NotaSalida_Seleccionar " + n_IdNotaSalida, conexion);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ddlAlmacen.SelectedValue = dt.Rows[0]["n_IdAlmacen"].ToString();
                ddlTipoMovimiento.SelectedValue = dt.Rows[0]["n_IdMotivoTraslado"].ToString();
                txtFechaInicial.Text = DateTime.Parse(dt.Rows[0]["d_FechaEmision"].ToString()).ToShortDateString();
                txtReferencia.Text = dt.Rows[0]["v_Referencia"].ToString();
                txtObservacion.Text = dt.Rows[0]["t_Observacion"].ToString();
                lblNumero.Text = dt.Rows[0]["v_NumeroNotaSalida"].ToString();
                bool Estado = bool.Parse(dt.Rows[0]["b_Estado"].ToString());
                if (Estado == true)
                {
                    lblEstado.Text = "Activo";
                    lblEstado.ForeColor = System.Drawing.Color.Green;
                    ibAnular.Visible = true;
                }
                else
                {
                    lblEstado.Text = "Anulado";
                    lblEstado.ForeColor = System.Drawing.Color.Red;
                    Label1.ForeColor = System.Drawing.Color.Red;
                    lblNumero.ForeColor = System.Drawing.Color.Red;
                    ibAnular.Visible = false;
                }

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

                SqlDataAdapter daDetalle = new SqlDataAdapter("Play_NotaSalidaDetalle_Seleccionar " + n_IdNotaSalida, conexion);
                DataTable dtDetalle = new DataTable();
                daDetalle.Fill(dtDetalle);
                gv.DataSource = dtDetalle;
                gv.DataBind();
                BloquearNotaSalida();
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
            BloquearNotaSalida();
        }
    }

    public void ListarMotivo()
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter("Play_MotivoTraslado_Combo 'V'", conexion);
        da.Fill(dt);
        ddlTipoMovimiento.DataSource = dt;
        ddlTipoMovimiento.DataTextField = "v_Descripcion";
        ddlTipoMovimiento.DataValueField = "n_IdMotivoTraslado";
        ddlTipoMovimiento.DataBind();
        ddlTipoMovimiento.SelectedIndex = 0;
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

    void BloquearNotaSalida() 
    {
        btnGuardar.Enabled = false;
        ddlAlmacen.Enabled = false;
        txtFechaInicial.Enabled = false;
        ddlTipoMovimiento.Enabled = false;
        txtReferencia.Enabled = false;
        gv.Enabled = false;
        txtObservacion.Enabled = false;
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

            string numero;

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
                cmd.CommandText = "Play_NotaSalida_Registrar";
                cmd.Parameters.AddWithValue("@n_IdAlmacen", ddlAlmacen.SelectedValue);
                cmd.Parameters.AddWithValue("@n_IdMotivoTraslado", ddlTipoMovimiento.SelectedValue);
                cmd.Parameters.AddWithValue("@d_FechaEmision", DateTime.Parse(txtFechaInicial.Text + " " + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00")));
                cmd.Parameters.AddWithValue("@v_Referencia", txtReferencia.Text.Trim());
                cmd.Parameters.AddWithValue("@t_Observacion", txtObservacion.Text);
                cmd.Parameters.AddWithValue("@n_IdUsuarioCreacion", n_IdUsuario);

                string n_IdNotaSalida = cmd.ExecuteScalar().ToString();

                cmd.Dispose();

                if (n_IdNotaSalida.Trim() == "0")
                {
                    tran.Rollback();
                    cn.Close();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'El correlativo de la Nota de Salida ha terminado' });</script>", false);
                    return;
                }

                SqlCommand cmd0 = new SqlCommand();
                cmd0.Connection = cn;
                cmd0.Transaction = tran;
                cmd0.CommandType = CommandType.Text;
                cmd0.CommandText = "select v_NumeroNotaSalida from NotaSalida where n_IdNotaSalida = " + n_IdNotaSalida;
                numero = cmd0.ExecuteScalar().ToString();


                int stockContable = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //Obtener el id del producto por su nombre
                    string n_IdProducto = "";

                    n_IdProducto = dt.Rows[i]["n_IdProducto"].ToString();

                    //validar el stock antes de guardar 
                    SqlCommand cmdStockContable = new SqlCommand();
                    cmdStockContable.Connection = cn;
                    cmdStockContable.Transaction = tran;
                    cmdStockContable.CommandType = CommandType.Text;
                    cmdStockContable.CommandText = "select f_StockContable from stock where n_IdProducto = " + dt.Rows[i]["n_IdProducto"].ToString() + " and n_IdAlmacen = " + ddlAlmacen.SelectedValue;
                    stockContable = int.Parse(cmdStockContable.ExecuteScalar().ToString());

                    if (stockContable < int.Parse(dt.Rows[i]["Cantidad"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'No hay stock suficiente' });</script>", false);
                        tran.Rollback();
                        cn.Close();
                        return;
                    }


                    //Registrar Detalle
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.Connection = cn;
                    cmd2.Transaction = tran;
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.CommandText = "Play_NotaSalidaDetalle_Insert";
                    cmd2.Parameters.AddWithValue("@n_IdNotaSalida", n_IdNotaSalida);
                    cmd2.Parameters.AddWithValue("@n_IdProducto", n_IdProducto);
                    cmd2.Parameters.AddWithValue("@i_Cantidad", dt.Rows[i]["Cantidad"].ToString());
                    cmd2.ExecuteNonQuery();
                    cmd2.Dispose();

                    //Restar Stock
                    SqlCommand cmd3 = new SqlCommand();
                    cmd3.Connection = cn;
                    cmd3.Transaction = tran;
                    cmd3.CommandType = CommandType.StoredProcedure;
                    cmd3.CommandText = "Play_Stock_Restar_Actualizar";
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
                    cmd4.CommandText = "Play_Kardex_Insertar";
                    cmd4.Parameters.AddWithValue("@d_FechaMovimiento", DateTime.Parse(txtFechaInicial.Text + " " + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00")));
                    cmd4.Parameters.AddWithValue("@c_TipoMovimiento", "S");
                    cmd4.Parameters.AddWithValue("@i_IdMotivoTraslado", ddlTipoMovimiento.SelectedValue);
                    cmd4.Parameters.AddWithValue("@n_IdProducto", n_IdProducto);
                    cmd4.Parameters.AddWithValue("@n_IdAlmacen", ddlAlmacen.SelectedValue);
                    cmd4.Parameters.AddWithValue("@f_Cantidad", dt.Rows[i]["Cantidad"].ToString());
                    cmd4.Parameters.AddWithValue("@n_IdTipoDocumento", 9);
                    cmd4.Parameters.AddWithValue("@v_NumeroDocumento", numero);
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
                cmd5.Parameters.AddWithValue("@n_IdTipoDocumento", 9);
                cmd5.Parameters.AddWithValue("@n_IdAlmacen", ddlAlmacen.SelectedValue);
                cmd5.ExecuteNonQuery();

                tran.Commit();
                lblNumero.Text = numero;

                BloquearNotaSalida();

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Nota de Salida registrada Satisfactoriamente' });</script>", false);
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

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("CrearNotaSalida.aspx");
    }

    protected void btnSalir_Click(object sender, ImageClickEventArgs e)
    {
        Session.Remove("Detalle");
        Response.Redirect("ListarNotaSalida.aspx");
    }

    protected void lnkAgregarProducto_Click(object sender, EventArgs e)
    {

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
        
        //Mostrar los datos y fila nueva
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
    
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
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

            dt.Rows.RemoveAt(e.RowIndex);

            Session["Detalle"] = dt;

            gv.DataSource = dt;
            gv.DataBind();

            if (dt.Rows.Count == 0) { InicializarGrilla(); }

        }
        catch (Exception)
        {

        }
    }

    protected void ibAnular_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Request.QueryString["n_IdNotaSalida"] != null)
            {
                string n_IdNotaSalida = Request.QueryString["n_IdNotaSalida"];

                if (Session["dtUsuario"] != null)
                {
                    DataTable dtUsuario = new DataTable();
                    dtUsuario = (DataTable)Session["dtUsuario"];
                    string n_IdUsuario = dtUsuario.Rows[0]["n_IdUsuario"].ToString();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conexion;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Play_NotaSalida_Anular";
                    cmd.Parameters.AddWithValue("@n_IdNotaSalida", n_IdNotaSalida);
                    cmd.Parameters.AddWithValue("@n_IdUsuarioAnulacion", n_IdUsuario);
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                    conexion.Close();

                    lblEstado.Text = "Anulado";
                    lblEstado.ForeColor = System.Drawing.Color.Red;
                    Label1.ForeColor = System.Drawing.Color.Red;
                    lblNumero.ForeColor = System.Drawing.Color.Red;
                    ibAnular.Visible = false;

                    BloquearNotaSalida();

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Nota de Salida Anulada Satisfactoriamente' });</script>", false);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.error({ message: '" + ex.Message + "' });</script>", false);
        }
    }

    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField IdProducto = (HiddenField)e.Row.FindControl("hfIdProducto");
            TextBox txtProducto = (TextBox)e.Row.FindControl("txtProducto");

            if (int.Parse(IdProducto.Value) > 0)
            {
                txtProducto.BackColor = System.Drawing.Color.FromName("#DBB7FF");
                txtProducto.Enabled = false;
            }
        }
    }

    protected void hfIdProducto_ValueChanged(object sender, EventArgs e)
    {

        string selectedWidgetID = ((HiddenField)sender).Value;
        TextBox txtProducto = (TextBox)((HiddenField)sender).Parent.FindControl("txtProducto");

        //Producto seleccionado ya existe
        txtProducto.BackColor = System.Drawing.Color.FromName("#DBB7FF");
        txtProducto.Enabled = false;
        ((HiddenField)sender).Value = selectedWidgetID;

    }


}