﻿<%@ Page Title="Reporte de Ventas por Vendedor" Language="C#" MasterPageFile="~/Plantilla.master" AutoEventWireup="true" CodeFile="ReporteVentasVendedor.aspx.cs" Inherits="ReporteVentasVendedor" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divBusqueda">
        <table width="100%">
            <tr>
                <td>
                    <h1 class="label">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/statistics.png" />
                        &nbsp;Reporte de Ventas por Vendedor</h1>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="style1">
                        <tr>
                            <td class="label" width="100">
                                <asp:Label ID="Label2" runat="server" Text="Fecha Inicial:"></asp:Label>
                            </td>
                            <td width="110">
                                <asp:TextBox ID="txtFechaInicial" runat="server" CssClass="inputsFecha"
                            MaxLength="10"></asp:TextBox>
                                <cc1:CalendarExtender 
                        ID="CalendarExtender1" 
                        runat="server" 
                        TargetControlID="txtFechaInicial" Format="dd/MM/yyyy">
                                </cc1:CalendarExtender>
                            </td>
                            <td class="label" width="90">
                                <asp:Label ID="Label3" runat="server" Text="Fecha Final:"></asp:Label>
                            </td>
                            <td width="110">
                                <asp:TextBox ID="txtFechaFinal" runat="server" CssClass="inputsFecha" 
                            MaxLength="10"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtFechaFinal_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaFinal">
                                </cc1:CalendarExtender>
                            </td>
                            <td class="label" width="70">
                                &nbsp;</td>
                            <td width="210">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>


 <div class="toolbar">
            <table width="100%"><tr><td width="65">
               
                            <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/images/Buscar.jpg" 
                                onclick="btnBuscar_Click" />
               
                </td>
                <td width="65">
                    
                            <asp:ImageButton ID="btnSalir" runat="server" ImageUrl="~/images/Salir.jpg" 
                                onclick="btnSalir_Click" />
                    
                </td>
                <td>
                   
                    <asp:Label ID="lblFechaInicial" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblFechaFinal" runat="server" Visible="False"></asp:Label>
                   
                </td>
                </tr></table>
            </div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
    ConnectionString="<%$ ConnectionStrings:PlayConnectionString %>" SelectCommand="Play_VentasxVendedor_Reporte" 
    SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="lblFechaInicial" Name="fechaInicial" 
                PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="lblFechaFinal" Name="fechaFinal" 
                PropertyName="Text" Type="String" />
        </SelectParameters>
        </asp:SqlDataSource>
                         <table width="100%" 
        
        style="border-bottom: 1px solid #ddd; background-image: url('../images/form_sheetbg.png'); background-repeat: repeat; ">
        <tr>
            <td width="7%">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td width="7%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="7%">
                &nbsp;</td>
            <td>
                <div class="divDocumento">
                <table width="100%" cellspacing="5" >
        <tr>
            <td height="10" width="20">
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
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
                    Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
                    WaitMessageFont-Names="Verdana" 
    WaitMessageFont-Size="14pt" Width="100%">
                    <LocalReport ReportPath="VentasPorVendedor.rdlc">
                        <DataSources>
                            <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="dsVendedor" />
                        </DataSources>
                    </LocalReport>
                </rsweb:ReportViewer>
            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td width="20">
                &nbsp;</td>
        </tr>
        </table>
        <tr>
            <td width="7%">
                &nbsp;</td>
            <td>
                &nbsp;</table>

</asp:Content>

