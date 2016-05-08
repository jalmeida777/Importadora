<%@ Page Title="Boletas" Language="C#" MasterPageFile="~/Plantilla.master" AutoEventWireup="true" CodeFile="ListarBoletas.aspx.cs" Inherits="ListarBoletas" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        
        <div class="divBusqueda">
            <table width="100%">
                <tr>
                    <td colspan="12">
                        <h1 class="label">
                            Boletas de Venta</h1>
                    </td>
                </tr>
                <tr>
                    <td width="90" class="label">
                        <asp:Label ID="Label1" runat="server" Text="Fecha Inicial:" Font-Bold="False"></asp:Label>
                    </td>
                    <td width="110">
                        <asp:TextBox ID="txtFechaInicial" runat="server" CssClass="inputsFecha"
                            MaxLength="10"></asp:TextBox>
                        <cc1:CalendarExtender 
                        ID="CalendarExtender1" 
                        runat="server" 
                        TargetControlID="txtFechaInicial" Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                    </td>
                    <td width="85" class="label">
                        <asp:Label ID="Label2" runat="server" Text="Fecha Final:" Font-Bold="False"></asp:Label>
                    </td>
                    <td width="110">
                        <asp:TextBox ID="txtFechaFinal" runat="server" CssClass="inputsFecha" 
                            MaxLength="10"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtFechaFinal_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaFinal">
                        </cc1:CalendarExtender>
                    </td>
                    <td width="70">
                        <asp:Label ID="Label4" runat="server" Font-Bold="False" Text="Boleta N°:"></asp:Label>
                    </td>
                    <td width="90">
                        <asp:TextBox ID="txtBoleta" runat="server" AutoPostBack="True" 
                            CssClass="inputNormal" ontextchanged="txtBoleta_TextChanged" Width="80px"></asp:TextBox>
                    </td>
                    <td width="70">
                        <asp:Label ID="Label3" runat="server" Font-Bold="False" Text="Sucursal:"></asp:Label>
                    </td>
                    <td class="label" width="210">
                        <asp:DropDownList ID="ddlAlmacen" runat="server" AutoPostBack="True" 
                            CssClass="combo" onselectedindexchanged="ddlAlmacen_SelectedIndexChanged" 
                            Width="200px">
                        </asp:DropDownList>
                    </td>
                    <td class="label">
                        
                        <asp:CheckBox ID="chkHabilitado" runat="server" Checked="True" 
                            Font-Bold="False" Text="Habilitados" AutoPostBack="True" 
                            oncheckedchanged="chkHabilitado_CheckedChanged" />
                        
                    </td>
                    <td align="right">
                        </td>
                    <td align="right" width="70">
                        &nbsp;</td>
                    <td align="right" width="70">
                        &nbsp;&nbsp;</td>
                </tr>
            </table>
            </div>
            <div class="toolbar">
            <table width="100%"><tr><td width="65">
                <asp:ImageButton ID="btnConsultar" runat="server" 
                    ImageUrl="~/images/Buscar.jpg" onclick="btnConsultar_Click" ToolTip="Buscar" />
                </td>
                <td width="65">
                    <asp:ImageButton ID="btnSalir" runat="server" ImageUrl="~/images/Salir.jpg" 
                        onclick="btnSalir_Click" />
                </td>
                <td>
                    &nbsp;</td>
                </tr></table>
            </div>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
        AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <img src="images/loading.gif" />
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:Panel ID="Panel1" runat="server" Height="600px" ScrollBars="Vertical" 
                Width="100%">
                <asp:GridView ID="gvPedido" runat="server" AutoGenerateColumns="False" 
                    CssClass="grid" DataKeyNames="n_IdPedido" 
                    onrowdatabound="gvPedido_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="N° Boleta">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" 
                                    Text='<%# Bind("v_NumeroOrdenCompra") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" Font-Underline="True" 
                                    ForeColor="Blue" Text='<%# Bind("v_NumeroComprobante") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="d_FechaEmision" HeaderText="Fecha">
                        <ItemStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="v_Nombre" HeaderText="Cliente">
                        <ItemStyle Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Vendedor" HeaderText="Vendedor">
                        <ItemStyle Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Sucursal" DataField="v_Descripcion">
                        <ItemStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Total" DataField="f_Total" 
                            DataFormatString="{0:n2}">
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle CssClass="footer" />
                </asp:GridView>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

