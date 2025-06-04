Imports CapaDatos
Imports System.Data
Imports System.Data.SqlClient
Imports capaEntidad
Imports System.Text
Imports System.Runtime.InteropServices
Imports CapaDatos.Conexion
Public Class CD_TipoCompra
    Public Function Listar() As List(Of tipocompra)
        Dim lista As List(Of tipocompra) = New List(Of tipocompra)

        Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)

            Try
                Dim query As String = "select * from tipocompra where idTipocompra<>1 order by Nombre"
                Dim cmd As SqlCommand = New SqlCommand(query, oconexion)
                cmd.CommandType = CommandType.Text
                oconexion.Open()

                Using dr As SqlDataReader = cmd.ExecuteReader()

                    While dr.Read()
                        lista.Add(New tipocompra() With {
                            .idTipocompra = Convert.ToInt32(dr("idTipocompra")),
                            .Nombre = dr("Nombre").ToString(),
                            .Estado = dr("Estado").ToString()
                        })
                    End While
                End Using

            Catch ex As Exception
                lista = New List(Of tipocompra)()
            End Try
        End Using

        Return lista
    End Function
End Class
