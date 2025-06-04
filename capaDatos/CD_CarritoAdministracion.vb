Imports System.Data.SqlClient
Imports capaEntidad
Imports System.Text
Imports System.Runtime.InteropServices
Imports CapaDatos.Conexion
Public Class CD_CarritoAdministracion
    Public lista As List(Of carritoadministracion) = New List(Of carritoadministracion)
    Dim oconexion As New SqlConnection(conectar.Cadena)
    Public Function Listar(ByVal Referencia As String) As List(Of carritoadministracion)
        Dim lista As List(Of carritoadministracion) = New List(Of carritoadministracion)()
        Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)

            Try
                Dim query As StringBuilder = New StringBuilder()
                query.AppendLine("Select Case dbo.CarritoAdministracion.idCarrito, dbo.CarritoAdministracion.idSucursal, dbo.Sucursales.Nombre As SucursalNombre, dbo.CarritoAdministracion.idCliente,")
                query.AppendLine("dbo.CarritoAdministracion.fecha, dbo.CarritoAdministracion.idProducto, dbo.producto.Nombre As ProductoNombre, dbo.producto.CodigoBarras, dbo.CarritoAdministracion.Cantidad,")
                query.AppendLine("dbo.CarritoAdministracion.Precio, dbo.CarritoAdministracion.Referencia, dbo.usuario.Nombre as UsuarioNombre, concat(dbo.cliente.apellido, ' ', dbo.cliente.Nombre)  as NombreCompleto ")
                query.AppendLine("From dbo.CarritoAdministracion ")
                query.AppendLine("INNER Join dbo.cliente ON dbo.CarritoAdministracion.idcliente = dbo.cliente.idcliente ")
                query.AppendLine("INNER Join dbo.Sucursales ON dbo.CarritoAdministracion.idSucursal = dbo.Sucursales.idSucursal ")
                query.AppendLine("INNER Join dbo.producto ON dbo.CarritoAdministracion.idProducto = dbo.producto.idProducto ")
                query.AppendLine("INNER Join dbo.usuario ON dbo.CarritoAdministracion.idUsuario = dbo.usuario.idusuario")
                If Referencia = "" Then
                    query.AppendLine("Order by Fecha, referencia ")
                Else
                    query.AppendLine("WHERE referencia ='" & Referencia & "' order by Fecha, referencia ")
                End If

                Dim cmd As SqlCommand = New SqlCommand(query.ToString(), oconexion)
                cmd.CommandType = CommandType.Text
                oconexion.Open()

                Using dr As SqlDataReader = cmd.ExecuteReader()
                    While dr.Read()
                        lista.Add(New carritoadministracion() With {
                            .idCarrito = Convert.ToInt32(dr("idCarrito")),
                            .oSucursal = New sucursal() With {.idSucursal = dr("idSucursal"), .Nombre = dr("SucursdalNombre")},
                            .ocliente = New cliente() With {.idcliente = dr("idCliente"), .apellido = dr("NombreCompleto")},
                            .oProducto = New producto() With {.idProducto = dr("idProducto"), .Nombre = dr("ProductoNombre")},
                            .fecha = dr("Fecha"),
                            .cantidad = dr("Cantidad"),
                            .Precio = dr("Precio"),
                            .Referencia = dr("Referencia"),
                            .oUsuario = New usuario() With {.idusuario = dr("idusuario"), .Nombre = dr("UsuarioNombre")}
                        })

                    End While

                End Using

            Catch ex As Exception
                lista = New List(Of carritoadministracion)()
            End Try
        End Using

        Return lista
    End Function

    Public Function Registrar(ByVal obj As carritoadministracion, <Out> ByRef Mensaje As String) As Integer
        Dim Generado As Integer = 0
        Mensaje = String.Empty

        Try
            Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                Dim sqlCadena As String = "INSERT INTO carritoadministracion (idSucursal, idCliente, Fecha, IdProducto, cantidad, precio, referencia, idusuario) 
                                           values (@idSucursal, @idCliente, @Fecha, @IdProducto, @Cantidad, @Precio, @Referencia, @idUsuario)"
                Dim cmd As SqlCommand = New SqlCommand(sqlCadena, oconexion)
                cmd.Parameters.AddWithValue("@IdSucursal", obj.oSucursal.idSucursal)
                cmd.Parameters.AddWithValue("@IdCliente", obj.ocliente.idcliente)
                cmd.Parameters.AddWithValue("@Fecha", obj.fecha)
                cmd.Parameters.AddWithValue("@idProducto", obj.oProducto.idProducto)
                cmd.Parameters.AddWithValue("@cantidad", obj.cantidad)
                cmd.Parameters.AddWithValue("@Precio", obj.Precio)
                cmd.Parameters.AddWithValue("@Referencia", obj.Referencia)
                cmd.Parameters.AddWithValue("@idusuario", obj.oUsuario.idusuario)
                cmd.CommandType = CommandType.StoredProcedure
                oconexion.Open()
                cmd.ExecuteNonQuery()
            End Using
            Generado = 1
        Catch ex As Exception

            Mensaje = ex.Message
        End Try

        Return Mensaje
    End Function

    Public Function Editar(ByVal obj As carritoadministracion, <Out> ByRef Mensaje As String) As Boolean
        Dim Respuesta As Boolean = False
        Mensaje = String.Empty

        Try

            Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                Dim sqlCadena As String = "UPDATE carritoadministracion SET 
                                           idSucursal=@idSucursal, idCliente=@idCliente, Fecha= @Fecha, IdProducto= @IdProducto, cantidad= @Cantidad, 
                                           precio=@Precio, referencia=@referencia, idUsuario=@idUsuario WHERE Referencia=@Referencia"

                Dim cmd As SqlCommand = New SqlCommand(sqlCadena, oconexion)
                cmd.Parameters.AddWithValue("@idCarrito", obj.idCarrito)
                cmd.Parameters.AddWithValue("@IdSucursal", obj.oSucursal.idSucursal)
                cmd.Parameters.AddWithValue("@IdCliente", obj.ocliente.idcliente)
                cmd.Parameters.AddWithValue("@Fecha", obj.fecha)
                cmd.Parameters.AddWithValue("@idProducto", obj.oProducto.idProducto)
                cmd.Parameters.AddWithValue("@cantidad", obj.cantidad)
                cmd.Parameters.AddWithValue("@Precio", obj.Precio)
                cmd.Parameters.AddWithValue("@Referencia", obj.Referencia)
                cmd.Parameters.AddWithValue("@idusuario", obj.oUsuario.idusuario)
                cmd.CommandType = CommandType.StoredProcedure
                oconexion.Open()
                cmd.ExecuteNonQuery()
            End Using

        Catch ex As Exception
            Respuesta = True
            Mensaje = ex.Message
        End Try

        Return Respuesta
    End Function

    Public Function Eliminar(ByVal obj As carritoadministracion, <Out> ByRef Mensaje As String) As Boolean
        Dim Respuesta As Boolean = False
        Mensaje = String.Empty

        Try

            Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                Dim sqlCadena As String = "DELETE FROM carritoadministracion WHERE referencia=@Referencia "
                Dim cmd As SqlCommand = New SqlCommand("sp_carritoadmin_borrar", oconexion)
                cmd.Parameters.AddWithValue("@Referencia", obj.Referencia)
                cmd.CommandType = CommandType.StoredProcedure
                oconexion.Open()
                cmd.ExecuteNonQuery()
            End Using

        Catch ex As Exception
            Respuesta = True
            Mensaje = ex.Message
        End Try

        Return Respuesta

    End Function
End Class
