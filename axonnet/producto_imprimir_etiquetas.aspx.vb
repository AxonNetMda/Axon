Imports System.Data.SqlClient
Imports System.Drawing
Imports QRCoder
Imports System.Drawing.Printing ' Para la impresión
Imports CapaDatos
Imports CapaDatos.Conexion
Imports System.IO

Public Class producto_imprimir_etiquetas
    Inherits System.Web.UI.Page
    Public Property idproducto As Integer
    Public Property Nombreproducto As String
    Public Property pathficha As String
    Public Property Empresa As String
    Public Property logo As Image
    Public Property cantidad As String
    ' Conexión a la base de datos
    Public Property qrImageUrl As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' Datos de ejemplo: puedes cambiar esto dinámicamente
            Dim descripcionProducto As String = Request.QueryString("nombreproducto")
            Dim codigoProducto As String = Request.QueryString("idproducto")
            Dim urlFicha As String = Request.QueryString("urlFicha")
            Dim cantidad As String = Request.QueryString("cantidad")
            ' Asignar los valores a los controles de la página
            'lblDescripcionProducto.Text = descripcionProducto
            'lblCodigoProducto.Text = codigoProducto

            ' Generar código QR
            Dim qrData As String = codigoProducto ' Usar el código del producto como contenido del QR
            qrImageUrl = GenerarQRCode(qrData)
        End If
    End Sub

    ' Función para generar un código QR y devolver la URL de la imagen
    Private Function GenerarQRCode(ByVal data As String) As String
        Dim qrGenerator As New QRCodeGenerator()
        Dim qrCodeData As QRCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q)
        Dim qrCode As New QRCode(qrCodeData)

        ' Crear la imagen QR en memoria
        Using bitMap As Bitmap = qrCode.GetGraphic(20)
            Using stream As New MemoryStream()
                ' Guardar la imagen en un stream
                bitMap.Save(stream, System.Drawing.Imaging.ImageFormat.Png)
                ' Convertir la imagen en base64 para usarla en la etiqueta img
                Dim base64Image As String = Convert.ToBase64String(stream.ToArray())
                ' Devolver la URL de la imagen en base64
                Return "data:image/png;base64," & base64Image
            End Using
        End Using
    End Function
End Class


