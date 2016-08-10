<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrearBateria2.aspx.cs" Inherits="CrearBateria2" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Ingreso</title>
    <script src="js/jquery.growl.js" type="text/javascript"></script>
<link href="css/jquery.growl.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/demo.css" />
</head>
<body style="margin-top: 0px; margin-left: 0px">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

        <div class="divBusqueda">
            <table width="100%">
                <tr>
                    <td>
                        <h1>
                            Crear
                            Batería</h1>
                    </td>
                </tr>
                </table>
            </div>


 <div class="toolbar">
            <table width="100%"><tr><td width="65">
               
                            <asp:ImageButton ID="btnGuardar" runat="server" 
                                ImageUrl="~/images/btnGuardar_New.png" onclick="btnGuardar_Click" />
                            <cc1:ConfirmButtonExtender ID="btnGuardar_ConfirmButtonExtender" runat="server" 
                                ConfirmText="¿Seguro de guardar los datos?" Enabled="True" 
                                TargetControlID="btnGuardar">
                            </cc1:ConfirmButtonExtender>
               
                </td>
                <td width="65">
                    
                            &nbsp;</td>
                <td>
                   
                </td>
                </tr></table>
            </div>
                         <table width="100%" 
        style="background-image: url('images/form_sheetbg.png'); background-repeat: repeat; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #ddd;">
        <tr>
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
            <td width="120">
                <asp:Label ID="Label2" runat="server" Text="Descripción:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="inputNormal" placeholder="Descripción"
                    Width="300px" style="text-transform:uppercase"></asp:TextBox>
            </td>
            <td>
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
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;
                </td>
                </tr>
                </table>

    </form>
</body>
</html>

