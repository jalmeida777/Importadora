<%@ Page Title="Imprimir Comprobante" Language="C#" MasterPageFile="~/Plantilla.master" AutoEventWireup="true" CodeFile="Comprobante.aspx.cs" Inherits="Reportes_Comprobante" %>

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
                        &nbsp;Imprimir Comprobante</h1>
                </td>
            </tr>
        </table>
    </div>


 <div class="toolbar">
            <table width="100%"><tr>
                <td width="65">
                    
                            <asp:ImageButton ID="btnSalir" runat="server" ImageUrl="~/images/Salir.jpg" 
                                onclick="btnSalir_Click" />
                    
                </td>
                <td>
                   
                    <asp:ImageButton ID="btnImprimir" runat="server" 
                        ImageUrl="~/images/Imprimir.jpg" onclick="btnImprimir_Click" />
                </td>
                </tr></table>
            </div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:PlayConnectionString %>" 
        SelectCommand="Play_Pedido_Seleccionar" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:QueryStringParameter Name="n_IdPedido" QueryStringField="n_IdPedido" 
                Type="Decimal" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:PlayConnectionString %>" 
        SelectCommand="Play_DetPedido_Listar" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:QueryStringParameter Name="n_IdPedido" QueryStringField="n_IdPedido" 
                Type="Decimal" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
        ConnectionString="<%$ ConnectionStrings:PlayConnectionString %>" 
        SelectCommand="Play_Empresa_Seleccionar" SelectCommandType="StoredProcedure">
    </asp:SqlDataSource>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
        Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
        WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%" 
        Height="600px">
        <LocalReport ReportPath="Comprobante.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="dsComprobante" />
                <rsweb:ReportDataSource DataSourceId="SqlDataSource2" Name="dsDetalle" />
                <rsweb:ReportDataSource DataSourceId="SqlDataSource3" Name="dsEmpresa" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
</asp:Content>

