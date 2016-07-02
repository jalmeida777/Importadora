<%@ Page Title="" Language="C#" MasterPageFile="~/Plantilla.master" AutoEventWireup="true" CodeFile="ListarCorrelativo.aspx.cs" Inherits="ListarCorrelativo" %>

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
                                Administración de Correlativos</h1>
                        </td>
                    </tr>
                    <tr>
                        <td width="50" class="label">
                            <asp:Label ID="Label3" runat="server" Text="Sucursal:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSucursal" runat="server" AutoPostBack="True" 
                                CssClass="combo" Width="280px" 
                                onselectedindexchanged="ddlEmpresa_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="toolbar">
                <table width="100%">
                    <tr>
                        <td width="95">
                            <asp:ImageButton ID="btnConsultar" runat="server" 
                    ImageUrl="~/images/btnBuscar_New.png" onclick="btnConsultar_Click" />
                        </td>
                        <td width="65">
                            <asp:ImageButton ID="btnSalir" runat="server" ImageUrl="~/images/btnSalir_New.png" 
                        onclick="btnSalir_Click" />
                        </td>
                        <td>
                            &nbsp;</td>
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
                <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" 
                    CssClass="grid" DataKeyNames="i_IdCorrelativo" Width="100%" 
                    onrowdatabound="gv_RowDataBound" 
                    >
                    <Columns>
                        <asp:BoundField DataField="i_IdCorrelativo" HeaderText="Id" Visible="False">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="v_Descripcion" HeaderText="Documento" />
                        <asp:BoundField DataField="c_Serie" HeaderText="Serie"></asp:BoundField>
                        <asp:BoundField DataField="v_CorrelativoInicial" 
                            HeaderText="Correlativo Inicial" />
                        <asp:BoundField DataField="v_CorrelativoFinal" HeaderText="Correlativo Final" />
                        <asp:BoundField DataField="v_CorrelativoActual" 
                            HeaderText="Correlativo Actual" />
                        <asp:BoundField DataField="v_NroAutorizacion" HeaderText="NroAutorización" />
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

