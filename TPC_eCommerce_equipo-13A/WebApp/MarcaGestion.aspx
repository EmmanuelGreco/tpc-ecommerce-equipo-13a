<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MarcaGestion.aspx.cs" Inherits="WebApp.MarcaGestion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Gestión de Marcas</h1>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="updMarcas" runat="server">
        <ContentTemplate>

            <asp:GridView ID="dgvMarcas" runat="server" CssClass="table" DataKeyNames="Id"
                AutoGenerateColumns="false"
                OnRowDataBound="dgvMarcas_RowDataBound"
                OnRowEditing="dgvMarcas_RowEditing"
                OnRowUpdating="dgvMarcas_RowUpdating"
                OnRowCancelingEdit="dgvMarcas_RowCancelingEdit"
                style="margin-top:40px;">
                <Columns>
                    <asp:BoundField HeaderText="Nombre" Datafield="Nombre" />
                    <asp:CommandField HeaderText="Acción" ShowEditButton="true"
                        EditText="&#128221"
                        UpdateText="💾 Guardar"
                        CancelText="❌ Cancelar"/>
                </Columns>
            </asp:GridView>

            <div class="col mb-2 d-flex flex-column" style="width: 300px; margin-top:40px;">
                <asp:Label runat="server" CssClass="form-label" for="txtNuevaMarca">Nueva marca:</asp:Label>
                <asp:TextBox ID="txtNuevaMarca" CssClass="form-control" placeholder="Ejemplo: Sony" MaxLength="50" runat="server"></asp:TextBox>
                <div style="min-height: 1.5em;">
                    <asp:CustomValidator ID="errorMarcaCustom" runat="server" ControlToValidate="txtNuevaMarca" ErrorMessage="Aca va el error del back" ForeColor="Red" Display="Dynamic" EnableClientScript="false" />
                </div>
                <asp:Button ID="btnAgregar" runat="server" Text="➕ Agregar" CssClass="btn btn-primary mt-2" OnClick="btnAgregar_Click" />
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
