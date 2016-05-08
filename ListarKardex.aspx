<%@ Page Title="Kardex" Language="C#" MasterPageFile="~/Plantilla.master" AutoEventWireup="true" CodeFile="ListarKardex.aspx.cs" Inherits="ListarKardex" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <div class="divBusqueda">
            <table width="100%">
                <tr>
                    <td colspan="4">
                        <h1 class="label">
                            Kardex</h1>
                    </td>
                </tr>
                <tr>
                    <td width="90" class="label">
                       
                <asp:Label ID="Label1" runat="server" Text="Almacén:"></asp:Label>
                       
                    </td>
                    <td>
                        
                <asp:Label ID="lblAlmacen" runat="server"></asp:Label>
                        
                    </td>
                    <td align="right" width="70">
                        &nbsp;</td>
                    <td align="right" width="70">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td width="90" class="label">
                       
                <asp:Label ID="Label2" runat="server" Text="Producto:"></asp:Label>
                       
                    </td>
                    <td>
                        
                <asp:Label ID="lblProducto" runat="server"></asp:Label>
                        
                    </td>
                    <td align="right" width="70">
                        &nbsp;</td>
                    <td align="right" width="70">
                        &nbsp;</td>
                </tr>
            </table>
            </div>

            

 <div class="toolbar">
            <table width="100%"><tr><td width="65">
               
                                    <asp:ImageButton ID="btnSalir" runat="server" ImageUrl="~/images/Salir.jpg" 
                                        onclick="btnSalir_Click" />
                        
                </td>
                <td width="65">
                    
                </td>
                <td>
                   
                </td>
                </tr></table>
            </div>

    <asp:GridView ID="gvKardex" runat="server" AutoGenerateColumns="False" 
        CssClass="grid" Width="100%" onrowdatabound="gvKardex_RowDataBound">
        <Columns>
            <asp:BoundField DataField="d_FechaMovimiento" HeaderText="Fecha" />
            <asp:BoundField DataField="v_Descripcion" HeaderText="Motivo de Movimiento" />
            <asp:BoundField DataField="Movimiento" HeaderText="Tipo de Movimiento" />
            <asp:BoundField DataField="f_Cantidad" HeaderText="Cantidad Mov.">
            <ItemStyle HorizontalAlign="Right" Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="f_Saldo" HeaderText="Saldo Actual">
            <ItemStyle HorizontalAlign="Right" Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="TipoDocumento" HeaderText="Tipo de Documento" />
            <asp:TemplateField HeaderText="Número">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" 
                        Text='<%# Bind("v_NumeroDocumento") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" Font-Underline="True" 
                        ForeColor="#0000CC" Text='<%# Bind("v_NumeroDocumento") %>'></asp:LinkButton>
                    <asp:Label ID="lblIdDocumento" runat="server" Text='<%# Bind("IdDocumento") %>' 
                        Visible="False"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Cliente" HeaderText="Cliente" />
            <asp:BoundField DataField="Proveedor" HeaderText="Proveedor" />
        </Columns>
    </asp:GridView>
</asp:Content>

