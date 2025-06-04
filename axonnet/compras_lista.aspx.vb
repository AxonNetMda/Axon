Imports System.Data.SqlClient
Imports CapaDatos
Imports CapaDatos.Conexion
Imports capaEntidad
Imports capaNegocio
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class compras_lista
    Inherits System.Web.UI.Page
    Public Property scomando As String
    Public Property sMensaje As String
    Public Property nUsuario As Integer
    Dim filtro As String = ""
    Dim nSuc As Integer
    Dim valorCelda As Integer
    Dim listrepo As List(Of sucursal)
    Dim FilaIndex As Integer
    ' Obtener la fila seleccionada
    Dim selectedFila As GridViewRow
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim valorCelda As Integer
            Dim listrepo As List(Of sucursal)
            nSuc = Session("IdSucursal")
            nUsuario = Session("Idusuario")
            listrepo = New CN_Sucursal().Listar(0)
            For item = 0 To listrepo.Count - 1
                cboSucursal.Items.Insert(item, New ListItem(listrepo(item).Nombre, listrepo(item).idSucursal))
            Next
            cboSucursal.Enabled = False
            cboSucursal.SelectedIndex = 0
            For Each item As ListItem In cboSucursal.Items
                If Integer.TryParse(listrepo(0).idSucursal, valorCelda) Then
                    ' Compara el valor de la celda con el valor del ítem del ComboBox.
                    If item.Value = nSuc Then
                        cboSucursal.SelectedValue = item.Value
                        Exit For
                    End If
                End If
            Next
            If nSuc = 1 Then
                btnReponueva.Visible = True

            Else
                btnReponueva.Visible = False
            End If
            CargarCabecera(nSuc)

        Else

        End If
    End Sub

    ' Método para cargar la lista de pedidos desde la base de datos
    Private Sub CargarCabecera(ByVal nSuc As Integer)
        If Trim(txtBuscar.Text) <> "" Then
            filtro = " WHERE  dbo.compras.idsucursal=1 AND dbo.proveedor.RazonSocial LIKE '%" & txtBuscar.Text & "%'"
        Else
            filtro = " WHERE compras.idsucursal=1"

        End If
        Dim query As String = "SELECT dbo.compras.idCompra, dbo.compras.idSucursal, dbo.compras.idTipoCompra, dbo.compras.Fecha, dbo.compras.NetoGravado,
                            dbo.compras.AlicuotaIva0, dbo.compras.AlicuotaIVA25, dbo.compras.AlicuotaIVA5, dbo.compras.AlicuotaIVA105, dbo.compras.AlicuotaIVA21,
                            dbo.compras.AlicuotaIVA27, dbo.compras.PercepcionIngresosBrutos, dbo.compras.ImpuestosInternos, dbo.compras.PercepcionIVA,
                            dbo.compras.NoGravado, dbo.compras.ImporteTotal, dbo.compras.DescuentoPorc, dbo.compras.idUsuario, dbo.Sucursales.Nombre AS NombreSucursal,
                            dbo.compras.Estado, dbo.compras.idproveedor, dbo.compras.PuntoVenta, dbo.compras.NumeroComprobante, dbo.compras.IdTipoComprobante,
                            dbo.compras.Letra, dbo.compras.Referencia, dbo.compras.idFormaPago, dbo.FormaDePago.Nombre AS NombreFormaPago, dbo.tipocompra.Nombre AS NombreTipoCompra,
                            dbo.proveedor.RazonSocial ,
                            dbo.compras.Letra + '-' + RIGHT(REPLICATE('0', 4) + CAST(dbo.compras.PuntoVenta AS VARCHAR), 4) + '-' + RIGHT(REPLICATE('0', 8) + CAST(dbo.compras.Numerocomprobante AS VARCHAR), 8) AS ComprobanteFormateado
                            FROM dbo.compras
                            INNER JOIN dbo.Sucursales ON dbo.compras.idSucursal = dbo.Sucursales.idSucursal
                            INNER JOIN dbo.FormaDePago ON dbo.compras.idFormaPago = dbo.FormaDePago.idFormaPago
                            INNER JOIN dbo.tipocompra ON dbo.compras.idTipoCompra = dbo.tipocompra.idTipoCompra
                            INNER JOIN dbo.proveedor ON dbo.compras.idproveedor = dbo.proveedor.idProveedor
                            " & filtro & " ORDER BY Fecha, RazonSocial"
        Dim dt As New DataTable()

        Using connection As New SqlConnection(conectar.Cadena)
            Using adapter As New SqlDataAdapter(query, connection)
                adapter.Fill(dt)
            End Using
        End Using

        gvCabecera.DataSource = dt
        gvCabecera.PageSize = cboMostrar.SelectedValue
        gvCabecera.DataBind()

    End Sub


    Protected Sub gvCabecera_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If nSuc = 0 Then
            nSuc = cboSucursal.SelectedValue
        End If
        CargarCabecera(nSuc)
        If e.CommandArgument = "" Then

            Exit Sub
        End If
        If e.CommandName = "mostrar" Then
            ' Obtener el IdReposicion del CommandArgument

            Dim idReposicion As Integer = Convert.ToInt32(e.CommandArgument)

            ' Obtener la fila desde la cual se realizó el comando
            Dim row As GridViewRow = CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)
            Dim index As Integer = row.RowIndex
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)

            ' Obtener la fila seleccionada
            Dim fila As GridViewRow = gvCabecera.Rows(index)

            ' Encontrar el botón en esa fila
            Dim btnToggle As Button = CType(fila.FindControl("btnToggleDetails"), Button)

            ' Cambiar el texto del botón
            If btnToggle.Text = "+" Then
                btnToggle.Text = "-"
            Else
                btnToggle.Text = "+"
            End If
            ' Verificar si ya existe una fila de detalles debajo de la fila seleccionada
            If index + 1 < gvCabecera.Rows.Count AndAlso
               gvCabecera.Rows(index + 1).Cells(0).ColumnSpan = gvCabecera.Columns.Count Then
                ' Si ya existe, removerla (ocultar detalles)
                gvCabecera.Controls(0).Controls.RemoveAt(index + 1)
                CType(e.CommandSource, Button).Text = "+"
                'CType(e.CommandSource, Button).CssClass = "fas fa-edit"
            Else
                ' Cargar los detalles de la reposición
                Dim dtDetalle As DataTable = CargarFacturaDetalle(idReposicion)
                If CType(e.CommandSource, Button).Text = "-" Then
                    'If CType(e.CommandSource, Button).CssClass = "fas fa-edit" Then
                    CargarCabecera(nSuc)
                    Exit Sub
                Else
                    'CType(e.CommandSource, Button).Text = "fas fa-cancel"
                    CType(e.CommandSource, Button).Text = "-"
                End If
                ' Crear un GridView para mostrar los detalles
                Dim gvDetalle As New GridView()
                gvDetalle.ID = "GridViewDetalle"
                gvDetalle.DataSource = dtDetalle
                gvDetalle.AutoGenerateColumns = False
                gvDetalle.CssClass = "table table-bordered bg-white"

                ' Definir las columnas para el GridView de detalles
                gvDetalle.Columns.Add(New BoundField() With {.DataField = "IdCompra", .HeaderText = "ID"})
                gvDetalle.Columns.Add(New BoundField() With {.DataField = "idProducto", .HeaderText = "Codigo"})
                gvDetalle.Columns.Add(New BoundField() With {.DataField = "Cantidad", .HeaderText = "Cantidad"})
                gvDetalle.Columns.Add(New BoundField() With {.DataField = "NombreProducto", .HeaderText = "Nombre Producto"})
                gvDetalle.Columns.Add(New BoundField() With {.DataField = "AlicuotaIVA", .HeaderText = "IVA"})
                gvDetalle.Columns.Add(New BoundField() With {.DataField = "Precio", .HeaderText = "Precio", .DataFormatString = "{0:C2}"})
                gvDetalle.Columns.Add(New BoundField() With {.DataField = "Subtotal", .HeaderText = "Subtotal", .DataFormatString = "{0:C2}"})
                gvDetalle.Columns(0).HeaderStyle.Width = 50
                gvDetalle.Columns(0).HeaderStyle.HorizontalAlign = HorizontalAlign.Right
                gvDetalle.Columns(0).ItemStyle.HorizontalAlign = HorizontalAlign.Right
                gvDetalle.Columns(1).HeaderStyle.Width = 50
                gvDetalle.Columns(1).ItemStyle.HorizontalAlign = HorizontalAlign.Right
                gvDetalle.Columns(2).HeaderStyle.Width = 50
                gvDetalle.Columns(2).ItemStyle.HorizontalAlign = HorizontalAlign.Right
                'gvDetalle.Columns(3).HeaderStyle.Width = 250
                gvDetalle.Columns(3).ItemStyle.HorizontalAlign = HorizontalAlign.Left
                gvDetalle.Columns(4).HeaderStyle.Width = 100
                gvDetalle.Columns(4).ItemStyle.HorizontalAlign = HorizontalAlign.Right
                gvDetalle.Columns(5).HeaderStyle.Width = 100
                gvDetalle.Columns(5).ItemStyle.HorizontalAlign = HorizontalAlign.Right
                gvDetalle.Columns(6).HeaderStyle.Width = 100
                gvDetalle.Columns(6).ItemStyle.HorizontalAlign = HorizontalAlign.Right
                gvDetalle.HeaderStyle.BackColor() = Drawing.Color.Gold
                gvDetalle.RowStyle.BackColor = Drawing.Color.Cornsilk
                gvDetalle.DataBind()

                ' Crear una nueva fila en la tabla GridView
                Dim newRow As New GridViewRow(index + 1, index + 1, DataControlRowType.DataRow, DataControlRowState.Normal)
                Dim cell As New TableCell()
                cell.ColumnSpan = gvCabecera.Columns.Count
                cell.Controls.Add(gvDetalle)
                newRow.Cells.Add(cell)

                ' Insertar la nueva fila de detalles debajo de la fila seleccionada

                gvCabecera.Controls(0).Controls.AddAt(index + 2, newRow)
                CType(e.CommandSource, Button).CssClass = "btn btn-success fas fa-plus"
                'CType(e.CommandSource, Button).Text = "fas fa-cancel"
                CType(e.CommandSource, Button).Text = "-"
            End If
        ElseIf e.CommandName = "Imprimir" Then
            Dim idReposicion As Integer = Convert.ToInt32(e.CommandArgument)
            'Dim script As String
            ' Dim idReposicion As Integer = Convert.ToInt32(e.CommandArgument)
            'Response.Redirect("Reposiciones_impresion1.aspx?idReposicion=" & idReposicion, False)
            'Imprimir(idReposicion)
            'lblMensajeAtencion.Text = "Imprimir"
            'script = "window.open(\'Reposiciones_impresion1.aspx?id=" & idReposicion & "\', \'_blank\'); return false;"
            'script = "$(function() { showModalReposicion(); }); "
            'ScriptManager.RegisterStartupScript(Me, Page.GetType(), "mdlConsulta", script, True)
        ElseIf e.CommandName = "Anular" Then
            If nSuc <> 1 Then
                Dim script As String
                lblMensajeAtencion.Text = "No tiene autorizacion para anular esta reposicion"
                script = "$(function() { showModalAtencion(); }); "
                ScriptManager.RegisterStartupScript(Me, Page.GetType(), "mdlConsulta", script, True)
            Else
                Dim script As String
                lblMensajeAtencion.Text = "Quiere Anular esta reposicion"
                script = "$(function() { showModalAtencion(); }); "
                ScriptManager.RegisterStartupScript(Me, Page.GetType(), "mdlConsulta", script, True)
            End If
            If e.CommandName = "Imprimir" Then
                Dim idReposicion As Integer = Convert.ToInt32(e.CommandArgument)

                'Dim idReposicion As Integer = Convert.ToInt32(e.CommandArgument)
                'Imprimir(idReposicion)
                'Dim script As String
                'lblMensajeAtencion.Text = "Quiere Anular esta reposicion"
                'script = "$(function() { showModalReposicion(); }); "
                'ScriptManager.RegisterStartupScript(Me, Page.GetType(), "mdlConsulta", script, True)
            End If
        Else
            CargarCabecera(nSuc)
            Exit Sub
        End If
    End Sub


    ' Método para cargar los datos de la tabla ReposicionDetalle
    Private Function CargarFacturaDetalle(ByVal idReposicion As Integer) As DataTable
        Dim query As String = "Select dbo.compras_detalle.idCompra, dbo.compras_detalle.idProducto, dbo.compras_detalle.cantidad, compras_detalle.Precio, compras_detalle.AlicuotaIVA,
                               dbo.producto.Nombre as NombreProducto, (cantidad * dbo.compras_detalle.precio) as SubTotal  FROM dbo.compras_detalle 
                               INNER JOIN dbo.producto ON dbo.compras_detalle.idProducto = dbo.producto.idProducto WHERE compras_detalle.idCompra = @idCompra"
        Dim dt As New DataTable()

        Using connection As New SqlConnection(conectar.Cadena)
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@idCompra", idReposicion)
                Using adapter As New SqlDataAdapter(command)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using

        Return dt
    End Function
    Private Sub txtBuscar_TextChanged(sender As Object, e As EventArgs) Handles txtBuscar.TextChanged
        nSuc = cboSucursal.SelectedValue
        If Trim(txtBuscar.Text) <> "" Then
            If nSuc = 1 Then
                filtro = " WHERE  compras.idsucursal=1 and  dbo.proveedor.RazonSocial LIKE '%" & txtBuscar.Text & "%' or dbo.compras.fecha LIKE '%" & txtBuscar.Text & "%' AND  dbo.compras.Estado=1"
            Else
                filtro = " WHERE  compras.idsucursal=" & nSuc & " And  dbo.proveedor.RazonSocial Like '%" & txtBuscar.Text & "%' or dbo.compras.fecha LIKE '%" & txtBuscar.Text & "%'  AND  dbo.compras.Estado=1"
            End If
        Else
            If nSuc = 1 Then
                filtro = " WHERE compras.idsucursal=1  AND  dbo.compras.Estado=1"
            Else
                filtro = " WHERE compras.idsucursal=" & nSuc & " AND  dbo.compras.Estado=1"
            End If
        End If
        scomando = "SELECT dbo.compras.idCompra, dbo.compras.idSucursal, dbo.compras.idTipoCompra, dbo.compras.Fecha, dbo.compras.NetoGravado,
                            dbo.compras.AlicuotaIva0, dbo.compras.AlicuotaIVA25, dbo.compras.AlicuotaIVA5, dbo.compras.AlicuotaIVA105, dbo.compras.AlicuotaIVA21,
                            dbo.compras.AlicuotaIVA27, dbo.compras.PercepcionIngresosBrutos, dbo.compras.ImpuestosInternos, dbo.compras.PercepcionIVA,
                            dbo.compras.NoGravado, dbo.compras.ImporteTotal, dbo.compras.DescuentoPorc, dbo.compras.idUsuario, dbo.Sucursales.Nombre AS NombreSucursal,
                            dbo.compras.Estado, dbo.compras.idproveedor, dbo.compras.PuntoVenta, dbo.compras.NumeroComprobante, dbo.compras.IdTipoComprobante,
                            dbo.compras.Letra, dbo.compras.Referencia, dbo.compras.idFormaPago, dbo.FormaDePago.Nombre AS NombreFormaPago, dbo.tipocompra.Nombre AS NombreTipoCompra,
                            dbo.proveedor.RazonSocial ,
                            dbo.compras.Letra + '-' + RIGHT(REPLICATE('0', 4) + CAST(dbo.compras.PuntoVenta AS VARCHAR), 4) + '-' + RIGHT(REPLICATE('0', 8) + CAST(dbo.compras.Numerocomprobante AS VARCHAR), 8) AS ComprobanteFormateado
                            FROM dbo.compras
                            INNER JOIN dbo.Sucursales ON dbo.compras.idSucursal = dbo.Sucursales.idSucursal
                            INNER JOIN dbo.FormaDePago ON dbo.compras.idFormaPago = dbo.FormaDePago.idFormaPago
                            INNER JOIN dbo.tipocompra ON dbo.compras.idTipoCompra = dbo.tipocompra.idTipoCompra
                            INNER JOIN dbo.proveedor ON dbo.compras.idproveedor = dbo.proveedor.idProveedor " & filtro & " ORDER BY Fecha, dbo.proveedor.RazonSocial"

        Dim dt As New DataTable()

        Using connection As New SqlConnection(conectar.Cadena)
            Using adapter As New SqlDataAdapter(scomando, connection)
                adapter.Fill(dt)
            End Using
        End Using

        gvCabecera.DataSource = dt
        gvCabecera.PageSize = cboMostrar.SelectedValue
        gvCabecera.DataBind()
    End Sub

    Protected Sub gvCabecera_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        ' Cambiar el índice de la página del GridView
        gvCabecera.PageIndex = e.NewPageIndex
        If nSuc = 0 Then
            nSuc = cboSucursal.SelectedValue
        End If
        CargarCabecera(nSuc)
        ' Vuelve a enlazar los datos al GridView
        'BindGridView()
    End Sub

    Private Sub AnularRepo(ByVal idRepo As Integer, ByVal dt As DataTable)
        Dim referecia As String = Date.Now.Ticks
        Dim nNumerpComprobante As Integer = 0
        Try
            Dim objMovi As maestromovimiento
            Dim idgenerado As Integer = 0
            Dim mensaje As String = ""

            Dim detalles As New List(Of maestromovimiento_detalle)
            Dim MaestroMovimientosdetalle As New DataTable
            MaestroMovimientosdetalle.Columns.Add("idSucursal", GetType(Integer))
            MaestroMovimientosdetalle.Columns.Add("idProducto", GetType(Integer))
            MaestroMovimientosdetalle.Columns.Add("Cantidad", GetType(Integer))
            MaestroMovimientosdetalle.Columns.Add("StockCritico", GetType(Integer))
            MaestroMovimientosdetalle.Columns.Add("PrecioUnitario", GetType(Decimal))
            MaestroMovimientosdetalle.Columns.Add("ALicuotaIVA", GetType(Decimal))
            MaestroMovimientosdetalle.Columns.Add("PrecioVetna", GetType(Decimal))
            MaestroMovimientosdetalle.Columns.Add("Referencia", GetType(String))


            Dim tbDetalleInventario As New DataTable
            tbDetalleInventario.Columns.Add("idDeSucursal", GetType(Integer))
            tbDetalleInventario.Columns.Add("idASucursdal", GetType(Integer))
            tbDetalleInventario.Columns.Add("idProducto", GetType(Integer))
            tbDetalleInventario.Columns.Add("FecFecha", GetType(Date))
            tbDetalleInventario.Columns.Add("Detalle", GetType(String))
            tbDetalleInventario.Columns.Add("idProveedor", GetType(Integer))
            tbDetalleInventario.Columns.Add("CantidadEntrada", GetType(Integer))
            tbDetalleInventario.Columns.Add("PrecioEntrada", GetType(Decimal))
            tbDetalleInventario.Columns.Add("idCliente", GetType(Integer))
            tbDetalleInventario.Columns.Add("CantidadSalida", GetType(Integer))
            tbDetalleInventario.Columns.Add("PrecioSalida", GetType(Decimal))
            tbDetalleInventario.Columns.Add("FechaActualizacion", GetType(Date))
            tbDetalleInventario.Columns.Add("IdUsuario", GetType(Integer))
            tbDetalleInventario.Columns.Add("TipoOperacion", GetType(Integer))
            tbDetalleInventario.Columns.Add("Referencia", GetType(String))

            For i = 0 To dt.Rows.Count - 1
                MaestroMovimientosdetalle.Rows.Add(cboSucursal.SelectedValue, dt.Rows(i).Item(0).Text, dt.Rows(i).Item(2).Text, 0, dt.Rows(i).Item(3).Text, 0, 0, dt.Rows(i).Item(5).Text)

            Next i

            objMovi = New maestromovimiento() With {
                .idMovimiento = 0,
                .oDeSucursal = New sucursal() With {.idSucursal = 1},
                .oASucursal = New sucursal() With {.idSucursal = cboSucursal.SelectedValue},
                .Fecha = Date.Today,
                .oCliente = New cliente() With {.idcliente = 0},
                .oProveedor = New proveedor() With {.idProveedor = 0},
                .oUsuario = New usuario() With {.idusuario = 1},
                .PuntoVenta = 0,
                .oTipoMovimiento = New tipomovimiento() With {.idTipoMovimiento = 3},
                .oTipoComprobante = New tipocomprobantes() With {.idTipoComprobante = 0},
                .Numerocomprobante = nNumerpComprobante,
                .letra = "CPA",
                .NetoGravado = 0,
                .AlicuotaIVA0 = 0,
                .AlicuotaIVA25 = 0,
                .AlicuotaIVA5 = 0,
                .AlicuotaIVA105 = 0,
                .AlicuotaIVA21 = 0,
                .AlicuotaIVA27 = 0,
                .tbtDetalleInventario = MaestroMovimientosdetalle,
                .Comprobante = "CPA-" & nSuc.ToString().PadLeft(4, "0"c) & "-" & nNumerpComprobante.ToString().PadLeft(8, "0"c),
                .ImpuestosInternos = 0,
                .PercepcionIngresosBrutos = 0,
                .NoGravado = 0,
                .ImporteTotal = 0,
                .Referencia = referecia,
                .Notas = ""
              }


            idgenerado = New CD_MaestroMovimiento().Registrar(objMovi, MaestroMovimientosdetalle, mensaje)

            If idgenerado <> 0 Then
                Dim script As String
                script = "$(function() { showModalAtencion(); }); "
                ScriptManager.RegisterStartupScript(Me, Page.GetType(), "editmdl", script, True)
            Else
                'idgenerado = New CD_MaestroMovimiento().ActualiarStockRepo(MaestroMovimientosdetalle, mensaje)
                'If idgenerado <> 0 Then

                'Else
                If mensaje <> "" Then
                    lblMensajeAtencion.Text = mensaje
                    Dim script As String
                    script = "$(function() { showModalAtencion(); }); "
                    ScriptManager.RegisterStartupScript(Me, Page.GetType(), "editmdl", script, True)
                Else
                    Response.Redirect("repisiciones_emision.aspx")
                End If

                'End If
            End If


        Catch ex As Exception
            Dim script As String
            lblMensajeAtencion.Text = ex.Message
            script = "$(function() { showModalAtencion(); }); "
            ScriptManager.RegisterStartupScript(Me, Page.GetType(), "editmdl", script, True)
        End Try



    End Sub

    Private Sub gvCabecera_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvCabecera.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblEstado As Label = CType(e.Row.FindControl("lblEstado"), Label)
            Dim estado As String = DataBinder.Eval(e.Row.DataItem, "Estado").ToString()

            ' Evaluar el estado y cambiar el color del texto
            If estado = "True" Then
                lblEstado.Text = "ANULADA"
                lblEstado.ForeColor = System.Drawing.Color.Red
            Else
                lblEstado.Text = "APROBADA"
                lblEstado.ForeColor = System.Drawing.Color.Black
            End If

            ' Encontrar los botones en la fila actual
            Dim btnToggleDetails As Button = CType(e.Row.FindControl("btnToggleDetails"), Button)
            Dim btnAnular As Button = CType(e.Row.FindControl("btnAnular"), Button)
            Dim btnPrint As Button = CType(e.Row.FindControl("btnPrint"), Button)

            ' Evaluar el estado y deshabilitar los botones si es necesario
            If estado = "False" Then
                'btnToggleDetails.Visible = False
                btnAnular.Visible = False
                'btnPrint.Visible = False
            End If
        End If
    End Sub

    Private Sub cboMostrar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboMostrar.SelectedIndexChanged
        If IsPostBack Then
            CargarCabecera(cboSucursal.SelectedValue)
        End If
    End Sub


End Class
