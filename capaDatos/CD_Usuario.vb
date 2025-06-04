Imports System.Data.SqlClient
Imports capaEntidad
Imports System.Text
Imports System.Runtime.InteropServices
Imports CapaDatos.Conexion

Public Class CD_Usuario
    Public lista As List(Of usuario) = New List(Of usuario)
    Dim oconexion As New SqlConnection(conectar.Cadena)
    Public Function Listar(ByVal nIdUsuario As Integer) As List(Of usuario)
        Dim lista As List(Of usuario) = New List(Of usuario)()
        Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)

            Try
                Dim query As StringBuilder = New StringBuilder()
                If nIdUsuario = 0 Then
                    query.AppendLine("SELECT usuario.idUsuario, usuario.Nombre, usuario.Clave, usuario.Domicilio, usuario.CodigoPostal, usuario.Localidad, usuario.provincia, usuario.celular,usuario.email,")
                    query.AppendLine("r.idRol, r.Nombre As RolNombre, usuarios.intHabilitado FROM usuarios  inner join rol r On usuarios.idRol = r.Idrol")
                    query.AppendLine(" WHERE usuarios.Estado <>0 1 And usuarios.idrol <>1 order by usuarios.nombre ")
                Else
                    query.AppendLine("SELECT usuario.idUsuario, usuario.Nombre, usuario.Clave, usuario.Domicilio, usuario.CodigoPostal, usuario.Localidad, usuario.provincia, usuario.celular, usuario.email,")
                    query.AppendLine("usuario.celular, r.idRol, r.Nombre As RolNombre, usuario.estado, usuario.FotoNombre, usuario.FotoPath  FROM usuario  inner join rol r On usuario.idRol = r.Idrol")
                    query.AppendLine("WHERE usuario.idusuario =" & nIdUsuario & " order by usuario.nombre 
")
                End If

                Dim cmd As SqlCommand = New SqlCommand(query.ToString(), oconexion)
                cmd.CommandType = CommandType.Text
                oconexion.Open()

                Using dr As SqlDataReader = cmd.ExecuteReader()

                    While dr.Read()
                        lista.Add(New usuario() With {
                            .idusuario = Convert.ToInt32(dr("idusuario")),
                            .Nombre = dr("Nombre").ToString(),
                            .Domicilio = dr("Domicilio").ToString(),
                            .CodigoPostal = dr("CodigoPostal").ToString(),
                            .Localidad = dr("Localidad").ToString(),
                            .Provincia = dr("Provincia").ToString(),
                            .Celular = dr("Celular").ToString(),
                            .FotoNombre = dr("FotoNombre").ToString(),
                            .FotoPath = dr("FotoPath").ToString(),
                            .Clave = dr("Clave").ToString(),
                            .Estado = Convert.ToBoolean(dr("Estado")),
                            .Email = dr("Email"),
                            .oRol = New rol() With {.idRol = Convert.ToInt32(dr("idRol")), .Nombre = dr("RolNombre").ToString()}
                        })

                    End While

                End Using

            Catch ex As Exception
                lista = New List(Of usuario)()
            End Try
        End Using

        Return lista
    End Function

    Public Function Registrar(ByVal obj As usuario, <Out> ByRef Mensaje As String) As Integer
        Dim idGenerado As Integer = 0
        Mensaje = String.Empty

        Try

            Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                Dim cmd As SqlCommand = New SqlCommand("sp_usuario_add", oconexion)
                cmd.Parameters.AddWithValue("@Nombre", obj.Nombre)
                cmd.Parameters.AddWithValue("@Domicilio", obj.Domicilio)
                cmd.Parameters.AddWithValue("@CodigoPostal", obj.CodigoPostal)
                cmd.Parameters.AddWithValue("@Localidad", obj.Localidad)
                cmd.Parameters.AddWithValue("@Provincia", obj.Provincia)
                cmd.Parameters.AddWithValue("@Celular", obj.Celular)
                cmd.Parameters.AddWithValue("@idrol", obj.oRol.idRol)
                cmd.Parameters.AddWithValue("@Email", obj.Email)
                cmd.Parameters.AddWithValue("@Clave", "")
                cmd.Parameters.AddWithValue("@FotoPath", obj.FotoPath)
                cmd.Parameters.AddWithValue("@FotoNombre", obj.FotoNombre)
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

    Public Function Editar(ByVal obj As usuario, <Out> ByRef Mensaje As String) As Boolean
        Dim Respuesta As Boolean = False
        Mensaje = String.Empty

        Try

            Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                Dim cmd As SqlCommand = New SqlCommand("sp_usuario_edit", oconexion)
                cmd.Parameters.AddWithValue("@IdUsuario", obj.idusuario)
                cmd.Parameters.AddWithValue("@Nombre", obj.Nombre)
                cmd.Parameters.AddWithValue("@Domicilio", obj.Domicilio)
                cmd.Parameters.AddWithValue("@CodigoPostal", obj.CodigoPostal)
                cmd.Parameters.AddWithValue("@Localidad", obj.Localidad)
                cmd.Parameters.AddWithValue("@Provincia", obj.Provincia)
                cmd.Parameters.AddWithValue("@Celular", obj.Celular)
                cmd.Parameters.AddWithValue("@idrol", obj.oRol.idRol)
                cmd.Parameters.AddWithValue("@Email", obj.Email)
                cmd.Parameters.AddWithValue("@Clave", "")
                cmd.Parameters.AddWithValue("@FotoPath", obj.FotoPath)
                cmd.Parameters.AddWithValue("@FotoNombre", obj.FotoNombre)
                cmd.Parameters.AddWithValue("@Estado", True)
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

    Public Function Eliminar(ByVal obj As usuario, <Out> ByRef Mensaje As String) As Boolean
        Dim Respuesta As Boolean = False
        Mensaje = String.Empty

        Try

            Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                Dim cmd As SqlCommand = New SqlCommand("sp_usuarios_borrar", oconexion)
                cmd.Parameters.AddWithValue("p_idUsuario", obj.idusuario)
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

    Public Function Login(ByVal sUsuario As String) As List(Of usuario)
        Dim lista As List(Of usuario) = New List(Of usuario)()
        Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)

            Try
                Dim query As StringBuilder = New StringBuilder()
                query.AppendLine("Select u.idusuario, u.nombre, u.clave, u.idRol, u.estado, r.Idrol, r.nombre as RolNombre  FROM usuario u")
                query.AppendLine("inner join rol r on u.idRol = r.Idrol WHERE u.Email = '" & sUsuario & "' and u.IdUsuario <> 5 order by u.nombre")
                Dim cmd As SqlCommand = New SqlCommand(query.ToString(), oconexion)
                cmd.CommandType = CommandType.Text
                oconexion.Open()

                Using dr As SqlDataReader = cmd.ExecuteReader()

                    While dr.Read()
                        lista.Add(New usuario() With {
                            .idusuario = Convert.ToInt32(dr("idusuario")),
                            .Nombre = dr("Nombre").ToString(),
                            .Clave = dr("Clave").ToString(),
                            .Estado = Convert.ToBoolean(dr("Estado")),
                            .oRol = New rol() With {.idRol = Convert.ToInt32(dr("idRol")), .Nombre = dr("RolNombre").ToString()}
                        })

                    End While

                End Using

            Catch ex As Exception
                lista = New List(Of usuario)()
            End Try
        End Using

        Return lista
    End Function
End Class
