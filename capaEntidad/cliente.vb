Public Class cliente
	Public Property idcliente As Integer
	Public Property nombre As String
	Public Property apellido As String
	Public Property NombreCompleto As String
	Public Property Direccion As String
	Public Property codigopostal As String
	Public Property localidad As String
	Public Property provincia As String
	Public Property pais As String
	Public Property oTipoDocumento As TipoDocumento
	Public Property oTipoResponsable As tiposresponsables
	Public Property NumeroDocumento As String
	Public Property TelefonoMovil As Integer '0=movil 1=telefono
	Public Property TelefonoPais As String
	Public Property TelefonoArea As String
	Public Property TelefonoNumero As String
	Public Property TelefonoNumerocompleto As String
	Public Property correo As String
	Public Property clave As String
	Public Property Saldo As Decimal
	Public Property restablecer As Boolean
	Public Property fechaAlta As Date
	Public Property Estado As Boolean
End Class
