Imports System.Data.SqlClient
Imports CapaDatos
Imports CapaDatos.Conexion
Imports capaEntidad
Imports capaNegocio
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class Ventas_lista
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
            filtro = " WHERE  dbo.ventas.idsucursal=1 AND NombreCliente LIKE '%" & txtBuscar.Text & "%'"
        Else
            filtro = " WHERE ventas.idsucursal=1"

        End If
        Dim query As String = "SELECT dbo.ventas.idVenta, dbo.ventas.idSucursal, dbo.ventas.idTipoventa, dbo.ventas.Fecha, dbo.ventas.NetoGravado,
                            dbo.ventas.AlicuotaIva0, dbo.ventas.AlicuotaIVA25, dbo.ventas.AlicuotaIVA5, dbo.ventas.AlicuotaIVA105, dbo.ventas.AlicuotaIVA21,
                            dbo.ventas.AlicuotaIVA27, dbo.ventas.PercepcionIngresosBrutos, dbo.ventas.ImpuestosInternos, dbo.ventas.PercepcionIVA,
                            dbo.ventas.NoGravado, dbo.ventas.ImporteTotal, dbo.ventas.DescuentoPorc, dbo.ventas.idUsuario, dbo.Sucursales.Nombre AS NombreSucursal,
                            dbo.ventas.Estado, dbo.ventas.idcliente, dbo.ventas.PuntoVenta, dbo.ventas.NumeroComprobante, dbo.ventas.IdTipoComprobante,
                            dbo.ventas.Letra, dbo.ventas.Referencia, dbo.ventas.idFormaPago, dbo.FormaDePago.Nombre AS NombreFormaPago, dbo.tipoventa.Nombre AS NombreTipoCompra,
                            dbo.cliente.Apellido + ' ' + dbo.cliente.nombre as NombreCliente ,
                            dbo.ventas.Letra + '-' + RIGHT(REPLICATE('0', 4) + CAST(dbo.ventas.PuntoVenta AS VARCHAR), 4) + '-' + RIGHT(REPLICATE('0', 8) + CAST(dbo.ventas.Numerocomprobante AS VARCHAR), 8) AS ComprobanteFormateado
                            FROM dbo.ventas
                            INNER JOIN dbo.Sucursales ON dbo.ventas.idSucursal = dbo.Sucursales.idSucursal
                            INNER JOIN dbo.FormaDePago ON dbo.ventas.idFormaPago = dbo.FormaDePago.idFormaPago
                            INNER JOIN dbo.tipoventa ON dbo.ventas.idTipoVenta= dbo.tipoventa.idTipoventa
                            INNER JOIN dbo.cliente ON dbo.ventas.idcliente = dbo.cliente.idCliente
                            " & filtro & " ORDER BY Fecha, NombreCliente"
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
                gvDetalle.Columns.Add(New BoundField() With {.DataField = "IdVenta", .HeaderText = "ID"})
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
                lblMensajeAtencion.Text = "No tiene autorizacion para anular este comprobante de venta"
                script = "$(function() { showModalAtencion(); }); "
                ScriptManager.RegisterStartupScript(Me, Page.GetType(), "mdlConsulta", script, True)
            Else
                Dim script As String

                lblMensajeAtencion.Text = "Quiere Anular este conprobante de Venta. Para ello debe hacer una nota de credito"
                script = "$(function() { showModalAtencion(); }); "
                ScriptManager.RegisterStartupScript(Me, Page.GetType(), "mdlConsulta", script, True)
            End If
        Else
            CargarCabecera(nSuc)
            Exit Sub
        End If
    End Sub


    ' Método para cargar los datos de la tabla ReposicionDetalle
    Private Function CargarFacturaDetalle(ByVal idReposicion As Integer) As DataTable
        Dim query As String = "Select dbo.ventas_detalle.idventa, dbo.ventas_detalle.idProducto, dbo.ventas_detalle.cantidad, ventas_detalle.Precio, ventas_detalle.AlicuotaIVA,
                               dbo.producto.Nombre as NombreProducto, (cantidad * dbo.ventas_detalle.precio) as SubTotal  FROM dbo.ventas_detalle 
                               INNER JOIN dbo.producto ON dbo.ventas_detalle.idProducto = dbo.producto.idProducto WHERE ventas_detalle.idVenta = @idVenta"
        Dim dt As New DataTable()

        Using connection As New SqlConnection(conectar.Cadena)
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@idVenta", idReposicion)
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
            filtro = " WHERE  ventas.idsucursal=" & nSuc & " And  dbo.proveedor.RazonSocial Like '%" & txtBuscar.Text & "%' or dbo.ventas.fecha LIKE '%" & txtBuscar.Text & "%'  AND  dbo.ventas.Estado=1"
        Else
            filtro = " "
        End If
        scomando = "SELECT dbo.ventas.idVenta, dbo.ventas.idSucursal, dbo.ventas.idTipoventa, dbo.ventas.Fecha, dbo.ventas.NetoGravado,
                            dbo.ventas.AlicuotaIva0, dbo.ventas.AlicuotaIVA25, dbo.ventas.AlicuotaIVA5, dbo.ventas.AlicuotaIVA105, dbo.ventas.AlicuotaIVA21,
                            dbo.ventas.AlicuotaIVA27, dbo.ventas.PercepcionIngresosBrutos, dbo.ventas.ImpuestosInternos, dbo.ventas.PercepcionIVA,
                            dbo.ventas.NoGravado, dbo.ventas.ImporteTotal, dbo.ventas.DescuentoPorc, dbo.ventas.idUsuario, dbo.Sucursales.Nombre AS NombreSucursal,
                            dbo.ventas.Estado, dbo.ventas.idproveedor, dbo.ventas.PuntoVenta, dbo.ventas.NumeroComprobante, dbo.ventas.IdTipoComprobante,
                            dbo.ventas.Letra, dbo.ventas.Referencia, dbo.ventas.idFormaPago, dbo.FormaDePago.Nombre AS NombreFormaPago, dbo.tipoventa.Nombre AS NombreTipoCompra,
                            dbo.clientes.Apellido + ' ' + dbo.clientes.nombre as NombreCliente ,
                            dbo.ventas.Letra + '-' + RIGHT(REPLICATE('0', 4) + CAST(dbo.ventas.PuntoVenta AS VARCHAR), 4) + '-' + RIGHT(REPLICATE('0', 8) + CAST(dbo.ventas.Numerocomprobante AS VARCHAR), 8) AS ComprobanteFormateado
                            FROM dbo.ventas
                            INNER JOIN dbo.Sucursales ON dbo.ventas.idSucursal = dbo.Sucursales.idSucursal
                            INNER JOIN dbo.FormaDePago ON dbo.ventas.idFormaPago = dbo.FormaDePago.idFormaPago
                            INNER JOIN dbo.tipoventa ON dbo.ventas.idTipoCompra = dbo.tipocompra.idTipoCompra
                            INNER JOIN dbo.proveedor ON dbo.ventas.idproveedor = dbo.proveedor.idProveedor
                            " & filtro & " ORDER BY Fecha, NombreCliente"

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
            For i = 0 To dt.Rows.Count - 1
                MaestroMovimientosdetalle.Rows.Add(cboSucursal.SelectedValue, dt.Rows(i).Item(0).Text, dt.Rows(i).Item(2).Text, 0, dt.Rows(i).Item(3).Text, 0, 0, dt.Rows(i).Item(5).Text)
                detalles.Add(New maestromovimiento_detalle With {
               .idSucursal = cboSucursal.SelectedValue,
               .IdProducto = Convert.ToInt32(dt.Rows(i).Item(0).Text),
               .Cantidad = Convert.ToInt32(dt.Rows(i).Item(2).Text),
               .stockcritico = 0,
               .PrecioUnitario = Convert.ToDecimal(dt.Rows(i).Item(3).Text),
               .AlicuotaIVA = 0,
               .PrecioVenta = 0,
               .Referencia = dt.Rows(i).Item(5).text
   })
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
                .letra = "REP",
                .NetoGravado = 0,
                .AlicuotaIVA0 = 0,
                .AlicuotaIVA25 = 0,
                .AlicuotaIVA5 = 0,
                .AlicuotaIVA105 = 0,
                .AlicuotaIVA21 = 0,
                .AlicuotaIVA27 = 0,
                .tbDetalleRepo = MaestroMovimientosdetalle,
                .Comprobante = "REP-" & nSuc.ToString().PadLeft(4, "0"c) & "-" & nNumerpComprobante.ToString().PadLeft(8, "0"c),
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
            If estado = "False" Then
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