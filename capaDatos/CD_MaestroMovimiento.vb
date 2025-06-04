Imports System.Data.SqlClient
Imports capaEntidad
Imports System.Text
Imports System.Runtime.InteropServices
Imports CapaDatos.Conexion

Public Class CD_MaestroMovimiento
    Public lista As List(Of maestromovimiento) = New List(Of maestromovimiento)
    Dim oconexion As New SqlConnection(conectar.Cadena)
    Public Function Listar(ByVal nidMovimiento As Integer, ByVal nidSucursal As Integer) As List(Of maestromovimiento)
        Dim lista As List(Of maestromovimiento) = New List(Of maestromovimiento)()
        Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
            Dim filtro As String

            Try
                Dim query As String
                If nidMovimiento = 0 Then
                    filtro = " Where idDeSucursal=" & nidSucursal
                Else
                    filtro = " Where idDeSucursal=" & nidSucursal & " AND idMaestroCabecera=" & nidMovimiento
                End If
                query = "SELECT dbo.Maestro_Movimientos_Cab.idMaestroCabecera, dbo.Maestro_Movimientos_Cab.idTipoVenta, dbo.tipoventa.Nombre AS NombreTipoVenta,
                        dbo.Maestro_Movimientos_Cab.idTipoMovimiento, dbo.tipomovimiento.Nombre AS NombreTipoMovimiento, dbo.tipomovimiento.Letra AS LetraTipoMovimiento,
                        dbo.Maestro_Movimientos_Cab.idSucursalEmisor, se.sucEmisora AS SucursalEmisor, dbo.Maestro_Movimientos_Cab.idSucursalReceptor,
                        sr.Nombre AS sucReceptora, dbo.Maestro_Movimientos_Cab.idTipoComprobante, dbo.tiposdocumentos.Nombre AS NombreTipoDocumento,
                        dbo.Maestro_Movimientos_Cab.Letra, dbo.Maestro_Movimientos_Cab.PuntoVenta, dbo.Maestro_Movimientos_Cab.NumeroComprobante,
                        dbo.Maestro_Movimientos_Cab.Comprobante, dbo.Maestro_Movimientos_Cab.FechaEmision,  dbo.Maestro_Movimientos_Cab.idCliente,
                        dbo.cliente.nombre, dbo.cliente.apellido, dbo.Maestro_Movimientos_Cab.idProveedor, dbo.proveedor.RazonSocial AS NombreProveedor,
                        dbo.Maestro_Movimientos_Cab.NetoGravado, dbo.Maestro_Movimientos_Cab.AlicuotaIVA0, dbo.Maestro_Movimientos_Cab.AlicuotaIva25,
                        dbo.Maestro_Movimientos_Cab.AlicuotaIVA5, dbo.Maestro_Movimientos_Cab.AlicuotaIva105, dbo.Maestro_Movimientos_Cab.AlicuotaIva21,
                        dbo.Maestro_Movimientos_Cab.AlicuotaIva27, dbo.Maestro_Movimientos_Cab.ImpuestosInternos, dbo.Maestro_Movimientos_Cab.PercepcionIngresosBrutos,
                        dbo.Maestro_Movimientos_Cab.PercepcionIVA, dbo.Maestro_Movimientos_Cab.NoGravado, dbo.Maestro_Movimientos_Cab.ImporteTotal,
                        dbo.Maestro_Movimientos_Cab.DescuentoPorc, dbo.Maestro_Movimientos_Cab.Referencia, dbo.Maestro_Movimientos_Cab.IdUsuario,
                        dbo.usuario.Nombre AS NombreUsuario, dbo.Maestro_Movimientos_Cab.Estado, dbo.Maestro_Movimientos_Cab.idFormaPago, 
                        dbo.tipodecomprobante.idComprobante, dbo.tipodecomprobante.Nombre as NombreComprobante 
                        FROM dbo.Maestro_Movimientos_Cab
                        INNER JOIN dbo.tipoventa ON dbo.Maestro_Movimientos_Cab.idTipoVenta = dbo.tipoventa.idTipoVenta
                        INNER JOIN dbo.tipomovimiento ON dbo.Maestro_Movimientos_Cab.idTipoMovimiento = dbo.tipomovimiento.idTipoMovimiento
                        INNER JOIN dbo.Sucursales se ON dbo.Maestro_Movimientos_Cab.idSucursalEmisor = se.idSucursal
                        INNER JOIN dbo.Sucursales sr ON dbo.Maestro_Movimientos_Cab.idSucursalReceptor = sr.idSucursal
                        INNER JOIN dbo.tiposdocumentos ON dbo.Maestro_Movimientos_Cab.idTipoComprobante = dbo.tiposdocumentos.idTipoDocumento
                        INNER JOIN dbo.tipodecomprobante ON dbo.Maestro_Movimientos_Cab.idTipoComprobante = dbo.tipodecomprobante.idComprobante
                        INNER JOIN dbo.cliente ON dbo.Maestro_Movimientos_Cab.idCliente = dbo.cliente.idcliente
                        INNER JOIN dbo.proveedor ON dbo.Maestro_Movimientos_Cab.idProveedor = dbo.proveedor.idProveedor
                        INNER JOIN dbo.usuario ON dbo.Maestro_Movimientos_Cab.IdUsuario = dbo.usuario.idUsuario
                        WHERE dbo.Maestro_Movimientos_Cab.idSucursalEmisor = 1 " & filtro & " order by FechaEmision"
                Dim cmd As SqlCommand = New SqlCommand(query.ToString(), oconexion)
                cmd.CommandType = CommandType.Text
                oconexion.Open()

                Using dr As SqlDataReader = cmd.ExecuteReader()

                    While dr.Read()
                        lista.Add(New maestromovimiento() With {
                            .idMovimiento = Convert.ToInt32(dr("idMovimiento")),
                            .oTipoVenta = New tipoventa() With {.idTipoVenta = dr("idTipoventa"), .Nombre = dr("NombreTipoVenta")},
                            .oTipoMovimiento = New tipomovimiento() With {.idTipoMovimiento = dr("idTipoMovimiento"), .Nombre = dr("NombreTipoMovimiento")},
                            .letra = dr("Letra"),
                            .oDeSucursal = New sucursal() With {.idSucursal = dr("idDeSucursal"), .Nombre = dr("sucEmisora")},
                            .oASucursal = New sucursal() With {.idSucursal = dr("idASucursal"), .Nombre = dr("sucReceptora")},
                            .PuntoVenta = dr("PuntoVenta"),
                            .Numerocomprobante = dr("NumeroComprobante"),
                            .Comprobante = dr("comprobante"),
                            .Fecha = FormatDateTime(dr("FechaEmision"), vbShortDate),
                            .oCliente = New cliente() With {.idcliente = dr("idCliente"), .nombre = dr("Apellido") & " " & dr("Nombre")},
                            .oProveedor = New proveedor() With {.idProveedor = dr("idProveedor"), .RazonSocial = dr("NombreProveedor")},
                            .NetoGravado = dr("NetoGravado"),
                            .AlicuotaIVA0 = dr("AlicuotaIVA0"),
                            .AlicuotaIVA25 = dr("AlicuotaIVA25"),
                            .AlicuotaIVA5 = dr("AlicuotaIVA5"),
                            .AlicuotaIVA105 = dr("AlicuotaIVA105"),
                            .AlicuotaIVA21 = dr("AlicuotaIVA21"),
                            .AlicuotaIVA27 = dr("AlicuotaIVA27"),
                            .ImpuestosInternos = dr("ImpuestosInternos"),
                            .PercepcionIngresosBrutos = dr("PercepcionIngresosBrutos"),
                            .PercepcionIVA = dr("PercepcionIVA"),
                            .NoGravado = dr("NoGravado"),
                            .ImporteTotal = dr("ImpuestosInternos"),
                            .DescuentoPorc = dr("DescuentoPorc"),
                            .oUsuario = New usuario() With {.idusuario = dr("IdUsuario")},
                            .Notas = dr("Notas"),
                            .idFormaPago = dr("idFormaPago"),
                            .Estado = dr("Estado"),
                            .oTipoComprobante = New tipocomprobantes() With {.idTipoComprobante = dr("idTipoComprobante"), .strNombre = dr("NombreComprobante")},
                            .Referencia = dr("Referencia")
                        })

                    End While

                End Using

            Catch ex As Exception
                lista = New List(Of maestromovimiento)()
            End Try
        End Using

        Return lista
    End Function

    Public Function Registrar(ByVal obj As maestromovimiento, ByVal objDetalle As DataTable, <Out> ByRef Mensaje As String) As Integer
        Dim idGenerado As Integer = 0
        Mensaje = String.Empty
        Dim nComprobante As Integer = 0
        Try

            Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                Dim cmd As SqlCommand = New SqlCommand("sp_MaestroReposicion_Add", oconexion)
                cmd.Parameters.AddWithValue("@idReposicion", obj.idMovimiento)
                cmd.Parameters.AddWithValue("@idDeSucursal", obj.oDeSucursal.idSucursal)
                cmd.Parameters.AddWithValue("@idASucursal", obj.oASucursal.idSucursal)
                cmd.Parameters.AddWithValue("@PuntoVenta", obj.PuntoVenta)
                cmd.Parameters.AddWithValue("@idTipoMovimiento", obj.oTipoMovimiento.idTipoMovimiento)
                cmd.Parameters.AddWithValue("@idTipocomprobante", 0)
                cmd.Parameters.AddWithValue("@Numerocomprobante", obj.Numerocomprobante)
                cmd.Parameters.AddWithValue("@Letra", "REP")
                cmd.Parameters.AddWithValue("@Comprobante", obj.Comprobante)
                cmd.Parameters.AddWithValue("@Fecha", obj.Fecha)
                cmd.Parameters.AddWithValue("@idCliente", 0)
                cmd.Parameters.AddWithValue("@idProveedor", 0)
                cmd.Parameters.AddWithValue("@IdUsuario", obj.oUsuario.idusuario)
                cmd.Parameters.AddWithValue("@Referencia", obj.Referencia)
                cmd.Parameters.AddWithValue("@Notas", obj.Notas)
                cmd.Parameters.AddWithValue("@Estado", True)
                cmd.Parameters.AddWithValue("@DetalleRepo", objDetalle)
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
    Public Function RegistrarRepo(ByVal obj As maestromovimiento, ByVal objDetalle As DataTable, ByVal objDInventario As DataTable, <Out> ByRef Mensaje As String) As Integer
        Dim idGenerado As Integer = 0
        Mensaje = String.Empty
        Dim nComprobante As Integer = 0
        Try

            Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                Dim cmd As SqlCommand = New SqlCommand("sp_MaestroReposicion_Add", oconexion)
                cmd.Parameters.AddWithValue("@idReposicion", obj.idMovimiento)
                cmd.Parameters.AddWithValue("@idDeSucursal", obj.oDeSucursal.idSucursal)
                cmd.Parameters.AddWithValue("@idASucursal", obj.oASucursal.idSucursal)
                cmd.Parameters.AddWithValue("@PuntoVenta", obj.PuntoVenta)
                cmd.Parameters.AddWithValue("@idTipoMovimiento", obj.oTipoMovimiento.idTipoMovimiento)
                cmd.Parameters.AddWithValue("@idTipocomprobante", 0)
                cmd.Parameters.AddWithValue("@Numerocomprobante", obj.Numerocomprobante)
                cmd.Parameters.AddWithValue("@Letra", "REP")
                cmd.Parameters.AddWithValue("@Comprobante", obj.Comprobante)
                cmd.Parameters.AddWithValue("@Fecha", obj.Fecha)
                cmd.Parameters.AddWithValue("@idCliente", 0)
                cmd.Parameters.AddWithValue("@idProveedor", 0)
                cmd.Parameters.AddWithValue("@IdUsuario", obj.oUsuario.idusuario)
                cmd.Parameters.AddWithValue("@Referencia", obj.Referencia)
                cmd.Parameters.AddWithValue("@Notas", obj.Notas)
                cmd.Parameters.AddWithValue("@Estado", True)
                cmd.Parameters.AddWithValue("@DetalleRepo", objDetalle)
                cmd.Parameters.AddWithValue("@DetalleInventario", objDInventario)
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
    Public Function AnularRepo(ByVal obj As maestromovimiento, ByVal objDetalle As DataTable, <Out> ByRef Mensaje As String) As Integer
        Dim idGenerado As Integer = 0
        Mensaje = String.Empty
        Dim nComprobante As Integer = 0
        Try
            Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                Dim cmd As SqlCommand = New SqlCommand("sp_MaestroReposicion_anular", oconexion)
                cmd.Parameters.AddWithValue("@idReposicion", obj.idMovimiento)
                cmd.Parameters.AddWithValue("@idDeSucursal", obj.oDeSucursal.idSucursal)
                cmd.Parameters.AddWithValue("@idASucursal", obj.oASucursal.idSucursal)
                cmd.Parameters.AddWithValue("@PuntoVenta", obj.PuntoVenta)
                cmd.Parameters.AddWithValue("@idTipoMovimiento", obj.oTipoMovimiento.idTipoMovimiento)
                cmd.Parameters.AddWithValue("@Letra", "REP")
                cmd.Parameters.AddWithValue("@idTipocomprobante", 0)
                cmd.Parameters.AddWithValue("@Numerocomprobante", obj.Numerocomprobante)
                cmd.Parameters.AddWithValue("@Comprobante", obj.Comprobante)
                cmd.Parameters.AddWithValue("@Fecha", obj.Fecha)
                cmd.Parameters.AddWithValue("@idCliente", 0)
                cmd.Parameters.AddWithValue("@idProveedor", 0)
                cmd.Parameters.AddWithValue("@Detalle_Maestro", objDetalle)
                cmd.Parameters.AddWithValue("@IdUsuario", obj.oUsuario.idusuario)
                cmd.Parameters.AddWithValue("@Notas", obj.Notas)
                cmd.Parameters.AddWithValue("@Estado", True)
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
    Public Function ActualiarStockRepo(ByVal tblStock As DataTable, <Out> ByRef Mensaje As String) As Integer
        Dim idGenerado As Integer = 0
        Dim dt As New DataTable
        Mensaje = String.Empty
        Dim nComprobante As Integer = 0
        Try
            '@idSucursal int,
            '@idProducto int,
            '@Cantidad int,
            '@Precio decimal,
            '@FechaActualizacion date,
            'ActualizarStock.Columns.Add("idSucursal", GetType(Integer))
            'ActualizarStock.Columns.Add("idProducto", GetType(Integer))
            'ActualizarStock.Columns.Add("StockActual", GetType(Integer))
            'ActualizarStock.Columns.Add("Preciolista", GetType(Decimal))
            'ActualizarStock.Columns.Add("FechaActualizacion", GetType(Date))

            Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                dt = tblStock
                Dim cmd As SqlCommand
                oconexion.Open()
                For i = 0 To dt.Rows.Count - 1
                    cmd = New SqlCommand("sp_ActualizarStock", oconexion)
                    cmd.Parameters.AddWithValue("@idSucursal", dt.Rows(i).Item("IdSucursal"))
                    cmd.Parameters.AddWithValue("@idProducto", dt.Rows(i).Item("idProducto"))
                    cmd.Parameters.AddWithValue("@Cantidad", dt.Rows(i).Item("Cantidad"))
                    cmd.Parameters.AddWithValue("@StockCritico", dt.Rows(i).Item("StockCritico"))
                    cmd.Parameters.AddWithValue("@Precio", dt.Rows(i).Item("PrecioUnitario"))
                    cmd.Parameters.AddWithValue("@FechaActualizacion", Date.Today)
                    cmd.Parameters.AddWithValue("@Referencia", dt.Rows(i).Item("Referencia"))
                    cmd.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output
                    cmd.CommandType = CommandType.StoredProcedure

                    cmd.ExecuteNonQuery()
                    idGenerado = Convert.ToInt32(cmd.Parameters("@Resultado").Value)
                    Mensaje = cmd.Parameters("@Mensaje").Value.ToString()
                Next i
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
End Class
