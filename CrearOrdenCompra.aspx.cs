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
            ListarProveedor();
            ListarCategoria();
            InicializarGrilla();
            ListarProductos("","","");
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
        dt.Columns.Add("CostoUnitario", typeof(Double));
        dt.Columns.Add("CostoTotal", typeof(Double));
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
                cmd.Parameters.AddWithValue("@n_IdProveedor", ddlProveedor.SelectedValue);
                cmd.Parameters.AddWithValue("@n_IdMoneda", 1);
                cmd.Parameters.AddWithValue("@d_FechaEmision", DateTime.Parse(txtFechaInicial.Text + " " + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00")));
                cmd.Parameters.AddWithValue("@v_Referencia", txtReferencia.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@t_Observacion", txtObservacion.Text.Trim());
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
                    cmdDetalle.Parameters.AddWithValue("@f_CostoUnidad", dtDetalle.Rows[i]["CostoUnitario"].ToString());
                    cmdDetalle.Parameters.AddWithValue("@f_CostoTotal", dtDetalle.Rows[i]["CostoTotal"].ToString());
                    cmdDetalle.ExecuteNonQuery();
                    cmdDetalle.Dispose();
                }

                //Actualizar Correlativo Orden Compra
                SqlCommand cmd5 = new SqlCommand();
                cmd5.Connection = cn;
                cmd5.Transaction = tran;
                cmd5.CommandType = CommandType.StoredProcedure;
                cmd5.CommandText = "Play_Correlativo_Aumentar_SinAlmacen";
                cmd5.Parameters.AddWithValue("@n_IdTipoDocumento", 10);
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
                dr["CostoUnitario"] = Precio;
                dr["CostoTotal"] = Precio;
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
        double CostoTotal = 0;
        double CostoUnitario = 0;
        double TotalColumna1 = 0;

        double SubTotal = 0;
        double Igv = 0;
        double Total = 0;

        DataTable dt = new DataTable();
        dt = (DataTable)Session["Detalle"];

        for (int i = 0; i < gv.Rows.Count; i++)
        {
            TextBox txtCantidad = (TextBox)gv.Rows[i].FindControl("txtCantidad");

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

            dt.Rows[i]["Cantidad"] = Cantidad;
            dt.Rows[i]["Producto"] = dt.Rows[i]["Producto"].ToString();

            CostoUnitario = double.Parse(dt.Rows[i]["CostoUnitario"].ToString());
            CostoTotal = Cantidad * CostoUnitario;

            TotalColumna1 = TotalColumna1 + CostoTotal;

            dt.Rows[i]["CostoUnitario"] = CostoUnitario;
            dt.Rows[i]["CostoTotal"] = CostoTotal;

            dt.Rows[i]["n_IdProducto"] = dt.Rows[i]["n_IdProducto"].ToString();

            SubTotal = SubTotal + CostoTotal;
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
            gv.FooterRow.Cells[3].Text = TotalColumna1.ToString("n2");
        }
    }

    void BloquearOrdenCompra() 
    {
        ddlProveedor.Enabled = false;
        txtFechaInicial.Enabled = false;
        txtReferencia.Enabled = false;
        lnkAgregarProducto.Enabled = false;
        gv.Enabled = false;
        txtObservacion.Enabled = false;
        btnGuardar.Enabled = false;
    }

    protected void ibCerrarProductos_Click(object sender, ImageClickEventArgs e)
    {
        panelProductos.Visible = false;
        tblGeneral.Visible = true;
    }

}