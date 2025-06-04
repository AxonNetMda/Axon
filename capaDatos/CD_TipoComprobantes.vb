Imports System.Data
Imports System.Data.SqlClient
Imports capaEntidad
Imports System.Text
Imports System.Runtime.InteropServices
Imports CapaDatos.Conexion
Public Class CD_TipoComprobantes
    Public Function Listar() As List(Of tipocomprobantes)
        Dim lista As List(Of tipocomprobantes) = New List(Of tipocomprobantes)

        Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)

            Try
                Dim query As String = "select * from tipodecomprobante where idComprobante<>0 and inthabilitado<>0 order by idComprobante"
                Dim cmd As SqlCommand = New SqlCommand(query, oconexion)
                cmd.CommandType = CommandType.Text
                oconexion.Open()

                Using dr As SqlDataReader = cmd.ExecuteReader()

                    While dr.Read()
                        lista.Add(New tipocomprobantes() With {
                            .idTipoComprobante = Convert.ToInt32(dr("idComprobante")),
                            .strNombre = dr("strNombre").ToString(),
                            .intHAbilitado = dr("intHabilitado").ToString(),
                            .Letra = dr("letra").ToString
                        })
                    End While
                End Using

            Catch ex As Exception
                lista = New List(Of tipocomprobantes)()
            End Try
        End Using

        Return lista
    End Function
End Class
