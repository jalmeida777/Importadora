<%@ Page Title="" Language="C#" MasterPageFile="~/Plantilla.master" AutoEventWireup="true" CodeFile="FlujoCajaDiaria.aspx.cs" Inherits="FlujoCajaDiaria" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<script src="js/jquery.growl.js" type="text/javascript"></script>
<link href="css/jquery.growl.css" rel="stylesheet" type="text/css" />

    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        
        <div class="divBusqueda" __designer:mapid="92">
            <table width="100%" __designer:mapid="93">
                <tr __designer:mapid="94">
                    <td colspan="5" __designer:mapid="95">
                        <h1 __designer:mapid="96">
                            Movimientos de Caja</h1>
                    </td>
                </tr>
                <tr __designer:mapid="97">
                    <td width="60" style="padding-left: 10px" __designer:mapid="98">
                            <asp:Label ID="Label33" runat="server" Text="Fecha:" ForeColor="#4C4C4C"></asp:Label>
                    </td>
                    <td width="110" __designer:mapid="9a">
                            <asp:TextBox ID="txtFecha" runat="server" CssClass="inputsFecha" 
                                MaxLength="10"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                                TargetControlID="txtFecha">
                            </cc1:CalendarExtender>
                    </td>
                    <td width="70" __designer:mapid="a2">
                        <asp:Label ID="Label3" runat="server" Font-Bold="False" Text="Sucursal:"></asp:Label>
                    </td>
                    <td width="210" __designer:mapid="a4">
                <asp:DropDownList ID="ddlTienda" runat="server" CssClass="combo" Width="150px" 
                                ClientIDMode="Static">
                </asp:DropDownList>
                    </td>
                    <td __designer:mapid="a6">
                        
                        &nbsp;</td>
                </tr>
            </table>
            </div>
            <div class="toolbar" __designer:mapid="aa">
            <table width="100%" __designer:mapid="ab"><tr __designer:mapid="ac">
                <td width="95" __designer:mapid="af">
                    <asp:ImageButton ID="btnConsultar" runat="server" ImageUrl="~/images/btnBuscar_New.png" 
                        onclick="btnConsultar_Click" ToolTip="Buscar" />
                </td>
                <td align="left" __designer:mapid="b1">
                    <asp:ImageButton ID="btnSalir" runat="server" ImageUrl="~/images/btnSalir_New.png" 
                        onclick="btnSalir_Click" />

              
                </td>
                </tr></table>
            </div>
    <table width="100%" cellspacing="5" id="tblConsulta" runat="server">
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
                <table width="100%">
                    <tr>
                        <td>
                            <table border="0" width="100%" cellpadding="5">
                                <tr>
                                    <td width="120">
                            <asp:Label ID="Label2" runat="server" Text="Caja Inicial:" ForeColor="#4C4C4C" 
                                            Font-Names="Segoe UI Light" Font-Size="14pt"></asp:Label>
                                    </td>
                                    <td class="data" align="right" width="100">
                            <asp:Label ID="lblCajaInicial" runat="server" Font-Bold="True" Font-Names="Segoe UI Light" 
                                            Font-Size="14pt">0.00</asp:Label>
                                    </td>
                                    <td class="data" width="120">
                            <asp:Label ID="Label34" runat="server" Text="Ventas:" 
                                ForeColor="#4C4C4C" style="text-align: justify" Font-Names="Segoe UI Light" 
                                            Font-Size="14pt"></asp:Label>
                                    </td>
                                    <td class="data">
                            <asp:Label ID="lblTotalVentas" runat="server" ForeColor="#006600" Font-Bold="True" 
                                            Font-Names="Segoe UI Light" Font-Size="14pt">0.00</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                <asp:GridView ID="gvReciboCaja" runat="server" AutoGenerateColumns="False" 
                    CssClass="grid" DataKeyNames="i_IdCaja" ShowFooter="True">
                    <Columns>
                        <asp:BoundField DataField="TipoDocumento" HeaderText="Documento" >
                        <ItemStyle Width="100px" HorizontalAlign="Left" Font-Names="Segoe UI Light" 
                            Font-Size="14pt" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Número" DataField="v_NroDocumento" >
                        <ItemStyle Width="100px" HorizontalAlign="Left" Font-Names="Segoe UI Light" 
                            Font-Size="14pt" />
                        </asp:BoundField>
                        <asp:BoundField DataField="d_FechaMovimiento" HeaderText="Hora">
                        <ItemStyle Width="100px" Font-Names="Segoe UI Light" Font-Size="14pt" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Movimiento" DataField="c_TipoMovimiento">
                        <FooterStyle Font-Bold="True" Font-Names="Segoe UI Light" 
                            Font-Size="14pt" HorizontalAlign="Right" />
                        <ItemStyle Width="100px" HorizontalAlign="Right" 
                            Font-Names="Segoe UI Light" Font-Size="14pt" />
                        </asp:BoundField>
                        <asp:BoundField DataField="f_Importe" HeaderText="Importe" 
                            DataFormatString="{0:C}">
                        <FooterStyle Font-Bold="True" Font-Names="Segoe UI Light" Font-Size="14pt" 
                            HorizontalAlign="Right" />
                        <ItemStyle Font-Names="Segoe UI Light" Font-Size="14pt" HorizontalAlign="Right" 
                            Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="v_Usuario" HeaderText="Usuario">
                        <ItemStyle Font-Names="Segoe UI Light" Font-Size="14pt" Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Concepto" HeaderText="Concepto">
                        <ItemStyle Font-Names="Segoe UI Light" Font-Size="14pt" Width="100px" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <table border="0" width="100%" cellpadding="5">
                                <tr>
                                    <td width="120">
                            <asp:Label ID="Label29" runat="server" Text="Total Ingresos:" 
                                ForeColor="#4C4C4C" style="text-align: justify" Font-Names="Segoe UI Light" 
                                            Font-Size="14pt"></asp:Label>
                                    </td>
                                    <td class="data" align="right" width="100">
                            <asp:Label ID="lblTotalIngresos" runat="server" ForeColor="#006600" Font-Bold="True" 
                                            Font-Names="Segoe UI Light" Font-Size="14pt">0.00</asp:Label>
                                    </td>
                                    <td class="data">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                            <asp:Label ID="Label30" runat="server" Text="Total Salidas:" 
                                ForeColor="#4C4C4C" Font-Names="Segoe UI Light" Font-Size="14pt"></asp:Label>
                                    </td>
                                    <td class="data" align="right">
                            <asp:Label ID="lblTotalSalidas" runat="server" ForeColor="#CC0000" Font-Bold="True" 
                                            Font-Names="Segoe UI Light" Font-Size="14pt">0.00</asp:Label>
                                    </td>
                                    <td class="data">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                            <asp:Label ID="Label28" runat="server" Text="Caja Final:" Font-Names="Segoe UI Light" 
                                            Font-Size="14pt"></asp:Label>
                                    </td>
                                    <td class="data" align="right">
                            <asp:Label ID="lblCajaFinal" runat="server" Font-Bold="True" Font-Names="Segoe UI Light" 
                                            Font-Size="14pt">0.00</asp:Label>
                                    </td>
                                    <td class="data">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                            <asp:Label ID="Label32" runat="server" Text="Caja Real:" Font-Names="Segoe UI Light" 
                                            Font-Size="14pt"></asp:Label>
                                    </td>
                                    <td class="data" align="right">
                            <asp:Label ID="lblCajaReal" runat="server" Font-Bold="True" Font-Names="Segoe UI Light" 
                                            Font-Size="14pt">0.00</asp:Label>
                                    </td>
                                    <td class="data">
                                        &nbsp;</td>
                                </tr>
                            </table>
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
                                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

