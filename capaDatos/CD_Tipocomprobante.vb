
Imports System.Data
Imports capaEntidad
Imports System.Text
Imports System.Runtime.InteropServices
Imports System.Data.SqlClient
Imports CapaDatos.Conexion

Public Class CD_Tipocomprobante
    Public lista As List(Of tipocomprobantes) = New List(Of tipocomprobantes)
    Dim oconexion As New SqlConnection(conectar.Cadena)
    Public Function Listar() As List(Of tipocomprobantes)
        Dim lista As List(Of tipocomprobantes) = New List(Of tipocomprobantes)()
        Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)

            Try
                Dim query As String
                query = "select * FROM tipocomprobante ORDER BY strNombre"
                Dim cmd As SqlCommand = New SqlCommand(query, oconexion)
                cmd.CommandType = CommandType.Text
                oconexion.Open()

                Using dr As SqlDataReader = cmd.ExecuteReader()

                    While dr.Read()
                        lista.Add(New tipocomprobantes() With {
                            .idTipoComprobante = Convert.ToInt32(dr("idTipoComprobante")),
                            .strNombre = dr("strNombre").ToString(),
                            .intHAbilitado = Convert.ToBoolean(dr("intHabilitado")),
                            .Letra = Convert.ToString(dr("Letra"))
                        })

                    End While

                End Using

            Catch ex As Exception
                lista = New List(Of tipocomprobantes)()
            End Try
        End Using

        Return lista
    End Function

    Public Function Registrar(ByVal obj As tipocomprobantes, <Out> ByRef Mensaje As String) As Integer
        Dim idGenerado As Integer = 0
        Mensaje = String.Empty

        Try
            '`idTipoComprobante` int(11) Not NULL AUTO_INCREMENT,
            '`strNombre` varchar(50) DEFAULT NULL,
            '`intHabilitado` bit(1) DEFAULT b'0',
            '`CuandoVendo` bit(1) DEFAULT b'0',
            '`CuandoCompro` bit(1) DEFAULT b'0',
            '`Interno` bit(1) DEFAULT b'0',
            '`Letra` varchar(10) DEFAULT NULL,
            '`DiscriminaIva` bit(1) DEFAULT b'0',
            '`idComprobanteAfip` int(11) DEFAULT '0',
            '`MontoMaximoSinIdentificar` decimal(11,2) DEFAULT '0.00',
            Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                Dim cmd As SqlCommand = New SqlCommand("sp_tipocomprobante_add", oconexion)
                cmd.Parameters.AddWithValue("p_strNombre", obj.strNombre)
                cmd.Parameters.AddWithValue("p_Habilitado", 1)
                cmd.Parameters.Add("o_Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output
                cmd.Parameters.Add("o_Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output
                cmd.CommandType = CommandType.StoredProcedure
                oconexion.Open()
                cmd.ExecuteNonQuery()
                idGenerado = Convert.ToInt32(cmd.Parameters("o_Respuesta").Value)
                Mensaje = cmd.Parameters("o_Mensaje").Value.ToString()
            End Using

        Catch ex As Exception
            idGenerado = 0
            Mensaje = ex.Message
        End Try

        Return idGenerado
    End Function

    Public Function Editar(ByVal obj As tipocomprobantes, <Out> ByRef Mensaje As String) As Boolean
        Dim Respuesta As Boolean = False
        Mensaje = String.Empty

        Try

            Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                Dim cmd As SqlCommand = New SqlCommand("sp_tipocomprobante_editar", oconexion)
                cmd.Parameters.AddWithValue("p_strNombre", obj.strNombre)
                cmd.Parameters.AddWithValue("p_letra", obj.Letra)
                cmd.Parameters.AddWithValue("p_Habilitado", 1)
                cmd.Parameters.Add("o_Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output
                cmd.Parameters.Add("o_Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output
                cmd.CommandType = CommandType.StoredProcedure
                oconexion.Open()
                cmd.ExecuteNonQuery()
                Respuesta = Convert.ToBoolean(cmd.Parameters("o_Respuesta").Value)
                Mensaje = cmd.Parameters("o_Mensaje").Value.ToString()
            End Using

        Catch ex As Exception
            Respuesta = False
            Mensaje = ex.Message
        End Try

        Return Respuesta
    End Function

    Public Function Eliminar(ByVal obj As tipocomprobantes, <Out> ByRef Mensaje As String) As Boolean
        Dim Respuesta As Boolean = False
        Mensaje = String.Empty

        Try

            Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                Dim cmd As SqlCommand = New SqlCommand("sp_Tipocomprobante_borrar", oconexion)
                cmd.Parameters.AddWithValue("p_idTipoResponsable", obj.idTipoComprobante)
                cmd.Parameters.Add("o_Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output
                cmd.Parameters.Add("o_Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output
                cmd.CommandType = CommandType.StoredProcedure
                oconexion.Open()
                cmd.ExecuteNonQuery()
                Respuesta = Convert.ToBoolean(cmd.Parameters("o_Respuesta").Value)
                Mensaje = cmd.Parameters("o_Mensaje").Value.ToString()
            End Using

        Catch ex As Exception
            Respuesta = False
            Mensaje = ex.Message
        End Try

        Return Respuesta

    End Function

End Class
