Imports CapaDatos
Imports System.Data
Imports System.Data.SqlClient
Imports capaEntidad
Imports System.Text
Imports System.Runtime.InteropServices
Imports CapaDatos.Conexion
Public Class CD_TipoDocumentos
    Public Function Listar() As List(Of TipoDocumento)
        Dim lista As List(Of TipoDocumento) = New List(Of TipoDocumento)

        Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)

            Try
                Dim query As String = "select * from tiposdocumentos where estado<>0 order by Nombre"
                Dim cmd As SqlCommand = New SqlCommand(query, oconexion)
                cmd.CommandType = CommandType.Text
                oconexion.Open()

                Using dr As SqlDataReader = cmd.ExecuteReader()

                    While dr.Read()
                        lista.Add(New TipoDocumento() With {
                            .idTipoDocumento = Convert.ToInt32(dr("idTipoDocumento")),
                            .Nombre = dr("Nombre").ToString(),
                            .Estado = Convert.ToBoolean(dr("Estado"))
                        })
                    End While
                End Using

            Catch ex As Exception
                lista = New List(Of TipoDocumento)()
            End Try
        End Using

        Return lista
    End Function
End Class
