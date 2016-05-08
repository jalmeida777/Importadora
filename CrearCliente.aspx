<%@ Page Title="" Language="C#" MasterPageFile="~/Plantilla.master" AutoEventWireup="true" CodeFile="CrearCliente.aspx.cs" Inherits="CrearCliente" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<script src="js/jquery.growl.js" type="text/javascript"></script>
<link href="css/jquery.growl.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divBusqueda">
        <table width="100%">
            <tr>
                <td>
                    <h1 class="label">
                        Cliente</h1>
                </td>
            </tr>
        </table>
    </div>
    <div class="toolbar">
        <table width="100%">
            <tr>
                <td width="65">
                    <asp:ImageButton ID="btnGuardar" runat="server" 
                                ImageUrl="~/images/Guardar.jpg" onclick="btnGuardar_Click" />
                    <cc1:ConfirmButtonExtender ID="btnGuardar_ConfirmButtonExtender" runat="server" 
                                ConfirmText="¿Seguro de guardar los datos?" Enabled="True" 
                                TargetControlID="btnGuardar">
                    </cc1:ConfirmButtonExtender>
                </td>
                <td width="65">
                    <asp:ImageButton ID="btnSalir" runat="server" ImageUrl="~/images/Salir.jpg" 
                                onclick="btnSalir_Click" />
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    
     <table width="100%" runat="server" id="tblCliente"
        
        style="background-image: url('images/form_sheetbg.png'); background-repeat: repeat; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #ddd;" 
        visible="True">
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
            <td colspan="2">
                                <asp:Label ID="Label1" runat="server" Text="Código:" Visible="False"></asp:Label>
                                <asp:Label ID="lblCodigo" runat="server" Visible="False"></asp:Label>
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
                <asp:Label ID="Label42" runat="server" Text="Fecha Cumpleaños:"></asp:Label>
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
                <asp:Label ID="Label44" runat="server" Text="Genero:"></asp:Label>
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
                <asp:Label ID="Label43" runat="server" Text="Celular:"></asp:Label>
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
                <asp:Label ID="Label45" runat="server" Text="Distrito:"></asp:Label>
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
                    Width="210px"></asp:TextBox>
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
                &nbsp;</td>
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
     
</asp:Content>

