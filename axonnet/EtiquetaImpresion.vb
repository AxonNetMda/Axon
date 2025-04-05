Imports System.Drawing
Imports System.Drawing.Printing
Public Class EtiquetaImpresion
    Private logo As Image
    Private qr As Image
    Private nombreProducto As String
    Private precio As String
    Private anchoEtiqueta As Integer
    Private altoEtiqueta As Integer
    Public Sub New(logoPath As String, qrPath As String, nombre As String, precioProducto As String, ancho As Integer, alto As Integer)
        logo = Image.FromFile(logoPath)
        qr = Image.FromFile(qrPath)
        nombreProducto = nombre
        precio = precioProducto
        anchoEtiqueta = ancho
        altoEtiqueta = alto
    End Sub

    ' Método para imprimir
    Public Sub ImprimirEtiqueta()
        Dim printDoc As New PrintDocument()
        AddHandler printDoc.PrintPage, AddressOf Me.DibujarEtiqueta

        printDoc.Print()
    End Sub
    ' Método para dibujar la etiqueta
    Private Sub DibujarEtiqueta(sender As Object, e As PrintPageEventArgs)
        Dim g As Graphics = e.Graphics
        Dim fuenteTitulo As New Font("Arial", 12, FontStyle.Bold)
        Dim fuenteTexto As New Font("Arial", 10)
        Dim fuentePrecio As New Font("Arial", 16, FontStyle.Bold)

        ' Dibujar el logo
        g.DrawImage(logo, New Rectangle(10, 10, 100, 50))

        ' Dibujar el código QR
        g.DrawImage(qr, New Rectangle(10, 70, 100, 100))

        ' Dibujar la descripción
        g.DrawString(nombreProducto, fuenteTexto, Brushes.Black, New Rectangle(120, 70, anchoEtiqueta - 130, 50))

        ' Dibujar el precio
        g.DrawString("$ " & precio, fuentePrecio, Brushes.Black, New Point(120, 140))
    End Sub
End Class
