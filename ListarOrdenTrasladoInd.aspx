<%@ Page Title="" Language="C#" MasterPageFile="~/Plantilla.master" AutoEventWireup="true" CodeFile="ListarOrdenTrasladoInd.aspx.cs" Inherits="ListarOrdenTrasladoInd" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<%@ Register assembly="DevExpress.Web.v11.2, Version=11.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallback" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.2, Version=11.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.2, Version=11.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.2, Version=11.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="divBusqueda">
                <table width="100%">
                    <tr>
                        <td colspan="9">
                            <h1 class="label">
                                Orden de Traslado</h1>
                        </td>
                    </tr>
                    <tr>
                        <td class="label" width="90">
                            <asp:Label ID="Label1" runat="server" Font-Bold="False" Text="Fecha Inicial:"></asp:Label>
                        </td>
                        <td width="110">
                            <asp:TextBox ID="txtFechaInicial" runat="server" CssClass="inputsFecha" 
                                MaxLength="10"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                                TargetControlID="txtFechaInicial">
                            </cc1:CalendarExtender>
                        </td>
                        <td class="label" width="85">
                            <asp:Label ID="Label2" runat="server" Font-Bold="False" Text="Fecha Final:"></asp:Label>
                        </td>
                        <td width="110">
                            <asp:TextBox ID="txtFechaFinal" runat="server" CssClass="inputsFecha" 
                                MaxLength="10"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtFechaFinal_CalendarExtender" runat="server" 
                                Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaFinal">
                            </cc1:CalendarExtender>
                        </td>
                        <td width="155">
                            <asp:CheckBox ID="chkHabilitado" runat="server" Checked="True" 
                                Font-Bold="False" Text="Habilitados" />
                        </td>
                        <td width="110">
                            &nbsp;</td>
                        <td align="right">
                            <asp:Label ID="lblFechaInicial" runat="server" Visible="False"></asp:Label>
                        </td>
                        <td align="right" width="70">
                            <asp:Label ID="lblFechaFinal" runat="server" Visible="False"></asp:Label>
                        </td>
                        <td align="right" width="70">
                            &nbsp;</td>
                    </tr>
                </table>
            </div>
            <div class="toolbar">
                <table width="100%">
                    <tr>
                        <td width="65">
                            <asp:ImageButton ID="btnConsultar" runat="server" 
                                ImageUrl="~/images/Buscar.jpg" onclick="btnConsultar_Click" ToolTip="Buscar" />
                        </td>
                        <td width="65">
                            <asp:ImageButton ID="btnNuevo" runat="server" ImageUrl="~/images/Nuevo.jpg" 
                                onclick="btnNuevo_Click" ToolTip="Nuevo" />
                        </td>
                        <td>
                            <asp:ImageButton ID="btnSalir" runat="server" ImageUrl="~/images/Salir.jpg" 
                                onclick="btnSalir_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <img src="images/loading.gif" />
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:Panel ID="Panel1" runat="server" Height="600px" ScrollBars="Vertical" 
                Width="100%">
                <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" 
                    CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue" 
                    DataSourceID="SqlDataSource1" EnableCallbackCompression="False" 
                    EnableCallBacks="False" EnableRowsCache="False" EnableTheming="False" 
                    EnableViewState="False" KeyFieldName="i_IdOrdenTraslado" Width="100%">
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="N° Orden de Traslado" 
                            FieldName="v_NumeroOrdenTraslado" VisibleIndex="0" Width="130px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Fecha" FieldName="d_FechaEmision" 
                            VisibleIndex="1" Width="150px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Sucursal Origen" FieldName="Origen" 
                            VisibleIndex="2" Width="100px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Creado Por" FieldName="v_Nombre" 
                            VisibleIndex="4" Width="150px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Observación" FieldName="t_Observacion" 
                            VisibleIndex="5">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Sucursal Destino" FieldName="Destino" 
                            VisibleIndex="3" Width="100px">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsBehavior AllowGroup="False" AutoFilterRowInputDelay="0" />
                    <SettingsPager PageSize="5" ShowDefaultImages="False">
                        <AllButton Text="All">
                        </AllButton>
                        <NextPageButton Text="Next &gt;">
                        </NextPageButton>
                        <PrevPageButton Text="&lt; Prev">
                        </PrevPageButton>
                    </SettingsPager>
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
                            <dx1:ASPxGridView ID="gvDocumento" runat="server" AutoGenerateColumns="False" 
                                CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue" 
                                DataSourceID="SqlDataSource2" EnableCallbackCompression="False" 
                                EnableCallBacks="False" EnableRowsCache="False" EnableTheming="False" 
                                EnableViewState="False" KeyFieldName="v_NumeroDocumento" 
                                Width="100%" 
                                onbeforeperformdataselect="gvDocumento_BeforePerformDataSelect" 
                                Caption="Documentos">
                                <SettingsBehavior AllowGroup="False" AutoFilterRowInputDelay="0" />
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="Número de Documento" 
                                        FieldName="v_NumeroDocumento" VisibleIndex="0" Width="130px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Tipo de Documento" 
                                        FieldName="TipoDocumento" VisibleIndex="1" Width="130px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Sucursal Destino" FieldName="Almacen" 
                                        VisibleIndex="2">
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <SettingsBehavior AllowGroup="False" AutoFilterRowInputDelay="0" />
                                <SettingsPager PageSize="7" ShowDefaultImages="False">
                                    <AllButton Text="All">
                                    </AllButton>
                                    <NextPageButton Text="Next &gt;">
                                    </NextPageButton>
                                    <PrevPageButton Text="&lt; Prev">
                                    </PrevPageButton>
                                </SettingsPager>
                                <SettingsCookies StoreFiltering="False" />
                                <SettingsCookies StoreFiltering="False" />
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
                            </dx1:ASPxGridView>
                            <dx:ASPxGridView ID="gvProducto" runat="server" AutoGenerateColumns="False" 
                                Caption="Productos" CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" 
                                CssPostfix="PlasticBlue" DataSourceID="SqlDataSource3" 
                                KeyFieldName="n_IdProducto" 
                                onbeforeperformdataselect="gvProducto_BeforePerformDataSelect">
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="Producto" FieldName="v_Descripcion" 
                                        VisibleIndex="0">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Cantidad" FieldName="i_Cantidad" 
                                        VisibleIndex="1" Width="50px">
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <SettingsPager ShowDefaultImages="False">
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
                    SelectCommand="Play_OrdenTraslado_Listar" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="lblFechaInicial" Name="FechaInicio" 
                            PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblFechaFinal" Name="FechaFin" 
                            PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="chkHabilitado" Name="b_Estado" 
                            PropertyName="Checked" Type="Boolean" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:PlayConnectionString %>" 
                    SelectCommand="Play_OrdenTrasladoDocumento_Listar" 
                    SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:SessionParameter Name="i_IdOrdenTraslado" SessionField="i_IdOrdenTraslado" 
                            Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:PlayConnectionString %>" 
                    SelectCommand="Importadora_OrdenTrasladoDetalle_Listar" 
                    SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:SessionParameter Name="i_IdOrdenTraslado" SessionField="i_IdOrdenTraslado" 
                            Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

