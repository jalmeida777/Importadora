<%@ Page Title="Pedido" Language="C#" MasterPageFile="~/Plantilla.master" AutoEventWireup="true" CodeFile="CrearPedido.aspx.cs" Inherits="CrearPedido" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

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

         function OnClienteSeleccionado(source, eventArgs) {
             var hdnValueID = "<%= hdnValue.ClientID %>";

             document.getElementById(hdnValueID).value = eventArgs.get_value();
             __doPostBack(hdnValueID, "");
         }


 
 </script>

  <style type="text/css">
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
      .style1
      {
          width: 100%;
      }
      .style2
      {
          height: 20px;
      }
 </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:hiddenfield id="hdnValue" onvaluechanged="hdnValue_ValueChanged" runat="server"/>

                <div class="toolbar" id="toolbar" runat ="server">
            <table width="100%"><tr><td width="65">
                
                <asp:ImageButton ID="btnGuardar" runat="server" ImageUrl="~/images/Guardar.jpg" 
                    onclick="btnGuardar_Click" OnClientClick="if (confirm('Seguro de guardar?')) { btnGuardar.disabled = false; return true; } else { return false; }" />
                
                </td>
                <td width="65">
                  
                    <asp:ImageButton ID="btnNuevo" runat="server" ImageUrl="~/images/Nuevo.jpg" 
                        onclick="btnNuevo_Click" />
                  
                </td>
                <td width="65">
                   
                    <asp:ImageButton ID="btnAnular" runat="server" ImageUrl="~/images/Anular.jpg" 
                        Visible="False" onclick="btnAnular_Click" OnClientClick="if (confirm('Seguro de anular?')) { btnAnular.disabled = false; return true; } else { return false; }" />
                   
                </td>
                <td width="65">
                   
                    <asp:ImageButton ID="btnImprimir" runat="server" 
                        ImageUrl="~/images/Imprimir.jpg" Visible="False" 
                        onclick="btnImprimir_Click" />

                        <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" 
                    ConfirmText="¿Seguro de imprimir?" Enabled="True" 
                    TargetControlID="btnImprimir">
                </cc1:ConfirmButtonExtender>
                   
                </td>
                <td width="65" align="left">
                   
                    <asp:ImageButton ID="btnSalir" runat="server" ImageUrl="~/images/Salir.jpg" 
                        onclick="btnSalir_Click" />
                   
                </td>
                <td>
                   
                    &nbsp;</td>
                </tr></table>
            </div>

 <table width="100%" runat="server" id="tblGeneral"
        style="background-image: url('images/form_sheetbg.png'); background-repeat: repeat; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #ddd;">
        <tr>
            <td width="5%">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td width="5%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="5%">
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
                <asp:Label ID="Label1" runat="server" Font-Bold="True" 
                    Text="Pedido N°:" ForeColor="#4C4C4C" Font-Size="20pt"></asp:Label>
                &nbsp;<asp:Label ID="lblNumeroPedido" runat="server" Font-Bold="True" 
                    ForeColor="#4C4C4C" Font-Size="20pt"></asp:Label>
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
                <asp:Label ID="lblIdPedido" runat="server" Visible="False"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933" 
                width="100">
                <asp:Label ID="Label6" runat="server" Text="Cliente"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <asp:TextBox ID="txtCliente" runat="server" CssClass="inputNormal" 
                    Width="200px" AutoPostBack="True"></asp:TextBox>
                <cc1:AutoCompleteExtender ID="txtCliente_AutoCompleteExtender" runat="server" 
                    CompletionInterval="100" CompletionListCssClass="AutoExtender" 
                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight" 
                    CompletionListItemCssClass="AutoExtenderList" DelimiterCharacters="" 
                    Enabled="True" MinimumPrefixLength="2" 
                    ServiceMethod="BuscarClientes" ServicePath="" TargetControlID="txtCliente" 
                    onclientitemselected="OnClienteSeleccionado" 
                    ShowOnlyCurrentWordInCompletionListItem="True">
                </cc1:AutoCompleteExtender>
&nbsp;<asp:ImageButton ID="btnCliente" runat="server" ImageUrl="~/images/proveedor.gif" 
                    ToolTip="Editar Cliente" Visible="False" onclick="btnCliente_Click" />

            </td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933" 
                width="70">
                <asp:Label ID="Label2" runat="server" Text="Fecha" ForeColor="#4C4C4C"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                        <asp:TextBox ID="txtFechaInicial" runat="server" CssClass="inputsFecha"
                            MaxLength="10"></asp:TextBox>
                        <cc1:CalendarExtender 
                        ID="CalendarExtender1" 
                        runat="server" 
                        TargetControlID="txtFechaInicial" Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>

                    </td>
            <td style="padding-left: 5px">
                        &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933">
                <asp:Label ID="Label23" runat="server" Text="Forma de Pago"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <asp:DropDownList ID="ddlFormaPago" runat="server" CssClass="combo" 
                    Width="220px" AutoPostBack="True" 
                    onselectedindexchanged="ddlFormaPago_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933">
                <asp:Label ID="Label24" runat="server" Text="Moneda"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <asp:DropDownList ID="ddlMoneda" runat="server" AutoPostBack="True" 
                    CssClass="combo" Enabled="False" 
                    onselectedindexchanged="ddlMoneda_SelectedIndexChanged" Width="100px">
                </asp:DropDownList>

                    </td>
            <td style="padding-left: 5px">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933">
                <asp:Label ID="Label40" runat="server" Text="Banco"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <asp:DropDownList ID="ddlBanco" runat="server" CssClass="combo" Enabled="False" 
                    Width="150px">
                </asp:DropDownList>

            </td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933">
                <asp:Label ID="Label42" runat="server" Text="Sucursal"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <table cellpadding="0" cellspacing="0" class="style1">
                    <tr>
                        <td width="160">
                <asp:DropDownList ID="ddlTienda" runat="server" CssClass="combo" Width="150px" 
                                ClientIDMode="Static">
                </asp:DropDownList>
                        </td>
                        <td>
                            <asp:ImageButton ID="ibEstablecerSucursal" runat="server" 
                                ImageUrl="~/images/auth_ok.png" 
                                ToolTip="Establecer Sucursal"  
                                ClientIDMode="Static" CausesValidation="False" 
                                onclick="ibEstablecerSucursal_Click"/>
                        </td>
                    </tr>
                </table>
            </td>
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
                                    ForeColor="#7C7BAD" onclick="lnkAgregarProducto_Click" 
                     ClientIDMode="Static" Enabled="False">Agregar Producto</asp:LinkButton>
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
                    CssClass="grid" onrowdeleting="gv_RowDeleting" DataKeyNames="n_IdProducto">
                    <Columns>
                        <asp:TemplateField HeaderText="Cantidad">
                            <ItemTemplate>
                                <asp:TextBox ID="txtCantidad" runat="server" CssClass="inputNormalMoneda" 
                                    MaxLength="5" Width="45px" onkeypress="return ValidaEntero(event);" 
                                    AutoPostBack="True" ontextchanged="txtCantidad_TextChanged" 
                                    Text='<%# Bind("i_Cantidad") %>'></asp:TextBox>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Stock">
                            <ItemTemplate>
                                <asp:Label ID="lblStock" runat="server" Text='<%# Bind("Stock") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Stock") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle Font-Bold="True" ForeColor="#FF6600" HorizontalAlign="Right" 
                                Width="50px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Codigo" HeaderText="Código">
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Producto" HeaderText="Producto" />
                        <asp:TemplateField HeaderText="Precio">
                            <ItemTemplate>
                                <asp:TextBox ID="txtPrecio" runat="server" CssClass="inputNormalMoneda" 
                                    Text='<%# Bind("f_PrecioUnitario", "{0:n2}") %>' Width="60px"
                                    onkeypress="return ValidaNumeros(event);" AutoPostBack="True" 
                                    ontextchanged="txtPrecio_TextChanged"></asp:TextBox>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" 
                                    Text='<%# Bind("f_PrecioUnitario") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Total" 
                            DataFormatString="{0:n2}" DataField="f_PrecioTotal" >
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
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
                        <td align="left">
                            <table width="100%">
                                <tr>
                                    <td align="left" 
                                        style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933" 
                                        width="120">
                                        <asp:Label ID="Label50" runat="server" Text="Vendedor:"></asp:Label>
                                    </td>
                                    <td align="left" style="padding-left: 5px">
                                        <asp:DropDownList ID="ddlVendedor" runat="server" CssClass="combo" 
                                            Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
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
                        <td align="left">
                            &nbsp;</td>
                        <td align="right" width="100">
                            <asp:Label ID="Label52" runat="server" Text="Descuento:"></asp:Label>
                        </td>
                        <td align="right" width="20">
                            <asp:Label ID="lblSigno6" runat="server" Text="S/."></asp:Label>
                        </td>
                        <td align="right" width="100">
                            <asp:TextBox ID="txtDescuento" runat="server" AutoPostBack="True" 
                                CssClass="inputNormalMoneda" ontextchanged="txtDescuento_TextChanged" onkeypress="return ValidaNumeros(event);"
                                Width="60px">0.00</asp:TextBox>
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
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td align="right" 
                            style="border-top-style: solid; border-top-width: 1px; border-top-color: #999999">
                            <asp:Label ID="Label25" runat="server" Text="Pagó con:"></asp:Label>
                        </td>
                        <td align="right" 
                            style="border-top-style: solid; border-top-width: 1px; border-top-color: #999999" 
                            width="20">
                            <asp:Label ID="lblSigno4" runat="server" Text="S/."></asp:Label>
                        </td>
                        <td align="right" 
                            style="border-top-style: solid; border-top-width: 1px; border-top-color: #999999">

                <asp:Label runat="server" Text="*" Font-Bold="True" Font-Size="10pt" 
                            ForeColor="#18AC85" ID="Label29"></asp:Label>

                            <asp:TextBox ID="txtPago" runat="server" AutoPostBack="True" 
                                CssClass="inputNormalMoneda" Width="60px" 
                                onkeypress="return ValidaNumeros(event);" ontextchanged="txtPago_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td align="right">
                            <asp:Label ID="Label26" runat="server" Text="Vuelto:"></asp:Label>
                        </td>
                        <td align="right" 
                            width="20">
                            <asp:Label ID="lblSigno5" runat="server" Text="S/."></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblVuelto" runat="server"></asp:Label>
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
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="4">
                <table width="300">
                    <tr>
                        <td rowspan="2" width="60">
                            <asp:ImageButton ID="ibUsuarioRegistro" runat="server" Height="50px" 
                                ImageUrl="~/images/face.jpg" Width="50px" />
                        </td>
                        <td>
                            Creado por:
                            <asp:Label ID="lblUsuarioRegistro" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblFechaRegistro" runat="server"></asp:Label>
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
</div>

</td>
            <td width="5%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="5%">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td width="5%">
                &nbsp;</td>
        </tr>
    </table>
    
     <table width="100%" runat="server" id="tblCliente"
        
        style="background-image: url('images/form_sheetbg.png'); background-repeat: repeat; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #ddd;" 
        visible="False">
        <tr>
            <td width="5%" class="style2">
                </td>
            <td class="style2">
                </td>
            <td width="5%">
                </td>
        </tr>
        <tr>
            <td width="5%">
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
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td colspan="2">
                <asp:Label ID="Label39" runat="server" Font-Bold="True" Font-Size="20pt" 
                    Text="Cliente" ForeColor="#4C4C4C"></asp:Label>
                </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 180px;">
                <asp:Label ID="Label30" runat="server" Text="Nombre:"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <asp:TextBox ID="txtNombre" runat="server" CssClass="inputNormal" 
                    MaxLength="50" Width="200px"></asp:TextBox>

                <asp:Label runat="server" Text="*" Font-Bold="True" Font-Size="10pt" 
                            ForeColor="#18AC85" ID="Label41"></asp:Label>

            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933">
                <asp:Label ID="Label31" runat="server" Text="Número de Documento:"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <asp:TextBox ID="txtNumeroDocumento" runat="server" CssClass="inputNormal" 
                    MaxLength="11"></asp:TextBox>
            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933">
                <asp:Label ID="Label32" runat="server" Text="Dirección:"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <asp:TextBox ID="txtDireccion" runat="server" CssClass="inputNormal" 
                    MaxLength="1000" Width="200px"></asp:TextBox>
            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933">
                <asp:Label ID="Label33" runat="server" Text="Teléfono:"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="inputNormal" 
                    MaxLength="20"></asp:TextBox>
            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933">
                <asp:Label ID="Label34" runat="server" Text="Email:"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <asp:TextBox ID="txtEmail" runat="server" CssClass="inputNormal" MaxLength="50" 
                    Width="200px"></asp:TextBox>
            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933">
                <asp:Label ID="Label35" runat="server" Text="Facebook:"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <asp:TextBox ID="txtFacebook" runat="server" CssClass="inputNormal" 
                    MaxLength="50" Width="200px"></asp:TextBox>
            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933">
                <asp:Label ID="Label36" runat="server" Text="Contacto:"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <asp:TextBox ID="txtContacto" runat="server" CssClass="inputNormal" 
                    MaxLength="50" Width="200px"></asp:TextBox>
            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933">
                <asp:Label ID="Label46" runat="server" Text="Fecha Cumpleaños:"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <asp:TextBox ID="txtfecump" runat="server" CssClass="inputsFecha" 
                    MaxLength="10"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtfecump_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtfecump">
                        </cc1:CalendarExtender>
            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933">
                <asp:Label ID="Label47" runat="server" Text="Genero:"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                    <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" 
                    Width="210px" ID="rblSexo"><asp:ListItem Selected="True" Value="A">Femenino</asp:ListItem>
<asp:ListItem Value="O">Masculino</asp:ListItem>
</asp:RadioButtonList>

            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933">
                <asp:Label ID="Label48" runat="server" Text="Celular:"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <asp:TextBox ID="txtCelular" runat="server" CssClass="inputNormal" 
                    MaxLength="50" Width="200px"></asp:TextBox>
            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933">
                <asp:Label ID="Label49" runat="server" Text="Distrito:"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                    <asp:DropDownList runat="server" CssClass="combo" Width="215px" 
                    ID="ddlDistrito" TabIndex="5">
                </asp:DropDownList>

            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933">
                <asp:Label ID="Label37" runat="server" Text="Comentario:"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <asp:TextBox ID="txtComentario" runat="server" TextMode="MultiLine" 
                    Width="200px"></asp:TextBox>
            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933">
                <asp:Label ID="Label51" runat="server" Text="Puntos Acumulados:"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <asp:Label ID="lblPuntos" runat="server" Text="0"></asp:Label>
            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933">
                <asp:Label ID="Label38" runat="server" Text="Estado:"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <asp:CheckBox ID="chkEstado" runat="server" Checked="True" Text="Habilitado" />
            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td style="padding-left: 5px">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td width="65">
                            <asp:ImageButton ID="btnGuardarCliente" runat="server" 
                                ImageUrl="~/images/Guardar.jpg" onclick="btnGuardarCliente_Click" />
                        </td>
                        <td>
                            <asp:ImageButton ID="btnSalirCliente" runat="server" 
                                ImageUrl="~/images/Salir.jpg" onclick="btnSalirCliente_Click" />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td width="20">
                &nbsp;</td>
        </tr>
        </table>
        </div>

            </td>
            <td width="5%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="5%">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td width="5%">
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
                                <table class="style1">
                                    <tr>
                                        <td align="right">
                                            <asp:RadioButtonList ID="rblTipo" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True" Value="C">Código</asp:ListItem>
                                                <asp:ListItem Value="P">Nombre</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td align="right" width="170">
                                            <asp:TextBox ID="txtBuscar" runat="server" AutoPostBack="True" 
                                                CssClass="inputsProducto" ontextchanged="txtBuscar_TextChanged" 
                                                placeholder="Buscar Productos" Width="150px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
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
                                                                            ForeColor="White" Text='<%# String.Format("{0:C}", Eval("f_Precio") ) %>'></asp:Label>
                                                                    </div>
                                                                    <div align="right" 
                                                                        
                                                                        style="padding: 2px 3px 1px 3px; background-color: #FF9966; height: 15px; right: 8px; top: 29px; color: #FFFFFF; float: right; position: absolute;">
                                                                        <asp:Label ID="lblStock" runat="server" Font-Bold="True" Font-Size="8pt" 
                                                                            ForeColor="White" Text='<%# Eval("f_StockContable") %>'></asp:Label>
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
                                                                    <asp:Label ID="lblPrecio2" runat="server" Text='<%# Bind("f_Precio") %>' 
                                                                        Visible="False"></asp:Label>
                                                                    <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("v_CodigoInterno") %>' 
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

