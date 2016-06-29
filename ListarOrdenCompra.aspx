<%@ Page Title="Orden De Compra" Language="C#" MasterPageFile="~/Plantilla.master" AutoEventWireup="true" CodeFile="ListarOrdenCompra.aspx.cs" Inherits="ListarOrdenCompra" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        
        <div class="divBusqueda">
            <table width="100%">
                <tr>
                    <td colspan="8">
                        <h1>
                            Ordenes de Compra</h1>
                    </td>
                </tr>
                <tr>
                    <td width="95" style="padding-left: 10px">
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
                    <td width="85">
                        <asp:Label ID="Label2" runat="server" Text="Fecha Final:" Font-Bold="False"></asp:Label>
                    </td>
                    <td width="110">
                        <asp:TextBox ID="txtFechaFinal" runat="server" CssClass="inputsFecha" 
                            MaxLength="10"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtFechaFinal_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaFinal">
                        </cc1:CalendarExtender>
                    </td>
                    <td width="65">
                        <asp:Label ID="Label3" runat="server" Text="Estado:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlEstado" runat="server" CssClass="combo" Width="150px">
                        </asp:DropDownList>
                        </td>
                    <td align="right" width="70">
                        &nbsp;</td>
                    <td align="right" width="70">
                        &nbsp;&nbsp;</td>
                </tr>
            </table>
            </div>
            <div class="toolbar">
            <table width="100%"><tr><td width="95">
                <asp:ImageButton ID="btnNuevo" runat="server" 
                    ImageUrl="~/images/btnNuevo_New.png" onclick="btnNuevo_Click" ToolTip="Nuevo" />
                </td>
                <td width="95">
                    <asp:ImageButton ID="btnConsultar" runat="server" ImageUrl="~/images/btnBuscar_New.png" 
                        onclick="btnConsultar_Click" ToolTip="Buscar" />
                </td>
                <td align="left">
                    <asp:ImageButton ID="btnSalir" runat="server" ImageUrl="~/images/btnSalir_New.png" 
                        onclick="btnSalir_Click" />
                </td>
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
                <asp:GridView ID="gvOrdenCompra" runat="server" AutoGenerateColumns="False" 
                    CssClass="grid" DataKeyNames="i_IdOrdenCompra" 
                    onrowdatabound="gvOrdenCompra_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="N° Orden de Compra">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" 
                                    Text='<%# Bind("v_NumeroOrdenCompra") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" Font-Underline="True" 
                                    ForeColor="Blue" Text='<%# Bind("v_NumeroOrdenCompra") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="d_FechaEmision" HeaderText="Fecha">
                        <ItemStyle Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="v_Nombre" HeaderText="Proveedor">
                        <ItemStyle Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Moneda" DataField="v_DescripcionMoneda">
                        <ItemStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Usuario" HeaderText="Creado por">
                        <ItemStyle Width="150px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Total" DataField="f_Total" 
                            DataFormatString="{0:n2}">
                        <ItemStyle HorizontalAlign="Right" Width="70px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="v_DescripcionEstado" HeaderText="Estado">
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle CssClass="footer" />
                </asp:GridView>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

