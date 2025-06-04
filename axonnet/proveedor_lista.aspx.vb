
Public Class proveedor_lista
    Inherits System.Web.UI.Page
    Public Property scomando As String
    Public Property sMensaje As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        scomando = "SELECT  * FROM proveedor where idproveedor>1 ORDER BY RazonSocial"
        SqlProveedor.SelectCommand = scomando
        dgvData.PageSize = cboMostrar.SelectedValue
    End Sub
    Private Sub dgvData_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvData.RowCommand

        If e.CommandName = "editar" Then
            Dim indice As Integer
            indice = Convert.ToInt32(e.CommandArgument)
            Response.Redirect("proveedor_lista_ABM.aspx?idProveedor=" & Convert.ToInt32(dgvData.Rows(indice).Cells(0).Text) & "&TituloForm=MODIFICAR PROVEEDOR &Accion=E")
        ElseIf e.CommandName = "borrar" Then
            Dim indice As Integer
            indice = Convert.ToInt32(e.CommandArgument)
            Response.Redirect("proveedor_lista_ABM.aspx?idProveedor=" & Convert.ToInt32(dgvData.Rows(indice).Cells(0).Text) & "&TituloForm=ELIMINAR PROVEEDOR &Accion=B")
        End If
    End Sub
    Protected Sub BtnNuevo_Click(sender As Object, e As EventArgs) Handles BtnNuevo.Click
        Response.Redirect("proveedor_Lista_ABM.aspx?idProveedor=0&TituloForm=PROVEEDOR NUEVO&Accion=N")
    End Sub
    Protected Sub txtBuscar_TextChanged(sender As Object, e As EventArgs) Handles txtBuscar.TextChanged
        If IsPostBack Then
            Dim filtro As String = ""
            If Trim(txtBuscar.Text) <> "" Then
                filtro = " WHERE RazonSocial LIKE '%" & txtBuscar.Text & "%'"
            End If
            SqlProveedor.SelectCommand = "SELECT * FROM proveedor " & filtro & " ORDER BY RazonSocial"
            dgvData.DataBind()
        End If
    End Sub

End Class