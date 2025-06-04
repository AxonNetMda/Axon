Public Class reposicion
    Public Property idReposicion As Integer
    Public Property oDeSucursal As sucursal
    Public Property oASucursal As sucursal
    Public Property fecha As Date
    Public Property oProducto As producto
    Public Property cantidad As Integer
    Public Property oUsuario As usuario
    Public Property Notas As String
    Public Property Referencia As String
    Public Property PuntoVenta As Integer
    Public Property NumeroComprobante As Integer
    Public Property oDetalle As List(Of reposiciondetalles)
    Public Property Estado As Boolean
End Class
