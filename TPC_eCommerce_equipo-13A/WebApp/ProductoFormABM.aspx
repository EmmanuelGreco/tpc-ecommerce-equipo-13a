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

        <%--CAROUSEL--%>

        <%  
            int idImagen = 0;
            if (listaImagenes != null)
            { %>
        <div class="col">
            <div class="card h-100">
                <div id="carouselImagenes" class="carousel slide" data-bs-ride="carousel">
                    <div class="carousel-indicators">
                        <% 
                            if (listaImagenes.Count > 1)
                            {
                                foreach (Dominio.ProductoImagen img in listaImagenes)
                                {
                        %>
                        <button type="button" style="filter: invert(1)" data-bs-target="#carouselImagenes" data-bs-slide-to="<%: idImagen %>" aria-label="Slide <%: idImagen + 1 %>" <%:idImagen == 0 ? "class=active aria-current=true" : ""  %>></button>
                        <% idImagen++;
                                }
                            }%>
                    </div>
                    <div class="carousel-inner">
                        <%
                            idImagen = 0;
                            foreach (Dominio.ProductoImagen img in listaImagenes)
                            {
                        %>
                        <div class="carousel-item <%: idImagen == 0 ? "active" : "" %>" style="height: 300px">
                            <img src="<%: img.ImagenUrl %>" class="d-block w-100" style="max-height: 300px; object-fit: contain;">
                        </div>
                        <% idImagen++;
                            }%>
                    </div>
                    <% if (listaImagenes.Count > 1)
                        {%>
                    <button class="carousel-control-prev" style="filter: invert(1)" type="button" data-bs-target="#carouselImagenes" data-bs-slide="prev">
                        <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                        <span class="visually-hidden">Previous</span>
                    </button>
                    <button class="carousel-control-next" style="filter: invert(1)" type="button" data-bs-target="#carouselImagenes" data-bs-slide="next">
                        <span class="carousel-control-next-icon" aria-hidden="true"></span>
                        <span class="visually-hidden">Next</span>
                    </button>
                    <%} %>
                </div>
                <div class="card-body">
                    <label class="form-label">URL imágen <%: idImagen %>:</label>
                    <asp:TextBox ID="URLImagen" CssClass="form-control" placeholder="https://images.example.com/products/img01.jpg" MaxLength="200" runat="server"></asp:TextBox>
                    <asp:Button ID="btnAgregarImagen" runat="server" Text="➕ Agregar Imágen" CssClass="btn btn-primary mt-2" OnClick="btnAgregarImagen_Click" />
                </div>
            </div>
        </div>
    </div>
    <%
        }
        else
        { %>
    <div class="alert alert-danger" role="alert">
        No se pudo establecer una conexión con la base de datos. Inténtelo nuevamente más tarde.
    </div>
    <% return;
        }%>



    <%--CAROUSEL--%>

    <asp:Button ID="btnAgregar" runat="server" Text="➕ Agregar" CssClass="btn btn-primary mt-2" OnClick="btnAgregar_Click" />
    <a id="btnCancelar" class="btn btn-primary mt-2" href="/ProductoGestion.aspx" />❌ Cancelar</a>
    </div>



</asp:Content>
