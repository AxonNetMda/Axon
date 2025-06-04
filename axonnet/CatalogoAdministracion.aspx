<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CatalogoAdministracion.aspx.vb" Inherits="axonnet.CatalogoAdministracion" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="capaDatos" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="capaEntidad" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <title>Catalogo Web</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://use.fontawesome.com/releases/v6.3.0/js/all.js" crossorigin="anonymous"></script>
    <link href="css/styles.css" rel="stylesheet" />
    <link href="css/paraimagenes.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
<nav class="navbar navbar-expand-lg navbar-light bg-warning">
    <div class="container px-4 px-lg-5">
        <a class="navbar-brand" href="#">Catálogo</a>
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent"
                aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarSupportedContent">
            <ul class="navbar-nav me-auto mb-2 mb-lg-0 ms-lg-4">
                <li class="nav-item">
                    <a class="nav-link active" href="#">Inicio</a>
                </li>
                <li class="nav-item dropdown">
                    <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button"
                       data-bs-toggle="dropdown" aria-expanded="false">Productos</a>
                    <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
                        <li><a class="dropdown-item" href="#">Todos</a></li>
                        <li><a class="dropdown-item" href="#">Nuevos</a></li>
                        <li><a class="dropdown-item" href="#">Ofertas</a></li>
                    </ul>
                </li>
            </ul>

            <div class="d-flex">
                <span class="me-3">
                    <% 
                        If Session("usuario") IsNot Nothing Then %>
                        <i class="fas fa-user"></i> <%= Session("usuario") %>
                    <% Else %>
                        <a href="Login.aspx" class="btn btn-outline-dark btn-sm">
                            <i class="fas fa-sign-in-alt"></i> Iniciar Sesión
                        </a>
                    <% End If %>
                </span>

                <a class="btn btn-outline-dark" href="Carrito.aspx">
                    <i class="fas fa-shopping-cart me-1"></i>
                    Carrito
                    <span class="badge bg-dark text-white ms-1 rounded-pill" id="badgeCarrito">
                        <% Dim nCantidad As Integer
                            If Session("Carrito") IsNot Nothing Then %>
                            <%= CType(Session("Carrito"), List(Of Object)).Count %>
                        <% Else %>
                            0
                        <% End If %>
                    </span>
                </a>
            </div>
        </div>
    </div>
</nav>

        <header class="py-2 bg-dark text-white">
            <div class="container">
                <!-- Cabecera personalizada si lo deseás -->
            </div>
        </header>

        <section class="py-5">
            <div class="container mt-4">
                <div class="row" id="productosContainer" runat="server">
                    <%
                        Dim query As String = "SELECT p.idProducto, p.Nombre as NombreProducto, ps.StockActual,
                                            dbo.preciodeventa2(p.PrecioCosto, p.AlicuotaIVA, p.Ganancia) as PrecioVenta,
                                            p.RutaImagen, p.nombreFoto1, p.EsDestacado, p.EsOferta
                                            FROM dbo.producto p
                                            INNER JOIN dbo.producto_Stock ps ON p.idProducto = ps.idProducto
                                            WHERE ps.idSucursal = 1 AND p.MostrarCatalogo = 1
                                            ORDER BY p.Nombre"
                        Dim dt As New DataTable()
                        Using conn As New SqlConnection(Conexion.conectar.Cadena)
                            Using cmd As New SqlCommand(query, conn)
                                conn.Open()
                                Dim da As New SqlDataAdapter(cmd)
                                da.Fill(dt)
                            End Using
                        End Using

                        Dim sImagen As String
                        For Each row As DataRow In dt.Rows
                            sImagen = If(row("nombreFoto1").ToString() = "sinfoto.JPG", "imagenes/imgsinfoto.png", "productos/" & row("nombreFoto1"))
                    %>
                        <div class="col-md-3 mb-4">
                            <div class="card h-100 position-relative">
                                <img src="<%=sImagen %>" class="card-img-top" alt="<%=row("NombreProducto") %>" />
                                <div class="card-body text-center">
                                    <h5 class="card-title"><%= row("idProducto") %> - <%= row("NombreProducto") %></h5>
                                    <p class="card-text text-success fw-bold"><%= FormatCurrency(row("PrecioVenta"), 2) %></p>
                                    <p class="card-text">Stock: <%= row("StockActual") %></p>
                                </div>

                                <% If row("EsDestacado") Then %>
                                    <span class="badge bg-warning text-black position-absolute top-0 end-0 m-2">Nuevo</span>
                                <% End If %>
                                <% If Convert.ToBoolean(row("EsOferta")) Then %>
                                    <span class="badge bg-danger text-white position-absolute top-0 start-0 m-2">Oferta</span>
                                <% End If %>

                                <div class="card-footer d-flex justify-content-around">
                                    <a href='Catalogo_Ficha.aspx?IdProducto=<%= row("idProducto") %>' class="btn btn-outline-dark btn-sm">Info</a>
                                    <a href="javascript:void(0);" class="btn btn-outline-primary btn-sm"
                                       onclick="agregarAlCarrito('<%= row("idProducto") %>', '<%= row("PrecioVenta") %>')">Agregar al carrito</a>
                                </div>
                            </div>
                        </div>
                    <%
                        Next
                    %>
                </div>
            </div>
        </section>
    </form>

    <script>
        let cantidadCarrito = 0;
        function agregarAlCarrito(idProducto, precio) {
            cantidadCarrito++;
            document.getElementById("badgeCarrito")?.textContent = cantidadCarrito;

            const xhr = new XMLHttpRequest();
            xhr.open("POST", "GuardarEnCarrito.aspx", true);
            xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");

            const sucursal = 1;
            const referencia = "WEB";

            const data = `idProducto=${idProducto}&precio=${precio}&sucursal=${sucursal}&referencia=${referencia}&cantidad=1`;
            xhr.send(data);
        }
    </script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
