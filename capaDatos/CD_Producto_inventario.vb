Imports System.Data.SqlClient
Imports capaEntidad
Imports System.Text
Imports System.Runtime.InteropServices
Imports CapaDatos.Conexion
Public Class CD_Producto_inventario
    Public lista As List(Of producto_inventario) = New List(Of producto_inventario)
    Dim oconexion As New SqlConnection(conectar.Cadena)
    Public Function ListarPorSucursal(ByVal nProducto As Integer, ByVal nsucursal As Integer) As List(Of producto_inventario)
        Dim lista As List(Of producto_inventario) = New List(Of producto_inventario)()
        Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)

            Try
                Dim query As String
                query = "SELECT dbo.producto_inventario.idProducto, dbo.producto.Nombre AS NombreProducto,dbo.producto_inventario.idSucursal,
                    dbo.Sucursales.Nombre AS NombreSucursal, dbo.producto_inventario.fecFecha, dbo.producto_inventario.Detalle,
                    dbo.producto_inventario.idProveedor, dbo.producto_inventario.CantidadEntrada, dbo.producto_inventario.PrecioEntrada,
                    dbo.producto_inventario.idCliente, dbo.producto_inventario.CantidadSalida, dbo.producto_inventario.PrecioSalida,
                    dbo.producto_inventario.Referencia, dbo.producto_inventario.FechaActualizado, dbo.producto_inventario.idUsuario,
                    dbo.usuario.Nombre AS NombreUsuario, dbo.producto_inventario.TipoOperacion, dbo.inventario_tipoOperacion.nombre AS NombreOperacion,
                    dbo.producto_inventario.idInventario, COALESCE(dbo.proveedor.RazonSocial,'-') AS NombreProveedor,
                    COALESCE(dbo.cliente.nombre, '-') as NombreCliente 
                    FROM dbo.producto_inventario 
                    INNER JOIN dbo.producto ON dbo.producto_inventario.idProducto = dbo.producto.idProducto
                    INNER JOIN dbo.Sucursales ON dbo.producto_inventario.idSucursal = dbo.Sucursales.idSucursal
                    INNER JOIN dbo.usuario ON dbo.producto_inventario.idUsuario = dbo.usuario.idUsuario
                    INNER JOIN dbo.inventario_tipoOperacion ON dbo.producto_inventario.TipoOperacion = dbo.inventario_tipoOperacion.identificador
                    LEFT JOIN dbo.proveedor ON dbo.producto_inventario.idProveedor = dbo.proveedor.idProveedor
                    left JOIN dbo.cliente ON dbo.producto_inventario.idCliente = dbo.cliente.idcliente
                    WHERE
                    dbo.producto_inventario.idSucursal = " & nsucursal & " AND
                    dbo.producto_inventario.idProducto = " & nProducto & "
                    ORDER BY
                    dbo.producto_inventario.fecFecha ASC"



                Dim cmd As SqlCommand = New SqlCommand(query, oconexion)
                cmd.CommandType = CommandType.Text
                oconexion.Open()


                Using dr As SqlDataReader = cmd.ExecuteReader()

                    While dr.Read()
                        lista.Add(New producto_inventario() With {
                            .idSucursal = dr("idSucural"),
                            .idProducto = dr("idProducto"),
                            .fecFecha = dr("fecFecha"),
                            .oProveedor = New proveedor() With {.idProveedor = dr("idproveedor"), .RazonSocial = dr("NombreProveedor")},
                            .CantidadEntrada = dr("CantidadEntrada"),
                            .PrecioEntrada = dr("PrecioEntrada"),
                            .oCliente = New cliente() With {.idcliente = dr("idCliente"), .nombre = dr("NombreCliente")},
                            .CantidadSalida = dr("CantidadSalida"),
                            .PrecioSalida = dr("PrecioSalida"),
                            .Detalle = dr("Detalle"),
                            .FechaActualizado = dr("FechaActualizado"),
                            .oUsuario = New usuario() With {.idusuario = dr("idUsuario"), .Nombre = dr("NombreUsuario")},
                            .Referencia = dr("referencia"),
                            .TipoOperacion = dr("TipoOperacion")
                        })

                    End While

                End Using

            Catch ex As Exception
                lista = New List(Of producto_inventario)()
            End Try
        End Using

        Return lista
    End Function

    Public Function Registrar(ByVal obj As categoria, <Out> ByRef Mensaje As String) As Integer
        Dim idGenerado As Integer = 0
        Mensaje = String.Empty

        Try

            Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                Dim cmd As SqlCommand = New SqlCommand("sp_categoria_add", oconexion)
                cmd.Parameters.AddWithValue("@Nombre", obj.Nombre)
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

    Public Function Editar(ByVal obj As categoria, <Out> ByRef Mensaje As String) As Boolean
        Dim Respuesta As Boolean = False
        Mensaje = String.Empty

        Try

            Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                Dim cmd As SqlCommand = New SqlCommand("sp_categoria_edit", oconexion)
                cmd.Parameters.AddWithValue("@Idcategoria", obj.idCategoria)
                cmd.Parameters.AddWithValue("@Nombre", obj.Nombre)
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

    Public Function Eliminar(ByVal obj As categoria, <Out> ByRef Mensaje As String) As Boolean
        Dim Respuesta As Boolean = False
        Mensaje = String.Empty

        Try

            Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                Dim cmd As SqlCommand = New SqlCommand("sp_categoria_borrar", oconexion)
                cmd.Parameters.AddWithValue("@idcategoria", obj.idCategoria)
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
