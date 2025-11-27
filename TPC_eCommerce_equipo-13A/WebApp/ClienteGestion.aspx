<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ClienteGestion.aspx.cs" Inherits="WebApp.ClienteGestion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 style="margin-bottom: 40px;">📝 Gestión de clientes</h1>

    <div class="row">
        <div class="col-4">
            <div class="mb-5">
                <asp:Label Text="Buscar cliente por nombre, apellido, documento o email:" runat="server" />
                <asp:TextBox runat="server" ID="txtFiltro" CssClass="form-control" AutoPostBack="true" OnTextChanged="filtro_TextChanged" />
            </div>
        </div>
    </div>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="updClientes" runat="server">
        <ContentTemplate>

            <div class="container w-100">
                <asp:GridView ID="dgvClientes" runat="server" CssClass="table" DataKeyNames="Id"
                    AutoGenerateColumns="false"
                    OnRowDataBound="dgvClientes_RowDataBound">
                    <Columns>
                        <asp:BoundField HeaderText="Nombre" DataField="Usuario.Nombre" />
                        <asp:BoundField HeaderText="Apellido" DataField="Usuario.Apellido" />
                        <asp:BoundField HeaderText="Documento" DataField="Usuario.Documento" />
                        <asp:BoundField HeaderText="Email" DataField="Usuario.Email" />

                        <asp:CheckBoxField HeaderText="Activo" DataField="Activo" />

                        <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <asp:Button Text="Inactivar" ID="btnInactivar" CssClass="btn btn-warning"
                                    OnClick="btnInactivar_Click" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Acción">
                            <ItemTemplate>
                                <a href='<%# "ClienteFormABM.aspx?id=" + Eval("Id") %>' class="btn" title="Editar Cliente 📝">📝</a>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <a href="ClienteFormABM.aspx" class="btn btn-primary" style="margin-top: 40px;">➕ Agregar</a>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
