Public Class compra
    Public Property idcompra As Integer
    Public Property oSucursal As sucursal
    Public Property oTipoCompra As tipocompra
    Public Property fecha As Date
    Public Property oFormaPago As formadepago
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
    Public Property oUsuario As usuario
    Public Property Estado As Boolean
    Public Property oProveedor As proveedor
    Public Property puntoVenta As Integer
    Public Property Numerocomprobante As Integer
    Public Property oTipoComprobante As tipocomprobantes
    Public Property Letra As String
    Public Property Referencia As String
    Public Property oDetalle As List(Of compra_detalle)

    '*************************************************

End Class
