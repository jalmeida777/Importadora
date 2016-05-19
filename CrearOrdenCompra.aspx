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
            .style1
            {
                width: 100%;
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

         function OnClienteSeleccionado(source, eventArgs) {
             var hdnValueID = "<%= hdnValue.ClientID %>";

             document.getElementById(hdnValueID).value = eventArgs.get_value();
             __doPostBack(hdnValueID, "");
         }


         function OnProductoSeleccionado(source, eventArgs) {

             if (source) {
                 // Get the HiddenField ID.
                 var hiddenfieldID = source.get_id().replace("txtProducto_AutoCompleteExtender", "hfIdProducto");
                 $get(hiddenfieldID).value = eventArgs.get_value();

                 __doPostBack(hiddenfieldID, "");

             }

         }

 </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

                <div class="toolbar" id="toolbar" runat="server">
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

<asp:hiddenfield id="hdnValue" onvaluechanged="hdnValue_ValueChanged" runat="server"/>

                </td>
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
                width="100">
                <asp:Label ID="Label6" runat="server" Text="Proveedor"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <asp:TextBox ID="txtProveedor" runat="server" CssClass="inputNormal" 
                    Width="200px" AutoPostBack="True"></asp:TextBox>
                &nbsp;<cc1:AutoCompleteExtender ID="txtProveedor_AutoCompleteExtender" runat="server" 
                    CompletionInterval="100" CompletionListCssClass="AutoExtender" 
                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight" 
                    CompletionListItemCssClass="AutoExtenderList" DelimiterCharacters="" 
                    Enabled="True" MinimumPrefixLength="2" 
                    ServiceMethod="BuscarProveedores" ServicePath="" TargetControlID="txtProveedor" 
                    onclientitemselected="OnClienteSeleccionado" 
                    ShowOnlyCurrentWordInCompletionListItem="True">
                </cc1:AutoCompleteExtender>
                <asp:ImageButton ID="btnProveedor" runat="server" ImageUrl="~/images/proveedor.gif" 
                    ToolTip="Editar Proveedor" Visible="False" onclick="btnProveedor_Click" />

            </td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933" 
                width="100">
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
                <asp:Label ID="Label7" runat="server" Text="Moneda"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <asp:Label ID="Label23" runat="server" Font-Bold="True" Text="Soles"></asp:Label>

            </td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933">
                <asp:Label ID="Label5" runat="server" Text="Referencia" ForeColor="#4C4C4C"></asp:Label>
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
                <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" 
                    CssClass="grid" onrowdeleting="gv_RowDeleting" DataKeyNames="n_IdProducto" 
                    onrowdatabound="gv_RowDataBound" ShowFooter="True">
                    <Columns>
                        <asp:TemplateField HeaderText="Producto">
                            <ItemTemplate>
                                <asp:TextBox ID="txtProducto" runat="server" CssClass="inputNormal" Width="300px" 
                                    AutoPostBack="True" 
                                    Text='<%# Bind("Producto") %>' 
                                    placeholder="CODIGO DE BARRAS O DESCRIPCION"></asp:TextBox>
                                <asp:HiddenField ID="hfIdProducto" runat="server" 
                                    onvaluechanged="hfIdProducto_ValueChanged" 
                                    Value='<%# Bind("n_IdProducto") %>' />
                                <cc1:AutoCompleteExtender ID="txtProducto_AutoCompleteExtender" runat="server" 
                                    CompletionInterval="100" CompletionListCssClass="AutoExtender" 
                                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight" 
                                    CompletionListItemCssClass="AutoExtenderList" DelimiterCharacters="" 
                                    Enabled="True" MinimumPrefixLength="2" 
                                    onclientitemselected="OnProductoSeleccionado" ServiceMethod="BuscarProductos" 
                                    ServicePath="" ShowOnlyCurrentWordInCompletionListItem="True" 
                                    TargetControlID="txtProducto">
                                </cc1:AutoCompleteExtender>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Producto") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:LinkButton ID="lnkAgregarProducto" runat="server" Font-Bold="True" 
                                    ForeColor="#7C7BAD" onclick="lnkAgregarProducto_Click">Agregar Producto</asp:LinkButton>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cantidad">
                            <ItemTemplate>
<asp:TextBox ID="txtCantidad" runat="server" AutoPostBack="True" 
                                    CssClass="inputNormalMoneda" 
                                    Text='<%# Bind("Cantidad") %>' Width="45px" MaxLength="5" 
                                    onkeypress="return ValidaEntero(event);" 
                                    ontextchanged="txtCantidad_TextChanged"></asp:TextBox>

                                
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Costo Unidad S/.">
                            <ItemTemplate>
                                <asp:TextBox ID="txtCostoUnidad" runat="server" AutoPostBack="True" 
                                    CssClass="inputNormalMoneda" ontextchanged="txtCostoUnidad_TextChanged" 
                                    Text='<%# Bind("CostoUnitario", "{0:n2}") %>' Width="80px"></asp:TextBox>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("CostoUnitario") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                        </asp:TemplateField>

                        <asp:BoundField HeaderText="Costo Total S/." DataField="CostoTotal" 
                            DataFormatString="{0:n2}">
                        <FooterStyle HorizontalAlign="Right" />
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                        </asp:BoundField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="ibQuitar" runat="server" CommandName="delete" 
                                    ImageUrl="~/images/delete.gif" ToolTip="Quitar" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                        </asp:TemplateField>

                    </Columns>
                    <FooterStyle Font-Bold="True" ForeColor="Black" BackColor="#F3F3F3" />
                </asp:GridView>
                
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
                        <td rowspan="3" valign="top">
                <asp:TextBox ID="txtObservacion" runat="server" Height="80px" 
                    TextMode="MultiLine" Width="380px" placeholder="Comentarios"></asp:TextBox>
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
</div></td>
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
                         <table width="100%" id="tblProveedor" runat="server"
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
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td width="100" colspan="2">
                <asp:Label ID="Label39" runat="server" Font-Bold="True" Font-Size="20pt" 
                    Text="Proveedor" ForeColor="#4C4C4C"></asp:Label>
            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td width="100" 
                style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 180px;">
                <asp:Label ID="Label17" runat="server" Text="Ruc:"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <asp:TextBox ID="txtRuc" runat="server" CssClass="inputNormal" MaxLength="11" 
                    Width="90px"></asp:TextBox>
            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 180px;">
                <asp:Label ID="Label26" runat="server" Text="Nombre:"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <asp:TextBox ID="txtNombre" runat="server" CssClass="inputNormal"
                    Width="300px" style="text-transform:uppercase" MaxLength="100"></asp:TextBox>
                <asp:Label ID="Label16" runat="server" Font-Bold="True" ForeColor="#18AC85" 
                        Text="*"></asp:Label>
            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 180px;">
                <asp:Label ID="Label18" runat="server" Text="Teléfono:"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="inputNormal" 
                    MaxLength="50"></asp:TextBox>
            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 180px;">
                <asp:Label ID="Label19" runat="server" Text="Dirección:"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <asp:TextBox ID="txtDireccion" runat="server" CssClass="inputNormal" 
                    MaxLength="100" Width="300px"></asp:TextBox>
            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 180px;">
                <asp:Label ID="Label27" runat="server" Text="Contacto:"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <asp:TextBox ID="txtContacto" runat="server" CssClass="inputNormal" 
                    MaxLength="50" Width="300px"></asp:TextBox>
            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 180px;">
                <asp:Label ID="Label28" runat="server" Text="Email:"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <asp:TextBox ID="txtEmail" runat="server" CssClass="inputNormal" MaxLength="50" 
                    Width="300px"></asp:TextBox>
            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 180px;">
                <asp:Label ID="Label3" runat="server" Text="Estado:"></asp:Label>
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
                <table cellpadding="0" cellspacing="0" class="style1">
                    <tr>
                        <td width="65">
                            <asp:ImageButton ID="btnGuardarProveedor" runat="server" 
                                ImageUrl="~/images/Guardar.jpg" onclick="btnGuardarProveedor_Click" />
                        </td>
                        <td>
                            <asp:ImageButton ID="btnSalirProveedor" runat="server" 
                                ImageUrl="~/images/Salir.jpg" onclick="btnSalirProveedor_Click" />
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
            <td></td>
            </tr>
        <tr>
            <td width="15%">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>&nbsp;</td>
            </tr>
            </table>
</asp:Content>

