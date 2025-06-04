Imports System.Security.Cryptography.X509Certificates

Public Class CertificadoAFIP
	Public Property CUIT As String
	Public Property Certificado As X509Certificate2
	Public Property CertificadoBase64 As String
	Public Property ClaveCertificado As String
	Public Property Token As String
	Public Property Sign As String
	Public Property Homologacion As Boolean
	Public Property ExpirationTime As DateTime
End Class
