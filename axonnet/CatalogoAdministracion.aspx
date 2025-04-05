<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CatalogoAdministracion.aspx.vb" Inherits="capaAdministracion.CatalogoAdministracion" %>
<%@ Import Namespace= "System.Data.SqlClient" %>
<%@ Import Namespace= "capaDatos" %>
<%@ Import Namespace= "System.Data" %>
<%@ Import Namespace= "capaEntidad" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <meta charset="utf-8" />
        <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />       
        <title>Catalogo Web</title>
        <link rel="icon" type="image/x-icon" href="assets/favicon.ico" />
        <link href="css/styles.css" rel="stylesheet" />
        <script src="Scripts/WebForms/MSAjax/MicrosoftAjax.js"></script>
       <script src="Scripts/WebForms/MSAjax/MicrosoftAjaxWebForms.js"></script>
       <link href="css/styles.css" rel="stylesheet" />
        <link href="css/paraimagenes.css" rel="stylesheet" />
        <link href="css/check_css.css" rel="stylesheet" />
        <script type="text/javascript" src="js/plugins/bootstrap/bootstrap-file-input.js"></script>
        <script src="js/datatables/dataTables.fixedHeader.min.js"></script>
        <script src="js/datatables/jquery.dataTables.min.js"></script>
        <script src="js/datatables/jquery3701.js"></script>
        <link href="css/datatables/fixedHeader.dataTables.min.css" rel="stylesheet" />
        <link href="css/datatables/jquery.dataTables.min.css" rel="stylesheet" />  
        <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js"></script>
        <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</head>
<body>
   <form id="form1" runat="server">   
    <nav class="navbar navbar-expand-lg navbar-light bg-warning">
            <div class="container px-4 px-lg-5">
                <a class="navbar-brand" href="#!">Catalogo</a>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation"><span class="navbar-toggler-icon"></span></button>
                <div class="collapse navbar-collapse" id="navbarSupportedContent">
                    <ul class="navbar-nav me-auto mb-2 mb-lg-0 ms-lg-4">
                        <li class="nav-item"><a class="nav-link active" aria-current="page" href="default.aspx">Home</a></li>                        
                       <%-- <li class="nav-item dropdown">
                            <a class="nav-link dropdown-toggle" id="navbarDropdown" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">Shop</a>
                            <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
                                <li><a class="dropdown-item" href="#!">Todos los productos</a></li>
                                <li><hr class="dropdown-divider" /></li>
                                <li><a class="dropdown-item" href="#!">Destacados</a></li>
                                <li><a class="dropdown-item" href="#!">Ofertas</a></li>
                                <li><a class="dropdown-item" href="#!">Nuevos ingresos</a></li>
                            </ul>
                        </li>--%>
                    </ul>
<%--                    <form class="d-flex">--%>
                         <asp:HiddenField ID="nCantidad" runat="server" Value="0"/>
                          <button class="btn btn-outline-dark" type="button">
                            <i class="bi-cart-fill me-1"></i>
                            Carrito
                            <span id="badgeCarrito" class="badge bg-dark text-white ms-1 rounded-pill"><%=nCantidad.Value %></span>
                             
                        </button>
<%--                    </form>--%>
                </div>
            </div>
        </nav>
     <header class="bg-opacity-100 py-2 bg-dark">
    <div class="container bg-dark px-4 px-lg-5">
     <div class="text-center text-white" >
        <h1 class="display-6 fw-bolder">Productos</h1>
     </div>
         
    </div>
         </header>
 <section class="py-5">

   
       
        <div class="container mt-4">
           <div class="row" id="productosContainer" runat="server">            
               <%
                   Dim query As String = "SELECT dbo.producto.idProducto, dbo.producto.Nombre as NombreProducto, dbo.producto_Stock.StockActual,
                                    dbo.preciodeventa(producto.PrecioCosto,  producto.AlicuotaIVA, producto.Ganancia) as PrecioVenta, dbo.producto.RutaImagen,
                                    dbo.producto.nombreFoto1, dbo.producto.nombreFoto2, dbo.producto.nombreFoto3, dbo.producto.nombreFoto4,
                                    dbo.producto.nombreFoto5,dbo.producto.nombreFoto6,dbo.producto.nombreFoto7, dbo.producto.nombreFoto8,
                                    dbo.producto.nombreFoto9, dbo.producto.nombreFoto10, dbo.producto.nombreFoto11, dbo.producto.nombreFoto12,
                                    dbo.producto.Notas,  dbo.producto.EsDestacado,  dbo.producto.EsOferta
                                    FROM dbo.producto
                                    INNER JOIN dbo.producto_Stock ON dbo.producto.idProducto = dbo.producto_Stock.idProducto
                                    WHERE dbo.producto_Stock.idSucursal = 1 AND dbo.producto.MostrarCatalogo = 1 ORDER BY dbo.producto.Nombre"
                   Dim dt As New DataTable()

                   ' Obtener los datos desde la base de datos
                   Using conn As New SqlConnection(Conexion.conectar.Cadena)
                       Using cmd As New SqlCommand(query, conn)
                           conn.Open()
                           Dim da As New SqlDataAdapter(cmd)
                           da.Fill(dt)
                       End Using
                   End Using
                   Dim sImagen As String
                   ' Generar dinámicamente las tarjetas de productos
                   For Each row As DataRow In dt.Rows
                       If row("nombreFoto1") = "sinfoto.JPG" Then
                           sImagen = "imagenes/imgsinfoto.png"

                       Else
                           sImagen = "productos/" & row("nombreFoto1")
                       End If
                %>
               <div class='col-md-3 mb-3'>
                          <div class='card h-100'>
                                <img src='<%=sImagen %>' class='card-img-top' alt='<%=row("NombreProducto") %>' />
                                <div class='card-body'>
                                    <h5 class='card-title'> <%= row("idProducto") %>  <%=row("NombreProducto") %> </h5>
                                    <p class='card-text'><strong> <%= FormatCurrency(row("PrecioVenta"), 2) %></strong> </p>
                                    <p class='card-text'>Stock: <%=row("StockActual") %></p>
                                    <div class='card-footer p-4 pt-0 border-top-0 bg-transparent' style='bottom: 0.5rem; left: 0.5rem'> </div> 
                                <%--' Etiqueta de Nuevo--%>
                                    <%
                                        If row("EsDestacado") Then %>
                                                <span class='badge bg-warning text-black position-absolute' style='top: 0.5rem; right: 0.5rem'>Nuevo</span> 
                                            <%End If
                                    %>

                               <%-- ' Etiqueta de Oferta--%>
                                   <%If Convert.ToBoolean(row("EsOferta")) Then  %>
                                        <span class='badge bg-danger text-white position-absolute' style='top: 0.5rem; left: 0.5rem'>Oferta</span>
                                    <% End If %>
                               
                                <%--' Cerrar el cuerpo de la tarjeta--%>
                            </div>
                                <%-- Cerrar card-body

                                Agregar el pie de tarjeta con el botón dentro del footer--%>

                                <div class='card-footer text-center'>
                                     <asp:button runat="server" ID="BtnInfo" cssclass="btn btn-outline-dark btn-sm" Text="INFO.." PostBackUrl='Catalogo_Ficha.aspx?IdProducto=<%=row("NombreProducto")%>' />
                                     <%--<a href='#' class='btn btn-warning btn-sm'>Info</a> &nbsp; &nbsp;--%>
<%--                                     <a href='#' class='btn btn-outline-dark btn-sm'>Agregar al carrito</a>--%>
                                     <asp:button runat="server" ID="btnAgregarCarrito" cssclass="btn btn-outline-dark btn-sm" Text="Agregar al carrito" OnClick="btnAgregarCarrito_Click" />
                                </div>

                                 <%--Cerrar la tarjeta completa--%>
                        </div>

                     </div>
               <% Next  %>
            </div>
        </div>
 
   
</section> 

   </form>
   
        <script>
            function resetearCarrito() {
                document.getElementById('badgeCarrito').textContent = '0';
            }

           let cantidadCarrito = 0;
           function agregarAlCarrito(idProducto, precio) {
                cantidadCarrito++;
            document.getElementById("badgeCarrito").textContent = cantidadCarrito;

               // Enviar los datos al servidor usando AJAX
               const xhr = new XMLHttpRequest();
               xhr.open("POST", "GuardarEnCarrito.aspx", true);
               xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");

               xhr.onreadystatechange = function () {
                   if (xhr.readyState === 4 && xhr.status === 200) {
                       console.log("Producto agregado al carrito.");
                   }
               };

               // Enviar los datos al servidor
               const data = `idProducto=${idProducto}&precio=${precio}&sucursal=${sucursal}&referencia=${referencia}&cantidad=1`;
               xhr.send(data);
    }

         </script>
</body>
</html>
