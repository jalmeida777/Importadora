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

public partial class CrearNotaIngreso : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            txtFechaInicial.Text = DateTime.Now.ToShortDateString();
            ListarMarcas();
            ddlModelo.Items.Insert(0, "SELECCIONAR");
            ListarCategoria();
            ddlSubCategoria.Items.Insert(0, "SELECCIONAR");
            ListarProveedor();
            ListarEdad();
            ListarBaterias();
            ListarAlmacen();
            ListarMotivo();
            InicializarGrilla();
            lblEstado.Text = "Activo";
            lblEstado.ForeColor = System.Drawing.Color.Green;


            if (Request.QueryString["n_IdNotaIngreso"] != null)
            {
                string n_IdNotaIngreso = Request.QueryString["n_IdNotaIngreso"];
                SqlDataAdapter da = new SqlDataAdapter("Play_NotaIngreso_Seleccionar " + n_IdNotaIngreso, conexion);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ddlAlmacen.SelectedValue = dt.Rows[0]["n_IdAlmacen"].ToString();
                ddlTipoMovimiento.SelectedValue = dt.Rows[0]["n_IdMotivoTraslado"].ToString();
                txtFechaInicial.Text = DateTime.Parse(dt.Rows[0]["d_FechaEmision"].ToString()).ToShortDateString();
                txtReferencia.Text = dt.Rows[0]["v_Referencia"].ToString();
                txtObservacion.Text = dt.Rows[0]["t_Observacion"].ToString();
                lblNumero.Text = dt.Rows[0]["v_NumeroNotaIngreso"].ToString();
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

                SqlDataAdapter daDetalle = new SqlDataAdapter("Play_NotaIngresoDetalle_Seleccionar " + n_IdNotaIngreso, conexion);
                DataTable dtDetalle = new DataTable();
                daDetalle.Fill(dtDetalle);
                gv.DataSource = dtDetalle;
                gv.DataBind();
                BloquearNotaIngreso();
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

    void ListarProveedor()
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

    void BloquearNotaIngreso() 
    {
        btnGuardar.Enabled = false;
        ddlAlmacen.Enabled = false;
        txtFechaInicial.Enabled = false;
        ddlTipoMovimiento.Enabled = false;
        txtReferencia.Enabled = false;
        gv.Enabled = false;
        txtObservacion.Enabled = false;
    }

    public void ListarMotivo() 
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter("Play_MotivoTraslado_Combo 'C'", conexion);
        da.Fill(dt);
        ddlTipoMovimiento.DataSource = dt;
        ddlTipoMovimiento.DataTextField = "v_Descripcion";
        ddlTipoMovimiento.DataValueField = "n_IdMotivoTraslado";
        ddlTipoMovimiento.DataBind();
        ddlTipoMovimiento.SelectedIndex = 0;
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
                productos.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Crear '" + prefixText + "'", "*"));
                productos.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Crear y Editar ... '" + prefixText + "'", "%"));
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

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("CrearNotaIngreso.aspx");
    }

    protected void btnSalir_Click(object sender, ImageClickEventArgs e)
    {
        Session.Remove("Detalle");
        Response.Redirect("ListarNotaIngreso.aspx");
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
                cmd.CommandText = "Play_NotaIngreso_Insertar";
                cmd.Parameters.AddWithValue("@n_IdAlmacen", ddlAlmacen.SelectedValue);
                cmd.Parameters.AddWithValue("@n_IdMotivoTraslado", ddlTipoMovimiento.SelectedValue);
                cmd.Parameters.AddWithValue("@d_FechaEmision", DateTime.Parse(txtFechaInicial.Text + " " + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00")));
                cmd.Parameters.AddWithValue("@v_Referencia", txtReferencia.Text.Trim());
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
                numero = cmd0.ExecuteScalar().ToString();
                


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
                    cmd3.CommandText = "Play_Stock_Actualizar";
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
                    cmd4.Parameters.AddWithValue("@c_TipoMovimiento", "I");
                    cmd4.Parameters.AddWithValue("@i_IdMotivoTraslado", ddlTipoMovimiento.SelectedValue);
                    cmd4.Parameters.AddWithValue("@n_IdProducto", n_IdProducto);
                    cmd4.Parameters.AddWithValue("@n_IdAlmacen", ddlAlmacen.SelectedValue);
                    cmd4.Parameters.AddWithValue("@f_Cantidad", dt.Rows[i]["Cantidad"].ToString());
                    cmd4.Parameters.AddWithValue("@n_IdTipoDocumento", 8);
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
                cmd5.Parameters.AddWithValue("@n_IdTipoDocumento", 8);
                cmd5.Parameters.AddWithValue("@n_IdAlmacen", ddlAlmacen.SelectedValue);
                cmd5.ExecuteNonQuery();

                tran.Commit();
                lblNumero.Text = numero;

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

    protected void hfIdProducto_ValueChanged(object sender, EventArgs e)
    {
        string selectedWidgetID = ((HiddenField)sender).Value;
        TextBox txtProducto = (TextBox)((HiddenField)sender).Parent.FindControl("txtProducto");
        ImageButton btnNuevoProducto = (ImageButton)((HiddenField)sender).Parent.FindControl("btnNuevoProducto");

        if (selectedWidgetID == "*")
        {
            string n_IdProducto = "";
            int longitud = (txtProducto.Text.Length - 8);
            string producto = txtProducto.Text.Substring(7, longitud);

            //Validar que el no exista el producto con el mismo nombre
            SqlDataAdapter daProducto = new SqlDataAdapter("select count(1) from producto where v_Descripcion = '" + producto.ToUpper().Trim() + "'", conexion);
            DataTable dtProducto = new DataTable();
            daProducto.Fill(dtProducto);
            int cantidad = int.Parse(dtProducto.Rows[0][0].ToString());
            if (cantidad == 0)
            {
                //Crear el producto
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Play_Producto_RegistrarRapido";
                cmd.Parameters.AddWithValue("@v_Descripcion", producto.ToUpper());
                conexion.Open();
                n_IdProducto = cmd.ExecuteScalar().ToString();
                conexion.Close();
                cmd.Dispose();

                ((HiddenField)sender).Value = n_IdProducto;
                lblCodigo.Text = n_IdProducto;
                txtProducto.Text = producto.Trim().ToUpper();
                txtDescripcion.Text = producto.Trim().ToUpper();
                txtPrecio.Text = "1";
                txtCodigoInterno.Text = n_IdProducto;
                btnNuevoProducto.Visible = true;
                txtProducto.BackColor = System.Drawing.Color.FromName("#DBB7FF");
                txtProducto.Enabled = false;
            }
            else 
            {
                txtProducto.Text = "";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'El nombre del producto ya existe!' });</script>", false);

            }
        }
        else if (selectedWidgetID == "%")
        {
            string n_IdProducto = "";
            //Crear y Editar
            int longitud = (txtProducto.Text.Length - 21);
            string producto = txtProducto.Text.Substring(20, longitud);

            //Validar que el no exista el producto con el mismo nombre
            SqlDataAdapter daProducto = new SqlDataAdapter("select count(1) from producto where v_Descripcion = '" + producto.ToUpper().Trim() + "'", conexion);
            DataTable dtProducto = new DataTable();
            daProducto.Fill(dtProducto);
            int cantidad = int.Parse(dtProducto.Rows[0][0].ToString());
            if (cantidad == 0)
            {

                //Crear el cliente
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Play_Producto_RegistrarRapido";
                cmd.Parameters.AddWithValue("@v_Descripcion", producto.ToUpper());
                conexion.Open();
                n_IdProducto = cmd.ExecuteScalar().ToString();
                conexion.Close();
                cmd.Dispose();

                ((HiddenField)sender).Value = n_IdProducto;
                lblCodigo.Text = n_IdProducto;

                tblProducto.Visible = true;
                tblGeneral.Visible = false;
                toolbar.Visible = false;
                txtDescripcion.Text = producto.Trim().ToUpper();
                txtProducto.Text = producto.Trim().ToUpper();
                txtPrecio.Text = "1";
                txtCodigoInterno.Text = n_IdProducto;
                btnNuevoProducto.Visible = true;
                txtProducto.BackColor = System.Drawing.Color.FromName("#DBB7FF");
                txtProducto.Enabled = false;
            }
            else
            {
                txtProducto.Text = "";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'El nombre del producto ya existe!' });</script>", false);
            }
        }
        else
        {
            //Producto seleccionado ya existe
            txtProducto.BackColor = System.Drawing.Color.FromName("#DBB7FF");
            txtProducto.Enabled = false;
            btnNuevoProducto.Visible = true;
            ((HiddenField)sender).Value = selectedWidgetID;
        }
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

    protected void ddlMarca_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListarModelos();
    }

    protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListarSubCategorias();
    }

    protected void btnSalirProducto_Click(object sender, ImageClickEventArgs e)
    {
        tblProducto.Visible = false;
        tblGeneral.Visible = true;
        toolbar.Visible = true;
    }

    protected void btnGuardarProducto_Click(object sender, ImageClickEventArgs e)
    {
        if (txtDescripcion.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar la descripción' });</script>", false);
            txtDescripcion.Focus();
            return;
        }
        if (txtPrecio.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar el precio' });</script>", false);
            txtPrecio.Focus();
            return;
        }
        if (txtCodigoInterno.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar el código interno' });</script>", false);
            txtCodigoInterno.Focus();
            return;
        }

        try
        {
            


            string resultado = "";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Play_Producto_Actualizar";
            cmd.Parameters.AddWithValue("@n_IdProducto", lblCodigo.Text.Trim());
            cmd.Parameters.AddWithValue("@v_Descripcion", txtDescripcion.Text.Trim().ToUpper());
            cmd.Parameters.AddWithValue("@v_Presentacion", txtPresentacion.Text.Trim().ToUpper());
            if (ddlEdad.SelectedIndex == 0) { cmd.Parameters.AddWithValue("@n_IdEdad", DBNull.Value); } else { cmd.Parameters.AddWithValue("@n_IdEdad", ddlEdad.SelectedValue.ToString()); }
            cmd.Parameters.AddWithValue("@c_Sexo", rblSexo.SelectedValue);
            cmd.Parameters.AddWithValue("@v_RutaImagen", lblRuta.Text.Trim().ToUpper());
            cmd.Parameters.AddWithValue("@f_Precio", txtPrecio.Text);
            cmd.Parameters.AddWithValue("@f_Costo", txtCosto.Text);
            cmd.Parameters.AddWithValue("@f_StockMinimo", txtStockMinimo.Text);
            if (ddlProveedor.SelectedIndex == 0) { cmd.Parameters.AddWithValue("@n_IdProveedor", DBNull.Value); } else { cmd.Parameters.AddWithValue("@n_IdProveedor", ddlProveedor.SelectedValue.ToString()); }
            if (ddlMarca.SelectedIndex == 0) { cmd.Parameters.AddWithValue("@n_IdMarca", DBNull.Value); } else { cmd.Parameters.AddWithValue("@n_IdMarca", ddlMarca.SelectedValue.ToString()); }
            if (ddlModelo.SelectedIndex == 0) { cmd.Parameters.AddWithValue("@n_IdModelo", DBNull.Value); } else { cmd.Parameters.AddWithValue("@n_IdModelo", ddlModelo.SelectedValue.ToString()); }
            if (ddlCategoria.SelectedIndex == 0) { cmd.Parameters.AddWithValue("@n_IdCategoria", DBNull.Value); } else { cmd.Parameters.AddWithValue("@n_IdCategoria", ddlCategoria.SelectedValue.ToString()); }
            if (ddlSubCategoria.SelectedIndex == 0) { cmd.Parameters.AddWithValue("@n_IdSubCategoria", DBNull.Value); } else { cmd.Parameters.AddWithValue("@n_IdSubCategoria", ddlSubCategoria.SelectedValue.ToString()); }
            cmd.Parameters.AddWithValue("@b_Estado", chkEstado.Checked);
            if (ddlBateria.SelectedIndex == 0) { cmd.Parameters.AddWithValue("@n_IdPilas", DBNull.Value); } else { cmd.Parameters.AddWithValue("@n_IdPilas", ddlBateria.SelectedValue); }
            cmd.Parameters.AddWithValue("@i_CantidadPilas", txtCantidadBaterias.Text);
            cmd.Parameters.AddWithValue("@v_CodigoInterno", txtCodigoInterno.Text);
            conexion.Open();
            resultado = cmd.ExecuteScalar().ToString();
            conexion.Close();
            lblCodigo.Text = resultado;

            //Obtener código de barras
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select v_CodigoBarras from Producto where n_IdProducto=" + resultado, conexion);
            da.Fill(dt);
            lblCodigoBarras.Text = dt.Rows[0]["v_CodigoBarras"].ToString();

            //Crear imagen redimensionada
            string path = HttpContext.Current.Server.MapPath(lblRuta.Text.Trim().ToUpper());
            byte[] binaryImage = File.ReadAllBytes(path);
            HandleImageUpload(binaryImage, "~/Productos/Redimensionada/" + lblCodigo.Text.Trim() + ".jpg");

            tblProducto.Visible = false;
            tblGeneral.Visible = true;
            toolbar.Visible = true;

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Producto actualizado.' });</script>", false);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.error({ message: '" + ex.Message + "' });</script>", false);
        }
    }

    protected void btnNuevoProducto_Click(object sender, ImageClickEventArgs e)
    {
        HiddenField hfIdProducto = (HiddenField)((ImageButton)sender).Parent.FindControl("hfIdProducto");

        if (hfIdProducto.Value.Trim() == "")
        {

        }
        else
        {
            //Limpiar campos
            lblCodigo.Text = "";
            lblCodigoBarras.Text = "";
            txtDescripcion.Text = "";
            txtPresentacion.Text = "";
            lblRuta.Text = "~/images/Prev.jpg";
            ibImagen.ImageUrl = lblRuta.Text;
            txtPrecio.Text = "0";
            txtCosto.Text = "0";
            txtStockMinimo.Text = "0";
            rblSexo.SelectedValue = "O";
            ddlEdad.SelectedIndex = 0;
            ddlProveedor.SelectedIndex = 0;
            ddlMarca.SelectedIndex = 0;
            ddlMarca_SelectedIndexChanged(null, null);
            ddlModelo.SelectedIndex = 0;
            ddlCategoria.SelectedIndex = 0;
            ddlCategoria_SelectedIndexChanged(null, null);
            ddlSubCategoria.SelectedIndex = 0;
            chkEstado.Checked = true;
            ddlBateria.SelectedIndex = 0;
            ddlBateria_SelectedIndexChanged(null, null);
            txtCantidadBaterias.Text = "0";
            txtCodigoInterno.Text = "";


            //Consultar datos del cliente y mostrarlos
            string n_IdProducto = hfIdProducto.Value;
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Play_Producto_Seleccionar " + n_IdProducto, conexion);
            da.Fill(dt);
            lblCodigo.Text = n_IdProducto;
            lblCodigoBarras.Text = dt.Rows[0]["v_CodigoBarras"].ToString();
            txtDescripcion.Text = dt.Rows[0]["v_Descripcion"].ToString();
            txtPresentacion.Text = dt.Rows[0]["v_Presentacion"].ToString();

            if (dt.Rows[0]["v_RutaImagen"].ToString().Trim() == "")
            {
                lblRuta.Text = "~/images/Prev.jpg";
            }
            else
            {
                lblRuta.Text = dt.Rows[0]["v_RutaImagen"].ToString();
            }
            ibImagen.ImageUrl = lblRuta.Text;

            txtPrecio.Text = float.Parse(dt.Rows[0]["f_Precio"].ToString()).ToString("N2");
            txtCosto.Text = float.Parse(dt.Rows[0]["f_Costo"].ToString()).ToString("N2");
            txtStockMinimo.Text = float.Parse(dt.Rows[0]["f_StockMinimo"].ToString()).ToString("N2");
            rblSexo.SelectedValue = dt.Rows[0]["c_Sexo"].ToString();

            if (dt.Rows[0]["n_IdEdad"].ToString() != "") { ddlEdad.SelectedValue = dt.Rows[0]["n_IdEdad"].ToString(); }
            if (dt.Rows[0]["n_IdProveedor"].ToString() != "") { ddlProveedor.SelectedValue = dt.Rows[0]["n_IdProveedor"].ToString(); }
            if (dt.Rows[0]["n_IdMarca"].ToString() != "") { ddlMarca.SelectedValue = dt.Rows[0]["n_IdMarca"].ToString(); ddlMarca_SelectedIndexChanged(null, null); }
            if (dt.Rows[0]["n_IdModelo"].ToString() != "") { ddlModelo.SelectedValue = dt.Rows[0]["n_IdModelo"].ToString(); }
            if (dt.Rows[0]["n_IdCategoria"].ToString() != "") { ddlCategoria.SelectedValue = dt.Rows[0]["n_IdCategoria"].ToString(); ddlCategoria_SelectedIndexChanged(null, null); }
            if (dt.Rows[0]["n_IdSubCategoria"].ToString() != "") { ddlSubCategoria.SelectedValue = dt.Rows[0]["n_IdSubCategoria"].ToString(); }
            chkEstado.Checked = bool.Parse(dt.Rows[0]["b_Estado"].ToString());

            if (dt.Rows[0]["n_IdPilas"].ToString() != "") { ddlBateria.SelectedValue = dt.Rows[0]["n_IdPilas"].ToString(); ddlBateria_SelectedIndexChanged(null, null); }
            txtCantidadBaterias.Text = dt.Rows[0]["i_CantidadPilas"].ToString();
            txtCodigoInterno.Text = dt.Rows[0]["v_CodigoInterno"].ToString();


        }
        tblProducto.Visible = true;
        tblGeneral.Visible = false;
        toolbar.Visible = false;
    }

    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField IdProducto = (HiddenField)e.Row.FindControl("hfIdProducto");
            TextBox txtProducto = (TextBox)e.Row.FindControl("txtProducto");
            ImageButton btnNuevoProducto = (ImageButton)e.Row.FindControl("btnNuevoProducto");

            if (int.Parse(IdProducto.Value) > 0)
            {
                txtProducto.BackColor = System.Drawing.Color.FromName("#DBB7FF");
                btnNuevoProducto.Visible = true;
                txtProducto.Enabled = false;
            }
        }

    }

    protected void ibAnular_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Request.QueryString["n_IdNotaIngreso"] != null)
            {
                string n_IdNotaIngreso = Request.QueryString["n_IdNotaIngreso"];

                if (Session["dtUsuario"] != null)
                {
                    DataTable dtUsuario = new DataTable();
                    dtUsuario = (DataTable)Session["dtUsuario"];
                    string n_IdUsuario = dtUsuario.Rows[0]["n_IdUsuario"].ToString();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conexion;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Play_NotaIngreso_Anular";
                    cmd.Parameters.AddWithValue("@n_IdNotaIngreso", n_IdNotaIngreso);
                    cmd.Parameters.AddWithValue("@n_IdUsuarioAnulacion", n_IdUsuario);
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                    conexion.Close();

                    lblEstado.Text = "Anulado";
                    lblEstado.ForeColor = System.Drawing.Color.Red;
                    Label1.ForeColor = System.Drawing.Color.Red;
                    lblNumero.ForeColor = System.Drawing.Color.Red;
                    ibAnular.Visible = false;

                    BloquearNotaIngreso();

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Nota de Ingreso Anulada Satisfactoriamente' });</script>", false);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.error({ message: '" + ex.Message + "' });</script>", false);
        }
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

    private MemoryStream BytearrayToStream(byte[] arr)
    {
        return new MemoryStream(arr, 0, arr.Length);
    }

    private void HandleImageUpload(byte[] binaryImage, string NuevoNombre)
    {
        System.Drawing.Image img = RezizeImage(System.Drawing.Image.FromStream(BytearrayToStream(binaryImage)), 100, 100);
        img.Save(Server.MapPath(NuevoNombre), System.Drawing.Imaging.ImageFormat.Jpeg);
    }


}