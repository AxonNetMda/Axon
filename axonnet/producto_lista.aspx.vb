
Imports System.Data.SqlClient
Imports CapaDatos
Imports CapaDatos.Conexion
Imports capaEntidad

Public Class producto_lista
    Inherits System.Web.UI.Page
    Public Property scomando As String
    Public Property sMensaje As String
    Dim filtro As String = ""
    Dim nSuc As Integer
    Dim verificaTabla As String = "CREATE TABLE [dbo].[producto] (
            [idProducto] int NOT NULL IDENTITY(1,1) ,
            [CodigoBarras] varchar(150) COLLATE Modern_Spanish_CI_AS NULL DEFAULT ((0)) ,
            [Nombre] varchar(250) COLLATE Modern_Spanish_CI_AS NULL ,
            [idCategoria] int NULL ,
            [idSubCategoria] int NULL ,
            [idMarca] int NULL ,
            [idProveedor] int NULL DEFAULT ((1)) ,
            [PrecioCosto] decimal(18,2) NULL DEFAULT ((0)) ,
            [AlicuotaIVA] decimal(18,2) NULL DEFAULT ((21)) ,
            [Ganancia] decimal(18,2) NULL DEFAULT ((0)) ,
            [Redondeo] decimal(18,2) NULL ,
            [StockCritico] decimal(11) NULL DEFAULT ((0)) ,
            [RutaImagen] varchar(150) COLLATE Modern_Spanish_CI_AS NULL DEFAULT ('productos') ,
            [nombreFoto1] varchar(150) COLLATE Modern_Spanish_CI_AS NULL DEFAULT ('oferta.png') ,
            [nombreFoto2] varchar(150) COLLATE Modern_Spanish_CI_AS NULL DEFAULT ('oferta.png') ,
            [nombreFoto3] varchar(150) COLLATE Modern_Spanish_CI_AS NULL DEFAULT ('oferta.png') ,
            [nombreFoto4] varchar(150) COLLATE Modern_Spanish_CI_AS NULL DEFAULT ('oferta.png') ,
            [nombreFoto5] varchar(150) COLLATE Modern_Spanish_CI_AS NULL DEFAULT ('oferta.png') ,
            [nombreFoto6] varchar(150) COLLATE Modern_Spanish_CI_AS NULL DEFAULT ('oferta.png') ,
            [nombreFoto7] varchar(150) COLLATE Modern_Spanish_CI_AS NULL DEFAULT ('oferta.png') ,
            [nombreFoto8] varchar(150) COLLATE Modern_Spanish_CI_AS NULL DEFAULT ('oferta.png') ,
            [nombreFoto9] varchar(150) COLLATE Modern_Spanish_CI_AS NULL DEFAULT ('oferta.png') ,
            [nombreFoto10] varchar(150) COLLATE Modern_Spanish_CI_AS NULL DEFAULT ('oferta.png') ,
            [nombreFoto11] varchar(150) COLLATE Modern_Spanish_CI_AS NULL DEFAULT ('oferta.png') ,
            [nombreFoto12] varchar(150) COLLATE Modern_Spanish_CI_AS NULL DEFAULT ('oferta.png') ,
            [Notas] varchar(250) COLLATE Modern_Spanish_CI_AS NULL DEFAULT ('oferta.png') ,
            [FechaUltimacompra] date NULL DEFAULT NULL ,
            [FechaCreacion] date NULL DEFAULT (getdate()) ,
            [FechaUltimaActualizacion] date NULL ,
            [MostrarCatalogo] bit NULL DEFAULT ((0)) ,
            [EsDestacado] bit NULL DEFAULT ((0)) ,
            [EsOferta] bit NULL DEFAULT ((0)) ,
            [EsOutlet] bit NULL DEFAULT ((0)) ,
            [Estado] bit NULL DEFAULT ((1)) ,
            [FechaCompra] nchar(10) COLLATE Modern_Spanish_CI_AS NULL 
            )
            ON [PRIMARY]"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        nSuc = Session("idSucursal")
        If Not IsPostBack Then
            Dim script As String
            Dim mensaje As String = VerificarYCrearTabla(verificaTabla)
            If mensaje <> "" Then
                lblMensajeAtencion.Text = "Error en la ccreacion de la tabla:" & mensaje
                script = "$(function() { showModalAtencion(); }); "
                ScriptManager.RegisterStartupScript(Me, Page.GetType(), "mdlAtencion", script, True)
            Else
                Dim listsuc As List(Of sucursal) = New CD_Sucursal().Listar(0)
                For item = 0 To listsuc.Count - 1
                    cboSucursal.Items.Insert(item, New ListItem(listsuc(item).Nombre, listsuc(item).idSucursal))
                Next
                cboSucursal.SelectedIndex = 0
            End If
            CargarDatosGridView()
        End If

    End Sub

    Private Sub CargarDatosGridView()

        If Trim(txtBuscar.Text) <> "" Then
            filtro = " WHEREid producto.Nombre LIKE '%" & txtBuscar.Text & "%' or proveedor.RazonSocial LIKE '%" & txtBuscar.Text & "%' or producto_marca.Nombre LIKE '%" & txtBuscar.Text & "%'"
        Else
            filtro = ""
        End If
        scomando = "SELECT producto.idProducto, producto.CodigoBarras, producto.Nombre, producto.idCategoria, producto.idSubCategoria,
                    producto.idMarca, producto.idProveedor, producto.PrecioCosto, producto.AlicuotaIVA, producto.Ganancia, 
                    dbo.preciodeventa2(PrecioCosto, AlicuotaIVA, Ganancia) as PrecioVenta,
                    producto.Redondeo, dbo.producto.StockCritico, producto.rutaimagen, 
                    producto.nombrefoto1, producto.nombrefoto2, producto.nombrefoto2, producto.nombrefoto3, producto.nombrefoto4, producto.nombrefoto5, producto.nombrefoto6, 
                    producto.nombrefoto7, dbo.producto.nombrefoto8, dbo.producto.nombrefoto9, producto.nombrefoto10, producto.nombrefoto11, producto.nombrefoto12,
                    producto.notas, producto.fechaUltimacompra, producto.FechaCreacion, producto.Estado, producto_marca.Nombre AS NombreMarca, proveedor.RazonSocial,
                    dbo.producto.MostrarCatalogo
                    FROM dbo.producto
                    LEFT JOIN producto_marca ON producto.idMarca = producto_marca.idMarca
                    LEFT JOIN proveedor ON producto.idProveedor = proveedor.idProveedor " & filtro & " ORDER BY producto.Nombre"


        sqlProducto.SelectCommand = scomando
        dgvData.PageSize = cboMostrar.SelectedValue
        dgvData.DataBind()
    End Sub
    Protected Sub dgvData_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles dgvData.PageIndexChanging
        dgvData.PageIndex = e.NewPageIndex
        CargarDatosGridView()
    End Sub
    Private Sub dgvData_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvData.RowCommand
        Dim nProducto As Integer = 0
        If e.CommandName = "editar" Then
            Dim indice As Integer
            indice = Convert.ToInt32(e.CommandArgument)
            Dim filaSeleccionada As GridViewRow = dgvData.Rows(indice)
            nProducto = Convert.ToInt32(dgvData.DataKeys(indice).Value)
            Response.Write("Índice de fila seleccionada: " & indice)
            Response.Write("<br />ID del producto seleccionado: " & nProducto)
            Response.Redirect("producto_lista_ABM.aspx?idProducto=" & nProducto & "&TituloForm=MODIFICAR PRODUCTO &Accion=E")
        ElseIf e.CommandName = "borrar" Then
            Dim indice As Integer
            indice = Convert.ToInt32(e.CommandArgument)
            Response.Redirect("producto_lista_ABM.aspx?idProducto=" & Convert.ToInt32(dgvData.DataKeys(indice).Value) & "&TituloForm=ELIMINAR PRODUCTO &Accion=B")
        ElseIf e.CommandName = "barcode" Then
            Dim indice As Integer
            indice = Convert.ToInt32(e.CommandArgument)
            Response.Redirect("producto_Etiquetas.aspx?idProducto=" & Convert.ToInt32(dgvData.DataKeys(indice).Value))
        ElseIf e.CommandName = "inventario" Then
            Dim indice As Integer
            indice = Convert.ToInt32(e.CommandArgument)
            Response.Redirect("producto_inventario.aspx?idProducto=" & Convert.ToInt32(dgvData.DataKeys(indice).Value) & "&idSucursal=" & nSuc & " ")
        ElseIf e.CommandName = "fotos" Then
            Dim indice As Integer
            indice = Convert.ToInt32(e.CommandArgument)
            Response.Redirect("producto_fotos.aspx?idProducto=" & Convert.ToInt32(dgvData.DataKeys(indice).Value) & "&TituloForm=FOTOS PRODUCTO &Accion=F")
        Else
        End If
    End Sub
    Protected Sub BtnNuevo_Click(sender As Object, e As EventArgs) Handles BtnNuevo.Click
        Response.Redirect("producto_Lista_ABM.aspx?idProducto=0&TituloForm=PRODUCTO NUEVO&Accion=N")
    End Sub

    Private Sub txtBuscar_TextChanged(sender As Object, e As EventArgs) Handles txtBuscar.TextChanged
        If IsPostBack Then
            CargarDatosGridView()
        End If

    End Sub

    Private Sub cboMostrar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboMostrar.SelectedIndexChanged
        CargarDatosGridView()
    End Sub

End Class