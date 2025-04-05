<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="producto_imprimir_etiquetas.aspx.vb" Inherits="axonnet.producto_imprimir_etiquetas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <title></title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
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

  
    <style>
        /* Contenedor de la etiqueta */
        .etiqueta {
            width: 9cm;
            height: 3cm;
            border: 1px solid #000;
            padding: 5px;
            box-sizing: border-box;
            display: flex;
            flex-direction: column;
            justify-content: space-between;
            text-align: center;
        }

        /* Logo de la empresa */
        .etiqueta img.logo {
            width: 2.5cm;
            height: auto;
            margin: 0 auto;

        }

        /* Código QR */
        .etiqueta img.qr {
            width: 2cm;
            height: 2cm;
            margin: 0 auto;
        }

        /* Descripción del producto */
        .descripcion {
            font-size: 10px;
            margin-top: 3px;
        }

        /* Código del producto */
        .codigo-producto {
            font-weight: bold;
            font-size: 12px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
         <%If Session("sNombreUsuario") = "" Then
                Response.Redirect("~/login.aspx?idUsuario=0")
            End If %>
  
        <div class="etiqueta">
            <div class="row">
                <div class="col-sm-2">
                    <img src="Imagenes/logowh130.fw.png" alt="Logo Empresa" class="logo" />
                </div>
                 <div class="col-sm-6">
                    <img src='<%= qrImageUrl %>' alt="Código QR" class="qr" />
                 </div>
            </div>
            <!-- Logo de la empresa -->
            

            <!-- Código QR -->
            

            <!-- Descripción del producto -->
            <%--<div class="descripcion">
                <asp:Label ID="lblDescripcionProducto" runat="server" Text="Descripción del producto"></asp:Label>
            </div>--%>

           <%-- <!-- Código del producto -->
            <div class="codigo-producto">
                <asp:Label ID="lblCodigoProducto" runat="server" Text="Código del producto"></asp:Label>
            </div>--%>
        </div>
    </form>
</body>
    <script>
function abrirEnOtraPestana() {
    var url = "https://www.tupagina.com/producto.aspx?id=123&nombre=ProductoEjemplo";
    window.open(url, '_blank');  // Abrir la URL en una nueva pestaña
        }
      
    </script>
</html>
