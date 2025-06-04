
Imports CapaDatos
Imports System.Data
Imports capaEntidad
Imports System.Text
Imports System.Runtime.InteropServices
Imports System.Data.SqlClient
Imports CapaDatos.Conexion

Public Class CD_Proveedor
    Public lista As List(Of proveedor) = New List(Of proveedor)
    Dim oconexion As New SqlConnection(conectar.Cadena)
    Public Function Listar(ByVal nproveedor As Integer) As List(Of proveedor)
        Dim lista As List(Of proveedor) = New List(Of proveedor)()
        Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)

            Try
                Dim query As StringBuilder = New StringBuilder()
                'Select proveedor.idProveedor, proveedor.RazonSocial, proveedor.NombreComercial, proveedor.Direccion,
                'proveedor.CodigoPostal, proveedor.Localidad, proveedor.Provincia, proveedor.NumeroDocumento,
                'proveedor.idTipoDocumento, proveedor.idTipoResponsable, proveedor.NumeroDocumento, proveedor.Telefono,
                'proveedor.celular, proveedor.WhatsApp, proveedor.Email, proveedor.estado, proveedor.Saldo, proveedor.FechaAlta,
                'tiposderesponsables.Nombre As Responsable, tiposdocumentos.Nombre as DocNombre FROM proveedor
                'INNER Join tiposderesponsables ON proveedor.idTipoResponsable = tiposderesponsables.idTipoResponsable
                'INNER Join tiposdocumentos ON proveedor.idTipoDocumento = tiposdocumentos.idTipoDocumento 
                'WHERE proveedor.idProveedor = 17
                'ORDER BY proveedor.RazonSocial Asc


                query.AppendLine("Select proveedor.idProveedor, proveedor.RazonSocial, proveedor.NombreComercial, proveedor.Direccion,")
                query.AppendLine("proveedor.CodigoPostal, proveedor.Localidad, proveedor.Provincia, proveedor.NumeroDocumento,")
                query.AppendLine("proveedor.idTipoDocumento, proveedor.idTipoResponsable, proveedor.NumeroDocumento, proveedor.Telefono,")
                query.AppendLine("proveedor.celular, proveedor.WhatsApp, proveedor.Email, proveedor.estado, proveedor.Saldo, proveedor.FechaAlta,")
                query.AppendLine("tiposderesponsables.Nombre As Responsable, tiposdocumentos.Nombre as DocNombre FROM proveedor")
                query.AppendLine("INNER Join tiposderesponsables ON proveedor.idTipoResponsable = tiposderesponsables.idTipoResponsable")
                query.AppendLine("INNER Join tiposdocumentos ON proveedor.idTipoDocumento = tiposdocumentos.idTipoDocumento ")
                If nproveedor = 0 Then
                    'query.AppendLine("WHERE proveedor.idProveedor <>1 ")
                Else
                    query.AppendLine("WHERE proveedor.idProveedor =" & nproveedor)
                End If
                query.AppendLine("ORDER BY proveedor.RazonSocial Asc")
                Dim cmd As SqlCommand = New SqlCommand(query.ToString(), oconexion)
                cmd.CommandType = CommandType.Text
                oconexion.Open()

                Using dr As SqlDataReader = cmd.ExecuteReader()

                    While dr.Read()
                        lista.Add(New proveedor() With {
                            .idProveedor = Convert.ToInt32(dr("idproveedor")),
                            .RazonSocial = dr("RazonSocial").ToString(),
                            .NombreComercial = dr("NombreComercial").ToString(),
                            .Direccion = dr("Direccion").ToString(),
                            .CodigoPostal = dr("CodigoPostal").ToString(),
                            .Localidad = dr("Localidad").ToString(),
                            .Provincia = dr("Provincia").ToString(),
                            .estado = Convert.ToBoolean(dr("Estado")),
                            .oTipoResponsable = New tiposresponsables() With {.idTipoResponsable = Convert.ToInt32(dr("idTipoResponsable")), .Nombre = dr("Responsable").ToString()},
                            .oTipoDocumento = New TipoDocumento() With {.idTipoDocumento = Convert.ToInt32(dr("idTipoDocumento")), .Nombre = dr("DocNombre").ToString()},
                            .Telefono = dr("Telefono").ToString(),
                            .celular = dr("Celular").ToString(),
                            .WhatsApp = dr("WhatsApp").ToString(),
                            .Email = dr("Email").ToString(),
                            .NumeroDocumento = dr("NumeroDocumento").ToString(),
                            .Saldo = dr("Saldo").ToString(),
                            .FechaAlta = IIf(IsDBNull(dr("FechaAlta").ToString()), Date.Today, dr("FechaAlta").ToString())
                        })


                    End While
                End Using
                If lista.Count = 0 Then
                    lista = New List(Of proveedor)()
                End If
            Catch ex As Exception
                lista = New List(Of proveedor)()
            End Try
        End Using

        Return lista
    End Function

    Public Function Registrar(ByVal obj As proveedor, <Out> ByRef Mensaje As String) As Integer
        Dim idGenerado As Integer = 0
        Mensaje = String.Empty

        Try

            Using oconexion As SqlConnection = New SqlConnection(Conexion.conectar.Cadena)
                Dim cmd As SqlCommand = New SqlCommand("sp_proveedor_add", oconexion)
                cmd.Parameters.AddWithValue("@RazonSocial", obj.RazonSocial)
                cmd.Parameters.AddWithValue("@NombreComercial", obj.NombreComercial)
                cmd.Parameters.AddWithValue("@Direccion", obj.Direccion)
                cmd.Parameters.AddWithValue("@CodigoPostal", obj.CodigoPostal)
                cmd.Parameters.AddWithValue("@Localidad", obj.Localidad)
                cmd.Parameters.AddWithValue("@Provincia", obj.Provincia)
                cmd.Parameters.AddWithValue("@Telefono", obj.Telefono)
                cmd.Parameters.AddWithValue("@Celular", obj.celular)
                cmd.Parameters.AddWithValue("@Whatsapp", obj.celular)
                cmd.Parameters.AddWithValue("@Email", obj.Email)
                cmd.Parameters.AddWithValue("@idTipoDocumento", obj.oTipoDocumento.idTipoDocumento)
                cmd.Parameters.AddWithValue("@NumeroDocumento", obj.NumeroDocumento)
                cmd.Parameters.AddWithValue("@idTipoResponsable", obj.oTipoResponsable.idTipoResponsable)
                cmd.Parameters.AddWithValue("@Saldo", obj.Saldo)
                cmd.Parameters.AddWithValue("@fechaAlta", Format(obj.FechaAlta, "yyyy-MM-dd"))
                cmd.Parameters.AddWithValue("@Estado", obj.estado)
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

    Public Function Editar(ByVal obj As proveedor, <Out> ByRef Mensaje As String) As Boolean
        Dim Respuesta As Boolean = False
        Mensaje = String.Empty

        Try

            Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                Dim cmd As SqlCommand = New SqlCommand("sp_proveedor_edit", oconexion)
                cmd.Parameters.AddWithValue("@IdProveedor", obj.idProveedor)
                cmd.Parameters.AddWithValue("@RazonSocial", obj.RazonSocial)
                cmd.Parameters.AddWithValue("@NombreComercial", obj.NombreComercial)
                cmd.Parameters.AddWithValue("@Direccion", obj.Direccion)
                cmd.Parameters.AddWithValue("@CodigoPostal", obj.CodigoPostal)
                cmd.Parameters.AddWithValue("@Localidad", obj.Localidad)
                cmd.Parameters.AddWithValue("@Provincia", obj.Provincia)
                cmd.Parameters.AddWithValue("@Telefono", obj.Telefono)
                cmd.Parameters.AddWithValue("@Celular", obj.celular)
                cmd.Parameters.AddWithValue("@Whatsapp", obj.celular)
                cmd.Parameters.AddWithValue("@Email", obj.Email)
                cmd.Parameters.AddWithValue("@idTipoDocumento", obj.oTipoDocumento.idTipoDocumento)
                cmd.Parameters.AddWithValue("@NumeroDocumento", obj.NumeroDocumento)
                cmd.Parameters.AddWithValue("@idTipoResponsable", obj.oTipoResponsable.idTipoResponsable)
                cmd.Parameters.AddWithValue("@Saldo", obj.Saldo)
                cmd.Parameters.AddWithValue("@fechaAlta", Format(obj.FechaAlta, "yyyy-MM-dd"))
                cmd.Parameters.AddWithValue("@Estado", obj.estado)
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

    Public Function Eliminar(ByVal obj As proveedor, <Out> ByRef Mensaje As String) As Boolean
        Dim Respuesta As Boolean = False
        Mensaje = String.Empty

        Try
            If obj.idProveedor = 1 Then
                Respuesta = False
                Mensaje = "No se puede eliminar este proveedor"
            Else
                Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                    Dim cmd As SqlCommand = New SqlCommand("sp_proveedor_borrar", oconexion)
                    cmd.Parameters.AddWithValue("p_idProveedor", obj.idProveedor)
                    cmd.Parameters.Add("o_Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output
                    cmd.Parameters.Add("o_Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output
                    cmd.CommandType = CommandType.StoredProcedure
                    oconexion.Open()
                    cmd.ExecuteNonQuery()
                    Respuesta = Convert.ToBoolean(cmd.Parameters("o_Respuesta").Value)
                    Mensaje = cmd.Parameters("o_Mensaje").Value.ToString()
                End Using
            End If


        Catch ex As Exception
            Respuesta = False
            Mensaje = ex.Message
        End Try

        Return Respuesta

    End Function
End Class
