using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class CrearOrdenCompra : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            if (Session["Detalle"] != null) { Session.Remove("Detalle"); }
            txtFechaInicial.Text = DateTime.Now.ToShortDateString();
            ListarAlmacen();
            ListarProveedor();
            ListarMoneda();
            ListarCategoria();
            InicializarGrilla();
            ListarProductos("","","");
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
            BloquearOrdenCompra();
        }
    }

    public void ListarProveedor()
    {
        DataTable dt = new System.Data.DataTable();
        SqlDataAdapter da = new SqlDataAdapter("Play_Proveedor_Combo", conexion);
        da.Fill(dt);
        ddlProveedor.DataSource = dt;
        ddlProveedor.DataTextField = "v_Nombre";
        ddlProveedor.DataValueField = "n_IdProveedor";
        ddlProveedor.DataBind();
        ddlProveedor.Items.Insert(0, "SELECCIONAR");
        ddlProveedor.SelectedIndex = 0;
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

    void ListarProductos(string Familia, string SubFamilia, string Busqueda)
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter("Play_Producto_Listar_Imagenes '" + Familia + "','" + SubFamilia + "','" + Busqueda + "'", conexion);
        da.Fill(dt);
        gvProductos.DataSource = dt;
        gvProductos.DataBind();
    }

    void InicializarGrilla()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Cantidad", typeof(int));
        dt.Columns.Add("Producto", typeof(String));
        dt.Columns.Add("CostoTotal", typeof(Double));
        dt.Columns.Add("CostoCambio", typeof(Double));
        dt.Columns.Add("CostoCambioPlay", typeof(Double));
        dt.Columns.Add("CostoUnitario", typeof(Double));
        dt.Columns.Add("Utilidad", typeof(Double));
        dt.Columns.Add("Precio", typeof(Double));
        dt.Columns.Add("n_IdProducto");

        Session["Detalle"] = dt;
        gv.DataSource = dt;
        gv.DataBind();
    }

    protected void btnSalir_Click(object sender, ImageClickEventArgs e)
    {
        Session.Remove("Detalle");
        Response.Redirect("ListarOrdenCompra.aspx");
    }

    protected void lnkAgregarProducto_Click(object sender, EventArgs e)
    {
        panelProductos.Visible = true;
        tblGeneral.Visible = false;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlProveedor.SelectedIndex == 0) 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe seleccionar un proveedor' });</script>", false);
            ddlProveedor.Focus();
            return;
        }

        if (Session["dtUsuario"] != null)
        {
            DataTable dtUsuario = new DataTable();
            dtUsuario = (DataTable)Session["dtUsuario"];
            string n_IdUsuario = dtUsuario.Rows[0]["n_IdUsuario"].ToString();
            double f_TC = double.Parse(dtUsuario.Rows[0]["f_TC"].ToString());

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
                cmd.CommandText = "Play_OrdenCompra_Registrar";
                cmd.Parameters.AddWithValue("@n_IdAlmacen", ddlAlmacen.SelectedValue);
                cmd.Parameters.AddWithValue("@n_IdMotivoTraslado", "3");
                cmd.Parameters.AddWithValue("@n_IdProveedor", ddlProveedor.SelectedValue);
                cmd.Parameters.AddWithValue("@n_IdMoneda", ddlMoneda.SelectedValue);
                cmd.Parameters.AddWithValue("@d_FechaEmision", DateTime.Parse(txtFechaInicial.Text + " " + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00")));
                cmd.Parameters.AddWithValue("@v_Referencia", txtReferencia.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@t_Observacion", txtObservacion.Text.Trim());
                cmd.Parameters.AddWithValue("@f_TipoCambio", f_TC);
                cmd.Parameters.AddWithValue("@f_SubTotal", double.Parse(lblSubTotal.Text));
                cmd.Parameters.AddWithValue("@f_IGV", double.Parse(lblIgv.Text));
                cmd.Parameters.AddWithValue("@f_Total", double.Parse(lblTotal.Text));
                cmd.Parameters.AddWithValue("@n_IdUsuarioCreacion", n_IdUsuario);
                
                string i_IdOrdenCompra = cmd.ExecuteScalar().ToString();
                cmd.Dispose();

                if (i_IdOrdenCompra.Trim() == "0")
                {
                    tran.Rollback();
                    cn.Close();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'El correlativo de la Orden de Compra ha terminado' });</script>", false);
                    return;
                }

                SqlCommand cmd0 = new SqlCommand();
                cmd0.Connection = cn;
                cmd0.Transaction = tran;
                cmd0.CommandType = CommandType.Text;
                cmd0.CommandText = "select v_NumeroOrdenCompra from OrdenCompra where i_IdOrdenCompra = " + i_IdOrdenCompra;
                lblNumero.Text = cmd0.ExecuteScalar().ToString();
                cmd0.Dispose();

                DataTable dtDetalle = new DataTable();
                dtDetalle = (DataTable)Session["Detalle"];

                for (int i = 0; i < dtDetalle.Rows.Count; i++)
                {
                    //Registrar Detalle
                    SqlCommand cmdDetalle = new SqlCommand();
                    cmdDetalle.Connection = cn;
                    cmdDetalle.Transaction = tran;
                    cmdDetalle.CommandType = CommandType.StoredProcedure;
                    cmdDetalle.CommandText = "Play_OrdenCompraDetalle_Insertar";
                    cmdDetalle.Parameters.AddWithValue("@i_IdOrdenCompra", i_IdOrdenCompra);
                    cmdDetalle.Parameters.AddWithValue("@n_IdProducto", dtDetalle.Rows[i]["n_IdProducto"].ToString());
                    cmdDetalle.Parameters.AddWithValue("@i_Cantidad", dtDetalle.Rows[i]["Cantidad"].ToString());
                    cmdDetalle.Parameters.AddWithValue("@f_CostoTotal", dtDetalle.Rows[i]["CostoTotal"].ToString());
                    cmdDetalle.Parameters.AddWithValue("@f_CostoTotalCambioDia", dtDetalle.Rows[i]["CostoCambio"].ToString());
                    cmdDetalle.Parameters.AddWithValue("@f_CostoTotalCambioPlay", dtDetalle.Rows[i]["CostoCambioPlay"].ToString());
                    cmdDetalle.Parameters.AddWithValue("@f_CostoUnidad", dtDetalle.Rows[i]["CostoUnitario"].ToString());
                    cmdDetalle.Parameters.AddWithValue("@i_Utilidad", dtDetalle.Rows[i]["Utilidad"].ToString());
                    cmdDetalle.Parameters.AddWithValue("@f_PrecioVenta", dtDetalle.Rows[i]["Precio"].ToString());
                    cmdDetalle.ExecuteNonQuery();
                    cmdDetalle.Dispose();

                    //Actualizar Stock
                    SqlCommand cmdStock = new SqlCommand();
                    cmdStock.Connection = cn;
                    cmdStock.Transaction = tran;
                    cmdStock.CommandType = CommandType.StoredProcedure;
                    cmdStock.CommandText = "Play_Stock_Actualizar";
                    cmdStock.Parameters.AddWithValue("@n_IdAlmacen", ddlAlmacen.SelectedValue);
                    cmdStock.Parameters.AddWithValue("@n_IdProducto", dtDetalle.Rows[i]["n_IdProducto"].ToString());
                    cmdStock.Parameters.AddWithValue("@f_Cantidad", dtDetalle.Rows[i]["Cantidad"].ToString());
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
                    cmdKardex.Parameters.AddWithValue("@i_IdMotivoTraslado", 3);
                    cmdKardex.Parameters.AddWithValue("@n_IdProducto", dtDetalle.Rows[i]["n_IdProducto"].ToString());
                    cmdKardex.Parameters.AddWithValue("@n_IdAlmacen", ddlAlmacen.SelectedValue);
                    cmdKardex.Parameters.AddWithValue("@f_Cantidad", dtDetalle.Rows[i]["Cantidad"].ToString());
                    cmdKardex.Parameters.AddWithValue("@n_IdTipoDocumento", 10);
                    cmdKardex.Parameters.AddWithValue("@v_NumeroDocumento", lblNumero.Text.Trim());
                    cmdKardex.Parameters.AddWithValue("@n_IdCliente", DBNull.Value);
                    cmdKardex.Parameters.AddWithValue("@n_IdProveedor", ddlProveedor.SelectedValue);
                    cmdKardex.ExecuteNonQuery();
                    cmdKardex.Dispose();

                    //Actualizar Costo y Precio del Producto
                    SqlCommand cmdCosto = new SqlCommand();
                    cmdCosto.Connection = cn;
                    cmdCosto.Transaction = tran;
                    cmdCosto.CommandType = CommandType.StoredProcedure;
                    cmdCosto.CommandText = "Play_Producto_Actualiza_Costo_Precio";
                    cmdCosto.Parameters.AddWithValue("@n_IdProducto", dtDetalle.Rows[i]["n_IdProducto"].ToString());
                    cmdCosto.Parameters.AddWithValue("@f_Costo", dtDetalle.Rows[i]["CostoUnitario"].ToString());
                    cmdCosto.Parameters.AddWithValue("@f_Precio", dtDetalle.Rows[i]["Precio"].ToString());
                    cmdCosto.ExecuteNonQuery();
                    cmdCosto.Dispose();
                }

                //Actualizar Correlativo Orden Compra
                SqlCommand cmd5 = new SqlCommand();
                cmd5.Connection = cn;
                cmd5.Transaction = tran;
                cmd5.CommandType = CommandType.StoredProcedure;
                cmd5.CommandText = "Play_Correlativo_Aumentar";
                cmd5.Parameters.AddWithValue("@n_IdTipoDocumento", 10);
                cmd5.Parameters.AddWithValue("@n_IdAlmacen", ddlAlmacen.SelectedValue);
                cmd5.ExecuteNonQuery();
                cmd5.Dispose();


                tran.Commit();
                BloquearOrdenCompra();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Orden de Compra Registrada Satisfactoriamente' });</script>", false);
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
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("CrearOrdenCompra.aspx");
    }

    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
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

    protected void MenuFamilia_MenuItemClick(object sender, MenuEventArgs e)
    {
        ListarSubCategorias();
        string Familia = MenuFamilia.SelectedItem.Text;
        string Busqueda = txtBuscar.Text.Trim();
        ListarProductos(Familia, "", Busqueda);
    }

    protected void ibTodos_Click(object sender, ImageClickEventArgs e)
    {
        ListarProductos("", "", "");
    }

    protected void MenuSubFamilia_MenuItemClick(object sender, MenuEventArgs e)
    {
        string Familia = MenuFamilia.SelectedItem.Text;
        string SubFamilia = MenuSubFamilia.SelectedItem.Text;
        string Busqueda = txtBuscar.Text.Trim();
        ListarProductos(Familia, SubFamilia, Busqueda);
    }

    protected void txtBuscar_TextChanged(object sender, EventArgs e)
    {
        string Busqueda = txtBuscar.Text.Trim();
        ListarProductos("", "", Busqueda);
        txtBuscar.Focus();
    }

    protected void gvProductos_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "AgregarProducto")
        {
            string n_IdProducto = gvProductos.DataKeys[e.Item.ItemIndex].ToString();
            Label lblDescripcion = (Label)gvProductos.Items[e.Item.ItemIndex].FindControl("lblDescripcion");
            string Descripcion = lblDescripcion.Text;
            Label lblPrecio = (Label)gvProductos.Items[e.Item.ItemIndex].FindControl("lblPrecio2");
            double Precio = double.Parse(lblPrecio.Text);

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

                dr["Cantidad"] = "1";
                dr["Producto"] = Descripcion;
                dr["CostoTotal"] = Precio;
                dr["CostoCambio"] = "0.00";
                dr["CostoCambioPlay"] = "0.00";
                dr["CostoUnitario"] = "0.00";
                dr["Utilidad"] = "100";
                dr["Precio"] = "0.00";
                dr["n_IdProducto"] = n_IdProducto;

                dt.Rows.Add(dr);
                Session["Detalle"] = dt;
                gv.DataSource = dt;
                gv.DataBind();
                CalcularGrilla();
                panelProductos.Visible = false;
                tblGeneral.Visible = true;
            }
            else 
            {

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Producto Repetido' });</script>", false);
            }

        }
    }

    protected void ddlMoneda_SelectedIndexChanged(object sender, EventArgs e)
    {
        string n_IdMoneda = ddlMoneda.SelectedValue;
        SqlDataAdapter da = new SqlDataAdapter("Play_Moneda_Seleccionar " + n_IdMoneda, conexion);
        DataTable dt = new DataTable();
        da.Fill(dt);
        string v_Simbolo = dt.Rows[0]["v_Simbolo"].ToString();
        gv.Columns[2].HeaderText = " Costo Total " + v_Simbolo;
        lblSigno1.Text = v_Simbolo;
        lblSigno2.Text = v_Simbolo;
        lblSigno3.Text = v_Simbolo;

        DataTable dtDetalle = new DataTable();
        dtDetalle = (DataTable)Session["Detalle"];

        CalcularGrilla();
    }

    protected void txtCosto_TextChanged(object sender, EventArgs e)
    {
        CalcularGrilla();
    }

    protected void txtCantidad_TextChanged(object sender, EventArgs e)
    {
        CalcularGrilla();
    }

    void CalcularGrilla() 
    {
        double Cantidad = 0;
        double Costo = 0;
        int Utilidad = 0;
        double f_TC = 1;
        double f_TCPlay = 1;
        double CostoCambioSoles = 0;
        double CostoCambioPlaySoles = 0;
        double CostoUnitario = 0;
        double Precio = 0;
        double TotalColumna1 = 0;
        double TotalColumna2 = 0;

        double SubTotal = 0;
        double Igv = 0;
        double Total = 0;
        double decimales = 0;
        double NuevoPrecio = 0;

        DataTable dt = new DataTable();
        dt = (DataTable)Session["Detalle"];

        for (int i = 0; i < gv.Rows.Count; i++)
        {
            TextBox txtCantidad = (TextBox)gv.Rows[i].FindControl("txtCantidad");
            TextBox txtCosto = (TextBox)gv.Rows[i].FindControl("txtCosto");
            TextBox txtUtilidad = (TextBox)gv.Rows[i].FindControl("txtUtilidad");

            if (txtCantidad.Text.Trim() == "")
            {
                Cantidad = 1;
                txtCantidad.Text = "1";
            }
            else 
            {
                Cantidad = double.Parse(txtCantidad.Text.Trim());
            }

            if (txtCosto.Text.Trim() == "")
            {
                Costo = 1;
                txtCosto.Text = "1";
            }
            else 
            {
                Costo = double.Parse(txtCosto.Text.Trim());
            }

            if (txtUtilidad.Text.Trim() == "")
            {
                Utilidad = 100;
                txtUtilidad.Text = "100";
            }
            else 
            {
                Utilidad = int.Parse(txtUtilidad.Text.Trim());
            }
            

            if (Cantidad <= 0) 
            {
                Cantidad = 1;
            }
            if (Costo <= 0) 
            {
                Costo = 1;
            }
            if (Utilidad <= 0) 
            {
                Utilidad = 1;
            }
            if (Utilidad > 100) 
            {
                Utilidad = 100;
            }

            f_TC = 1;
            f_TCPlay = 1;
            CostoCambioSoles = 0;
            CostoCambioPlaySoles = 0;

            if (ddlMoneda.SelectedItem.Text.Trim() == "DOLARES")
            {
                int anio = DateTime.Now.Year;
                int mes = DateTime.Now.Month;
                int dia = DateTime.Now.Day;
                bool existe = false;
                SqlDataAdapter daTC = new SqlDataAdapter("Play_TC_Existencia " + anio + "," + mes + "," + dia,conexion);
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
                    f_TCPlay = double.Parse(dtTC.Rows[0]["f_TCPlay"].ToString());
                }
                else 
                {
                    BloquearOrdenCompra();
                    ddlMoneda.SelectedValue = "1";
                    ddlMoneda_SelectedIndexChanged(null, null);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.error({ message: 'Debe ingresar el tipo de cambio para el día de hoy' });</script>", false);
                    return;
                }
            }


            dt.Rows[i]["Cantidad"] = Cantidad;
            dt.Rows[i]["Producto"] = dt.Rows[i]["Producto"].ToString();
            dt.Rows[i]["CostoTotal"] = Costo;
            TotalColumna1 = TotalColumna1 + Costo;

            //CostoCambio
            if (ddlMoneda.SelectedItem.Text.Trim() == "SOLES")
            {
                dt.Rows[i]["CostoCambio"] = Costo;
                CostoCambioSoles = Costo;
            }
            else 
            {
                //dolares
                CostoCambioSoles = Costo * f_TC;
                dt.Rows[i]["CostoCambio"] = CostoCambioSoles;
            }
            TotalColumna2 = TotalColumna2 + CostoCambioSoles;

            //CostoPlay
            if (ddlMoneda.SelectedItem.Text.Trim() == "SOLES")
            {
                dt.Rows[i]["CostoCambioPlay"] = Costo;
                CostoCambioPlaySoles = Costo;
            }
            else 
            {
                //dolares
                CostoCambioPlaySoles = Costo * f_TCPlay;
                dt.Rows[i]["CostoCambioPlay"] = CostoCambioPlaySoles;
            }

            CostoUnitario = (CostoCambioPlaySoles / Cantidad);

            dt.Rows[i]["CostoUnitario"] = CostoUnitario;

            dt.Rows[i]["Utilidad"] = Utilidad;

            Precio = ((CostoUnitario * Utilidad / 100) + CostoUnitario);

            //Regla sobre el Precio
            decimales = Precio - Math.Floor(Precio);
            if (decimales >= 0.30)
            {
                NuevoPrecio = Math.Floor(Precio) + 0.90;
            }
            else 
            {
                NuevoPrecio = Math.Floor(Precio) - 1 + 0.90;
            }

            dt.Rows[i]["Precio"] = NuevoPrecio;

            dt.Rows[i]["n_IdProducto"] = dt.Rows[i]["n_IdProducto"].ToString();

            SubTotal = SubTotal + Costo;
        }

        Igv = SubTotal * 18 / 100;
        Total = SubTotal + Igv;

        lblSubTotal.Text = SubTotal.ToString("N2");
        lblIgv.Text = Igv.ToString("N2");
        lblTotal.Text = Total.ToString("N2");

        Session["Detalle"] = dt;

        gv.DataSource = dt;
        gv.DataBind();

        if (gv.Rows.Count > 0)
        {
            gv.FooterRow.Cells[2].Text = TotalColumna1.ToString("n2");
            gv.FooterRow.Cells[3].Text = TotalColumna2.ToString("n2");
        }
    }

    void BloquearOrdenCompra() 
    {
        ddlAlmacen.Enabled = false;
        ddlProveedor.Enabled = false;
        ddlMoneda.Enabled = false;
        txtFechaInicial.Enabled = false;
        txtReferencia.Enabled = false;
        lnkAgregarProducto.Enabled = false;
        gv.Enabled = false;
        txtObservacion.Enabled = false;
        btnGuardar.Enabled = false;
    }

    protected void txtUtilidad_TextChanged(object sender, EventArgs e)
    {
        CalcularGrilla();
    }

    protected void ibCerrarProductos_Click(object sender, ImageClickEventArgs e)
    {
        panelProductos.Visible = false;
        tblGeneral.Visible = true;
    }
}