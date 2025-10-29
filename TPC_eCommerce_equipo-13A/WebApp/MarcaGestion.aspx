<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MarcaGestion.aspx.cs" Inherits="WebApp.MarcaGestion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Gestión de Marcas</h1>
    <asp:GridView ID="dgvMarcas" runat="server" CssClass="table" DataKeyNames="Id"
        AutoGenerateColumns="false"
        OnRowEditing="dgvMarcas_RowEditing"
        OnRowUpdating="dgvMarcas_RowUpdating"
        OnRowCancelingEdit="dgvMarcas_RowCancelingEdit">
        <Columns>
            <asp:BoundField HeaderText="Nombre" Datafield="Nombre" />
            <asp:CommandField HeaderText="Acción" ShowEditButton="true"
                EditText="&#128221"
                UpdateText="💾 Guardar"
                CancelText="❌ Cancelar"/>
        </Columns>
    </asp:GridView>
</asp:Content>
