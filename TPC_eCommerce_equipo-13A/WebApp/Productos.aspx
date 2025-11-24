<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="WebApp.Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 style="margin-bottom: 40px;">Productos</h1>

    <div class="row row-cols-1 row-cols-md-3 g-5">
        <asp:Repeater ID="RepeaterProductos" runat="server" OnItemDataBound="RepeaterProductos_ItemDataBound">
            <ItemTemplate>
                <div class="col">
                    <div class="card h-100">

                        <div id="carouselProd<%# Eval("Id") %>" class="carousel slide" data-bs-ride="carousel">
                            <asp:Repeater ID="repIndicadores" runat="server" DataSource='<%# Eval("ListaImagen") %>'>
                                <HeaderTemplate>
                                    <div class="carousel-indicators">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <button type="button" style="filter: invert(1)"
                                        data-bs-target="#carouselProd<%# Eval("Id", "{0}") %>"
                                        data-bs-slide-to="<%# Container.ItemIndex %>"
                                        <%# Container.ItemIndex == 0 ? "class='active' aria-current='true'" : "" %>
                                        aria-label="Slide <%# Container.ItemIndex + 1 %>">
                                    </button>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </div>
                                </FooterTemplate>
                            </asp:Repeater>

                            <asp:Repeater ID="repImagenes" runat="server" DataSource='<%# Eval("ListaImagen") %>'>
                                <HeaderTemplate>
                                    <div class="carousel-inner">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div class="carousel-item <%# Container.ItemIndex == 0 ? "active" : "" %>" style="height: 200px;">
                                        <img src="<%# Eval("ImagenUrl") %>" class="d-block w-100"
                                            style="max-height: 200px; object-fit: contain;">
                                    </div>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </div>
                                </FooterTemplate>
                            </asp:Repeater>

                            <button class="carousel-control-prev" style="filter: invert(1)" type="button"
                                data-bs-target="#carouselProd<%# Eval("Id") %>" data-bs-slide="prev">
                                <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                                <span class="visually-hidden">Previous</span>
                            </button>
                            <button class="carousel-control-next" style="filter: invert(1)" type="button"
                                data-bs-target="#carouselProd<%# Eval("Id") %>" data-bs-slide="next">
                                <span class="carousel-control-next-icon" aria-hidden="true"></span>
                                <span class="visually-hidden">Next</span>
                            </button>
                        </div>

                        <div class="card-body">
                            <p class="card-subtitle text-muted fst-italic"><%# Eval("Categoria") %></p>
                            <h4 class="card-title"><%# Eval("Nombre") %></h4>
                            <h5 class="card-subtitle mb-2 text-muted"><%# Eval("Marca") %></h5>
                            <!--p class="card-text"><%# Eval("Descripcion") %></p-->
                            <!--p class="card-text">Origen: <%# Eval("Origen") %></p-->
                            <h3><%# Eval("Precio", "{0:C}") %></h3>

                            <%--<a class="btn btn-primary" href='Carrito.aspx?idProducto=<%# Eval("Id") %>'>Comprar</a>--%>
                            <%--<div class="container  w-100">--%>
                            <div class="row">
                                <div class="col-md-8">
                                    <asp:Button Text="Agregar al carrito" ID="btnAgregarCarrito" CssClass="btn btn-primary w-100"
                                        CommandName="Agregar"
                                        CommandArgument='<%# Eval("Id") %>'
                                        OnCommand="btnAgregarCarrito_Command"
                                        runat="server" />
                                </div>
                                <div class="col-md-4">
                                    <%--<asp:TextBox ID="txtNumber" runat="server" TextMode="Number" min="1" max="10" value="1" ReadOnly="true"></asp:TextBox>--%>
                                    <asp:TextBox ID="cantidadElegida" runat="server" TextMode="Number"
                                        value="1"
                                        onkeydown="return allowArrows(event)"
                                        onpaste="return false"
                                        CssClass="form-control"
                                        Style="text-align: right">
                                    </asp:TextBox>
                                </div>
                            </div>
                            <asp:CustomValidator ID="cvStock" runat="server"
                                ControlToValidate="cantidadElegida"
                                Display="Dynamic"
                                ForeColor="red">
                            </asp:CustomValidator>
                            <%--</div>--%>
                            <%--ESTE JS PERMITE EL USO DE LAS FLECHAS GRÁFICAS Y DEL TECLADO, PERO NO DE LOS NUMEROS DEL TECLADO NI DEL PEGADO DE NUMEROS.--%>
                            <script>
                                function allowArrows(e) {
                                    if (e.key === "ArrowUp" || e.key === "ArrowDown") {
                                        return true;
                                    }
                                    return false;
                                }
                            </script>


                        </div>

                        <div class="card-footer d-flex justify-content-between">
                            <small class="text-muted">Código: <%# Eval("Codigo") %></small>
                            <small class="text-muted">Stock: <%# Eval("Stock") %>u.</small>
                        </div>

                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

</asp:Content>
