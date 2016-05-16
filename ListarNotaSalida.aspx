<%@ Page Title="Nota de Salida" Language="C#" MasterPageFile="~/Plantilla.master" AutoEventWireup="true" CodeFile="ListarNotaSalida.aspx.cs" Inherits="ListarNotaSalida" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="divBusqueda">
                <table width="100%">
                    <tr>
                        <td colspan="11">
                            <h1 class="label">
                                Notas de Salida</h1>
                        </td>
                    </tr>
                    <tr>
                        <td class="label" width="90">
                            <asp:Label ID="Label1" runat="server" Font-Bold="False" Text="Fecha Inicial:"></asp:Label>
                        </td>
                        <td width="110">
                            <asp:TextBox ID="txtFechaInicial" runat="server" CssClass="inputsFecha" 
                                MaxLength="10"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                                TargetControlID="txtFechaInicial">
                            </cc1:CalendarExtender>
                        </td>
                        <td class="label" width="85">
                            <asp:Label ID="Label2" runat="server" Font-Bold="False" Text="Fecha Final:"></asp:Label>
                        </td>
                        <td width="110">
                            <asp:TextBox ID="txtFechaFinal" runat="server" CssClass="inputsFecha" 
                                MaxLength="10"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtFechaFinal_CalendarExtender" runat="server" 
                                Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaFinal">
                            </cc1:CalendarExtender>
                        </td>
                        <td width="70" class="label">
                            <asp:Label ID="Label3" runat="server" Font-Bold="False" Text="Sucursal:"></asp:Label>
                        </td>
                        <td width="155">
                            <asp:DropDownList ID="ddlAlmacen" runat="server" 
                                CssClass="combo" Width="150px">
                            </asp:DropDownList>
                        </td>
                        <td width="155">
                            <asp:CheckBox ID="chkHabilitado" runat="server" Checked="True" 
                                Font-Bold="False" Text="Habilitados" />
                        </td>
                        <td width="110">
                            &nbsp;</td>
                        <td align="right">
                            &nbsp;</td>
                        <td align="right" width="70">
                            &nbsp;</td>
                        <td align="right" width="70">
                            &nbsp;</td>
                    </tr>
                </table>
            </div>
            <div class="toolbar">
                <table width="100%">
                    <tr>
                        <td width="65">
                            <asp:ImageButton ID="btnConsultar" runat="server" 
                                ImageUrl="~/images/Buscar.jpg" onclick="btnConsultar_Click" ToolTip="Buscar" />
                        </td>
                        <td width="65">
                            <asp:ImageButton ID="btnNuevo" runat="server" ImageUrl="~/images/Nuevo.jpg" 
                                onclick="btnNuevo_Click" ToolTip="Nuevo" />
                        </td>
                        <td>
                            <asp:ImageButton ID="btnSalir" runat="server" ImageUrl="~/images/Salir.jpg" 
                                onclick="btnSalir_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <img src="images/loading.gif" />
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:Panel ID="Panel1" runat="server" Height="600px" ScrollBars="Vertical" 
                Width="100%">
                <asp:GridView ID="gvNotaSalida" runat="server" AutoGenerateColumns="False" 
                    CssClass="grid" DataKeyNames="n_IdNotaSalida" 
                    onrowdatabound="gvNotaSalida_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="N° Nota de Salida">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" 
                                    Text='<%# Bind("v_NumeroOrdenCompra") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" Font-Underline="True" 
                                    ForeColor="Blue" Text='<%# Bind("v_NumeroNotaSalida") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="d_FechaEmision" HeaderText="Fecha">
                        <ItemStyle Width="150px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Sucursal Origen" DataField="v_Descripcion">
                        <ItemStyle Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Motivo" HeaderText="Motivo">
                        <ItemStyle Width="200px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Observación" DataField="t_Observacion" />
                    </Columns>
                    <FooterStyle CssClass="footer" />
                </asp:GridView>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

