using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class CrearOrdenTrasladoInd : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            InicializarTabla();
            txtFechaInicial.Text = DateTime.Now.ToShortDateString();
            ListarSucursalOrigen();
        }
    }

    void InicializarTabla()
    {
        DataTable dtDetalle = new DataTable();
        dtDetalle.Columns.Add("n_IdProducto");
        dtDetalle.Columns.Add("Producto");
        dtDetalle.Columns.Add("v_CodigoInterno");
        dtDetalle.Columns.Add("i_Cantidad");
        dtDetalle.Columns.Add("f_StockContable");
        Session["Detalle"] = dtDetalle;
    }

    void ListarSucursalOrigen()
    {
        if (Session["dtAlmacenes"] != null)
        {
            DataTable dtAlmacen = new DataTable();
            dtAlmacen = (DataTable)Session["dtAlmacenes"];
            ddlAlmacenOrigen.DataSource = dtAlmacen;
            ddlAlmacenOrigen.DataTextField = "v_Descripcion";
            ddlAlmacenOrigen.DataValueField = "n_IdAlmacen";
            ddlAlmacenOrigen.DataBind();
            ddlAlmacenOrigen.SelectedIndex = 0;
            if (dtAlmacen.Rows.Count > 1)
            {
                ddlAlmacenOrigen.Enabled = true;
            }
            else if (dtAlmacen.Rows.Count == 0)
            {
                BloquearTodo();
            }
            else if (dtAlmacen.Rows.Count == 1) 
            {
                BloquearTodo();
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Su sesión ha caducado. Vuelva a ingresar al sistema.' });</script>", false);
            BloquearTodo();
        }
    }

    void ListarSucursalDestino()
    {
        if (Session["dtAlmacenes"] != null)
        {
            DataTable dtAlmacen2 = new DataTable();
            dtAlmacen2 = (DataTable)Session["dtAlmacenes"];

            if (dtAlmacen2.Rows.Count > 1)
            {
                ddlAlmacenDestino.Enabled = true;
                string origen = ddlAlmacenOrigen.SelectedValue;
                DataTable dtFinal = new DataTable();
                dtFinal.Columns.Add("n_IdAlmacen");
                dtFinal.Columns.Add("v_Descripcion");
                DataRow dr;
                for (int i = 0; i < dtAlmacen2.Rows.Count; i++)
                {
                    if (origen != dtAlmacen2.Rows[i]["n_IdAlmacen"].ToString())
                    {
                        dr = dtFinal.NewRow();
                        dr[0] = dtAlmacen2.Rows[i]["n_IdAlmacen"].ToString();
                        dr[1] = dtAlmacen2.Rows[i]["v_Descripcion"].ToString();
                        dtFinal.Rows.Add(dr);
                    }
                }

                ddlAlmacenDestino.DataSource = dtFinal;
                ddlAlmacenDestino.DataTextField = "v_Descripcion";
                ddlAlmacenDestino.DataValueField = "n_IdAlmacen";
                ddlAlmacenDestino.DataBind();
                ddlAlmacenDestino.SelectedIndex = 0;
            }
            else if (dtAlmacen2.Rows.Count == 0)
            {
                BloquearTodo();
            }
            else if (dtAlmacen2.Rows.Count == 1)
            {
                BloquearTodo();
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Su sesión ha caducado. Vuelva a ingresar al sistema.' });</script>", false);
            BloquearTodo();
        }
    }

    void BloquearTodo() 
    {
        ddlAlmacenOrigen.Enabled = false;
        ddlAlmacenDestino.Enabled = false;
        ibEstablecerSucursalOrigen.Visible = false;
        ibEstablecerSucursalDestino.Visible = false;
        txtFechaInicial.Enabled = false;
        lnkAgregarProducto.Enabled = false;
        gv.Enabled = false;
        txtObservacion.Enabled = false;
        btnGuardar.Enabled = false;
    }

    protected void ibEstablecerSucursal_Click(object sender, ImageClickEventArgs e)
    {
        ddlAlmacenOrigen.Enabled = false;
        ibEstablecerSucursalOrigen.Visible = false;
        ddlAlmacenDestino.Enabled = true;
        ibEstablecerSucursalDestino.Visible = true;
        ListarSucursalDestino();
        gvBuscar.DataBind();
    }

    protected void ibEstablecerSucursalDestino_Click(object sender, ImageClickEventArgs e)
    {
        ddlAlmacenDestino.Enabled = false;
        ibEstablecerSucursalDestino.Visible = false;
        lnkAgregarProducto.Enabled = true;
    }

    protected void lnkAgregarProducto_Click(object sender, EventArgs e)
    {
        panelProductos.Visible = true;
        tblGeneral.Visible = false;
    }

    protected void btnSalirBusqueda_Click(object sender, ImageClickEventArgs e)
    {
        panelProductos.Visible = false;
        tblGeneral.Visible = true;
    }

    protected void btnSalir_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["Detalle"] != null)
        {
            Session.Remove("Detalle");
        }
        Response.Redirect("ListarOrdenTrasladoInd.aspx");
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("CrearOrdenTrasladoInd.aspx");
    }

    protected void gvBuscar_RowCommand(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs e)
    {
        if (e.CommandArgs.CommandName == "Seleccionar")
        {
            int n_IdProducto = int.Parse(e.KeyValue.ToString());
            SqlDataAdapter daBusqueda = new SqlDataAdapter("IMPORT_Stock_Almacen_Listar " + ddlAlmacenOrigen.SelectedValue + "," + n_IdProducto, conexion);
            DataTable dtBusqueda = new DataTable();
            daBusqueda.Fill(dtBusqueda);
            if (dtBusqueda.Rows.Count > 0)
            {
                DataTable dtDetalle = new DataTable();
                dtDetalle = (DataTable)Session["Detalle"];

                //Validar Producto Repetido
                for (int i = 0; i < dtDetalle.Rows.Count; i++)
                {
                    if (dtDetalle.Rows[i]["n_IdProducto"].ToString() == n_IdProducto.ToString())
                    {
                        return;
                    }
                }

                DataRow dr;
                dr = dtDetalle.NewRow();
                dr["n_IdProducto"] = dtBusqueda.Rows[0]["n_IdProducto"].ToString();
                dr["Producto"] = dtBusqueda.Rows[0]["v_Descripcion"].ToString();
                dr["v_CodigoInterno"] = dtBusqueda.Rows[0]["v_CodigoInterno"].ToString();
                dr["i_Cantidad"] = 0;
                dr["f_StockContable"] = dtBusqueda.Rows[0]["f_StockContable"].ToString();
                dtDetalle.Rows.Add(dr);
                Session["Detalle"] = dtDetalle;
                gv.DataSource = dtDetalle;
                gv.DataBind();


                panelProductos.Visible = false;
                tblGeneral.Visible = true;
            }
        }
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["dtUsuario"] != null)
        {
            DataTable dtUsuario = new DataTable();
            dtUsuario = (DataTable)Session["dtUsuario"];
            string n_IdUsuario = dtUsuario.Rows[0]["n_IdUsuario"].ToString();

            int stock = 0;
            bool HayStock = true;

            //Validar que hay stock suficiente para hacer la salida
            for (int i = 0; i < gv.Rows.Count; i++)
            {
                TextBox txtCantidad = new TextBox();
                txtCantidad = (TextBox)gv.Rows[i].FindControl("txtCantidad");
                int Cantidad = int.Parse(txtCantidad.Text);
                if (Cantidad > 0)
                {
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter("select isnull(f_StockContable,0) from stock where n_IdProducto = " + gv.DataKeys[i].Value.ToString() + " and n_IdAlmacen = " + ddlAlmacenOrigen.SelectedValue, conexion);
                    da.Fill(dt);
                    stock = int.Parse(dt.Rows[0][0].ToString());
                    if (stock < Cantidad) { HayStock = false; break; }
                }
            }
            if (HayStock == false)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'No hay stock suficiente para relizar la operación' });</script>", false);
                return;
            }

            SqlTransaction tran;
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);
            cn.Open();
            tran = cn.BeginTransaction();

            try
            {
                //Registrar Cabecera de Orden de Traslado
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = tran;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Play_OrdenTraslado_Insertar";
                cmd.Parameters.AddWithValue("@n_IdAlmacenOrigen", ddlAlmacenOrigen.SelectedValue);
                cmd.Parameters.AddWithValue("@n_IdAlmacenDestino", ddlAlmacenDestino.SelectedValue);
                cmd.Parameters.AddWithValue("@d_FechaEmision", DateTime.Parse(txtFechaInicial.Text + " " + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00")));
                cmd.Parameters.AddWithValue("@t_Observacion", txtObservacion.Text.Trim());
                cmd.Parameters.AddWithValue("@n_IdUsuarioCreacion", n_IdUsuario);

                string i_IdOrdenTraslado = cmd.ExecuteScalar().ToString();
                cmd.Dispose();

                if (i_IdOrdenTraslado.Trim() == "0")
                {
                    tran.Rollback();
                    cn.Close();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'El correlativo de la Orden de Traslado ha terminado' });</script>", false);
                    return;
                }

                SqlCommand cmd0 = new SqlCommand();
                cmd0.Connection = cn;
                cmd0.Transaction = tran;
                cmd0.CommandType = CommandType.Text;
                cmd0.CommandText = "select v_NumeroOrdenTraslado from OrdenTraslado where i_IdOrdenTraslado = " + i_IdOrdenTraslado;
                lblNumero.Text = cmd0.ExecuteScalar().ToString();
                cmd0.Dispose();

                //Registrar Detalle de Orden de Traslado
                for (int i = 0; i < gv.Rows.Count; i++)
                {
                    TextBox txtCantidad = new TextBox();
                    txtCantidad = (TextBox)gv.Rows[i].FindControl("txtCantidad");
                    if (int.Parse(txtCantidad.Text) > 0)
                    {
                        SqlCommand cmdDetalleTraslado = new SqlCommand();
                        cmdDetalleTraslado.Connection = cn;
                        cmdDetalleTraslado.Transaction = tran;
                        cmdDetalleTraslado.CommandType = CommandType.StoredProcedure;
                        cmdDetalleTraslado.CommandText = "Play_OrdenTrasladoDetalle_Insertar";
                        cmdDetalleTraslado.Parameters.AddWithValue("@i_IdOrdenTraslado", i_IdOrdenTraslado);
                        cmdDetalleTraslado.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                        cmdDetalleTraslado.Parameters.AddWithValue("@i_Cantidad", int.Parse(txtCantidad.Text));
                        cmdDetalleTraslado.ExecuteNonQuery();
                        cmdDetalleTraslado.Dispose();
                    }
                }

                //Registrar Cabecera de Nota de Salida
                SqlCommand cmdNotaSalida = new SqlCommand();
                cmdNotaSalida.Connection = cn;
                cmdNotaSalida.Transaction = tran;
                cmdNotaSalida.CommandType = CommandType.StoredProcedure;
                cmdNotaSalida.CommandText = "Play_NotaSalida_Registrar";
                cmdNotaSalida.Parameters.AddWithValue("@n_IdAlmacen", ddlAlmacenOrigen.SelectedValue);
                cmdNotaSalida.Parameters.AddWithValue("@n_IdMotivoTraslado", 6);
                cmdNotaSalida.Parameters.AddWithValue("@d_FechaEmision", DateTime.Parse(txtFechaInicial.Text + " " + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00")));
                cmdNotaSalida.Parameters.AddWithValue("@v_Referencia", lblNumero.Text);
                cmdNotaSalida.Parameters.AddWithValue("@t_Observacion", txtObservacion.Text.Trim());
                cmdNotaSalida.Parameters.AddWithValue("@n_IdUsuarioCreacion", n_IdUsuario);

                string n_IdNotaSalida = cmdNotaSalida.ExecuteScalar().ToString();
                cmdNotaSalida.Dispose();

                if (n_IdNotaSalida.Trim() == "0")
                {
                    tran.Rollback();
                    cn.Close();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'El correlativo de la Nota de Salida ha terminado' });</script>", false);
                    return;
                }

                string v_NumeroNotaSalida = "";
                SqlCommand cmdSalida = new SqlCommand();
                cmdSalida.Connection = cn;
                cmdSalida.Transaction = tran;
                cmdSalida.CommandType = CommandType.Text;
                cmdSalida.CommandText = "select v_NumeroNotaSalida from NotaSalida where n_IdNotaSalida = " + n_IdNotaSalida;
                v_NumeroNotaSalida = cmdSalida.ExecuteScalar().ToString();
                cmdSalida.Dispose();

                //Registrar Detalle Nota de Salida
                for (int i = 0; i < gv.Rows.Count; i++)
                {
                    TextBox txtCantidad = new TextBox();
                    txtCantidad = (TextBox)gv.Rows[i].FindControl("txtCantidad");
                    if (int.Parse(txtCantidad.Text) > 0)
                    {
                        SqlCommand cmdDetalleNotaSalida = new SqlCommand();
                        cmdDetalleNotaSalida.Connection = cn;
                        cmdDetalleNotaSalida.Transaction = tran;
                        cmdDetalleNotaSalida.CommandType = CommandType.StoredProcedure;
                        cmdDetalleNotaSalida.CommandText = "Play_NotaSalidaDetalle_Insert";
                        cmdDetalleNotaSalida.Parameters.AddWithValue("@n_IdNotaSalida", n_IdNotaSalida);
                        cmdDetalleNotaSalida.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                        cmdDetalleNotaSalida.Parameters.AddWithValue("@i_Cantidad", int.Parse(txtCantidad.Text));
                        cmdDetalleNotaSalida.ExecuteNonQuery();
                        cmdDetalleNotaSalida.Dispose();

                        //Actualizar Stock
                        SqlCommand cmdStockS = new SqlCommand();
                        cmdStockS.Connection = cn;
                        cmdStockS.Transaction = tran;
                        cmdStockS.CommandType = CommandType.StoredProcedure;
                        cmdStockS.CommandText = "Play_Stock_Restar_Actualizar";
                        cmdStockS.Parameters.AddWithValue("@n_IdAlmacen", ddlAlmacenOrigen.SelectedValue);
                        cmdStockS.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                        cmdStockS.Parameters.AddWithValue("@f_Cantidad", int.Parse(txtCantidad.Text));
                        cmdStockS.ExecuteNonQuery();
                        cmdStockS.Dispose();

                        //Registrar Kardex
                        SqlCommand cmdKardex = new SqlCommand();
                        cmdKardex.Connection = cn;
                        cmdKardex.Transaction = tran;
                        cmdKardex.CommandType = CommandType.StoredProcedure;
                        cmdKardex.CommandText = "Play_Kardex_Insertar";
                        cmdKardex.Parameters.AddWithValue("@d_FechaMovimiento", DateTime.Parse(txtFechaInicial.Text + " " + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00")));
                        cmdKardex.Parameters.AddWithValue("@c_TipoMovimiento", "S");
                        cmdKardex.Parameters.AddWithValue("@i_IdMotivoTraslado", 6);
                        cmdKardex.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                        cmdKardex.Parameters.AddWithValue("@n_IdAlmacen", ddlAlmacenOrigen.SelectedValue);
                        cmdKardex.Parameters.AddWithValue("@f_Cantidad", int.Parse(txtCantidad.Text));
                        cmdKardex.Parameters.AddWithValue("@n_IdTipoDocumento", 9);
                        cmdKardex.Parameters.AddWithValue("@v_NumeroDocumento", v_NumeroNotaSalida);
                        cmdKardex.Parameters.AddWithValue("@n_IdCliente", DBNull.Value);
                        cmdKardex.Parameters.AddWithValue("@n_IdProveedor", DBNull.Value);
                        cmdKardex.ExecuteNonQuery();
                        cmdKardex.Dispose();
                    }
                }

                 //Registrar Documentos de Orden de Traslado
                SqlCommand cmdOrdenTrasladoDocumento = new SqlCommand();
                cmdOrdenTrasladoDocumento.Connection = cn;
                cmdOrdenTrasladoDocumento.Transaction = tran;
                cmdOrdenTrasladoDocumento.CommandType = CommandType.StoredProcedure;
                cmdOrdenTrasladoDocumento.CommandText = "Play_OrdenTrasladoDocumento_Insertar";
                cmdOrdenTrasladoDocumento.Parameters.AddWithValue("@i_IdOrdenTraslado", i_IdOrdenTraslado);
                cmdOrdenTrasladoDocumento.Parameters.AddWithValue("@n_IdAlmacen", ddlAlmacenOrigen.SelectedValue);
                cmdOrdenTrasladoDocumento.Parameters.AddWithValue("@n_IdTipoDocumento", 9);//Nota de Salida
                cmdOrdenTrasladoDocumento.Parameters.AddWithValue("@v_NumeroDocumento", v_NumeroNotaSalida);
                cmdOrdenTrasladoDocumento.ExecuteNonQuery();
                cmdOrdenTrasladoDocumento.Dispose();


                //Actualizar Correlativo de Orden de Traslado
                SqlCommand cmd5 = new SqlCommand();
                cmd5.Connection = cn;
                cmd5.Transaction = tran;
                cmd5.CommandType = CommandType.StoredProcedure;
                cmd5.CommandText = "Play_Correlativo_Aumentar";
                cmd5.Parameters.AddWithValue("@n_IdTipoDocumento", 11);//Orden de Traslado
                cmd5.Parameters.AddWithValue("@n_IdAlmacen", ddlAlmacenOrigen.SelectedValue);
                cmd5.ExecuteNonQuery();
                cmd5.Dispose();

                //Actualizar Correlativo de Nota de Salida
                SqlCommand cmdNotaSalidaCorrelativo = new SqlCommand();
                cmdNotaSalidaCorrelativo.Connection = cn;
                cmdNotaSalidaCorrelativo.Transaction = tran;
                cmdNotaSalidaCorrelativo.CommandType = CommandType.StoredProcedure;
                cmdNotaSalidaCorrelativo.CommandText = "Play_Correlativo_Aumentar";
                cmdNotaSalidaCorrelativo.Parameters.AddWithValue("@n_IdTipoDocumento", 9);//Nota de Salida
                cmdNotaSalidaCorrelativo.Parameters.AddWithValue("@n_IdAlmacen", ddlAlmacenOrigen.SelectedValue);
                cmdNotaSalidaCorrelativo.ExecuteNonQuery();
                cmdNotaSalidaCorrelativo.Dispose();


                //Registrar Cabecera de la Nota de Ingreso
                SqlCommand cmdNotaIngreso = new SqlCommand();
                cmdNotaIngreso.Connection = cn;
                cmdNotaIngreso.Transaction = tran;
                cmdNotaIngreso.CommandType = CommandType.StoredProcedure;
                cmdNotaIngreso.CommandText = "Play_NotaIngreso_Insertar";
                cmdNotaIngreso.Parameters.AddWithValue("@n_IdAlmacen", ddlAlmacenDestino.SelectedValue);
                cmdNotaIngreso.Parameters.AddWithValue("@n_IdMotivoTraslado", 6);
                cmdNotaIngreso.Parameters.AddWithValue("@d_FechaEmision", DateTime.Parse(txtFechaInicial.Text + " " + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00")));
                cmdNotaIngreso.Parameters.AddWithValue("@v_Referencia", lblNumero.Text);
                cmdNotaIngreso.Parameters.AddWithValue("@t_Observacion", txtObservacion.Text.Trim());
                cmdNotaIngreso.Parameters.AddWithValue("@n_IdUsuarioCreacion", n_IdUsuario);

                string n_IdNotaIngreso = cmdNotaIngreso.ExecuteScalar().ToString();
                cmdNotaIngreso.Dispose();

                if (n_IdNotaIngreso.Trim() == "0")
                {
                    tran.Rollback();
                    cn.Close();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'El correlativo de la Nota de Ingreso ha terminado' });</script>", false);
                    return;
                }

                string v_NumeroNotaIngreso = "";
                SqlCommand cmdIngreso = new SqlCommand();
                cmdIngreso.Connection = cn;
                cmdIngreso.Transaction = tran;
                cmdIngreso.CommandType = CommandType.Text;
                cmdIngreso.CommandText = "select v_NumeroNotaIngreso from NotaIngreso where n_IdNotaIngreso = " + n_IdNotaIngreso;
                v_NumeroNotaIngreso = cmdIngreso.ExecuteScalar().ToString();
                cmdIngreso.Dispose();

                for (int i = 0; i < gv.Rows.Count; i++)
                {
                    //Registrar Detalle Nota de Ingreso
                    TextBox txtCantidad = new TextBox();
                    txtCantidad = (TextBox)gv.Rows[i].FindControl("txtCantidad");
                    if (int.Parse(txtCantidad.Text) > 0)
                    {
                        SqlCommand cmdDetalleNotaIngreso = new SqlCommand();
                        cmdDetalleNotaIngreso.Connection = cn;
                        cmdDetalleNotaIngreso.Transaction = tran;
                        cmdDetalleNotaIngreso.CommandType = CommandType.StoredProcedure;
                        cmdDetalleNotaIngreso.CommandText = "Play_NotaIngresoDetalle_Insert";
                        cmdDetalleNotaIngreso.Parameters.AddWithValue("@n_IdNotaIngreso", n_IdNotaIngreso);
                        cmdDetalleNotaIngreso.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                        cmdDetalleNotaIngreso.Parameters.AddWithValue("@i_Cantidad", int.Parse(txtCantidad.Text));
                        cmdDetalleNotaIngreso.ExecuteNonQuery();
                        cmdDetalleNotaIngreso.Dispose();

                        //Actualizar Stock
                        SqlCommand cmdStock = new SqlCommand();
                        cmdStock.Connection = cn;
                        cmdStock.Transaction = tran;
                        cmdStock.CommandType = CommandType.StoredProcedure;
                        cmdStock.CommandText = "Play_Stock_Actualizar";
                        cmdStock.Parameters.AddWithValue("@n_IdAlmacen", ddlAlmacenDestino.SelectedValue);
                        cmdStock.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                        cmdStock.Parameters.AddWithValue("@f_Cantidad", int.Parse(txtCantidad.Text));
                        cmdStock.ExecuteNonQuery();
                        cmdStock.Dispose();

                        //Registrar Kardex
                        SqlCommand cmdKardex = new SqlCommand();
                        cmdKardex.Connection = cn;
                        cmdKardex.Transaction = tran;
                        cmdKardex.CommandType = CommandType.StoredProcedure;
                        cmdKardex.CommandText = "Play_Kardex_Insertar";
                        cmdKardex.Parameters.AddWithValue("@d_FechaMovimiento", DateTime.Parse(txtFechaInicial.Text + " " + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00")));
                        cmdKardex.Parameters.AddWithValue("@c_TipoMovimiento", "I");
                        cmdKardex.Parameters.AddWithValue("@i_IdMotivoTraslado", 6);
                        cmdKardex.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                        cmdKardex.Parameters.AddWithValue("@n_IdAlmacen", ddlAlmacenDestino.SelectedValue);
                        cmdKardex.Parameters.AddWithValue("@f_Cantidad", int.Parse(txtCantidad.Text));
                        cmdKardex.Parameters.AddWithValue("@n_IdTipoDocumento", 8);
                        cmdKardex.Parameters.AddWithValue("@v_NumeroDocumento", v_NumeroNotaIngreso);
                        cmdKardex.Parameters.AddWithValue("@n_IdCliente", DBNull.Value);
                        cmdKardex.Parameters.AddWithValue("@n_IdProveedor", DBNull.Value);
                        cmdKardex.ExecuteNonQuery();
                        cmdKardex.Dispose();
                    }
                }

                //Registrar Documentos de Orden de Traslado
                SqlCommand cmdOrdenTrasladoDocumentoIngreso = new SqlCommand();
                cmdOrdenTrasladoDocumentoIngreso.Connection = cn;
                cmdOrdenTrasladoDocumentoIngreso.Transaction = tran;
                cmdOrdenTrasladoDocumentoIngreso.CommandType = CommandType.StoredProcedure;
                cmdOrdenTrasladoDocumentoIngreso.CommandText = "Play_OrdenTrasladoDocumento_Insertar";
                cmdOrdenTrasladoDocumentoIngreso.Parameters.AddWithValue("@i_IdOrdenTraslado", i_IdOrdenTraslado);
                cmdOrdenTrasladoDocumentoIngreso.Parameters.AddWithValue("@n_IdAlmacen", ddlAlmacenDestino.SelectedValue);
                cmdOrdenTrasladoDocumentoIngreso.Parameters.AddWithValue("@n_IdTipoDocumento", 8);
                cmdOrdenTrasladoDocumentoIngreso.Parameters.AddWithValue("@v_NumeroDocumento", v_NumeroNotaIngreso);
                cmdOrdenTrasladoDocumentoIngreso.ExecuteNonQuery();
                cmdOrdenTrasladoDocumentoIngreso.Dispose();

                //Actualizar Correlativo de Nota de Ingreso
                SqlCommand cmdNotaIngresoCorrelativo = new SqlCommand();
                cmdNotaIngresoCorrelativo.Connection = cn;
                cmdNotaIngresoCorrelativo.Transaction = tran;
                cmdNotaIngresoCorrelativo.CommandType = CommandType.StoredProcedure;
                cmdNotaIngresoCorrelativo.CommandText = "Play_Correlativo_Aumentar";
                cmdNotaIngresoCorrelativo.Parameters.AddWithValue("@n_IdTipoDocumento", 8);
                cmdNotaIngresoCorrelativo.Parameters.AddWithValue("@n_IdAlmacen", ddlAlmacenDestino.SelectedValue);
                cmdNotaIngresoCorrelativo.ExecuteNonQuery();
                cmdNotaIngresoCorrelativo.Dispose();

                tran.Commit();
                BloquearOrden();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Orden de Traslado Registrada Satisfactoriamente' });</script>", false);
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

    void BloquearOrden()
    {
        ddlAlmacenOrigen.Enabled = false;
        ddlAlmacenDestino.Enabled = false;
        txtFechaInicial.Enabled = false;
        gv.Enabled = false;
        txtObservacion.Enabled = false;
        btnGuardar.Enabled = false;
        lnkAgregarProducto.Enabled = false;
    }


}