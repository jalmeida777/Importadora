﻿<%@ Page Title="Orden de Compra" Language="C#" MasterPageFile="~/Plantilla.master" AutoEventWireup="true" CodeFile="CrearOrdenCompra.aspx.cs" Inherits="CrearOrdenCompra" %>
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
            <table width="100%"><tr><td width="95">
                
                    <asp:ImageButton ID="btnNuevo" runat="server" 
                        ImageUrl="~/images/btnNuevo_New.png" onclick="btnNuevo_Click" />
                  
                </td>
                <td width="95">
                  
                <asp:ImageButton ID="btnGuardar" runat="server" 
                    ImageUrl="~/images/btnGuardar_New.png" onclick="btnGuardar_Click" 
                        onclientclick="if (confirm('Seguro de guardar?')) { btnGuardar.disabled = false; return true; } else { return false; }" />
                
                </td>
                <td width="95">
                   
                    <asp:ImageButton ID="btnDespachar" runat="server" 
                        ImageUrl="~/images/btnDespachar_New.png" onclick="btnDespachar_Click" />
                   
                </td>
                <td align="left" width="95">
                   
                    <asp:ImageButton ID="btnAnular" runat="server" 
                        ImageUrl="~/images/btnAnular_New.png" 
                        
                        onclientclick="if (confirm('Seguro de anular?')) { btnAnular.disabled = false; return true; } else { return false; }" 
                        onclick="btnAnular_Click" />
                   
                </td>
                <td align="left">
                   
                    <asp:ImageButton ID="btnSalir" runat="server" 
                        ImageUrl="~/images/btnSalir_New.png" onclick="btnSalir_Click" />
                   
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
                <asp:HiddenField ID="hfIdOrdenCompra" runat="server" />
            </td>
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
                <asp:Label ID="Label56" runat="server" Font-Bold="True" ForeColor="#18AC85" 
                        Text="*"></asp:Label>
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
                <asp:Label ID="Label23" runat="server" Font-Bold="True" Text="Dolares"></asp:Label>

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
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933">
                <asp:Label ID="Label41" runat="server" Text="Adjunto"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <table cellpadding="0" cellspacing="0" class="style1">
                    <tr>
                        <td width="135">
                            <asp:FileUpload ID="fu1" runat="server" Width="135px" />
                        </td>
                        <td width="30">
                            <asp:ImageButton ID="btnSubirArchivo" runat="server" 
                                ImageUrl="~/images/upload.png" onclick="btnSubirArchivo_Click" />
                        </td>
                        <td>
                            <asp:LinkButton ID="lbAdjunto" runat="server" Font-Underline="True" 
                                ForeColor="Blue"></asp:LinkButton>
                        </td>
                    </tr>
                </table>

            </td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933">
                <asp:Label ID="Label40" runat="server" Text="Estado"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <asp:Label ID="lblEstado" runat="server" Text="PENDIENTE"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933">
                <asp:Label ID="Label57" runat="server" Text="Condición de pago"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <asp:RadioButtonList ID="rblCondicion" runat="server" 
                    RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="1">CONTADO</asp:ListItem>
                    <asp:ListItem Value="2">CREDITO</asp:ListItem>
                </asp:RadioButtonList>

            </td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933">
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
                        <asp:BoundField DataField="Saldo" HeaderText="Saldo">
                        <ItemStyle HorizontalAlign="Right" Width="50px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Costo Unidad US$">
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

                        <asp:BoundField HeaderText="Costo Total US$" DataField="CostoTotal" 
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
                        <td valign="top" rowspan="3">
                <asp:TextBox ID="txtObservacion" runat="server" Height="80px" 
                    TextMode="MultiLine" Width="380px" placeholder="Comentarios"></asp:TextBox>
                        </td>
                        <td align="right" width="100" valign="top">
                            <asp:Label ID="Label22" runat="server" Font-Bold="True" Font-Size="12pt" 
                                Text="Total"></asp:Label>
                        </td>
                        <td align="right" width="20" valign="top">
                            <asp:Label ID="lblSigno3" runat="server" Font-Bold="True" Font-Size="12pt" 
                                Text="US$"></asp:Label>
                        </td>
                        <td align="right" width="100" valign="top">
                            <asp:Label ID="lblTotal" runat="server" Font-Bold="True" Font-Size="12pt"></asp:Label>
                        </td>
                        <td align="right" width="20" valign="top" rowspan="3">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="right" width="100" valign="top">
                            <asp:Label ID="Label58" runat="server" Font-Bold="True" Font-Size="12pt" 
                                Text="Adelanto"></asp:Label>
                        </td>
                        <td align="right" width="20" valign="top">
                            <asp:Label ID="lblSigno4" runat="server" Font-Bold="True" Font-Size="12pt" 
                                Text="US$"></asp:Label>
                        </td>
                        <td align="right" width="100" valign="top">
                            <asp:TextBox ID="txtAdelanto" runat="server" CssClass="inputNormalMoneda" 
                                Width="80px" AutoPostBack="True" ontextchanged="txtAdelanto_TextChanged" onkeypress="return ValidaNumeros(event);">0.00</asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" width="100" valign="top">
                            <asp:Label ID="Label59" runat="server" Font-Bold="True" Font-Size="12pt" 
                                Text="Saldo"></asp:Label>
                        </td>
                        <td align="right" width="20" valign="top">
                            <asp:Label ID="lblSigno5" runat="server" Font-Bold="True" Font-Size="12pt" 
                                Text="US$"></asp:Label>
                        </td>
                        <td align="right" width="100" valign="top">
                            <asp:Label ID="lblSaldo" runat="server" Font-Bold="True" Font-Size="12pt"></asp:Label>
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
                        <td width="95">
                            <asp:ImageButton ID="btnGuardarProveedor" runat="server" 
                                ImageUrl="~/images/btnGuardar_New.png" 
                                onclick="btnGuardarProveedor_Click" />
                        </td>
                        <td>
                            <asp:ImageButton ID="btnSalirProveedor" runat="server" 
                                ImageUrl="~/images/btnSalir_New.png" onclick="btnSalirProveedor_Click" />
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
            <td width="5%">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>&nbsp;</td>
            </tr>
            </table>

        <table width="100%" id="tblProducto" runat="server"
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

                    <table width="100%" __designer:mapid="fb" cellpadding="5">
                            <tr __designer:mapid="fc">
            <td height="10" width="20" __designer:mapid="fd">
                &nbsp;</td>
            <td __designer:mapid="fe">
                    &nbsp;</td>
            <td colspan="3" __designer:mapid="ff">
                    &nbsp;</td>
            <td align="right" __designer:mapid="100">

                                    
                           
                                
                &nbsp;</td>
            <td width="20" __designer:mapid="103">
                &nbsp;</td>
        </tr>
                            <tr __designer:mapid="fc">
            <td height="10" width="20" __designer:mapid="fd">
                &nbsp;</td>
            <td __designer:mapid="fe" colspan="5">
                <asp:Label ID="Label55" runat="server" Font-Bold="True" Font-Size="20pt" 
                    Text="Producto" ForeColor="#4C4C4C"></asp:Label>
                                </td>
            <td width="20" __designer:mapid="103">
                &nbsp;</td>
        </tr>
                            <tr __designer:mapid="104">
                                <td height="10" width="20" __designer:mapid="105">
                                    &nbsp;</td>
                                <td __designer:mapid="106" 
                                    
                                    style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 160px;">
                <asp:Label runat="server" Text="Nombre del Producto:" ID="Label54" 
                    Font-Bold="False"></asp:Label>

                                </td>
                                <td colspan="3" __designer:mapid="108">
                                    <asp:TextBox runat="server" MaxLength="20" CssClass="inputNormal" Width="300px" 
                                        ID="txtProducto"></asp:TextBox>

                                </td>
            <td rowspan="4" align="left" __designer:mapid="100">

                                    
                           
                                
<a href="javascript:void(0);" onclick="OnMoreInfoClick(this)" __designer:mapid="101">
                <asp:Image runat="server" ImageUrl="~/images/Prev.jpg" Height="150px" 
                    Width="200px" ID="ibImagen"></asp:Image>

                    </a>


                </td>
                                <td width="20" __designer:mapid="10b">
                                    &nbsp;</td>
                            </tr>
                            <tr __designer:mapid="104">
                                <td height="10" width="20" __designer:mapid="105">
                                    &nbsp;</td>
                                <td __designer:mapid="106" 
                                    
                                    style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 160px;">
                                    <asp:Label runat="server" Text="C&#243;digo Interno:" ID="Label42"></asp:Label>

                                </td>
                                <td colspan="3" __designer:mapid="108">
                                    <asp:TextBox runat="server" MaxLength="20" CssClass="inputNormal" Width="150px" 
                                        ID="txtCodigoInterno"></asp:TextBox>

                                    <asp:Label runat="server" Text="*" Font-Bold="True" Font-Size="10pt" 
                                        ForeColor="#18AC85" ID="Label43"></asp:Label>

                                </td>
                                <td width="20" __designer:mapid="10b">
                                    &nbsp;</td>
                            </tr>
        <tr __designer:mapid="10c">
            <td height="10" width="20" __designer:mapid="10d">
                &nbsp;</td>
            <td __designer:mapid="10e" 
                
                style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 160px;">
                    <asp:Label runat="server" Text="Presentaci&#243;n:" ID="Label44"></asp:Label>

                </td>
            <td colspan="3" __designer:mapid="110">
                    <asp:TextBox runat="server" CssClass="inputNormal" Width="300px" 
                        ID="txtPresentacion" placeholder="Presentación" 
                        style="text-transform:uppercase"></asp:TextBox>

                </td>
            <td width="20" __designer:mapid="112">
                &nbsp;</td>
        </tr>
        <tr __designer:mapid="11a">
            <td height="10" width="20" __designer:mapid="11b">
                &nbsp;</td>
            <td __designer:mapid="11c" 
                
                style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 160px;">
                    <asp:Label runat="server" Text="Genero:" ID="Label46"></asp:Label>

                </td>
            <td colspan="3" __designer:mapid="11e">
                    <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" Width="215px" 
                        ID="rblSexo"><asp:ListItem Selected="True" Value="O">Ni&#241;o</asp:ListItem>
<asp:ListItem Value="A">Ni&#241;a</asp:ListItem>
<asp:ListItem Value="U">Ambos</asp:ListItem>
</asp:RadioButtonList>

                </td>
            <td width="20" __designer:mapid="123">
                &nbsp;</td>
        </tr>
        <tr __designer:mapid="124">
            <td height="10" width="20" __designer:mapid="125">
                &nbsp;</td>
            <td __designer:mapid="126" 
                
                style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 160px;">
                    <asp:Label runat="server" Text="Bater&#237;as:" ID="Label47"></asp:Label>

                </td>
            <td __designer:mapid="128">
                                <asp:DropDownList runat="server" CssClass="combo" 
                                    Width="150px" ID="ddlBateria"></asp:DropDownList>

                            </td>
            <td __designer:mapid="12a">
                                &nbsp;</td>
            <td __designer:mapid="12c">
                                &nbsp;</td>
            <td align="right" __designer:mapid="12e">
                <table class="style1" __designer:mapid="12f">
                    <tr __designer:mapid="130">
                        <td align="left" __designer:mapid="131">
                                <asp:FileUpload runat="server" Width="130px" ID="fu2"></asp:FileUpload>

                            </td>
                        <td align="left" __designer:mapid="133">
                            <asp:ImageButton runat="server" ImageUrl="~/images/upload.png" ID="ibUpload" 
                                style="height: 16px" OnClick="ibUpload_Click"></asp:ImageButton>

                        </td>
                    </tr>
                </table>
            </td>
            <td width="20" __designer:mapid="135">
                &nbsp;</td>
        </tr>
        <tr __designer:mapid="136">
            <td height="10" width="20" __designer:mapid="137">
                &nbsp;</td>
            <td __designer:mapid="138" 
                
                style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 160px;">
                    <asp:Label runat="server" Text="Precio S/.:" ID="Label49"></asp:Label>

                </td>
            <td __designer:mapid="13a">
                    <asp:TextBox runat="server" CssClass="inputNormalMoneda" Width="50px" 
                        ID="txtPrecio" placeholder="Precio" onkeypress="return ValidaNumeros(event);">0</asp:TextBox>

                <asp:Label runat="server" Text="*" Font-Bold="True" Font-Size="10pt" 
                        ForeColor="#18AC85" ID="Label50"></asp:Label>

                </td>
            <td __designer:mapid="13d">
                    &nbsp;</td>
            <td __designer:mapid="13f">
                    &nbsp;</td>
            <td __designer:mapid="142">
                    <asp:Label runat="server" Text="C&#243;digo:" ID="Label51" Visible="False"></asp:Label>

                    <asp:Label runat="server" Font-Bold="True" ID="lblCodigo" Visible="False"></asp:Label>

                </td>
            <td width="20" __designer:mapid="145">
                &nbsp;</td>
        </tr>
        <tr __designer:mapid="136">
            <td height="10" width="20" __designer:mapid="137">
                &nbsp;</td>
            <td __designer:mapid="138" 
                
                style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 160px;">
                    <asp:Label runat="server" Text="Costo US$.:" ID="Label8"></asp:Label>

                </td>
            <td __designer:mapid="13a">
                    <asp:TextBox runat="server" CssClass="inputNormalMoneda" Width="50px" 
                        ID="txtCosto" placeholder="Costo" onkeypress="return ValidaNumeros(event);">0</asp:TextBox>

                    <asp:Label runat="server" Text="*" Font-Bold="True" Font-Size="10pt" 
                        ForeColor="#18AC85" ID="Label25"></asp:Label>

                </td>
            <td __designer:mapid="13d">
                    &nbsp;</td>
            <td __designer:mapid="13f">
                    &nbsp;</td>
            <td __designer:mapid="142">
                    &nbsp;</td>
            <td width="20" __designer:mapid="145">
                &nbsp;</td>
        </tr>
        <tr __designer:mapid="146">
            <td height="10" width="20" __designer:mapid="147">
                &nbsp;</td>
            <td __designer:mapid="148" 
                
                style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 160px;">
                    <asp:Label runat="server" Text="Stock M&#237;nimo:" ID="Label9"></asp:Label>

                </td>
            <td __designer:mapid="14a">
                    <asp:TextBox runat="server" CssClass="inputNormalMoneda" Width="50px" 
                        ID="txtStockMinimo" placeholder="Stock" style="text-transform:uppercase" 
                        onkeypress="return ValidaEntero(event);">0</asp:TextBox>

                    <asp:Label runat="server" Text="*" Font-Bold="True" Font-Size="10pt" 
                        ForeColor="#18AC85" ID="Label24"></asp:Label>

                </td>
            <td __designer:mapid="14d">
                &nbsp;</td>
            <td __designer:mapid="14e">
                &nbsp;</td>
            <td __designer:mapid="14f">
                    <asp:Label runat="server" ID="lblRuta" Visible="False"></asp:Label>

                    <asp:Label runat="server" ID="lblExtension" Visible="False"></asp:Label>

                </td>
            <td width="20" __designer:mapid="151">
                &nbsp;</td>
        </tr>
        <tr __designer:mapid="15f">
            <td height="10" width="20" __designer:mapid="160">
                &nbsp;</td>
            <td __designer:mapid="161" 
                
                style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 160px;">
                    <asp:Label runat="server" Text="Familia:" ID="Label12"></asp:Label>

                </td>
            <td __designer:mapid="163">
                    <asp:DropDownList runat="server" CssClass="combo" 
                        Width="150px" ID="ddlCategoria"></asp:DropDownList>

                </td>
            <td __designer:mapid="165">
                    &nbsp;</td>
            <td __designer:mapid="167">
                    &nbsp;</td>
            <td __designer:mapid="169">
                &nbsp;</td>
            <td width="20" __designer:mapid="16a">
                &nbsp;</td>
        </tr>
        <tr __designer:mapid="15f">
            <td height="10" width="20" __designer:mapid="160">
                &nbsp;</td>
            <td __designer:mapid="161">
                    &nbsp;</td>
            <td __designer:mapid="163">
                <table cellpadding="0" cellspacing="0" class="style1">
                    <tr>
                        <td width="95">
                            <asp:ImageButton ID="btnGuardarProducto" runat="server" 
                                ImageUrl="~/images/btnGuardar_New.png" onclick="btnGuardarProducto_Click" 
                                Width="95px" />
                        </td>
                        <td>
                            <asp:ImageButton ID="btnSalirProducto" runat="server" 
                                ImageUrl="~/images/btnSalir_New.png" onclick="btnSalirProducto_Click" />
                        </td>
                    </tr>
                </table>

                </td>
            <td __designer:mapid="165">
                    &nbsp;</td>
            <td __designer:mapid="167">
                    &nbsp;</td>
            <td __designer:mapid="169">
                &nbsp;</td>
            <td width="20" __designer:mapid="16a">
                &nbsp;</td>
        </tr>

                            <tr __designer:mapid="177">
                                <td height="10" width="20" __designer:mapid="178">
                                    &nbsp;</td>
                                <td __designer:mapid="179">
                                    &nbsp;</td>
                                <td __designer:mapid="17a">
                                    &nbsp;</td>
                                <td __designer:mapid="17b">
                                    &nbsp;</td>
                                <td __designer:mapid="17c">
                                    &nbsp;</td>
                                <td align="right" __designer:mapid="17d">
                                    <asp:Label runat="server" Text="* Campos obligatorios" Font-Bold="False" 
                                        Font-Size="10pt" ForeColor="#18AC85" ID="Label53"></asp:Label>

                                </td>
                                <td width="20" __designer:mapid="17f">
                                    &nbsp;</td>
                            </tr>

                    </table>

                </div>
            </td>
            <td></td>
            </tr>
        <tr>
            <td width="5%">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>&nbsp;</td>
            </tr>
            </table>
</asp:Content>

