<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="PedidoDetalle.aspx.cs" Inherits="WebApp.PedidoDetalle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h1 style="margin-bottom: 40px;">Detalle del pedido</h1>

        <asp:Label ID="lblPedido" runat="server" CssClass="h4 d-block mt-3"></asp:Label>
        <asp:Label ID="lblFecha" runat="server" CssClass="d-block"></asp:Label>
        <asp:Label ID="lblUsuario" runat="server" CssClass="d-block"></asp:Label>
        <asp:Label ID="lblMetodoPago" runat="server" CssClass="d-block"></asp:Label>
        <asp:Label ID="lblMetodoEnvio" runat="server" CssClass="d-block"></asp:Label>

        <hr />

        <h3>Productos</h3>

        <asp:GridView ID="gvProductos" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">
            <Columns>
                <asp:BoundField DataField="Nombre" HeaderText="Producto" />
                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                <asp:BoundField DataField="Stock" HeaderText="Cantidad" />
                <asp:BoundField DataField="Precio" HeaderText="Precio unitario" DataFormatString="{0:C}" />
                <asp:TemplateField HeaderText="Subtotal">
                    <ItemTemplate>
                        <%# String.Format("{0:C}", ((decimal)Eval("Precio") * (int)Eval("Stock"))) %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <asp:Label ID="lblTotal" runat="server" CssClass="h4 d-block mt-3"></asp:Label>

        <asp:Label ID="lblError" runat="server" CssClass="text-danger d-block mt-3"></asp:Label>

        <a href="PedidosHistorial.aspx" class="btn btn-primary mt-3">Volver al historial</a>
    </div>
</asp:Content>
