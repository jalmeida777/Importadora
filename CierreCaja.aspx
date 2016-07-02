<%@ Page Title="" Language="C#" MasterPageFile="~/Plantilla.master" AutoEventWireup="true" CodeFile="CierreCaja.aspx.cs" Inherits="CierreCaja" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script src="js/jquery.growl.js" type="text/javascript"></script>
  <link href="css/jquery.growl.css" rel="stylesheet" type="text/css" />
    <style type="text/css">


      .style1
      {
          width: 100%;
      }
      </style>

          <script type="text/javascript">


              function ValidaNumeros(e) {
                  var tecla = document.all ? tecla = e.keyCode : tecla = e.which;
                  if (tecla > 31 && (tecla < 48 || tecla > 57) && tecla != 46)
                      return false;
              }


 
 </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="toolbar">
        <table width="100%">
            <tr>
                <td width="95">
                    <asp:ImageButton ID="btnGuardar" runat="server" ImageUrl="~/images/btnGuardar_New.png" 
                    onclick="btnGuardar_Click" 
                        
                        OnClientClick="if (confirm('Seguro de cerrar la caja?')) { btnGuardar.disabled = false; return true; } else { return false; }" 
                        Enabled="False" />
                </td>
                <td>
                    <asp:ImageButton ID="btnSalir" runat="server" ImageUrl="~/images/btnSalir_New.png" 
                        onclick="btnSalir_Click" />
                </td>
            </tr>
        </table>
    </div>
    <table width="100%" runat="server" id="tblGeneral"
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
                            <td width="20">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                &nbsp;</td>
                            <td>
                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="20pt" 
                    Text="Cierre de Caja" ForeColor="#4C4C4C"></asp:Label>
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
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933" 
                                            width="100">
                                <asp:Label ID="Label30" runat="server" Text="Sucursal:" ForeColor="#4C4C4C"></asp:Label>
                                        </td>
                                        <td style="padding-left: 5px">
                <table cellpadding="0" cellspacing="0" class="style1">
                    <tr>
                        <td width="160">
                <asp:DropDownList ID="ddlTienda" runat="server" CssClass="combo" Width="150px" 
                                ClientIDMode="Static">
                </asp:DropDownList>
                        </td>
                        <td>
                            <asp:ImageButton ID="ibEstablecerSucursal" runat="server" 
                                ImageUrl="~/images/auth_ok.png" 
                                ToolTip="Establecer Sucursal"  
                                ClientIDMode="Static" CausesValidation="False" 
                                onclick="ibEstablecerSucursal_Click"/>
                        </td>
                    </tr>
                </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933" 
                                            width="100">
                                            <asp:Label ID="Label2" runat="server" Text="Fecha:" ForeColor="#4C4C4C"></asp:Label>
                                        </td>
                                        <td style="padding-left: 5px">
                                            <asp:Label ID="lblFecha" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933" 
                                            width="100">
                                            <asp:Label ID="Label29" runat="server" Text="Caja Final:" ForeColor="#4C4C4C"></asp:Label>
                                        </td>
                                        <td style="padding-left: 5px">
                                            <asp:Label ID="lblCajaFinal" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933">
                                            <asp:Label ID="Label28" runat="server" Text="Monto S/.:"></asp:Label>
                                        </td>
                                        <td style="padding-left: 5px">
                                            <asp:TextBox ID="txtmonto" runat="server" CssClass="inputNormalMoneda" 
                                                onkeypress="return ValidaNumeros(event);" Enabled="False" Width="70px">0.00</asp:TextBox>
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
                            <td class="style1">
                                &nbsp;</td>
                            <td>
                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                &nbsp;</td>
                            <td>
                                <table width="300">
                                    <tr>
                                        <td rowspan="2" width="60">
                                            <asp:ImageButton ID="ibUsuarioRegistro" runat="server" Height="50px" 
                                ImageUrl="~/images/face.jpg" Width="50px" />
                                        </td>
                                        <td>
                            Creado por:
                                            <asp:Label ID="lblUsuarioCierre" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblFechaCierre" runat="server"></asp:Label>
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

