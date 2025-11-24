<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="WebApp.Carrito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 style="margin-bottom: 40px;">🛒 Carrito</h1>

    <div class="row row-cols-1 row-cols-md-1 g-5">
        <asp:Repeater ID="RepeaterCarrito" runat="server" OnItemDataBound="RepeaterCarrito_ItemDataBound">
            <ItemTemplate>
                <div class="container w-100">
                    <div class="row align-items-center">
                        <div class="col-md-3">
                            <img src="<%# ((Dominio.Producto)Container.DataItem).ListaImagen[0].ImagenUrl  %>" class="d-block w-100" style="max-height: 150px; object-fit: contain;">
                        </div>
                        <div class="col-md-3">
                            <h4 class="card-title"><%# Eval("Nombre") %></h4>
                        </div>
                        <div class="col-md-3">
                            <div class="input-group" style="max-width: 180px;">
                                <asp:Button ID="btnRestar" runat="server" Text="➖" CssClass="btn btn-outline-danger" OnClick="btnRestar_Click" CommandArgument='<%# Eval("Id") %>' />
                                <%--<asp:TextBox ID="cantidadElegida" runat="server" TextMode="Number"
                                    CssClass="form-control"
                                    Style="text-align: right"
                                    Enabled="false">
                                </asp:TextBox>--%>
                                <h4 id="cantidadElegida" runat="server" style="width: 45px; text-align: center;"></h4>
                                <asp:Button ID="btnSumar" runat="server" Text="➕" CssClass="btn btn-outline-success" OnClick="btnSumar_Click" CommandArgument='<%# Eval("Id") %>' />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <%--<p>Cantidad: <%# Eval("Stock") %></p>--%>
                            <h5><%# String.Format("{0:C}", (decimal)Eval("Precio")*(int)Eval("Stock")) %></h5>
                        </div>
                    </div>
                    <div clas="row">
                        <asp:CustomValidator ID="cvStock" runat="server"
                            Display="Dynamic"
                            ForeColor="red">
                        </asp:CustomValidator>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <h2 id="lblCarritoVacio" runat="server" visible="false">¡Ningún elemento en el carrito!</h2>
        <%-- AGREGAR AQUI UN BOTON PARA IR A LA COMPRA EN SI --%>
    </div>
</asp:Content>
