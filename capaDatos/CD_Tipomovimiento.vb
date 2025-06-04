Imports CapaDatos
Imports System.Data
Imports System.Data.SqlClient
Imports capaEntidad
Imports System.Text
Imports System.Runtime.InteropServices
Imports CapaDatos.Conexion
Public Class CD_Tipomovimiento
    Public Function Listar() As List(Of tipomovimiento)
        Dim lista As List(Of tipomovimiento) = New List(Of tipomovimiento)

        Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)

            Try
                Dim query As String = "select * from tipomovimiento order by Nombre"
                Dim cmd As SqlCommand = New SqlCommand(query, oconexion)
                cmd.CommandType = CommandType.Text
                oconexion.Open()

                Using dr As SqlDataReader = cmd.ExecuteReader()

                    While dr.Read()
                        lista.Add(New tipomovimiento() With {
                            .idTipoMovimiento = Convert.ToInt32(dr("idTipoMovimiento")),
                            .Letra = dr("Letra").ToString(),
                            .Nombre = dr("Nombre").ToString(),
                            .Estado = dr("Estado").ToString()
                        })
                    End While
                End Using

            Catch ex As Exception
                lista = New List(Of tipomovimiento)()
            End Try
        End Using

        Return lista
    End Function
End Class
