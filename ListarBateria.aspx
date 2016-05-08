<%@ Page Title="Administración de Baterías" Language="C#" MasterPageFile="~/Plantilla.master" AutoEventWireup="true" CodeFile="ListarBateria.aspx.cs" Inherits="ListarBateria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

        <div class="divBusqueda">

            <table width="100%" cellpadding="3" cellspacing="3">
                <tr>
                    <td colspan="2">
                        <h1 class="label">
                            Administración de Baterías</h1>
                    </td>
                </tr>
                <tr>
                    <td width="55" class="label">
                        <asp:Label ID="Label2" runat="server" Text="Buscar:"></asp:Label>
                    </td>
                    <td>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td width="225">
                                    <asp:TextBox ID="txtBuscar" runat="server" CssClass="inputs" 
                                        placeholder="Buscar" Width="200px" AutoPostBack="True" 
                                        ontextchanged="txtBuscar_TextChanged"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkEstado" runat="server" AutoPostBack="True" Checked="True" 
                                        oncheckedchanged="chkEstado_CheckedChanged" Text="Ver Habilitados" />
                                </td>
                                <td align="right" width="70">
                                    &nbsp;</td>
                                <td align="right" width="70">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            </div>

                <div class="toolbar">
            <table width="100%"><tr><td width="65">
                
                <asp:ImageButton ID="btnConsultar" runat="server" 
                    ImageUrl="~/images/Buscar.jpg" onclick="btnConsultar_Click" />
                
                </td>
                <td width="65">
                  
                    <asp:ImageButton ID="btnNuevo" runat="server" ImageUrl="~/images/Nuevo.jpg" 
                        onclick="btnNuevo_Click" />
                  
                </td>
                <td>
                   
                    <asp:ImageButton ID="btnSalir" runat="server" ImageUrl="~/images/Salir.jpg" 
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
                <asp:GridView ID="gvBaterias" runat="server" AutoGenerateColumns="False" 
                    CssClass="grid" DataKeyNames="n_IdPilas" 
                    onrowdatabound="gvBaterias_RowDataBound" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="n_IdPilas" HeaderText="Id" Visible="False">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="v_Descripcion" HeaderText="Batería" />
                        <asp:TemplateField HeaderText="Editar">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" ImageUrl="~/images/edit.png" 
                                    ToolTip="Editar" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </asp:Panel>
            <table width="100%">
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

