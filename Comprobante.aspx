<%@ Page Title="Imprimir Comprobante" Language="C#" MasterPageFile="~/Plantilla.master" AutoEventWireup="true" CodeFile="Comprobante.aspx.cs" Inherits="Reportes_Comprobante" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

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
                   
                    &nbsp;</td>
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

            <cr:crystalreportsource ID="CrystalReportSource1" runat="server">
            <report filename="Ticket.rpt">
                <DataSources>
                    <CR:DataSourceRef DataSourceID="SqlDataSource1" 
                        TableName="Play_Pedido_Seleccionar" />
                    <CR:DataSourceRef DataSourceID="SqlDataSource2" 
                        TableName="Play_DetPedido_Listar" />
                    <CR:DataSourceRef DataSourceID="SqlDataSource3" 
                        TableName="Play_Empresa_Seleccionar" />
                </DataSources>
            </report>
        </cr:crystalreportsource>

                    <cr:crystalreportviewer ID="CrystalReportViewer1" runat="server" 
                        AutoDataBind="true" DisplayGroupTree="False" HasDrillUpButton="False" 
                        HasGotoPageButton="False" HasSearchButton="False" 
                        HasToggleGroupTreeButton="False" HasViewList="False" HasZoomFactorList="False" 
                        PrintMode="ActiveX" ReportSourceID="CrystalReportSource1" 
                        ReuseParameterValuesOnRefresh="True" 
                        ToolbarImagesFolderUrl="../images/toolbar/" 
        HasCrystalLogo="False" HasExportButton="False" 
        HasPageNavigationButtons="False" />

    </asp:Content>

