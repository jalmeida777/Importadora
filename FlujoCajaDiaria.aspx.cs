﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class FlujoCajaDiaria : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            txtFecha.Text = DateTime.Now.ToShortDateString();
            

        }
    }
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dtUsuario = new DataTable();
        dtUsuario = (DataTable)Session["dtUsuario"];
        string n_IdUsuario = dtUsuario.Rows[0]["n_IdUsuario"].ToString();

        DataTable dtAlmacen = new DataTable();
        dtAlmacen = (DataTable)Session["dtAlmacenes"];
        string idAlmacen = dtAlmacen.Rows[0]["i_IdAlmacen"].ToString();

        string anio, mes, dia;
        
        anio = DateTime.Parse(txtFecha.Text).Year.ToString("0000");
        mes = DateTime.Parse(txtFecha.Text).Month.ToString("00");
        dia = DateTime.Parse(txtFecha.Text).Day.ToString("00");

        DataTable dtIdCaja = new DataTable();
        SqlDataAdapter daIdCaja = new SqlDataAdapter("BDVETER_CajaHistorica_SelectID " + idAlmacen + "," + anio + "," + mes + "," + dia, conexion);
        daIdCaja.Fill(dtIdCaja);
        string idCaja = dtIdCaja.Rows[0]["i_IdCaja"].ToString();


        SqlDataAdapter da = new SqlDataAdapter("BDVETER_FlujoCaja_Listar " + idCaja, conexion);
        DataTable dt = new DataTable();
        da.Fill(dt);

        lblCajaInicial.Text = dt.Rows[0]["f_CajaInicial"].ToString();
        lblTotalIngresos.Text = dt.Rows[0]["f_IngresoAdicional"].ToString();
        lblTotalSalidas.Text = dt.Rows[0]["f_SalidaAdicional"].ToString();
        lblTotalVentas.Text = dt.Rows[0]["f_TotalVenta"].ToString();
        lblCajaFinal.Text = dt.Rows[0]["f_CajaFinal"].ToString();
        lblCajaReal.Text = dt.Rows[0]["f_CajaReal"].ToString();


        DataTable dt2 = new DataTable();
        SqlDataAdapter da2 = new SqlDataAdapter("BDVETER_CajaMovimiento_Listar " + idCaja, conexion);
        da2.Fill(dt2);
        gvReciboCaja.DataSource = dt2;
        gvReciboCaja.DataBind();
              
              
    }

   
}