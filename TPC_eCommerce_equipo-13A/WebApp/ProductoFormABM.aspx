<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ProductoFormABM.aspx.cs" Inherits="WebApp.ProductoFormABM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1 style="margin-bottom: 40px;">Agregar producto</h1>
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
            <div class="col-md-4">
                <asp:Label runat="server" CssClass="form-label" for="txtOrigen">País de origen:</asp:Label>
                <asp:TextBox ID="txtOrigen" CssClass="form-control" placeholder="Ejemplo: Argentina" MaxLength="50" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <asp:Label runat="server" CssClass="form-label" for="txtPrecio">Precio:</asp:Label>
                <asp:TextBox ID="txtPrecio" CssClass="form-control" placeholder="Ejemplo: 50.000" MaxLength="50" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <asp:Label runat="server" CssClass="form-label" for="txtStock">Stock:</asp:Label>
                <asp:TextBox ID="txtStock" CssClass="form-control" placeholder="Ejemplo: 1000" MaxLength="50" runat="server"></asp:TextBox>
            </div>
        </div>

        <div style="min-height: 1.5em;">
            <asp:CustomValidator ID="errorNombre" runat="server" ControlToValidate="txtNombre" ErrorMessage="Error en el nombre" ForeColor="Red" Display="Dynamic" EnableClientScript="false" />
        </div>

        <%--CAROUSEL--%>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <asp:UpdatePanel runat="server" ID="CarouselUP">
            <ContentTemplate>
                <%  
                        idImagen = (int)Session["sessionIdImagen"];
                %>
                <div class="col">
                    <div class="card h-100">
                        <div id="carouselImagenes" class="carousel slide" data-bs-ride="carousel">
                            <div class="carousel-inner">
                                <div class="carousel-item active" style="height: 300px">
                                    <img src="<%:listaImagenes.Count != 0 ? listaImagenes[idImagen-1].ImagenUrl.ToString() : "https://www.svgrepo.com/show/508699/landscape-placeholder.svg" %>" class="d-block w-100" style="max-height: 300px; object-fit: contain;">
                                </div>
                            </div>
                            <% if (listaImagenes.Count > 1)
                                {%>
                            <asp:Button ID="Button1" runat="server" CssClass="carousel-control-prev" Style="filter: invert(1)" Text="←" OnClick="btnAnterior_Click" />
                            <asp:Button ID="Button2" runat="server" CssClass="carousel-control-next" Style="filter: invert(1)" Text="→" OnClick="btnSiguiente_Click" />
                            <%} %>
                        </div>
                        <div class="card-body">
                            <label class="form-label">URL imagen <%: idImagen %>:</label>
                            <asp:TextBox ID="txtURLImagen" CssClass="form-control" placeholder="https://images.example.com/products/img01.jpg" MaxLength="200" runat="server"></asp:TextBox>
                            <asp:Button ID="btnConfirmarImagen" runat="server" Text="✔ Confirmar Imagen" CssClass="btn btn-primary mt-2" OnClick="btnConfirmarImagen_Click"/>
                            <asp:Button ID="btnAgregarImagen" runat="server" Text="➕ Agregar Imagen" CssClass="btn btn-primary mt-2" OnClick="btnAgregarImagen_Click"/>
                            <asp:Button ID="btnRemoverImagen" runat="server" Text="➖ Remover Imagen" CssClass="btn btn-primary mt-2" OnClick="btnRemoverImagen_Click"/>
                        </div>
                    </div>
                </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <%--CAROUSEL--%>

        <asp:Button ID="btnAgregar" runat="server" Text="➕ Agregar" CssClass="btn btn-primary mt-2" OnClick="btnAgregar_Click" />
        <a id="btnCancelar" class="btn btn-primary mt-2" href="/ProductoGestion.aspx" />❌ Cancelar</a>
    </div>
</asp:Content>
