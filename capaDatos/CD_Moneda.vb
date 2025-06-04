Imports System.Data.SqlClient
Imports capaEntidad
Imports System.Text
Imports System.Runtime.InteropServices
Imports CapaDatos.Conexion

Public Class CD_Moneda
    Public lista As List(Of moneda) = New List(Of moneda)
    Dim oconexion As New SqlConnection(conectar.Cadena)
    Public Function Listar(ByVal nIdMoneda As Integer) As List(Of moneda)
        Dim lista As List(Of moneda) = New List(Of moneda)()
        Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)

            Try
                Dim query As StringBuilder = New StringBuilder()
                If nIdMoneda = 0 Then
                    query.AppendLine("SELECT *  from moneda ")
                    query.AppendLine("Order by Nombre ")
                Else
                    query.AppendLine("SELECT * from moneda ")
                    query.AppendLine("WHERE IdMoneda =" & nIdMoneda & " order by nombre ")
                End If

                Dim cmd As SqlCommand = New SqlCommand(query.ToString(), oconexion)
                cmd.CommandType = CommandType.Text
                oconexion.Open()

                Using dr As SqlDataReader = cmd.ExecuteReader()

                    While dr.Read()
                        lista.Add(New moneda() With {
                            .idMoneda = Convert.ToInt32(dr("idMoneda")),
                            .Nombre = UCase(dr("Nombre").ToString()),
                            .Cotizacion = Convert.ToDecimal(dr("Cotizacion")),
                            .fechaActualizacion = FormatDateTime(dr("FechaCotizacion"), vbShortDate),
                            .MonedaPrincipal = Convert.ToBoolean(dr("MonedaPrincipal")),
                            .Estado = Convert.ToBoolean(dr("Estado"))
                        })

                    End While

                End Using

            Catch ex As Exception
                lista = New List(Of moneda)()
            End Try
        End Using

        Return lista
    End Function

    Public Function Registrar(ByVal obj As moneda, <Out> ByRef Mensaje As String) As Integer
        Dim idGenerado As Integer = 0
        Mensaje = String.Empty

        Try

            Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                Dim cmd As SqlCommand = New SqlCommand("sp_moneda_add", oconexion)
                cmd.Parameters.AddWithValue("@Nombre", obj.Nombre)
                cmd.Parameters.AddWithValue("@Cotizacion", Convert.ToDecimal(obj.Cotizacion))
                cmd.Parameters.AddWithValue("@fechaCotizacion", obj.fechaActualizacion)
                cmd.Parameters.AddWithValue("@MonedaPrincipal", Convert.ToBoolean(obj.MonedaPrincipal))
                cmd.Parameters.AddWithValue("@Estado", True)
                cmd.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output
                cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output
                cmd.CommandType = CommandType.StoredProcedure
                oconexion.Open()
                cmd.ExecuteNonQuery()
                idGenerado = Convert.ToInt32(cmd.Parameters("@Resultado").Value)
                Mensaje = cmd.Parameters("@Mensaje").Value.ToString()
            End Using

        Catch ex As Exception
            idGenerado = 0
            Mensaje = ex.Message
        End Try

        Return idGenerado
    End Function

    Public Function Editar(ByVal obj As moneda, <Out> ByRef Mensaje As String) As Boolean
        Dim Respuesta As Boolean = False
        Mensaje = String.Empty
        Dim importe As Double = 0
        Try

            Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                importe = Replace((obj.Cotizacion), ".", ",")
                Dim cmd As SqlCommand = New SqlCommand("sp_moneda_edit", oconexion)
                cmd.Parameters.AddWithValue("@IdMoneda", obj.idMoneda)
                cmd.Parameters.AddWithValue("@Nombre", obj.Nombre)
                cmd.Parameters.AddWithValue("@Cotizacion", Convert.ToDouble(obj.Cotizacion))
                cmd.Parameters.AddWithValue("@fechaCotizacion", obj.fechaActualizacion)
                cmd.Parameters.AddWithValue("@MonedaPrincipal", Convert.ToBoolean(obj.MonedaPrincipal))
                cmd.Parameters.AddWithValue("@Estado", Convert.ToBoolean(obj.Estado))
                cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output
                cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output
                cmd.CommandType = CommandType.StoredProcedure
                oconexion.Open()
                cmd.ExecuteNonQuery()
                Respuesta = Convert.ToBoolean(cmd.Parameters("@Resultado").Value)
                Mensaje = cmd.Parameters("@Mensaje").Value.ToString()
            End Using

        Catch ex As Exception
            Respuesta = False
            Mensaje = ex.Message
        End Try

        Return Respuesta
    End Function

    Public Function Eliminar(ByVal obj As moneda, <Out> ByRef Mensaje As String) As Boolean
        Dim Respuesta As Boolean = False
        Mensaje = String.Empty

        Try

            Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                Dim cmd As SqlCommand = New SqlCommand("sp_marca_borrar", oconexion)
                cmd.Parameters.AddWithValue("@idmarca", obj.idMoneda)
                cmd.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output
                cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output
                cmd.CommandType = CommandType.StoredProcedure
                oconexion.Open()
                cmd.ExecuteNonQuery()
                Respuesta = Convert.ToBoolean(cmd.Parameters("@Resultado").Value)
                Mensaje = cmd.Parameters("@Mensaje").Value.ToString()
            End Using

        Catch ex As Exception
            Respuesta = False
            Mensaje = ex.Message
        End Try

        Return Respuesta

    End Function
End Class
