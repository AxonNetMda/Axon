Imports System.Data.SqlClient
Imports System.Drawing
Imports CapaDatos
Imports CapaDatos.Conexion
Imports ZXing

Module funciones
    Public Class OpcionCombo
        Public Property valor
        Public Property texto

    End Class
    Public Function UltimoNumeroComprobante(ByVal sTabla As String, ByVal sCampo As String, ByVal sLetra As String) As Integer
        Dim cnn As New SqlConnection(Conexion.conectar.Cadena)
        Dim sSQL As String
        Dim numero As Integer = 0
        sSQL = "SELECT max(" & sTabla & "." & sCampo & ") FROM " & sTabla & " WHERE Letra='" & sLetra & "';"
        'creo comando
        Dim cmd As New SqlCommand(sSQL, cnn)
        'creo datareader
        Dim odataReader As SqlDataReader
        cnn.Open()
        odataReader = cmd.ExecuteReader()
        odataReader.Read()
        If IsDBNull(odataReader.Item(0)) Then
            numero = 1
        Else
            numero = odataReader.Item(0) + 1
        End If
        Return numero
    End Function
    Public Function VerificarYCrearTabla(ByVal queryVerificar As String) As String
        Dim mensaje As String = ""
        Dim conexionString As String = conectar.Cadena
        Using conexion As New SqlConnection(conexionString)
            Try
                conexion.Open()
                Using comando As New SqlCommand(queryVerificar, conexion)
                    comando.ExecuteNonQuery()
                End Using
                conexion.Close()
            Catch ex As Exception
                ' Manejo de errores
                mensaje = "Error en la creacion de la tabla:" & ex.Message
            End Try
        End Using
        Return mensaje
    End Function
    Public Function LeerCodigoQR(rutaImagen As String) As String
        Try
            ' Cargar la imagen del código QR
            Dim imagenQR As Bitmap = New Bitmap(rutaImagen)

            ' Inicializar el lector de QR
            Dim lector As New BarcodeReader()
            Dim resultado As ZXing.Result = lector.Decode(imagenQR)

            ' Si se pudo leer el QR, devolver el contenido
            If resultado IsNot Nothing Then
                Return resultado.Text ' Contenido del QR (Código, Producto, URL)
            Else
                Return "No se pudo leer el código QR."
            End If
        Catch ex As Exception
            Return "Error al leer el QR: " & ex.Message
        End Try
    End Function
End Module
