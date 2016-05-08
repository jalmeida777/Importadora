<%@ Page Title="Asignar Permisos" Language="C#" MasterPageFile="~/Plantilla.master" AutoEventWireup="true" CodeFile="AsignarPermisos.aspx.cs" Inherits="AsignarPermisos" %>

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
                        Asignar Permisos</h1>
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
                            <td height="10" width="20">
                &nbsp;</td>
                            <td class="label" width="50">
                                <asp:Label ID="Label2" runat="server" Text="Rol:"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlRol" runat="server" AutoPostBack="True" 
                                    CssClass="combo" onselectedindexchanged="ddlRol_SelectedIndexChanged" 
                                    Width="200px">
                                </asp:DropDownList>
                            </td>
                            <td colspan="2" align="left" valign="top">
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
                                <asp:TreeView ID="Arbol" runat="server" ShowCheckBoxes="All" 
                                    onselectednodechanged="Arbol_SelectedNodeChanged">
                                </asp:TreeView>
                            </td>
                            <td colspan="2" align="left" valign="top">
                                <asp:Panel ID="panelPermisos" runat="server" BackColor="#E4E4E4" 
                                    BorderColor="#EEEEEE" BorderStyle="Solid" BorderWidth="4px" Height="100px" 
                                    Width="100px">
                                    <asp:CheckBoxList ID="chkPermisos" runat="server" AutoPostBack="True" 
                                        onselectedindexchanged="chkPermisos_SelectedIndexChanged">
                                    </asp:CheckBoxList>
                                </asp:Panel>
                            </td>
                            <td width="20">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20" class="style3">
                            </td>
                            <td>
                                &nbsp;</td>
                            <td class="style3">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td class="style3">
                            </td>
                            <td width="20" class="style3">
                            </td>
                        </tr>
                        <tr>
                            <td height="10" width="20">
                &nbsp;</td>
                            <td class="style2">
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
                    </table>
        <tr>
            <td width="15%">
                &nbsp;</td>
            <td>
                &nbsp;</table>
</asp:Content>

