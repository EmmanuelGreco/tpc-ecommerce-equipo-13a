<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="EmpleadoGestion.aspx.cs" Inherits="WebApp.EmpleadoGestion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 style="margin-bottom: 40px;">📝 Gestión de empleados</h1>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="updEmpleados" runat="server">
        <ContentTemplate>

            <div class="container w-100">
                <asp:GridView ID="dgvEmpleados" runat="server" CssClass="table" DataKeyNames="Id"
                    AutoGenerateColumns="false"
                    OnRowDataBound="dgvEmpleados_RowDataBound">
                    <Columns>
                        <asp:BoundField HeaderText="Nombre" DataField="Usuario.Nombre" />
                        <asp:BoundField HeaderText="Apellido" DataField="Usuario.Apellido" />
                        <asp:BoundField HeaderText="Legajo" DataField="Legajo" />
                        <asp:BoundField HeaderText="Cargo" DataField="Cargo" />
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
                                <a href='<%# "EmpleadoFormABM.aspx?id=" + Eval("Id") %>' class="btn" title="Editar Empleado 📝">📝</a>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <a href="EmpleadoFormABM.aspx" class="btn btn-primary" style="margin-top: 40px;">➕ Agregar</a>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
