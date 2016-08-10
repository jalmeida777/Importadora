using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

public partial class CrearProducto : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {

            ListarCategoria();
            ListarProveedor();
            ListarBaterias();
            

            if (Request.QueryString["n_IdProducto"] != null)
            {
                string n_IdProducto = Request.QueryString["n_IdProducto"];
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("Play_Producto_Seleccionar " + n_IdProducto, conexion);
                da.Fill(dt);
                lblCodigo.Text = n_IdProducto;
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
                //ibImagen.ImageUrl = lblRuta.Text;
                ibImagen.ImageUrl = "~/Productos/Redimensionada/" + lblCodigo.Text + ".jpg";

                txtPrecio.Text = float.Parse(dt.Rows[0]["f_Precio"].ToString()).ToString("N2");
                txtCosto.Text = float.Parse(dt.Rows[0]["f_Costo"].ToString()).ToString("N2");
                txtStockMinimo.Text = int.Parse(dt.Rows[0]["f_StockMinimo"].ToString()).ToString();
                rblSexo.SelectedValue = dt.Rows[0]["c_Sexo"].ToString();

                if (dt.Rows[0]["n_IdProveedor"].ToString() != "") { ddlProveedor.SelectedValue = dt.Rows[0]["n_IdProveedor"].ToString(); }
                if (dt.Rows[0]["n_IdCategoria"].ToString() != "") { ddlCategoria.SelectedValue = dt.Rows[0]["n_IdCategoria"].ToString(); }
                chkEstado.Checked = bool.Parse(dt.Rows[0]["b_Estado"].ToString());

                if (dt.Rows[0]["n_IdPilas"].ToString() != "") { ddlBateria.SelectedValue = dt.Rows[0]["n_IdPilas"].ToString(); }
                txtCodigoInterno.Text = dt.Rows[0]["v_CodigoInterno"].ToString();

                ListarStock();
                ibAtras.Visible = true;
                ibSiguiente.Visible = true;
            }
            else 
            {
                txtPrecio.Text = "0.00";
                txtCosto.Text = "0.00";
                txtStockMinimo.Text = "0";
            }
        }

        string eventTarget = Convert.ToString(Request.Params.Get("__EVENTTARGET"));
        string eventArgument = Convert.ToString(Request.Params.Get("__EVENTARGUMENT"));

        if (eventTarget == "baterias")
        {
            ListarBaterias();
            ddlBateria.SelectedValue = eventArgument;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Batería registrada.' });</script>", false);
        }
        else if (eventTarget == "familias")
        {
            ListarCategoria();
            ddlCategoria.SelectedValue = eventArgument;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Familia registrada.' });</script>", false);
        }
        else if (eventTarget == "proveedores")
        {
            ListarProveedor();
            ddlProveedor.SelectedValue = eventArgument;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Proveedor registrado.' });</script>", false);
        }


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

    void ListarStock() 
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter("Play_Stock_Almacen " + lblCodigo.Text, conexion);
        da.Fill(dt);
        gvStock.DataSource = dt;
        gvStock.DataBind();
    }

    void ListarKardex()
    {
        string n_IdProducto = lblCodigo.Text;
        string n_IdAlmacen = gvStock.SelectedDataKey.Value.ToString();

        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter("Play_Kardex_Listar " + n_IdProducto + "," + n_IdAlmacen, conexion);
        da.Fill(dt);
        gvKardex.DataSource = dt;
        gvKardex.DataBind();
        Label21.Visible = true;
        Panel1.Visible = true;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        if (txtDescripcion.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar la descripción' });</script>", false);
            txtDescripcion.Focus();
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





        if (lblCodigo.Text.Trim() == "")
        {
           
            string resultado = "";
            //Validar que el no exista el producto con el mismo nombre
            SqlDataAdapter daProducto = new SqlDataAdapter("select count(1) from producto where v_Descripcion = '" + txtDescripcion.Text.Trim() + "'", conexion);
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
                cmd.Parameters.AddWithValue("@v_Descripcion", txtDescripcion.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Presentacion", txtPresentacion.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@n_IdEdad", DBNull.Value);
                cmd.Parameters.AddWithValue("@c_Sexo", rblSexo.SelectedValue);
                cmd.Parameters.AddWithValue("@v_RutaImagen", lblRuta.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@f_Precio", txtPrecio.Text);
                cmd.Parameters.AddWithValue("@f_Costo", txtCosto.Text);
                cmd.Parameters.AddWithValue("@f_StockMinimo", int.Parse(txtStockMinimo.Text));
                if (ddlProveedor.SelectedIndex == 0) { cmd.Parameters.AddWithValue("@n_IdProveedor", DBNull.Value); } else { cmd.Parameters.AddWithValue("@n_IdProveedor", ddlProveedor.SelectedValue.ToString()); }
                cmd.Parameters.AddWithValue("@n_IdMarca", DBNull.Value);
                cmd.Parameters.AddWithValue("@n_IdModelo", DBNull.Value);
                if (ddlCategoria.SelectedIndex == 0) { cmd.Parameters.AddWithValue("@n_IdCategoria", DBNull.Value); } else { cmd.Parameters.AddWithValue("@n_IdCategoria", ddlCategoria.SelectedValue.ToString()); }
                cmd.Parameters.AddWithValue("@n_IdSubCategoria", DBNull.Value);
                cmd.Parameters.AddWithValue("@b_Estado", chkEstado.Checked);
                if (ddlBateria.SelectedIndex == 0) { cmd.Parameters.AddWithValue("@n_IdPilas", DBNull.Value); } else { cmd.Parameters.AddWithValue("@n_IdPilas", ddlBateria.SelectedValue); }
                cmd.Parameters.AddWithValue("@i_CantidadPilas", 0);
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

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Producto registrado.' });</script>", false);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.error({ message: '" + ex.Message + "' });</script>", false);
            }
            

        }
        else
        {
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
                cmd.Parameters.AddWithValue("@n_IdEdad", DBNull.Value);
                cmd.Parameters.AddWithValue("@c_Sexo", rblSexo.SelectedValue);
                cmd.Parameters.AddWithValue("@v_RutaImagen", lblRuta.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@f_Precio", txtPrecio.Text);
                cmd.Parameters.AddWithValue("@f_Costo", txtCosto.Text);
                cmd.Parameters.AddWithValue("@f_StockMinimo", int.Parse(txtStockMinimo.Text));
                if (ddlProveedor.SelectedIndex == 0) { cmd.Parameters.AddWithValue("@n_IdProveedor", DBNull.Value); } else { cmd.Parameters.AddWithValue("@n_IdProveedor", ddlProveedor.SelectedValue.ToString()); }
                cmd.Parameters.AddWithValue("@n_IdMarca", DBNull.Value);
                cmd.Parameters.AddWithValue("@n_IdModelo", DBNull.Value);
                if (ddlCategoria.SelectedIndex == 0) { cmd.Parameters.AddWithValue("@n_IdCategoria", DBNull.Value); } else { cmd.Parameters.AddWithValue("@n_IdCategoria", ddlCategoria.SelectedValue.ToString()); }
                cmd.Parameters.AddWithValue("@n_IdSubCategoria", DBNull.Value);
                cmd.Parameters.AddWithValue("@b_Estado", chkEstado.Checked);
                if (ddlBateria.SelectedIndex == 0) { cmd.Parameters.AddWithValue("@n_IdPilas", DBNull.Value); } else { cmd.Parameters.AddWithValue("@n_IdPilas", ddlBateria.SelectedValue); }
                cmd.Parameters.AddWithValue("@i_CantidadPilas", 0);
                cmd.Parameters.AddWithValue("@v_CodigoInterno", txtCodigoInterno.Text);
                conexion.Open();
                resultado = cmd.ExecuteScalar().ToString();
                conexion.Close();
                lblCodigo.Text = resultado;

                //Crear imagen redimensionada
                string path = HttpContext.Current.Server.MapPath(lblRuta.Text.Trim().ToUpper());
                byte[] binaryImage = File.ReadAllBytes(path);
                HandleImageUpload(binaryImage, "~/Productos/Redimensionada/" + lblCodigo.Text.Trim() + ".jpg");

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Producto actualizado.' });</script>", false);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.error({ message: 'Ya hay un producto registrado con el mismo código interno!' });</script>", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.error({ message: '" + ex.Message + "' });</script>", false);
                }
            }
        }
        

    }

    protected void btnSalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("ListarStockActual.aspx");
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
                cmd.CommandText = "select v_Descripcion from Producto where v_Descripcion like '%' + @SearchText + '%' order by v_Descripcion";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Connection = conn;
                conn.Open();
                List<string> productos = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        productos.Add(sdr["v_Descripcion"].ToString());
                    }
                }
                conn.Close();
                return productos;
            }
        }
    }

    protected void gvStock_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListarKardex();
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

    protected void ibSiguiente_Click(object sender, ImageClickEventArgs e)
    {
        if (Request.QueryString["n_IdProducto"] != null && Request.QueryString["IdMenu"]!=null)
        {
            string n_IdProducto = Request.QueryString["n_IdProducto"];
            string siguiente = "";
            SqlDataAdapter da = new SqlDataAdapter("select n_IdProducto from Producto order by v_Descripcion asc", conexion);
            DataTable dt = new DataTable();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["n_IdProducto"].ToString().ToUpper() == n_IdProducto)
                {
                    if ((i + 1) == dt.Rows.Count) { break; }
                    else
                    {
                        siguiente = dt.Rows[i + 1]["n_IdProducto"].ToString();
                        break;
                    }
                }
            }
            if (siguiente != "")
            {
                Response.Redirect("CrearProducto.aspx?n_IdProducto=" + siguiente);
            }
        }
    }

    protected void ibAtras_Click(object sender, ImageClickEventArgs e)
    {
        if (Request.QueryString["n_IdProducto"] != null && Request.QueryString["IdMenu"] != null)
        {
            string n_IdProducto = Request.QueryString["n_IdProducto"];
            string anterior = "";
            SqlDataAdapter da = new SqlDataAdapter("select n_IdProducto from Producto order by v_Descripcion asc", conexion);
            DataTable dt = new DataTable();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["n_IdProducto"].ToString().ToUpper() == n_IdProducto)
                {
                    if ((i - 1) == -1) { break; }
                    else
                    {
                        anterior = dt.Rows[i - 1]["n_IdProducto"].ToString();
                        break;
                    }
                }
            }
            if (anterior != "")
            {
                Response.Redirect("CrearProducto.aspx?n_IdProducto=" + anterior);
            }
        }
    }

    void BloquearProducto() 
    {
        txtDescripcion.Enabled = false;
        ibAtras.Enabled = false;
        ibSiguiente.Enabled = false;
        txtPresentacion.Enabled = false;
        rblSexo.Enabled = false;
        ddlBateria.Enabled = false;
        txtPrecio.Enabled = false;
        txtCosto.Enabled = false;
        fu1.Enabled = false;
        ibUpload.Enabled = false;
        txtStockMinimo.Enabled = false;
        ddlCategoria.Enabled = false;
        ddlProveedor.Enabled = false;
        chkEstado.Enabled = false;
        TabContainer1.Enabled = false;
    }

    protected void callbackPanel_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        if (Request.QueryString["n_IdProducto"] != null)
        {
            string n_IdProducto = Request.QueryString["n_IdProducto"];
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select v_RutaImagen,v_CodigoInterno,v_Descripcion from producto where n_IdProducto=" + n_IdProducto, conexion);
            da.Fill(dt);
            lblCodigo0.Text = dt.Rows[0]["v_CodigoInterno"].ToString();
            lblProducto.Text = dt.Rows[0]["v_Descripcion"].ToString();
            ImagenGrande.ImageUrl = dt.Rows[0]["v_RutaImagen"].ToString();
        }
    }

    protected void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        if (Request.QueryString["n_IdProducto"] != null)
        {
            string n_IdProducto = Request.QueryString["n_IdProducto"];

            DataTable dtCodigo = new DataTable();
            SqlDataAdapter daCodigo = new SqlDataAdapter("select v_CodigoInterno from Producto where n_IdProducto=" + n_IdProducto, conexion);
            daCodigo.Fill(dtCodigo);
            string v_CodigoInterno = dtCodigo.Rows[0]["v_CodigoInterno"].ToString();

            Response.Redirect("BarcodeJquery.aspx?v_CodigoInterno=" + v_CodigoInterno + "&n_IdProducto=" + n_IdProducto);
        }
    }
}