Imports System.Data.SqlClient
Imports CapaDatos

Public Class CatalogoAdministracion
    Inherits System.Web.UI.Page
    Public Property nCantidadCarrito As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
            nCantidad.Value += 1
            nCantidadCarrito = nCantidad.Value

        Else
            Dim query As String = "SELECT dbo.producto.idProducto, dbo.producto.Nombre as NombreProducto, dbo.producto_Stock.StockActual, 
                            dbo.preciodeventa(producto.PrecioCosto,  producto.AlicuotaIVA, producto.Ganancia) as PrecioVenta, 
                            dbo.producto.RutaImagen, dbo.producto.nombreFoto1, dbo.producto.EsDestacado, dbo.producto.EsOferta
                            FROM dbo.producto
                            INNER JOIN dbo.producto_Stock ON dbo.producto.idProducto = dbo.producto_Stock.idProducto
                            WHERE dbo.producto_Stock.idSucursal = 1 AND dbo.producto.MostrarCatalogo = 1 ORDER BY dbo.producto.Nombre"

            Dim dt As New DataTable()
            Using conn As New SqlConnection(Conexion.conectar.Cadena)
                Using cmd As New SqlCommand(query, conn)
                    conn.Open()
                    Dim da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using

            ' Generar los controles dinámicamente en la página
            For Each row As DataRow In dt.Rows
                Dim btnInfo As New Button()
                btnInfo.Text = "INFO.."
                btnInfo.CssClass = "btn btn-outline-dark btn-sm"
                btnInfo.PostBackUrl = "Catalogo_Ficha.aspx?IdProducto=" & Server.UrlEncode(row("NombreProducto").ToString())
            Next
            nCantidadCarrito = 0
        End If
    End Sub

    Protected Sub btnAgregarCarrito_Click(sender As Object, e As EventArgs) Handles btnAgregarCarrito.Click
        'nCantidad.Value += 1
    End Sub
End Class