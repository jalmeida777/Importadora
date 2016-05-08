<%@ Page Title="Empresa" Language="C#" MasterPageFile="~/Plantilla.master" AutoEventWireup="true" CodeFile="Empresa.aspx.cs" Inherits="Empresa" %>

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

    

 </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divBusqueda">
        <table width="100%">
            <tr>
                <td>
                    <h1 class="label">
                            Empresa</h1>
                </td>
            </tr>
        </table>
    </div>


 <div class="toolbar">
            <table width="100%"><tr><td width="65">
               
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
                </tr></table>
            </div>
                         <table width="100%" 
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
            <td width="100">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td>
                <asp:Label ID="Label20" runat="server" Text="Razón Social:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="inputNormal" placeholder="Descripción"
                    Width="300px" style="text-transform:uppercase" MaxLength="200"></asp:TextBox>
                <asp:Label ID="Label16" runat="server" Font-Bold="True" ForeColor="#18AC85" 
                        Text="*"></asp:Label>
            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td>
                <asp:Label ID="Label23" runat="server" Text="Ruc:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtRuc" runat="server" CssClass="inputNormal" MaxLength="11" onkeypress="return ValidaEntero(event);"></asp:TextBox>
                <asp:Label ID="Label26" runat="server" Font-Bold="True" ForeColor="#18AC85" 
                        Text="*"></asp:Label>
            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td>
                <asp:Label ID="Label24" runat="server" Text="Dirección:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDireccion" runat="server" CssClass="inputNormal" 
                    MaxLength="1000" Width="300px"></asp:TextBox>
                <asp:Label ID="Label27" runat="server" Font-Bold="True" ForeColor="#18AC85" 
                        Text="*"></asp:Label>
            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td>
                <asp:Label ID="Label25" runat="server" Text="Teléfono:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="inputNormal" 
                    MaxLength="50"></asp:TextBox>
            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td>
                <asp:Label ID="Label28" runat="server" Text="Logo:"></asp:Label>
            </td>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="205">
                    <tr>
                        <td colspan="2">
                <asp:Image runat="server" ImageUrl="~/images/Prev.jpg" Height="70px" Width="180px" 
                                ID="ibImagen"></asp:Image>

                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                                <asp:FileUpload runat="server" Width="130px" ID="fu1"></asp:FileUpload>

                            </td>
                        <td align="right">
                            <asp:ImageButton runat="server" ImageUrl="~/images/upload.png" ID="ibUpload" 
                                style="height: 16px" OnClick="ibUpload_Click"></asp:ImageButton>

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
                <asp:Label ID="Label22" runat="server" Text="Estado:"></asp:Label>
            </td>
            <td>
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
            <td>
                <asp:Label ID="lblRuta" runat="server" Visible="False"></asp:Label>
            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        </table>
        <tr>
            <td width="15%">
                &nbsp;</td>
            <td>
                &nbsp;</table>

                         </asp:Content>

