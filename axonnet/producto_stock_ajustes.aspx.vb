Imports System.Data.SqlClient
Imports CapaDatos
Imports CapaDatos.Conexion
Imports capaEntidad
Imports capaNegocio
Public Class producto_stock_ajustes
    Inherits System.Web.UI.Page
    Public Property nidProducto As Integer
    Public Property nIdSucursal As Integer
    Public Property sBuscar As String
    Dim dblPrecioCosto As Decimal = 0
    Dim dblPrecioVenta As Decimal = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("sNombreUsuario") = "" Then
            Response.Redirect("~/login.aspx")
        End If

        If Not IsPostBack Then
            nidProducto = Request.QueryString("Idproducto")
            nIdSucursal = Request.QueryString("idSucursal")
            sBuscar = Request.QueryString("sBuscar")
            Try
                Dim mensaje As String = ""
                Dim lstproducto As List(Of producto) = New CD_Producto().ListarEnSucursal(nIdSucursal, nidProducto)

                If lstproducto.Count > 0 Then
                    lblIdSucursal.Text = lstproducto(0).stk_idsucursal.idSucursal
                    lblSucursal.Text = lstproducto(0).stk_idsucursal.Nombre
                    lblIdProducto.Text = lstproducto(0).idProducto
                    lblProductoNombre.Text = lstproducto(0).Nombre
                    lblStockActual.Text = lstproducto(0).stk_StockActual
                    txtStockCritico.Text = lstproducto(0).stk_Critico
                    txtCantidad.Text = 0
                    dblPrecioCosto = lstproducto(0).PrecioCosto
                    dblPrecioVenta = lstproducto(0).PrecioVenta
                Else
                    Dim lstsuc As List(Of sucursal) = New CN_Sucursal().Listar(nIdSucursal)
                    lblIdSucursal.Text = lstsuc(0).idSucursal
                    lblSucursal.Text = lstsuc(0).Nombre
                    Dim lstprod As List(Of producto) = New CD_Producto().Listar(nidProducto)
                    lblIdProducto.Text = nidProducto
                    lblProductoNombre.Text = lstprod(0).Nombre
                    lblStockActual.Text = 0
                    txtStockCritico.Text = 0
                    txtCantidad.Text = 0
                    dblPrecioCosto = lstprod(0).PrecioCosto
                    dblPrecioVenta = lstprod(0).PrecioVenta
                End If

            Catch ex As Exception
                Dim script As String
                lblMensajeAtencion.Text = ex.Message
                script = "$(function() { showModalAtencion(); }); "
                ScriptManager.RegisterStartupScript(Me, Page.GetType(), "editmdl", script, True)
            End Try

        Else

        End If


    End Sub

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        Dim idgenerado As Integer = 0
        Dim mensaje As String = ""
        '      Public Property idProducto As Integer
        'Public Property idSucursal As Integer
        'Public Property fecFecha As Date
        'Public Property Detalle As String
        'Public Property oProveedor As proveedor
        'Public Property CantidadEntrada As Decimal
        'Public Property PrecioEntrada As Decimal
        'Public Property oCliente As cliente
        'Public Property CantidadSalida As Decimal
        'Public Property PrecioSalida As Decimal
        'Public Property FechaActualizado As Date
        'Public Property oUsuario As usuario
        'Public Property TipoOperacion As Integer
        'Public Property Referencia As String
        Dim inv_cantidadEntrada As Decimal = If(Val(txtCantidad.Text) > 0, txtCantidad.Text, 0)
        Dim inv_cantidadsalida As Decimal = If(Val(txtCantidad.Text) < 0, txtCantidad.Text, 0)
        Dim nTipoOperacion As Integer = If(inv_cantidadEntrada > 0, 1, 2)
        Try
            Dim objproducto As producto = New producto() With {
            .idProducto = lblIdProducto.Text,
            .stk_idsucursal = New sucursal() With {.idSucursal = lblIdSucursal.Text},
            .stk_StockActual = txtCantidad.Text,
            .stk_Critico = txtStockCritico.Text,
            .inv_Detalle = "AJUSTE DE STOCK",
            .oProveedor = New proveedor() With {.idProveedor = 0, .RazonSocial = ""},
            .inv_CantidadEntrada = inv_cantidadEntrada,
            .inv_PrecioEntrada = dblPrecioCosto,
            .oCliente = New cliente() With {.idcliente = 0, .nombre = ""},
            .inv_CantidadSalida = inv_cantidadsalida,
            .inv_PrecioSalida = dblPrecioVenta,
            .Inv_fecFecha = Date.Today,
            .inv_Referencia = Date.Now.Ticks,
            .oUsuario = New usuario() With {.idusuario = Session("idUsuario")},
            .inv_TipoOperacion = New tipomovimiento() With {.idTipoMovimiento = nTipoOperacion}
          }


            idgenerado = New CD_Producto().ActualizarStock(objproducto, mensaje)
            If idgenerado <> 0 Then
                Dim script As String
                lblMensajeAtencion.Text = mensaje
                script = "$(function() { showModalAtencion(); }); "
                ScriptManager.RegisterStartupScript(Me, Page.GetType(), "editmdl", script, True)
            Else
                Response.Redirect("producto_stock_listado.aspx?nSucursal=" & Convert.ToInt32(lblIdSucursal.Text) & "&sBuscar=" & Request.QueryString("sBuscar"))
            End If
        Catch ex As Exception

        End Try

    End Sub
End Class