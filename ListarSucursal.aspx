<%@ Page Title="Administración de Sucursales" Language="C#" MasterPageFile="~/Plantilla.master" AutoEventWireup="true" CodeFile="ListarSucursal.aspx.cs" Inherits="ListarSucursal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="100%" cellpadding="3" cellspacing="3">
                <tr>
                    <td>
                &nbsp;</td>
                    <td>
                &nbsp;</td>
                </tr>
                <tr>
                    <td width="70">
                        <asp:Label ID="Label2" runat="server" Text="Buscar:"></asp:Label>
                    </td>
                    <td>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td width="225">
                                    <asp:TextBox ID="txtBuscar" 
                            runat="server" 
                                CssClass="inputs" 
                                Width="200px"
                                placeholder="Buscar"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnConsultar" runat="server" 
                                        ImageUrl="~/images/Buscar.jpg" onclick="btnConsultar_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td width="70">
                &nbsp;</td>
                    <td>
                &nbsp;</td>
                </tr>
            </table>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
        AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <img src="images/loading.gif" />
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:GridView ID="gvSucursal" runat="server" AutoGenerateColumns="False" 
                CssClass="grid" Width="100%">
                <Columns>
                    <asp:BoundField DataField="n_IdSucursal" HeaderText="Id">
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="v_Descripcion" HeaderText="Sucursal" />
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

