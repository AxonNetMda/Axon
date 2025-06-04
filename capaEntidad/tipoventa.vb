Public Class tipoventa
    Private nIdTipoVenta As Integer
    Private sNombre As String
    Private bEstado As Boolean
    Public Property idTipoVenta() As Integer
        Get
            Return nIdTipoVenta
        End Get
        Set(ByVal value As Integer)
            nIdTipoVenta = value
        End Set
    End Property

    Public Property Estado() As String
        Get
            Return bEstado
        End Get
        Set(ByVal value As String)
            bEstado = value
        End Set
    End Property

    Public Property Nombre() As String
        Get
            Return sNombre
        End Get
        Set(ByVal value As String)
            sNombre = value
        End Set
    End Property
End Class
