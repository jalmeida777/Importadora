<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>

    <link rel="stylesheet" type="text/css" href="css/demo.css" />
</head>
<body background="images/fondo.jpg">
    <form id="form1" runat="server">
    <div style="position: absolute; width: 300px; height: 200px; margin: auto; top: 150px; right: 0px; bottom: 0px; left: 0px;">
    
        <table cellpadding="4" cellspacing="4" width="100%">
            <tr>
                <td class="label">
                    <asp:Label ID="Label1" runat="server" Text="Usuario:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtUsuario" runat="server" CssClass="inputNormal" 
                        Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="Label2" runat="server" Text="Contraseña:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtContraseña" runat="server" TextMode="Password" 
                        CssClass="inputNormal" Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:ImageButton ID="btnEntrar" runat="server" ImageUrl="~/images/Entrar.gif" 
                        onclick="btnEntrar_Click" />
                </td>
            </tr>
        </table>
    
    </div>

    <div style="margin: auto; position: absolute; top: 80px; right: 0px; width: 520px; height: 100px; left: 0px;">
    </div>
    </form>
</body>
</html>
