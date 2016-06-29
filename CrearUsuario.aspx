<%@ Page Title="Usuarios" Language="C#" MasterPageFile="~/Plantilla.master" AutoEventWireup="true" CodeFile="CrearUsuario.aspx.cs" Inherits="CrearUsuario" %>

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
                        Usuario</h1>
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
                                <asp:Label ID="Label1" runat="server" Text="Código:" Visible="False"></asp:Label>
                                <asp:Label ID="lblCodigo" runat="server" Visible="False"></asp:Label>
                            </td>
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
                            <td>
                                <asp:Label ID="Label17" runat="server" Text="Nombre:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="inputNormal" placeholder="NOMBRES Y APELLIDOS"
                    Width="300px" style="text-transform:uppercase" MaxLength="100" TabIndex="1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ControlToValidate="txtNombre" ErrorMessage="Campo obligatorio" Font-Bold="True" 
                                    ForeColor="#18AC85" SetFocusOnError="True">*Campo obligatorio</asp:RequiredFieldValidator>
                            </td>
                            <td colspan="3" rowspan="6" align="left" valign="top">
                                <table class="style1">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label24" runat="server" Text="Foto:"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
        <asp:ImageButton runat="server" ImageUrl="~/images/Prev.jpg" Height="134px" Width="164px" ID="ibImagen" 
                                    BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"></asp:ImageButton>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table width="130">
                                                <tr>
                                                    <td width="130">
                                <asp:FileUpload runat="server" Width="130px" ID="fu1"></asp:FileUpload>

                                                    </td>
                                                    <td>
                            <asp:ImageButton runat="server" ImageUrl="~/images/upload.png" ID="ibUpload" 
                                    OnClick="ibUpload_Click" Width="16px"></asp:ImageButton>

                                                    </td>
                                                </tr>
                                            </table>
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
                                <asp:Label ID="Label25" runat="server" Text="Email:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="inputNormal" 
                                    MaxLength="100" Width="300px"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                    ControlToValidate="txtEmail" ErrorMessage="Email incorrecto" Font-Bold="True" 
                                    ForeColor="#18AC85" SetFocusOnError="True" 
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*Email Incorrecto</asp:RegularExpressionValidator>
                            </td>
                            <td width="20">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td height="10" width="20">
                &nbsp;</td>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Usuario:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtUsuario" runat="server" CssClass="inputNormal" placeholder="USUARIO"
                    Width="100px" style="text-transform:uppercase" MaxLength="30" TabIndex="2"></asp:TextBox>
                <asp:Label ID="Label20" runat="server" Font-Bold="True" ForeColor="#18AC85" 
                        Text="*"></asp:Label>
                                <asp:LinkButton ID="lbContraseña" runat="server" Font-Underline="True" 
                                    ForeColor="Blue" onclick="lbContraseña_Click" Visible="False">Cambiar Contraseña</asp:LinkButton>
                            </td>
                            <td width="20">
                                &nbsp;</td>
                        </tr>
                        <tr id="filaContraseña" runat="server" visible="False">
                            <td height="10" width="20">
                &nbsp;</td>
                            <td>
                                <asp:Label ID="Label18" runat="server" Text="Contraseña:"></asp:Label>
                            </td>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0" width="360">
                                    <tr>
                                        <td width="140">
                                <asp:TextBox ID="txtPwd" runat="server" CssClass="inputNormal" 
                    MaxLength="10" TextMode="Password" Width="100px" TabIndex="3"></asp:TextBox>
                <asp:Label ID="Label21" runat="server" Font-Bold="True" ForeColor="#18AC85" 
                        Text="*"></asp:Label>
                                        </td>
                                        <td width="70">
                                            <asp:Label ID="Label22" runat="server" Text="Repetir:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPwd2" runat="server" CssClass="inputNormal" Width="100px" 
                                                TextMode="Password" TabIndex="4" MaxLength="10"></asp:TextBox>
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
                                <asp:Label ID="Label19" runat="server" Text="Rol:"></asp:Label>
                            </td>
                            <td>
                    <asp:DropDownList runat="server" CssClass="combo" Width="150px" ID="ddlRol" TabIndex="5"></asp:DropDownList>

                            </td>
                            <td width="20">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td height="10" width="20">
                                &nbsp;</td>
                            <td>
                                <asp:Label ID="Label23" runat="server" Text="Sucursales:"></asp:Label>
                            </td>
                            <td>
                                <asp:CheckBoxList ID="chkSucursal" runat="server" TabIndex="6">
                                </asp:CheckBoxList>

                            </td>
                            <td width="20">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20" class="style3">
                            </td>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Estado:"></asp:Label>
                            </td>
                            <td class="style3">
                                <asp:CheckBox ID="chkEstado" runat="server" Checked="True" Text="Habilitado" 
                                    TabIndex="7" />
                            </td>
                            <td width="130px" align="left">
                    <asp:Label runat="server" ID="lblRuta" Visible="False"></asp:Label>

                            </td>
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

