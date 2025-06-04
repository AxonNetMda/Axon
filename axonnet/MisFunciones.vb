Imports System.Security.Cryptography
Imports System.Web
Public Class MisFunciones
    Implements IHttpModule
    Public Shared nUsuario As Integer
    Public Shared sNombreUsuario As String
    Public Shared sRolNombre As String
    Public Shared nRol As Integer
    Public Shared nSucursal As Integer
    Public Shared nPtoVenta As Integer
    Public Shared sCodigoAtencion As String
    Public Shared modalMensaje As String
    Private WithEvents _context As HttpApplication

    ''' <summary>
    '''  Deberá configurar este módulo en el archivo web.config de su
    '''  web y registrarlo en IIS para poder usarlo. Para obtener más información
    '''  consulte el vínculo siguiente: https://go.microsoft.com/?linkid=8101007
    ''' </summary>
#Region "Miembros de IHttpModule"

    Public Sub Dispose() Implements IHttpModule.Dispose

        ' Ponga aquí el código de limpieza

    End Sub

    Public Sub Init(ByVal context As HttpApplication) Implements IHttpModule.Init
        _context = context
    End Sub

#End Region

    Public Sub OnLogRequest(ByVal source As Object, ByVal e As EventArgs) Handles _context.LogRequest

        ' Controla el evento LogRequest para proporcionar una implementación de 
        ' registro personalizado para él

    End Sub
    Public Class OpcionesCombo
        Public Property texto As String
        Public Property valor As Object
        Public Overrides Function ToString() As String
            Return texto
        End Function


    End Class

    Public Shared Function Encriptar(ByVal Input As String) As String

        Dim IV() As Byte = ASCIIEncoding.ASCII.GetBytes("qualityi") 'La clave debe ser de 8 caracteres
        Dim EncryptionKey() As Byte = Convert.FromBase64String("rpaSPvIvVLlrcmtzPU9/c67Gkj7yL1S5") 'No se puede alterar la cantidad de caracteres pero si la clave
        Dim buffer() As Byte = Encoding.UTF8.GetBytes(Input)
        Dim des As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider
        des.Key = EncryptionKey
        des.IV = IV

        Return Convert.ToBase64String(des.CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length()))

    End Function

    Public Shared Function Desencriptar(ByVal Input As String) As String
        If Trim(Input) = "" Then
            Return ""
        Else


            Dim IV() As Byte = ASCIIEncoding.ASCII.GetBytes("qualityi") 'La clave debe ser de 8 caracteres
            Dim EncryptionKey() As Byte = Convert.FromBase64String("rpaSPvIvVLlrcmtzPU9/c67Gkj7yL1S5") 'No se puede alterar la cantidad de caracteres pero si la clave
            Dim buffer() As Byte = Convert.FromBase64String(Input)
            Dim des As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider
            des.Key = EncryptionKey
            des.IV = IV
            Return Encoding.UTF8.GetString(des.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length()))
        End If
    End Function

End Class
