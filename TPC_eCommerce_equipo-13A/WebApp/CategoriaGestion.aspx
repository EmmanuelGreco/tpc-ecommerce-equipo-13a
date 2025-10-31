<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CategoriaGestion.aspx.cs" Inherits="WebApp.CategoriaGestion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 style="margin-bottom: 40px;">📝 Gestión de Categorías</h1>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="updCategorias" runat="server">
        <ContentTemplate>

            <div class="container">
                <asp:GridView ID="dgvCategorias" runat="server" CssClass="table" DataKeyNames="Id"
                    AutoGenerateColumns="false"
                    OnRowDataBound="dgvCategorias_RowDataBound"
                    OnRowEditing="dgvCategorias_RowEditing"
                    OnRowUpdating="dgvCategorias_RowUpdating"
                    OnRowCancelingEdit="dgvCategorias_RowCancelingEdit">
                    <Columns>
                        <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                        <asp:CommandField HeaderText="Acción" ShowEditButton="true"
                            EditText="📝"
                            UpdateText="💾 Guardar"
                            CancelText="❌ Cancelar" />
                    </Columns>
                </asp:GridView>
            </div>

            <h3 style="margin-top: 40px">Agregar categoría:</h3>
            <div class="container w-100">
                <div class="row">
                    <div class="col-md-8">
                        <asp:TextBox ID="txtNuevaCategoria" CssClass="form-control" placeholder="Ejemplo: Televisores" MaxLength="50" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <asp:Button ID="btnAgregar" runat="server" Text="➕ Agregar" CssClass="btn btn-primary w-100" OnClick="btnAgregar_Click" />
                    </div>
                </div>
                <div class="row">
                    <div class="col">
                        <div style="min-height: 1.5em;">
                            <asp:CustomValidator ID="errorCategoriaCustom" runat="server" ControlToValidate="txtNuevaCategoria" ErrorMessage="Aca va el error del back" ForeColor="Red" Display="Dynamic" EnableClientScript="false" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
