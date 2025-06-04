Imports System.Data.SqlClient
Imports CapaDatos
Imports CapaDatos.Conexion
Imports capaEntidad
Imports capaNegocio
Imports axonnet.funciones
Public Class Repisiciones_Emision
    Inherits System.Web.UI.Page
    Public Property scomando As String
    Public Property scomando2 As String
    Public Property sMensaje As String
    Dim filtro As String = ""
    Dim nSuc As Integer
    Dim nUsuario As Integer
    Dim sReferencia As String = ""
    Dim valorCelda As Integer
    Dim listrepo As List(Of sucursal)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("sNombreUsuario") Is Nothing Then
            Response.Redirect("~/login.aspx")
        End If

        If IsPostBack Then
            If Trim(txtBuscar.Text) <> "" Then
                filtro = " WHERE dbo.producto_Stock.idSucursal=1 AND producto.Nombre LIKE '%" & txtBuscar.Text & "%' or proveedor.RazonSocial LIKE '%" & txtBuscar.Text & "%' or producto_marca.Nombre LIKE '%" & txtBuscar.Text & "%'"
            Else
                filtro = " WHERE dbo.producto_Stock.idSucursal=1 "
            End If
            scomando = "SELECT dbo.producto.idProducto, dbo.producto.Nombre, dbo.preciodeventa2(PrecioCosto, AlicuotaIVA, Ganancia) as PrecioVenta,
                    dbo.producto.StockCritico, dbo.producto.Estado, dbo.producto_Stock.StockActual, dbo.producto_Stock.idSucursal,  dbo.producto_Stock.stockCritico
                    FROM dbo.producto
                    LEFT JOIN dbo.producto_Stock ON dbo.producto.idProducto = dbo.producto_Stock.idProducto " & filtro & " ORDER BY dbo.producto.Nombre"
        Else
            sReferencia = Date.Today.Ticks
            InicializarTabla()
            scomando = "SELECT dbo.producto.idProducto, dbo.producto.Nombre, dbo.preciodeventa2(PrecioCosto, AlicuotaIVA,Ganancia) as PrecioVenta,
                    dbo.producto.StockCritico, dbo.producto.Estado, dbo.producto_Stock.StockActual, dbo.producto_Stock.idSucursal,  dbo.producto_Stock.stockCritico
                    FROM dbo.producto
                    LEFT JOIN dbo.producto_Stock ON dbo.producto.idProducto = dbo.producto_Stock.idProducto
                   WHERE dbo.producto_Stock.idSucursal=1 ORDER BY producto.Nombre"
            'scomando = "SELECT p.IDProducto, p.Nombre AS Nombre, p.Estado, COALESCE(st.StockActual, 0) AS StockActual, COALESCE(st.StockCritico, 0) AS StockCritico
            '            FROM Producto p
            '            LEFT JOIN producto_Stock st ON p.idProducto = st.idProducto AND st.idSucursal =1 ORDER BY p.Nombre;"
            nSuc = Session("idSucursal")
            nUsuario = Session("Idusuario")
            cboSucursal.Items.Clear()
            listrepo = New CN_Sucursal().Listar(0)
            Dim i As Integer = 0
            For item = 0 To listrepo.Count - 1
                If listrepo(item).idSucursal <> nSuc Then
                    cboSucursal.Items.Insert(i, New ListItem(listrepo(item).Nombre, listrepo(item).idSucursal))
                    i += 1
                End If
            Next
            'cboSucursal.Enabled = False
            cboSucursal.SelectedIndex = 0
            'For Each item As ListItem In cboSucursal.Items
            '    If Integer.TryParse(listrepo(0).idSucursal, valorCelda) Then
            '        ' Compara el valor de la celda con el valor del ítem del ComboBox.
            '        If item.Value = nSuc Then
            '            cboSucursal.SelectedValue = item.Value
            '            Exit For
            '        End If
            '    End If
            'Next

        End If
        sqlProducto.SelectCommand = scomando
        dgvData.PageSize = cboMostrar.SelectedValue
        dgvData.DataBind()

    End Sub
    Private Sub dgvRepo_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvRepo.RowCommand
        Dim script As String = ""
        If e.CommandName = "Eliminar" Then
            Dim indice As Integer
            indice = Convert.ToInt32(e.CommandArgument)
            Dim dt As DataTable = CType(ViewState("Datos"), DataTable)
            For i = 0 To dt.Rows.Count - 1
                If dt.Rows(i).Item("IdProducto") = dgvRepo.Rows(indice).Cells(0).Text Then
                    dt.Rows(i).Delete()
                    Exit For
                End If
            Next i
            ' Almacenar el DataTable actualizado en el ViewState
            ViewState("Datos") = dt

            ' Actualizar el GridView con los datos del DataTable
            dgvRepo.DataSource = dt
            dgvRepo.DataBind()
        ElseIf e.CommandName = "mas" Then
            Dim indice As Integer
            indice = Convert.ToInt32(e.CommandArgument)
            Dim dt As DataTable = CType(ViewState("Datos"), DataTable)
            For i = 0 To dt.Rows.Count - 1
                If dt.Rows(i).Item("IdProducto") = dgvRepo.Rows(indice).Cells(0).Text Then
                    dt.Rows(i).Item("Cantidad") += 1
                    Exit For
                End If
            Next i
            ' Almacenar el DataTable actualizado en el ViewState
            ViewState("Datos") = dt

            ' Actualizar el GridView con los datos del DataTable
            dgvRepo.DataSource = dt
            dgvRepo.DataBind()
        ElseIf e.CommandName = "menos" Then
            Dim indice As Integer
            Dim nCant As Integer
            indice = Convert.ToInt32(e.CommandArgument)
            Dim dt As DataTable = CType(ViewState("Datos"), DataTable)
            For i = 0 To dt.Rows.Count - 1
                If dt.Rows(i).Item("IdProducto") = dgvRepo.Rows(indice).Cells(0).Text Then
                    nCant = dt.Rows(i).Item("Cantidad")
                    nCant -= 1
                    If nCant <= 0 Then
                        dt.Rows(i).Delete()
                    Else
                        dt.Rows(i).Item("Cantidad") = nCant
                    End If
                    Exit For
                End If
            Next i
            ' Almacenar el DataTable actualizado en el ViewState
            ViewState("Datos") = dt

            ' Actualizar el GridView con los datos del DataTable
            dgvRepo.DataSource = dt
            dgvRepo.DataBind()
        End If
    End Sub

    Private Sub dgvData_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvData.RowCommand
        Dim script As String = ""
        If e.CommandName = "Agregar" Then
            Dim indice As Integer
            indice = Convert.ToInt32(e.CommandArgument)

            Dim nCodigo As Integer = Convert.ToInt32(dgvData.Rows(indice).Cells(0).Text)

            Dim lst As List(Of producto) = New CN_Producto().Listar(nCodigo)
            Dim ExisteProducto As Boolean = False
            Dim precio As Decimal = Convert.ToDecimal(dgvData.Rows(indice).Cells(4).Text)

            Dim dt As DataTable = CType(ViewState("Datos"), DataTable)
            For i = 0 To dt.Rows.Count - 1
                If dt.Rows(i).Item("IdProducto") = lst(0).idProducto Then
                    dt.Rows(i).Item("Cantidad") += 1
                    ExisteProducto = True
                End If
            Next i
            ' Añadir una nueva fila al DataTable
            If Not ExisteProducto Then
                Dim nuevaFila As DataRow = dt.NewRow()

                nuevaFila("idProducto") = lst(0).idProducto ' ID incrementado automáticamente
                nuevaFila("Nombre") = lst(0).Nombre
                nuevaFila("Cantidad") = 1
                nuevaFila("PrecioVenta") = precio
                nuevaFila("Referencia") = sReferencia
                dt.Rows.Add(nuevaFila)
            End If
            ' Almacenar el DataTable actualizado en el ViewState
            ViewState("Datos") = dt

            ' Actualizar el GridView con los datos del DataTable
            dgvRepo.DataSource = dt
            dgvRepo.DataBind()
            'Response.Redirect("producto_lista_ABM.aspx?idProducto=" & Convert.ToInt32(dgvData.Rows(indice).Cells(0).Text) & "&TituloForm=MODIFICAR PRODUCTO &Accion=E")
        ElseIf e.CommandName = "restar" Then
            Dim indice As Integer
            indice = Convert.ToInt32(e.CommandArgument)
            Response.Redirect("producto_lista_ABM.aspx?idProducto=" & Convert.ToInt32(dgvData.Rows(indice).Cells(0).Text) & "&TituloForm=ELIMINAR PRODUCTO &Accion=B")
        ElseIf e.CommandName = "Ajuste" Then
            Dim indice As Integer
            indice = Convert.ToInt32(e.CommandArgument)
            'lblIdProducto.Text = dgvData.Rows(indice).Cells(0).Text
            'lblSucursal.Text = cboSucursal.SelectedItem.Text
            'lblProductoMDL.Text = dgvData.Rows(indice).Cells(1).Text
            'lblStock.Text = dgvData.Rows(indice).Cells(3).Text
            'txtCantidadMDL.Text = 0
            script = "$(function() { showModalAjuste(); }); "
            ScriptManager.RegisterStartupScript(Me, Page.GetType(), "editmdl", script, True)
        ElseIf e.CommandName = "Eliminar" Then
            Dim indice As Integer
            indice = Convert.ToInt32(e.CommandArgument)
            Dim dt As DataTable = CType(ViewState("Datos"), DataTable)
            For i = 0 To dt.Rows.Count - 1
                If dt.Rows(i).Item("IdProducto") = dgvData.Rows(indice).Cells(0).Text Then
                    dt.Rows(i).Delete()
                End If
            Next i
            ' Almacenar el DataTable actualizado en el ViewState
            ViewState("Datos") = dt

            ' Actualizar el GridView con los datos del DataTable
            dgvRepo.DataSource = dt
            dgvRepo.DataBind()
        End If
    End Sub

    Private Sub InicializarTabla()
        ' Crear un DataTable con columnas para el GridView
        Dim dt As New DataTable()
        dt.Columns.Add("IdProducto", GetType(Integer))
        dt.Columns.Add("Nombre", GetType(String))
        dt.Columns.Add("Cantidad", GetType(Decimal))

        dt.Columns.Add("PrecioVenta", GetType(Decimal))
        dt.Columns.Add("Referencia", GetType(String))
        '   idProducto INT,
        '   Cantidad Int,
        '   PrecioUnitario Decimal(18, 2),
        'ALicuotaIVA Decimal(18, 2),
        '   PrecioVetna Decimal(18, 2),
        'Referencia varchar(50)

        ' Almacenar el DataTable en el ViewState para mantener el estado entre postbacks
        ViewState("Datos") = dt

        ' Vincular el DataTable al GridView
        'dgvRepo.DataSource = dt
        'dgvRepo.DataBind()
    End Sub
    'Protected Sub btnAgregarFila_Click(sender As Object, e As EventArgs)
    '    ' Recuperar el DataTable almacenado en el ViewState
    '    Dim dt As DataTable = CType(ViewState("Datos"), DataTable)

    '    ' Añadir una nueva fila al DataTable
    '    Dim nuevaFila As DataRow = dt.NewRow()
    '    nuevaFila("ProductoID") = dt.Rows.Count + 1 ' ID incrementado automáticamente
    '    nuevaFila("Nombre") = "Nuevo Producto"
    '    nuevaFila("Cantidad") = 0
    '    nuevaFila("Precio") = 999.99 ' Precio de ejemplo
    '    dt.Rows.Add(nuevaFila)

    '    ' Almacenar el DataTable actualizado en el ViewState
    '    ViewState("Datos") = dt

    '    ' Actualizar el GridView con los datos del DataTable
    '    dgvRepo.DataSource = dt
    '    dgvRepo.DataBind()
    'End Sub

    Protected Sub BtnNuevoRepo(sender As Object, e As EventArgs) Handles BtnNuevaRepo.Click
        InicializarTabla()
        dgvRepo.DataBind()
    End Sub
    Protected Sub BtnGuardarRepo_Click(sender As Object, e As EventArgs) Handles BtnGuardarRepo.Click
        lblMensajeRepo.Text = "¿Guardar la reposición?"
        Dim script As String = "showModalReposicion();"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalRepo", script, True)
    End Sub

    Protected Sub btnMDLGuardarReposicion_Click(sender As Object, e As EventArgs) Handles btnMDLGuardarReposicion.Click
        'CREATE Type EDetalleRepo AS TABLE (
        '    idCompra Int,
        '    idProducto INT,
        '    Cantidad Int,
        '    PrecioUnitario Decimal,
        '    AlicuotaIVA Decimal,
        '    SubTotal Decimal,
        '    Referencia varchar(50)

        Dim procedureName As String = "CrearType_tbEDetalleRepo"
        Dim resultado As Integer = 0
        Dim Mensaje As String = ""
        ' Llamar al procedimiento almacenado
        Using connection As New SqlConnection(conectar.Cadena)
            Try
                connection.Open()
                Using command As New SqlCommand(procedureName, connection)
                    command.CommandType = CommandType.StoredProcedure
                    command.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output
                    command.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output
                    command.ExecuteNonQuery()
                    Mensaje = command.Parameters("@Mensaje").Value.ToString()
                    resultado = Convert.ToInt32(command.Parameters("@Resultado").Value)
                End Using

            Catch ex As Exception
                lblMensajeAtencion.Text = ex.Message
                Dim script As String
                script = "$(function() { showModalAtencion(); }); "
                ScriptManager.RegisterStartupScript(Me, Page.GetType(), "editmdl", script, True)
                Exit Sub
            End Try
        End Using


        procedureName = "CrearType_tbDetalleInventario"
        resultado = 0
        Mensaje = ""
        ' Llamar al procedimiento almacenado
        Using connection As New SqlConnection(conectar.Cadena)
            Try
                connection.Open()
                Using command As New SqlCommand(procedureName, connection)
                    command.CommandType = CommandType.StoredProcedure
                    command.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output
                    command.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output
                    command.ExecuteNonQuery()
                    Mensaje = command.Parameters("@Mensaje").Value.ToString()
                    resultado = Convert.ToInt32(command.Parameters("@Resultado").Value)
                End Using

            Catch ex As Exception
                lblMensajeAtencion.Text = ex.Message
                Dim script As String
                script = "$(function() { showModalAtencion(); }); "
                ScriptManager.RegisterStartupScript(Me, Page.GetType(), "editmdl", script, True)
                Exit Sub
            End Try
        End Using
        Dim referencia As String = Date.Now.Ticks
        Dim nNumerpComprobante As Integer = 0
        nNumerpComprobante = UltimoNumeroComprobante("Maestro_Movimientos_Cab", "NumeroComprobante", "REP")
        Try
            Dim objMovi As maestromovimiento
            Dim idgenerado As Integer = 0
            Mensaje = ""
            nUsuario = Session("IdUsuario")
            'Dim detalles As New List(Of maestromovimiento_detalle)
            Dim tbDetalleRepo As New DataTable
            tbDetalleRepo.Columns.Add("idCompra", GetType(Integer))
            tbDetalleRepo.Columns.Add("idProducto", GetType(Integer))
            tbDetalleRepo.Columns.Add("Cantidad", GetType(Integer))
            tbDetalleRepo.Columns.Add("PrecioUnitario", GetType(Decimal))
            tbDetalleRepo.Columns.Add("ALicuotaIVA", GetType(Decimal))
            tbDetalleRepo.Columns.Add("PrecioVetna", GetType(Decimal))
            tbDetalleRepo.Columns.Add("Referencia", GetType(Decimal))

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




            For i = 0 To dgvRepo.Rows.Count - 1
                tbDetalleRepo.Rows.Add(0, dgvRepo.Rows(i).Cells(0).Text, dgvRepo.Rows(i).Cells(2).Text, 0, dgvRepo.Rows(i).Cells(3).Text, 0, referencia)
                tbDetalleInventario.Rows.Add(1, cboSucursal.SelectedValue, dgvRepo.Rows(i).Cells(0).Text, Date.Today, "Reposicion", 0, dgvRepo.Rows(i).Cells(2).Text, dgvRepo.Rows(i).Cells(3).Text,
                                       0, dgvRepo.Rows(i).Cells(2).Text, dgvRepo.Rows(i).Cells(3).Text, Date.Today, nUsuario, 8, referencia)
            Next i

            objMovi = New maestromovimiento() With {
                .idMovimiento = 0,
                .oDeSucursal = New sucursal() With {.idSucursal = 1},
                .oASucursal = New sucursal() With {.idSucursal = cboSucursal.SelectedValue},
                .Fecha = Date.Today,
                .oCliente = New cliente() With {.idcliente = 0},
                .oProveedor = New proveedor() With {.idProveedor = 0},
                .oUsuario = New usuario() With {.idusuario = nUsuario},
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
                .tbDetalleRepo = tbDetalleRepo,
                .tbtDetalleInventario = tbDetalleInventario,
                .Comprobante = "REP-" & nSuc.ToString().PadLeft(4, "0"c) & "-" & nNumerpComprobante.ToString().PadLeft(8, "0"c),
                .ImpuestosInternos = 0,
                .PercepcionIngresosBrutos = 0,
                .NoGravado = 0,
                .ImporteTotal = 0,
                .Referencia = referencia,
                .Notas = txtNotas.Text
              }



            idgenerado = New CD_MaestroMovimiento().RegistrarRepo(objMovi, tbDetalleRepo, tbDetalleInventario, Mensaje)

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
End Class