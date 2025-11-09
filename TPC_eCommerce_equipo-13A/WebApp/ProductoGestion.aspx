<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ProductoGestion.aspx.cs" Inherits="WebApp.ProductoGestion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 style="margin-bottom: 40px;">📝 Gestión de Productos</h1>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="updProductos" runat="server">
        <ContentTemplate>

            <div class="container  w-100">
                <asp:GridView ID="dgvProductos" runat="server" CssClass="table" DataKeyNames="Id"
                    AutoGenerateColumns="false"
                    OnRowCommand="dgvProductos_RowCommand">
                    <Columns>
                        <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                        <asp:TemplateField HeaderText="Acción">
                            <ItemTemplate>
                                <a href='<%# "ProductoFormABM.aspx?id=" + Eval("Id") %>' class="btn btn-link">📝 Editar</a>

                                <asp:LinkButton ID="btnEliminar" runat="server"
                                    Text="🗑️ Eliminar"
                                    CommandName="EliminarProducto"
                                    CommandArgument='<%# Eval("Id") %>'
                                    CssClass="btn btn-link text-danger"
                                    OnClientClick="return confirm('¿Estás seguro de que deseas eliminar este producto?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <a href="ProductoFormABM.aspx" class="btn btn-primary" style="margin-top: 40px;">➕ Agregar</a>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
