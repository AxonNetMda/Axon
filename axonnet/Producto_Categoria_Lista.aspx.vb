Imports System.Data.SqlClient
Imports CapaDatos.Conexion

Public Class Categorias_Lista
    Inherits System.Web.UI.Page
    Public Property scomando As String
    Public Property sMensaje As String
    Public Property sTitulo As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        scomando = "SELECT  * FROM producto_categoria ORDER BY Nombre"
        Dim dt As New DataTable()

        Using connection As New SqlConnection(conectar.Cadena)
            Using adapter As New SqlDataAdapter(scomando, connection)
                adapter.Fill(dt)
            End Using
        End Using
        SqlCategoria.SelectCommand = scomando
        'dgvData.DataSource = dt
        dgvData.PageSize = cboMostrar.SelectedValue
        'dgvData.DataBind()

    End Sub
    Private Sub dgvData_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvData.RowCommand

        If e.CommandName = "editar" Then
            Dim indice As Integer
            indice = Convert.ToInt32(e.CommandArgument)
            Response.Redirect("producto_categoria_lista_ABM.aspx?idCategoria=" & Convert.ToInt32(dgvData.Rows(indice).Cells(0).Text) & "&TituloForm=MODIFICAR CATEGORIA &Accion=E")
        ElseIf e.CommandName = "borrar" Then
            Dim indice As Integer
            indice = Convert.ToInt32(e.CommandArgument)
            Response.Redirect("producto_categoria_lista_ABM.aspx?idCategoria=" & Convert.ToInt32(dgvData.Rows(indice).Cells(0).Text) & "&TituloForm=ELIMINAR CATEGORIA &Accion=B")
        End If
    End Sub
    Protected Sub BtnNuevo_Click(sender As Object, e As EventArgs) Handles BtnNuevo.Click
        Response.Redirect("producto_categoria_Lista_ABM.aspx?idCategoria=0&TituloForm=CATEGORIA NUEVO&Accion=N")
    End Sub
    Protected Sub txtBuscar_TextChanged(sender As Object, e As EventArgs) Handles txtBuscar.TextChanged
        If IsPostBack Then
            Dim filtro As String = ""
            'Dim dt As DataTable = ObtenerDatos()
            'Dim dv As New DataView(dt)
            'dv.RowFilter = String.Format("Nombre LIKE '%{0}%' OR Domicilio LIKE '%{0}%'", txtBuscar.Text)
            If Trim(txtBuscar.Text) <> "" Then
                filtro = " WHERE Nombre LIKE '%" & txtBuscar.Text & "%'"
            End If
            SqlCategoria.SelectCommand = "SELECT * FROM producto_categoria " & filtro & " ORDER BY Nombre"
            dgvData.DataBind()
        End If
    End Sub
End Class