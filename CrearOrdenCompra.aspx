<%@ Page Title="Orden de Compra" Language="C#" MasterPageFile="~/Plantilla.master" AutoEventWireup="true" CodeFile="CrearOrdenCompra.aspx.cs" Inherits="CrearOrdenCompra" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<script src="js/jquery.growl.js" type="text/javascript"></script>
<link href="css/jquery.growl.css" rel="stylesheet" type="text/css" />
        <style>
        .AutoExtender
        {
            font-family: Verdana, Helvetica, sans-serif;
            font-size: .8em;
            font-weight: normal;
            border: solid 1px #006699;
            line-height: 20px;
            padding: 10px;
            background-color: White;
            margin-left:10px;
            z-index:99;
        }
        .AutoExtenderList
        {
            border-bottom: dotted 1px #006699;
            cursor: pointer;
            color: Maroon;
        }
        .AutoExtenderHighlight
        {
            color: White;
            background-color: #006699;
            cursor: pointer;
        }
        #divwidth
        {
          width: 150px !important;    
        }
        #divwidth div
       {
        width: 150px !important;   
       }
 </style>

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
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="4">
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="20pt" 
                    Text="Orden de Compra N° " ForeColor="#4C4C4C"></asp:Label>
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
                <asp:Label ID="Label3" runat="server" Text="Sucursal:" ForeColor="#4C4C4C"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <asp:DropDownList ID="ddlAlmacen" runat="server" Width="150px" CssClass="combo">
                </asp:DropDownList>

                <asp:Label runat="server" Text="*" Font-Bold="True" Font-Size="10pt" 
                    ForeColor="#18AC85" ID="Label16"></asp:Label>

            </td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933" 
                width="100">
                <asp:Label ID="Label2" runat="server" Text="Fecha:" ForeColor="#4C4C4C"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                        <asp:TextBox ID="txtFechaInicial" runat="server" CssClass="inputsFecha"
                            MaxLength="10"></asp:TextBox>
                        <cc1:CalendarExtender 
                        ID="CalendarExtender1" 
                        runat="server" 
                        TargetControlID="txtFechaInicial" Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>

                <asp:Label runat="server" Text="*" Font-Bold="True" Font-Size="10pt" 
                            ForeColor="#18AC85" ID="Label19"></asp:Label>

                    </td>
            <td style="padding-left: 5px">
                        &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933">
                <asp:Label ID="Label6" runat="server" Text="Proveedor:"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <asp:DropDownList ID="ddlProveedor" runat="server" CssClass="combo" 
                    Width="200px">
                </asp:DropDownList>

                <asp:Label runat="server" Text="*" Font-Bold="True" Font-Size="10pt" 
                    ForeColor="#18AC85" ID="Label17"></asp:Label>

            </td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933">
                <asp:Label ID="Label5" runat="server" Text="Referencia:" ForeColor="#4C4C4C"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <asp:TextBox ID="txtReferencia" runat="server" CssClass="inputNormal" 
                    MaxLength="20"></asp:TextBox>
            </td>
            <td style="padding-left: 5px">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933">
                <asp:Label ID="Label7" runat="server" Text="Moneda:"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <asp:DropDownList ID="ddlMoneda" runat="server" CssClass="combo" Width="100px" 
                    AutoPostBack="True" onselectedindexchanged="ddlMoneda_SelectedIndexChanged">
                </asp:DropDownList>

                <asp:Label runat="server" Text="*" Font-Bold="True" Font-Size="10pt" 
                    ForeColor="#18AC85" ID="Label18"></asp:Label>

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
            <td>
                <asp:LinkButton ID="lnkAgregarProducto" runat="server" Font-Bold="True" 
                                    ForeColor="#7C7BAD" onclick="lnkAgregarProducto_Click">Agregar Producto</asp:LinkButton>
            </td>
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
            <td colspan="4">
                <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" 
                    CssClass="grid" onrowdeleting="gv_RowDeleting" DataKeyNames="n_IdProducto" 
                    ShowFooter="True">
                    <Columns>
                        <asp:TemplateField HeaderText="Cantidad">
                            <ItemTemplate>
                                <asp:TextBox ID="txtCantidad" runat="server" CssClass="inputNormalMoneda" 
                                    MaxLength="5" Width="45px" onkeypress="return ValidaEntero(event);" 
                                    AutoPostBack="True" ontextchanged="txtCantidad_TextChanged" 
                                    Text='<%# Bind("Cantidad") %>'></asp:TextBox>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle Width="50px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Producto" HeaderText="Producto" />
                        <asp:TemplateField HeaderText="Costo Total">
                            <ItemTemplate>
                                <asp:TextBox ID="txtCosto" runat="server" CssClass="inputNormalMoneda" 
                                    onkeypress="return ValidaNumeros(event);" Width="45px" AutoPostBack="True" 
                                    ontextchanged="txtCosto_TextChanged" 
                                    Text='<%# String.Format("{0:n2}", Eval("CostoTotal") ) %>'></asp:TextBox>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle Width="50px" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Costo Cambio Día S/." DataField="CostoCambio" 
                            DataFormatString="{0:n2}" >
                        <ItemStyle Width="50px" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Costo Cambio Play S/." DataField="CostoCambioPlay" 
                            DataFormatString="{0:n2}" >
                        <ItemStyle Width="50px" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Costo Unidad S/." DataField="CostoUnitario" 
                            DataFormatString="{0:n2}" >
                        <ItemStyle Width="50px" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Utilidad %">
                            <ItemTemplate>
                                <asp:TextBox ID="txtUtilidad" runat="server" CssClass="inputNormalMoneda" 
                                    MaxLength="3" Width="45px" onkeypress="return ValidaEntero(event);" 
                                    AutoPostBack="True" ontextchanged="txtUtilidad_TextChanged" 
                                    Text='<%# Bind("Utilidad") %>'></asp:TextBox>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle Width="50px" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="P.V.P. S/." DataField="Precio" 
                            DataFormatString="{0:n2}" >
                        <ItemStyle Width="50px" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="ibQuitar" runat="server" CommandName="delete" 
                                    ImageUrl="~/images/delete.gif" ToolTip="Quitar" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle CssClass="footer" />
                </asp:GridView>
                
                <cc1:RoundedCornersExtender ID="panelProductos_RoundedCornersExtender" 
                    runat="server" Color="LightGray" Enabled="True" Radius="10" 
                    TargetControlID="panelProductos" BorderColor="120, 120, 120">
                </cc1:RoundedCornersExtender>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="4">
                <table border="0" cellpadding="5" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td align="right" width="100">
                            <asp:Label ID="Label20" runat="server" Text="SubTotal:"></asp:Label>
                        </td>
                        <td align="right" width="20">
                            <asp:Label ID="lblSigno1" runat="server" Text="S/."></asp:Label>
                        </td>
                        <td align="right" width="100">
                            <asp:Label ID="lblSubTotal" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td align="right">
                            <asp:Label ID="Label21" runat="server" Text="I.G.V.:"></asp:Label>
                        </td>
                        <td align="right" width="20">
                            <asp:Label ID="lblSigno2" runat="server" Text="S/."></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblIgv" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td align="right" 
                            style="border-top-style: solid; border-top-width: 1px; border-top-color: #999999">
                            <asp:Label ID="Label22" runat="server" Font-Bold="True" Font-Size="12pt" 
                                Text="Total:"></asp:Label>
                        </td>
                        <td align="right" 
                            style="border-top-style: solid; border-top-width: 1px; border-top-color: #999999" 
                            width="20">
                            <asp:Label ID="lblSigno3" runat="server" Font-Bold="True" Font-Size="12pt" 
                                Text="S/."></asp:Label>
                        </td>
                        <td align="right" 
                            style="border-top-style: solid; border-top-width: 1px; border-top-color: #999999">
                            <asp:Label ID="lblTotal" runat="server" Font-Bold="True" Font-Size="12pt"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="2">
                <asp:TextBox ID="txtObservacion" runat="server" Height="80px" 
                    TextMode="MultiLine" Width="380px" placeholder="Comentarios"></asp:TextBox>
            </td>
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
<asp:Panel ID="panelProductos" runat="server" Height="100%" Width="100%" 
                    Visible="False" BackColor="LightGray">
                    <table width="100%" bgcolor="#E2E2E2" border="0" cellpadding="0" 
                        cellspacing="0">
                        <tr bgcolor="LightGray" 
                            style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #C7C7C7">
                            <td>
                                <table class="style1">
                                    <tr>
                                        <td width="30" style="padding-left: 5px">
                                            <asp:ImageButton ID="ibTodos" runat="server" Height="30px" Width="30px" 
                                                ImageUrl="~/images/home.png" onclick="ibTodos_Click" />
                                        </td>
                                        <td>
                                            <asp:Menu ID="MenuFamilia" runat="server" Orientation="Horizontal" 
                                                onmenuitemclick="MenuFamilia_MenuItemClick" RenderingMode="Table" 
                                                Font-Size="9pt">
                                                <DynamicHoverStyle Font-Underline="True" />
                                                <StaticHoverStyle Font-Underline="True" />
                                                <StaticMenuItemStyle ItemSpacing="5px" />
                                                <StaticMenuStyle HorizontalPadding="5px" />
                                                <StaticSelectedStyle BackColor="#6EC89B" Font-Bold="True" ForeColor="White" 
                                                    Height="15px" HorizontalPadding="5px" ItemSpacing="4px" VerticalPadding="7px" />
                                            </asp:Menu>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="right">
                                <asp:TextBox ID="txtBuscar" runat="server" CssClass="inputsProducto" 
                                    Width="150px" placeholder="Buscar Productos" AutoPostBack="True" 
                                    ontextchanged="txtBuscar_TextChanged"></asp:TextBox>
                            </td>
                            <td align="right" style="padding-right: 12px" width="22">
                                <asp:ImageButton ID="ibCerrarProductos" runat="server" 
                                    ImageUrl="~/images/close.png" onclick="ibCerrarProductos_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" 
                                
                                style="border-bottom-style: solid; border-bottom-width: 3px; border-bottom-color: #6EC89B; padding-right: 7px; padding-left: 7px; border-top-style: solid; border-top-width: 1px; border-top-color: #C7C7C7;" 
                                bgcolor="#E2E2E2" height="30">
                                <asp:Menu ID="MenuSubFamilia" runat="server" Orientation="Horizontal" 
                                    RenderingMode="Table" Font-Size="9pt" 
                                    onmenuitemclick="MenuSubFamilia_MenuItemClick">
                                    <StaticHoverStyle Font-Underline="True" />
                                    <StaticMenuItemStyle ItemSpacing="5px" />
                                    <StaticMenuStyle HorizontalPadding="5px" />
                                    <StaticSelectedStyle BackColor="#6EC89B" Font-Bold="True" ForeColor="White" 
                                        Height="15px" HorizontalPadding="5px" ItemSpacing="4px" VerticalPadding="7px" />
                                </asp:Menu>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Panel ID="Panel1" runat="server" Height="600px" ScrollBars="Vertical">
                                    <table width="100%" 
                                        style="background-image: url('images/form_sheetbg.png'); background-repeat: repeat; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #ddd;">
                                        <tr>
                                            <td width="20">
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td width="20">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td width="20">
                                                &nbsp;</td>
                                            <td>
                                                <asp:DataList ID="gvProductos" runat="server" 
                                                    onitemcommand="gvProductos_ItemCommand" RepeatColumns="7" ShowFooter="False" 
                                                    ShowHeader="False" Width="100%" DataKeyField="n_IdProducto">
                                                    <ItemTemplate>
                                                        <table style="position: relative">
                                                            <tr>
                                                                <td height="100">
                                                                    <div align="right" 
                                                                        style="padding: 2px 3px 1px 3px; background-color: #7F82AC; height: 15px; right: 8px; top: 8px; color: #FFFFFF; float: right; position: absolute;">
                                                                        <asp:Label ID="lblPrecio" runat="server" Font-Bold="True" Font-Size="8pt" 
                                                                            ForeColor="White" Text='<%# String.Format("{0:C}", Eval("f_Costo") ) %>'></asp:Label>
                                                                    </div>
                                                                    <asp:ImageButton ID="ibImagen" runat="server" BorderColor="#E2E2E2" 
                                                                        BorderStyle="Solid" BorderWidth="1px" CommandName="AgregarProducto" 
                                                                        Height="100px" ImageUrl='<%# Bind("v_RutaImagen") %>' Width="100px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td height="35" valign="top">
                                                                    <asp:Label ID="lblDescripcion" runat="server" Font-Size="8pt" 
                                                                        Text='<%# Bind("v_Descripcion") %>' Width="100px"></asp:Label>
                                                                    <asp:Label ID="lblPrecio2" runat="server" Text='<%# Bind("f_Costo") %>' 
                                                                        Visible="False"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </td>
                                            <td width="20">
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
</asp:Content>

