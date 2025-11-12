<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ProductoGestion.aspx.cs" Inherits="WebApp.ProductoGestion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 style="margin-bottom: 40px;">📝 Gestión de Productos</h1>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="updProductos" runat="server">
        <contenttemplate>

            <div class="container  w-100">
                <asp:GridView ID="dgvProductos" runat="server" CssClass="table" DataKeyNames="Id"
                    AutoGenerateColumns="false"
                    OnRowCommand="dgvProductos_RowCommand"
                    OnRowDataBound="dgvProductos_RowDataBound">
                    <columns>
                        <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                        <asp:TemplateField HeaderText="Acción">
                            <itemtemplate>
                                <a href='<%# "ProductoFormABM.aspx?id=" + Eval("Id") %>' class="btn">📝</a>
                            </itemtemplate>
                        </asp:TemplateField>

                        <asp:CheckBoxField HeaderText="Activo" DataField="Activo" />

                        <asp:TemplateField HeaderText="Estado">
                            <itemtemplate>
                                <asp:Button Text="Inactivar" ID="btnInactivar" CssClass="btn btn-warning"
                                    OnClick="btnInactivar_Click" runat="server" />
                            </itemtemplate>
                        </asp:TemplateField>
                    </columns>
                </asp:GridView>
                <a href="ProductoFormABM.aspx" class="btn btn-primary" style="margin-top: 40px;">➕ Agregar</a>
            </div>

        </contenttemplate>
    </asp:UpdatePanel>
</asp:Content>
