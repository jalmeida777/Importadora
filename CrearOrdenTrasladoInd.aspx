<%@ Page Title="Crear Orden de Traslado" Language="C#" MasterPageFile="~/Plantilla.master" AutoEventWireup="true" CodeFile="CrearOrdenTrasladoInd.aspx.cs" Inherits="CrearOrdenTrasladoInd" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.2, Version=11.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx1" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.2, Version=11.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.2, Version=11.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallback" tagprefix="dx1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script src="js/jquery.growl.js" type="text/javascript"></script>
<link href="css/jquery.growl.css" rel="stylesheet" type="text/css" />
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

    <style type="text/css">

.dxpControl_PlasticBlue
{
	padding: 0px;
}
.dxpControl_PlasticBlue
{
	font: 12px Tahoma;
	color: #5A83D0;
}
.dxpSummary_PlasticBlue
{
	color: #909090;
	white-space: nowrap;
	text-align: center;
	vertical-align: middle;
	padding: 0px 4px;
}
.dxpDisabled_PlasticBlue
{
	color: #B8B8B8;
	border-color: #B8B8B8;
	cursor: default;
}

.dxpDisabledButton_PlasticBlue
{
	text-decoration: none;
}
.dxpButton_PlasticBlue
{
	text-decoration: none;
	white-space: nowrap;
	text-align: center;
	vertical-align: middle;
}
.dxpCurrentPageNumber_PlasticBlue
{
	color: #FFFFFF;
	background-color: #5066AC;
	font-weight: normal;
	text-decoration: none;
	padding: 2px 3px 3px;
	white-space: nowrap;
}
.dxpPageNumber_PlasticBlue
{
	text-decoration: none;
	text-align: center;
	vertical-align: middle;
	padding: 0px 5px;
}
        .style1
        {
            width: 100%;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

                <div class="toolbar">
            <table width="100%"><tr><td width="65">
                
                <asp:ImageButton ID="btnGuardar" runat="server" ImageUrl="~/images/Guardar.jpg" 
                    onclick="btnGuardar_Click" OnClientClick="if (confirm('Seguro de guardar?')) { btnGuardar.disabled = false; return true; } else { return false; }" />
                
                </td>
                <td width="65">
                  
                    <asp:ImageButton ID="btnNuevo" runat="server" ImageUrl="~/images/Nuevo.jpg" 
                        onclick="btnNuevo_Click" />
                  
                </td>
                <td>
                   
                    <asp:ImageButton ID="btnSalir" runat="server" ImageUrl="~/images/Salir.jpg" 
                        onclick="btnSalir_Click" />
                   
                </td>
                </tr></table>
            </div>

 <table width="100%" runat="server" id="tblGeneral"
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
            <td colspan="4">
                &nbsp;</td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="4">
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="20pt" 
                    Text="Orden de Traslado N°:" ForeColor="#4C4C4C"></asp:Label>
                <asp:Label ID="lblNumero" runat="server" Font-Bold="True" Font-Size="20pt" 
                    ForeColor="#4C4C4C"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933" 
                width="120">
                <asp:Label ID="Label20" runat="server" Text="Sucursal Origen:" 
                    ForeColor="#4C4C4C"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                        <table cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td width="155">
                        <asp:DropDownList ID="ddlAlmacenOrigen" runat="server" 
                            CssClass="combo" 
                            Width="150px">
                        </asp:DropDownList>

                                </td>
                                <td>
                            <asp:ImageButton ID="ibEstablecerSucursalOrigen" runat="server" 
                                ImageUrl="~/images/auth_ok.png" onclick="ibEstablecerSucursal_Click" 
                                ToolTip="Establecer Sucursal" />
                                </td>
                            </tr>
                        </table>

                    </td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933;">
                <asp:Label ID="Label2" runat="server" Text="Fecha:" ForeColor="#4C4C4C"></asp:Label>

                    </td>
            <td style="padding-left: 5px">
                        <asp:TextBox ID="txtFechaInicial" runat="server" CssClass="inputsFecha"
                            MaxLength="10"></asp:TextBox>
                        <cc1:calendarextender 
                        ID="CalendarExtender1" 
                        runat="server" 
                        TargetControlID="txtFechaInicial" Format="dd/MM/yyyy">
                        </cc1:calendarextender>

                <asp:Label runat="server" Text="*" Font-Bold="True" Font-Size="10pt" 
                            ForeColor="#18AC85" ID="Label19"></asp:Label>

                    </td>
            <td style="padding-left: 5px">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933" 
                width="120">
                <asp:Label ID="Label21" runat="server" Text="Sucursal Destino:" 
                    ForeColor="#4C4C4C"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                        <table cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td width="155">
                        <asp:DropDownList ID="ddlAlmacenDestino" runat="server" 
                            CssClass="combo" 
                            Width="150px" Enabled="False">
                        </asp:DropDownList>

                                </td>
                                <td>
                            <asp:ImageButton ID="ibEstablecerSucursalDestino" runat="server" 
                                ImageUrl="~/images/auth_ok.png" onclick="ibEstablecerSucursalDestino_Click" 
                                ToolTip="Establecer Sucursal" Visible="False" />
                                </td>
                            </tr>
                        </table>

                    </td>
            <td>
                &nbsp;</td>
            <td style="padding-left: 5px">
                        &nbsp;</td>
            <td style="padding-left: 5px">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="4" 
                
                
                style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #CCCCCC;">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="4">
                <asp:LinkButton ID="lnkAgregarProducto" runat="server" Font-Bold="True" 
                                    ForeColor="#7C7BAD"
                    Enabled="False" onclick="lnkAgregarProducto_Click">Agregar Producto</asp:LinkButton>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="4">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" 
                    CssClass="grid" DataKeyNames="n_IdProducto">
                            <Columns>
                                <asp:BoundField HeaderText="Stock Actual" DataField="f_StockContable">
                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="v_CodigoInterno" HeaderText="Código">
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Producto" HeaderText="Producto" />
                                <asp:TemplateField HeaderText="Cantidad">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtCantidad" runat="server" AutoPostBack="True" 
                                    CssClass="inputNormalMoneda" MaxLength="5" 
                                    onkeypress="return ValidaEntero(event);" Width="45px"></asp:TextBox>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="footer" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
                
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="4">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="4">
                <asp:TextBox ID="txtObservacion" runat="server" Height="80px" 
                    TextMode="MultiLine" Width="380px" placeholder="Comentarios"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</div></td>
            <td width="15%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="15%">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td width="15%">
                &nbsp;</td>
        </tr>
    </table>

                <asp:Panel ID="panelProductos" runat="server" Height="400px" Visible="False" 
                    Width="100%">

                     <table width="100%" runat="server" id="Table1"
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
<dx1:ASPxGridView ID="gvBuscar" runat="server" AutoGenerateColumns="False" 
            CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue" 
            DataSourceID="SqlDataSource1" KeyFieldName="n_IdProducto" Width="100%" 
        EnableCallbackCompression="False" EnableCallBacks="False" 
        EnableRowsCache="False" EnableTheming="False" EnableViewState="False" 
                        onrowcommand="gvBuscar_RowCommand">
            <Columns>
                <dx1:gridviewdatatextcolumn FieldName="v_CodigoInterno" VisibleIndex="0" 
                    Caption="Código" Width="60px">
                    <Settings AutoFilterCondition="Equals" AllowAutoFilter="True" 
                        AllowAutoFilterTextInputTimer="False" />
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dx1:gridviewdatatextcolumn>
                             <dx1:gridviewdatatextcolumn 
                    VisibleIndex="2" Caption="Producto" FieldName="v_Descripcion">
                    <Settings AllowAutoFilter="True" AllowAutoFilterTextInputTimer="False" AutoFilterCondition="Contains" />
                    <DataItemTemplate>
                        <asp:LinkButton ID="lbProducto" runat="server" 
                            Text='<%# Bind("v_Descripcion") %>' CommandName="Seleccionar"></asp:LinkButton>
                    </DataItemTemplate>
                </dx1:gridviewdatatextcolumn>
                <dx1:gridviewdatatextcolumn VisibleIndex="3" 
                    Caption="Stock" FieldName="f_StockContable" Width="60px">
                    <Settings AllowAutoFilter="False" />
                    <CellStyle Font-Bold="False">
                    </CellStyle>
                </dx1:gridviewdatatextcolumn>
                <dx1:gridviewdatatextcolumn FieldName="f_Precio" VisibleIndex="4" 
                    Caption="Precio" Width="100px">
                    <PropertiesTextEdit DisplayFormatString="C">

                        
                    </PropertiesTextEdit>
                    <Settings AllowAutoFilter="True" AllowAutoFilterTextInputTimer="False" AllowGroup="False" AllowHeaderFilter="False" AllowSort="True" AutoFilterCondition="Equals" />
                    <CellStyle horizontalalign="Right">
                    </CellStyle>
                </dx1:gridviewdatatextcolumn>
   
            </Columns>
            <SettingsBehavior AllowGroup="False" AutoFilterRowInputDelay="0" />
            <SettingsPager ShowDefaultImages="False" PageSize="7">
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
                <LoadingPanelOnStatusBar 
                        Url="~/App_Themes/PlasticBlue/GridView/gvLoadingOnStatusBar.gif">
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
                    <asp:ImageButton ID="btnSalirBusqueda" runat="server" 
                        ImageUrl="~/images/Salir.jpg" onclick="btnSalirBusqueda_Click" />
</div></td>
            <td width="15%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="15%">
                &nbsp;</td>
            <td>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:PlayConnectionString %>" 
                    SelectCommand="Play_ProductoStock_BuscarConStock" 
                    SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlAlmacenOrigen" Name="n_IdAlmacen" 
                            PropertyName="SelectedValue" Type="Decimal" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
            <td width="15%">
                &nbsp;</td>
        </tr>
    </table>

                    
                </asp:Panel>

</asp:Content>

