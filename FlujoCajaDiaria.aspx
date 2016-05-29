<%@ Page Title="" Language="C#" MasterPageFile="~/Plantilla.master" AutoEventWireup="true" CodeFile="FlujoCajaDiaria.aspx.cs" Inherits="FlujoCajaDiaria" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                    Text="Flujo de Caja Diaria" ForeColor="#4C4C4C" style="text-align: center"></asp:Label>
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
                        <td>
                            <table border="0" width="100%">
                                <tr>
                                    <td class="label" width="120">
                            <asp:Label ID="Label33" runat="server" Text="Fecha:" ForeColor="#4C4C4C"></asp:Label>
                                    </td>
                                    <td class="data">
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td width="100">
                            <asp:TextBox ID="txtFecha" runat="server" CssClass="inputsFecha" 
                                MaxLength="10"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                                TargetControlID="txtFecha">
                            </cc1:CalendarExtender>
                                                </td>
                                                <td>
                            <asp:ImageButton ID="btnConsultar" runat="server" 
                                ImageUrl="~/images/Buscar.jpg" onclick="btnConsultar_Click" 
                                ToolTip="Buscar" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                            <asp:Label ID="Label2" runat="server" Text="Caja Inicial:" ForeColor="#4C4C4C"></asp:Label>
                                    </td>
                                    <td class="data">
                            <asp:Label ID="lblCajaInicial" runat="server"></asp:Label>
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
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933">
                <asp:GridView ID="gvReciboCaja" runat="server" AutoGenerateColumns="False" 
                    CssClass="grid" DataKeyNames="i_IdCaja">
                    <Columns>
                        <asp:BoundField HeaderText="Documento" DataField="v_NroDocumento" >
                        <ItemStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="d_FechaMovimiento" HeaderText="Fecha-Hora">
                        <ItemStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="v_Descripcion" HeaderText="Descripcion" >
                        <ItemStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="f_Ingreso" HeaderText="Ingreso">
                        <ItemStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Salida" DataField="f_Salida">
                        <ItemStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="f_Saldo" HeaderText="Saldo">
                        <ItemStyle Width="100px" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle CssClass="footer" />
                </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <table border="0" width="100%">
                                <tr>
                                    <td class="label" width="120">
                            <asp:Label ID="Label29" runat="server" Text="Total Ingresos:" 
                                ForeColor="#4C4C4C" style="text-align: justify"></asp:Label>
                                    </td>
                                    <td class="data">
                            <asp:Label ID="lblTotalIngresos" runat="server" ForeColor="#339966"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                            <asp:Label ID="Label30" runat="server" Text="Total Salidas:" 
                                ForeColor="#4C4C4C"></asp:Label>
                                    </td>
                                    <td class="data">
                            <asp:Label ID="lblTotalSalidas" runat="server" ForeColor="#CC0000"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                            <asp:Label ID="Label31" runat="server" Text="Total Ventas:" 
                                                ForeColor="#4C4C4C"></asp:Label>
                                    </td>
                                    <td class="data">
                            <asp:Label ID="lblTotalVentas" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                            <asp:Label ID="Label28" runat="server" Text="Caja Final:"></asp:Label>
                                    </td>
                                    <td class="data">
                            <asp:Label ID="lblCajaFinal" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                            <asp:Label ID="Label32" runat="server" Text="Caja Real:"></asp:Label>
                                    </td>
                                    <td class="data">
                            <asp:Label ID="lblCajaReal" runat="server"></asp:Label>
                                    </td>
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
                <table width="300">
                    <tr>
                        <td rowspan="2" width="60">
                            <asp:ImageButton ID="ibUsuarioRegistro" runat="server" Height="50px" 
                                ImageUrl="~/images/face.jpg" Width="50px" />
                        </td>
                        <td>
                            Creado por:
                                            <asp:Label ID="lblUsuarioRegistro" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblFechaRegistro" runat="server"></asp:Label>
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
</asp:Content>

