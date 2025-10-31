<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Contacto.aspx.cs" Inherits="WebApp.Contacto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 style="margin-bottom: 40px;">📩 Contáctanos</h1>
    <div class="row justify-content-left">
        <div class="col-md-6">
            <asp:Panel ID="pnlContacto" runat="server" CssClass="card shadow p-4 rounded-3">
                <div class="mb-3">
                    <label for="txtNombre" class="form-label">Nombre</label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Tu nombre" />
                    <asp:RequiredFieldValidator ID="rfvNombre" runat="server"
                        ControlToValidate="txtNombre" ErrorMessage="El nombre es obligatorio"
                        CssClass="text-danger" Display="Dynamic" />
                </div>

                <div class="mb-3">
                    <label for="txtEmail" class="form-label">Correo electrónico</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="nombre@correo.com" />
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
                        ControlToValidate="txtEmail" ErrorMessage="El correo es obligatorio"
                        CssClass="text-danger" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="revEmail" runat="server"
                        ControlToValidate="txtEmail"
                        ErrorMessage="Formato de correo inválido"
                        CssClass="text-danger"
                        ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$" />
                </div>

                <div class="mb-3">
                    <label for="txtMensaje" class="form-label">Mensaje</label>
                    <asp:TextBox ID="txtMensaje" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control" placeholder="Escribí tu mensaje..." />
                    <asp:RequiredFieldValidator ID="rfvMensaje" runat="server"
                        ControlToValidate="txtMensaje" ErrorMessage="El mensaje es obligatorio"
                        CssClass="text-danger" Display="Dynamic" />
                </div>

                <div class="d-grid">
                    <asp:Button ID="btnEnviar" runat="server" Text="Enviar mensaje" CssClass="btn btn-primary" OnClick="btnEnviar_Click" />
                </div>

                <asp:Label ID="lblResultado" runat="server" CssClass="mt-3 d-block fw-bold"></asp:Label>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
