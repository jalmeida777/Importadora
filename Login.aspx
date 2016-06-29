<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ingreso</title>

    <link rel="stylesheet" type="text/css" href="css/demo.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="border: 1px solid #CCCCCC; position: absolute; width: 370px; height: 270px; margin: auto; top: 0px; right: 0px; bottom: 0px; left: 0px;">
    
        <table cellpadding="4" cellspacing="4" width="350">
            <tr>
                <td width="30">
                    &nbsp;</td>
                <td colspan="2" style="height: 70px">
                    <asp:Label ID="Label3" runat="server" Font-Names="Segoe UI Light" 
                        Font-Size="24pt" Text="Ingreso"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td colspan="2">
                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="VillarToys"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td colspan="2">
                    <asp:TextBox ID="txtUsuario" runat="server" CssClass="inputNormalLogin" 
                        Width="300px" placeholder="Usuario" Height="35px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td colspan="2">
                    <asp:TextBox ID="txtContraseña" runat="server" TextMode="Password" 
                        CssClass="inputNormalLogin" Width="300px" placeholder="Contraseña" 
                        Height="35px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td align="left">
                    <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="#0066FF" 
                        PostBackUrl="~/AperturaCaja.aspx">Olvidé mi contraseña</asp:LinkButton>
                </td>
                <td align="right">
                    <asp:ImageButton ID="btnEntrar" runat="server" ImageUrl="~/images/Entrar.gif" 
                        onclick="btnEntrar_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td align="left">
                    &nbsp;</td>
                <td align="right">
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>


    </form>
</body>
</html>
