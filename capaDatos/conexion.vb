﻿Imports System.Configuration

Public Class Conexion
    Public Class conectar
        Public Shared Property Cadena As String = ConfigurationManager.ConnectionStrings("cadena2").ToString()
    End Class
End Class
