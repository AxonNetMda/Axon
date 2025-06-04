Imports System.Data.SqlClient
Imports capaEntidad
Imports System.Text
Imports System.Runtime.InteropServices
Imports CapaDatos.Conexion

Public Class CD_Reposicion
    Public lista As List(Of reposicion) = New List(Of reposicion)
    Dim oconexion As New SqlConnection(conectar.Cadena)
    Public Function Listar(ByVal nidReposicion As Integer, ByVal nidSucursal As Integer) As List(Of reposicion)
        Dim lista As List(Of reposicion) = New List(Of reposicion)()
        Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
            Dim filtro As String
            nidSucursal = 1
            Try
                Dim query As String
                If nidReposicion = 0 Then
                    filtro = " Where idDeSucursal=" & nidSucursal
                Else
                    filtro = " Where idDeSucursal=" & nidSucursal & " AND idReposicion=" & nidReposicion
                End If
                query = "SELECT dbo.reposiciones.idReposicion, dbo.reposiciones.idDeSucursal, se.Nombre AS SucEmisora, dbo.reposiciones.idASucursal,  dbo.reposiciones.PuntoVenta,
                        sr.Nombre AS SucReceptora, dbo.reposiciones.fecha,dbo.reposiciones.idUsuario, dbo.usuario.Nombre, dbo.reposiciones.Referencia,  dbo.reposiciones.NumeroComprobante, 
                        dbo.reposiciones.Notas,  dbo.reposiciones.Estado 
                        FROM dbo.reposiciones
                        INNER JOIN dbo.Sucursales se ON dbo.reposiciones.idDeSucursal = se.idSucursal
                        INNER JOIN dbo.usuario ON dbo.reposiciones.idUsuario = dbo.usuario.idUsuario
                        INNER JOIN dbo.Sucursales sr ON dbo.reposiciones.idASucursal = sr.idSucursal " & filtro & " order by Fecha"
                Dim cmd As SqlCommand = New SqlCommand(query.ToString(), oconexion)
                cmd.CommandType = CommandType.Text
                oconexion.Open()

                Using dr As SqlDataReader = cmd.ExecuteReader()
                    '.oProducto = New producto() With {.idProducto = dr("idproducto")},
                    '        .cantidad = dr("Cantidad"),
                    '        .oUsuario = New usuario() With {.idusuario = dr("IdUsuario")}, .Notas = dr("Notas"),
                    While dr.Read()
                        lista.Add(New reposicion() With {
                            .idReposicion = Convert.ToInt32(dr("idReposicion")),
                            .oDeSucursal = New sucursal() With {.idSucursal = dr("idDeSucursal"), .Nombre = dr("sucEmisora")},
                            .oASucursal = New sucursal() With {.idSucursal = dr("idASucursal"), .Nombre = dr("sucReceptora")},
                            .fecha = FormatDateTime(dr("Fecha"), vbShortDate),
                            .PuntoVenta = dr("puntoventa"),
                            .NumeroComprobante = dr("NumeroComprobante"),
                            .oUsuario = New usuario() With {.idusuario = dr("idusuario")},
                            .Notas = dr("Notas"),
                            .Referencia = dr("Referencia"),
                            .Estado = dr("Estado")
                        })

                    End While

                End Using

            Catch ex As Exception
                lista = New List(Of reposicion)()
            End Try
        End Using

        Return lista
    End Function
    Public Function ListarDetalle(ByVal nidReposicion As Integer) As List(Of reposiciondetalles)
        Dim lista As List(Of reposiciondetalles) = New List(Of reposiciondetalles)()
        Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)

            Try
                Dim query As String

                query = "SELECT dbo.reposicionesdetalles.idRepoDetalle, dbo.reposicionesdetalles.idReposicion, dbo.reposicionesdetalles.idProducto, 
                         dbo.reposicionesdetalles.Cantidad, dbo.producto.Nombre, dbo.preciocostoconiva(dbo.producto.PrecioCosto, dbo.producto.AlicuotaIVA) as precio
                        FROM dbo.reposicionesdetalles
                        INNER JOIN dbo.producto ON dbo.reposicionesdetalles.idProducto = dbo.producto.idProducto
                        WHERE dbo.reposicionesdetalles.idReposicion = " & nidReposicion & " ORDER BY dbo.reposicionesdetalles.idRepoDetalle ASC"
                Dim cmd As SqlCommand = New SqlCommand(query.ToString(), oconexion)
                cmd.CommandType = CommandType.Text
                oconexion.Open()

                Using dr As SqlDataReader = cmd.ExecuteReader()

                    While dr.Read()
                        lista.Add(New reposiciondetalles() With {
                            .idRepoDetalle = Convert.ToInt32(dr("idRepoDetalle")),
                            .idReposicion = Convert.ToInt32(dr("idReposicion")),
                            .oProducto = New producto() With {.idProducto = dr("idproducto")},
                            .Cantidad = dr("Cantidad")
                        })

                    End While

                End Using

            Catch ex As Exception
                lista = New List(Of reposiciondetalles)()
            End Try
        End Using

        Return lista
    End Function
    Public Function Registrar(ByVal obj As reposicion, <Out> ByRef Mensaje As String) As Integer
        Dim idGenerado As Integer = 0
        Mensaje = String.Empty
        Dim nComprobante As Integer = 0
        Try
            Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                Dim cmd As SqlCommand = New SqlCommand("sp_reposicion_add", oconexion)
                cmd.Parameters.AddWithValue("@idReposicion", 0)
                cmd.Parameters.AddWithValue("@idDeSucursal", obj.oDeSucursal.idSucursal)
                cmd.Parameters.AddWithValue("@idASucursal", obj.oASucursal.idSucursal)
                cmd.Parameters.AddWithValue("@Fecha", obj.fecha)
                cmd.Parameters.AddWithValue("@idProducto", obj.oProducto.idProducto)
                cmd.Parameters.AddWithValue("@Cantidad", obj.cantidad)
                cmd.Parameters.AddWithValue("@IdUsuario", obj.oUsuario.idusuario)
                cmd.Parameters.AddWithValue("@Notas", obj.Notas)
                cmd.Parameters.AddWithValue("@Referencia", obj.Referencia)
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

    Public Function Editar(ByVal obj As reposicion, <Out> ByRef Mensaje As String) As Boolean
        Dim Respuesta As Boolean = False
        Mensaje = String.Empty
        Dim importe As Double = 0
        Try

            Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                Dim cmd As SqlCommand = New SqlCommand("sp_reposicion_edit", oconexion)
                cmd.Parameters.AddWithValue("@idReposicion", obj.idReposicion)
                cmd.Parameters.AddWithValue("@idDeSucursal", obj.oDeSucursal.idSucursal)
                cmd.Parameters.AddWithValue("@idASucursal", obj.oASucursal.idSucursal)
                cmd.Parameters.AddWithValue("@Fecha", obj.fecha)
                cmd.Parameters.AddWithValue("@idProducto", obj.oProducto.idProducto)
                cmd.Parameters.AddWithValue("@Cantidad", obj.cantidad)
                cmd.Parameters.AddWithValue("@IdUsuario", obj.oUsuario.idusuario)
                cmd.Parameters.AddWithValue("@Notas", obj.Notas)
                cmd.Parameters.AddWithValue("@Referencia", obj.Referencia)
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

    Public Function Eliminar(ByVal obj As reposicion, <Out> ByRef Mensaje As String) As Boolean
        Dim Respuesta As Boolean = False
        Mensaje = String.Empty

        Try

            Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                Dim cmd As SqlCommand = New SqlCommand("sp_reposicion_borrar", oconexion)
                cmd.Parameters.AddWithValue("@iReposicion", obj.idReposicion)
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
    Public Function AnularRepo(ByVal obj As reposicion, ByVal objDetalle As DataTable, <Out> ByRef Mensaje As String) As Boolean
        Dim Respuesta As Boolean = False
        Mensaje = String.Empty
        Dim importe As Double = 0
        Try
            '@idReposicion int,
            '@idDeSucursal int,
            '@idASucursal int,
            '@Letra varchar(10),
            '@Referencia varchar(50),
            '@Estado bit,
            '@Detalle_Reposicion [EDetalleReposicion] readonly,
            '@Mensaje varchar(500) output,
            '@Resultado int output
            Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                Dim cmd As SqlCommand = New SqlCommand("sp_reposicion_Anular", oconexion)
                cmd.Parameters.AddWithValue("@idReposicion", obj.idReposicion)
                cmd.Parameters.AddWithValue("@idDeSucursal", obj.oDeSucursal.idSucursal)
                cmd.Parameters.AddWithValue("@idASucursal", obj.oASucursal.idSucursal)
                cmd.Parameters.AddWithValue("@Letra", "REP")
                cmd.Parameters.AddWithValue("@Estado", 0)
                cmd.Parameters.AddWithValue("@Detalle_Reposicion", objDetalle)
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
End Class
