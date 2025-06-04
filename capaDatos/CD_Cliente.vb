
Imports CapaDatos
Imports System.Data
Imports capaEntidad
Imports System.Text
Imports System.Runtime.InteropServices
Imports System.Data.SqlClient
Imports CapaDatos.Conexion

Public Class CD_Cliente
    Public lista As List(Of cliente) = New List(Of cliente)
    Dim oconexion As New SqlConnection(conectar.Cadena)
    Public Function Listar(ByVal ncliente As Integer) As List(Of cliente)
        Dim lista As List(Of cliente) = New List(Of cliente)()
        Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
            If ncliente = 1 Then
                lista = New List(Of cliente)()
                Exit Function
            End If
            Try
                Dim query As StringBuilder = New StringBuilder()
                query.AppendLine("Select idCliente, cliente.apellido, cliente.Nombre, COALESCE(cliente.apellido, '') + ' ' + COALESCE(cliente.nombre, '') as NombreCompleto, cliente.Domicilio,")
                query.AppendLine("cliente.CodigoPostal, cliente.Localidad, cliente.Provincia, cliente.NumeroDocumento,")
                query.AppendLine("cliente.idTipoDocumento, cliente.idTipoResponsable, cliente.NumeroDocumento, cliente.TelefonoMovil, cliente.TelefonoPais, cliente.TelefonoArea, cliente.TelefonoNumero, ")
                query.AppendLine(" COALESCE(cliente.TelefonoPais, '') + ' ' + COALESCE(cliente.TelefonoArea, '') + ''+ COALESCE(cliente.TelefonoNumero, '')  as TelefonoNumeroCompleto, cliente.correo, cliente.estado, Saldo, FechaAlta, ")
                query.AppendLine("tiposderesponsables.Nombre As Responsable, tiposdocumentos.Nombre as DocNombre FROM cliente")
                query.AppendLine("INNER Join tiposderesponsables ON cliente.idTipoResponsable = tiposderesponsables.idTipoResponsable")
                query.AppendLine("INNER Join tiposdocumentos ON cliente.idTipoDocumento = tiposdocumentos.idTipoDocumento ")
                If ncliente = 0 Then
                    'query.AppendLine("WHERE proveedor.idProveedor <>1 ")
                Else
                    query.AppendLine("WHERE cliente.idcliente =" & ncliente)
                End If
                query.AppendLine("ORDER BY NombreCompleto  Asc")
                Dim cmd As SqlCommand = New SqlCommand(query.ToString(), oconexion)
                cmd.CommandType = CommandType.Text
                oconexion.Open()

                Using dr As SqlDataReader = cmd.ExecuteReader()

                    While dr.Read()
                        lista.Add(New cliente() With {
                            .idcliente = Convert.ToInt32(dr("idCliente")),
                            .apellido = dr("apellido").ToString(),
                            .nombre = dr("Nombre").ToString(),
                            .NombreCompleto = dr("NombreCompleto").ToString(),
                            .Direccion = dr("Domicilio").ToString(),
                            .codigopostal = dr("CodigoPostal").ToString(),
                            .localidad = dr("Localidad").ToString(),
                            .provincia = dr("Provincia").ToString(),
                            .Estado = Convert.ToBoolean(dr("Estado")),
                            .oTipoResponsable = New tiposresponsables() With {.idTipoResponsable = Convert.ToInt32(dr("idTipoResponsable")), .Nombre = dr("Responsable").ToString()},
                            .oTipoDocumento = New TipoDocumento() With {.idTipoDocumento = Convert.ToInt32(dr("idTipoDocumento")), .Nombre = dr("DocNombre").ToString()},
                            .TelefonoMovil = Convert.ToBoolean(dr("Telefonomovil")),
                            .TelefonoPais = dr("Telefonopais").ToString(),
                            .TelefonoArea = dr("Telefonoarea").ToString(),
                            .TelefonoNumero = dr("Telefononumero").ToString(),
                            .TelefonoNumerocompleto = dr("TelefonoNumerocompleto").ToString(),
                            .correo = dr("correo").ToString(),
                            .NumeroDocumento = dr("NumeroDocumento").ToString(),
                            .Saldo = dr("Saldo").ToString(),
                            .fechaAlta = IIf(IsDBNull(dr("FechaAlta").ToString()), Date.Today, dr("FechaAlta").ToString())
                        })


                    End While
                End Using
                If lista.Count = 0 Then
                    lista = New List(Of cliente)()
                End If
            Catch ex As Exception
                lista = New List(Of cliente)()
            End Try
        End Using

        Return lista
    End Function

    Public Function Registrar(ByVal obj As cliente, <Out> ByRef Mensaje As String) As Integer
        Dim idGenerado As Integer = 0
        Mensaje = String.Empty

        Try
            '@Apellido varchar(100),
            '@Nombre varchar(100),
            '@Direccion varchar(100),
            '@CodigoPostal varchar(20),
            '@Localidad varchar(100),
            '@Pais varchar(100),
            '@Provincia varchar(100),
            '@TelefonoMovil varchar(20),
            '@TelefonoPais varchar(20),
            '@TelefonoArea varchar(20),
            '@TelefonoNumero varchar(20),
            '@idTipoDocumento int,
            '@NumeroDocumento varchar(20),
            '@idTipoResponsable int,

            '
            '@Correo varchar(100),

            '@Estado bit,
            Using oconexion As SqlConnection = New SqlConnection(Conexion.conectar.Cadena)
                Dim cmd As SqlCommand = New SqlCommand("sp_cliente_add", oconexion)
                cmd.Parameters.AddWithValue("@apellido", obj.apellido)
                cmd.Parameters.AddWithValue("@Nombre", obj.nombre)
                cmd.Parameters.AddWithValue("@Direccion", obj.Direccion)
                cmd.Parameters.AddWithValue("@CodigoPostal", obj.codigopostal)
                cmd.Parameters.AddWithValue("@Localidad", obj.localidad)
                cmd.Parameters.AddWithValue("@pais", obj.pais)
                cmd.Parameters.AddWithValue("@Provincia", obj.provincia)
                cmd.Parameters.AddWithValue("@TelefonoMovil", obj.TelefonoMovil)
                cmd.Parameters.AddWithValue("@TelefonoPais", obj.TelefonoPais)
                cmd.Parameters.AddWithValue("@Telefonoarea", obj.TelefonoArea)
                cmd.Parameters.AddWithValue("@TelefonoNumero", obj.TelefonoNumero)
                cmd.Parameters.AddWithValue("@Correo", obj.correo)
                cmd.Parameters.AddWithValue("@idTipoDocumento", obj.oTipoDocumento.idTipoDocumento)
                cmd.Parameters.AddWithValue("@NumeroDocumento", obj.NumeroDocumento)
                cmd.Parameters.AddWithValue("@idTipoResponsable", obj.oTipoResponsable.idTipoResponsable)
                cmd.Parameters.AddWithValue("@Saldo", obj.Saldo)
                cmd.Parameters.AddWithValue("@fechaAlta", Format(obj.fechaAlta, "yyyy-MM-dd"))
                cmd.Parameters.AddWithValue("@Estado", obj.Estado)
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

    Public Function Editar(ByVal obj As cliente, <Out> ByRef Mensaje As String) As Boolean
        Dim Respuesta As Boolean = False
        Mensaje = String.Empty
        '@IdCliente int,
        '@Apellido varchar(100),
        '@Nombre varchar(100),
        '@Direccion varchar(100),
        '@CodigoPostal varchar(20),
        '@Pais varchar(100),
        '@Localidad varchar(100),
        '@Provincia varchar(100),
        '@TelefonoMovil varchar(20),
        '@TelefonoPais varchar(20),
        '@TelefonoArea varchar(20),
        '@TelefonoNumero varchar(20),
        '@Correo varchar(100),
        '@idTipoResponsable int,
        '@idTipoDocumento int,
        '@NumeroDocumento varchar(20),

        '@Saldo decimal,
        '@Estado bit,

        Try

            Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                Dim cmd As SqlCommand = New SqlCommand("sp_cliente_edit", oconexion)
                cmd.Parameters.AddWithValue("@IdCliente", obj.idcliente)
                cmd.Parameters.AddWithValue("@apellido", obj.apellido)
                cmd.Parameters.AddWithValue("@Nombre", obj.nombre)
                cmd.Parameters.AddWithValue("@Direccion", obj.Direccion)
                cmd.Parameters.AddWithValue("@CodigoPostal", obj.codigopostal)
                cmd.Parameters.AddWithValue("@Pais", obj.pais)
                cmd.Parameters.AddWithValue("@Localidad", obj.localidad)
                cmd.Parameters.AddWithValue("@Provincia", obj.provincia)
                cmd.Parameters.AddWithValue("@TelefonoMovil", obj.TelefonoMovil)
                cmd.Parameters.AddWithValue("@TelefonoPais", obj.TelefonoPais)
                cmd.Parameters.AddWithValue("@Telefonoarea", obj.TelefonoArea)
                cmd.Parameters.AddWithValue("@Telefononumero", obj.TelefonoNumero)
                cmd.Parameters.AddWithValue("@Correo", obj.correo)
                cmd.Parameters.AddWithValue("@idTipoDocumento", obj.oTipoDocumento.idTipoDocumento)
                cmd.Parameters.AddWithValue("@NumeroDocumento", obj.NumeroDocumento)
                cmd.Parameters.AddWithValue("@idTipoResponsable", obj.oTipoResponsable.idTipoResponsable)
                cmd.Parameters.AddWithValue("@Saldo", obj.Saldo)
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

    Public Function Eliminar(ByVal obj As cliente, <Out> ByRef Mensaje As String) As Boolean
        Dim Respuesta As Boolean = False
        Mensaje = String.Empty

        Try
            If obj.idcliente = 1 Then
                Respuesta = False
                Mensaje = "No se puede eliminar este cliente"
            Else
                Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                    Dim cmd As SqlCommand = New SqlCommand("sp_cliente_borrar", oconexion)
                    cmd.Parameters.AddWithValue("p_idProveedor", obj.idcliente)
                    cmd.Parameters.Add("o_Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output
                    cmd.Parameters.Add("o_Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output
                    cmd.CommandType = CommandType.StoredProcedure
                    oconexion.Open()
                    cmd.ExecuteNonQuery()
                    Respuesta = Convert.ToBoolean(cmd.Parameters("o_Respuesta").Value)
                    Mensaje = cmd.Parameters("o_Mensaje").Value.ToString()
                End Using
            End If


        Catch ex As Exception
            Respuesta = False
            Mensaje = ex.Message
        End Try

        Return Respuesta

    End Function
End Class
