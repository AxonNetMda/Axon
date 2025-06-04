Imports CapaDatos.capaDatos
Imports System.Data
Imports MySql.Data.MySqlClient
Imports capaEntidad
Imports System.Text
Imports System.Runtime.InteropServices
Imports CapaDatos.Conexion
Imports System.Data.SqlClient

Public Class CD_TipoDocumento
    Public lista As List(Of TipoDocumento) = New List(Of TipoDocumento)
    Dim oconexion As New SqlConnection(conectar.Cadena)
    Public Function Listar() As List(Of TipoDocumento)
        Dim lista As List(Of TipoDocumento) = New List(Of TipoDocumento)()
        Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)

            Try
                Dim query As String
                query = "select * FROM tiposdocumentos ORDER BY strNombre"
                Dim cmd As SqlCommand = New SqlCommand(query, oconexion)
                cmd.CommandType = CommandType.Text
                oconexion.Open()

                Using dr As SqlDataReader = cmd.ExecuteReader()

                    While dr.Read()
                        lista.Add(New TipoDocumento() With {
                            .idTipoDocumento = Convert.ToInt32(dr("idTipoDocumento")),
                            .Nombre = dr("strNombre").ToString(),
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

    Public Function Registrar(ByVal obj As TipoDocumento, <Out> ByRef Mensaje As String) As Integer
        Dim idGenerado As Integer = 0
        Mensaje = String.Empty

        Try

            Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                Dim cmd As SqlCommand = New SqlCommand("sp_TipoDocumento_add", oconexion)
                cmd.Parameters.AddWithValue("p_strNombre", obj.Nombre)
                cmd.Parameters.AddWithValue("p_Habilitado", 1)
                cmd.Parameters.Add("o_Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output
                cmd.Parameters.Add("o_Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output
                cmd.CommandType = CommandType.StoredProcedure
                oconexion.Open()
                cmd.ExecuteNonQuery()
                idGenerado = Convert.ToInt32(cmd.Parameters("o_Respuesta").Value)
                Mensaje = cmd.Parameters("o_Mensaje").Value.ToString()
            End Using

        Catch ex As Exception
            idGenerado = 0
            Mensaje = ex.Message
        End Try

        Return idGenerado
    End Function

    Public Function Editar(ByVal obj As TipoDocumento, <Out> ByRef Mensaje As String) As Boolean
        Dim Respuesta As Boolean = False
        Mensaje = String.Empty

        Try

            Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                Dim cmd As SqlCommand = New SqlCommand("sp_TipoDocumento_editar", oconexion)
                cmd.Parameters.AddWithValue("p_idTipoDocumento", obj.idTipoDocumento)
                cmd.Parameters.AddWithValue("p_strNombre", obj.Nombre)
                cmd.Parameters.AddWithValue("p_Habilitado", 1)
                cmd.Parameters.Add("o_Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output
                cmd.Parameters.Add("o_Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output
                cmd.CommandType = CommandType.StoredProcedure
                oconexion.Open()
                cmd.ExecuteNonQuery()
                Respuesta = Convert.ToBoolean(cmd.Parameters("o_Respuesta").Value)
                Mensaje = cmd.Parameters("o_Mensaje").Value.ToString()
            End Using

        Catch ex As Exception
            Respuesta = False
            Mensaje = ex.Message
        End Try

        Return Respuesta
    End Function

    Public Function Eliminar(ByVal obj As TipoDocumento, <Out> ByRef Mensaje As String) As Boolean
        Dim Respuesta As Boolean = False
        Mensaje = String.Empty

        Try

            Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                Dim cmd As SqlCommand = New SqlCommand("sp_TipoDocumento_borrar", oconexion)
                cmd.Parameters.AddWithValue("p_idTipoDocumento", obj.idTipoDocumento)
                cmd.Parameters.Add("o_Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output
                cmd.Parameters.Add("o_Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output
                cmd.CommandType = CommandType.StoredProcedure
                oconexion.Open()
                cmd.ExecuteNonQuery()
                Respuesta = Convert.ToBoolean(cmd.Parameters("o_Respuesta").Value)
                Mensaje = cmd.Parameters("o_Mensaje").Value.ToString()
            End Using

        Catch ex As Exception
            Respuesta = False
            Mensaje = ex.Message
        End Try

        Return Respuesta

    End Function

End Class
