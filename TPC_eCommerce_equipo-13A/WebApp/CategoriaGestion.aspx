<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CategoriaGestion.aspx.cs" Inherits="WebApp.CategoriaGestion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 style="margin-bottom:40px;">Gestión de Categorías</h1>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="updCategorias" runat="server">
        <ContentTemplate>

            <asp:GridView ID="dgvCategorias" runat="server" CssClass="table" DataKeyNames="Id"
                AutoGenerateColumns="false"
                OnRowDataBound="dgvCategorias_RowDataBound"
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

            <div class="col mb-2 d-flex flex-column" style="width: 300px; margin-top:40px;">
                <asp:Label runat="server" CssClass="form-label" for="txtNuevaCategoria">Nueva categoría:</asp:Label>
                <asp:TextBox ID="txtNuevaCategoria" CssClass="form-control" placeholder="Ejemplo: Televisores" MaxLength="50" runat="server"></asp:TextBox>
                <div style="min-height: 1.5em;">
                    <asp:CustomValidator ID="errorCategoriaCustom" runat="server" ControlToValidate="txtNuevaCategoria" ErrorMessage="Aca va el error del back" ForeColor="Red" Display="Dynamic" EnableClientScript="false" />
                </div>
                <asp:Button ID="btnAgregar" runat="server" Text="➕ Agregar" CssClass="btn btn-primary mt-2" OnClick="btnAgregar_Click" />
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
