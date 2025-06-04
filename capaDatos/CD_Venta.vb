Imports System.Data.SqlClient
Imports capaEntidad
Imports System.Text
Imports System.Runtime.InteropServices
Imports CapaDatos.Conexion

Public Class CD_Venta
    Public lista As List(Of venta) = New List(Of venta)
    Dim oconexion As New SqlConnection(conectar.Cadena)
    Public Function Listar(ByVal nidventa As Integer, ByVal nidSucursal As Integer) As List(Of venta)
        Dim lista As List(Of venta) = New List(Of venta)()
        Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
            Dim filtro As String

            Try
                Dim query As String
                If nidventa = 0 Then
                    filtro = " Where idDeSucursal=" & nidSucursal
                Else
                    filtro = " Where idDeSucursal=" & nidSucursal & " AND idMaestroCabecera=" & nidventa
                End If
                query = "SELECT dbo.ventas.idventa, dbo.ventas.idTipoventa, dbo.tipoventa.Nombre AS NombreTipoventa,
                        dbo.ventas.idSucursal, dbo.Sucursales.Nombre AS NombreSucursal, dbo.ventas.idTipoComprobante, dbo.tiposdocumentos.Nombre AS NombreTipoDocumento,
                        dbo.ventas.Letra, dbo.ventas.PuntoVenta, dbo.ventas.NumeroComprobante,
                        dbo.ventas.Fecha, dbo.ventas.idCliente, dbo.Cliente.Apellido AS NombreCliente,
                        dbo.ventas.NetoGravado, dbo.ventas.AlicuotaIVA0, dbo.ventas.AlicuotaIva25,
                        dbo.ventas.AlicuotaIVA5, dbo.ventas.AlicuotaIva105, dbo.ventas.AlicuotaIva21,
                        dbo.ventas.AlicuotaIva27, dbo.ventas.ImpuestosInternos, dbo.ventas.PercepcionIngresosBrutos,
                        dbo.ventas.PercepcionIVA, dbo.ventas.NoGravado, dbo.ventas.ImporteTotal,
                        dbo.ventas.DescuentoPorc, dbo.ventas.Referencia, dbo.ventas.IdUsuario,
                        dbo.usuario.Nombre AS NombreUsuario, dbo.ventas.Estado, dbo.ventas.idFormaPago, dbo.formadepago.nombre as NombreFormaPago, 
                        dbo.tipodecomprobante.idComprobante, dbo.tipodecomprobante.strNombre as NombreComprobante 
                        FROM dbo.ventas
                        INNER JOIN dbo.tipoventa ON dbo.ventas.idTipoventa = dbo.tipoventa.idTipoventa
                        INNER JOIN dbo.formadepago ON dbo.ventas.idformapago = dbo.formadepago.idformaPago 
                        INNER JOIN dbo.Sucursales ON dbo.ventas.idSucursal = dbo.Sucursales.idSucursal
                        INNER JOIN dbo.tiposdocumentos ON dbo.ventas.idTipoComprobante = dbo.tiposdocumentos.idTipoDocumento
                        INNER JOIN dbo.tipodecomprobante ON dbo.ventas.idTipoComprobante = dbo.tipodecomprobante.idComprobante
                        INNER JOIN dbo.cliente ON dbo.ventas.idCliente = dbo.cliente.idCliente
                        INNER JOIN dbo.usuario ON dbo.ventas.IdUsuario = dbo.usuario.idUsuario " & filtro & " order by FechaEmision"
                Dim cmd As SqlCommand = New SqlCommand(query.ToString(), oconexion)
                cmd.CommandType = CommandType.Text
                oconexion.Open()

                Using dr As SqlDataReader = cmd.ExecuteReader()

                    While dr.Read()
                        lista.Add(New venta() With {
                            .idVenta = Convert.ToInt32(dr("idventa")),
                            .oSucursal = New sucursal() With {.idSucursal = dr("idSucursal"), .Nombre = dr("NombreSucursal")},
                            .oTipoVenta = New tipoventa() With {.idTipoVenta = dr("idTipoventa"), .Nombre = dr("NombreTipoComrpa")},
                            .fecha = FormatDateTime(dr("FechaEmision"), vbShortDate),
                            .oFormaPago = New formadepago() With {.idformapago = dr("idFormaPago"), .Nombre = dr("NombreFormapPago")},
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
                            .Estado = dr("Estado"),
                            .oCliente = New cliente() With {.idcliente = dr("idProveedor"), .apellido = dr("NombreCliente")},
                            .puntoVenta = dr("PuntoVenta"),
                            .Numerocomprobante = dr("NumeroComprobante"),
                            .oTipoComprobante = New tipocomprobantes() With {.idTipoComprobante = dr("idTipoComprobante"), .strNombre = dr("NombreComprobante")},
                            .Letra = dr("Letra"),
                            .Referencia = dr("Referencia")
                        })

                    End While

                End Using

            Catch ex As Exception
                lista = New List(Of venta)()
            End Try
        End Using

        Return lista
    End Function
    Public Function Registrar(ByVal obj As venta, ByVal objDetalle As DataTable, <Out> ByRef Mensaje As String) As Integer
        Dim idGenerado As Integer = 0
        Mensaje = String.Empty
        Dim nComprobante As Integer = 0
        Try
            Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                Dim cmd As SqlCommand = New SqlCommand("sp_ventas_Registrar", oconexion)
                cmd.Parameters.AddWithValue("@idSucursal", obj.oSucursal.idSucursal)
                cmd.Parameters.AddWithValue("@idTipoventa", obj.oTipoVenta.idTipoVenta)
                cmd.Parameters.AddWithValue("@idTipoMovimiento", 2)
                cmd.Parameters.AddWithValue("@Fecha", obj.fecha)
                cmd.Parameters.AddWithValue("@idFormaPago", obj.oFormaPago.idformapago)
                cmd.Parameters.AddWithValue("@NetoGravado", obj.NetoGravado)
                cmd.Parameters.AddWithValue("@AlicuotaIVA0", obj.AlicuotaIVA0)
                cmd.Parameters.AddWithValue("@AlicuotaIVA25", obj.AlicuotaIVA25)
                cmd.Parameters.AddWithValue("@AlicuotaIVA5", obj.AlicuotaIVA5)
                cmd.Parameters.AddWithValue("@AlicuotaIVA105", obj.AlicuotaIVA105)
                cmd.Parameters.AddWithValue("@AlicuotaIVA21", obj.AlicuotaIVA21)
                cmd.Parameters.AddWithValue("@AlicuotaIVA27", obj.AlicuotaIVA27)
                cmd.Parameters.AddWithValue("@ImpuestosInternos", obj.ImpuestosInternos)
                cmd.Parameters.AddWithValue("@PercepcionIngresosBrutos", obj.PercepcionIngresosBrutos)
                cmd.Parameters.AddWithValue("@PercepcionIVA", obj.PercepcionIVA)
                cmd.Parameters.AddWithValue("@NoGravado", obj.NoGravado)
                cmd.Parameters.AddWithValue("@ImporteTotal", obj.ImporteTotal)
                cmd.Parameters.AddWithValue("@DescuentoPorc", obj.DescuentoPorc)
                cmd.Parameters.AddWithValue("@IdUsuario", obj.oUsuario.idusuario)
                cmd.Parameters.AddWithValue("@Estado", True)
                cmd.Parameters.AddWithValue("@idCliente", obj.oCliente.idcliente)
                cmd.Parameters.AddWithValue("@PuntoVenta", obj.puntoVenta)
                cmd.Parameters.AddWithValue("@Numerocomprobante", obj.Numerocomprobante)
                cmd.Parameters.AddWithValue("@idTipocomprobante", obj.oTipoComprobante.idTipoComprobante)
                cmd.Parameters.AddWithValue("@Letra", obj.Letra)
                cmd.Parameters.AddWithValue("@Detalle_venta", objDetalle)
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

End Class
