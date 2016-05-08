<%@ Page Title="Stock Valorizado" Language="C#" MasterPageFile="~/Plantilla.master" AutoEventWireup="true" CodeFile="ListarStockValorizado.aspx.cs" Inherits="ListarStockValorizado" %>

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
            <td colspan="5">
                <h1 class="label">Stock Valorizado</h1></td>
        </tr>
        <tr>
            <td width="65" class="label">
                <asp:Label ID="Label1" runat="server" Text="Almacén:"></asp:Label>
            </td>
            <td width="205">
                <asp:DropDownList ID="ddlAlmacen" runat="server" Width="200px" CssClass="combo">
                </asp:DropDownList>
            </td>
            <td width="55" class="label">
                &nbsp;</td>
            <td width="205">
                <asp:CheckBox ID="chkEditar" runat="server" Visible="False" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    </div>


        <div class="toolbar">
            <table width="100%"><tr>
                <td width="65">
                  
                    <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/images/Buscar.jpg" />
                  
                </td>
                <td width="65">
                    <asp:ImageButton ID="btnSalir" runat="server" ImageUrl="~/images/Salir.jpg" 
                        onclick="btnSalir_Click" />
                </td>
                <td>
                   
                </td>
                </tr></table>
            </div>

    <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
        AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <img src="images/loading.gif" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <dx:ASPxGridView ID="gvStock" runat="server" AutoGenerateColumns="False" 
        CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue" 
        DataSourceID="SqlDataSource1" KeyFieldName="n_IdProducto" Width="100%" 
        EnableRowsCache="False" EnableViewState="False" 
        onhtmlrowprepared="gvStock_HtmlRowPrepared">
        <TotalSummary>
            <dx:ASPxSummaryItem FieldName="f_StockContable" ShowInGroupFooterColumn="Stock" 
                SummaryType="Sum" />
            <dx:ASPxSummaryItem FieldName="Total" ShowInGroupFooterColumn="Total S/." 
                SummaryType="Sum" ValueDisplayFormat="C" />
        </TotalSummary>
        <Columns>

            <dx:GridViewDataTextColumn Caption="Código" FieldName="v_CodigoInterno" 
                VisibleIndex="1" Width="60px">
                <Settings AllowAutoFilter="True" AllowAutoFilterTextInputTimer="False" 
                    AllowDragDrop="False" AllowGroup="False" AllowSort="True" 
                    AutoFilterCondition="Equals" />
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dx:GridViewDataTextColumn>

                        <dx:GridViewDataTextColumn Caption="Imágen" 
                ReadOnly="True" VisibleIndex="2" Width="65px">
                <Settings AllowAutoFilter="False" AllowAutoFilterTextInputTimer="False" 
                    AllowDragDrop="False" AllowGroup="False" AllowSort="False" />
                <DataItemTemplate>
                        <a href="javascript:void(0);" onclick="OnMoreInfoClick(this, '<%# Container.KeyValue %>')">
                            <asp:Image ID="Image1" runat="server" Height="60px" 
                                ImageUrl='<%# Bind("v_RutaImagen") %>' style="margin-right: 0px" Width="60px" />
                                </a>
                </DataItemTemplate>
            </dx:GridViewDataTextColumn>


            <dx:GridViewDataTextColumn Caption="Producto" FieldName="v_Descripcion" 
                VisibleIndex="3">
                <Settings AllowAutoFilter="True" AllowAutoFilterTextInputTimer="False" 
                    AllowDragDrop="False" AllowGroup="False" AllowSort="True" 
                    AutoFilterCondition="Contains" />
                <DataItemTemplate>
                    <asp:LinkButton ID="lbProducto" runat="server" 
                        Text='<%# Bind("v_Descripcion") %>'></asp:LinkButton>
                </DataItemTemplate>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Stock" FieldName="f_StockContable" 
                VisibleIndex="4" Width="100px">
                <Settings AllowAutoFilter="False" AllowAutoFilterTextInputTimer="False" 
                    AllowDragDrop="False" AllowGroup="False" AllowSort="True" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Precio S/." FieldName="f_Precio" 
                VisibleIndex="5" Width="100px">
                <PropertiesTextEdit DisplayFormatString="C">
                </PropertiesTextEdit>
                <Settings AllowAutoFilter="True" AllowAutoFilterTextInputTimer="False" 
                    AllowDragDrop="True" AllowGroup="False" AllowSort="True" 
                    AllowHeaderFilter="False" AutoFilterCondition="Equals" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Total S/." FieldName="Total" 
                ReadOnly="True" VisibleIndex="6" Width="100px">
                <PropertiesTextEdit DisplayFormatString="C">
                </PropertiesTextEdit>
                <Settings AllowAutoFilter="False" AllowAutoFilterTextInputTimer="False" 
                    AllowDragDrop="False" AllowGroup="False" AllowSort="True" />
            </dx:GridViewDataTextColumn>

        </Columns>
        <SettingsPager PageSize="5" ShowDefaultImages="False">
            <AllButton Text="All">
            </AllButton>
            <NextPageButton Text="Next &gt;">
            </NextPageButton>
            <PrevPageButton Text="&lt; Prev">
            </PrevPageButton>
        </SettingsPager>
        <Settings ShowFilterRow="True" ShowFooter="True" />
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
        SelectCommand="Play_Stock_Valorizado_Listar" 
        SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlAlmacen" Name="n_IdAlmacen" 
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
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

