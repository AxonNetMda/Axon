<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="index.aspx.vb" Inherits="axonnet.index" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Empresa de Desarrollo Web</title>
    <link href="Content/bootstrap.css" rel="stylesheet">
    <style>
        .carousel-item {
            min-height: 400px;
        }
        .carousel-inner {
            display: flex;
        }
        .carousel-item .container {
            display: flex;
            align-items: center;
            justify-content: center;
        }
        .carousel-item img {
            max-width: 500px;
            height: auto;
            display: block;
            margin-right: 20px;
        }
        .carousel-item .text-container {
            flex: 1;
            text-align: left;
        }
        .navbar + #carouselExample {
            margin-top: 20px;
        }
        .carousel-item img {
            max-height: 400px;
            object-fit: cover;
        }
    </style>
</head>
<body>
    <!-- Navbar -->
     <form id="form1" runat="server">
    <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
        <div class="container">
            <a class="navbar-brand d-flex align-items-center" href="#">
                <img src="imagenes/logo.png" alt="Logo Empresa" class="img-fluid me-2" width="100"/>
                <span class="fw-bold d-none d-md-inline">SISTEMA DE CONTROL DE STOCK E INVENTARIO</span>
                <span class="fw-bold d-md-none">Control de Stock</span>
            </a>
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse justify-content-end" id="navbarNav">
                <ul class="navbar-nav">
                    <li class="nav-item"><a class="nav-link" href="Login.aspx">Iniciar Sesión</a></li>
                    <li class="nav-item"><a class="nav-link" href="RegistroClientes.aspx">Registrarse</a></li>
                    <li class="nav-item"><a class="nav-link" href="Contacto.aspx">Contacto</a></li>
                </ul>
            </div>
        </div>
    </nav>

    <!-- Carrusel -->
    <div id="carouselExample" class="carousel slide" data-bs-ride="carousel">
        <div class="carousel-inner">
            <div class="carousel-item active">
                <div class="container d-flex flex-column flex-lg-row align-items-center text-center text-lg-start gap-4">
                    <div class="col-lg-6">
                        <img src="imagenes/stock.png" class="img-fluid w-100" alt="Sistema de Control de Stock">
                    </div>
                    <div class="col-lg-6">
                        <h2 class="text-bg-warning text-black">Tu Stock: Control Inteligente</h2>
                        <p>Optimiza tu inventario, reduce pérdidas y maximiza ganancias.</p>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <hr>
    <section class="container my-5 text-center">
        <h2>Sobre Nosotros</h2>
        <p>Somos una empresa argentina enfocada en la optimización de procesos de producción, comercialización y gestión.</p>
    </section>
    <hr>
    <footer class="bg-dark text-white text-center py-3">
        <p>Contacto: info@axonnet.store | Tel: +54 9 2257 400092</p>
        <p>&copy; 2025 Empresa Web. Todos los derechos reservados.</p>
    </footer>
    <script src="Scripts/bootstrap.bundle.min.js"></script>
    </form>
</body>
</html>
