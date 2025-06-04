Imports System.Drawing
Imports System.Drawing.Printing
Imports CapaDatos
Imports capaEntidad
Imports capaNegocio
Imports QRCoder
Imports axonnet.EtiquetaImpresion

Public Class producto_etiquetas
    Inherits System.Web.UI.Page
    Public Property spath As String
    Public Property idproducto As Integer

    Dim Mensaje As String
    Dim Script As String



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txtidProducto.Value = Request.QueryString("idproducto")

            Dim lst As List(Of producto) = New CN_Producto().Listar(txtidProducto.Value)
            If lst.Count > 0 Then
                txtNombre.Text = lst(0).Nombre
                txtIdProd.Text = lst(0).idProducto
                txtPrecio.Text = lst(0).PrecioVenta
                spath = "Catalogo_Ficha.aspx?IdProducto=" & txtIdProd.Text
                GenerarEtiqueta(txtIdProd.Text, txtNombre.Text, spath)
                AsignarImagenQR()
            Else

            End If
        End If
    End Sub
    Private Sub AsignarImagenQR()
        ' Obtener el ID del producto desde el TextBox
        'Dim idProducto As String = txtIdProd.Text

        '' Construir la URL de la imagen QR


        '' Asignar la URL al control Image



        Dim rutaImagenQR As String = Server.MapPath("~/QRImagenes/" & txtidProducto.Value & ".png") ' Reemplaza con el nombre del archivo
        Dim contenidoQR As String = LeerCodigoQR(rutaImagenQR)

        If contenidoQR.Contains("Código:") Then
            ' Separar los datos del QR en líneas
            Dim lineas() As String = contenidoQR.Split(vbCrLf)

            ' Extraer los valores
            Dim idProducto As String = lineas(0).Replace("Código: ", "").Trim()
            Dim nombreProducto As String = lineas(1).Replace("Producto: ", "").Trim()
            Dim urlProducto As String = lineas(2).Replace("Ver más: ", "").Trim()
            Dim rutaImagen As String = ResolveUrl("~/QRImagenes/" & idProducto & ".png")
            imgQR.ImageUrl = rutaImagen
            ' Mostrar en la página o usarlo en la lógica
            lblMensajeAtencion.Text = "ID: " & idProducto & "<br>Nombre: " & nombreProducto & "<br>URL: <a href='" & urlProducto & "'>Ver Producto</a>"
            Script = "$(function() { showModalMensaje(); }); "
            ScriptManager.RegisterStartupScript(Me, Page.GetType(), "btn1", Script, True)
        Else
            lblMensajeAtencion.Text = "Error: " & contenidoQR
            Script = "$(function() { showModalMensaje(); }); "
            ScriptManager.RegisterStartupScript(Me, Page.GetType(), "btn1", Script, True)
        End If
    End Sub
    Protected Sub btnImprimir_Click(sender As Object, e As EventArgs)
        'If txtidProducto.Value <> 0 Then
        '    ImprimirEtiqueta()
        '    Response.Redirect("producto_imprimir_etiquetas.aspx?idProducto=" & txtidProducto.Value & "&Nombre=" & txtNombre.Text & "&urlFicha=" & spath & "&Cantidad=" & Val(txtCantidad.Text))
        'Else
        '    lblMensajeAtencion.Text = Mensaje
        '    Script = "$(function() { showModalMensaje(); }); "
        '    ScriptManager.RegisterStartupScript(Me, Page.GetType(), "btn1", Script, True)
        'End If
        Try
            ' Ruta de las imágenes (ajusta según tu servidor)
            Dim logoPath As String = Server.MapPath("~/Imagenes/logo.png")
            Dim qrPath As String = Server.MapPath("~/QRImagenes/" & txtIdProd.Text & ".png")

            ' Datos del producto
            Dim nombreProducto As String = "Producto de Ejemplo" ' Aquí puedes obtener el valor de un TextBox u otra fuente
            Dim precio As String = "10.000" ' Puedes obtenerlo de un control o BD
            Dim anchoEtiqueta As Integer = 300
            Dim altoEtiqueta As Integer = 150

            ' Crear instancia de la etiqueta e imprimir
            Dim etiqueta As New EtiquetaImpresion(logoPath, qrPath, nombreProducto, precio, anchoEtiqueta, altoEtiqueta)
            etiqueta.ImprimirEtiqueta()

        Catch ex As Exception
            ' Manejo de errores
            Response.Write("<script>alert('Error al imprimir: " & ex.Message & "');</script>")
        End Try
    End Sub

    Private Sub GenerarEtiqueta(codigoProducto As Integer, nombreProducto As String, urlProducto As String)
        ' Generar los datos para el código QR
        Dim datosQR As String = "Código: " & codigoProducto & vbCrLf &
                                "Producto: " & nombreProducto & vbCrLf &
                                "Ver más: " & urlProducto

        ' Generar el código QR
        Dim generator As New QRCodeGenerator()
        Dim data = generator.CreateQrCode(datosQR, QRCodeGenerator.ECCLevel.Q)
        Dim qrCode As New QRCode(data)
        Dim qrImage As Bitmap = qrCode.GetGraphic(20)

        ' Guardar la imagen en el servidor (opcional, puedes directamente imprimir)
        Dim rutaImagen As String = Server.MapPath("~/QRImagenes/") & codigoProducto.ToString() & ".png"
        qrImage.Save(rutaImagen)

        ' Imprimir la etiqueta usando el método de impresión
        ' ImprimirEtiqueta(qrImage, codigoProducto, nombreProducto)
    End Sub

    Private Sub ImprimirEtiqueta(codigoProducto As String, nombreProducto As String, urlProducto As String)
        ' Crear un objeto PrintDocument
        Dim printDoc As New PrintDocument()
        Dim datosQR As String = "Código: " & codigoProducto & vbCrLf &
                                "Producto: " & nombreProducto & vbCrLf &
                                "Ver más: " & urlProducto

        ' Generar el código QR
        Dim generator As New QRCodeGenerator()
        Dim data = generator.CreateQrCode(datosQR, QRCodeGenerator.ECCLevel.Q)
        Dim qrCode As New QRCode(data)
        Dim qrImage As Bitmap = qrCode.GetGraphic(20)

        '' Guardar la imagen en el servidor (opcional, puedes directamente imprimir)
        'Dim rutaImagen As String = Server.MapPath("~/QRImagenes/") & codigoProducto.ToString() & ".png"
        'qrImage.Save(rutaImagen)


        ' Configurar el evento de impresión
        AddHandler printDoc.PrintPage, Sub(sender As Object, e As PrintPageEventArgs)
                                           ' Dibujar el código QR en la página de impresión
                                           e.Graphics.DrawImage(qrImage, 50, 50, 150, 150) ' Ajusta las posiciones y tamaños

                                           ' Dibujar el código del producto y nombre
                                           e.Graphics.DrawString("Código: " & codigoProducto, New Font("Arial", 12), Brushes.Black, 50, 200)
                                           e.Graphics.DrawString("Producto: " & nombreProducto, New Font("Arial", 12), Brushes.Black, 50, 200)
                                           e.Graphics.DrawString("Precio: " & txtPrecio.Text, New Font("Arial", 12), Brushes.Black, 50, 200)
                                       End Sub

        ' Enviar el documento a la impresora predeterminada
        Try
            printDoc.Print()
        Catch ex As Exception
            ' Manejo de errores de impresión
            Response.Write("<script>alert('Error al imprimir: " & ex.Message & "');</script>")
        End Try
    End Sub


End Class