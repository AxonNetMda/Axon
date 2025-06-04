Imports capaEntidad
Imports System.Text
Imports System.Runtime.InteropServices
Imports CapaDatos.Conexion
Imports System.Data.SqlClient
Public Class CD_Pais
    Public lista As List(Of Pais) = New List(Of Pais)
    Dim oconexion As New SqlConnection(conectar.Cadena)
    Public Function Listar(ByVal IdPais As Integer) As List(Of Pais)
        Dim lista As List(Of Pais) = New List(Of Pais)()
        Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)

            Try
                Dim query As StringBuilder = New StringBuilder()
                If IdPais = 0 Then
                    query.AppendLine("SELECT * from paises ")
                    query.AppendLine("order by nombrepais")
                Else
                    query.AppendLine("SELECT * from paises ")
                    query.AppendLine("WHERE idPais =" & IdPais & " order by nombre Pais")
                End If

                Dim cmd As SqlCommand = New SqlCommand(query.ToString(), oconexion)
                cmd.CommandType = CommandType.Text
                oconexion.Open()

                Using dr As SqlDataReader = cmd.ExecuteReader()

                    While dr.Read()
                        lista.Add(New Pais() With {
                            .idPais = Convert.ToInt32(dr("idPais")),
                            .sIdPais = UCase(dr("PaisCodigo").ToString()),
                            .NombrePais = dr("NombrePais")
                        })

                    End While

                End Using

            Catch ex As Exception
                lista = New List(Of Pais)()
            End Try
        End Using

        Return lista
    End Function

End Class
