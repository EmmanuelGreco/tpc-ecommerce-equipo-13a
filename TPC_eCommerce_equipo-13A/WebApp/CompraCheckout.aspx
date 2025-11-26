<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CompraCheckout.aspx.cs" Inherits="WebApp.CompraCheckout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 style="margin-bottom: 40px;">Confirmar compra</h1>

    <asp:Label ID="lblUsuario" runat="server" CssClass="h5"></asp:Label>
    <br /><br />

    <h3>Resumen del carrito</h3>
    <asp:Repeater ID="RepeaterResumen" runat="server">
        <ItemTemplate>
            <div class="row">
                <div class="col-md-6">
                    <%# Eval("Nombre") %> (x<%# Eval("Stock") %>)
                </div>
                <div class="col-md-6">
                    <%# String.Format("{0:C}", (decimal)Eval("Precio") * (int)Eval("Stock")) %>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>

    <asp:Label ID="lblTotal" runat="server" CssClass="h4"></asp:Label>

    <hr />

    <h3>Datos de la compra</h3>

    <div class="mb-3">
        <label>Método de pago:</label>
        <asp:DropDownList ID="ddlMetodoPago" runat="server" CssClass="form-select">
        </asp:DropDownList>
    </div>

    <div class="mb-3">
        <label>Método de envío:</label>
        <asp:DropDownList ID="ddlMetodoEnvio" runat="server" CssClass="form-select">
        </asp:DropDownList>
    </div>

    <asp:Button ID="btnConfirmarCompra" runat="server"
        Text="Confirmar compra"
        CssClass="btn btn-success btn-lg"
        OnClick="btnConfirmarCompra_Click" />

    <br /><br />
    <asp:Label ID="lblErrorCompra" runat="server" ForeColor="Red"></asp:Label>
</asp:Content>
