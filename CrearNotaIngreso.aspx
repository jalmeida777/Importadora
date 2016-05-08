<%@ Page Title="Nota de Ingreso" Language="C#" MasterPageFile="~/Plantilla.master" AutoEventWireup="true" CodeFile="CrearNotaIngreso.aspx.cs" Inherits="CrearNotaIngreso" %>

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

         function OnClienteSeleccionado(source, eventArgs) {

             if (source) {
                 // Get the HiddenField ID.
                 var hiddenfieldID = source.get_id().replace("txtProducto_AutoCompleteExtender", "hfIdProducto");
                 $get(hiddenfieldID).value = eventArgs.get_value();

                 __doPostBack(hiddenfieldID, "");

             }
             //alert("HiddenFieldID :" + hiddenfieldID + " HiddenFieldValue : " + $get(hiddenfieldID).value);
             //alert(" Key : " + eventArgs.get_text() + "  Value :  " + eventArgs.get_value());

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
                <td width="65">
                   
                    <asp:ImageButton ID="ibAnular" runat="server" ImageUrl="~/images/Anular.jpg" 
                        onclick="ibAnular_Click" Visible="False" OnClientClick="if (confirm('Seguro de anular?')) { ibAnular.disabled = false; return true; } else { return false; }" />
                </td>
                <td>
                   
                    <asp:ImageButton ID="btnSalir" runat="server" ImageUrl="~/images/Salir.jpg" 
                        onclick="btnSalir_Click" />
                   
                </td>
                </tr></table>
            </div>

 <table width="100%" id="tblGeneral" runat="server"
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
                    Text="Nota de Ingreso N° " ForeColor="#4C4C4C"></asp:Label>
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
                    </td>
            <td style="padding-left: 5px">
                        &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933">
                <asp:Label ID="Label4" runat="server" Text="Tipo Movimiento:" 
                    ForeColor="#4C4C4C"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <asp:DropDownList ID="ddlTipoMovimiento" runat="server" Width="200px" 
                    CssClass="combo">
                </asp:DropDownList>
            </td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933">
                <asp:Label ID="Label5" runat="server" Text="Referencia:" ForeColor="#4C4C4C"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <asp:TextBox ID="txtReferencia" runat="server" CssClass="inputNormal"></asp:TextBox>
            </td>
            <td style="padding-left: 5px">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933">
                <asp:Label ID="Label28" runat="server" Text="Estado:"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <asp:Label ID="lblEstado" runat="server" Font-Bold="True"></asp:Label>
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
                <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" 
                    CssClass="grid" onrowdeleting="gv_RowDeleting" ShowFooter="True" 
                    onrowdatabound="gv_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Producto">
                            <ItemTemplate>
                                <asp:TextBox ID="txtProducto" runat="server" Width="300px" 
                                    CssClass="inputNormal" Text='<%# Bind("Producto") %>' 
                                    placeholder="Ingrese Producto" AutoPostBack="True"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="txtProducto_AutoCompleteExtender" runat="server" 
                                    DelimiterCharacters="" Enabled="True" ServicePath="" 
                                    TargetControlID="txtProducto" CompletionInterval="100" 
                                    CompletionListCssClass="AutoExtender" 
                                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight" 
                                    CompletionListItemCssClass="AutoExtenderList" 
                                    MinimumPrefixLength="2" ServiceMethod="BuscarProductos" 
                                    onclientitemselected="OnClienteSeleccionado" 
                                    ShowOnlyCurrentWordInCompletionListItem="True">
                                </cc1:AutoCompleteExtender>

                                <asp:ImageButton ID="btnNuevoProducto" runat="server" 
                                    ImageUrl="~/images/edit.png" Visible="False" 
                                    onclick="btnNuevoProducto_Click" />

                                <asp:HiddenField ID="hfIdProducto" runat="server" 
                                    onvaluechanged="hfIdProducto_ValueChanged" 
                                    Value='<%# Bind("n_IdProducto") %>' />

                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:LinkButton ID="lnkAgregarProducto" runat="server" Font-Bold="True" 
                                    ForeColor="#7C7BAD" onclick="lnkAgregarProducto_Click">Agregar Producto</asp:LinkButton>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cantidad">
                            <ItemTemplate>
                                <asp:TextBox ID="txtCantidad" runat="server" Width="100px" 
                                    CssClass="inputNormalMoneda" Text='<%# Bind("Cantidad") %>' onkeypress="return ValidaEntero(event);"></asp:TextBox>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="ibQuitar" runat="server" CommandName="delete" 
                                    ImageUrl="~/images/delete.gif" ToolTip="Quitar" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
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
            <td colspan="2">
                <asp:TextBox ID="txtObservacion" runat="server" Height="80px" 
                    TextMode="MultiLine" Width="400px" placeholder="Comentarios"></asp:TextBox>
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
     <table width="100%" id="tblProducto" runat="server" visible="false"
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
                    <table width="100%" __designer:mapid="2b4">
                            <tr __designer:mapid="2b5">
            <td height="10" width="20" __designer:mapid="2b6">
                &nbsp;</td>
            <td __designer:mapid="2b7">
                    &nbsp;</td>
            <td colspan="4" __designer:mapid="2b8">
                    &nbsp;</td>
            <td align="right" __designer:mapid="2b9">
                &nbsp;</td>
            <td width="20" __designer:mapid="2bb">
                &nbsp;</td>
        </tr>
                            <tr __designer:mapid="2b5">
            <td height="10" width="20" __designer:mapid="2b6">
                &nbsp;</td>
            <td __designer:mapid="2b7">
                <asp:Label runat="server" Text="Nombre del Producto:" ID="Label27" 
                    Font-Bold="False"></asp:Label>
                                </td>
            <td colspan="5" __designer:mapid="2b8">
                <asp:TextBox runat="server" CssClass="inputNormal" Width="600px" 
                    ID="txtDescripcion" placeholder="Nombre del Producto" style="text-transform:uppercase" 
                    Font-Bold="True" Font-Size="20pt" Height="40px"></asp:TextBox>
                <cc1:AutoCompleteExtender runat="server" MinimumPrefixLength="2" 
                    CompletionInterval="100" ServiceMethod="BuscarProductos" ServicePath="" 
                    CompletionListCssClass="AutoExtender" 
                    CompletionListItemCssClass="AutoExtenderList" 
                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight" 
                    EnableCaching="False" DelimiterCharacters="" Enabled="True" 
                    TargetControlID="txtDescripcion" ID="txtDescripcion_AutoCompleteExtender">
                </cc1:AutoCompleteExtender>
                <asp:Label runat="server" Text="*" Font-Bold="True" ForeColor="#18AC85" 
                    ID="Label15" Font-Size="16pt"></asp:Label>
                                </td>
            <td width="20" __designer:mapid="2bb">
                &nbsp;</td>
        </tr>
                            <tr __designer:mapid="2bc">
                                <td height="10" width="20" __designer:mapid="2bd">
                                    &nbsp;</td>
                                <td __designer:mapid="2be">
                                    <asp:Label runat="server" Text="C&#243;digo Interno:" ID="Label22"></asp:Label>

                                </td>
                                <td colspan="2" __designer:mapid="2c0">
                                    <asp:TextBox runat="server" MaxLength="4" CssClass="inputNormal" Width="50px" 
                                        ID="txtCodigoInterno" onkeypress="return ValidaEntero(event);"></asp:TextBox>

                <asp:Label runat="server" Text="*" Font-Bold="True" Font-Size="10pt" 
                        ForeColor="#18AC85" ID="Label29"></asp:Label>

                                </td>
                                <td __designer:mapid="2c2">
                                    <asp:Label runat="server" Text="C&#243;digo de Barras:" ID="Label23"></asp:Label>

                                </td>
                                <td __designer:mapid="2c4">
                                    <asp:Label runat="server" Font-Bold="True" ID="lblCodigoBarras"></asp:Label>

                                </td>
            <td rowspan="4" align="right" __designer:mapid="2b9">
                <asp:Image ID="ibImagen" runat="server" Height="150px" 
                    ImageUrl="~/images/Prev.jpg" Width="200px" />

                </td>
                                <td width="20" __designer:mapid="2c6">
                                    &nbsp;</td>
                            </tr>
        <tr __designer:mapid="2c7">
            <td height="10" width="20" __designer:mapid="2c8">
                &nbsp;</td>
            <td __designer:mapid="2c9">
                    <asp:Label runat="server" Text="Presentaci&#243;n:" ID="Label24"></asp:Label>

                </td>
            <td colspan="4" __designer:mapid="2cb">
                    <asp:TextBox runat="server" CssClass="inputNormal" Width="300px" 
                        ID="txtPresentacion" placeholder="Presentación" 
                        style="text-transform:uppercase"></asp:TextBox>

                </td>
            <td width="20" __designer:mapid="2cd">
                &nbsp;</td>
        </tr>
        <tr __designer:mapid="2ce">
            <td height="10" width="20" __designer:mapid="2cf">
                &nbsp;</td>
            <td __designer:mapid="2d0">
                    <asp:Label runat="server" Text="Edad:" ID="Label6"></asp:Label>

                </td>
            <td colspan="4" __designer:mapid="2d2">
                    <asp:DropDownList runat="server" CssClass="combo" Width="150px" ID="ddlEdad"></asp:DropDownList>

                </td>
            <td width="20" __designer:mapid="2d4">
                &nbsp;</td>
        </tr>
        <tr __designer:mapid="2d5">
            <td height="10" width="20" __designer:mapid="2d6">
                &nbsp;</td>
            <td __designer:mapid="2d7">
                    <asp:Label runat="server" Text="Genero:" ID="Label18"></asp:Label>

                </td>
            <td colspan="4" __designer:mapid="2d9">
                    <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" Width="215px" 
                        ID="rblSexo"><asp:ListItem Selected="True" Value="O">Ni&#241;o</asp:ListItem>
<asp:ListItem Value="A">Ni&#241;a</asp:ListItem>
<asp:ListItem Value="U">Ambos</asp:ListItem>
</asp:RadioButtonList>

                </td>
            <td width="20" __designer:mapid="2de">
                &nbsp;</td>
        </tr>
        <tr __designer:mapid="2df">
            <td height="10" width="20" __designer:mapid="2e0">
                &nbsp;</td>
            <td __designer:mapid="2e1">
                    <asp:Label runat="server" Text="Bater&#237;as:" ID="Label19"></asp:Label>

                </td>
            <td __designer:mapid="2e3">
                                <asp:DropDownList runat="server" AutoPostBack="True" CssClass="combo" 
                                    Width="150px" ID="ddlBateria" 
                                    OnSelectedIndexChanged="ddlBateria_SelectedIndexChanged"></asp:DropDownList>

                            </td>
            <td colspan="2" __designer:mapid="2e5">
                                <asp:Label runat="server" Text="Cantidad:" ID="Label20"></asp:Label>

                            </td>
            <td __designer:mapid="2e7">
                                <asp:TextBox runat="server" CssClass="inputNormal" Enabled="False" Width="30px" 
                                    ID="txtCantidadBaterias">0</asp:TextBox>

                            </td>
            <td align="right" __designer:mapid="2e9">
                <table class="style1" __designer:mapid="2ea">
                    <tr __designer:mapid="2eb">
                        <td align="right" __designer:mapid="2ec">
                                <asp:FileUpload runat="server" Width="130px" ID="fu1"></asp:FileUpload>

                            </td>
                        <td align="right" __designer:mapid="2ee">
                            <asp:ImageButton runat="server" ImageUrl="~/images/upload.png" ID="ibUpload" 
                                style="height: 16px" OnClick="ibUpload_Click"></asp:ImageButton>

                        </td>
                    </tr>
                </table>
            </td>
            <td width="20" __designer:mapid="2f0">
                &nbsp;</td>
        </tr>
        <tr __designer:mapid="2f1">
            <td height="10" width="20" __designer:mapid="2f2">
                &nbsp;</td>
            <td __designer:mapid="2f3">
                    <asp:Label runat="server" Text="Precio S/.:" ID="Label7"></asp:Label>

                </td>
            <td __designer:mapid="2f5">
                    <asp:TextBox runat="server" CssClass="inputNormalMoneda" ID="txtPrecio" 
                        placeholder="Precio" onkeypress="return ValidaNumeros(event);"></asp:TextBox>

                <asp:Label runat="server" Text="*" Font-Bold="True" Font-Size="10pt" 
                        ForeColor="#18AC85" ID="Label16"></asp:Label>

                </td>
            <td colspan="2" __designer:mapid="2f8">
                    <asp:Label runat="server" Text="Costo S/.:" ID="Label8"></asp:Label>

                </td>
            <td __designer:mapid="2fa">
                    <asp:TextBox runat="server" CssClass="inputNormalMoneda" ID="txtCosto" 
                        placeholder="Costo" onkeypress="return ValidaNumeros(event);"></asp:TextBox>

                </td>
            <td __designer:mapid="2fc">
                    <asp:Label runat="server" Text="C&#243;digo:" ID="Label25" Visible="False"></asp:Label>

                    <asp:Label runat="server" Font-Bold="True" ID="lblCodigo" Visible="False"></asp:Label>

                </td>
            <td width="20" __designer:mapid="2ff">
                &nbsp;</td>
        </tr>
        <tr __designer:mapid="300">
            <td height="10" width="20" __designer:mapid="301">
                &nbsp;</td>
            <td __designer:mapid="302">
                    <asp:Label runat="server" Text="Stock M&#237;nimo:" ID="Label9"></asp:Label>

                </td>
            <td __designer:mapid="304">
                    <asp:TextBox runat="server" CssClass="inputNormalMoneda" ID="txtStockMinimo" 
                        placeholder="Stock" style="text-transform:uppercase"></asp:TextBox>

                </td>
            <td colspan="2" __designer:mapid="306">
                &nbsp;</td>
            <td __designer:mapid="307">
                &nbsp;</td>
            <td __designer:mapid="308">
                    <asp:Label runat="server" ID="lblRuta" Visible="False"></asp:Label>

                </td>
            <td width="20" __designer:mapid="30a">
                &nbsp;</td>
        </tr>
        <tr __designer:mapid="30b">
            <td height="10" width="20" __designer:mapid="30c">
                &nbsp;</td>
            <td __designer:mapid="30d">
                    <asp:Label runat="server" Text="Marca:" ID="Label10"></asp:Label>

                </td>
            <td __designer:mapid="30f">
                    <asp:DropDownList runat="server" AutoPostBack="True" CssClass="combo" 
                        Width="150px" ID="ddlMarca" 
                        OnSelectedIndexChanged="ddlMarca_SelectedIndexChanged"></asp:DropDownList>

                </td>
            <td colspan="2" __designer:mapid="311">
                    <asp:Label runat="server" Text="Modelo:" ID="Label11"></asp:Label>

                </td>
            <td __designer:mapid="313">
                    <asp:DropDownList runat="server" CssClass="combo" Enabled="False" Width="150px" 
                        ID="ddlModelo"></asp:DropDownList>

                </td>
            <td __designer:mapid="315">
                    <asp:Label runat="server" ID="lblExtension" Visible="False"></asp:Label>

                </td>
            <td width="20" __designer:mapid="317">
                &nbsp;</td>
        </tr>
        <tr __designer:mapid="318">
            <td height="10" width="20" __designer:mapid="319">
                &nbsp;</td>
            <td __designer:mapid="31a">
                    <asp:Label runat="server" Text="Familia:" ID="Label12"></asp:Label>

                </td>
            <td __designer:mapid="31c">
                    <asp:DropDownList runat="server" AutoPostBack="True" CssClass="combo" 
                        Width="150px" ID="ddlCategoria" 
                        OnSelectedIndexChanged="ddlCategoria_SelectedIndexChanged"></asp:DropDownList>

                </td>
            <td colspan="2" __designer:mapid="31e">
                    <asp:Label runat="server" Text="Sub Familia:" ID="Label13"></asp:Label>

                </td>
            <td __designer:mapid="320">
                    <asp:DropDownList runat="server" CssClass="combo" Enabled="False" Width="150px" 
                        ID="ddlSubCategoria"></asp:DropDownList>

                </td>
            <td __designer:mapid="322">
                &nbsp;</td>
            <td width="20" __designer:mapid="323">
                &nbsp;</td>
        </tr>
        <tr __designer:mapid="324">
            <td height="10" width="20" __designer:mapid="325">
                &nbsp;</td>
            <td __designer:mapid="326">
                    <asp:Label runat="server" Text="Proveedor:" ID="Label14"></asp:Label>

                </td>
            <td __designer:mapid="328">
                    <asp:DropDownList runat="server" CssClass="combo" Width="150px" 
                        ID="ddlProveedor"></asp:DropDownList>

                </td>
            <td colspan="2" __designer:mapid="32a">
                    <asp:Label runat="server" Text="Estado:" ID="Label26"></asp:Label>

                </td>
            <td __designer:mapid="32c">
                    <asp:CheckBox runat="server" Checked="True" Text="Habilitado" ID="chkEstado"></asp:CheckBox>

                </td>
            <td __designer:mapid="32e">
                &nbsp;</td>
            <td width="20" __designer:mapid="32f">
                &nbsp;</td>
        </tr>

        <tr __designer:mapid="324">
            <td height="10" width="20" __designer:mapid="325">
                &nbsp;</td>
            <td __designer:mapid="326">
                    &nbsp;</td>
            <td __designer:mapid="328">
                    &nbsp;</td>
            <td colspan="2" __designer:mapid="32a">
                    &nbsp;</td>
            <td __designer:mapid="32c">
                    &nbsp;</td>
            <td __designer:mapid="32e">
                &nbsp;</td>
            <td width="20" __designer:mapid="32f">
                &nbsp;</td>
        </tr>

        <tr __designer:mapid="324">
            <td height="10" width="20" __designer:mapid="325">
                &nbsp;</td>
            <td __designer:mapid="326" colspan="6">
            <table width="100%"><tr><td width="65">
                
                <asp:ImageButton ID="btnGuardarProducto" runat="server" ImageUrl="~/images/Guardar.jpg" 
                    onclick="btnGuardarProducto_Click" />
                <cc1:ConfirmButtonExtender ID="btnGuardarProducto_ConfirmButtonExtender" runat="server" 
                    ConfirmText="¿Seguro de guardar los datos?" Enabled="True" 
                    TargetControlID="btnGuardarProducto">
                </cc1:ConfirmButtonExtender>
                
                </td>
                <td>
                   
                    <asp:ImageButton ID="btnSalirProducto" runat="server" ImageUrl="~/images/Salir.jpg" 
                        onclick="btnSalirProducto_Click" />
                   
                </td>
                </tr></table>

                </td>
            <td width="20" __designer:mapid="32f">
                &nbsp;</td>
        </tr>

                            <tr __designer:mapid="330">
                                <td height="10" width="20" __designer:mapid="331">
                                    &nbsp;</td>
                                <td __designer:mapid="332">
                                    &nbsp;</td>
                                <td __designer:mapid="333">
                                    &nbsp;</td>
                                <td colspan="2" __designer:mapid="334">
                                    &nbsp;</td>
                                <td __designer:mapid="335">
                                    &nbsp;</td>
                                <td align="right" __designer:mapid="336">
                                    <asp:Label runat="server" Text="* Campos obligatorios" Font-Bold="False" 
                                        Font-Size="10pt" ForeColor="#18AC85" ID="Label17"></asp:Label>

                                </td>
                                <td width="20" __designer:mapid="338">
                                    &nbsp;</td>
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
    </tr>
        <tr>
            <td width="15%">
                &nbsp;</td>
            <td>
                &nbsp;</td>
    </tr>
    </table>
</asp:Content>

