Imports System.Data.SqlClient
Imports capaEntidad
Imports System.Text
Imports System.Runtime.InteropServices
Imports CapaDatos.Conexion
Public Class CD_Sucursal
    Public lista As List(Of sucursal) = New List(Of sucursal)
    Dim oconexion As New SqlConnection(conectar.Cadena)
    Public Function Listar(ByVal nidSucursal As Integer) As List(Of sucursal)
        Dim lista As List(Of sucursal) = New List(Of sucursal)()
        Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)

            Try
                Dim query As String
                If nidSucursal = 0 Then
                    query = "SELECT * FROM sucursales order by nombre where Estado=1"
                Else
                    query = "SELECT * FROM sucursales WHERE idSucursal =" & nidSucursal & " order by nombre"
                End If
                Dim cmd As SqlCommand = New SqlCommand(query, oconexion)
                cmd.CommandType = CommandType.Text
                oconexion.Open()

                Using dr As SqlDataReader = cmd.ExecuteReader()

                    While dr.Read()
                        lista.Add(New sucursal() With {
                            .idSucursal = Convert.ToInt32(dr("idSucursal")),
                            .Nombre = dr("Nombre").ToString(),
                            .Direccion = dr("Direccion").ToString(),
                            .Localidad = dr("Localidad"),
                            .Provincia = dr("Provincia"),
                            .CodigoPostal = dr("CodigoPostal"),
                            .Telefono = dr("Telefono"),
                            .Celular = dr("Celular"),
                            .Instagram = dr("Instagram"),
                            .Email = dr("Email"),
                            .Estado = dr("Estado"),
                            .EsCentral = dr("EsDepositoCentral"),
                            .PuedeComprar = dr("PuedeComprar")
                        })

                    End While

                End Using

            Catch ex As Exception
                lista = New List(Of sucursal)()
            End Try
        End Using

        Return lista
    End Function
    Public Function ListarLogin() As List(Of sucursal)
        Dim lista As List(Of sucursal) = New List(Of sucursal)()
        Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)

            Try
                Dim query As String

                query = "SELECT * FROM sucursales where Estado=1 order by nombre "

                Dim cmd As SqlCommand = New SqlCommand(query, oconexion)
                cmd.CommandType = CommandType.Text
                oconexion.Open()

                Using dr As SqlDataReader = cmd.ExecuteReader()

                    While dr.Read()
                        lista.Add(New sucursal() With {
                            .idSucursal = Convert.ToInt32(dr("idSucursal")),
                            .Nombre = dr("Nombre").ToString(),
                            .Direccion = dr("Direccion").ToString(),
                            .Localidad = dr("Localidad"),
                            .Provincia = dr("Provincia"),
                            .CodigoPostal = dr("CodigoPostal"),
                            .Telefono = dr("Telefono"),
                            .Celular = dr("Celular"),
                            .Instagram = dr("Instagram"),
                            .Email = dr("Email"),
                            .Estado = dr("Estado"),
                            .EsCentral = dr("EsDepositoCentral"),
                            .PuedeComprar = dr("PuedeComprar")
                        })

                    End While

                End Using

            Catch ex As Exception
                lista = New List(Of sucursal)()
            End Try
        End Using

        Return lista
    End Function
    Public Function Registrar(ByVal obj As sucursal, <Out> ByRef Mensaje As String) As Integer
        Dim idGenerado As Integer = 0
        Mensaje = String.Empty

        Try

            Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                Dim cmd As SqlCommand = New SqlCommand("sp_sucursal_add", oconexion)
                cmd.Parameters.AddWithValue("@Nombre", obj.Nombre)
                cmd.Parameters.AddWithValue("@Direccion", obj.Direccion)
                cmd.Parameters.AddWithValue("@Localidad", obj.Direccion)
                cmd.Parameters.AddWithValue("@Provincia", obj.Provincia)
                cmd.Parameters.AddWithValue("@CodigoPostal", obj.CodigoPostal)
                cmd.Parameters.AddWithValue("@Telefono", obj.Telefono)
                cmd.Parameters.AddWithValue("@Celular", obj.Celular)
                cmd.Parameters.AddWithValue("@Instagram", obj.Instagram)
                cmd.Parameters.AddWithValue("@PuedeComprar", obj.PuedeComprar)
                cmd.Parameters.AddWithValue("@EsDepositoCentral", obj.EsCentral)
                cmd.Parameters.AddWithValue("@Email", obj.Email)
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

    Public Function Editar(ByVal obj As sucursal, <Out> ByRef Mensaje As String) As Boolean
        Dim Respuesta As Boolean = False
        Mensaje = String.Empty

        Try

            Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                Dim cmd As SqlCommand = New SqlCommand("sp_sucursal_edit", oconexion)
                cmd.Parameters.AddWithValue("@Idsucursal", obj.idSucursal)
                cmd.Parameters.AddWithValue("@Nombre", obj.Nombre)
                cmd.Parameters.AddWithValue("@Direccion", obj.Direccion)
                cmd.Parameters.AddWithValue("@Localidad", obj.Localidad)
                cmd.Parameters.AddWithValue("@Provincia", obj.Provincia)
                cmd.Parameters.AddWithValue("@CodigoPostal", obj.CodigoPostal)
                cmd.Parameters.AddWithValue("@Telefono", obj.Telefono)
                cmd.Parameters.AddWithValue("@Celular", obj.Celular)
                cmd.Parameters.AddWithValue("@Instagram", obj.Instagram)
                cmd.Parameters.AddWithValue("@PuedeComprar", obj.PuedeComprar)
                cmd.Parameters.AddWithValue("@EsDepositoCentral", obj.EsCentral)
                cmd.Parameters.AddWithValue("@Email", obj.Email)
                cmd.Parameters.AddWithValue("@Estado", obj.Estado)
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

    Public Function Eliminar(ByVal obj As sucursal, <Out> ByRef Mensaje As String) As Boolean
        Dim Respuesta As Boolean = False
        Mensaje = String.Empty

        Try

            Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                Dim cmd As SqlCommand = New SqlCommand("sp_Sucursal_borrar", oconexion)
                cmd.Parameters.AddWithValue("@idSucursal", obj.idSucursal)
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
