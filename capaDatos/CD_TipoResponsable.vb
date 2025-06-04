Imports CapaDatos
Imports System.Data
Imports System.Data.SqlClient
Imports capaEntidad
Imports System.Text
Imports System.Runtime.InteropServices
Imports CapaDatos.Conexion
Public Class CD_TipoResponsable
    Public Function Listar() As List(Of tiposresponsables)
        Dim lista As List(Of tiposresponsables) = New List(Of tiposresponsables)

        Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)

            Try
                Dim query As String = "select * from tiposderesponsables where idTiporesponsable<>0 order by Nombre"
                Dim cmd As SqlCommand = New SqlCommand(query, oconexion)
                cmd.CommandType = CommandType.Text
                oconexion.Open()

                Using dr As SqlDataReader = cmd.ExecuteReader()

                    While dr.Read()
                        lista.Add(New tiposresponsables() With {
                            .idTipoResponsable = Convert.ToInt32(dr("idTiporesponsable")),
                            .Nombre = dr("Nombre").ToString(),
                            .Estado = dr("Estado").ToString()
                        })
                    End While
                End Using

            Catch ex As Exception
                lista = New List(Of tiposresponsables)()
            End Try
        End Using

        Return lista
    End Function
End Class
