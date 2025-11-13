<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ClienteFormABM.aspx.cs" Inherits="WebApp.ClienteFormABM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1 style="margin-bottom: 40px;">Agregar cliente</h1>
    <div class="container  w-100">
        <div class="row">
            <div class="col-md-3">
                <asp:Label runat="server" CssClass="form-label" for="txtNombre">Nombre:</asp:Label>
                <asp:TextBox ID="txtNombre" CssClass="form-control" placeholder="Ejemplo: Carlos" MaxLength="50" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <asp:Label runat="server" CssClass="form-label" for="txtApellido">Apellido:</asp:Label>
                <asp:TextBox ID="txtApellido" CssClass="form-control" placeholder="Ejemplo: Perez" MaxLength="50" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <asp:Label runat="server" CssClass="form-label" for="txtNombre">Documento:</asp:Label>
                <asp:TextBox ID="txtDocumento" CssClass="form-control" TextMode="number" placeholder="12345678" MaxLength="8" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <asp:Label runat="server" CssClass="form-label" for="txtFechaNacimiento">Fecha de nacimiento:</asp:Label>
                <asp:TextBox ID="txtFechaNacimiento" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
                <asp:Label runat="server" CssClass="form-label" for="txtTelefono">Teléfono:</asp:Label>
                <asp:TextBox ID="txtTelefono" CssClass="form-control" TextMode="number" placeholder="1122223333" MaxLength="10" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <asp:Label runat="server" CssClass="form-label" for="txtDireccion">Dirección:</asp:Label>
                <asp:TextBox ID="txtDireccion" CssClass="form-control" placeholder="Rivadavia 1259" MaxLength="100" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <asp:Label runat="server" CssClass="form-label" for="txtCodigoPostal">Código Postal:</asp:Label>
                <asp:TextBox ID="txtCodigoPostal" CssClass="form-control" placeholder="C1562" MaxLength="20" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
                <asp:Label runat="server" CssClass="form-label" for="txtEmail">Email:</asp:Label>
                <asp:TextBox ID="txtEmail" CssClass="form-control" TextMode="email"  runat="server"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <asp:Label runat="server" CssClass="form-label" for="txtContrasenia">Contraseña:</asp:Label>
                <asp:TextBox ID="txtContrasenia" CssClass="form-control" TextMode="password" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <asp:Label runat="server" CssClass="form-label" for="txtFechaAlta">Fecha de dado de alta:</asp:Label>
                <asp:TextBox ID="txtFechaAlta" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox>
            </div>
        </div>


        <%--<div style="min-height: 1.5em;">
            <asp:CustomValidator ID="errorNombre" runat="server" ControlToValidate="txtNombre" ErrorMessage="Error en el nombre" ForeColor="Red" Display="Dynamic" EnableClientScript="false" />
        </div>--%>

        <asp:Button ID="btnAgregar" runat="server" Text="➕ Agregar" CssClass="btn btn-primary mt-2" OnClick="btnAgregar_Click" />
        <a id="btnCancelar" class="btn btn-primary mt-2" href="/ClienteGestion.aspx" />❌ Cancelar</a>
    </div>
</asp:Content>
