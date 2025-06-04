Imports System.Data.SqlClient
Imports capaEntidad
Imports System.Text
Imports System.Runtime.InteropServices
Imports CapaDatos.Conexion
Public Class CD_Categoria
    Public lista As List(Of categoria) = New List(Of categoria)
    Dim oconexion As New SqlConnection(conectar.Cadena)
    Public Function Listar(ByVal nIdCategoria As Integer) As List(Of categoria)
        Dim lista As List(Of categoria) = New List(Of categoria)()
        Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)

            Try
                Dim query As StringBuilder = New StringBuilder()
                If nIdCategoria = 0 Then
                    query.AppendLine("SELECT * from producto_categoria ")
                    query.AppendLine("order by producto_categoria.nombre ")
                Else
                    query.AppendLine("SELECT * from producto_categoria ")
                    query.AppendLine("WHERE producto_categoria.idCategoria =" & nIdCategoria & " order by producto_categoria.nombre 
")
                End If

                Dim cmd As SqlCommand = New SqlCommand(query.ToString(), oconexion)
                cmd.CommandType = CommandType.Text
                oconexion.Open()

                Using dr As SqlDataReader = cmd.ExecuteReader()

                    While dr.Read()
                        lista.Add(New categoria() With {
                            .idCategoria = Convert.ToInt32(dr("idCategoria")),
                            .Nombre = UCase(dr("Nombre").ToString()),
                            .Estado = Convert.ToBoolean(dr("Estado"))
                        })

                    End While

                End Using

            Catch ex As Exception
                lista = New List(Of categoria)()
            End Try
        End Using

        Return lista
    End Function

    Public Function Registrar(ByVal obj As categoria, <Out> ByRef Mensaje As String) As Integer
        Dim idGenerado As Integer = 0
        Mensaje = String.Empty

        Try

            Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                Dim cmd As SqlCommand = New SqlCommand("sp_categoria_add", oconexion)
                cmd.Parameters.AddWithValue("@Nombre", obj.Nombre)
                cmd.Parameters.AddWithValue("@Estado", True)
                cmd.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output
                cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output
                cmd.CommandType = CommandType.StoredProcedure
                oconexion.Open()
                cmd.ExecuteNonQuery()
                idGenerado = Convert.ToInt32(cmd.Parameters("@Resultado").Value)
                Mensaje = cmd.Parameters("@Mensaje").Value.ToString()
            End Using

        Catch ex As Exception
            idGenerado = 0
            Mensaje = ex.Message
        End Try

        Return idGenerado
    End Function

    Public Function Editar(ByVal obj As categoria, <Out> ByRef Mensaje As String) As Boolean
        Dim Respuesta As Boolean = False
        Mensaje = String.Empty

        Try

            Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                Dim cmd As SqlCommand = New SqlCommand("sp_categoria_edit", oconexion)
                cmd.Parameters.AddWithValue("@Idcategoria", obj.idCategoria)
                cmd.Parameters.AddWithValue("@Nombre", obj.Nombre)
                cmd.Parameters.AddWithValue("@Estado", Convert.ToBoolean(obj.Estado))
                cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output
                cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output
                cmd.CommandType = CommandType.StoredProcedure
                oconexion.Open()
                cmd.ExecuteNonQuery()
                Respuesta = Convert.ToBoolean(cmd.Parameters("@Resultado").Value)
                Mensaje = cmd.Parameters("@Mensaje").Value.ToString()
            End Using

        Catch ex As Exception
            Respuesta = False
            Mensaje = ex.Message
        End Try

        Return Respuesta
    End Function

    Public Function Eliminar(ByVal obj As categoria, <Out> ByRef Mensaje As String) As Boolean
        Dim Respuesta As Boolean = False
        Mensaje = String.Empty

        Try

            Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                Dim cmd As SqlCommand = New SqlCommand("sp_categoria_borrar", oconexion)
                cmd.Parameters.AddWithValue("@idcategoria", obj.idCategoria)
                cmd.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output
                cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output
                cmd.CommandType = CommandType.StoredProcedure
                oconexion.Open()
                cmd.ExecuteNonQuery()
                Respuesta = Convert.ToBoolean(cmd.Parameters("@Resultado").Value)
                Mensaje = cmd.Parameters("@Mensaje").Value.ToString()
            End Using

        Catch ex As Exception
            Respuesta = False
            Mensaje = ex.Message
        End Try

        Return Respuesta

    End Function


End Class
