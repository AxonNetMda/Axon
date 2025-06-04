Imports CapaDatos
Imports System.Data
Imports capaEntidad
Imports System.Text
Imports System.Runtime.InteropServices
Imports System.Data.SqlClient
Imports CapaDatos.Conexion
Imports Microsoft.SqlServer
Imports System.Drawing
Imports System.IO
Imports System.Web.UI.WebControls

Public Class CD_Producto
    Public lista As List(Of producto) = New List(Of producto)
    Dim oconexion As New SqlConnection(conectar.Cadena)
    Public Function Listar(ByVal nproducto As Integer) As List(Of producto)
        Dim lista As List(Of producto) = New List(Of producto)()
        Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)

            Try
                Dim query As StringBuilder = New StringBuilder
                query.AppendLine("SELECT  producto.idProducto, producto.Nombre, producto.codigobarras, producto.idCategoria, producto.idSubcategoria, producto.idMarca, producto.idProveedor, ")
                query.AppendLine("producto.PrecioCosto, producto.AlicuotaIVA, producto.Ganancia, producto.redondeo, producto.rutaimagen, producto.nombrefoto1, producto.nombrefoto2, ")
                query.AppendLine("producto.nombrefoto3, producto.nombrefoto4, producto.nombrefoto5, producto.nombrefoto6, producto.nombrefoto7, producto.nombrefoto8, producto.nombrefoto9, ")
                query.AppendLine("producto.nombrefoto10, producto.nombrefoto11,  producto.nombrefoto12, producto.FechaCreacion, producto.FechaUltimaActualizacion, dbo.preciodeventa2(preciocosto,alicuotaiva,ganancia) as precioVenta, ")
                query.AppendLine("producto.FechaUltimaCompra, producto.Estado, producto_categoria.Nombre AS NombreCategoria, producto_subCategoria.Nombre AS NombreSubCategoria, producto.stockcritico, ")
                query.AppendLine("proveedor.RazonSocial as NombreProveedor, producto_marca.Nombre AS NombreMarca, producto.Notas, producto.mostrarcatalogo, producto.EsDestacado, producto.EsOferta FROM producto ")
                query.AppendLine("LEFT JOIN producto_categoria ON producto.idCategoria = producto_categoria.idCategoria ")
                query.AppendLine("LEFT JOIN producto_subCategoria ON producto.idSubcategoria = producto_subCategoria.idSubCategoria ")
                query.AppendLine("LEFT JOIN producto_marca ON producto.idMarca = producto_marca.idMarca ")
                query.AppendLine("LEFT JOIN proveedor ON producto.idProveedor = proveedor.idProveedor")
                If nproducto = 0 Then

                Else
                    query.AppendLine(" WHERE  producto.idProducto =" & nproducto)
                End If
                query.AppendLine(" ORDER BY producto.Nombre Asc")
                Dim cmd As SqlCommand = New SqlCommand(query.ToString(), oconexion)
                cmd.CommandType = CommandType.Text
                oconexion.Open()

                Using dr As SqlDataReader = cmd.ExecuteReader()

                    While dr.Read()
                        lista.Add(New producto() With {
                            .idProducto = Convert.ToInt32(dr("idproducto")),
                            .CodigoBarras = dr("CodigoBarras"),
                            .Nombre = Convert.ToString(dr("Nombre")),
                            .oCategoria = New categoria() With {.idCategoria = Convert.ToInt32(dr("IdCategoria")), .Nombre = Convert.ToString(dr("NombreCategoria"))},
                            .oSubCategoria = New subcategoria() With {.idSubCategoria = Convert.ToInt32(dr("idSubCategoria")), .Nombre = Convert.ToString(dr("NombreSubcategoria"))},
                            .oMarca = New marca() With {.idMarca = Convert.ToInt32(dr("idMarca")), .Nombre = Convert.ToString(dr("NombreMarca"))},
                            .oProveedor = New proveedor() With {.idProveedor = Convert.ToInt32(dr("idproveedor")), .RazonSocial = Convert.ToString(dr("NombreProveedor"))},
                            .PrecioCosto = Convert.ToDecimal(dr("PrecioCosto")),
                            .AlicuotaIVA = Convert.ToDecimal(dr("AlicuotaIVA")),
                            .Ganancia = Convert.ToDecimal(dr("ganancia")),
                            .Redondeo = Convert.ToDecimal(dr("Redondeo")),
                            .StockCritico = Convert.ToInt32(dr("StockCritico")),
                            .rutaImagen = dr("rutaimagen").ToString(),
                            .NombreFoto1 = Convert.ToString(dr("Nombrefoto1")),
                            .NombreFoto2 = Convert.ToString(dr("Nombrefoto2")),
                            .NombreFoto3 = Convert.ToString(dr("Nombrefoto3")),
                            .NombreFoto4 = Convert.ToString(dr("Nombrefoto4")),
                            .NombreFoto5 = Convert.ToString(dr("Nombrefoto5")),
                            .NombreFoto6 = Convert.ToString(dr("Nombrefoto6")),
                            .NombreFoto7 = Convert.ToString(dr("Nombrefoto7")),
                            .NombreFoto8 = Convert.ToString(dr("Nombrefoto8")),
                            .NombreFoto9 = Convert.ToString(dr("Nombrefoto9")),
                            .NombreFoto10 = Convert.ToString(dr("Nombrefoto10")),
                            .NombreFoto11 = Convert.ToString(dr("Nombrefoto11")),
                            .NombreFoto12 = Convert.ToString(dr("Nombrefoto12")),
                            .Notas = Convert.ToString(dr("Notas")),
                            .FechaCreacion = dr("FechaCreacion"),
                            .FechaUltimaActualizacion = CDate(dr("FechaUltimaActualizacion")),
                            .FechaUltimaCompra = IIf(IsDBNull(dr("FechaUltimaCompra")), Date.Today, dr("FechaUltimaCompra")),
                            .MostrarCatalogo = Convert.ToBoolean(dr("MostrarCatalogo")),
                            .EsDestacado = Convert.ToBoolean(dr("EsDestacado")),
                            .EsOferta = Convert.ToBoolean(dr("EsOferta")),
                            .Estado = dr("estado").ToString(),
                            .PrecioVenta = dr("PrecioVenta").ToString()
                        })
                        ',
                        '
                        '


                    End While
                End Using
                If lista.Count = 0 Then
                    lista = New List(Of producto)()
                End If
            Catch ex As Exception
                lista = New List(Of producto)()
            End Try
        End Using
        '.FechaCreacion = Convert.ToDateTime(dr("FechaCreacion")).ToShortDateString,
        '.FechaUltimaActualizacion = Convert.ToDateTime(dr("FechaUltimaActualizacion")).ToShortDateString,

        Return lista
    End Function
    Public Function ListarEnSucursal(ByVal nSuc As Integer, ByVal nproducto As Integer) As List(Of producto)
        Dim lista As List(Of producto) = New List(Of producto)()
        Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)

            Try
                Dim query As String
                If nproducto = 0 Then
                    query = "SELECT p.IDProducto, p.Nombre AS NombreProducto, p.StockCritico, p.Estado, COALESCE(st.StockActual, 0) AS StockActual, COALESCE(st.StockCritico, 0) AS StockCritico, s.Nombre  as nombreSucursal, s.idSucursal
                        FROM Producto p
                        LEFT JOIN producto_Stock st ON p.idProducto = st.idProducto AND st.idSucursal = " & nSuc & " 
					    INNER JOIN Sucursales s ON st.idSucursal=s.idSucursal                         
                        ORDER BY p.Nombre;"

                Else
                    query = "SELECT p.IDProducto, p.Nombre AS NombreProducto, p.StockCritico, p.Estado, COALESCE(st.StockActual, 0) AS StockActual, COALESCE(st.StockCritico, 0) AS StkCritico, s.Nombre as nombreSucursal, s.idSucursal 
                        FROM Producto p
                        LEFT JOIN producto_Stock st ON p.idProducto = st.idProducto AND st.idSucursal = " & nSuc & " 
					    INNER JOIN Sucursales s ON st.idSucursal=s.idSucursal 
                        where st.idProducto=" & nproducto & "     
                        ORDER BY p.Nombre;"
                End If
                Dim cmd As SqlCommand = New SqlCommand(query.ToString(), oconexion)
                cmd.CommandType = CommandType.Text
                oconexion.Open()

                Using dr As SqlDataReader = cmd.ExecuteReader()

                    While dr.Read()
                        lista.Add(New producto() With {
                            .idProducto = Convert.ToInt32(dr("idproducto")),
                            .Nombre = dr("NombreProducto"),
                            .stk_idsucursal = New sucursal() With {.idSucursal = dr("idSucursal"), .Nombre = dr("nombreSucursal")},
                            .stk_StockActual = dr("StockActual"),
                            .stk_Critico = dr("StkCritico")
                        })
                    End While
                End Using
                If lista.Count = 0 Then
                    lista = New List(Of producto)()
                End If
            Catch ex As Exception
                lista = New List(Of producto)()
            End Try
        End Using
        '.FechaCreacion = Convert.ToDateTime(dr("FechaCreacion")).ToShortDateString,
        '.FechaUltimaActualizacion = Convert.ToDateTime(dr("FechaUltimaActualizacion")).ToShortDateString,

        Return lista
    End Function
    Public Function ListadoPreciosVenta(ByVal nproducto As Integer) As List(Of producto)
        Dim lista As List(Of producto) = New List(Of producto)()
        Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)

            Try
                Dim query As String
                Dim filtro As String = ""
                If nproducto = 0 Then
                    filtro = " ORDER BY producto.nombre"
                Else
                    filtro = "Where IdProducto =" & nproducto & " ORDER BY producto.nombre"
                End If


                query = "SELECT dbo.producto.idProducto, dbo.producto.CodigoBarras, dbo.producto.PrecioCosto, dbo.producto.AlicuotaIVA, dbo.producto.Ganancia, dbo.producto.Nombre,
                         dbo.preciodeventa(dbo.producto.PrecioCosto, dbo.producto.AlicuotaIVA, dbo.producto.Ganancia) as PrecioVenta 
                          FROM dbo.producto " & filtro

                Dim cmd As SqlCommand = New SqlCommand(query.ToString(), oconexion)
                cmd.CommandType = CommandType.Text
                oconexion.Open()

                Using dr As SqlDataReader = cmd.ExecuteReader()

                    While dr.Read()
                        lista.Add(New producto() With {
                            .idProducto = Convert.ToInt32(dr("idproducto")),
                            .CodigoBarras = dr("CodigoBarras"),
                            .Nombre = Convert.ToString(dr("Nombre")),
                            .PrecioCosto = Convert.ToDecimal(dr("PrecioCosto")),
                            .AlicuotaIVA = Convert.ToDecimal(dr("AlicuotaIVA")),
                            .Ganancia = Convert.ToDecimal(dr("ganancia")),
                            .PrecioVenta = Convert.ToDecimal(dr("PrecioVenta"))
                        })
                        ',
                        '
                        '


                    End While
                End Using
                If lista.Count = 0 Then
                    lista = New List(Of producto)()
                End If
            Catch ex As Exception
                lista = New List(Of producto)()
            End Try
        End Using
        '.FechaCreacion = Convert.ToDateTime(dr("FechaCreacion")).ToShortDateString,
        '.FechaUltimaActualizacion = Convert.ToDateTime(dr("FechaUltimaActualizacion")).ToShortDateString,

        Return lista
    End Function

    Public Function Registrar(ByVal obj As producto, <Out> ByRef Mensaje As String) As Integer
        Dim idGenerado As Integer = 0
        Mensaje = String.Empty

        Try

            Using oconexion As SqlConnection = New SqlConnection(Conexion.conectar.Cadena)
                Dim cmd As SqlCommand = New SqlCommand("sp_producto_add", oconexion)
                cmd.Parameters.AddWithValue("@Nombre", obj.Nombre)
                cmd.Parameters.AddWithValue("@CodigoBarras", obj.CodigoBarras)
                cmd.Parameters.AddWithValue("@idCategoria", obj.oCategoria.idCategoria)
                cmd.Parameters.AddWithValue("@idSubcategoria", obj.oSubCategoria.idSubCategoria)
                cmd.Parameters.AddWithValue("@idMarca", obj.oMarca.idMarca)
                cmd.Parameters.AddWithValue("@idProveedor", obj.oProveedor.idProveedor)
                cmd.Parameters.AddWithValue("@PrecioCosto", obj.PrecioCosto)
                cmd.Parameters.AddWithValue("@AlicuotaIVA", obj.AlicuotaIVA)
                cmd.Parameters.AddWithValue("@Ganancia", obj.Ganancia)
                cmd.Parameters.AddWithValue("@Redondeo", obj.Redondeo)
                cmd.Parameters.AddWithValue("@rutaimagen", obj.rutaImagen)
                cmd.Parameters.AddWithValue("@nombreimagen1", obj.NombreFoto1)
                cmd.Parameters.AddWithValue("@nombreimagen2", obj.NombreFoto2)
                cmd.Parameters.AddWithValue("@nombreimagen3", obj.NombreFoto3)
                cmd.Parameters.AddWithValue("@nombreimagen4", obj.NombreFoto4)
                cmd.Parameters.AddWithValue("@nombreimagen5", obj.NombreFoto5)
                cmd.Parameters.AddWithValue("@nombreimagen6", obj.NombreFoto6)
                cmd.Parameters.AddWithValue("@nombreimagen7", obj.NombreFoto7)
                cmd.Parameters.AddWithValue("@nombreimagen8", obj.NombreFoto8)
                cmd.Parameters.AddWithValue("@nombreimagen9", obj.NombreFoto9)
                cmd.Parameters.AddWithValue("@nombreimagen10", obj.NombreFoto10)
                cmd.Parameters.AddWithValue("@nombreimagen11", obj.NombreFoto11)
                cmd.Parameters.AddWithValue("@nombreimagen12", obj.NombreFoto12)
                cmd.Parameters.AddWithValue("@fechacreacion", Date.Today)
                cmd.Parameters.AddWithValue("@fechaultimaactualizacion", Date.Today)
                cmd.Parameters.AddWithValue("@stockCritico", obj.StockCritico)
                cmd.Parameters.AddWithValue("@Notas", obj.Notas)
                cmd.Parameters.AddWithValue("@MostrarCatalogo", obj.MostrarCatalogo)
                cmd.Parameters.AddWithValue("@EsDestacado", obj.EsDestacado)
                cmd.Parameters.AddWithValue("@EsOferta", obj.EsOferta)
                cmd.Parameters.AddWithValue("@EsOutlet", obj.EsOutlet)
                cmd.Parameters.AddWithValue("@Estado", obj.Estado)
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
    Public Function ActualizarStock(ByVal obj As producto, <Out> ByRef Mensaje As String) As Integer
        Dim idGenerado As Integer = 0
        Mensaje = String.Empty

        Try
            Using oconexion As SqlConnection = New SqlConnection(Conexion.conectar.Cadena)
                Dim cmd As SqlCommand = New SqlCommand("sp_producto_Actualizar_Stock", oconexion)
                cmd.Parameters.AddWithValue("@idSucursal", obj.stk_idsucursal.idSucursal)
                cmd.Parameters.AddWithValue("@idProducto", obj.idProducto)
                cmd.Parameters.AddWithValue("@Cantidad", obj.stk_StockActual)
                cmd.Parameters.AddWithValue("@Critico", obj.stk_Critico)
                cmd.Parameters.AddWithValue("@fecFecha", obj.Inv_fecFecha)
                cmd.Parameters.AddWithValue("@Detalle", obj.inv_Detalle)
                cmd.Parameters.AddWithValue("@idProveedor", 0)
                cmd.Parameters.AddWithValue("@CantidadEntrada", obj.inv_CantidadEntrada)
                cmd.Parameters.AddWithValue("@PrecioENtrada", obj.inv_PrecioEntrada)
                cmd.Parameters.AddWithValue("@idCliente", 0)
                cmd.Parameters.AddWithValue("@CantidadSalida", obj.inv_CantidadSalida)
                cmd.Parameters.AddWithValue("@PrecioSalida", obj.inv_PrecioSalida)
                cmd.Parameters.AddWithValue("@Referencia", obj.inv_Referencia)
                cmd.Parameters.AddWithValue("@idUsuario", obj.oUsuario.idusuario)
                cmd.Parameters.AddWithValue("@TipoOperacion", obj.inv_TipoOperacion.idTipoMovimiento)
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
    Public Function Editar(ByVal obj As producto, <Out> ByRef Mensaje As String) As Boolean
        Dim Respuesta As Integer
        Mensaje = String.Empty

        Try

            Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                Dim cmd As SqlCommand = New SqlCommand("sp_producto_edit", oconexion)
                cmd.Parameters.AddWithValue("@IdProducto", obj.idProducto)
                cmd.Parameters.AddWithValue("@Nombre", obj.Nombre)
                cmd.Parameters.AddWithValue("@idCategoria", obj.oCategoria.idCategoria)
                cmd.Parameters.AddWithValue("@idSubcategoria", obj.oSubCategoria.idSubCategoria)
                cmd.Parameters.AddWithValue("@idMarca", obj.oMarca.idMarca)
                cmd.Parameters.AddWithValue("@idProveedor", obj.oProveedor.idProveedor)
                cmd.Parameters.AddWithValue("@PrecioCosto", obj.PrecioCosto)
                cmd.Parameters.AddWithValue("@AlicuotaIVA", obj.AlicuotaIVA)
                cmd.Parameters.AddWithValue("@Ganancia", obj.Ganancia)
                cmd.Parameters.AddWithValue("@Redondeo", obj.Redondeo)
                cmd.Parameters.AddWithValue("@rutaimagen", obj.rutaImagen)
                cmd.Parameters.AddWithValue("@nombreimagen1", obj.NombreFoto1)
                cmd.Parameters.AddWithValue("@nombreimagen2", obj.NombreFoto2)
                cmd.Parameters.AddWithValue("@nombreimagen3", obj.NombreFoto3)
                cmd.Parameters.AddWithValue("@nombreimagen4", obj.NombreFoto4)
                cmd.Parameters.AddWithValue("@nombreimagen5", obj.NombreFoto5)
                cmd.Parameters.AddWithValue("@nombreimagen6", obj.NombreFoto6)
                cmd.Parameters.AddWithValue("@nombreimagen7", obj.NombreFoto7)
                cmd.Parameters.AddWithValue("@nombreimagen8", obj.NombreFoto8)
                cmd.Parameters.AddWithValue("@nombreimagen9", obj.NombreFoto9)
                cmd.Parameters.AddWithValue("@nombreimagen10", obj.NombreFoto10)
                cmd.Parameters.AddWithValue("@nombreimagen11", obj.NombreFoto11)
                cmd.Parameters.AddWithValue("@nombreimagen12", obj.NombreFoto12)
                cmd.Parameters.AddWithValue("@fechacreacion", obj.FechaCreacion)
                cmd.Parameters.AddWithValue("@fechaultimaactualizacion", Date.Today)
                cmd.Parameters.AddWithValue("@stockCritico", obj.StockCritico)
                cmd.Parameters.AddWithValue("@Notas", obj.Notas)
                cmd.Parameters.AddWithValue("@MostrarCatalogo", obj.MostrarCatalogo)
                cmd.Parameters.AddWithValue("@EsDestacado", obj.EsDestacado)
                cmd.Parameters.AddWithValue("@EsOferta", obj.EsOferta)
                cmd.Parameters.AddWithValue("@EsOutlet", obj.EsOutlet)
                cmd.Parameters.AddWithValue("@Estado", obj.Estado)
                cmd.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output
                cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output
                cmd.CommandType = CommandType.StoredProcedure
                oconexion.Open()
                cmd.ExecuteNonQuery()
                Respuesta = Convert.ToInt32(cmd.Parameters("@Resultado").Value)
                Mensaje = cmd.Parameters("@Mensaje").Value.ToString()
            End Using

        Catch ex As Exception
            Respuesta = False
            Mensaje = ex.Message
        End Try

        Return Respuesta
    End Function

    Public Function Eliminar(ByVal obj As producto, <Out> ByRef Mensaje As String) As Boolean
        Dim Respuesta As Boolean = False
        Mensaje = String.Empty

        Try

            Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                Dim cmd As SqlCommand = New SqlCommand("sp_producto_borrar", oconexion)
                cmd.Parameters.AddWithValue("@idProducto", obj.idProducto)
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
    Public Function GuardarFotos(ByVal obj As producto, <Out> ByRef Mensaje As String) As Boolean
        Dim Respuesta As Integer
        Mensaje = String.Empty

        Try

            Using oconexion As SqlConnection = New SqlConnection(conectar.Cadena)
                Dim cmd As SqlCommand = New SqlCommand("sp_producto_GuardarFoto", oconexion)
                cmd.Parameters.AddWithValue("@IdProducto", obj.idProducto)
                cmd.Parameters.AddWithValue("@NombreImagen1", obj.NombreFoto1)
                cmd.Parameters.AddWithValue("@Nombreimagen2", obj.NombreFoto2)
                cmd.Parameters.AddWithValue("@nombreimagen3", obj.NombreFoto3)
                cmd.Parameters.AddWithValue("@nombreimagen4", obj.NombreFoto4)
                cmd.Parameters.AddWithValue("@nombreimagen5", obj.NombreFoto5)
                cmd.Parameters.AddWithValue("@nombreimagen6", obj.NombreFoto6)
                cmd.Parameters.AddWithValue("@nombreimagen7", obj.NombreFoto7)
                cmd.Parameters.AddWithValue("@nombreimagen8", obj.NombreFoto8)
                cmd.Parameters.AddWithValue("@nombreimagen9", obj.NombreFoto9)
                cmd.Parameters.AddWithValue("@nombreimagen10", obj.NombreFoto10)
                cmd.Parameters.AddWithValue("@nombreimagen11", obj.NombreFoto11)
                cmd.Parameters.AddWithValue("@nombreimagen12", obj.NombreFoto12)
                cmd.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output
                cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output
                cmd.CommandType = CommandType.StoredProcedure
                oconexion.Open()
                cmd.ExecuteNonQuery()
                Respuesta = Convert.ToInt32(cmd.Parameters("@Resultado").Value)
                Mensaje = cmd.Parameters("@Mensaje").Value.ToString()
            End Using

        Catch ex As Exception
            Respuesta = False
            Mensaje = ex.Message
        End Try

        Return Respuesta
    End Function

    Public Function GrabarFoto(ByVal FileUpFoto As FileUpload, ByVal nidProducto As Integer, ByVal nombreFoto As String, ByVal sCarpetaFotos As String) As String
        Dim folderPath As String = sCarpetaFotos
        Dim Etiqueta As String = ""
        'Check whether Directory (Folder) exists.
        If Not Directory.Exists(folderPath) Then
            'If Directory (Folder) does not exists. Create it.
            Directory.CreateDirectory(folderPath)
        End If
        FileUpFoto.SaveAs(folderPath & Path.GetFileName(FileUpFoto.FileName))
        Etiqueta = FileUpFoto.FileName
        Return Etiqueta

    End Function
End Class
