<%@ Page Title="Reporte Stock Actual" Language="C#" MasterPageFile="~/Plantilla.master" AutoEventWireup="true" CodeFile="ReporteStockActual.aspx.cs" Inherits="Reportes_ReporteStockActual" %>

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
                        &nbsp;Reporte Stock Actual</h1>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="style1">
                        <tr>
                            <td class="label" width="70">
                                <asp:Label ID="Label4" runat="server" Text="Sucursal:"></asp:Label>
                            </td>
                            <td width="210">
                                <asp:DropDownList ID="ddlAlmacen" runat="server" 
                            CssClass="combo" 
                            Width="200px">
                                </asp:DropDownList>
                            </td>
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
                   
                    &nbsp;</td>
                </tr></table>
            </div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
    ConnectionString="<%$ ConnectionStrings:PlayConnectionString %>" SelectCommand="Play_Stock_Reporte" 
    SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlAlmacen" Name="n_IdAlmacen" 
                PropertyName="SelectedValue" Type="Decimal" />
        </SelectParameters>
        </asp:SqlDataSource>
                         <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:PlayConnectionString %>" 
        SelectCommand="select v_Descripcion,v_Direccion from Almacen where n_IdAlmacen = @n_IdAlmacen ">
                             <SelectParameters>
                                 <asp:ControlParameter ControlID="ddlAlmacen" Name="n_IdAlmacen" 
                                     PropertyName="SelectedValue" />
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
                    <LocalReport ReportPath="StockActual.rdlc">
                        <DataSources>
                            <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
                            <rsweb:ReportDataSource DataSourceId="SqlDataSource2" Name="dsAlmacen" />
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

