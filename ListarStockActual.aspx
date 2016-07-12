<%@ Page Title="Stock Actual" Language="C#" MasterPageFile="~/Plantilla.master" AutoEventWireup="true" CodeFile="ListarStockActual.aspx.cs" Inherits="Procesos_ListarStockActual" %>
<%@ MasterType virtualpath="~/Plantilla.master" %>
<%@ Register Assembly="DevExpress.Web.v11.2, Version=11.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v11.2, Version=11.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.2, Version=11.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.2, Version=11.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.2, Version=11.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallbackPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.2, Version=11.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
   
   <style type="text/css">
        .InfoTable td
        {
            padding: 0 4px;
            vertical-align: top;
        }
    </style>
    <script type="text/javascript">
        var keyValue;
        function OnMoreInfoClick(element, key) {
            callbackPanel.SetContentHtml("");
            popup.ShowAtElement(element);
            keyValue = key;
        }
        function popup_Shown(s, e) {
            callbackPanel.PerformCallback(keyValue);
        }
    </script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<div class="divBusqueda">
    <table width="100%" cellpadding="3" cellspacing="3">
        <tr>
            <td>
                <h1 class="label">Productos</h1></td>
        </tr>
    </table>
    </div>


        <div class="toolbar">
            <table width="100%"><tr><td width="95">
                
                <asp:ImageButton ID="btnNuevo" runat="server" 
                    ImageUrl="~/images/btnNuevo_New.png" onclick="btnNuevo_Click" />
                
                </td>
                <td width="95">
                    <asp:ImageButton ID="btnImprimir" runat="server" 
                        ImageUrl="~/images/btnImprimir_New.png" onclick="btnImprimir_Click" />
                </td>
                <td width="65">
                    &nbsp;</td>
                <td width="95">
                    <asp:ImageButton ID="btnNotaIngreso" runat="server" 
                        ImageUrl="~/images/btnNotaIngreso_New.png" onclick="btnNotaIngreso_Click" />
                </td>
                <td width="95">
                    <asp:ImageButton ID="btnNotaSalida" runat="server" 
                        ImageUrl="~/images/btnNotaSalida_New.png" onclick="btnNotaSalida_Click" />
                </td>
                <td width="95">
                    <asp:ImageButton ID="btnTraslado" runat="server" 
                        ImageUrl="~/images/btnTraslado_New.png" onclick="btnTraslado_Click" />
                </td>
                <td align="left">
                   
                    <asp:ImageButton ID="btnSalir" runat="server" 
                        ImageUrl="~/images/btnSalir_New.png" onclick="btnSalir_Click" />
                   
                </td>
                </tr></table>
            </div>


        <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" 
            CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue" 
            DataSourceID="SqlDataSource1" KeyFieldName="n_IdProducto" Width="100%" 
            onhtmlrowprepared="ASPxGridView1_HtmlRowPrepared" 
        EnableCallbackCompression="False" EnableCallBacks="False" 
        EnableRowsCache="False" EnableTheming="False" EnableViewState="False">
            <Columns>
                <dx:GridViewDataTextColumn FieldName="SOTANO" VisibleIndex="4" 
                    Caption="Sótano" Width="100px">
                    <Settings AllowAutoFilter="False" AllowSort="True" />
                    <DataItemTemplate>
                        <asp:LinkButton ID="lbSotano" runat="server" Text='<%# Bind("Sotano") %>'></asp:LinkButton>
                        <asp:Label ID="lblIdSotano" runat="server" Text="1" Visible="False"></asp:Label>
                    </DataItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle Font-Bold="False" HorizontalAlign="Center" Font-Size="12pt">
                    </CellStyle>
                    <Settings AllowAutoFilter="False" />
                    <CellStyle Font-Bold="False">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
   
                <dx:GridViewDataTextColumn FieldName="v_CodigoInterno" VisibleIndex="0" 
                    Caption="Código" Width="60px">
                    <Settings AutoFilterCondition="BeginsWith" AllowAutoFilter="True" 
                        AllowAutoFilterTextInputTimer="False" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center" Font-Size="12pt">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                             <dx:GridViewDataTextColumn ReadOnly="True" 
                    VisibleIndex="1" Caption="Imágen" Width="65px">
                    <Settings AllowAutoFilter="False" AllowSort="False" />
                    <DataItemTemplate>
                        <a href="javascript:void(0);" onclick="OnMoreInfoClick(this, '<%# Container.KeyValue %>')">
                            <asp:Image ID="Image1" runat="server" Height="60px" 
                                ImageUrl='<%# Bind("v_RutaImagen") %>' style="margin-right: 0px" Width="60px" />
                                </a>
                    </DataItemTemplate>
                                 <HeaderStyle HorizontalAlign="Center" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn VisibleIndex="2" 
                    Caption="Producto" FieldName="Producto">
                    <Settings AutoFilterCondition="Contains" 
                        AllowAutoFilterTextInputTimer="False" AllowAutoFilter="True" />
                    <DataItemTemplate>
                        <asp:LinkButton ID="lbProducto" runat="server" Text='<%# Bind("Producto") %>' 
                            Font-Size="14pt"></asp:LinkButton>
                    </DataItemTemplate>
                    <CellStyle>
                        <Paddings PaddingLeft="10px" />
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="f_Precio" VisibleIndex="3" 
                    Caption="Precio" Width="100px">
                    <PropertiesTextEdit DisplayFormatString="C">
                    </PropertiesTextEdit>
                    <Settings AllowAutoFilter="True" AllowAutoFilterTextInputTimer="False" 
                        AllowGroup="False" AllowHeaderFilter="False" AllowSort="True" 
                        AutoFilterCondition="Equals" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Right" Font-Size="12pt">
                        <Paddings PaddingRight="7px" />
                    </CellStyle>
                </dx:GridViewDataTextColumn>
   
                <dx:GridViewDataTextColumn Caption="Semi Sótano" FieldName="SEMISOTANO" 
                    VisibleIndex="5" Width="100px">
                    <Settings AllowAutoFilter="False" AllowSort="True" />
                    <DataItemTemplate>
                        <asp:LinkButton ID="lbSemiSotano" runat="server" 
                            Text='<%# Bind("SemiSotano") %>'></asp:LinkButton>
                        <asp:Label ID="lblIdSemiSotano" runat="server" Text="2" Visible="False"></asp:Label>
                    </DataItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center" Font-Size="12pt">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Tercer Piso" FieldName="TERCERPISO" 
                    VisibleIndex="6" Width="100px">
                    <Settings AllowAutoFilter="False" AllowSort="True" />
                    <DataItemTemplate>
                        <asp:LinkButton ID="lbTercerPiso" runat="server" 
                            Text='<%# Bind("TercerPiso") %>'></asp:LinkButton>
                        <asp:Label ID="lblIdTercerPiso" runat="server" Text="3" Visible="False"></asp:Label>
                    </DataItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center" Font-Size="12pt">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Full Tienda" FieldName="FULLTIENDA" 
                    VisibleIndex="7" Width="100px">
                    <Settings AllowAutoFilter="False" AllowSort="True" />
                    <DataItemTemplate>
                        <asp:LinkButton ID="lbFullTienda" runat="server" 
                            Text='<%# Bind("FullTienda") %>'></asp:LinkButton>
                        <asp:Label ID="lblIdFullTienda" runat="server" Text="4" Visible="False"></asp:Label>
                    </DataItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center" Font-Size="12pt">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Total" FieldName="TOTAL" VisibleIndex="9" 
                    Width="100px">
                    <Settings AllowAutoFilter="False" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center" Font-Size="12pt">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
            </Columns>
            <SettingsBehavior AllowGroup="False" AutoFilterRowInputDelay="0" />
            <SettingsPager ShowDefaultImages="False" PageSize="5">
                <AllButton Text="All">
                </AllButton>
                <NextPageButton Text="Next &gt;">
                </NextPageButton>
                <PrevPageButton Text="&lt; Prev">
                </PrevPageButton>
            </SettingsPager>
            <Settings ShowFilterRow="True" />
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
        </dx:ASPxGridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:PlayConnectionString %>" 
            SelectCommand="Play_StockGlobal_Listar" 
        SelectCommandType="StoredProcedure">
        </asp:SqlDataSource>

    <dx:ASPxPopupControl ID="popup" runat="server" AllowDragging="True" 
        ClientInstanceName="popup" 
        CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue" 
        HeaderText="Foto" Height="200px" MaxHeight="400px" MaxWidth="400px" 
        MinHeight="200px" MinWidth="200px" PopupHorizontalAlign="OutsideRight" 
        SpriteCssFilePath="~/App_Themes/PlasticBlue/{0}/sprite.css">
        <CloseButtonStyle>
            <Paddings Padding="0px" />
        </CloseButtonStyle>
        <ContentStyle>
            <BorderBottom BorderColor="#E0E0E0" BorderStyle="Solid" BorderWidth="1px" />
        </ContentStyle>
        <HeaderStyle>
        <Paddings PaddingBottom="4px" PaddingLeft="10px" PaddingRight="4px" 
            PaddingTop="4px" />
        </HeaderStyle>
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxCallbackPanel ID="callbackPanel" runat="server" 
                    ClientInstanceName="callbackPanel" Height="200px" 
                    OnCallback="callbackPanel_Callback" RenderMode="Table" Width="200px">
                    <panelcollection>
                        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                            <table class="style1">
                                <tr>
                                    <td>
                                        <asp:Image ID="ImagenGrande" runat="server" Height="200px" Width="200px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblCodigo" runat="server" Font-Bold="True" ForeColor="#FF6600"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblProducto" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </dx:PanelContent>
                    </panelcollection>
                </dx:ASPxCallbackPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
        <LoadingPanelImage Url="~/App_Themes/PlasticBlue/Web/dvLoading.gif">
        </LoadingPanelImage>
        <ClientSideEvents Shown="popup_Shown" />
    </dx:ASPxPopupControl>

    </ContentTemplate>
    </asp:UpdatePanel>

    </asp:Content>

