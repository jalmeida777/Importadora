﻿<%@ Page Title="Tipo de Cambio" Language="C#" MasterPageFile="~/Plantilla.master" AutoEventWireup="true" CodeFile="TipoCambio.aspx.cs" Inherits="TipoCambio" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

      <script type="text/javascript">
          function ValidaEntero(e) {
              var tecla = document.all ? tecla = e.keyCode : tecla = e.which;
              if (tecla > 31 && (tecla < 48 || tecla > 57))
                  return false;
          }

          function ValidaNumeros(e) {
              var tecla = document.all ? tecla = e.keyCode : tecla = e.which;
              if (tecla > 31 && (tecla < 48 || tecla > 57) && tecla != 46)
                  return false;
          }
         </script>

         <script src="js/jquery.growl.js" type="text/javascript"></script>
        <link href="css/jquery.growl.css" rel="stylesheet" type="text/css" />

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">



    <div class="divBusqueda">
        <table width="100%">
            <tr>
                <td>
                    <h1 class="label">
                        Tipo de Cambio Diario</h1>
                </td>
            </tr>
        </table>
    </div>


 <div class="toolbar">
            <table width="100%"><tr><td width="96">
               
                            <asp:ImageButton ID="btnGuardar" runat="server" 
                                ImageUrl="~/images/btnGuardar_New.png" onclick="btnGuardar_Click" />
                            <cc1:confirmbuttonextender ID="btnGuardar_ConfirmButtonExtender" runat="server" 
                                ConfirmText="¿Seguro de guardar los datos?" Enabled="True" 
                                TargetControlID="btnGuardar">
                            </cc1:confirmbuttonextender>
               
                </td>
                <td width="65">
                    
                            <asp:ImageButton ID="btnSalir" runat="server" ImageUrl="~/images/btnSalir_New.png" 
                                onclick="btnSalir_Click" />
                    
                </td>
                <td>
                   
                </td>
                </tr></table>
            </div>
                         <table width="100%" 
        style="background-image: url('images/form_sheetbg.png'); background-repeat: repeat; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #ddd;">
        <tr>
            <td width="15%">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td width="15%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="15%">
                &nbsp;</td>
            <td>
                <div class="divDocumento">
                <table width="100%" cellspacing="5" >
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td>
                <asp:Label ID="Label19" runat="server" Text="Fecha:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFecha" runat="server"></asp:Label>
            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td width="170">
                <asp:Label ID="Label2" runat="server" Text="Tipo de Cambio S/.:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtTipoCambio" runat="server" CssClass="inputNormalMoneda"
                    Width="60px" onkeypress="return ValidaNumeros(event);"></asp:TextBox>
                <asp:Label ID="Label16" runat="server" Font-Bold="True" ForeColor="#18AC85" 
                        Text="*"></asp:Label>
            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td width="20">
                &nbsp;</td>
        </tr>
        </table>
        <tr>
            <td width="15%">
                &nbsp;</td>
            <td>
                &nbsp;</table>


</asp:Content>