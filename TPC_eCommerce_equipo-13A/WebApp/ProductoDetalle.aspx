<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ProductoDetalle.aspx.cs" Inherits="WebApp.ProductoDetalle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 style="margin-bottom: 40px;">Detalle del producto</h1>

    <div class="container mt-4">

        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-5">
                        <div id="carouselProducto" class="carousel slide" data-bs-ride="carousel">
                            <asp:Repeater ID="repIndicadores" runat="server">
                                <HeaderTemplate><div class="carousel-indicators">
                                </HeaderTemplate>

                                <ItemTemplate>
                                    <button type="button" style="filter: invert(1)" data-bs-target="#carouselProducto"
                                        data-bs-slide-to="<%# Container.ItemIndex %>"
                                        <%# Container.ItemIndex == 0 ? "class='active' aria-current='true'" : "" %>
                                        aria-label="Slide <%# Container.ItemIndex + 1 %>">
                                    </button>
                                </ItemTemplate>

                                <FooterTemplate>
                                    </div>
                                </FooterTemplate>
                            </asp:Repeater>

                            <asp:Repeater ID="repImagenes" runat="server">
                                <HeaderTemplate>
                                    <div class="carousel-inner">
                                </HeaderTemplate>

                                <ItemTemplate>
                                    <div class="carousel-item <%# Container.ItemIndex == 0 ? "active" : "" %>"
                                         style="height: 300px;">
                                        <img src="<%# Eval("ImagenUrl") %>" class="d-block w-100"
                                            style="max-height: 300px; object-fit: contain;">
                                    </div>
                                </ItemTemplate>

                                <FooterTemplate>
                                    </div>
                                </FooterTemplate>
                            </asp:Repeater>

                            <button class="carousel-control-prev" style="filter: invert(1)" type="button"
                                data-bs-target="#carouselProducto" data-bs-slide="prev">
                                <span class="carousel-control-prev-icon"></span>
                            </button>

                            <button class="carousel-control-next" style="filter: invert(1)" type="button"
                                data-bs-target="#carouselProducto" data-bs-slide="next">
                                <span class="carousel-control-next-icon"></span>
                            </button>
                        </div>
                    </div>

                    <div class="col-md-7">
                        <h2 class="mb-3"><asp:Label ID="lblNombre" runat="server" /></h2>
                        <h5 class="text-muted mb-3"><asp:Label ID="lblMarca" runat="server" /></h5>
                        <p><strong class="text-muted">Código: </strong><asp:Label ID="lblCodigo" runat="server" /></p>
                        <p><strong class="text-muted">Descripción:</strong><br /><asp:Label ID="lblDescripcion" runat="server" /></p>
                        <p><strong class="text-muted">Categoría: </strong><asp:Label ID="lblCategoria" runat="server" /></p>
                        <p><strong class="text-muted">Origen: </strong><asp:Label ID="lblOrigen" runat="server" /></p>
                        <h3><asp:Label ID="lblPrecio" runat="server" /></h3>
                        <p><strong class="text-muted">Stock disponible: </strong><asp:Label ID="lblStock" runat="server" />u</p>
                        <div class="row mt-4">
                            <div class="col-8">
                                <asp:Button ID="btnAgregarCarritoDetalle" runat="server" Text="Agregar al carrito"
                                    CssClass="btn btn-primary w-100" OnClick="btnAgregarCarritoDetalle_Click" />
                            </div>

                            <div class="col-4">
                                <asp:TextBox ID="txtCantidadDetalle" runat="server" TextMode="Number"
                                    Text="1" CssClass="form-control text-end"></asp:TextBox>
                            </div>
                        </div>

                        <asp:CustomValidator ID="cvStockDetalle" runat="server"
                            ControlToValidate="txtCantidadDetalle" Display="Dynamic" ForeColor="red">
                        </asp:CustomValidator>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
