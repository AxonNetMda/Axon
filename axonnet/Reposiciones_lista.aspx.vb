Imports System.Data.SqlClient
Imports CapaDatos
Imports CapaDatos.Conexion
Imports capaEntidad
Imports capaNegocio
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class Reposiciones_lista
    Inherits System.Web.UI.Page
    Public Property scomando As String
    Public Property sMensaje As String
    Public Property nUsuario As Integer
    Dim filtro As String = ""
    Dim nSuc As Integer
    Dim nASuc As Integer
    Dim nDeSuc As Integer
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
            nDeSuc = nSuc
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



            nSuc = Session("IdSucursal")
            CargarReposicionCabecera(nSuc)
        End If
    End Sub

    ' Método para cargar la lista de pedidos desde la base de datos
    Private Sub CargarReposicionCabecera(ByVal nSuc As Integer)
        If Trim(txtBuscar.Text) <> "" Then
            If nSuc = 1 Then
                filtro = " WHERE  reposiciones.idDesucursal=1 and sr.Nombre LIKE '%" & txtBuscar.Text & "%' or dbo.reposiciones.fecha LIKE '%" & txtBuscar.Text & "%'"
            Else
                filtro = " WHERE  reposiciones.idAsucursal=" & nSuc & " And SR.Nombre Like '%" & txtBuscar.Text & "%' or dbo.reposiciones.fecha LIKE '%" & txtBuscar.Text & "%' "
            End If
        Else
            If nSuc = 1 Then
                filtro = " WHERE reposiciones.idDEsucursal=1"
            Else
                filtro = " WHERE reposiciones.idAsucursal=" & nSuc
            End If
        End If
        Dim query As String = "SELECT dbo.reposiciones.idReposicion, dbo.reposiciones.idDeSucursal, se.Nombre AS SucEmisora, dbo.reposiciones.idASucursal, dbo.reposiciones.Referencia, 
                        sr.Nombre AS SucReceptora, dbo.reposiciones.fecha, dbo.reposiciones.idUsuario,dbo.reposiciones.referencia, dbo.usuario.Nombre, dbo.reposiciones.Notas, dbo.reposiciones.Estado 
                        FROM dbo.reposiciones
                        INNER JOIN dbo.Sucursales se ON dbo.reposiciones.idDeSucursal = se.idSucursal
                        INNER JOIN dbo.usuario ON dbo.reposiciones.idUsuario = dbo.usuario.idUsuario
                        INNER JOIN dbo.Sucursales sr ON dbo.reposiciones.idASucursal = sr.idSucursal " & filtro & " ORDER BY Nombre"
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
        CargarReposicionCabecera(nSuc)
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
                    CargarReposicionCabecera(nSuc)
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
                gvDetalle.Columns.Add(New BoundField() With {.DataField = "IdRepoDetalle", .HeaderText = "ID"})
                gvDetalle.Columns.Add(New BoundField() With {.DataField = "IdReposicion", .HeaderText = "Reposición", .DataFormatString = "{0:00000}"})
                gvDetalle.Columns.Add(New BoundField() With {.DataField = "IdProducto", .HeaderText = "Producto"})
                gvDetalle.Columns.Add(New BoundField() With {.DataField = "NombreProducto", .HeaderText = "Nombre Producto"})
                gvDetalle.Columns.Add(New BoundField() With {.DataField = "Cantidad", .HeaderText = "Cantidad"})
                gvDetalle.Columns.Add(New BoundField() With {.DataField = "PrecioVenta", .HeaderText = "Precio", .DataFormatString = "{0:C2}"})
                gvDetalle.Columns(0).HeaderStyle.Width = 50
                gvDetalle.Columns(0).HeaderStyle.HorizontalAlign = HorizontalAlign.Right
                gvDetalle.Columns(0).ItemStyle.HorizontalAlign = HorizontalAlign.Right
                gvDetalle.Columns(1).HeaderStyle.Width = 50
                gvDetalle.Columns(1).ItemStyle.HorizontalAlign = HorizontalAlign.Right
                gvDetalle.Columns(2).HeaderStyle.Width = 50
                gvDetalle.Columns(2).ItemStyle.HorizontalAlign = HorizontalAlign.Right
                gvDetalle.Columns(4).HeaderStyle.Width = 50
                gvDetalle.Columns(4).ItemStyle.HorizontalAlign = HorizontalAlign.Right
                gvDetalle.Columns(5).HeaderStyle.Width = 150
                gvDetalle.Columns(5).HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                gvDetalle.Columns(5).ItemStyle.HorizontalAlign = HorizontalAlign.Right
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
            Response.Redirect("Reposiciones_impresion1.aspx?idReposicion=" & idReposicion, False)
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
                ' Obtener la fila desde la cual se realizó el comando
                Dim row As GridViewRow = CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)
                Dim index As Integer = row.RowIndex
                Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)

                ' Obtener la fila seleccionada
                Dim fila As GridViewRow = gvCabecera.Rows(index)
                FilaIndex = index
                hdfidReposicion.Value = rowIndex
                lblMensajeRepoAnular.Text = "Quiere Anular esta reposicion"
                script = "$(function() { showModalReposicionAnular(); }); "
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
            CargarReposicionCabecera(nSuc)
            Exit Sub
        End If
    End Sub


    ' Método para cargar los datos de la tabla ReposicionDetalle
    Private Function CargarFacturaDetalle(ByVal idReposicion As Integer) As DataTable
        Dim query As String = "Select dbo.reposicionesdetalles.idRepoDetalle,  dbo.reposicionesdetalles.idReposicion, dbo.reposicionesdetalles.idProducto, dbo.reposicionesdetalles.Cantidad,
                               dbo.producto.Nombre as NombreProducto,  dbo.preciodeventa(PrecioCosto, AlicuotaIVA, Ganancia) as PrecioVenta  FROM dbo.reposicionesdetalles 
                               INNER JOIN dbo.producto ON dbo.reposicionesdetalles.idProducto = dbo.producto.idProducto WHERE idreposicion = @idReposicion"
        Dim dt As New DataTable()

        Using connection As New SqlConnection(conectar.Cadena)
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@idReposicion", idReposicion)
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
                filtro = " WHERE  reposiciones.idDesucursal=1 and sr.Nombre LIKE '%" & txtBuscar.Text & "%' or dbo.reposiciones.fecha LIKE '%" & txtBuscar.Text & "%' AND  dbo.reposiciones.Estado=1"
            Else
                filtro = " WHERE  reposiciones.idAsucursal=" & nSuc & " And SR.Nombre Like '%" & txtBuscar.Text & "%' or dbo.reposiciones.fecha LIKE '%" & txtBuscar.Text & "%'  AND  dbo.reposiciones.Estado=1"
            End If
        Else
            If nSuc = 1 Then
                filtro = " WHERE reposiciones.idDEsucursal=1  AND  dbo.reposiciones.Estado=1"
            Else
                filtro = " WHERE reposiciones.idAsucursal=" & nSuc & " AND  dbo.reposiciones.Estado=1"
            End If
        End If
        scomando = "SELECT dbo.reposiciones.idReposicion, dbo.reposiciones.idDeSucursal, se.Nombre AS SucEmisora, dbo.reposiciones.idASucursal, dbo.reposiciones.Estado, 
                        sr.Nombre AS SucReceptora, dbo.reposiciones.fecha, dbo.reposiciones.idUsuario,dbo.reposiciones.referencia, dbo.usuario.Nombre, dbo.reposiciones.Notas
                        FROM dbo.reposiciones
                        INNER JOIN dbo.Sucursales se ON dbo.reposiciones.idDeSucursal = se.idSucursal
                        INNER JOIN dbo.usuario ON dbo.reposiciones.idUsuario = dbo.usuario.idUsuario
                        INNER JOIN dbo.Sucursales sr ON dbo.reposiciones.idASucursal = sr.idSucursal " & filtro & " ORDER BY Nombre"
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

    Protected Sub gvCabecera_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvCabecera.PageIndexChanging
        ' Cambiar el índice de la página del GridView

        CargarReposicionCabecera(nSuc)

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
    Private Sub Imprimir(ByVal idReposicion As Integer)
        Dim ocn As New SqlConnection(CapaDatos.Conexion.conectar.Cadena)
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim ssql As String = ""
        Dim ds As New DataSet
        ' Dim Repo As New crReposiciones
        Dim dt As New DataTable
        Dim path As String = Server.MapPath("~/")


        ssql = "SELECT dbo.reposiciones.idReposicion, dbo.reposiciones.idDeSucursal, se.Nombre AS SucEmisora, dbo.reposiciones.idASucursal, sr.Nombre AS SucReceptora,
                    dbo.reposiciones.fecha, dbo.reposiciones.idUsuario, dbo.reposiciones.Referencia, dbo.usuario.Nombre, dbo.reposiciones.Notas,
                    dbo.reposicionesdetalles.idProducto, dbo.reposicionesdetalles.Cantidad, dbo.producto.Nombre 
                    FROM dbo.reposiciones
                    INNER JOIN dbo.Sucursales AS se ON dbo.reposiciones.idDeSucursal = se.idSucursal
                    INNER JOIN dbo.usuario ON dbo.reposiciones.idUsuario = dbo.usuario.idUsuario
                    INNER JOIN dbo.Sucursales AS sr ON dbo.reposiciones.idASucursal = sr.idSucursal
                    INNER JOIN dbo.reposicionesdetalles ON dbo.reposiciones.idReposicion = dbo.reposicionesdetalles.idReposicion
                    INNER JOIN dbo.producto ON dbo.reposicionesdetalles.idProducto = dbo.producto.idProducto
                    WHERE dbo.reposiciones.idReposicion = " & idReposicion & " ORDER BY SucReceptora ASC"
        ocn.Open()
        cmd.Connection = ocn
        cmd.CommandText = ssql
        da.SelectCommand = cmd
        da.Fill(ds)

        ds.WriteXml(path & "/Reposicion.xml", XmlWriteMode.WriteSchema)
        ' Crear una instancia del reporte
        'Dim reporte As New ReportDocument

        ' Cargar el archivo .rpt (Crystal Report)
        'Repo.Load(path & "crReposiciones.rpt")

        'crvReposicion.ReportSource = Repo
        'crvReposicion.DataBind()
        Try
            ' Crear una instancia del ReportDocument
            'Dim report As New ReportDocument()

            ' Cargar el archivo .rpt en el objeto ReportDocument
            'report.Load("C:\Desarrollo\WelcomeHome2024\capaAdministracion\crReposiciones.rpt")

            ' Si necesitas pasar un DataSet, puedes cargarlo aquí

            ds.ReadXml("C:\Desarrollo\WelcomeHome2024\capaAdministracion\Reposicion.xml")
            'report.SetDataSource(ds)

            ' Asignar el reporte al visor
            'crvReposicion.ReportSource = report

            ' Refrescar el visor para visualizar el reporte
            'crvReposicion.RefreshReport()
        Catch ex As Exception
            Dim script As String
            lblMensajeAtencion.Text = "Error al cargar el reporte: " & ex.Message
            script = "$(function() { showModalAtencion(); }); "
            ScriptManager.RegisterStartupScript(Me, Page.GetType(), "editmdl", script, True)

        End Try
    End Sub

    Private Sub btnMDLAnularRepo_Click(sender As Object, e As EventArgs) Handles btnMDLAnularRepo.Click
        Dim referecia As String = Date.Now.Ticks
        Dim nNumerpComprobante As Integer = 0
        Try
            Dim objMovi As reposicion
            Dim idgenerado As Integer = 0
            Dim mensaje As String = ""
            nDeSuc = cboSucursal.SelectedValue
            Dim lstRepo As List(Of reposicion) = New CD_Reposicion().Listar(hdfidReposicion.Value, nDeSuc)
            Dim detalles As List(Of reposiciondetalles) = New CD_Reposicion().ListarDetalle(hdfidReposicion.Value)
            Dim dt As New DataTable
            dt.Columns.Add("idReposicion", GetType(Integer))
            dt.Columns.Add("idProducto", GetType(Integer))
            dt.Columns.Add("Cantidad", GetType(Integer))



            For i = 0 To detalles.Count - 1
                dt.Rows.Add(detalles(i).idReposicion, detalles(i).oProducto.idProducto, Convert.ToInt32(detalles(i).Cantidad))

            Next i

            objMovi = New reposicion() With {
                .idReposicion = hdfidReposicion.Value,
                .oDeSucursal = New sucursal() With {.idSucursal = lstRepo(0).oDeSucursal.idSucursal},
                .oASucursal = New sucursal() With {.idSucursal = lstRepo(0).oASucursal.idSucursal},
                .fecha = Date.Today,
                .oDetalle = detalles,
                .Referencia = referecia
              }


            idgenerado = New CD_Reposicion().AnularRepo(objMovi, dt, mensaje)


            If mensaje <> "" Then
                lblMensajeAtencion.Text = mensaje
                Dim script As String
                script = "$(function() { showModalAtencion(); }); "
                ScriptManager.RegisterStartupScript(Me, Page.GetType(), "editmdl", script, True)
            Else
                Response.Redirect("repisiciones_lista.aspx")
            End If


        Catch ex As Exception
            Dim script As String
            lblMensajeAtencion.Text = ex.Message
            script = "$(function() { showModalAtencion(); }); "
            ScriptManager.RegisterStartupScript(Me, Page.GetType(), "editmdl", script, True)
        End Try

    End Sub

    Private Sub cboMostrar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboMostrar.SelectedIndexChanged
        If IsPostBack Then
            FilaIndex = gvCabecera.SelectedIndex
            selectedFila = gvCabecera.SelectedRow
            gvCabecera.PageSize = cboMostrar.SelectedValue
            gvCabecera.DataBind()
        End If
    End Sub

    'Private Sub gvCabecera_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvCabecera.SelectedIndexChanged
    '    

    '    ' Obtener la fila seleccionada
    '    
    'End Sub


End Class
'Module GridViewExtensions
'    <System.Runtime.CompilerServices.Extension()>
'    Public Function NextRow(row As GridViewRow) As GridViewRow
'        Dim gv As GridView = CType(row.NamingContainer, GridView)
'        Dim nextIndex As Integer = row.RowIndex + 1
'        If nextIndex < gv.Rows.Count Then
'            Return gv.Rows(nextIndex)
'        End If
'        Return Nothing
'    End Function
'End Module