Imports System.Data.SqlClient
Imports CapaDatos
Imports CapaDatos.Conexion
Imports capaEntidad
Imports capaNegocio

Public Class producto_inventario
    Inherits System.Web.UI.Page
    Public Property scomando As String
    Public Property sMensaje As String
    Public Property nProducto As Integer
    Public Property sBuscar As String
    Public Property nSuc As Integer
    Dim filtro As String = ""
    Public Property aTitulo As String

    Dim valorCelda As Integer
    Dim listsuc As List(Of sucursal)
    Dim fechaDesde As Date = Date.Parse("2024-01-01")
    Dim fechaHasta As Date = Date.Parse("2024-04-30")
    Dim idSucursal As Integer = 2
    Dim idProducto As Integer = 2
    Dim sDesdeDonde As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("sNombreUsuario") Is Nothing Then
            Response.Redirect("~/login.aspx")
        End If

        If IsPostBack Then
            'listsuc = New CN_Sucursal().Listar(0)
            If sDesdeDonde = "STK" Then
                idProducto = Convert.ToInt32(lblIdProducto.Text)
            Else


            End If
            fechaDesde = Date.Parse(fecDesde.Text).ToString("yyyy-MM-dd")
            fechaHasta = Date.Parse(fecHasta.Text).ToString("yyyy-MM-dd")
            idSucursal = Convert.ToInt32(cboSucursal.SelectedValue)
            idProducto = Convert.ToInt32(cboProducto.SelectedValue)
        Else

            sDesdeDonde = Request.QueryString("DesdeDonde")
            cboSucursal.Items.Clear()
            idSucursal = Request.QueryString("idSucursal")
            idProducto = Request.QueryString("idProducto")
            fecDesde.Attributes("value") = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd")
            fecHasta.Attributes("value") = DateTime.Now.ToString("yyyy-MM-dd")
            fechaDesde = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd")
            fechaHasta = DateTime.Now.ToString("yyyy-MM-dd")
            fecDesde.Text = Date.Parse(fechaDesde).ToString("yyyy-MM-dd")
            fecHasta.Text = Date.Parse(fechaHasta).ToString("yyyy-MM-dd")
            If sDesdeDonde = "STK" Then
                ' vengo desde la pagina de producto_stock_listado.aspx
                divNombreSucursalMostrar.Visible = True
                divMostrarComboProducto.Visible = False
            Else
                ' vengo desde el menu stock - inventario
                divNombreSucursalMostrar.Visible = False
                divMostrarComboProducto.Visible = True
            End If

            Dim listsuc As List(Of sucursal) = New CD_Sucursal().Listar(0)
            For item = 0 To listsuc.Count - 1
                cboSucursal.Items.Insert(item, New ListItem(listsuc(item).Nombre, listsuc(item).idSucursal))
            Next
            cboSucursal.SelectedIndex = 0
            For Each item As ListItem In cboSucursal.Items
                If Integer.TryParse(listsuc(0).idSucursal, valorCelda) Then
                    ' Compara el valor de la celda con el valor del ítem del ComboBox.
                    If item.Value = idSucursal Then
                        cboSucursal.SelectedValue = item.Value
                        lblidSucursal.Text = item.Value
                        lblNombreSucursal.Text = item.Text
                        Exit For
                    End If
                End If
            Next

            Dim lstproducto As List(Of producto) = New CD_Producto().Listar(0)
            For item = 0 To lstproducto.Count - 1
                cboProducto.Items.Insert(item, New ListItem(lstproducto(item).Nombre, lstproducto(item).idProducto))
            Next
            cboProducto.SelectedIndex = 0
            For Each item As ListItem In cboProducto.Items
                If Integer.TryParse(lstproducto(0).idProducto, valorCelda) Then
                    ' Compara el valor de la celda con el valor del ítem del ComboBox.
                    If item.Value = idProducto Then
                        cboProducto.SelectedValue = item.Value
                        lblIdProducto.Text = item.Value
                        lblNompreProducto.Text = item.Text
                        Exit For
                    End If
                End If
            Next



        End If
        CargarInventario()




    End Sub
    Private Sub CargarInventario()
        ' Definir los parámetros

        ' Cadena de conexión (ajustar según tu configuración)
        'Dim conexionString As String = conectar.Cadena

        'Using conexion As New SqlConnection(conexionString)
        '    conexion.Open()
        'DECLARE @idSucursal INT = 2;  -- Ajusta el valor según corresponda
        'DECLARE @idProducto INT = 2;
        'DECLARE @fechaDesde DATE = '2022-01-01';
        'DECLARE @fechahasta DATE = '2024-12-31';
        ' Definir la consulta SQL con saldo inicial y saldo acumulado
        scomando = "DECLARE @SaldoAnterior DECIMAL(18,2)
                    SELECT @SaldoAnterior = COALESCE(SUM(CantidadEntrada - CantidadSalida), 0.00) 
                    FROM dbo.producto_inventario
                    WHERE idSucursal =" & cboSucursal.SelectedValue & " 
                    AND idProducto =" & cboProducto.SelectedValue & " 
                    AND fecFecha < '" & fecDesde.Text & " ';
                    WITH Movimientos AS (
                    SELECT  
                    pi.fecFecha, 
                    ito.nombre AS Detalle, 	
		            (pi.PrecioEntrada + pi.PrecioSalida) AS PrecioUnitario, 
                    pi.CantidadEntrada AS Entrada, 
                    pi.CantidadSalida AS Salida, 
                    COALESCE(pr.RazonSocial, c.Nombre, 'AJUSTE DE STOCK INTERNO') AS ClienteProveedor
                    FROM dbo.producto_inventario pi
                    INNER JOIN dbo.producto_inventario_tipo_operacion ito ON pi.TipoOperacion = ito.idTipoOperacionInventario
                    LEFT JOIN dbo.proveedor pr ON pi.idProveedor = pr.idProveedor
                    LEFT JOIN dbo.cliente c ON pi.idCliente = c.idCliente
                    WHERE pi.idSucursal =" & cboSucursal.SelectedValue & "  AND pi.idProducto = " & cboProducto.SelectedValue & " AND pi.fecFecha BETWEEN '" & fecDesde.Text & "' AND '" & fecHasta.Text & "')
                    SELECT fecFecha, Detalle, PrecioUnitario, Entrada, Salida, SUM(COALESCE(Entrada, 0) - COALESCE(Salida, 0)) 
                           OVER (ORDER BY fecFecha ROWS UNBOUNDED PRECEDING) + @SaldoAnterior AS Saldo, ClienteProveedor
                    FROM Movimientos
                    UNION ALL
                   SELECT DATEADD(DAY, -1, '" & fecDesde.Text & "') AS fecFecha, 'Saldo anterior' AS Detalle, 0 AS PrecioUnitario, 
                          0 AS Entrada, 0 AS Salida, @SaldoAnterior AS Saldo,'' AS ClienteProveedor
                   ORDER BY fecFecha ASC;"


        sqlProducto.SelectCommand = scomando
            dgvData.PageSize = cboMostrar.SelectedValue
            dgvData.DataBind()
        'End Using
    End Sub

    Protected Sub btnVolver_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/DefaultAdmin.apsx")
    End Sub
End Class