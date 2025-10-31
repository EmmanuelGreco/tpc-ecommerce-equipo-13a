<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ProductoFormABM.aspx.cs" Inherits="WebApp.ProductoFormABM" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1 style="margin-bottom:40px;">Agregar producto</h1>
    <div class="container  w-100">
        <div class="row">
            <div class="col-md-8">
                <asp:Label runat="server" CssClass="form-label" for="txtNombre">Nombre:</asp:Label>
                <asp:TextBox ID="txtNombre" CssClass="form-control" placeholder="Ejemplo: Televisor" MaxLength="50" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <asp:Label runat="server" CssClass="form-label" for="txtCodigo">Codigo:</asp:Label>
                <asp:TextBox ID="txtCodigo" CssClass="form-control" placeholder="Ejemplo: M01" MaxLength="50" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <asp:Label runat="server" CssClass="form-label" for="txtDescripcion">Descripción:</asp:Label>
                <asp:TextBox ID="txtDescripcion" CssClass="form-control" TextMode="MultiLine" placeholder="Ejemplo: El televisor más nuevo..." MaxLength="150" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <asp:Label runat="server" CssClass="form-label" for="ddlMarca">Marca:</asp:Label>
                <asp:DropDownList ID="ddlMarca" CssClass="form-select" runat="server"></asp:DropDownList>
            </div>
            <div class="col-md-6">
                <asp:Label runat="server" CssClass="form-label" for="ddlCategoria">Categoria:</asp:Label>
                <asp:DropDownList ID="ddlCategoria" CssClass="form-select" runat="server"></asp:DropDownList>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <asp:Label runat="server" CssClass="form-label" for="txtOrigen">País de origen:</asp:Label>
                <asp:TextBox ID="txtOrigen" CssClass="form-control" placeholder="Ejemplo: Argentina" MaxLength="50" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-6">
                <asp:Label runat="server" CssClass="form-label" for="txtPrecio">Precio:</asp:Label>
                <asp:TextBox ID="txtPrecio" CssClass="form-control" placeholder="Ejemplo: 50.000" MaxLength="50" runat="server"></asp:TextBox>
            </div>
        </div>
    
        <div style="min-height: 1.5em;">
            <asp:CustomValidator ID="errorNombre" runat="server" ControlToValidate="txtNombre" ErrorMessage="Error en el nombre" ForeColor="Red" Display="Dynamic" EnableClientScript="false" />
        </div>
        <asp:Button ID="btnAgregar" runat="server" Text="➕ Agregar" CssClass="btn btn-primary mt-2" OnClick="btnAgregar_Click" />
    </div>

</asp:Content>
