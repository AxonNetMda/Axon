Public Class maestromovimiento
    Public Property idMovimiento As Integer
    Public Property oDeSucursal As sucursal
    Public Property oASucursal As sucursal
    Public Property PuntoVenta As Integer
    Public Property oTipoComprobante As tipocomprobantes
    Public Property Numerocomprobante As Integer
    Public Property Comprobante As String 'formato XXX-00000-00000000
    Public Property letra As String
    Public Property Fecha As Date
    Public Property oCliente As cliente
    Public Property oProveedor As proveedor
    Public Property NetoGravado As Decimal
    Public Property AlicuotaIVA0 As Decimal
    Public Property AlicuotaIVA25 As Decimal
    Public Property AlicuotaIVA5 As Decimal
    Public Property AlicuotaIVA105 As Decimal
    Public Property AlicuotaIVA21 As Decimal
    Public Property AlicuotaIVA27 As Decimal
    Public Property ImpuestosInternos As Decimal
    Public Property PercepcionIngresosBrutos As Decimal
    Public Property PercepcionIVA As Decimal
    Public Property NoGravado As Decimal
    Public Property ImporteTotal As Decimal
    Public Property DescuentoPorc As Decimal
    Public Property oTipoMovimiento As tipomovimiento
    Public Property oTipoVenta As tipoventa
    Public Property Referencia As String
    Public Property Notas As String
    Public Property oUsuario As usuario
    'Public Property oMaestroDetalle As List(Of maestromovimiento_detalle)
    Public Property tbtDetalleInventario As DataTable
    Public Property tbDetalleRepo As DataTable
    Public Property Estado As Boolean
    Public Property idFormaPago As Integer
    Public Property DetalleNotas As String
End Class
