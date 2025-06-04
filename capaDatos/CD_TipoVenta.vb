Imports CapaDatos
Imports System.Data
Imports System.Data.SqlClient
Imports capaEntidad
Imports System.Text
Imports System.Runtime.InteropServices
Imports CapaDatos.Conexion
Public Class CD_TipoVenta
    Public Function Listar() As List(Of tipoventa)
        Dim lista As List(Of tipoventa) = New List(Of tipoventa)

        Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)

            Try
                Dim query As String = "select * from tipoventa where idTipoVenta<>1 order by Nombre"
                Dim cmd As SqlCommand = New SqlCommand(query, oconexion)
                cmd.CommandType = CommandType.Text
                oconexion.Open()

                Using dr As SqlDataReader = cmd.ExecuteReader()

                    While dr.Read()
                        lista.Add(New tipoventa() With {
                            .idTipoVenta = Convert.ToInt32(dr("idTipoVenta")),
                            .Nombre = dr("Nombre").ToString(),
                            .Estado = dr("Estado").ToString()
                        })
                    End While
                End Using

            Catch ex As Exception
                lista = New List(Of tipoventa)()
            End Try
        End Using

        Return lista
    End Function
End Class
