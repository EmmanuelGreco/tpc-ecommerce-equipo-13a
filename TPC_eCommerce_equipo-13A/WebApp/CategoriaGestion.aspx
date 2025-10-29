<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CategoriaGestion.aspx.cs" Inherits="WebApp.CategoriaGestion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Gestión de Categorías</h1>
    <asp:GridView ID="dgvCategorias" runat="server" CssClass="table" DataKeyNames="Id"
        AutoGenerateColumns="false"
        OnRowEditing="dgvCategorias_RowEditing"
        OnRowUpdating="dgvCategorias_RowUpdating"
        OnRowCancelingEdit="dgvCategorias_RowCancelingEdit">
        <Columns>
            <asp:BoundField HeaderText="Nombre" Datafield="Nombre" />
            <asp:CommandField HeaderText="Acción" ShowEditButton="true"
                EditText="&#128221"
                UpdateText="💾 Guardar"
                CancelText="❌ Cancelar"/>
        </Columns>
    </asp:GridView>
</asp:Content>
