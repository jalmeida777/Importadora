<%@ Page Title="Administración de Productos" Language="C#" MasterPageFile="~/Plantilla.master" AutoEventWireup="true" CodeFile="ListarProducto.aspx.cs" Inherits="ListarProducto" %>

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
                        <h1 class="label">
                            Administración de Productos</h1>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkEstado" runat="server" Checked="True" 
                                        Text="Ver Habilitados" />
                                </td>
                                <td align="right" width="70">
                                    &nbsp;</td>
                                <td align="right" width="70">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            </div>
                <div class="toolbar">
            <table width="100%"><tr><td width="65">
                
                <asp:ImageButton ID="btnConsultar" runat="server" ImageUrl="~/images/Buscar.jpg" 
                    onclick="btnConsultar_Click" />
                
                </td>
                <td width="65">
                  
                    <asp:ImageButton ID="btnNuevo" runat="server" ImageUrl="~/images/Nuevo.jpg" 
                        onclick="btnNuevo_Click" />
                  
                </td>
                <td>
                   
                    <asp:ImageButton ID="btnSalir" runat="server" ImageUrl="~/images/Salir.jpg" 
                        onclick="btnSalir_Click" />
                   
                    <asp:CheckBox ID="chkEditar" runat="server" Visible="False" />
                   
                </td>
                </tr></table>
            </div>


    <dx:ASPxPopupControl ID="popup" ClientInstanceName="popup" runat="server" AllowDragging="True"
        PopupHorizontalAlign="OutsideRight" HeaderText="Foto" 
                CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue" 
                SpriteCssFilePath="~/App_Themes/PlasticBlue/{0}/sprite.css" Height="200px" 
                MaxHeight="400px" MaxWidth="400px" MinHeight="200px" MinWidth="200px">
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
                
                           
                                       <dx:ASPxCallbackPanel ID="callbackPanel" ClientInstanceName="callbackPanel" runat="server"
                    Width="200px" Height="200px" OnCallback="callbackPanel_Callback" RenderMode="Table">
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

                
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:PlayConnectionString %>" 
                SelectCommand="Play_Producto_Listar" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="chkEstado" DefaultValue="" Name="b_Estado" 
                            PropertyName="Checked" Type="Boolean" />
                    </SelectParameters>
            </asp:SqlDataSource>
            <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" 
                CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue" 
                DataSourceID="SqlDataSource1" KeyFieldName="n_IdProducto" 
                onhtmlrowprepared="ASPxGridView1_HtmlRowPrepared" Width="100%" 
                EnableRowsCache="False" EnableViewState="False">
                <Columns>
                    <dx:GridViewDataTextColumn Caption="Código" FieldName="v_CodigoInterno" 
                        VisibleIndex="0" Width="70px">
                        <Settings AllowGroup="False" AutoFilterCondition="Equals" 
                            AllowAutoFilter="True" AllowAutoFilterTextInputTimer="False" AllowSort="True" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>

                                       <dx:GridViewDataTextColumn Caption="Imágen" 
                        ReadOnly="True" VisibleIndex="1" Width="65px">
                        <Settings AllowAutoFilter="False" AllowGroup="False" AllowSort="False" />
                        <DataItemTemplate>
                        <a href="javascript:void(0);" onclick="OnMoreInfoClick(this, '<%# Container.KeyValue %>')">
                            <asp:Image ID="Image1" runat="server" Height="60px" 
                                ImageUrl='<%# Bind("v_RutaImagen") %>' style="margin-right: 0px" Width="60px" />
                                </a>
                        </DataItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn Caption="Producto" FieldName="v_Descripcion" 
                        VisibleIndex="2">
                        <Settings AllowGroup="False" AutoFilterCondition="Contains" 
                            AllowAutoFilter="True" AllowAutoFilterTextInputTimer="False" AllowSort="True" />
                        <DataItemTemplate>
                            <asp:LinkButton ID="lbProducto" runat="server" 
                                Text='<%# Bind("v_Descripcion") %>'></asp:LinkButton>
                        </DataItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                    </dx:GridViewDataTextColumn>

                                        <dx:GridViewDataTextColumn Caption="Categoría" FieldName="Categoria" 
                        VisibleIndex="3" Width="100px">
                        <Settings AutoFilterCondition="Contains" AllowAutoFilter="True" 
                            AllowAutoFilterTextInputTimer="False" AllowGroup="True" AllowSort="True" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn Caption="Precio" FieldName="f_Precio" 
                        VisibleIndex="4" Width="100px">
                        <PropertiesTextEdit DisplayFormatString="C">
                        </PropertiesTextEdit>
                        <Settings AllowAutoFilter="False" AllowGroup="False" AllowSort="True" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </dx:GridViewDataTextColumn>
 

                </Columns>
                <SettingsPager ShowDefaultImages="False" PageSize="5">
                    <AllButton Text="All">
                    </AllButton>
                    <NextPageButton Text="Next &gt;">
                    </NextPageButton>
                    <PrevPageButton Text="&lt; Prev">
                    </PrevPageButton>
                </SettingsPager>
                <Settings ShowFilterRow="True" />
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

     
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

