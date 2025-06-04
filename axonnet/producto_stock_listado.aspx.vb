Imports System.Data.SqlClient
Imports CapaDatos
Imports CapaDatos.Conexion
Imports capaEntidad
Imports capaNegocio

Public Class producto_stock_listado
    Inherits System.Web.UI.Page
    Public Property scomando As String
    Public Property sMensaje As String
    Public Property nProducto As Integer
    Public Property sBuscar As String
    Public Property nSuc As Integer
    Public Property pSuc As Integer = 0

    Dim filtro As String = ""
    Public Property aTitulo As String
    Dim valorCelda As Integer
    Dim listsuc As List(Of sucursal)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("sNombreUsuario") = "" Then
        '    Response.Redirect("~/login.aspx")
        'End If


        If IsPostBack Then
            'listsuc = New CN_Sucursal().Listar(0)
            'pSuc = Request.QueryString("nSucursal")
            sBuscar = Request.QueryString("sBuscar")
            nSuc = cboSucursal.SelectedValue


            If Trim(txtBuscar.Text) <> "" Then

                filtro = " WHERE dbo.producto.Nombre LIKE '%" & txtBuscar.Text & "%' and dbo.producto_stock.idsucursal=" & nSuc
            Else
                filtro = " WHERE  dbo.producto_stock.idsucursal=" & nSuc
            End If
            scomando = "SELECT dbo.producto_Stock.idStock_Producto, dbo.producto_Stock.idProducto, dbo.producto.Nombre as NombreProducto, dbo.producto_Stock.idSucursal,
                        dbo.Sucursales.Nombre, COALESCE(dbo.producto_stock.StockCritico, 0) AS StockCritico, COALESCE(dbo.producto_stock.StockActual, 0) AS StockActual,
                        dbo.producto_Stock.FechaActualizacion, dbo.producto_Stock.Preciolista, dbo.producto_Stock.PrecioTarjeta 
                        FROM dbo.producto_Stock
                        INNER JOIN dbo.producto ON dbo.producto_Stock.idProducto = dbo.producto.idProducto
                        INNER JOIN dbo.Sucursales ON dbo.producto_Stock.idSucursal = dbo.sucursales.idsucursal " & filtro & " ORDER BY dbo.producto.Nombre"

        Else
            If (nSuc = 0) And (Request.QueryString("nSucursal") = Nothing) Then
                nSuc = Session("idSucursal")
            End If
            If Request.QueryString("nSucursal") = Nothing Then
            Else
                If Request.QueryString("nSucursal") <> nSuc Then
                    nSuc = Request.QueryString("nSucursal")
                End If
            End If



            'cboAjuste.Items.Insert(0, "SUMAR")
            'cboAjuste.Items.Insert(1, "RESTAR")
            listsuc = New CN_Sucursal().Listar(0)
                For item = 0 To listsuc.Count - 1
                    cboSucursal.Items.Insert(item, New ListItem(listsuc(item).Nombre, listsuc(item).idSucursal))
                Next
                cboSucursal.SelectedIndex = 0
            For Each item As ListItem In cboSucursal.Items
                If Integer.TryParse(listsuc(0).idSucursal, valorCelda) Then
                    ' Compara el valor de la celda con el valor del ítem del ComboBox.
                    If item.Value = nSuc Then
                        cboSucursal.SelectedValue = item.Value
                        Exit For
                    End If
                End If
            Next
            If Trim(txtBuscar.Text) <> "" Then

                filtro = " WHERE dbo.producto.Nombre LIKE '%" & txtBuscar.Text & "%' and dbo.producto_stock.idsucursal=" & cboSucursal.SelectedValue
            Else
                filtro = " WHERE  dbo.producto_stock.idsucursal=" & cboSucursal.SelectedValue
            End If
            'scomando = "SELECT dbo.producto.idProducto, dbo.producto.Nombre, producto.StockCritico, dbo.producto_Stock.idSucursal, dbo.producto_Stock.StockActual, dbo.producto.Estado
            '            FROM dbo.producto LEFT JOIN dbo.producto_Stock ON dbo.producto.idProducto = dbo.producto_Stock.idProducto 
            '            WHERE idsucursal=" & nSuc & " ORDER BY Nombre"
            scomando = "SELECT dbo.producto_Stock.idStock_Producto, dbo.producto_Stock.idProducto, dbo.producto.Nombre AS nombreProducto, dbo.producto_Stock.idSucursal,
                        dbo.Sucursales.Nombre, COALESCE(dbo.producto_stock.StockCritico, 0) AS StockCritico, COALESCE(dbo.producto_stock.StockActual, 0) AS StockActual,
                        dbo.producto_Stock.FechaActualizacion, dbo.producto_Stock.Preciolista, dbo.producto_Stock.PrecioTarjeta 
                        FROM dbo.producto_Stock
                        INNER JOIN dbo.producto ON dbo.producto_Stock.idProducto = dbo.producto.idProducto
                        INNER JOIN dbo.Sucursales ON dbo.producto_Stock.idSucursal = dbo.sucursales.idSucursal " & filtro & " ORDER BY dbo.producto.Nombre;"
        End If

        sqlProducto.SelectCommand = scomando
        dgvData.PageSize = cboMostrar.SelectedValue
        dgvData.DataBind()

    End Sub
    Private Sub dgvData_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvData.RowCommand
        Dim indice As Integer
        Dim nIdProducto As Integer
        Dim script As String = ""
        If e.CommandName = "Ajuste" Then

            indice = Convert.ToInt32(e.CommandArgument)
            nIdProducto = dgvData.Rows(indice).Cells(0).Text
            'lblMDLSucursal.Text = cboSucursal.SelectedItem.Text
            'lblMDLProductoNombre.Text = dgvData.Rows(indice).Cells(1).Text
            'lblMDLStock.Text = dgvData.Rows(indice).Cells(3).Text
            'txtMDLStockCritico.Text = dgvData.Rows(indice).Cells(2).Text
            'txtMDLCantidad.Text = 0

            'script = "$(function() { showModalAjuste(); }); "
            'ScriptManager.RegisterStartupScript(Me, Page.GetType(), "editmdl", script, True)
            Response.Redirect("producto_stock_ajustes.aspx?idProducto=" & Convert.ToInt32(dgvData.Rows(indice).Cells(0).Text) & "&idSucursal=" & cboSucursal.SelectedValue & "&sBuscar=" & txtBuscar.Text)
        ElseIf e.CommandName = "Inventario" Then

            indice = Convert.ToInt32(e.CommandArgument)
            nIdProducto = dgvData.Rows(indice).Cells(0).Text
            Response.Redirect("producto_inventario.aspx?idProducto=" & Convert.ToInt32(dgvData.Rows(indice).Cells(0).Text) & "&idSucursal=" & cboSucursal.SelectedValue & "&DesdeDonde=STK")

        End If
    End Sub
    Protected Sub dgvData_RowCreated(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim stockActual As Integer = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "StockActual"))
            Dim stockCritico As Integer = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "StockCritico"))
            If stockActual <= 0 Then
                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#fc9686") ' Rojo claro
                e.Row.ForeColor = System.Drawing.Color.White ' Blanco
            ElseIf stockCritico > stockActual Then
                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffc107") ' Amarillo
                e.Row.ForeColor = System.Drawing.Color.Black ' Blanco
            Else
                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff") ' Blanco
                e.Row.ForeColor = System.Drawing.Color.Black ' Blanco
            End If

        End If
    End Sub
    Private Sub BindGridCarrito()
        If IsPostBack Then
            nSuc = cboSucursal.SelectedValue

            If Trim(txtBuscar.Text) <> "" Then

                filtro = " WHERE dbo.producto.Nombre LIKE '%" & txtBuscar.Text & "%' and dbo.producto_stock.idsucursal=" & cboSucursal.SelectedValue
            Else
                filtro = " WHERE  dbo.producto_stock.idsucursal=" & cboSucursal.SelectedValue
            End If
            scomando = "SELECT dbo.producto_Stock.idStock_Producto, dbo.producto_Stock.idProducto, dbo.producto.Nombre as nombreProducto, dbo.producto_Stock.idSucursal,
                        dbo.Sucursales.Nombre, COALESCE(dbo.producto_stock.StockCritico, 0) AS StockCritico, COALESCE(dbo.producto_stock.StockActual, 0) AS StockActual,
                        dbo.producto_Stock.FechaActualizacion, dbo.producto_Stock.Preciolista, dbo.producto_Stock.PrecioTarjeta 
                        FROM dbo.producto_Stock
                        INNER JOIN dbo.producto ON dbo.producto_Stock.idProducto = dbo.producto.idProducto
                        INNER JOIN dbo.Sucursales ON dbo.producto_Stock.idSucursal = dbo.sucursales.idsucursal " & filtro & " ORDER BY dbo.producto.Nombre;"
            sqlProducto.SelectCommand = scomando
            dgvData.PageSize = cboMostrar.SelectedValue
            dgvData.DataBind()
        End If
    End Sub

    Protected Sub btnAceptarMDLSave_Click(sender As Object, e As EventArgs)
        If IsPostBack Then
            If Val(lblMDLIdProducto.Text) <> 0 Then
                Dim cnn As New SqlConnection(conectar.Cadena)
                Dim ssql As String = "update producto_stock set stockactual+=" & Val(txtMDLCantidad.Text) & " WHERE idproducto=" & lblMDLIdProducto.Text & " AND idSucursal=" & lblMDLSucursal.Text
                Try
                    Dim cmd As New SqlCommand(ssql, cnn)
                    cnn.Open()
                    cmd.ExecuteNonQuery()
                    cnn.Close()
                    'BindGridCarrito()
                Catch ex As Exception
                    Dim script As String
                    lblMensajeAtencion.Text = ex.Message
                    script = "$(function() { showModalAtencion(); }); "
                    ScriptManager.RegisterStartupScript(Me, Page.GetType(), "editmdl", script, True)
                End Try
            Else

            End If
        End If

    End Sub
End Class