<%@ Page Title="Pedidos" Language="C#" MasterPageFile="~/Plantilla.master" AutoEventWireup="true" CodeFile="ListarPedidos.aspx.cs" Inherits="ListarPedidos" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.2, Version=11.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.2, Version=11.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallback" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.2, Version=11.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallback" tagprefix="dx1" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.2, Version=11.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        
        <div class="divBusqueda">
            <table width="100%">
                <tr>
                    <td colspan="7">
                        <h1>
                            Ventas</h1>
                    </td>
                </tr>
                <tr>
                    <td width="90" style="padding-left: 10px">
                        <asp:Label ID="Label1" runat="server" Text="Fecha Inicial:" Font-Bold="False"></asp:Label>
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
                    <td width="85">
                        <asp:Label ID="Label2" runat="server" Text="Fecha Final:" Font-Bold="False"></asp:Label>
                    </td>
                    <td width="110">
                        <asp:TextBox ID="txtFechaFinal" runat="server" CssClass="inputsFecha" 
                            MaxLength="10"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtFechaFinal_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaFinal">
                        </cc1:CalendarExtender>
                    </td>
                    <td width="70">
                        <asp:Label ID="Label3" runat="server" Font-Bold="False" Text="Sucursal:"></asp:Label>
                    </td>
                    <td width="210">
                        <asp:DropDownList ID="ddlAlmacen" runat="server" 
                            CssClass="combo" 
                            Width="200px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        
                        <asp:CheckBox ID="chkHabilitado" runat="server" Checked="True" 
                            Font-Bold="False" Text="Habilitados" />
                        
                        <asp:Label ID="lblFechaInicial" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblFechaFinal" runat="server" Visible="False"></asp:Label>
                        
                        <asp:Label ID="lblSucursal" runat="server" Visible="False"></asp:Label>
                        
                    </td>
                </tr>
            </table>
            </div>
            <div class="toolbar">
            <table width="100%"><tr><td width="95">
                <asp:ImageButton ID="btnNuevo" runat="server" 
                    ImageUrl="~/images/btnNuevo_New.png" onclick="btnNuevo_Click" ToolTip="Nuevo" />
                </td>
                <td width="95">
                    <asp:ImageButton ID="btnConsultar" runat="server" ImageUrl="~/images/btnBuscar_New.png" 
                        onclick="btnConsultar_Click" ToolTip="Buscar" />
                </td>
                <td align="left">
                    <asp:ImageButton ID="btnSalir" runat="server" ImageUrl="~/images/btnSalir_New.png" 
                        onclick="btnSalir_Click" />

              
                </td>
                </tr></table>
            </div>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
        AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <img src="images/loading.gif" />
                </ProgressTemplate>
            </asp:UpdateProgress>
       
                <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" 
                    CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue" 
                    DataSourceID="SqlDataSource1" EnableCallbackCompression="False" 
                    EnableCallBacks="False" EnableRowsCache="False" EnableTheming="False" 
                    EnableViewState="False" KeyFieldName="n_IdPedido" 
                    onhtmlrowprepared="ASPxGridView1_HtmlRowPrepared" Width="100%">
                    <TotalSummary>
                        <dx:ASPxSummaryItem DisplayFormat="{0:C}" FieldName="f_Total" 
                            ShowInColumn="Total" ShowInGroupFooterColumn="Total" SummaryType="Sum" />
                    </TotalSummary>
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="N° Pedido" VisibleIndex="0" Width="100px">
                            <DataItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" Font-Underline="True" 
                                    ForeColor="Blue" Text='<%# Bind("v_NumeroPedido") %>'></asp:LinkButton>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Fecha" FieldName="d_FechaEmision" 
                            VisibleIndex="1" Width="100px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Cliente" FieldName="v_Nombre" 
                            VisibleIndex="2" Width="120px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Forma de Pago" FieldName="v_FormaPago" 
                            VisibleIndex="3" Width="100px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Vendedor" FieldName="Vendedor" 
                            VisibleIndex="4" Width="120px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Creado por" FieldName="Usuario" 
                            VisibleIndex="5" Width="150px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Total" FieldName="f_Total" VisibleIndex="8" 
                            Width="100px">
                            <PropertiesTextEdit DisplayFormatString="{0:C}"></PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Sub Total" FieldName="f_SubTotal" 
                            VisibleIndex="6" Width="100px">
                            <PropertiesTextEdit DisplayFormatString="{0:C}"></PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Descuento" FieldName="f_Descuento" 
                            VisibleIndex="7" Width="100px">
                            <PropertiesTextEdit DisplayFormatString="{0:C}"></PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Sucursal" FieldName="Sucursal" 
                            VisibleIndex="9" Width="80px">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsBehavior AutoFilterRowInputDelay="0" />
                    <SettingsPager PageSize="5" ShowDefaultImages="False">
                        <AllButton Text="All">
                        </AllButton>
                        <NextPageButton Text="Next &gt;">
                        </NextPageButton>
                        <PrevPageButton Text="&lt; Prev">
                        </PrevPageButton>
                    </SettingsPager>
                    <Settings ShowFooter="True" />
                    <SettingsCookies StoreFiltering="False" />
                    <SettingsDetail ShowDetailRow="True" />
                    <Images SpriteCssFilePath="~/App_Themes/PlasticBlue/{0}/sprite.css">
                        <LoadingPanelOnStatusBar Url="~/App_Themes/PlasticBlue/GridView/gvLoadingOnStatusBar.gif">
                        </LoadingPanelOnStatusBar>
                        <LoadingPanel Url="~/App_Themes/PlasticBlue/GridView/Loading.gif">
                        </LoadingPanel>
                    </Images>
                    <ImagesFilterControl>
                        <LoadingPanel Url="~/App_Themes/PlasticBlue/Editors/Loading.gif">
                        </LoadingPanel>
                    </ImagesFilterControl>
                    <Styles CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" 
                        CssPostfix="PlasticBlue">
                        <Header ImageSpacing="10px" SortingImageSpacing="10px">
                        </Header>
                    </Styles>
                    <StylesEditors>
                        <CalendarHeader Spacing="11px">
                        </CalendarHeader>
                        <ProgressBar Height="25px">
                        </ProgressBar>
                    </StylesEditors>
                    <Templates>
                        <DetailRow>
                            <dx:ASPxGridView ID="gvDetalle" runat="server" AutoGenerateColumns="False" 
                                CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue" 
                                DataSourceID="SqlDataSource2" 
                                onbeforeperformdataselect="gvDetalle_BeforePerformDataSelect" Width="100%">
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="Cantidad" FieldName="i_Cantidad" 
                                        VisibleIndex="0" Width="80px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Precio Unitario S/." 
                                        FieldName="f_PrecioUnitario" VisibleIndex="3" Width="100px">
                                        <PropertiesTextEdit DisplayFormatString="{0:C}"></PropertiesTextEdit>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Precio Total S/." FieldName="f_PrecioTotal" 
                                        VisibleIndex="4" Width="100px">
                                        <PropertiesTextEdit DisplayFormatString="{0:C}"></PropertiesTextEdit>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Producto" FieldName="Producto" 
                                        VisibleIndex="2">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Código" FieldName="Codigo" VisibleIndex="1" 
                                        Width="80px">
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <SettingsPager ShowDefaultImages="False" Visible="False">
                                    <AllButton Text="All">
                                    </AllButton>
                                    <NextPageButton Text="Next &gt;">
                                    </NextPageButton>
                                    <PrevPageButton Text="&lt; Prev">
                                    </PrevPageButton>
                                </SettingsPager>
                                <Images SpriteCssFilePath="~/App_Themes/PlasticBlue/{0}/sprite.css">
                                    <LoadingPanelOnStatusBar Url="~/App_Themes/PlasticBlue/GridView/gvLoadingOnStatusBar.gif">
                                    </LoadingPanelOnStatusBar>
                                    <LoadingPanel Url="~/App_Themes/PlasticBlue/GridView/Loading.gif">
                                    </LoadingPanel>
                                </Images>
                                <ImagesFilterControl>
                                    <LoadingPanel Url="~/App_Themes/PlasticBlue/Editors/Loading.gif">
                                    </LoadingPanel>
                                </ImagesFilterControl>
                                <Styles CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" 
                                    CssPostfix="PlasticBlue">
                                    <Header ImageSpacing="10px" SortingImageSpacing="10px">
                                    </Header>
                                </Styles>
                                <StylesEditors>
                                    <CalendarHeader Spacing="11px">
                                    </CalendarHeader>
                                    <ProgressBar Height="25px">
                                    </ProgressBar>
                                </StylesEditors>
                            </dx:ASPxGridView>
                        </DetailRow>
                    </Templates>
                </dx:ASPxGridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:PlayConnectionString %>" 
                    SelectCommand="Play_Pedido_Listar" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="lblFechaInicial" Name="FechaInicio" 
                            PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblFechaFinal" Name="FechaFin" 
                            PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="chkHabilitado" Name="b_Estado" 
                            PropertyName="Checked" Type="Boolean" />
                        <asp:ControlParameter ControlID="lblSucursal" Name="v_Descripcion" 
                            PropertyName="Text" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:PlayConnectionString %>" 
                    SelectCommand="Play_DetPedido_Listar" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:SessionParameter Name="n_IdPedido" SessionField="n_IdPedido" 
                            Type="Decimal" />
                    </SelectParameters>
                </asp:SqlDataSource>
            
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

