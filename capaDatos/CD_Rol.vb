Imports CapaDatos
Imports System.Data
Imports System.Data.SqlClient
Imports capaEntidad
Imports System.Text
Imports System.Runtime.InteropServices
Imports CapaDatos.Conexion

Public Class CD_Rol
    Public Function Listar() As List(Of rol)
        Dim lista As List(Of rol) = New List(Of rol)

        Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)

            Try
                Dim query As String = "select * from rol where idRol<>1 order by Nombre"
                Dim cmd As SqlCommand = New SqlCommand(query, oconexion)
                cmd.CommandType = CommandType.Text
                oconexion.Open()

                Using dr As SqlDataReader = cmd.ExecuteReader()

                    While dr.Read()
                        lista.Add(New rol() With {
                            .idRol = Convert.ToInt32(dr("idrol")),
                            .Nombre = dr("Nombre").ToString(),
                            .Estado = dr("Estado").ToString()
                        })
                    End While
                End Using

            Catch ex As Exception
                lista = New List(Of rol)()
            End Try
        End Using

        Return lista
    End Function
End Class
