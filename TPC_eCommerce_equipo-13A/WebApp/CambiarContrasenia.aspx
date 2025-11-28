<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CambiarContrasenia.aspx.cs" Inherits="WebApp.CambiarContrasenia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <h1 runat="server" id="Titulo" style="margin-bottom: 40px;">Cambiar contraseña</h1>
    <div class="container  w-100">
        <div class="row">
            <div class="col-md-6">
                <asp:Label ID="lblContraseniaActual" runat="server" CssClass="form-label" for="txtContrasenia">Contraseña actual:</asp:Label>
                <asp:TextBox ID="txtContraseniaActual" CssClass="form-control" TextMode="password" runat="server"></asp:TextBox>
                <div style="min-height: 3em;">
                    <asp:RequiredFieldValidator ID="rfvContraseniaActual" ErrorMessage="El campo no puede estar vacío" ForeColor="Red" Display="Dynamic" ControlToValidate="txtContraseniaActual" runat="server" />
                    <asp:RegularExpressionValidator ID="revContraseniaActual" ErrorMessage="La contraseña debe tener mínimo 8 caracteres, e incluir al menos 1 mayúscula, 1 minúscula, 1 número y 1 símbolo."
                        ForeColor="Red" Display="Dynamic" ControlToValidate="txtContraseniaActual" runat="server"
                        ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,20}$">
                    </asp:RegularExpressionValidator>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <asp:Label runat="server" CssClass="form-label" for="txtContraseniaNueva">Contraseña nueva:</asp:Label>
                <asp:TextBox ID="txtContraseniaNueva" CssClass="form-control" TextMode="password" runat="server"></asp:TextBox>
                <div style="min-height: 3em;">
                    <asp:RequiredFieldValidator ErrorMessage="¡El campo no puede estar vacío!" ForeColor="Red" Display="Dynamic" ControlToValidate="txtContraseniaNueva" runat="server" />
                    <asp:RegularExpressionValidator ErrorMessage="La contraseña debe tener mínimo 8 caracteres, e incluir al menos 1 mayúscula, 1 minúscula, 1 número y 1 símbolo."
                        ForeColor="Red" Display="Dynamic" ControlToValidate="txtContraseniaNueva" runat="server"
                        ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,20}$">
                    </asp:RegularExpressionValidator>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <asp:Label runat="server" CssClass="form-label" for="txtContraseniaNuevaRepe">Contraseña nueva (repetir):</asp:Label>
                <asp:TextBox ID="txtContraseniaNuevaRepe" CssClass="form-control" TextMode="password" runat="server"></asp:TextBox>
                <div style="min-height: 3em;">
                    <asp:RequiredFieldValidator ErrorMessage="¡El campo no puede estar vacío!" ForeColor="Red" Display="Dynamic" ControlToValidate="txtContraseniaNuevaRepe" runat="server" />
                    <asp:RegularExpressionValidator ErrorMessage="La contraseña debe tener mínimo 8 caracteres, e incluir al menos 1 mayúscula, 1 minúscula, 1 número y 1 símbolo."
                        ForeColor="Red" Display="Dynamic" ControlToValidate="txtContraseniaNuevaRepe" runat="server"
                        ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,20}$">
                    </asp:RegularExpressionValidator>
                    <asp:CompareValidator
                        ID="cvContraseniaRepe" runat="server" ControlToValidate="txtContraseniaNuevaRepe" ControlToCompare="txtContraseniaNueva"
                        ErrorMessage="Las contraseñas no coinciden." ForeColor="Red" Display="Dynamic" />
                </div>
            </div>
        </div>

        <asp:Button ID="btnAgregar" runat="server" Text="➕ Agregar" CssClass="btn btn-primary mt-2" OnClick="btnAgregar_Click" />
        <a id="btnCancelar" class="btn btn-primary mt-2" href="Productos.aspx">❌ Cancelar</a>
    </div>
</asp:Content>
