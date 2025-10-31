<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ProductoGestion.aspx.cs" Inherits="WebApp.ProductoGestion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 style="margin-bottom:40px;">📝 Gestión de Productos</h1>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="updProductos" runat="server">
        <ContentTemplate>

            <div class="container  w-100">
                <asp:GridView ID="dgvProductos" runat="server" CssClass="table" DataKeyNames="Id"
                    AutoGenerateColumns="false"
                    OnRowDataBound="dgvProductos_RowDataBound"
                    OnRowEditing="dgvProductos_RowEditing"
                    OnRowUpdating="dgvProductos_RowUpdating"
                    OnRowCancelingEdit="dgvProductos_RowCancelingEdit">
                    <Columns>
                        <asp:BoundField HeaderText="Nombre" Datafield="Nombre" />
                        <asp:CommandField HeaderText="Acción" ShowEditButton="true"
                            EditText="📝"
                            UpdateText="💾 Guardar"
                            CancelText="❌ Cancelar"/>
                    </Columns>
                </asp:GridView>
                <a href="ProductoFormABM.aspx" class="btn btn-primary" style="margin-top: 40px;">➕ Agregar</a>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>