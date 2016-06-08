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

public partial class CrearOrdenCompra : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            if (Session["Detalle"] != null) { Session.Remove("Detalle"); }
            txtFechaInicial.Text = DateTime.Now.ToShortDateString();
            InicializarGrilla();
            tblProveedor.Visible = false;
            ListarMarcas();
            ddlMarca_SelectedIndexChanged(null, null);
            ListarCategoria();
            ddlCategoria_SelectedIndexChanged(null, null);
            ListarEdad();
            ListarBaterias();
            tblProducto.Visible = false;

            if (Request.QueryString["i_IdOrdenCompra"] != null)
            {
                string i_IdOrdenCompra = Request.QueryString["i_IdOrdenCompra"].ToString();
                //Cabecera de la Orden de Compra
                SqlDataAdapter da = new SqlDataAdapter("Play_OrdenCompra_Seleccionar " + i_IdOrdenCompra, conexion);
                DataTable dt = new DataTable();
                da.Fill(dt);
                lblNumero.Text = dt.Rows[0]["v_NumeroOrdenCompra"].ToString();
                hdnValue.Value = dt.Rows[0]["n_IdProveedor"].ToString();
                txtProveedor.Text = dt.Rows[0]["NombreProveedor"].ToString();
                txtFechaInicial.Text = DateTime.Parse(dt.Rows[0]["d_FechaEmision"].ToString()).ToShortDateString();
                txtReferencia.Text = dt.Rows[0]["v_Referencia"].ToString();
                txtObservacion.Text = dt.Rows[0]["t_Observacion"].ToString();
                lblSubTotal.Text = decimal.Parse(dt.Rows[0]["f_SubTotal"].ToString()).ToString("N2");
                lblIgv.Text = decimal.Parse(dt.Rows[0]["f_IGV"].ToString()).ToString("N2");
                lblTotal.Text = decimal.Parse(dt.Rows[0]["f_Total"].ToString()).ToString("N2");
                lblUsuarioRegistro.Text = dt.Rows[0]["Usuario"].ToString();
                lblFechaRegistro.Text = dt.Rows[0]["d_FechaEmision"].ToString();
                if (dt.Rows[0]["v_RutaFoto"].ToString().Trim() != "")
                {
                    ibUsuarioRegistro.ImageUrl = dt.Rows[0]["v_RutaFoto"].ToString();
                }
                else
                {
                    ibUsuarioRegistro.ImageUrl = "~/images/face.jpg";
                }
                lblEstado.Text = dt.Rows[0]["v_DescripcionEstado"].ToString();
                lbAdjunto.Text = dt.Rows[0]["v_RutaArchivo"].ToString();
                lbAdjunto.OnClientClick = "window.open('./OrdenCompra/" + dt.Rows[0]["v_RutaArchivo"].ToString() + "');";

                //Detalle de la Orden de Compra
                SqlDataAdapter daDetOC = new SqlDataAdapter("Play_OrdenCompraDetalle_Seleccionar " + i_IdOrdenCompra, conexion);
                DataTable dtDetOC = new DataTable();
                daDetOC.Fill(dtDetOC);
                Session["Detalle"] = dtDetOC;
                gv.DataSource = dtDetOC;
                gv.DataBind();

                if (lblEstado.Text.Trim().ToUpper() == "PENDIENTE")
                {
                    btnProveedor.Visible = true;
                }
                else
                {
                    LinkButton lb = (LinkButton)gv.FooterRow.FindControl("lnkAgregarProducto");
                    lb.Visible = false;
                    gv.Columns[5].Visible = false;
                    gv.Enabled = false;
                    btnProveedor.Visible = false;
                    btnGuardar.Visible = false;
                }
            }
            else 
            {
                gv.Columns[2].Visible = false;
            }
        }
    }

    void ListarMarcas()
    {
        DataTable dt = new System.Data.DataTable();
        SqlDataAdapter da = new SqlDataAdapter("Play_Marca_Combo", conexion);
        da.Fill(dt);
        ddlMarca.DataSource = dt;
        ddlMarca.DataTextField = "v_DescripcionMarca";
        ddlMarca.DataValueField = "n_IdMarca";
        ddlMarca.DataBind();
        ddlMarca.Items.Insert(0, "SELECCIONAR");
        ddlMarca.SelectedIndex = 0;
    }

    void ListarCategoria()
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter("Play_Categoria_Combo", conexion);
        da.Fill(dt);
        ddlCategoria.DataSource = dt;
        ddlCategoria.DataTextField = "v_Descripcion";
        ddlCategoria.DataValueField = "n_IdCategoria";
        ddlCategoria.DataBind();
        ddlCategoria.Items.Insert(0, "SELECCIONAR");
        ddlCategoria.SelectedIndex = 0;
    }

    void ListarEdad()
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter("Play_Edad_Combo", conexion);
        da.Fill(dt);
        ddlEdad.DataSource = dt;
        ddlEdad.DataTextField = "v_Descripcion";
        ddlEdad.DataValueField = "n_IdEdad";
        ddlEdad.DataBind();
        ddlEdad.Items.Insert(0, "SELECCIONAR");
        ddlEdad.SelectedIndex = 0;
    }

    void ListarModelos()
    {
        if (ddlMarca.SelectedIndex > 0)
        {
            string n_IdMarca = ddlMarca.SelectedValue.ToString();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Play_Modelo_Combo " + n_IdMarca, conexion);
            da.Fill(dt);
            ddlModelo.DataSource = dt;
            ddlModelo.DataTextField = "v_DescripcionModelo";
            ddlModelo.DataValueField = "n_IdModelo";
            ddlModelo.DataBind();
            ddlModelo.Items.Insert(0, "SELECCIONAR");
            ddlModelo.Enabled = true;
        }
        else
        {
            ddlModelo.SelectedIndex = 0;
            ddlModelo.Enabled = false;
        }
    }

    void ListarSubCategorias()
    {
        if (ddlCategoria.SelectedIndex > 0)
        {
            string n_IdCategoria = ddlCategoria.SelectedValue.ToString();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Play_SubCategoria_Combo " + n_IdCategoria, conexion);
            da.Fill(dt);
            ddlSubCategoria.DataSource = dt;
            ddlSubCategoria.DataTextField = "v_Descripcion";
            ddlSubCategoria.DataValueField = "n_IdSubCategoria";
            ddlSubCategoria.DataBind();
            ddlSubCategoria.Items.Insert(0, "SELECCIONAR");
            ddlSubCategoria.Enabled = true;
        }
        else
        {
            ddlSubCategoria.SelectedIndex = 0;
            ddlSubCategoria.Enabled = false;
        }
    }

    void ListarBaterias()
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter("Play_Pilas_Combo", conexion);
        da.Fill(dt);
        ddlBateria.DataSource = dt;
        ddlBateria.DataTextField = "v_Descripcion";
        ddlBateria.DataValueField = "n_IdPilas";
        ddlBateria.DataBind();
        ddlBateria.Items.Insert(0, "NINGUNO");
        ddlBateria.SelectedIndex = 0;
    }

    void InicializarGrilla()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Cantidad", typeof(int));
        dt.Columns.Add("Producto", typeof(String));
        dt.Columns.Add("CostoUnitario", typeof(Double));
        dt.Columns.Add("CostoTotal", typeof(Double));
        dt.Columns.Add("n_IdProducto");
        dt.Columns.Add("Saldo");

        DataRow dr;
        dr = dt.NewRow();
        dr["n_IdProducto"] = 0;
        dr["Producto"] = "";
        dr["Cantidad"] = "0";
        dr["CostoUnitario"] = "0";
        dr["CostoTotal"] = "0";
        dr["Saldo"] = "0";
        dt.Rows.Add(dr);

        Session["Detalle"] = dt;
        gv.DataSource = dt;
        gv.DataBind();
    }

    protected void btnSalir_Click(object sender, ImageClickEventArgs e)
    {
        Session.Remove("Detalle");
        Response.Redirect("ListarOrdenCompra.aspx");
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        if (hdnValue.Value == "") 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar un proveedor' });</script>", false);
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
                if (Request.QueryString["i_IdOrdenCompra"] == null)
                {
                    //Registrar Cabecera
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.Transaction = tran;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Play_OrdenCompra_Registrar";
                    cmd.Parameters.AddWithValue("@n_IdProveedor", hdnValue.Value);
                    cmd.Parameters.AddWithValue("@n_IdMoneda", 1);
                    cmd.Parameters.AddWithValue("@d_FechaEmision", DateTime.Parse(txtFechaInicial.Text + " " + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00")));
                    cmd.Parameters.AddWithValue("@v_Referencia", txtReferencia.Text.Trim().ToUpper());
                    cmd.Parameters.AddWithValue("@t_Observacion", txtObservacion.Text.Trim());
                    cmd.Parameters.AddWithValue("@f_SubTotal", double.Parse(lblSubTotal.Text));
                    cmd.Parameters.AddWithValue("@f_IGV", double.Parse(lblIgv.Text));
                    cmd.Parameters.AddWithValue("@f_Total", double.Parse(lblTotal.Text));
                    cmd.Parameters.AddWithValue("@n_IdUsuarioCreacion", n_IdUsuario);
                    cmd.Parameters.AddWithValue("@v_RutaArchivo", lbAdjunto.Text);

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
                    lblUsuarioRegistro.Text = dtUsuario.Rows[0]["v_Usuario"].ToString();
                    lblFechaRegistro.Text = DateTime.Now.ToString();
                    ibUsuarioRegistro.ImageUrl = dtUsuario.Rows[0]["v_RutaFoto"].ToString();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Orden de Compra Registrada Satisfactoriamente' });</script>", false);
                }
                else if (Request.QueryString["i_IdOrdenCompra"] != null)
                {
                    string i_IdOrdenCompra = Request.QueryString["i_IdOrdenCompra"];
                    //Actualizar datos
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.Transaction = tran;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Play_OrdenCompra_Actualizar";
                    cmd.Parameters.AddWithValue("@i_IdOrdenCompra", i_IdOrdenCompra);
                    cmd.Parameters.AddWithValue("@n_IdProveedor", hdnValue.Value);
                    cmd.Parameters.AddWithValue("@d_FechaEmision", DateTime.Parse(txtFechaInicial.Text + " " + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00")));
                    cmd.Parameters.AddWithValue("@v_Referencia", txtReferencia.Text.Trim().ToUpper());
                    cmd.Parameters.AddWithValue("@t_Observacion", txtObservacion.Text.Trim());
                    cmd.Parameters.AddWithValue("@f_SubTotal", double.Parse(lblSubTotal.Text));
                    cmd.Parameters.AddWithValue("@f_IGV", double.Parse(lblIgv.Text));
                    cmd.Parameters.AddWithValue("@f_Total", double.Parse(lblTotal.Text));
                    cmd.Parameters.AddWithValue("@v_RutaArchivo", lbAdjunto.Text);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

                    //Eliminar detalle
                    SqlCommand cmdEliminar = new SqlCommand();
                    cmdEliminar.Connection = cn;
                    cmdEliminar.Transaction = tran;
                    cmdEliminar.CommandType = CommandType.StoredProcedure;
                    cmdEliminar.CommandText = "Play_OrdenCompraDetalle_Eliminar";
                    cmdEliminar.Parameters.AddWithValue("@i_IdOrdenCompra", i_IdOrdenCompra);
                    cmdEliminar.ExecuteNonQuery();
                    cmdEliminar.Dispose();

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

                    tran.Commit();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Orden de Compra Actualizada Satisfactoriamente' });</script>", false);
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
            TextBox txtCostoUnidad = (TextBox)gv.Rows[i].FindControl("txtCostoUnidad");

            if (txtCantidad.Text.Trim() == "")
            {
                Cantidad = 1;
                txtCantidad.Text = "1";
            }
            else 
            {
                Cantidad = double.Parse(txtCantidad.Text.Trim());
            }

            if (txtCostoUnidad.Text.Trim() == "")
            {
                CostoUnitario = 1;
                txtCostoUnidad.Text = "1";
            }
            else 
            {
                CostoUnitario = double.Parse(txtCostoUnidad.Text.Trim());
            }

            if (Cantidad <= 0) 
            {
                Cantidad = 1;
            }

            dt.Rows[i]["Cantidad"] = Cantidad;
            dt.Rows[i]["Saldo"] = Cantidad;
            dt.Rows[i]["Producto"] = dt.Rows[i]["Producto"].ToString();
            dt.Rows[i]["CostoUnitario"] = CostoUnitario;

            CostoTotal = Cantidad * CostoUnitario;

            TotalColumna1 = TotalColumna1 + CostoTotal;

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
            gv.FooterRow.Cells[4].Text = TotalColumna1.ToString("n2");
        }
    }

    void BloquearOrdenCompra() 
    {
        txtFechaInicial.Enabled = false;
        txtReferencia.Enabled = false;
        gv.Enabled = false;
        txtObservacion.Enabled = false;
        btnGuardar.Enabled = false;
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> BuscarProveedores(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select v_Nombre,n_IdProveedor from Proveedor where v_Nombre+isnull(v_Ruc,'') like '%' + @SearchText + '%' order by v_Nombre";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Connection = conn;
                conn.Open();
                List<string> productos = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        productos.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(sdr["v_Nombre"].ToString(), Convert.ToString(sdr["n_IdProveedor"].ToString())));
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
                productos.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Crear y Editar ... '" + prefixText + "'", "%"));
                return productos;
            }
        }
    }

    protected void hdnValue_ValueChanged(object sender, EventArgs e)
    {
        string selectedWidgetID = ((HiddenField)sender).Value;

        if (selectedWidgetID == "*")
        {
            string n_IdProveedor = "";
            int longitud = (txtProveedor.Text.Length - 8);
            string proveedor = txtProveedor.Text.Substring(7, longitud);
            //Crear el cliente
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Play_Proveedor_RegistrarRapido";
            cmd.Parameters.AddWithValue("@v_Nombre", proveedor.ToUpper());
            conexion.Open();
            n_IdProveedor = cmd.ExecuteScalar().ToString();
            conexion.Close();
            cmd.Dispose();

            hdnValue.Value = n_IdProveedor;

            txtProveedor.Text = proveedor;
            btnProveedor.Visible = true;
        }
        else if (selectedWidgetID == "%")
        {
            string n_IdProveedor = "";
            //Crear y Editar
            int longitud = (txtProveedor.Text.Length - 21);
            string cliente = txtProveedor.Text.Substring(20, longitud);

            //Crear el cliente
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Play_Proveedor_RegistrarRapido";
            cmd.Parameters.AddWithValue("@v_Nombre", cliente.ToUpper());
            conexion.Open();
            n_IdProveedor = cmd.ExecuteScalar().ToString();
            conexion.Close();
            cmd.Dispose();

            hdnValue.Value = n_IdProveedor;

            tblProveedor.Visible = true;
            tblGeneral.Visible = false;
            toolbar.Visible = false;
            txtNombre.Text = cliente;
            txtProveedor.Text = cliente;
            btnProveedor.Visible = true;
        }
        else
        {
            //Cliente seleccionado ya existe
            btnProveedor.Visible = true;
        }
    }

    protected void btnGuardarProveedor_Click(object sender, ImageClickEventArgs e)
    {
        if (txtNombre.Text == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar el nombre' });</script>", false);
            txtNombre.Focus();
            return;
        }

        try
        {
            if (hdnValue.Value != "")
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Play_Proveedor_Actualizar";
                cmd.Parameters.AddWithValue("@n_IdProveedor", hdnValue.Value);
                cmd.Parameters.AddWithValue("@v_Ruc", txtRuc.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Nombre", txtNombre.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Telefono", txtTelefono.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Direccion", txtDireccion.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Contacto", txtContacto.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Email", txtEmail.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@b_Estado", chkEstado.Checked);
                conexion.Open();
                cmd.ExecuteNonQuery();
                conexion.Close();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Proveedor actualizado.' });</script>", false);
                tblProveedor.Visible = false;
                tblGeneral.Visible = true;
                toolbar.Visible = true;
            }
            else
            {
                string n_IdProveedor = "";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Play_Proveedor_Registrar";
                cmd.Parameters.AddWithValue("@v_Ruc", txtRuc.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Nombre", txtNombre.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Telefono", txtTelefono.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Direccion", txtDireccion.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Contacto", txtContacto.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Email", txtEmail.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@b_Estado", chkEstado.Checked);
                conexion.Open();
                n_IdProveedor = cmd.ExecuteScalar().ToString();
                conexion.Close();
                hdnValue.Value = n_IdProveedor;
                tblProveedor.Visible = false;
                tblGeneral.Visible = true;
                toolbar.Visible = true;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Proveedor registrado.' });</script>", false);
            }
            txtProveedor.Text = txtNombre.Text.Trim();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.error({ message: '" + ex.Message + "' });</script>", false);
        }
    }

    protected void btnProveedor_Click(object sender, ImageClickEventArgs e)
    {
        if (hdnValue.Value.Trim() == "")
        {

        }
        else
        {
            int n_IdProveedor = int.Parse(hdnValue.Value);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Play_Proveedor_Seleccionar " + n_IdProveedor.ToString(), conexion);
            da.Fill(dt);
            hdnValue.Value = n_IdProveedor.ToString();
            txtRuc.Text = dt.Rows[0]["v_Ruc"].ToString();
            txtNombre.Text = dt.Rows[0]["v_Nombre"].ToString();
            txtTelefono.Text = dt.Rows[0]["v_Telefono"].ToString();
            txtDireccion.Text = dt.Rows[0]["v_Direccion"].ToString();
            txtContacto.Text = dt.Rows[0]["v_Contacto"].ToString();
            txtEmail.Text = dt.Rows[0]["v_Email"].ToString();
            chkEstado.Checked = bool.Parse(dt.Rows[0]["b_Estado"].ToString());
        }
        tblProveedor.Visible = true;
        tblGeneral.Visible = false;
        toolbar.Visible = false;
    }

    protected void btnSalirProveedor_Click(object sender, ImageClickEventArgs e)
    {
        toolbar.Visible = true;
        tblGeneral.Visible = true;
        tblProveedor.Visible = false;
    }

    protected void lnkAgregarProducto_Click(object sender, EventArgs e)
    {
        TextBox tPro = new TextBox();
        TextBox tCan = new TextBox();
        TextBox txtCostoUnidad = new TextBox();
        HiddenField hf = new HiddenField();

        DataTable dt = new DataTable();
        dt = (DataTable)Session["Detalle"];

        //Pasar de la grilla a la tabla
        for (int i = 0; i < gv.Rows.Count; i++)
        {
            hf = (HiddenField)gv.Rows[i].FindControl("hfIdProducto");
            tPro = (TextBox)gv.Rows[i].Cells[0].FindControl("txtProducto");
            tCan = (TextBox)gv.Rows[i].Cells[1].FindControl("txtCantidad");
            txtCostoUnidad = (TextBox)gv.Rows[i].Cells[2].FindControl("txtCostoUnidad");

            dt.Rows[i]["n_IdProducto"] = hf.Value;
            dt.Rows[i]["Producto"] = tPro.Text.Trim();
            dt.Rows[i]["Cantidad"] = tCan.Text.Trim();
            dt.Rows[i]["CostoUnitario"] = txtCostoUnidad.Text.Trim();
        }

        //Mostrar los datos y fila nueva
        DataRow dr;
        dr = dt.NewRow();
        dr["n_IdProducto"] = 0;
        dr["Producto"] = "";
        dr["Cantidad"] = "0";
        dr["CostoUnitario"] = "0";
        dr["CostoTotal"] = "0";
        dr["Saldo"] = "0";
        dt.Rows.Add(dr);

        Session["Detalle"] = dt;

        gv.DataSource = dt;
        gv.DataBind();

        //int fila = gv.Rows.Count - 1;
        //TextBox tb = new TextBox();
        //tb = (TextBox)gv.Rows[fila].Cells[1].FindControl("txtProducto");
        //tb.Focus();
    }

    protected void hfIdProducto_ValueChanged(object sender, EventArgs e)
    {
        string IdProducto = ((HiddenField)sender).Value;
        if (IdProducto == "%")
        {
            //Crear producto y editar
            tblProducto.Visible = true;
            tblGeneral.Visible = false;
            toolbar.Visible = false;

            TextBox txtPro = (TextBox)((HiddenField)sender).Parent.FindControl("txtProducto");
            int longitud = (txtPro.Text.Length - 21);
            string DescripcionProducto = txtPro.Text.Substring(20, longitud);
            txtProducto.Text = DescripcionProducto;
            txtProducto.Focus();
        }
        else
        {
             //el producto ya existe
            TextBox txtProducto = (TextBox)((HiddenField)sender).Parent.FindControl("txtProducto");
            TextBox txtCostoUnidad = (TextBox)((HiddenField)sender).Parent.FindControl("txtCostoUnidad");
            DataTable dtCosto = new DataTable();
            SqlDataAdapter daCosto = new SqlDataAdapter("select f_Costo from Producto where n_IdProducto = " + IdProducto, conexion);
            daCosto.Fill(dtCosto);
            txtCostoUnidad.Text = dtCosto.Rows[0]["f_Costo"].ToString();

            TextBox tPro = new TextBox();
            TextBox tCan = new TextBox();
            TextBox tCu = new TextBox();
            HiddenField hf = new HiddenField();

            DataTable dt = new DataTable();
            dt = (DataTable)Session["Detalle"];

            //Pasar de la grilla a la tabla
            for (int i = 0; i < gv.Rows.Count; i++)
            {
                hf = (HiddenField)gv.Rows[i].FindControl("hfIdProducto");
                tPro = (TextBox)gv.Rows[i].Cells[0].FindControl("txtProducto");
                tCan = (TextBox)gv.Rows[i].Cells[1].FindControl("txtCantidad");
                tCu = (TextBox)gv.Rows[i].Cells[2].FindControl("txtCostoUnidad");

                dt.Rows[i]["n_IdProducto"] = hf.Value;
                dt.Rows[i]["Producto"] = tPro.Text.Trim();
                dt.Rows[i]["Cantidad"] = tCan.Text.Trim();
                dt.Rows[i]["CostoUnitario"] = tCu.Text.Trim();
            }
            Session["Detalle"] = dt;

            gv.DataSource = dt;
            gv.DataBind();

            //Producto seleccionado ya existe
            txtProducto.Font.Bold = true;
            txtProducto.Enabled = false;
            ((HiddenField)sender).Value = IdProducto;
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
                txtProducto.Font.Bold = true;
                txtProducto.Enabled = false;
            }
        }
    }

    protected void txtCostoUnidad_TextChanged(object sender, EventArgs e)
    {
        CalcularGrilla();
    }

    protected void btnSubirArchivo_Click(object sender, ImageClickEventArgs e)
    {
        string filename = Path.GetFileName(fu1.FileName);
        string fullpath = Server.MapPath("~/OrdenCompra/") + filename;

        if (File.Exists(fullpath))
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'El nombre del archivo ya existe!' });</script>", false);
        }
        else
        {
            fu1.SaveAs(fullpath);
            lbAdjunto.Text = filename;
            lbAdjunto.OnClientClick = "window.open('OrdenCompra/" + filename + "')";
        }

        if (Request.QueryString["i_IdOrdenCompra"] != null) 
        {
            string i_IdOrdenCompra = Request.QueryString["i_IdOrdenCompra"];
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Play_OrdenCompra_Actualizar_Archivo";
            cmd.Parameters.AddWithValue("@i_IdOrdenCompra", i_IdOrdenCompra);
            cmd.Parameters.AddWithValue("@v_RutaArchivo", lbAdjunto.Text);
            conexion.Open();
            cmd.ExecuteNonQuery();
            conexion.Close();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Archivo adjunto guardado correctamente' });</script>", false);
        }

    }

    protected void ddlMarca_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListarModelos();
    }

    protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListarSubCategorias();
    }

    protected void ibUpload_Click(object sender, ImageClickEventArgs e)
    {
        if (lblCodigo.Text.Trim() == "")
        {
            string filename = Path.GetFileName(fu1.FileName);
            fu1.SaveAs(Server.MapPath("~/temp/") + filename);
            ibImagen.ImageUrl = "~/temp/" + filename;
            lblRuta.Text = "~/temp/" + filename;
            string extension = Path.GetExtension(fu1.FileName);
            lblExtension.Text = extension;
        }
        else
        {
            try
            {
                //obtener extensión del archivo
                string extension = Path.GetExtension(fu1.FileName);
                lblExtension.Text = extension;
                fu1.SaveAs(Server.MapPath("~/Productos/") + lblCodigo.Text.Trim() + extension);
                ibImagen.ImageUrl = "~/Productos/" + lblCodigo.Text.Trim() + extension;
                lblRuta.Text = "~/Productos/" + lblCodigo.Text.Trim() + extension;

                //Crear imagen redimensionada
                string path = HttpContext.Current.Server.MapPath(lblRuta.Text);
                byte[] binaryImage = File.ReadAllBytes(path);
                HandleImageUpload(binaryImage, "~/Productos/Redimensionada/" + lblCodigo.Text.Trim() + lblExtension.Text);

                //Actualizar la ruta en la base de datos
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Play_Producto_RutaImagen_Actualizar";
                cmd.Parameters.AddWithValue("@n_IdProducto", lblCodigo.Text.Trim());
                cmd.Parameters.AddWithValue("@v_RutaImagen", lblRuta.Text.Trim().ToUpper());
                conexion.Open();
                cmd.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.error({ message: '" + ex.Message + "' });</script>", false);
            }
        }
    }

    private MemoryStream BytearrayToStream(byte[] arr)
    {
        return new MemoryStream(arr, 0, arr.Length);
    }

    private void HandleImageUpload(byte[] binaryImage, string NuevoNombre)
    {
        System.Drawing.Image img = RezizeImage(System.Drawing.Image.FromStream(BytearrayToStream(binaryImage)), 100, 100);
        img.Save(Server.MapPath(NuevoNombre), System.Drawing.Imaging.ImageFormat.Jpeg);
    }

    private System.Drawing.Image RezizeImage(System.Drawing.Image img, int maxWidth, int maxHeight)
    {
        if (img.Height < maxHeight && img.Width < maxWidth) return img;
        using (img)
        {
            Double xRatio = (double)img.Width / maxWidth;
            Double yRatio = (double)img.Height / maxHeight;
            Double ratio = Math.Max(xRatio, yRatio);
            int nnx = (int)Math.Floor(img.Width / ratio);
            int nny = (int)Math.Floor(img.Height / ratio);
            Bitmap cpy = new Bitmap(nnx, nny, PixelFormat.Format32bppArgb);
            using (Graphics gr = Graphics.FromImage(cpy))
            {
                gr.Clear(Color.Transparent);

                // This is said to give best quality when resizing images
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;

                gr.DrawImage(img,
                    new Rectangle(0, 0, nnx, nny),
                    new Rectangle(0, 0, img.Width, img.Height),
                    GraphicsUnit.Pixel);
            }
            return cpy;
        }

    }

    protected void btnGuardarProducto_Click(object sender, ImageClickEventArgs e)
    {
        if (txtProducto.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar la descripción' });</script>", false);
            txtProducto.Focus();
            return;
        }
        if (txtCodigoInterno.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar el código interno' });</script>", false);
            txtCodigoInterno.Focus();
            return;
        }
        if (txtPrecio.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar el precio' });</script>", false);
            txtPrecio.Focus();
            return;
        }
        if (txtCosto.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar el costo' });</script>", false);
            txtCosto.Focus();
            return;
        }
        if (txtStockMinimo.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar el stock mínimo' });</script>", false);
            txtStockMinimo.Focus();
            return;
        }
        if (int.Parse(txtStockMinimo.Text) == 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar un stock mínimo mayor a cero' });</script>", false);
            txtStockMinimo.Focus();
            return;
        }



        GuardarProducto();



        toolbar.Visible = true;
        tblGeneral.Visible = true;
        tblProducto.Visible = false;

    }

    protected void btnSalirProducto_Click(object sender, ImageClickEventArgs e)
    {
        toolbar.Visible = true;
        tblGeneral.Visible = true;
        tblProducto.Visible = false;
    }

    protected void ddlBateria_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBateria.SelectedIndex == 0)
        {
            txtCantidadBaterias.Text = "0";
            txtCantidadBaterias.Enabled = false;
        }
        else
        {
            txtCantidadBaterias.Text = "0";
            txtCantidadBaterias.Enabled = true;
            txtCantidadBaterias.Focus();
        }
    }

    void GuardarProducto() 
    {
        string resultado = "";
        //Validar que el no exista el producto con el mismo nombre
        SqlDataAdapter daProducto = new SqlDataAdapter("select count(1) from producto where v_Descripcion = '" + txtProducto.Text.Trim() + "'", conexion);
        DataTable dtProducto = new DataTable();
        daProducto.Fill(dtProducto);
        int cantidad = int.Parse(dtProducto.Rows[0][0].ToString());
        if (cantidad > 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'El nombre del producto ya existe!' });</script>", false);
            return;
        }
        //Validar que el código interno no exista
        SqlDataAdapter daCodigo = new SqlDataAdapter("select count(1) from Producto where v_CodigoInterno = '" + txtCodigoInterno.Text.Trim() + "'", conexion);
        DataTable dtCodigo = new DataTable();
        daCodigo.Fill(dtCodigo);
        int cantidad2 = int.Parse(dtCodigo.Rows[0][0].ToString());
        if (cantidad2 > 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'El código interno ya existe!' });</script>", false);
            return;
        }

        try
        {
            //Registrar Producto
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Play_Producto_Insertar";
            cmd.Parameters.AddWithValue("@v_Descripcion", txtProducto.Text.Trim().ToUpper());
            cmd.Parameters.AddWithValue("@v_Presentacion", txtPresentacion.Text.Trim().ToUpper());
            if (ddlEdad.SelectedIndex == 0) { cmd.Parameters.AddWithValue("@n_IdEdad", DBNull.Value); } else { cmd.Parameters.AddWithValue("@n_IdEdad", ddlEdad.SelectedValue.ToString()); }
            cmd.Parameters.AddWithValue("@c_Sexo", rblSexo.SelectedValue);
            cmd.Parameters.AddWithValue("@v_RutaImagen", lblRuta.Text.Trim().ToUpper());
            cmd.Parameters.AddWithValue("@f_Precio", txtPrecio.Text);
            cmd.Parameters.AddWithValue("@f_Costo", txtCosto.Text);
            cmd.Parameters.AddWithValue("@f_StockMinimo", int.Parse(txtStockMinimo.Text));

            if (hdnValue.Value == "") { cmd.Parameters.AddWithValue("@n_IdProveedor", DBNull.Value); }
            else { cmd.Parameters.AddWithValue("@n_IdProveedor", hdnValue.Value); }

            if (ddlMarca.SelectedIndex == 0) { cmd.Parameters.AddWithValue("@n_IdMarca", DBNull.Value); } else { cmd.Parameters.AddWithValue("@n_IdMarca", ddlMarca.SelectedValue.ToString()); }
            if (ddlModelo.SelectedIndex == 0) { cmd.Parameters.AddWithValue("@n_IdModelo", DBNull.Value); } else { cmd.Parameters.AddWithValue("@n_IdModelo", ddlModelo.SelectedValue.ToString()); }
            if (ddlCategoria.SelectedIndex == 0) { cmd.Parameters.AddWithValue("@n_IdCategoria", DBNull.Value); } else { cmd.Parameters.AddWithValue("@n_IdCategoria", ddlCategoria.SelectedValue.ToString()); }
            if (ddlSubCategoria.SelectedIndex == 0) { cmd.Parameters.AddWithValue("@n_IdSubCategoria", DBNull.Value); } else { cmd.Parameters.AddWithValue("@n_IdSubCategoria", ddlSubCategoria.SelectedValue.ToString()); }

            cmd.Parameters.AddWithValue("@b_Estado", 1);
            if (ddlBateria.SelectedIndex == 0) { cmd.Parameters.AddWithValue("@n_IdPilas", DBNull.Value); } else { cmd.Parameters.AddWithValue("@n_IdPilas", ddlBateria.SelectedValue); }
            cmd.Parameters.AddWithValue("@i_CantidadPilas", txtCantidadBaterias.Text);
            cmd.Parameters.AddWithValue("@v_CodigoInterno", txtCodigoInterno.Text);
            conexion.Open();
            resultado = cmd.ExecuteScalar().ToString();
            conexion.Close();
            lblCodigo.Text = resultado;


            //Guardar imagen con id del producto
            if (lblRuta.Text.Trim() != "")
            {
                string fullPath = Request.MapPath(lblRuta.Text.Trim());
                string fullPathDestino = Request.MapPath("~/Productos/");

                File.Copy(fullPath, fullPathDestino + lblCodigo.Text + lblExtension.Text.Trim(), true);
                //Limpiar carpeta temp
                File.Delete(fullPath);

                string nuevaRuta = "~/Productos/" + lblCodigo.Text.Trim() + lblExtension.Text;
                lblRuta.Text = nuevaRuta;

                ibImagen.ImageUrl = lblRuta.Text;

                //Crear imagen redimensionada
                string path = HttpContext.Current.Server.MapPath(lblRuta.Text);
                byte[] binaryImage = File.ReadAllBytes(path);
                HandleImageUpload(binaryImage, "~/Productos/Redimensionada/" + lblCodigo.Text.Trim() + lblExtension.Text);

                //Actualizar la ruta en la base de datos
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = conexion;
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.CommandText = "Play_Producto_RutaImagen_Actualizar";
                cmd2.Parameters.AddWithValue("@n_IdProducto", resultado);
                cmd2.Parameters.AddWithValue("@v_RutaImagen", nuevaRuta);
                conexion.Open();
                cmd2.ExecuteNonQuery();
                conexion.Close();
            }
            //Mandarlo a la grilla
            DataTable dt = new DataTable();
            dt = (DataTable)Session["Detalle"];
            int fila = dt.Rows.Count - 1;
            dt.Rows[fila]["n_IdProducto"] = resultado;
            dt.Rows[fila]["Producto"] = txtProducto.Text.Trim().ToUpper();
            dt.Rows[fila]["Cantidad"] = "1";
            dt.Rows[fila]["CostoUnitario"] = txtCosto.Text;
            Session["Detalle"] = dt;

            gv.DataSource = dt;
            gv.DataBind();
            CalcularGrilla();

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Producto registrado.' });</script>", false);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.error({ message: '" + ex.Message + "' });</script>", false);
        }



    }
}