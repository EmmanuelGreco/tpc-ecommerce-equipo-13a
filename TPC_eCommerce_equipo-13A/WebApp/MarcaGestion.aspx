<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MarcaGestion.aspx.cs" Inherits="WebApp.MarcaGestion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 style="margin-bottom: 40px;">📝 Gestión de Marcas</h1>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="updMarcas" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
        <ContentTemplate>
            
            <div class="container w-100">
                <asp:GridView ID="dgvMarcas" runat="server" CssClass="table" DataKeyNames="Id"
                    AutoGenerateColumns="false"
                    OnRowEditing="dgvMarcas_RowEditing"
                    OnRowUpdating="dgvMarcas_RowUpdating"
                    OnRowCancelingEdit="dgvMarcas_RowCancelingEdit"
                    OnRowDataBound="dgvMarcas_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Nombre">
                            <ItemTemplate>
                                <%# Eval("Nombre") %>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtNombre" runat="server" Text='<%# Bind("Nombre") %>'></asp:TextBox>
                                <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:CheckBoxField HeaderText="Activo" DataField="Activo"/>

                         <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <asp:Button Text="Inactivar" ID="btnInactivar" CssClass="btn btn-warning"
                                    OnClick="btnInactivar_Click" runat="server"/>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:CommandField HeaderText="Acción" ShowEditButton="true"
                            EditText="📝"
                            UpdateText="💾 Guardar"
                            CancelText="❌ Cancelar"/>
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblGlobalError" runat="server" ForeColor="Red" Visible="False"></asp:Label>
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
