<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="axonnet._Default" %>
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
        .navbar-brand span {
            color: white !important;
        }
    </style>
</head>
<body>
    <form runat="server">
    <!-- Navbar -->
    <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
        <div class="container">
            <a class="navbar-brand d-flex align-items-center flex-wrap" href="#">
                <img src="imagenes/logo.png" alt="Logo Empresa" class="img-fluid me-2" width="100"/>
                <span class="fw-bold d-block d-md-none text-white">Control de Stock</span>
                <span class="fw-bold d-none d-md-block text-white">SISTEMA DE CONTROL DE STOCK E INVENTARIO</span>
            </a>
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse justify-content-end" id="navbarNav">
                <ul class="navbar-nav">
                    <li class="nav-item"><a class="nav-link" href="Login.aspx">Iniciar Sesión</a></li>
                    <li class="nav-item"><a class="nav-link" href="Registrarse.aspx">Registrarse</a></li>
                    <li class="nav-item"><a class="nav-link" href="Contacto.aspx">Contacto</a></li>
                </ul>
            </div>
        </div>
    </nav>

   <div id="carouselExample" class="carousel slide" data-bs-ride="carousel">
    <div class="carousel-inner">
        <!-- Slide 1 -->
        <div class="carousel-item active">
            <div class="container d-flex flex-column flex-lg-row align-items-center text-center text-lg-start gap-4">
                <div class="col-lg-6">
                    <img src="imagenes/stock.png" class="img-fluid d-block w-100" alt="Sistema de Control de Stock">
                </div>
                <div class="col-lg-6">
                    <h2 class="text-black">TU STOCK : CONTROL INTELIGENTE</h2>
                    <p>Optimiza tu inventario, reduce pérdidas y maximiza ganancias.</p>
                    <p>Controla en tiempo real tu mercadería, materia prima y productos.</p>
                    <p>Toma decisiones estratégicas con datos precisos y actualizados.</p>
                </div>
            </div>
        </div>

        <!-- Slide 2 -->
        <div class="carousel-item">
            <div class="container d-flex flex-column flex-lg-row align-items-center text-center text-lg-start gap-4">
                <div class="col-lg-6">
                    <img src="imagenes/productos.png" class="img-fluid d-block w-100" alt="Gestión de Productos"/>
                </div>
                <div class="col-lg-6">
                    <h2 class="text-black">GESTION DE PRODUCTO</h2>
                    <p>Actualización de Precios: Mantén siempre tus costos y márgenes optimizados con precios actualizados en tiempo real.</p>
                    <p>Consulta por Proveedores: Encuentra rápidamente quién te abastece, qué productos ofrece y cuándo fue tu última compra.</p>
                </div>
            </div>
        </div>

        <!-- Slide 3 -->
        <div class="carousel-item">
            <div class="container d-flex flex-column flex-lg-row align-items-center text-center text-lg-start gap-4">
                <div class="col-lg-6">
                    <img src="imagenes/inventario.png" class="img-fluid d-block w-100" alt="Control de Inventario">
                </div>
                <div class="col-lg-6">
                    <h2 class="text-black">CONTROL DE INVENTARIO</h2>
                    <p>Registro en Tiempo Real: Mantén actualizado tu stock y evita errores en la gestión de productos.</p>
                    <p>Alertas Inteligentes: Recibe notificaciones cuando un producto esté en niveles críticos o próximos a vencer.</p>
                </div>
            </div>
        </div>
    </div>

    <!-- Controles del carrusel -->
    <button class="carousel-control-prev" type="button" data-bs-target="#carouselExample" data-bs-slide="prev">
        <span class="carousel-control-prev-icon"></span>
    </button>
    <button class="carousel-control-next" type="button" data-bs-target="#carouselExample" data-bs-slide="next">
        <span class="carousel-control-next-icon"></span>
    </button>
</div>
    <hr>
      <!-- Descripción de la Empresa -->
   
    <section class="container my-5 text-center">
      
        <h2>Sobre Nosotros</h2>
        <p>Somos una empresa argentina joven con una misión clara: informatizar y optimizar los procesos de producción, comercialización y gestión para que nuestros clientes alcancen su máximo potencial.

            Nos destacamos por ofrecer soluciones eficientes, diseñadas para mejorar el rendimiento de empresas, negocios y profesionales en un mercado cada vez más competitivo.

            Nuestro compromiso va más allá del software. Contamos con un servicio de atención al cliente permanente, porque sabemos que cada negocio es único y requiere respuestas rápidas y efectivas.</p>

            <p>🔹 Eficiencia, innovación y soporte continuo son los pilares de nuestro trabajo. </p>
            <p>🔹 Tu crecimiento es nuestro objetivo.  </p>
            <p>🔹 Digitalizamos tu negocio para que trabajes mejor y ganes más.</p>
    </section>
        <hr />
         <section class="container my-5 text-center">
             <h2>¡Queremos Impulsar Tu Negocio!</h2>
             <p> ¿Listo para optimizar tu gestión y llevar tu empresa al siguiente nivel? </p>

            <p>Solicitá información: Te contamos todo lo que nuestro sistema puede hacer por vos.</p>
            <p>Pedí una demo sin compromiso: Probalo y descubrí cómo simplifica tu trabajo.</p>
            <p> Contactanos hoy mismo:+54 2257 400092  - Estamos listos para asesorarte y responder todas tus dudas.  </p>
            <p> Fácil, rápido y sin vueltas. Escribinos y empezá a mejorar tu negocio desde ahora.</p>
         </section>
 <hr />
    <section class="container my-5 text-center">
        <h2>Solicita demo</h2>
        <asp:LinkButton runat="server" ID="btnSolicitarDemo" CssClass="link-primary" Text="Registrarme" PostBackUrl="~/Demo/Demo_Registrarse.aspx"/>
         <asp:LinkButton runat="server" ID="btnIniciarDemo" CssClass="link-primary" Text="Iniciar Sesion Demo" PostBackUrl="~/Demo/Demo_Login.aspx"/>
    </section>

    <hr />

    <section class="container my-5 text-center">
        <h3 class="lign-items-center text-center text-lg-start gap-4">Nuestros clientes</h3>
        <br />
        <div class="row">
             <div class="col-md-2">
             <img src="imagenes/sefeliz.png"class="img-fluid d-block w-50 w-md-50 w-lg-50"/>
         </div>
             <div class="col-md-2">
             <img src="imagenes/saboresalcarbon.png"class="img-fluid d-block w-50 w-md-50 w-lg-50"/>
         </div>
             <div class="col-md-2">
             <img src="imagenes/payomotos.png" class="img-fluid d-block w-50 w-md-75 w-lg-25"/>
        </div>                
             <div class="col-md-2">
           <img src="imagenes/welcomehome.jpg" class="img-fluid d-block w-50 w-md-75 w-lg-25"/>
       </div>   
             <div class="col-md-2">
             <img src="imagenes/ilcaballino.png" class="img-fluid d-block w-50 w-md-75 w-lg-25"/>
        </div>                                   
             <div class="col-md-2">
            <img src="imagenes/praline.png" class="img-fluid d-block w-50 w-md-75 w-lg-25" />
       </div>                
             <div class="col-md-2">
           <img src="imagenes/articoheladeria.png"class="img-fluid d-block w-50 w-md-75 w-lg-25"/>
       </div>                           
        </div>
    </section>
<br />



    <!-- Pie de Página -->
    <footer class="bg-dark text-white text-center py-3" style="background-color:">
        <p>Contacto: info@axonnet.store | Tel: +54 9 2257 400092</p>
        <p>&copy; 2025 Empresa Web. Todos los derechos reservados.</p>
    </footer>
		<script src="Scripts/bootstrap.bundle.min.js"></script>

</form>
</body>
</html>
