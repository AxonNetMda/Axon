Public Class producto
    Public Property idProducto As Integer
    Public Property CodigoBarras As String
    Public Property Nombre As String
    Public Property oCategoria As categoria
    Public Property oSubCategoria As subcategoria
    Public Property oMarca As marca
    Public Property oProveedor As proveedor
    Public Property PrecioCosto As Decimal
    Public Property AlicuotaIVA As Decimal
    Public Property Ganancia As Decimal
    Public Property Redondeo As Integer
    Public Property StockCritico As Integer
    Public Property rutaImagen As String
    Public Property NombreFoto1 As String
    Public Property NombreFoto2 As String

    Public Property NombreFoto3 As String

    Public Property NombreFoto4 As String

    Public Property NombreFoto5 As String

    Public Property NombreFoto6 As String

    Public Property NombreFoto7 As String

    Public Property NombreFoto8 As String
    Public Property NombreFoto9 As String

    Public Property NombreFoto10 As String

    Public Property NombreFoto11 As String

    Public Property NombreFoto12 As String
    Public Property Notas As String
    Public Property FechaUltimaCompra As Date
    Public Property FechaCreacion As Date
    Public Property FechaUltimaActualizacion As Date
    Public Property MostrarCatalogo As Boolean
    Public Property EsDestacado As Boolean
    Public Property EsOferta As Boolean
    Public Property EsOutlet As Boolean
    Public Property Estado As Boolean

    Public Property stk_idsucursal As sucursal
    Public Property stk_StockActual As Integer
    Public Property stk_Critico As Integer
    Public Property PrecioVenta As Decimal

    Public Property Inv_fecFecha As Date
    Public Property inv_Detalle As String
    Public Property inv_CantidadEntrada As Decimal
    Public Property inv_PrecioEntrada As Decimal
    Public Property oCliente As cliente
    Public Property inv_CantidadSalida As Decimal
    Public Property inv_PrecioSalida As Decimal
    Public Property oUsuario As usuario
    Public Property inv_TipoOperacion As tipomovimiento
    Public Property inv_Referencia As String
End Class
