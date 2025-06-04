Public Class ciudad
    Private Property nCiudad As String
    Private Property nCodigoPais As String
    Private sNombrePais As String
    Public Property NombrePais() As String
        Get
            Return sNombrePais
        End Get
        Set(ByVal value As String)
            sNombrePais = value
        End Set
    End Property
    Public Property CodigoPais() As String
        Get
            Return nCodigoPais
        End Get
        Set(ByVal value As String)
            nCodigoPais = value
        End Set
    End Property
    Public Property idCiudad() As String
        Get
            Return nCiudad
        End Get
        Set(ByVal value As String)
            nCiudad = value
        End Set
    End Property

End Class
