<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="PedidosHistorial.aspx.cs" Inherits="WebApp.PedidosHistorial" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 style="margin-bottom: 40px;">
        <asp:Label ID="lblTitulo" runat="server"></asp:Label>
    </h1>

    <asp:Label ID="lblUsuario" runat="server" CssClass="h5 d-block mb-3"></asp:Label>

    <asp:GridView ID="gvPedidos" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="N° Pedido" />
            <asp:BoundField DataField="FechaHoraVenta" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy HH:mm}hs" />
            <asp:BoundField DataField="MontoTotal" HeaderText="Total" DataFormatString="{0:C}" />

            <asp:TemplateField HeaderText="Pago">
                <ItemTemplate>
                    <%# GetEnumDisplayName(Eval("MetodoPago")) %>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Tipo de Entrega">
                <ItemTemplate>
                    <%# GetEnumDisplayName(Eval("MetodoEnvio")) %>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Detalle">
                <ItemTemplate>
                    <asp:HyperLink ID="lnkDetalle" 
                        runat="server" 
                        Text="Ver detalle"
                        NavigateUrl='<%# GetDetalleUrl(Eval("Id")) %>' />
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>

    <asp:Label ID="lblSinPedidos" runat="server" Visible="false" CssClass="text-muted"></asp:Label>
</asp:Content>
