Public Class tipocompra
    Private nIdTipocompra As Integer
    Private sNombre As String
    Private bEstado As Boolean
    Public Property idTipocompra() As Integer
        Get
            Return nIdTipocompra
        End Get
        Set(ByVal value As Integer)
            nIdTipocompra = value
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
