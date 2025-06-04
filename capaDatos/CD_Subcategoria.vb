Imports System.Data.SqlClient
Imports capaEntidad
Imports System.Text
Imports System.Runtime.InteropServices
Imports CapaDatos.Conexion
Public Class CD_Subcategoria
    Public lista As List(Of subcategoria) = New List(Of subcategoria)
    Dim oconexion As New SqlConnection(conectar.Cadena)
    Public Function Listar(ByVal nIdSubCategoria As Integer) As List(Of subcategoria)
        Dim lista As List(Of subcategoria) = New List(Of subcategoria)()
        Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)

            Try
                Dim query As StringBuilder = New StringBuilder()
                If nIdSubCategoria = 0 Then
                    query.AppendLine("Select dbo.producto_subCategoria.idSubCategoria, dbo.producto_subCategoria.idCategoria, dbo.producto_subCategoria.Nombre, dbo.producto_subCategoria.Estado, ")
                    query.AppendLine("dbo.producto_categoria.Nombre AS NombreCategoria From dbo.producto_subCategoria INNER Join dbo.producto_categoria ON dbo.producto_subCategoria.idCategoria = dbo.producto_categoria.idCategoria ")
                    query.AppendLine("Order By dbo.producto_subCategoria.Nombre")
                Else
                    query.AppendLine("Select dbo.producto_subCategoria.idSubCategoria, dbo.producto_subCategoria.idCategoria, dbo.producto_subCategoria.Nombre, dbo.producto_subCategoria.Estado, ")
                    query.AppendLine("dbo.producto_categoria.Nombre AS NombreCategoria From dbo.producto_subCategoria INNER Join dbo.producto_categoria ON dbo.producto_subCategoria.idCategoria = dbo.producto_categoria.idCategoria ")
                    query.AppendLine("Where dbo.producto_subCategoria.idSubCategoria = " & nIdSubCategoria & " Order By dbo.producto_subCategoria.Nombre")
                End If

                Dim cmd As SqlCommand = New SqlCommand(query.ToString(), oconexion)
                cmd.CommandType = CommandType.Text
                oconexion.Open()

                Using dr As SqlDataReader = cmd.ExecuteReader()

                    While dr.Read()
                        lista.Add(New subcategoria() With {
                            .idSubCategoria = Convert.ToInt32(dr("idCategoria")),
                            .oCategoria = New categoria() With {.idCategoria = dr("idCategoria"), .Nombre = dr("NombreCategoria")},
                            .Nombre = UCase(dr("Nombre").ToString()),
                            .Estado = Convert.ToBoolean(dr("Estado"))
                        })

                    End While

                End Using

            Catch ex As Exception
                lista = New List(Of subcategoria)()
            End Try
        End Using

        Return lista
    End Function
    Public Function ListarSubcategoria(ByVal idCategoria As Integer) As List(Of subcategoria)
        Dim lista As List(Of subcategoria) = New List(Of subcategoria)()
        Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)

            Try
                Dim query As StringBuilder = New StringBuilder()
                If idCategoria = 0 Then
                    query.AppendLine("Select dbo.producto_subCategoria.idSubCategoria, dbo.producto_subCategoria.idCategoria, dbo.producto_subCategoria.Nombre, dbo.producto_subCategoria.Estado, ")
                    query.AppendLine("dbo.producto_categoria.Nombre AS NombreCategoria From dbo.producto_subCategoria INNER Join dbo.producto_categoria ON dbo.producto_subCategoria.idCategoria = dbo.producto_categoria.idCategoria ")
                    query.AppendLine("Order By dbo.producto_subCategoria.Nombre")
                Else
                    query.AppendLine("Select dbo.producto_subCategoria.idSubCategoria, dbo.producto_subCategoria.idCategoria, dbo.producto_subCategoria.Nombre, dbo.producto_subCategoria.Estado, ")
                    query.AppendLine("dbo.producto_categoria.Nombre AS NombreCategoria From dbo.producto_subCategoria INNER Join dbo.producto_categoria ON dbo.producto_subCategoria.idCategoria = dbo.producto_categoria.idCategoria ")
                    query.AppendLine("Where dbo.producto_subCategoria.idCategoria = " & idCategoria & " Order By dbo.producto_subCategoria.Nombre")
                End If

                Dim cmd As SqlCommand = New SqlCommand(query.ToString(), oconexion)
                cmd.CommandType = CommandType.Text
                oconexion.Open()

                Using dr As SqlDataReader = cmd.ExecuteReader()

                    While dr.Read()
                        lista.Add(New subcategoria() With {
                            .idSubCategoria = Convert.ToInt32(dr("idCategoria")),
                            .oCategoria = New categoria() With {.idCategoria = dr("idCategoria"), .Nombre = dr("NombreCategoria")},
                            .Nombre = UCase(dr("Nombre").ToString()),
                            .Estado = Convert.ToBoolean(dr("Estado"))
                        })

                    End While

                End Using

            Catch ex As Exception
                lista = New List(Of subcategoria)()
            End Try
        End Using

        Return lista
    End Function

    Public Function Registrar(ByVal obj As subcategoria, <Out> ByRef Mensaje As String) As Integer
        Dim idGenerado As Integer = 0
        Mensaje = String.Empty

        Try

            Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                Dim cmd As SqlCommand = New SqlCommand("sp_subcategoria_add", oconexion)
                cmd.Parameters.AddWithValue("@idCategoria", obj.oCategoria.idCategoria)
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

    Public Function Editar(ByVal obj As subcategoria, <Out> ByRef Mensaje As String) As Boolean
        Dim Respuesta As Boolean = False
        Mensaje = String.Empty

        Try

            Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                Dim cmd As SqlCommand = New SqlCommand("sp_subcategoria_edit", oconexion)
                cmd.Parameters.AddWithValue("@IdSubcategoria", obj.idSubCategoria)
                cmd.Parameters.AddWithValue("@Idcategoria", obj.oCategoria.idCategoria)
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

    Public Function Eliminar(ByVal obj As subcategoria, <Out> ByRef Mensaje As String) As Boolean
        Dim Respuesta As Boolean = False
        Mensaje = String.Empty

        Try

            Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                Dim cmd As SqlCommand = New SqlCommand("sp_subcategoria_borrar", oconexion)
                cmd.Parameters.AddWithValue("@idSubcategoria", obj.idSubCategoria)
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
