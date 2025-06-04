Public Class producto_inventario
    Public Property idProducto As Integer
    Public Property idSucursal As Integer
    Public Property fecFecha As Date
    Public Property Detalle As String
    Public Property oProveedor As proveedor
    Public Property CantidadEntrada As Decimal
    Public Property PrecioEntrada As Decimal
    Public Property oCliente As cliente
    Public Property CantidadSalida As Decimal
    Public Property PrecioSalida As Decimal
    Public Property FechaActualizado As Date
    Public Property oUsuario As usuario
    Public Property TipoOperacion As Integer
    Public Property Referencia As String
End Class
