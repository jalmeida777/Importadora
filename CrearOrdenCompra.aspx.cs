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
            InicializarGrilla();
            tblProveedor.Visible = false;
        }
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

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {

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
                cmd.Parameters.AddWithValue("@n_IdProveedor", "");
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
                lblUsuarioRegistro.Text = dtUsuario.Rows[0]["v_Usuario"].ToString();
                lblFechaRegistro.Text = DateTime.Now.ToString();
                ibUsuarioRegistro.ImageUrl = dtUsuario.Rows[0]["v_RutaFoto"].ToString();
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
            if (lblCodigo.Text.Trim() != "")
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Play_Proveedor_Actualizar";
                cmd.Parameters.AddWithValue("@n_IdProveedor", lblCodigo.Text);
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
                lblCodigo.Text = n_IdProveedor;
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
            lblCodigo.Text = n_IdProveedor.ToString();
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
}