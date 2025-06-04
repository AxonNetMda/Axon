Public Class Pais
    Public Property idPais As Integer
    Public Property sIdPais As String
    Private Nombre As String
    Public Property NombrePais() As String
        Get
            Return Nombre
        End Get
        Set(ByVal value As String)
            Nombre = value
        End Set
    End Property

End Class
