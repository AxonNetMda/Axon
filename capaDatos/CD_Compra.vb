Imports System.Data.SqlClient
Imports capaEntidad
Imports System.Text
Imports System.Runtime.InteropServices
Imports CapaDatos.Conexion

Public Class CD_Compra
    Public lista As List(Of compra) = New List(Of compra)
    Dim oconexion As New SqlConnection(conectar.Cadena)
    Public Function Listar(ByVal nidcompra As Integer, ByVal nidSucursal As Integer) As List(Of compra)
        Dim lista As List(Of compra) = New List(Of compra)()
        Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
            Dim filtro As String

            Try
                Dim query As String
                If nidcompra = 0 Then
                    filtro = " Where idDeSucursal=" & nidSucursal
                Else
                    filtro = " Where idDeSucursal=" & nidSucursal & " AND idMaestroCabecera=" & nidcompra
                End If
                query = "SELECT dbo.compra.idcompra, dbo.compra.idTipocompra, dbo.tipocompra.Nombre AS NombreTipocompra,
                        dbo.compra.idSucursa, se.Nombre AS NombreSucursal, dbo.compra.idTipoComprobante, dbo.tiposdocumentos.Nombre AS NombreTipoDocumento,
                        dbo.compra.Letra, dbo.compra.PuntoVenta, dbo.compra.NumeroComprobante,
                        dbo.compra.Fecha, dbo.compra.idProveedor, dbo.proveedor.RazonSocial AS NombreProveedor,
                        dbo.compra.NetoGravado, dbo.compra.AlicuotaIVA0, dbo.compra.AlicuotaIva25,
                        dbo.compra.AlicuotaIVA5, dbo.compra.AlicuotaIva105, dbo.compra.AlicuotaIva21,
                        dbo.compra.AlicuotaIva27, dbo.compra.ImpuestosInternos, dbo.compra.PercepcionIngresosBrutos,
                        dbo.compra.PercepcionIVA, dbo.compra.NoGravado, dbo.compra.ImporteTotal,
                        dbo.compra.DescuentoPorc, dbo.compra.Referencia, dbo.compra.IdUsuario,
                        dbo.usuario.Nombre AS NombreUsuario, dbo.compra.Estado, dbo.compra.idFormaPago, dbo.formadepago.nombre as NombreFormaPago, 
                        dbo.tipodecomprobante.idComprobante, dbo.tipodecomprobante.Nombre as NombreComprobante 
                        FROM dbo.compra
                        INNER JOIN dbo.tipocompra ON dbo.compra.idTipocompra = dbo.tipoventa.idTipocompra
                        INNER JOIN dbo.formadepago ON dbo.compra.idformapago = dbo.formadepago.idformaPago 
                        INNER JOIN dbo.Sucursales se ON dbo.compra.idSucursal = se.idSucursal
                        INNER JOIN dbo.tiposdocumentos ON dbo.compra.idTipoComprobante = dbo.tiposdocumentos.idTipoDocumento
                        INNER JOIN dbo.tipodecomprobante ON dbo.compra.idTipoComprobante = dbo.tipodecomprobante.idComprobante
                        INNER JOIN dbo.proveedor ON dbo.compra.idProveedor = dbo.proveedor.idProveedor
                        INNER JOIN dbo.usuario ON dbo.compra.IdUsuario = dbo.usuario.idUsuario " & filtro & " order by FechaEmision"
                Dim cmd As SqlCommand = New SqlCommand(query.ToString(), oconexion)
                cmd.CommandType = CommandType.Text
                oconexion.Open()

                Using dr As SqlDataReader = cmd.ExecuteReader()

                    While dr.Read()
                        lista.Add(New compra() With {
                            .idcompra = Convert.ToInt32(dr("idMovimiento")),
                            .oSucursal = New sucursal() With {.idSucursal = dr("idSucursal"), .Nombre = dr("NombreSucursal")},
                            .oTipoCompra = New tipocompra() With {.idTipocompra = dr("idTipocompra"), .Nombre = dr("NombreTipoComrpa")},
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
                            .oProveedor = New proveedor() With {.idProveedor = dr("idProveedor"), .RazonSocial = dr("NombreProveedor")},
                            .puntoVenta = dr("PuntoVenta"),
                            .Numerocomprobante = dr("NumeroComprobante"),
                            .oTipoComprobante = New tipocomprobantes() With {.idTipoComprobante = dr("idTipoComprobante"), .strNombre = dr("NombreComprobante")},
                            .Letra = dr("Letra"),
                            .Referencia = dr("Referencia")
                        })

                    End While

                End Using

            Catch ex As Exception
                lista = New List(Of compra)()
            End Try
        End Using

        Return lista
    End Function
    Public Function Registrar(ByVal obj As compra, ByVal objDetalle As DataTable, <Out> ByRef Mensaje As String) As Integer
        Dim idGenerado As Integer = 0
        Mensaje = String.Empty
        Dim nComprobante As Integer = 0
        Try
            Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                Dim cmd As SqlCommand = New SqlCommand("sp_compras_Registrar", oconexion)
                cmd.Parameters.AddWithValue("@idSucursal", obj.oSucursal.idSucursal)
                cmd.Parameters.AddWithValue("@idTipoCompra", obj.oTipoCompra.idTipocompra)
                cmd.Parameters.AddWithValue("@idTipoMovimiento", 3)
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
                cmd.Parameters.AddWithValue("@idProveedor", obj.oProveedor.idProveedor)
                cmd.Parameters.AddWithValue("@PuntoVenta", obj.puntoVenta)
                cmd.Parameters.AddWithValue("@Numerocomprobante", obj.Numerocomprobante)
                cmd.Parameters.AddWithValue("@idTipocomprobante", obj.oTipoComprobante.idTipoComprobante)
                cmd.Parameters.AddWithValue("@Letra", obj.Letra)
                cmd.Parameters.AddWithValue("@Detallecompra", objDetalle)
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
