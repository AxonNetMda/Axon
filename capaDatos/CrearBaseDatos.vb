Imports System.Data.SqlClient
Imports capaDatos.conexion

Public Class CrearBaseDatos
	Private Sub VerificarBaseDeDatosYTabla()
		' Obtener la cadena de conexión desde el Web.config
		Dim cadenaConexion As String = conectar.Cadena
		Dim nombreBaseDatos As String = ""
		' Verificar y crear la base de datos si no existe

		Using conexion As New SqlConnection(cadenaConexion)
			Try
				conexion.Open()

				Dim verificarBDQuery As String = $"IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = '{nombreBaseDatos}') CREATE DATABASE {nombreBaseDatos}"
				Dim comando As New SqlCommand(verificarBDQuery, conexion)
				comando.ExecuteNonQuery()
			Catch ex As Exception
				Throw New Exception("Error al verificar o crear la base de datos: " & ex.Message)
			End Try
		End Using

		' Cadena de conexión con la base de datos
		Dim cadenaConexionBD As String = cadenaConexion.Replace("Database=NombreBaseDatos;", $"Database={nombreBaseDatos};")

		' Verificar y crear la tabla si no existe
		Using conexionBD As New SqlConnection(cadenaConexionBD)
			Try
				conexionBD.Open()

				Dim verificarTablaQuery As String =
				"IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Certificadosv3') " &
				"CREATE TABLE Certificadosv3 (" &
				"Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY, " &
				"CUIT VARCHAR(20) NOT NULL, " &
				"Servicio VARCHAR(50) NOT NULL, " &
				"CertificadoBase64 VARCHAR(MAX) NOT NULL, " &
				"CmsFirmadoBase64 VARCHAR(MAX) NULL, " &
				"Token VARCHAR(MAX) NULL, " &
				"Sign VARCHAR(MAX) NULL, " &
				"Fecha DATETIME NULL DEFAULT (GETDATE()), " &
				"ClaveCertificado VARCHAR(50) NULL, " &
				"Entorno VARCHAR(50) NULL DEFAULT ('Producción'), " &
				"FechaExpiracion DATE NULL, " &
				"CONSTRAINT UQ_CUIT_Servicio_Entorno UNIQUE (CUIT, Servicio, Entorno))"

				Dim comandoTabla As New SqlCommand(verificarTablaQuery, conexionBD)
				comandoTabla.ExecuteNonQuery()
			Catch ex As Exception
				Throw New Exception("Error al verificar o crear la tabla: " & ex.Message)
			End Try
		End Using
	End Sub

End Class
