<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MarcaGestion.aspx.cs" Inherits="WebApp.MarcaGestion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 style="margin-bottom:40px;">📝 Gestión de Marcas</h1>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="updMarcas" runat="server">
        <ContentTemplate>
            
            <div class="container w-100">
                <asp:GridView ID="dgvMarcas" runat="server" CssClass="table" DataKeyNames="Id"
                    AutoGenerateColumns="false"
                    OnRowDataBound="dgvMarcas_RowDataBound"
                    OnRowEditing="dgvMarcas_RowEditing"
                    OnRowUpdating="dgvMarcas_RowUpdating"
                    OnRowCancelingEdit="dgvMarcas_RowCancelingEdit">
                    <Columns>
                        <asp:BoundField HeaderText="Nombre" Datafield="Nombre" />
                        <asp:CommandField HeaderText="Acción" ShowEditButton="true"
                            EditText="📝"
                            UpdateText="💾 Guardar"
                            CancelText="❌ Cancelar"/>
                    </Columns>
                </asp:GridView>
            </div>

            <h3 style="margin-top: 40px">Agregar marca:</h3>
            <div class="container w-100">
                <div class="row">
                    <div class="col-md-8">
                        <asp:TextBox ID="txtNuevaMarca" CssClass="form-control" placeholder="Ejemplo: Sony" MaxLength="50" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <asp:Button ID="btnAgregar" runat="server" Text="➕ Agregar" CssClass="btn btn-primary w-100" OnClick="btnAgregar_Click" />
                    </div>
                </div>
                <div class="row">
                    <div class="col">
                        <div style="min-height: 1.5em;">
                            <asp:CustomValidator ID="errorMarcaCustom" runat="server" ControlToValidate="txtNuevaMarca" ErrorMessage="Aca va el error del back" ForeColor="Red" Display="Dynamic" EnableClientScript="false" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
