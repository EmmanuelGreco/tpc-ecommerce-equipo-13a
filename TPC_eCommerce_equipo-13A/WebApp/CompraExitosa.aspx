<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CompraExitosa.aspx.cs" Inherits="WebApp.CompraExitosa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h1>✅ ¡Compra realizada con éxito!</h1>

        <asp:Label ID="lblMensaje" runat="server" CssClass="h4 d-block mt-3"></asp:Label>

        <asp:Panel ID="panelResumen" runat="server" Visible="false" CssClass="mt-4">
            <h3>Resumen de la compra</h3>
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

            <asp:Label ID="lblTotal" runat="server" CssClass="h4 mt-3 d-block"></asp:Label>
        </asp:Panel>

        <a href="Default.aspx" class="btn btn-primary mt-4">Volver al inicio</a>
    </div>
</asp:Content>
