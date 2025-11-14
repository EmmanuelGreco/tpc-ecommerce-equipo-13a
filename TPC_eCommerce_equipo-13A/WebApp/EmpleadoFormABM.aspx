<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="EmpleadoFormABM.aspx.cs" Inherits="WebApp.EmpleadoFormABM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1 runat="server" ID="Titulo" style="margin-bottom: 40px;">Agregar empleado</h1>
    <div class="container  w-100">
        <div class="row">
            <div class="col-md-6">
                <asp:Label runat="server" CssClass="form-label" for="txtNombre">Nombre:</asp:Label>
                <asp:TextBox ID="txtNombre" CssClass="form-control" placeholder="Carlos" MaxLength="50" runat="server"></asp:TextBox>
                <div style="min-height: 1.5em;">
                    <asp:RequiredFieldValidator ErrorMessage="¡El Nombre es requerido!" ForeColor="Red" ControlToValidate="txtNombre" runat="server" />
                </div>
            </div>
            <div class="col-md-6">
                <asp:Label runat="server" CssClass="form-label" for="txtApellido">Apellido:</asp:Label>
                <asp:TextBox ID="txtApellido" CssClass="form-control" placeholder="Perez" MaxLength="50" runat="server"></asp:TextBox>
                <div style="min-height: 1.5em;">
                    <asp:RequiredFieldValidator ErrorMessage="¡El Apellido es requerido!" ForeColor="Red" ControlToValidate="txtApellido" runat="server" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
                <asp:Label runat="server" CssClass="form-label" for="txtDocumento">Documento:</asp:Label>
                <asp:TextBox ID="txtDocumento" CssClass="form-control" placeholder="12345678" MaxLength="8" runat="server"></asp:TextBox>
                <div style="min-height: 1.5em;">
                    <asp:RequiredFieldValidator ErrorMessage="¡El Documento es requerido!" ForeColor="Red" Display="Dynamic" ControlToValidate="txtDocumento" runat="server" />
                    <asp:RegularExpressionValidator ErrorMessage="¡El Documento debe ser numérico de 8 cifras! En caso de ser necesario, completar con 0 a la izquierda." ForeColor="Red" Display="Dynamic" 
                        ValidationExpression="^\d{8}$" ControlToValidate="txtDocumento" runat="server" />
                    <asp:CustomValidator ID="errorDNI" ControlToValidate="txtDocumento" ErrorMessage="Aca va el Error" ForeColor="Red" Display="Dynamic" EnableClientScript="false" runat="server" />
                </div>
            </div>
            <div class="col-md-4">
                <asp:Label runat="server" CssClass="form-label" for="txtTelefono">Teléfono:</asp:Label>
                <asp:TextBox ID="txtTelefono" CssClass="form-control" placeholder="1122223333" MaxLength="10" runat="server"></asp:TextBox>
                <div style="min-height: 1.5em;">
                    <asp:RequiredFieldValidator ErrorMessage="¡El Teléfono es requerido!" ForeColor="Red" Display="Dynamic" ControlToValidate="txtTelefono" runat="server" />
                    <asp:RegularExpressionValidator ErrorMessage="¡El Teléfono debe ser numérico! Hasta 10 cifras." ForeColor="Red" Display="Dynamic" 
                        ValidationExpression="^\d{1,10}$" ControlToValidate="txtTelefono" runat="server" />
                    <asp:CustomValidator ID="CustomValidator2" ControlToValidate="txtTelefono" ErrorMessage="Aca va el Error" ForeColor="Red" Display="Dynamic" EnableClientScript="false" runat="server" />
                </div>
            </div>
            <div class="col-md-4">
                <asp:Label runat="server" CssClass="form-label" for="txtFechaNacimiento">Fecha de nacimiento:</asp:Label>
                <asp:TextBox ID="txtFechaNacimiento" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox>
                <div style="min-height: 1.5em;">
                    <asp:RequiredFieldValidator ErrorMessage="¡La Fecha de Nacimiento es requerida!" ForeColor="Red" ControlToValidate="txtFechaNacimiento" runat="server" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
                <asp:Label runat="server" CssClass="form-label" for="txtDireccion">Dirección:</asp:Label>
                <asp:TextBox ID="txtDireccion" CssClass="form-control" placeholder="Rivadavia 1259" MaxLength="100" runat="server"></asp:TextBox>
                <div style="min-height: 1.5em;">
                    <asp:RequiredFieldValidator ErrorMessage="¡La Dirección es requerida!" ForeColor="Red" ControlToValidate="txtDireccion" runat="server" />
                </div>
            </div>
            <div class="col-md-4">
                <asp:Label runat="server" CssClass="form-label" for="txtCodigoPostal">Código Postal:</asp:Label>
                <asp:TextBox ID="txtCodigoPostal" CssClass="form-control" placeholder="C1562" MaxLength="20" runat="server"></asp:TextBox>
                <div style="min-height: 1.5em;">
                    <asp:RequiredFieldValidator ErrorMessage="¡El Código Postal es requerido!" ForeColor="Red" ControlToValidate="txtCodigoPostal" runat="server" />
                </div>
            </div>
            <div class="col-md-4">
                <asp:Label runat="server" CssClass="form-label" for="txtFechaAlta">Fecha de dado de alta:</asp:Label>
                <asp:TextBox ID="txtFechaAlta" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox>
                <div style="min-height: 1.5em;">
                    <asp:RequiredFieldValidator ErrorMessage="¡La Fecha de Alta es requerida!" ForeColor="Red" ControlToValidate="txtFechaNacimiento" runat="server" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <asp:Label runat="server" CssClass="form-label" for="txtEmail">Email:</asp:Label>
                <asp:TextBox ID="txtEmail" CssClass="form-control" TextMode="email" placeholder="CarlosPerez@gmail.com" MaxLength="50" runat="server"></asp:TextBox>
                <div style="min-height: 1.5em;">
                    <asp:RequiredFieldValidator ErrorMessage="¡El Email es requerido!"   ForeColor="Red" Display="Dynamic" ControlToValidate="txtEmail" runat="server" />
                    <asp:RegularExpressionValidator ErrorMessage="¡El Email debe ser un correo válido!" ForeColor="Red" Display="Dynamic"
                        ValidationExpression="^([\w\.-]+)@((\[[0-9]{1,3}(\.[0-9]{1,3}){3}\])|(([\w-]+\.)+[a-zA-Z]{2,4}))$" ControlToValidate="txtEmail" runat="server" />
                </div>
            </div>
            <div class="col-md-6">
                <asp:Label runat="server" CssClass="form-label" for="txtContrasenia">Contraseña:</asp:Label>
                <asp:TextBox ID="txtContrasenia" CssClass="form-control" TextMode="password" MaxLength="20" runat="server"></asp:TextBox>
                <div style="min-height: 3em;">
                    <asp:RequiredFieldValidator ErrorMessage="¡La Contraseña es requerida!" ForeColor="Red" Display="Dynamic" ControlToValidate="txtContrasenia" runat="server" />
                    <asp:RegularExpressionValidator ErrorMessage="La contraseña debe tener mínimo 8 caracteres, e incluir al menos 1 mayúscula, 1 minúscula, 1 número y 1 símbolo."
                        ForeColor="Red" Display="Dynamic" ControlToValidate="txtContrasenia" runat="server"                        
                        ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,20}$">
                    </asp:RegularExpressionValidator>
                </div>
            </div>
        </div>

        <div style="margin-top: 10px"></div>
        <div class="row">
            <div class="col-md-3">
                <asp:Label runat="server" CssClass="form-label" for="txtLegajo">Legajo:</asp:Label>
                <asp:TextBox ID="txtLegajo" CssClass="form-control" TextMode="number" placeholder="1001" MaxLength="50" runat="server"></asp:TextBox>
                <div style="min-height: 1.5em;">
                    <asp:RequiredFieldValidator ErrorMessage="¡El Legajo es requerido!" ForeColor="Red" ControlToValidate="txtLegajo" runat="server" />
                </div>
            </div>
            <div class="col-md-3">
                <asp:Label runat="server" CssClass="form-label" for="txtCargo">Cargo:</asp:Label>
                <asp:TextBox ID="txtCargo" CssClass="form-control" placeholder="Ejemplo: Vendedor" MaxLength="50" runat="server"></asp:TextBox>
                <div style="min-height: 1.5em;">
                    <asp:RequiredFieldValidator ErrorMessage="¡El Cargo es requerido!" ForeColor="Red" ControlToValidate="txtCargo" runat="server" />
                </div>
            </div>
            <div class="col-md-3">
                <asp:Label runat="server" CssClass="form-label" for="txtFechaIngreso">Fecha de ingreso:</asp:Label>
                <asp:TextBox ID="txtFechaIngreso" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox>
                <div style="min-height: 1.5em;">
                    <asp:RequiredFieldValidator ErrorMessage="¡La Fecha de Ingreso es requerida!" ForeColor="Red" ControlToValidate="txtFechaIngreso" runat="server" />
                </div>
            </div>
            <div class="col-md-3">
                <asp:Label runat="server" CssClass="form-label" for="txtFechaDespido">Fecha de despido:</asp:Label>
                <asp:TextBox ID="txtFechaDespido" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox>
            </div>
        </div>


        <%--<div style="min-height: 1.5em;">
            <asp:CustomValidator ID="errorNombre" runat="server" ControlToValidate="txtNombre" ErrorMessage="Error en el nombre" ForeColor="Red" Display="Dynamic" EnableClientScript="false" />
        </div>--%>

        <asp:Button ID="btnAgregar" runat="server" Text="➕ Agregar" CssClass="btn btn-primary mt-2" OnClick="btnAgregar_Click" />
        <a id="btnCancelar" class="btn btn-primary mt-2" href="/EmpleadoGestion.aspx">❌ Cancelar</a>
    </div>
</asp:Content>
