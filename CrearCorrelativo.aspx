<%@ Page Title="" Language="C#" MasterPageFile="~/Plantilla.master" AutoEventWireup="true" CodeFile="CrearCorrelativo.aspx.cs" Inherits="CrearCorrelativo" %>

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
                        Correlativo</h1>
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
                <td>
                    <asp:ImageButton ID="btnSalir" runat="server" ImageUrl="~/images/Salir.jpg" 
                                onclick="btnSalir_Click" />
                </td>
            </tr>
        </table>
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
                            <td width="140">
                                &nbsp;</td>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Código:" Visible="False"></asp:Label>
                                <asp:Label ID="lblCodigo" runat="server" Visible="False"></asp:Label>
                            </td>
                            <td width="20">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td height="10" width="20">
                &nbsp;</td>
                            <td>
                                <asp:Label ID="Label19" runat="server" Text="Documento:"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDocumento" runat="server" CssClass="combo" 
                    Width="200px" Enabled="False">
                                </asp:DropDownList>
                            </td>
                            <td width="20">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td height="10" width="20">
                &nbsp;</td>
                            <td>
                                <asp:Label ID="Label18" runat="server" Text="Sucursal:"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSucursal" runat="server" CssClass="combo" 
                    Width="280px" Enabled="False">
                                </asp:DropDownList>
                            </td>
                            <td width="20">
                &nbsp;</td>
                        </tr>
                        <tr>
                            <td height="10" width="20">
                                &nbsp;</td>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Serie:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSerie" runat="server" CssClass="inputNormal" placeholder="Descripción"
                    Width="50px" style="text-transform:uppercase" MaxLength="3"></asp:TextBox>
                                <asp:Label ID="Label17" runat="server" Font-Bold="True" ForeColor="#18AC85" 
                        Text="*"></asp:Label>
                            </td>
                            <td width="20">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td height="10" width="20">
                                &nbsp;</td>
                            <td>
                                <asp:Label ID="Label20" runat="server" Text="Correlativo Inicial:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCorrInicial" runat="server" CssClass="inputNormal" placeholder="Descripción"
                    Width="200px" style="text-transform:uppercase" MaxLength="12"></asp:TextBox>
                                <asp:Label ID="Label24" runat="server" Font-Bold="True" ForeColor="#18AC85" 
                        Text="*"></asp:Label>
                            </td>
                            <td width="20">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td height="10" width="20">
                                &nbsp;</td>
                            <td>
                                <asp:Label ID="Label21" runat="server" Text="Correlativo Final:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCorrFinal" runat="server" CssClass="inputNormal" placeholder="Descripción"
                    Width="200px" style="text-transform:uppercase" MaxLength="12"></asp:TextBox>
                                <asp:Label ID="Label25" runat="server" Font-Bold="True" ForeColor="#18AC85" 
                        Text="*"></asp:Label>
                            </td>
                            <td width="20">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td height="10" width="20">
                                &nbsp;</td>
                            <td>
                                <asp:Label ID="Label22" runat="server" Text="Correlativo Actual:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCorrActual" runat="server" CssClass="inputNormal" placeholder="Descripción"
                    Width="200px" style="text-transform:uppercase" MaxLength="12"></asp:TextBox>
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
                                <asp:Label ID="Label23" runat="server" Text="Nro Autorización:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAutorizacion" runat="server" CssClass="inputNormal" placeholder="Descripción"
                    Width="200px" style="text-transform:uppercase" MaxLength="50"></asp:TextBox>
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
        <tr>
            <td width="15%">
                &nbsp;</td>
            <td>
                &nbsp;</table>
</asp:Content>

