Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Threading
Imports CapaDatos
Imports CapaDatos.Conexion
Imports capaEntidad
Imports capaNegocio
Public Class Ventas_Emision
    Inherits System.Web.UI.Page
    Public Property nSuc As Integer
    Public Property nUsuario As Integer
    Dim auxNetoGravado As Double
    Dim auxAlicuotaIVA0 As Double
    Dim auxAlicuotaIVA25 As Double
    Dim auxAlicuotaIVA5 As Double
    Dim auxAlicuotaIVA105 As Double
    Dim auxAlicuotaIVA21 As Double
    Dim auxAlicuotaIVA27 As Double
    Dim auxImpInternos As Double
    Dim auxPercIIBB As Double
    Dim auxPercIVA As Double
    Dim auxNoGravado As Double
    Dim auxDescuento As Double
    Dim Mensaje As String = ""
    Private total As Decimal = 0
    Private dtDetalleFactura As DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim valorCelda As Integer
        If Session("sNombreUsuario") Is Nothing Then
            Response.Redirect("~/login.aspx")
        End If

        If Not IsPostBack Then
            ' Cambiar la configuración regional a es-AR (Argentina) solo para esta página
            Thread.CurrentThread.CurrentCulture = New CultureInfo("es-AR")
            Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-AR")
            auxNetoGravado = 0
            auxAlicuotaIVA0 = 0
            auxAlicuotaIVA25 = 0
            auxAlicuotaIVA5 = 0
            auxAlicuotaIVA105 = 0
            auxAlicuotaIVA21 = 0
            auxAlicuotaIVA27 = 0
            auxImpInternos = 0
            auxPercIIBB = 0
            auxPercIVA = 0
            auxNoGravado = 0
            auxDescuento = 0
            Dim lisacliente As List(Of cliente)
            Dim lisaresponsable As List(Of tipocomprobantes)
            nSuc = Session("IdSucursal")
            nUsuario = Session("Idusuario")
            lisacliente = New CD_Cliente().Listar(0)
            For item = 0 To lisacliente.Count - 1
                cboCliente.Items.Insert(item, New ListItem(lisacliente(item).apellido & " " & lisacliente(item).nombre, lisacliente(item).idcliente))
            Next
            cboCliente.SelectedIndex = 0

            lisaresponsable = New CD_TipoComprobantes().Listar()
            For item = 0 To lisaresponsable.Count - 1
                cboTipoComprobante.Items.Insert(item, New ListItem(lisaresponsable(item).strNombre, lisaresponsable(item).idTipoComprobante))
            Next
            cboTipoComprobante.SelectedIndex = 0
            For Each item As ListItem In cboTipoComprobante.Items
                If Integer.TryParse(lisaresponsable(0).idTipoComprobante, valorCelda) Then
                    ' Compara el valor de la celda con el valor del ítem del ComboBox.
                    If item.Value = valorCelda.ToString() Then
                        lblLetra.Text = lisaresponsable(0).Letra
                        Exit For
                    End If
                End If
            Next

            Dim listatipocompra As List(Of tipoventa) = New CD_TipoVenta().Listar()
            For item = 0 To listatipocompra.Count - 1
                cboTipoCompra.Items.Insert(item, New ListItem(listatipocompra(item).Nombre, listatipocompra(item).idTipoVenta))
            Next
            cboTipoCompra.SelectedIndex = 0


            cboCondicion.Items.Insert(0, New ListItem("CUENTA CORRIENTE", 1))
            cboCondicion.Items.Insert(1, New ListItem("CONTADO", 0))
            cboCondicion.DataTextField = "CONTADO"
            cboCondicion.DataValueField = 0
            cboCondicion.SelectedIndex = 0
            txtFecha.Text = DateTime.Now.ToString("yyyy-MM-dd")

            cboComprobante.Items.Insert(0, New ListItem("FACTURA", 1))
            cboComprobante.Items.Insert(1, New ListItem("COMPROBANTE", 0))
            cboComprobante.DataTextField = "FACTURA"
            cboComprobante.DataValueField = 1
            cboComprobante.SelectedIndex = 0

            cboDetalle.Items.Insert(0, New ListItem("SIN ITEMS", 0))
            cboDetalle.Items.Insert(1, New ListItem("CON ITEMS", 1))
            cboDetalle.DataTextField = "SIN ITEMS"
            cboDetalle.DataValueField = 0
            cboDetalle.SelectedIndex = 0

            cboConIVaSinIVa.Items.Insert(0, New ListItem("SIN IVA", 0))
            cboConIVaSinIVa.Items.Insert(1, New ListItem("CON IVA", 1))
            cboConIVaSinIVa.DataTextField = "SIN IVA"
            cboConIVaSinIVa.DataValueField = 0
            cboConIVaSinIVa.SelectedIndex = 0
            PanelDatos.Visible = False

            ddlProductos.Items.Insert(0, New ListItem("Seleccione un producto", "0"))
            txtCantidad.Text = 1
            txtPrecioUnitario.Text = "0,00"
            txtAlicuotaIVA.Text = "0,00"
            txtDescripcion.Value = ""
            txtidproducto.Value = 0

            cboDetalle.SelectedValue = 1
            cboDetalle.Enabled = False
            If cboDetalle.SelectedValue = 0 Then
                PanelDatos.Visible = False
            Else
                PanelDatos.Visible = True
            End If

            Dim lisaproducto As List(Of producto) = New CD_Producto().ListadoPreciosVenta(0)
            For item = 0 To lisaproducto.Count - 1
                ddlProductos.Items.Insert(item, New ListItem(lisaproducto(item).Nombre, lisaproducto(item).idProducto))
            Next
            ddlProductos.SelectedIndex = 0
            ObtenerProductosDesdeBaseDeDatos()
            CrearDataTableDetalle()
        Else
            Dim lisaresponsable As List(Of tipocomprobantes)
            Dim i As Integer = 0
            lisaresponsable = New CD_TipoComprobantes().Listar()
            For Each item As ListItem In cboTipoComprobante.Items
                ' Compara el valor de la celda con el valor del ítem del ComboBox.
                If item.Value = cboTipoComprobante.SelectedValue Then
                    lblLetra.Text = lisaresponsable(i).Letra.ToString()
                    Exit For
                End If
                i += 1
            Next



            If cboComprobante.SelectedValue = 0 Then
                cboTipoComprobante.Enabled = False
                lblLetra.Text = "T"
                cboConIVaSinIVa.SelectedIndex = 0
                cboConIVaSinIVa.Enabled = False
            Else
                cboTipoComprobante.Enabled = True
                cboConIVaSinIVa.Enabled = True
            End If
            cboDetalle.SelectedValue = 1
            cboDetalle.Enabled = False
            If cboDetalle.SelectedValue = 0 Then
                PanelDatos.Visible = False
            Else
                PanelDatos.Visible = True
            End If
            CalcularTotalFactura()

        End If

    End Sub
    Private Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click
        Dim script As String
        If lblLetra.Text = "T" Then
            If CDbl(txtNetoGravado.Text) = CDbl(lblTotal.Text) Then
                lblMEnsajeCompra.Text = "Desea Guardar el comprobante?"
                script = "$(function() { showModalGuardar(); }); "
            Else
                lblMensajeAtencion.Text = "Error en la carga de items. El monto total del comprobante no es igual al monto total del detalle"
                script = "$(function() { showModalAtencion(); }); "
            End If
        Else
            If cboConIVaSinIVa.SelectedValue = 0 Then
                If CDbl(txtNetoGravado.Text) = CDbl(lblTotal.Text) Then
                    lblMEnsajeCompra.Text = "Desea Guardar el comprobante?"
                    script = "$(function() { showModalGuardar(); }); "
                Else
                    lblMensajeAtencion.Text = "Error en la carga de items. El monto total del comprobante no es igual al monto total del detalle"
                    script = "$(function() { showModalAtencion(); }); "
                End If
            Else
                If CDbl(txtImporteFactura.Text) = CDbl(lblTotal.Text) Then
                    lblMEnsajeCompra.Text = "Desea Guardar el comprobante?"
                    script = "$(function() { showModalGuardar(); }); "
                Else
                    lblMensajeAtencion.Text = "Error en la carga de items. El monto total del comprobante no es igual al monto total del detalle"
                    script = "$(function() { showModalAtencion(); }); "
                End If
            End If
        End If

        If CDbl(txtNetoGravado.Text) = CDbl(lblTotal.Text) Then
            lblMEnsajeCompra.Text = "Desea Guardar el comprobante?"
            script = "$(function() { showModalGuardar(); }); "
        Else
            lblMensajeAtencion.Text = "Error en la carga de items. El monto total del comprobante no es igual al monto total del detalle"
            script = "$(function() { showModalAtencion(); }); "
        End If
        ScriptManager.RegisterStartupScript(Me, Page.GetType(), "mdlAtencion", script, True)
    End Sub
    Private Sub CrearDataTableDetalle()
        dtDetalleFactura = New DataTable()
        dtDetalleFactura.Columns.Add("idProducto", GetType(Integer))
        dtDetalleFactura.Columns.Add("Descripcion", GetType(String))
        dtDetalleFactura.Columns.Add("Cantidad", GetType(Integer))
        dtDetalleFactura.Columns.Add("Precio", GetType(Decimal))
        dtDetalleFactura.Columns.Add("AlicuotaIva", GetType(Decimal))
        dtDetalleFactura.Columns.Add("Subtotal", GetType(Decimal))

        ' Guardar en ViewState para mantener los datos durante los postbacks
        ViewState("DetalleFactura") = dtDetalleFactura
    End Sub

    Protected Sub btnAgregarProducto_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAgregarProducto.Click
        If txtidproducto.Value = 0 Then
            Exit Sub
        End If
        Dim lista As List(Of producto) = New CD_Producto().Listar(ddlProductos.SelectedValue)
        If lista.Count > 0 Then
            Dim seguiR As Boolean = True
            Dim PRECIO As Double
            Dim Total As Double = 0
            PRECIO = CDbl(txtPrecioUnitario.Text)

            dtDetalleFactura = CType(ViewState("DetalleFactura"), DataTable)
            For i = 0 To dtDetalleFactura.Rows.Count - 1
                If ddlProductos.SelectedValue = dtDetalleFactura.Rows(i).Item("idProducto") Then
                    dtDetalleFactura.Rows(i).Item("Precio") = FormatNumber(txtPrecioUnitario.Text, 2)
                    dtDetalleFactura.Rows(i).Item("AlicuotaIVA") = txtAlicuotaIVA.Text
                    dtDetalleFactura.Rows(i).Item("Cantidad") = txtCantidad.Text
                    dtDetalleFactura.Rows(i).Item("Subtotal") = FormatNumber(CDbl(txtPrecioUnitario.Text) * Val(txtCantidad.Text), 2)
                    seguiR = False
                End If
            Next
            If Not seguiR Then
                gvDetalle.DataSource = dtDetalleFactura
                gvDetalle.DataBind()
                txtCantidad.Text = 1
                txtPrecioUnitario.Text = 0
                ddlProductos.SelectedIndex = 0
                TotalcomprobanteCargaitems()
                Exit Sub
            Else

            End If

            lblTotal.Text = FormatNumber(Total, 2)
            dtDetalleFactura = CType(ViewState("DetalleFactura"), DataTable)
            Dim producto As Integer = Convert.ToInt32(lista(0).idProducto)
            Dim Descripcion As String = lista(0).Nombre
            Dim cantidad As Integer = Convert.ToInt32(txtCantidad.Text)
            Dim precioUnitario As Decimal = Convert.ToDecimal(txtPrecioUnitario.Text)
            Dim alicuotaIVA As Decimal = lista(0).AlicuotaIVA
            Dim subtotal As Decimal = cantidad * precioUnitario

            ' Crear nueva fila y agregar los datos
            Dim dr As DataRow = dtDetalleFactura.NewRow()
            dr("idProducto") = txtidproducto.Value
            dr("Descripcion") = Descripcion
            dr("Cantidad") = cantidad
            dr("Precio") = FormatNumber(precioUnitario, 2)
            dr("AlicuotaIVA") = FormatNumber(alicuotaIVA, 2)
            dr("Subtotal") = FormatNumber(subtotal, 2)

            dtDetalleFactura.Rows.Add(dr)

            gvDetalle.DataSource = dtDetalleFactura
            gvDetalle.DataBind()
            ' Actualizar el GridView
            ViewState("DetalleFactura") = dtDetalleFactura
            'gvDetalleFactura.DataSource = dtDetalleFactura
            'gvDetalleFactura.DataBind()
            txtCantidad.Text = 1
            txtPrecioUnitario.Text = 0
            ddlProductos.SelectedIndex = 0
            TotalcomprobanteCargaitems()
        End If


        ' Obtener los valores ingresados en los controles del GridView


        ' Calcular subtotal: (Cantidad * Precio Unitario) * (1 + AlicuotaIVA / 100)

    End Sub
    Private Sub CargarProductos()
        Dim dtProductos As DataTable = ObtenerProductosDesdeBaseDeDatos()

        ' Vincular productos al DropDownList
        ddlProductos.DataSource = dtProductos
        ddlProductos.DataTextField = "Descripcion" ' Muestra la descripción
        ddlProductos.DataValueField = "idProducto" ' Valor seleccionado (código del producto)
        ddlProductos.DataBind()

        ' Añadir un elemento "Seleccione"
        ddlProductos.Items.Insert(0, New ListItem("Seleccione un producto", "0"))
    End Sub
    Protected Sub ddlProductos_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim codigoProducto As String = ddlProductos.SelectedValue
        If codigoProducto <> "0" Then
            Dim dtProducto As DataTable = ObtenerDetallesProducto(codigoProducto)

            ' Asignar el precio unitario y la alícuota de IVA a los TextBox
            If dtProducto.Rows.Count > 0 Then
                txtidproducto.Value = dtProducto.Rows(0)("idproducto").ToString()
                txtCodigo.Text = dtProducto.Rows(0)("idproducto").ToString()
                txtDescripcion.Value = dtProducto.Rows(0)("Descripcion").ToString()
                txtPrecioUnitario.Text = dtProducto.Rows(0)("Precio").ToString()
                txtAlicuotaIVA.Text = dtProducto.Rows(0)("AlicuotaIVA").ToString()
            End If
        End If
    End Sub
    Private Function ObtenerProductosDesdeBaseDeDatos() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("IdProducto")
        dt.Columns.Add("Descripcion")
        dt.Columns.Add("Precio")
        dt.Columns.Add("AlicuotaIVA")

        Dim ssql As String
        ssql = "SELECT dbo.producto.idProducto, dbo.producto.Nombre as Descripcion, dbo.preciodeventa(PrecioCosto, AlicuotaIVA,Ganancia) as Precio, dbo.producto.AlicuotaIVA                     
                FROM dbo.producto WHERE dbo.producto.estado<>0  ORDER BY dbo.producto.Nombre"
        Dim ocn As New SqlConnection(conectar.Cadena)
        Dim da As New SqlDataAdapter(ssql, ocn)
        Dim dtp As New DataTable
        da.Fill(dtp)
        For item = 0 To dtp.Rows.Count - 1
            dt.Rows.Add(dtp.Rows(item).Item("idProducto"), dtp.Rows(item).Item("Descripcion"), dtp.Rows(item).Item("Precio"), dtp.Rows(item).Item("AlicuotaIVA"))
        Next
        ' Simulación de datos de productos
        'dt.Rows.Add("001", "Producto A")
        'dt.Rows.Add("002", "Producto B")
        'dt.Rows.Add("003", "Producto C")

        Return dt
    End Function
    ' Método para obtener los detalles del producto (precio unitario y alícuota IVA) desde la base de datos (simulado)
    Private Function ObtenerDetallesProducto(codigoProducto As String) As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("IdProducto")
        dt.Columns.Add("Descripcion")
        dt.Columns.Add("Precio", GetType(Decimal))
        dt.Columns.Add("AlicuotaIVA", GetType(Decimal))
        Dim listaproducto As List(Of producto) = New CD_Producto().ListadoPreciosVenta(codigoProducto)
        If listaproducto.Count > 0 Then
            dt.Rows.Add(listaproducto(0).idProducto, listaproducto(0).Nombre, listaproducto(0).PrecioVenta, listaproducto(0).AlicuotaIVA)
        End If


        Return dt
    End Function
    Protected Sub gvDetalleFactura_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        ' Verificar si la fila es una fila de datos (DataRow)
        If e.Row.RowType = DataControlRowType.DataRow Then
            ' Obtener el valor del Subtotal de la fila actual
            Dim subtotal As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Subtotal"))

            ' Sumar el subtotal al total acumulado
            total += subtotal
        End If

        ' Verificar si es el Footer para mostrar el total
        If e.Row.RowType = DataControlRowType.Footer Then
            ' Asignar el total al Footer en la columna Subtotal
            e.Row.Cells(3).Text = "Total: " & total.ToString("N2")
            e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
        End If
    End Sub

    Private Sub gvDetalle_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvDetalle.RowCommand
        If e.CommandName = "Anular" Then
            Dim index As Integer
            index = e.CommandArgument
            Dim dt As DataTable = CType(ViewState("DetalleFactura"), DataTable)
            For i = 0 To dt.Rows.Count - 1
                If dt.Rows(i).Item("IdProducto") = gvDetalle.Rows(index).Cells(0).Text Then
                    dt.Rows(i).Delete()
                    Exit For
                End If
            Next i
            ' Almacenar el DataTable actualizado en el ViewState
            ViewState("DetalleFactura") = dt

            ' Actualizar el GridView con los datos del DataTable
            gvDetalle.DataSource = dt
            gvDetalle.DataBind()
            TotalcomprobanteCargaitems()
        ElseIf e.CommandName = "editar" Then
            Dim index As Integer
            index = e.CommandArgument
            'txtAlicuotaIVA.Text = gvCompraDetalle.Rows(index).Cells(2).Text
            txtPrecioUnitario.Text = gvDetalle.Rows(index).Cells(3).Text
            txtCantidad.Text = gvDetalle.Rows(index).Cells(2).Text
            For Each item As ListItem In ddlProductos.Items

                ' Compara el valor de la celda con el valor del ítem del ComboBox.
                If item.Value = Val(gvDetalle.Rows(index).Cells(0).Text) Then
                    ddlProductos.SelectedValue = item.Value
                    Exit For
                End If
            Next
            TotalcomprobanteCargaitems()
        End If
    End Sub
    Private Sub TotalcomprobanteCargaitems()
        lblTotal.Text = 0
        Dim Total As Double
        Dim Precio As Double
        Dim Cantidad As Integer
        For i = 0 To gvDetalle.Rows.Count - 1

            Precio = gvDetalle.Rows(i).Cells(3).Text
            Cantidad = gvDetalle.Rows(i).Cells(2).Text
            Total += Precio * Cantidad
        Next i
        lblTotal.Text = FormatNumber(Total, 2)
    End Sub

    Private Sub CalcularTotalFactura()
        txtImporteFactura.Text = FormatNumber(CDbl(txtNetoGravado.Text) + CDbl(txtAlicuota105.Text) + CDbl(txtAlicuota21.Text) + CDbl(txtPercIIBB.Text) + CDbl(txtPrecIVA.Text) + CDbl(txtImpuestosInternos.Text) + CDbl(txtNoGravado.Text) - CDbl(txtDescuento.Text), 2)
    End Sub

    Private Sub GrabarComprobante()
        'CREATE Type EDetalleVenta AS TABLE (
        ' idVenta Int,
        ' idProducto INT,
        ' Cantidad Int,
        ' PrecioUnitario Decimal,
        'AlicuotaIVA Decimal,
        ' SubTotal Decimal,
        ' Referencia varchar(50))



        Dim referecia As String = Date.Now.Ticks
        Dim nNumerpComprobante As Integer = 0
        nNumerpComprobante = txtNumeroComprobante.Text
        Try
            Dim objVenta As venta
            Dim idgenerado As Integer = 0
            Dim mensaje As String = ""

            Dim detalles As New List(Of venta_detalle)
            Dim tbldetalles As New DataTable
            tbldetalles.Columns.Add("idProducto", GetType(Integer))
            tbldetalles.Columns.Add("Cantidad", GetType(Integer))
            tbldetalles.Columns.Add("Precio", GetType(Decimal))
            tbldetalles.Columns.Add("ALicuotaIVA", GetType(Decimal))
            tbldetalles.Columns.Add("Subtotal", GetType(Decimal))
            tbldetalles.Columns.Add("Referencia", GetType(String))

            For i = 0 To gvDetalle.Rows.Count - 1
                tbldetalles.Rows.Add(gvDetalle.Rows(i).Cells(0).Text, gvDetalle.Rows(i).Cells(2).Text, gvDetalle.Rows(i).Cells(3).Text, gvDetalle.Rows(i).Cells(4).Text, gvDetalle.Rows(i).Cells(5).Text, referecia)
                detalles.Add(New venta_detalle With {
                    .idProducto = New producto() With {.idProducto = Convert.ToInt32(gvDetalle.Rows(i).Cells(0).Text)},
                    .cantidad = Convert.ToInt32(gvDetalle.Rows(i).Cells(2).Text),
                    .Precio = Convert.ToDecimal(gvDetalle.Rows(i).Cells(3).Text),
                    .AlicuotaIVA = Convert.ToDecimal(gvDetalle.Rows(i).Cells(4).Text),
                    .SubTotal = Convert.ToDecimal(gvDetalle.Rows(i).Cells(5).Text),
                    .Referencia = referecia
  })
            Next i
            Dim ntpComprobante As Integer = 0
            If lblLetra.Text <> "X" Then
                ntpComprobante = cboTipoComprobante.SelectedValue
            End If
            objVenta = New venta() With {
                .oSucursal = New sucursal() With {.idSucursal = Convert.ToInt32(Session("idSucursal"))},
                .oTipoVenta = New tipoventa() With {.idTipoVenta = cboTipoCompra.SelectedValue},
                .fecha = Date.Today,
                .oFormaPago = New formadepago() With {.idformapago = cboCondicion.SelectedValue},
                .NetoGravado = Convert.ToDecimal(txtNetoGravado.Text),
                .AlicuotaIVA0 = 0,
                .AlicuotaIVA25 = 0,
                .AlicuotaIVA5 = 0,
                .AlicuotaIVA105 = Convert.ToDecimal(txtAlicuota105.Text),
                .AlicuotaIVA21 = Convert.ToDecimal(txtAlicuota21.Text),
                .AlicuotaIVA27 = 0,
                .ImpuestosInternos = Convert.ToDecimal(txtImpuestosInternos.Text),
                .PercepcionIVA = Convert.ToDecimal(txtPrecIVA.Text),
                .PercepcionIngresosBrutos = Convert.ToDecimal(txtPercIIBB.Text),
                .NoGravado = Convert.ToDecimal(txtNoGravado.Text),
                .ImporteTotal = Convert.ToDecimal(txtImporteFactura.Text),
                .DescuentoPorc = Convert.ToDecimal(txtDescuento.Text),
                .oUsuario = New usuario() With {.idusuario = Convert.ToInt32(Session("Idusuario"))},
                .Estado = 1,
                .oCliente = New cliente() With {.idcliente = cboCliente.SelectedValue},
                .puntoVenta = txtPuntoVenta.Text,
                .Numerocomprobante = txtNumeroComprobante.Text,
                .oTipoComprobante = New tipocomprobantes() With {.idTipoComprobante = ntpComprobante},
                .Letra = lblLetra.Text,
                .Referencia = referecia,
                .oDetalle = detalles
              }



            idgenerado = New CD_Venta().Registrar(objVenta, tbldetalles, mensaje)

            If idgenerado <> 0 Then
                If mensaje <> "" Then
                    lblMensajeAtencion.Text = mensaje
                    Dim script As String
                    script = "$(function() { showModalAtencion(); }); "
                    ScriptManager.RegisterStartupScript(Me, Page.GetType(), "editmdl", script, True)
                Else
                    Response.Redirect("ventas_lista.aspx")
                End If
                'Dim script As String
                'script = "$(function() { showModalAtencion(); }); "
                'ScriptManager.RegisterStartupScript(Me, Page.GetType(), "editmdl", script, True)
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
                    Response.Redirect("ventas_lista.aspx")
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

    Private Sub BtnGrabarCompra_Click(sender As Object, e As EventArgs) Handles BtnGrabarCompra.Click
        GrabarComprobante()

    End Sub
End Class