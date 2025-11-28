<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CompraError.aspx.cs" Inherits="WebApp.CompraError" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h1>❌ El pago no pudo completarse</h1>

        <asp:Label ID="lblMensaje" runat="server" CssClass="h4 d-block mt-3"></asp:Label>
        <asp:Label ID="lblDetalleEstado" runat="server" CssClass="d-block mt-2 text-muted"></asp:Label>

        <asp:Panel ID="panelResumen" runat="server" Visible="false" CssClass="mt-4">
            <h3>Resumen del pedido</h3>

            <asp:Repeater ID="RepeaterResumen" runat="server">
                <HeaderTemplate>
                    <div class="row fw-bold border-bottom pb-2 mb-2">
                        <div class="col-md-6">Producto</div>
                        <div class="col-md-2 text-center">Cantidad</div>
                        <div class="col-md-2 text-end">Precio unitario</div>
                        <div class="col-md-2 text-end">Subtotal</div>
                    </div>
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="row border-bottom py-2">
                        <div class="col-md-6">
                            <%# Eval("Nombre") %><br />
                            <small class="text-muted"><%# Eval("Descripcion") %></small>
                        </div>
                        <div class="col-md-2 text-center">
                            <%# Eval("Stock") %>
                        </div>
                        <div class="col-md-2 text-end">
                            <%# String.Format("{0:C}", Eval("Precio")) %>
                        </div>
                        <div class="col-md-2 text-end">
                            <%# String.Format("{0:C}", (decimal)Eval("Precio") * (int)Eval("Stock")) %>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

            <asp:Label ID="lblTotal" runat="server" CssClass="h4 mt-3 d-block"></asp:Label>
        </asp:Panel>

        <a href="Carrito.aspx" class="btn btn-secondary mt-4 me-2">Volver al carrito</a>
        <a href="Default.aspx" class="btn btn-primary mt-4">Volver al inicio</a>
    </div>
</asp:Content>
