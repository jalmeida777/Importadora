﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class CrearPedido : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            txtFechaInicial.Text = DateTime.Now.ToShortDateString();
            txtfecump.Text = DateTime.Now.ToShortDateString();
            ListarFormaPago();
            ListarSucursal();
            ddlVendedor.Enabled = false;
            ListarDistrito();
            if (ddlTienda.Items.Count == 0) { BloquearTodo(); return; }
            ListarMoneda();
            ListarCategoria();
            InicializarGrilla();
            ListarProductos("", "", "", ddlTienda.SelectedValue,"p");

            if (Request.QueryString["n_IdPedido"] != null)
            {
                string n_IdPedido = Request.QueryString["n_IdPedido"];
                //Ver Cabecera del Pedido
                SqlDataAdapter daPedido = new SqlDataAdapter("Play_Pedido_Seleccionar " + n_IdPedido, conexion);
                DataTable dtPedido = new DataTable();
                daPedido.Fill(dtPedido);

                lblNumeroPedido.Text = dtPedido.Rows[0]["v_NumeroPedido"].ToString();
                lblIdPedido.Text = n_IdPedido;
                hdnValue.Value = dtPedido.Rows[0]["n_IdCliente"].ToString();
                txtCliente.Text = dtPedido.Rows[0]["v_Nombre"].ToString();
                txtFechaInicial.Text = DateTime.Parse(dtPedido.Rows[0]["d_FechaEmision"].ToString()).ToShortDateString();
                ddlFormaPago.SelectedValue = dtPedido.Rows[0]["n_IdFormaPago"].ToString();
                ddlFormaPago_SelectedIndexChanged(null, null);
                ddlMoneda.SelectedValue = dtPedido.Rows[0]["n_IdMoneda"].ToString();
                ddlMoneda_SelectedIndexChanged(null, null);
                ddlBanco.SelectedValue = dtPedido.Rows[0]["n_IdBanco"].ToString();
                ddlTienda.SelectedValue = dtPedido.Rows[0]["n_IdAlmacen"].ToString();
                lblSubTotal.Text = decimal.Parse(dtPedido.Rows[0]["f_SubTotal"].ToString()).ToString("N2");
                lblTotal.Text = decimal.Parse(dtPedido.Rows[0]["f_Total"].ToString()).ToString("N2");
                txtPago.Text = decimal.Parse(dtPedido.Rows[0]["f_Pago"].ToString()).ToString("N2");
                lblVuelto.Text = decimal.Parse(dtPedido.Rows[0]["f_Vuelto"].ToString()).ToString("N2");
                txtObservacion.Text = dtPedido.Rows[0]["t_Obs"].ToString();
                bool Estado = bool.Parse(dtPedido.Rows[0]["b_EstadoPedido"].ToString());
                lblUsuarioRegistro.Text = dtPedido.Rows[0]["UsuarioRegistra"].ToString();
                lblFechaRegistro.Text = dtPedido.Rows[0]["d_FechaRegistra"].ToString();
                if (dtPedido.Rows[0]["UsuarioFotoRegistra"].ToString().Trim() != "")
                {
                    ibUsuarioRegistro.ImageUrl = dtPedido.Rows[0]["UsuarioFotoRegistra"].ToString();
                }
                else
                {
                    ibUsuarioRegistro.ImageUrl = "~/images/face.jpg";
                }

                txtDescuento.Text = decimal.Parse(dtPedido.Rows[0]["f_Descuento"].ToString()).ToString("N2");

                ibEstablecerSucursal_Click(null, null);

                lnkAgregarProducto.Visible = false;
                txtDescuento.Enabled = false;

                //Ver Detalle del Pedido
                SqlDataAdapter daDetPedido = new SqlDataAdapter("Play_DetPedido_Listar " + n_IdPedido, conexion);
                DataTable dtDetPedido = new DataTable();
                daDetPedido.Fill(dtDetPedido);
                Session["Detalle"] = dtDetPedido;
                gv.DataSource = dtDetPedido;
                gv.DataBind();
                gv.Columns[1].Visible = false;

                if (hdnValue.Value.Trim() != "")
                {
                    btnCliente.Visible = true;
                    btnCliente.Enabled = true;
                }


                string n_IdUsuarioVendedor = dtPedido.Rows[0]["n_IdUsuarioVendedor"].ToString();
                if (n_IdUsuarioVendedor != "")
                {
                    ddlVendedor.SelectedValue = n_IdUsuarioVendedor;
                }



                btnAnular.Visible = true;
                btnAnular.Enabled = true;
                lblNumeroPedido.Visible = true;
                lnkAgregarProducto.Enabled = true;
                Label1.Text = "Pedido N° ";
                BloquearPedido();

                if (Estado == false)  // Está anulado
                {
                    Label1.ForeColor = System.Drawing.Color.Red;
                    lblNumeroPedido.ForeColor = System.Drawing.Color.Red;
                    btnCliente.Visible = false;
                    gv.Columns[6].Visible = false;
                    lnkAgregarProducto.Visible = false;
                    BloquearTodo();
                }
            }
            
        }
    }

    public void ListarFormaPago()
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter("Play_FormaPago_Combo", conexion);
        da.Fill(dt);

        ddlFormaPago.DataSource = dt;
        ddlFormaPago.DataTextField = "v_FormaPago";
        ddlFormaPago.DataValueField = "n_IdFormaPago";
        ddlFormaPago.DataBind();
        ddlFormaPago.SelectedIndex = 0;
    }

    public void ListarMoneda()
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter("Play_Moneda_Combo", conexion);
        da.Fill(dt);
        ddlMoneda.DataSource = dt;
        ddlMoneda.DataTextField = "v_DescripcionMoneda";
        ddlMoneda.DataValueField = "n_IdMoneda";
        ddlMoneda.DataBind();
        ddlMoneda.SelectedIndex = 0;
        ddlMoneda_SelectedIndexChanged(null, null);
    }

    void ListarCategoria()
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter("Play_Categoria_Combo", conexion);
        da.Fill(dt);

        MenuItem mnuNewMenuItem;
        string v_Descripcion;
        string n_IdCategoria;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            v_Descripcion = dt.Rows[i]["v_Descripcion"].ToString();
            n_IdCategoria = dt.Rows[i]["n_IdCategoria"].ToString();
            mnuNewMenuItem = new MenuItem(v_Descripcion, n_IdCategoria);
            MenuFamilia.Items.Add(mnuNewMenuItem);
        }

    }

    void ListarSubCategorias()
    {
        MenuSubFamilia.Items.Clear();
        string n_IdCategoria = MenuFamilia.SelectedItem.Value;
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter("Play_SubCategoria_Combo " + n_IdCategoria, conexion);
        da.Fill(dt);

        MenuItem mnuNewMenuItem;
        string v_Descripcion;
        string n_IdSubCategoria;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            v_Descripcion = dt.Rows[i]["v_Descripcion"].ToString();
            n_IdSubCategoria = dt.Rows[i]["n_IdSubCategoria"].ToString();
            mnuNewMenuItem = new MenuItem(v_Descripcion, n_IdSubCategoria);
            MenuSubFamilia.Items.Add(mnuNewMenuItem);
        }
    }

    void ListarProductos(string Familia, string SubFamilia, string Busqueda, string IdAlmacen, string tipo)
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter("Play_Producto_Listar_Imagenes_Venta '" + Familia + "','" + SubFamilia + "','" + Busqueda + "'," + IdAlmacen + ",'" + tipo + "'", conexion);
        da.Fill(dt);
        gvProductos.DataSource = dt;
        gvProductos.DataBind();
    }

    void ListarSucursal() 
    {
        if (Session["dtAlmacenes"] != null)
        {
            DataTable dtAlmacen = new DataTable();
            dtAlmacen = (DataTable)Session["dtAlmacenes"];
            ddlTienda.DataSource = dtAlmacen;
            ddlTienda.DataTextField = "v_Descripcion";
            ddlTienda.DataValueField = "n_IdAlmacen";
            ddlTienda.DataBind();
            ddlTienda.SelectedIndex = 0;
            if (dtAlmacen.Rows.Count > 1)
            {
                ddlTienda.Enabled = true;
            }
            else
            {
                ddlTienda.Enabled = false;
            }
        }
        else 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Su sesión ha caducado. Vuelva a ingresar al sistema.' });</script>", false);
            BloquearTodo();
        }
    }

    void ListarUsuariosVendedores() 
    {
        try
        {
            string sucursal = ddlTienda.SelectedValue;
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Play_UsuarioVendedor_Combo " + sucursal, conexion);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                ddlVendedor.DataSource = dt;
                ddlVendedor.DataTextField = "v_Nombre";
                ddlVendedor.DataValueField = "n_IdUsuario";
                ddlVendedor.DataBind();
                ddlVendedor.Items.Insert(0, "NINGUNO");
                ddlVendedor.SelectedIndex = 0;
            }
            else { BloquearTodo(); }
        }
        catch (Exception)
        {
            
        }

    }

    void InicializarGrilla()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("i_Cantidad", typeof(int));
        dt.Columns.Add("Producto", typeof(String));
        dt.Columns.Add("f_PrecioUnitario", typeof(Double));
        dt.Columns.Add("f_PrecioTotal", typeof(Double));
        dt.Columns.Add("n_IdProducto");
        dt.Columns.Add("Codigo", typeof(String));
        dt.Columns.Add("Stock", typeof(int));

        Session["Detalle"] = dt;
        gv.DataSource = dt;
        gv.DataBind();
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dt = new DataTable();
        if (Session["Detalle"] != null)
        {
            //Validar que exista productos en el detalle del pedido
            dt = (DataTable)Session["Detalle"];
            if (dt.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'No hay productos para vender' });</script>", false);
                return;
            }
        }
        else 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Su sesión ha caducado. Vuelva a ingresar al sistema.' });</script>", false);
            return;
        }

        if (txtPago.Text.Trim() == "") 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar el monto Pagó con...' });</script>", false);
            txtPago.Focus();
            return;
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
                string n_IdPedido = "";

                if (Request.QueryString["n_IdPedido"] != null) 
                {
                    //Si el pedido aun no tiene comprobante
                    n_IdPedido = Request.QueryString["n_IdPedido"].ToString();
                    SqlCommand cmdPedido = new SqlCommand();
                    cmdPedido.Connection = cn;
                    cmdPedido.Transaction = tran;
                    cmdPedido.CommandType = CommandType.StoredProcedure;
                    cmdPedido.CommandText = "Play_Pedido_Actualizar";
                    cmdPedido.Parameters.AddWithValue("@n_IdPedido", n_IdPedido);

                    if (hdnValue.Value.Trim() == "")
                    {
                        cmdPedido.Parameters.AddWithValue("@n_IdCliente", DBNull.Value);
                    }
                    else
                    {
                        cmdPedido.Parameters.AddWithValue("@n_IdCliente", hdnValue.Value);
                    }

                    cmdPedido.Parameters.AddWithValue("@n_IdFormaPago", ddlFormaPago.SelectedValue);

                    if (ddlBanco.Items.Count > 0)
                    {
                        cmdPedido.Parameters.AddWithValue("@n_IdBanco", ddlBanco.SelectedValue);
                    }
                    else
                    {
                        cmdPedido.Parameters.AddWithValue("@n_IdBanco", DBNull.Value);
                    }

                    cmdPedido.Parameters.AddWithValue("@f_SubTotal", lblSubTotal.Text);
                    cmdPedido.Parameters.AddWithValue("@f_Impuesto", 0);
                    cmdPedido.Parameters.AddWithValue("@f_Total", lblTotal.Text);
                    cmdPedido.Parameters.AddWithValue("@f_Pago", txtPago.Text);
                    cmdPedido.Parameters.AddWithValue("@f_Vuelto", lblVuelto.Text);
                    cmdPedido.Parameters.AddWithValue("@t_Obs", txtObservacion.Text);
                    cmdPedido.Parameters.AddWithValue("@n_IdUsuarioModifica", n_IdUsuario);

                    cmdPedido.Parameters.AddWithValue("@d_FechaEmision", DateTime.Parse(txtFechaInicial.Text + " " + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00")));
                    if (ddlVendedor.SelectedIndex == 0) 
                    {
                        cmdPedido.Parameters.AddWithValue("@n_IdUsuarioVendedor", DBNull.Value);
                    }
                    else
                    {
                        cmdPedido.Parameters.AddWithValue("@n_IdUsuarioVendedor", ddlVendedor.SelectedValue);
                    }
                    cmdPedido.Parameters.AddWithValue("@f_Descuento", txtDescuento.Text);

                    cmdPedido.ExecuteNonQuery();
                    cmdPedido.Dispose();

                    
                    tran.Commit();
                    BloquearTodo();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Pedido Actualizado Satisfactoriamente' });</script>", false);

                }
                else if (Request.QueryString["n_IdPedido"] == null)
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.Transaction = tran;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Play_Pedido_Registrar";
                    cmd.Parameters.AddWithValue("@n_IdAlmacen", ddlTienda.SelectedValue);

                    if (hdnValue.Value.Trim() == "")
                    {
                        cmd.Parameters.AddWithValue("@n_IdCliente", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@n_IdCliente", hdnValue.Value);
                    }

                    cmd.Parameters.AddWithValue("@n_IdFormaPago", ddlFormaPago.SelectedValue);

                    if (ddlBanco.Items.Count > 0)
                    {
                        cmd.Parameters.AddWithValue("@n_IdBanco", ddlBanco.SelectedValue);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@n_IdBanco", DBNull.Value);
                    }

                    cmd.Parameters.AddWithValue("@n_IdMoneda", ddlMoneda.SelectedValue);

                    cmd.Parameters.AddWithValue("@d_FechaEmision", DateTime.Parse(txtFechaInicial.Text + " " + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00")));

                    cmd.Parameters.AddWithValue("@f_SubTotal", lblSubTotal.Text);
                    cmd.Parameters.AddWithValue("@f_Impuesto", 0);
                    cmd.Parameters.AddWithValue("@f_Total", lblTotal.Text);
                    cmd.Parameters.AddWithValue("@f_Pago", txtPago.Text);
                    cmd.Parameters.AddWithValue("@f_Vuelto", lblVuelto.Text);
                    cmd.Parameters.AddWithValue("@t_Obs", txtObservacion.Text);
                    cmd.Parameters.AddWithValue("@n_IdUsuarioRegistra", n_IdUsuario);

                    if (ddlVendedor.SelectedIndex == 0) 
                    {
                        cmd.Parameters.AddWithValue("@n_IdUsuarioVendedor", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@n_IdUsuarioVendedor", ddlVendedor.SelectedValue);
                    }
                    cmd.Parameters.AddWithValue("@f_Descuento", txtDescuento.Text);

                    n_IdPedido = cmd.ExecuteScalar().ToString();
                    
                    cmd.Dispose();

                    if (n_IdPedido.Trim() == "0")
                    {
                        tran.Rollback();
                        cn.Close();
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'El correlativo del Pedido ha terminado' });</script>", false);
                        return;
                    }

                    string NumeroPedido = "";

                    SqlCommand cmd0 = new SqlCommand();
                    cmd0.Connection = cn;
                    cmd0.Transaction = tran;
                    cmd0.CommandType = CommandType.Text;
                    cmd0.CommandText = "select v_NumeroPedido from Pedido where n_IdPedido = " + n_IdPedido;
                    NumeroPedido = cmd0.ExecuteScalar().ToString();
                    cmd0.Dispose();

                    int stockContable = 0;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //validar el stock antes de guardar 
                        SqlCommand cmdStockContable = new SqlCommand();
                        cmdStockContable.Connection = cn;
                        cmdStockContable.Transaction = tran;
                        cmdStockContable.CommandType = CommandType.Text;
                        cmdStockContable.CommandText = "select f_StockContable from stock where n_IdProducto = " + dt.Rows[i]["n_IdProducto"].ToString() + " and n_IdAlmacen = " + ddlTienda.SelectedValue;
                        stockContable = int.Parse(cmdStockContable.ExecuteScalar().ToString());

                        if (stockContable < int.Parse(dt.Rows[i]["i_Cantidad"].ToString()))
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'No hay stock suficiente' });</script>", false);
                            tran.Rollback();
                            cn.Close();
                            return;
                        }

                        SqlCommand cmdDetalle = new SqlCommand();
                        cmdDetalle.Connection = cn;
                        cmdDetalle.Transaction = tran;
                        cmdDetalle.CommandType = CommandType.StoredProcedure;
                        cmdDetalle.CommandText = "Play_DetPedido_Registrar";
                        cmdDetalle.Parameters.AddWithValue("@n_IdPedido", n_IdPedido);
                        cmdDetalle.Parameters.AddWithValue("@n_IdProducto", dt.Rows[i]["n_IdProducto"].ToString());
                        cmdDetalle.Parameters.AddWithValue("@i_Cantidad", dt.Rows[i]["i_Cantidad"].ToString());
                        cmdDetalle.Parameters.AddWithValue("@f_PrecioUnitario", dt.Rows[i]["f_PrecioUnitario"].ToString());
                        cmdDetalle.Parameters.AddWithValue("@f_PrecioTotal", dt.Rows[i]["f_PrecioTotal"].ToString());
                        cmdDetalle.ExecuteNonQuery();
                        cmdDetalle.Dispose();

                        //Actualizar el Stock Restar
                        SqlCommand cmdStock = new SqlCommand();
                        cmdStock.Connection = cn;
                        cmdStock.Transaction = tran;
                        cmdStock.CommandType = CommandType.StoredProcedure;
                        cmdStock.CommandText = "Play_Stock_Restar_Actualizar";
                        cmdStock.Parameters.AddWithValue("@n_IdAlmacen", ddlTienda.SelectedValue);
                        cmdStock.Parameters.AddWithValue("@n_IdProducto", dt.Rows[i]["n_IdProducto"].ToString());
                        cmdStock.Parameters.AddWithValue("@f_Cantidad", dt.Rows[i]["i_Cantidad"].ToString());
                        cmdStock.ExecuteNonQuery();
                        cmdStock.Dispose();

                        //Registrar Kardex
                        SqlCommand cmdKardex = new SqlCommand();
                        cmdKardex.Connection = cn;
                        cmdKardex.Transaction = tran;
                        cmdKardex.CommandType = CommandType.StoredProcedure;
                        cmdKardex.CommandText = "Play_Kardex_Insertar";
                        cmdKardex.Parameters.AddWithValue("@d_FechaMovimiento", DateTime.Parse(txtFechaInicial.Text + " " + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00")));
                        cmdKardex.Parameters.AddWithValue("@c_TipoMovimiento", "S");
                        cmdKardex.Parameters.AddWithValue("@i_IdMotivoTraslado", 1);
                        cmdKardex.Parameters.AddWithValue("@n_IdProducto", dt.Rows[i]["n_IdProducto"].ToString());
                        cmdKardex.Parameters.AddWithValue("@n_IdAlmacen", ddlTienda.SelectedValue);
                        cmdKardex.Parameters.AddWithValue("@f_Cantidad", int.Parse(dt.Rows[i]["i_Cantidad"].ToString()));
                        cmdKardex.Parameters.AddWithValue("@n_IdTipoDocumento", 1);//Pedido
                        cmdKardex.Parameters.AddWithValue("@v_NumeroDocumento", NumeroPedido);
                        if (hdnValue.Value.Trim() == "")
                        {
                            cmdKardex.Parameters.AddWithValue("@n_IdCliente", DBNull.Value);
                        }
                        else
                        {
                            cmdKardex.Parameters.AddWithValue("@n_IdCliente", hdnValue.Value);
                        }
                        cmdKardex.Parameters.AddWithValue("@n_IdProveedor", DBNull.Value);
                        cmdKardex.ExecuteNonQuery();
                        cmdKardex.Dispose();
                    }

                    //Actualizar Correlativo del Pedido
                    SqlCommand cmd5 = new SqlCommand();
                    cmd5.Connection = cn;
                    cmd5.Transaction = tran;
                    cmd5.CommandType = CommandType.StoredProcedure;
                    cmd5.CommandText = "Play_Correlativo_Aumentar";
                    cmd5.Parameters.AddWithValue("@n_IdTipoDocumento", 1);
                    cmd5.Parameters.AddWithValue("@n_IdAlmacen", ddlTienda.SelectedValue);
                    cmd5.ExecuteNonQuery();
                    cmd5.Dispose();

                    //Aumentar Puntos del Cliente
                    if (hdnValue.Value.Trim() != "")
                    {
                        int Puntos = Convert.ToInt32(Math.Floor(Convert.ToDouble(lblTotal.Text)));
                        SqlCommand cmd6 = new SqlCommand();
                        cmd6.Connection = cn;
                        cmd6.Transaction = tran;
                        cmd6.CommandType = CommandType.StoredProcedure;
                        cmd6.CommandText = "POI_Cliente_AumentarPuntos";
                        cmd6.Parameters.AddWithValue("@n_IdCliente", hdnValue.Value);
                        cmd6.Parameters.AddWithValue("@Cantidad", Puntos);
                        cmd6.ExecuteNonQuery();
                        cmd6.Dispose();
                    }

                    tran.Commit();
                    lblIdPedido.Text = n_IdPedido;
                    lblNumeroPedido.Text = NumeroPedido;
                    lblNumeroPedido.ForeColor = System.Drawing.Color.FromName("#0A9E5D");
                    Label1.ForeColor = System.Drawing.Color.FromName("#0A9E5D");
                    BloquearTodo();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Pedido Registrado Satisfactoriamente' });</script>", false);

                }

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
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Su sesión ha caducado. Vuelva a ingresar al sistema.' });</script>", false);
            BloquearTodo();
        }

    }

    void BloquearPedido() 
    {
        ddlTienda.Enabled = false;
        btnAnular.Enabled = true;
        btnImprimir.Visible = true;
        btnImprimir.Enabled = true;
        ibEstablecerSucursal.Visible = false;
        gv.Enabled = false;
        txtPago.Enabled = false;
    }

    void BloquearTodo() 
    {
        txtCliente.Enabled = false;
        txtFechaInicial.Enabled = false;
        ddlFormaPago.Enabled = false;
        ddlBanco.Enabled = false;
        btnCliente.Enabled = false;
        ddlMoneda.Enabled = false;
        ddlTienda.Enabled = false;
        lnkAgregarProducto.Enabled = false;
        gv.Enabled = false;
        txtPago.Enabled = false;
        txtObservacion.Enabled = false;
        btnGuardar.Enabled = false;
        btnAnular.Enabled = false;
        btnImprimir.Enabled = false;
        ddlVendedor.Enabled = false;
        ibEstablecerSucursal.Visible = false;
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
            Response.Redirect("CrearPedido.aspx");
    }

    protected void btnSalir_Click(object sender, ImageClickEventArgs e)
    {
        Session.Remove("Detalle");
        Response.Redirect("ListarPedidos.aspx");
    }

    protected void lnkAgregarProducto_Click(object sender, EventArgs e)
    {
        ListarProductos("", "", "", ddlTienda.SelectedValue, rblTipo.SelectedItem.Value);
        panelProductos.Visible = true;
        tblGeneral.Visible = false;
        toolbar.Visible = false;
    }

    void CalcularGrilla()
    {
        try
        {


            if (Session["Detalle"] != null)
            {
                double Cantidad = 0;
                double f_TC = 1;
                double PrecioSoles = 0;
                double Precio = 0;
                double PrecioTotal = 0;
                double SubTotal = 0;
                double Total = 0;
                int stock = 0;

                DataTable dt = new DataTable();
                dt = (DataTable)Session["Detalle"];

                for (int i = 0; i < gv.Rows.Count; i++)
                {
                    TextBox txtCantidad = (TextBox)gv.Rows[i].FindControl("txtCantidad");
                    Label lblStock = (Label)gv.Rows[i].FindControl("lblStock");
                    stock = int.Parse(lblStock.Text);

                    if (txtCantidad.Text.Trim() == "")
                    {
                        Cantidad = 1;
                        txtCantidad.Text = "1";
                    }
                    else
                    {
                        Cantidad = double.Parse(txtCantidad.Text.Trim());
                    }

                    if (Cantidad <= 0)
                    {
                        Cantidad = 1;
                    }

                    //Validar que la cantidad sea menor o igual al stock
                    if (Cantidad > stock)
                    {
                        txtCantidad.Text = stock.ToString();
                        Cantidad = stock;
                        return;
                    }

                    f_TC = 1;

                    if (ddlMoneda.SelectedItem.Text.Trim() == "DOLARES")
                    {
                        int anio = DateTime.Now.Year;
                        int mes = DateTime.Now.Month;
                        int dia = DateTime.Now.Day;
                        bool existe = false;
                        SqlDataAdapter daTC = new SqlDataAdapter("Play_TC_Existencia " + anio + "," + mes + "," + dia, conexion);
                        DataTable dtTC = new DataTable();
                        daTC.Fill(dtTC);
                        if (dtTC != null)
                        {
                            if (dtTC.Rows.Count > 0)
                            {
                                existe = true;
                            }
                            else
                            {
                                existe = false;
                            }
                        }
                        else { existe = false; }

                        if (existe == true)
                        {
                            f_TC = double.Parse(dtTC.Rows[0]["f_TC"].ToString());
                        }
                        else
                        {
                            //BloquearOrdenCompra();
                            ddlMoneda.SelectedValue = "1";
                            ddlMoneda_SelectedIndexChanged(null, null);
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.error({ message: 'Debe ingresar el tipo de cambio para el día de hoy' });</script>", false);
                            return;
                        }
                    }

                    dt.Rows[i]["i_Cantidad"] = Cantidad;
                    dt.Rows[i]["Producto"] = dt.Rows[i]["Producto"].ToString();
                    //PrecioSoles = double.Parse(dt.Rows[i]["f_PrecioUnitario"].ToString());
                    TextBox txtPrecio = (TextBox)gv.Rows[i].FindControl("txtPrecio");
                    PrecioSoles = double.Parse(txtPrecio.Text);
                    if (ddlMoneda.SelectedItem.Text.Trim() == "DOLARES")
                    {
                        Precio = PrecioSoles / f_TC;
                        dt.Rows[i]["f_PrecioUnitario"] = Precio;
                    }
                    else
                    {
                        Precio = PrecioSoles;
                        dt.Rows[i]["f_PrecioUnitario"] = Precio;
                    }

                    PrecioTotal = Cantidad * Precio;

                    dt.Rows[i]["f_PrecioTotal"] = PrecioTotal;

                    dt.Rows[i]["n_IdProducto"] = dt.Rows[i]["n_IdProducto"].ToString();

                    SubTotal = SubTotal + PrecioTotal;
                }

                lblSubTotal.Text = SubTotal.ToString("N2");

                //Total = SubTotal;
                double descuento = double.Parse(txtDescuento.Text);
                Total = SubTotal - descuento;

                lblTotal.Text = Total.ToString("N2");

                txtPago.Text = Total.ToString("N2");
                lblVuelto.Text = "0.00";

                Session["Detalle"] = dt;

                gv.DataSource = dt;
                gv.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Su sesión ha caducado. Vuelva a ingresar al sistema.' });</script>", false);
                BloquearTodo();
            }

        }
        catch (Exception)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Ha ingresado un valor no válido en las cantidades o montos.' });</script>", false);
        }
    }

    protected void txtCantidad_TextChanged(object sender, EventArgs e)
    {
        CalcularGrilla();
    }

    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Session["Detalle"] != null)
        {
            try
            {
                DataTable dtDetalle = new DataTable();
                dtDetalle = (DataTable)Session["Detalle"];
                dtDetalle.Rows.RemoveAt(e.RowIndex);
                Session["Detalle"] = dtDetalle;
                gv.DataSource = dtDetalle;
                gv.DataBind();
                CalcularGrilla();
            }
            catch (Exception)
            {

            }
        }
        else 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Su sesión ha caducado. Vuelva a ingresar al sistema.' });</script>", false);
            BloquearTodo();
        }
    }

    protected void ibTodos_Click(object sender, ImageClickEventArgs e)
    {
        ListarProductos("", "", "", ddlTienda.SelectedValue,rblTipo.SelectedItem.Value);
    }

    protected void MenuFamilia_MenuItemClick(object sender, MenuEventArgs e)
    {
        ListarSubCategorias();
        string Familia = MenuFamilia.SelectedItem.Text;
        string Busqueda = txtBuscar.Text.Trim();
        ListarProductos(Familia, "", Busqueda, ddlTienda.SelectedValue, rblTipo.SelectedItem.Value);
    }

    protected void txtBuscar_TextChanged(object sender, EventArgs e)
    {
        string Busqueda = txtBuscar.Text.Trim();
        ListarProductos("", "", Busqueda, ddlTienda.SelectedValue, rblTipo.SelectedItem.Value);
        txtBuscar.Focus();
    }

    protected void ibCerrarProductos_Click(object sender, ImageClickEventArgs e)
    {
        panelProductos.Visible = false;
        tblGeneral.Visible = true;
        toolbar.Visible = true;
    }

    protected void MenuSubFamilia_MenuItemClick(object sender, MenuEventArgs e)
    {
        string Familia = MenuFamilia.SelectedItem.Text;
        string SubFamilia = MenuSubFamilia.SelectedItem.Text;
        string Busqueda = txtBuscar.Text.Trim();
        ListarProductos(Familia, SubFamilia, Busqueda, ddlTienda.SelectedValue, rblTipo.SelectedItem.Value);
    }

    protected void gvProductos_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (Session["Detalle"] != null)
        {
            if (e.CommandName == "AgregarProducto")
            {
                string n_IdProducto = gvProductos.DataKeys[e.Item.ItemIndex].ToString();
                Label lblDescripcion = (Label)gvProductos.Items[e.Item.ItemIndex].FindControl("lblDescripcion");
                string Descripcion = lblDescripcion.Text;
                Label lblPrecio = (Label)gvProductos.Items[e.Item.ItemIndex].FindControl("lblPrecio2");
                double Precio = double.Parse(lblPrecio.Text);
                Label lblStock = (Label)gvProductos.Items[e.Item.ItemIndex].FindControl("lblStock");
                int Stock = int.Parse(lblStock.Text);
                Label lblCodigo = (Label)gvProductos.Items[e.Item.ItemIndex].FindControl("lblCodigo");
                string Codigo = lblCodigo.Text;

                //Validar que el producto exista
                DataTable dt = new DataTable();
                dt = (DataTable)Session["Detalle"];
                string n_IdProductoTabla = "";
                bool encontrado = false;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    n_IdProductoTabla = dt.Rows[i]["n_IdProducto"].ToString();
                    if (n_IdProducto.Trim() == n_IdProductoTabla.Trim())
                    {
                        encontrado = true;
                        break;
                    }
                }

                if (encontrado == false)
                {

                    DataRow dr;
                    dr = dt.NewRow();

                    dr["i_Cantidad"] = "1";
                    dr["Producto"] = Descripcion;
                    dr["f_PrecioUnitario"] = Precio;
                    dr["f_PrecioTotal"] = Precio;
                    dr["n_IdProducto"] = n_IdProducto;
                    dr["Codigo"] = Codigo;
                    dr["Stock"] = Stock;

                    dt.Rows.Add(dr);
                    Session["Detalle"] = dt;
                    gv.DataSource = dt;
                    gv.DataBind();
                    CalcularGrilla();
                    panelProductos.Visible = false;
                    tblGeneral.Visible = true;
                    toolbar.Visible = true;
                }
                else
                {

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Producto Repetido' });</script>", false);
                }
            }
        }
        else 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Su sesión ha caducado. Vuelva a ingresar al sistema.' });</script>", false);
            BloquearTodo();
        }
    }

    protected void ddlMoneda_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (gv.Rows.Count > 0)
        {
            CalcularGrilla();
        }
    }

    protected void rblTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
    {
        CalcularGrilla();
    }

    protected void txtPago_TextChanged(object sender, EventArgs e)
    {
        double total = double.Parse(lblTotal.Text);
        double pago = double.Parse(txtPago.Text);
        double vuelto = 0;
        if (pago < total) 
        {
            txtPago.Focus();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.error({ message: 'El pago debe ser mayor o igual al total.' });</script>", false);
            return;
        }
        vuelto = pago - total;
        lblVuelto.Text = vuelto.ToString("N2");

    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> BuscarClientes(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select v_Nombre,n_IdCliente from Cliente where v_Nombre+isnull(v_DocumentoIdentidad,'') like '%' + @SearchText + '%' order by v_Nombre";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Connection = conn;
                conn.Open();
                List<string> productos = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        productos.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(sdr["v_Nombre"].ToString(), Convert.ToString(sdr["n_IdCliente"].ToString())));
                    }
                }
                conn.Close();
                //if (productos.Count == 0)
                //{
                    productos.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Crear '" + prefixText + "'", "*"));
                    productos.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Crear y Editar ... '" + prefixText + "'", "%"));
                //}

                
                return productos;
            }
        }
    }

    protected void hdnValue_ValueChanged(object sender, EventArgs e)
    {
        string selectedWidgetID = ((HiddenField)sender).Value;

        if (selectedWidgetID == "*") 
        {
            string n_IdCliente = "";
            int longitud = (txtCliente.Text.Length - 8);
            string cliente = txtCliente.Text.Substring(7, longitud);
            //Crear el cliente
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Play_Cliente_RegistrarRapido";
            cmd.Parameters.AddWithValue("@v_Nombre", cliente.ToUpper());
            conexion.Open();
            n_IdCliente = cmd.ExecuteScalar().ToString();
            conexion.Close();
            cmd.Dispose();

            hdnValue.Value = n_IdCliente;

            txtCliente.Text = cliente;
            btnCliente.Visible = true;
        }
        else if (selectedWidgetID == "%")
        {
            string n_IdCliente = "";
            //Crear y Editar
            int longitud = (txtCliente.Text.Length - 21);
            string cliente = txtCliente.Text.Substring(20, longitud);

            //Crear el cliente
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Play_Cliente_RegistrarRapido";
            cmd.Parameters.AddWithValue("@v_Nombre", cliente.ToUpper());
            conexion.Open();
            n_IdCliente = cmd.ExecuteScalar().ToString();
            conexion.Close();
            cmd.Dispose();

            hdnValue.Value = n_IdCliente;

            tblCliente.Visible = true;
            tblGeneral.Visible = false;
            toolbar.Visible = false;
            txtNombre.Text = cliente;
            txtCliente.Text = cliente;
            btnCliente.Visible = true;
        }
        else 
        {
            //Cliente seleccionado ya existe
            btnCliente.Visible = true;
        }

    }

    protected void btnSalirCliente_Click(object sender, ImageClickEventArgs e)
    {
        tblCliente.Visible = false;
        tblGeneral.Visible = true;
        toolbar.Visible = true;
    }

    protected void btnGuardarCliente_Click(object sender, ImageClickEventArgs e)
    {
        if (txtNombre.Text.Trim() == "") 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar el nombre del cliente.' });</script>", false);
            txtNombre.Focus();
            return;
        }

        try
        {
            if (hdnValue.Value.Trim() == "")
            {
                //Registrar cliente nuevo
                string n_IdCliente = "0";
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
                cmd.Parameters.AddWithValue("@i_Puntos", 0);
                conexion.Open();
                n_IdCliente = cmd.ExecuteScalar().ToString();
                conexion.Close();
                cmd.Dispose();

                hdnValue.Value = n_IdCliente;

                tblCliente.Visible = false;
                tblGeneral.Visible = true;
                toolbar.Visible = true;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Cliente registrado satisfactoriamente' });</script>", false);
            }
            else 
            {
                string n_IdCliente = hdnValue.Value;

                //Actualizar datos del cliente
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Play_Cliente2_Actualizar";
                cmd.Parameters.AddWithValue("@n_IdCliente", n_IdCliente);
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
                tblCliente.Visible = false;
                tblGeneral.Visible = true;
                toolbar.Visible = true;

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Cliente actualizado satisfactoriamente' });</script>", false);
            }

            txtCliente.Text = txtNombre.Text.Trim().ToUpper();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.error({ message: '" + ex.Message + "' });</script>", false);
        }

    }

    protected void btnCliente_Click(object sender, ImageClickEventArgs e)
    {
        if (hdnValue.Value.Trim() == "")
        {

        }
        else 
        {
            //Consultar datos del cliente y mostrarlos
            string n_IdCliente = hdnValue.Value;
            SqlDataAdapter da = new SqlDataAdapter("Play_Cliente2_Seleccionar " + n_IdCliente, conexion);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt != null) 
            {
                if (dt.Rows.Count > 0) 
                {
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
                    if (dt.Rows[0]["v_Distrito"].ToString().Trim() != "")
                    {
                        ddlDistrito.SelectedValue = dt.Rows[0]["v_Distrito"].ToString();
                    }
                    txtComentario.Text = dt.Rows[0]["t_Comentario"].ToString();
                    chkEstado.Checked = bool.Parse(dt.Rows[0]["b_Estado"].ToString());
                    lblPuntos.Text = dt.Rows[0]["i_Puntos"].ToString();
                }
            }

            txtNombre.Focus();
        }
        tblCliente.Visible = true;
        tblGeneral.Visible = false;
        toolbar.Visible = false;
    }

    protected void ddlFormaPago_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFormaPago.SelectedIndex > 0) //Tarjeta
        {
            ddlBanco.Enabled = true;
            ListarBancos();
        }
        else 
        {
            ddlBanco.Enabled = false;
            ddlBanco.SelectedIndex = 0;
        }

    }

    void ListarBancos() 
    {
        SqlDataAdapter da = new SqlDataAdapter("Play_Banco_Listar", conexion);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddlBanco.DataSource = dt;
        ddlBanco.DataTextField = "v_Descripcion";
        ddlBanco.DataValueField = "n_IdBanco";
        ddlBanco.DataBind();
        ddlBanco.SelectedIndex = 0;
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

    protected void btnAnular_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //Validar que el documento no esté anulado


                if (Session["dtUsuario"] != null)
                {
                    SqlTransaction tran;
                    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);
                    cn.Open();
                    tran = cn.BeginTransaction();

                    try
                    {

                        DataTable dtUsuario = new DataTable();
                        dtUsuario = (DataTable)Session["dtUsuario"];
                        string n_IdUsuario = dtUsuario.Rows[0]["n_IdUsuario"].ToString();

                        //Restar Puntos del Cliente
                        if (hdnValue.Value.Trim() != "")
                        {
                            int Puntos = Convert.ToInt32(Math.Floor(Convert.ToDouble(lblTotal.Text)));
                            SqlCommand cmd6 = new SqlCommand();
                            cmd6.Connection = cn;
                            cmd6.Transaction = tran;
                            cmd6.CommandType = CommandType.StoredProcedure;
                            cmd6.CommandText = "POI_Cliente_RestarPuntos";
                            cmd6.Parameters.AddWithValue("@n_IdCliente", hdnValue.Value);
                            cmd6.Parameters.AddWithValue("@Cantidad", Puntos);
                            cmd6.ExecuteNonQuery();
                            cmd6.Dispose();
                        }

                        //Actualizar estado del comprobante y actualizar montos a cero
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = cn;
                        cmd.Transaction = tran;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Play_Comprobante_Anular";
                        cmd.Parameters.AddWithValue("@n_IdPedido", lblIdPedido.Text);
                        cmd.Parameters.AddWithValue("@n_IdUsuarioAnula", n_IdUsuario);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();

                        DataTable dtDetalle = new DataTable();
                       //Validar que exista productos en el detalle del pedido
                        dtDetalle = (DataTable)Session["Detalle"];
                        for (int i = 0; i < dtDetalle.Rows.Count; i++)
                        {
                            //Actualizar Stock
                            SqlCommand cmdStock = new SqlCommand();
                            cmdStock.Connection = cn;
                            cmdStock.Transaction = tran;
                            cmdStock.CommandType = CommandType.StoredProcedure;
                            cmdStock.CommandText = "Play_Stock_Actualizar";
                            cmdStock.Parameters.AddWithValue("@n_IdAlmacen", ddlTienda.SelectedValue);
                            cmdStock.Parameters.AddWithValue("@n_IdProducto", dtDetalle.Rows[i]["n_IdProducto"].ToString());
                            cmdStock.Parameters.AddWithValue("@f_Cantidad", dtDetalle.Rows[i]["i_Cantidad"].ToString());
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
                            cmdKardex.Parameters.AddWithValue("@i_IdMotivoTraslado", 22);//Venta Anulada
                            cmdKardex.Parameters.AddWithValue("@n_IdProducto", dtDetalle.Rows[i]["n_IdProducto"].ToString());
                            cmdKardex.Parameters.AddWithValue("@n_IdAlmacen", ddlTienda.SelectedValue);
                            cmdKardex.Parameters.AddWithValue("@f_Cantidad", dtDetalle.Rows[i]["i_Cantidad"].ToString());
                            cmdKardex.Parameters.AddWithValue("@n_IdTipoDocumento", 1);
                            cmdKardex.Parameters.AddWithValue("@v_NumeroDocumento", lblNumeroPedido.Text.Trim());
                            if (hdnValue.Value.Trim() == "")
                            {
                                cmdKardex.Parameters.AddWithValue("@n_IdCliente", DBNull.Value);
                            }
                            else
                            {
                                cmdKardex.Parameters.AddWithValue("@n_IdCliente", hdnValue.Value);
                            }
                            cmdKardex.Parameters.AddWithValue("@n_IdProveedor", DBNull.Value);
                            cmdKardex.ExecuteNonQuery();
                            cmdKardex.Dispose();
                        }

                        SqlCommand cmdp = new SqlCommand();
                        cmdp.Connection = cn;
                        cmdp.Transaction = tran;
                        cmdp.CommandType = CommandType.StoredProcedure;
                        cmdp.CommandText = "Play_Pedido_Anular";
                        cmdp.Parameters.AddWithValue("@n_IdPedido", lblIdPedido.Text);
                        cmdp.ExecuteNonQuery();
                        cmdp.Dispose();

                        tran.Commit(); 
                        BloquearTodo();
                        btnGuardar.Visible = false;
                        btnAnular.Visible = false;
                        Label1.ForeColor = System.Drawing.Color.Red;
                        lblNumeroPedido.ForeColor = System.Drawing.Color.Red;
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Pedido Anulado Satisfactoriamente' });</script>", false);
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
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Su sesión ha caducado. Vuelva a ingresar al sistema.' });</script>", false);
                    BloquearTodo();
                }

            

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.error({ message: '" + ex.Message + "' });</script>", false);
        }
    }

    protected void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Comprobante.aspx?n_IdPedido=" + lblIdPedido.Text);
    }

    protected void ibEstablecerSucursal_Click(object sender, ImageClickEventArgs e)
    {
        ListarUsuariosVendedores();
        ddlVendedor.Enabled = true;
        ddlTienda.Enabled = false;
        lnkAgregarProducto.Enabled = true;
        ibEstablecerSucursal.Visible = false;
    }

    protected void txtPrecio_TextChanged(object sender, EventArgs e)
    {
        CalcularGrilla();
    }

    protected void txtDescuento_TextChanged(object sender, EventArgs e)
    {
        CalcularGrilla();
    }
}