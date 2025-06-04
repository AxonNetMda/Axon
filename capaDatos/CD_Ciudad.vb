Imports capaEntidad
Imports System.Text
Imports System.Runtime.InteropServices
Imports CapaDatos.Conexion
Imports System.Data.SqlClient
Public Class CD_Ciudad
    Public lista As List(Of ciudad) = New List(Of ciudad)
    Dim oconexion As New SqlConnection(conectar.Cadena)
    Public Function Listar(ByVal IdPais As Integer) As List(Of ciudad)
        Dim lista As List(Of ciudad) = New List(Of ciudad)()
        Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)

            Try
                Dim query As StringBuilder = New StringBuilder()
                If IdPais = 0 Then
                    query.AppendLine("SELECT * from ciudades ")
                    query.AppendLine("order by nombreciudad")
                Else
                    query.AppendLine("SELECT * from ciudades ")
                    query.AppendLine("WHERE idPais =" & IdPais & " order by nombre Pais")
                End If

                Dim cmd As SqlCommand = New SqlCommand(query.ToString(), oconexion)
                cmd.CommandType = CommandType.Text
                oconexion.Open()

                Using dr As SqlDataReader = cmd.ExecuteReader()

                    While dr.Read()
                        lista.Add(New ciudad() With {
                            .idCiudad = Convert.ToInt32(dr("idCiudad")),
                            .CodigoPais = UCase(dr("PaisCodigo").ToString()),
                            .NombrePais = Convert.ToBoolean(dr("NombrePais"))
                        })

                    End While

                End Using

            Catch ex As Exception
                lista = New List(Of ciudad)()
            End Try
        End Using

        Return lista
    End Function
End Class
