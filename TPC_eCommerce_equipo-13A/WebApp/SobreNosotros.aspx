<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="SobreNosotros.aspx.cs" Inherits="WebApp.SobreNosotros" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .card-img-top {
            width: 220px;
            height: 220px;
            object-fit: cover;
            margin: auto;
            border-radius: 100%; /* opcional si querés esquinas suaves */
        }
    </style>
    <div class="container">

        <div class="text-center mb-5">
            <h1 class="fw-bold">Sobre Nosotros</h1>
            <p class="text-secondary fs-5">
                Somos una tienda comprometida con ofrecerte tecnología de calidad al mejor precio.
            </p>
        </div>

        <div class="row justify-content-center">
            <div class="col-md-10">
                <h3 class="fw-semibold">Quiénes somos</h3>
                <p class="fs-5" style="text-align: justify;">
                    En <strong>EP TecnoShop</strong>, tenemos como objetivo acercar el mundo de la tecnología a todos.
                Nuestras principal inspiraciones son la innovación, la experiencia del usuario y la transparencia, y por eso 
                trabajamos para que cada compra sea simple, segura y conveniente.
                </p>
            </div>
        </div>

        <div class="row text-center justify-content-center mb-4">
            <div class="col-md-10">
                <h3 class="fw-semibold">Nuestro Equipo</h3>

            </div>
        </div>

        <div class="row justify-content-center">

            <div class="col-md-4 mb-4">
                <div class="card border-0">
                    <img src="https://media.licdn.com/dms/image/v2/D4D35AQFUj7PWp9TWww/profile-framedphoto-shrink_200_200/B4DZjJ04t_GkAc-/0/1755732741369?e=1764961200&v=beta&t=MXwk-UgkRUr6DNapaxy7fWMq6ZOtegwcCrBCnsD6Q1w"
                        class="card-img-top" alt="Foto miembro">
                    <div class="card-body text-center">
                        <h5 class="fw-bold">Emmanuel Greco</h5>
                        <p class="text-muted">Fullstack Developer</p>
                    </div>
                </div>
            </div>

            <div class="col-md-4">
                <div class="card border-0">
                    <img src="https://media.licdn.com/dms/image/v2/D4D03AQFTcmiCdraGKg/profile-displayphoto-shrink_200_200/profile-displayphoto-shrink_200_200/0/1713568318679?e=1766016000&v=beta&t=V1_Dky_gJuqwiAAr6nVqVgdMo56huYKbs0ws7Qnq0C4"
                        class="card-img-top" alt="Foto miembro">
                    <div class="card-body text-center">
                        <h5 class="fw-bold">Pedro Taquino</h5>
                        <p class="text-muted">Backend Developer & Data</p>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
