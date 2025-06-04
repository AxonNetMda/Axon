Imports System.Data.SqlClient
Imports System.Runtime.InteropServices
Imports System.Text
Imports CapaDatos.Conexion
Imports capaEntidad

Public Class CD_CarritoTienda
    Public lista As List(Of Carritotienda) = New List(Of Carritotienda)
    Dim oconexion As New SqlConnection(conectar.Cadena)
    Public Function Listar(ByVal Referencia As String) As List(Of Carritotienda)
        Dim lista As List(Of Carritotienda) = New List(Of Carritotienda)()
        Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)

            Try
                Dim query As StringBuilder = New StringBuilder()
                query.AppendLine("Select Case dbo.CarritoAdministracion.idCarrito, dbo.CarritoAdministracion.idSucursal, dbo.Sucursales.Nombre As SucursalNombre, dbo.CarritoAdministracion.idCliente,")
                query.AppendLine("dbo.CarritoAdministracion.fecha, dbo.CarritoAdministracion.idProducto, dbo.producto.Nombre As ProductoNombre, dbo.producto.CodigoBarras, dbo.CarritoAdministracion.Cantidad,")
                query.AppendLine("dbo.CarritoAdministracion.Precio, dbo.CarritoAdministracion.Referencia, dbo.usuario.Nombre as UsuarioNombre, concat(dbo.cliente.apellido, ' ', dbo.cliente.Nombre)  as NombreCompleto ")
                query.AppendLine("From dbo.CarritoTienda")
                query.AppendLine("INNER Join dbo.cliente ON dbo.CarritoTienda.idcliente = dbo.cliente.idcliente ")
                query.AppendLine("INNER Join dbo.Sucursales ON dbo.CarritoTienda.idSucursal = dbo.Sucursales.idSucursal ")
                query.AppendLine("INNER Join dbo.producto ON dbo.CarritoTienda.idProducto = dbo.producto.idProducto ")
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
                        lista.Add(New Carritotienda() With {
                            .IdCarrito = Convert.ToInt32(dr("idCarrito")),
                            .oSucursal = New sucursal() With {.idSucursal = dr("idSucursal"), .Nombre = dr("SucursdalNombre")},
                            .oCliente = New cliente() With {.idcliente = dr("idCliente"), .apellido = dr("NombreCompleto")},
                            .oProducto = New producto() With {.idProducto = dr("idProducto"), .Nombre = dr("ProductoNombre")},
                            .Fecha = dr("Fecha"),
                            .Cantidad = dr("Cantidad"),
                            .Precio = dr("Precio"),
                            .Referencia = dr("Referencia")
                        })

                    End While

                End Using

            Catch ex As Exception
                lista = New List(Of Carritotienda)()
            End Try
        End Using

        Return lista
    End Function

    Public Function Registrar(ByVal obj As Carritotienda, <Out> ByRef Mensaje As String) As Integer
        Dim Generado As Integer = 0
        Mensaje = String.Empty

        Try
            Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                Dim sqlCadena As String = "INSERT INTO carritotienda (idSucursal, idCliente, Fecha, IdProducto, cantidad, precio, referencia) 
                                           values (@idSucursal, @idCliente, @Fecha, @IdProducto, @Cantidad, @Precio, @Referencia)"
                Dim cmd As SqlCommand = New SqlCommand(sqlCadena, oconexion)
                cmd.Parameters.AddWithValue("@IdSucursal", obj.oSucursal.idSucursal)
                cmd.Parameters.AddWithValue("@IdCliente", obj.oCliente.idcliente)
                cmd.Parameters.AddWithValue("@Fecha", obj.Fecha)
                cmd.Parameters.AddWithValue("@idProducto", obj.oProducto.idProducto)
                cmd.Parameters.AddWithValue("@cantidad", obj.Cantidad)
                cmd.Parameters.AddWithValue("@Precio", obj.Precio)
                cmd.Parameters.AddWithValue("@Referencia", obj.Referencia)
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

    Public Function Editar(ByVal obj As Carritotienda, <Out> ByRef Mensaje As String) As Boolean
        Dim Respuesta As Boolean = False
        Mensaje = String.Empty

        Try

            Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                Dim sqlCadena As String = "UPDATE Carritotienda SET 
                                           idSucursal=@idSucursal, idCliente=@idCliente, Fecha= @Fecha, IdProducto= @IdProducto, cantidad= @Cantidad, 
                                           precio=@Precio, referencia=@referencia WHERE Referencia=@Referencia"

                Dim cmd As SqlCommand = New SqlCommand(sqlCadena, oconexion)
                cmd.Parameters.AddWithValue("@idCarrito", obj.IdCarrito)
                cmd.Parameters.AddWithValue("@IdSucursal", obj.oSucursal.idSucursal)
                cmd.Parameters.AddWithValue("@IdCliente", obj.oCliente.idcliente)
                cmd.Parameters.AddWithValue("@Fecha", obj.Fecha)
                cmd.Parameters.AddWithValue("@idProducto", obj.oProducto.idProducto)
                cmd.Parameters.AddWithValue("@cantidad", obj.Cantidad)
                cmd.Parameters.AddWithValue("@Precio", obj.Precio)
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

    Public Function Eliminar(ByVal obj As Carritotienda, <Out> ByRef Mensaje As String) As Boolean
        Dim Respuesta As Boolean = False
        Mensaje = String.Empty

        Try

            Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                Dim sqlCadena As String = "DELETE FROM Carritotienda WHERE referencia=@Referencia "
                Dim cmd As SqlCommand = New SqlCommand(sqlCadena, oconexion)
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
