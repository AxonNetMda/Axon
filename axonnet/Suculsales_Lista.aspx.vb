Imports System.Data.SqlClient
Imports capaEntidad
Imports capaNegocio
Imports CapaDatos
Imports CapaDatos.Conexion

Public Class Suculsales_Lista
    Inherits System.Web.UI.Page

    Dim sqlQuery As String
    Dim cnn As New SqlConnection(conectar.Cadena)
    Dim scomando As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
        Else

        End If
        CargarInicioGrilla()
    End Sub
    Private Sub CargarInicioGrilla()
        scomando = "SELECT * FROM sucursales ORDER BY Nombre"
        sqlSucursales.SelectCommand = scomando

    End Sub

    Private Sub dgvData_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvData.RowCommand

        If e.CommandName = "editar" Then
            Dim indice As Integer
            indice = Convert.ToInt32(e.CommandArgument)
            Response.Redirect("Sucursales_ABM.aspx?IdSucursal=" & Convert.ToInt32(dgvData.Rows(indice).Cells(0).Text) & "&TituloForm=MODIFICAR SUCURSAL &Accion=E")

        ElseIf e.CommandName = "borrar" Then
            'Dim Script As String = ""
            'modalMensaje = "Error al eliminar este registro"
            'Script = "$(function() { showModalAtencion(); }); "
            'ScriptManager.RegisterStartupScript(Me, Page.GetType(), "btn1", Script, True)
            Dim indice As Integer
            indice = Convert.ToInt32(e.CommandArgument)
            Response.Redirect("Sucursales_ABM.aspx?IdSucursal=" & Convert.ToInt32(dgvData.Rows(indice).Cells(0).Text) & "&TituloForm=ELIMINAR SUCURSAL &Accion=B")
        End If
    End Sub

    Protected Sub BtnNuevo_Click(sender As Object, e As EventArgs) Handles BtnNuevo.Click
        Response.Redirect("Sucursales_ABM.aspx?idUsuario=0&TituloForm=SUCURSAL NUEVA &Accion=N")
        'Exit Sub
        'limpiar()
        'Dim Script As String = ""
        'Script = "$(function() { showModalNuevo(); }); "
        'ScriptManager.RegisterStartupScript(Me, Page.GetType(), "btn1", Script, True)
    End Sub



    Protected Sub txtBuscar_TextChanged(sender As Object, e As EventArgs) Handles txtBuscar.TextChanged
        If IsPostBack Then
            Dim filtro As String = ""
            'Dim dt As DataTable = ObtenerDatos()
            'Dim dv As New DataView(dt)
            'dv.RowFilter = String.Format("Nombre LIKE '%{0}%' OR Domicilio LIKE '%{0}%'", txtBuscar.Text)
            If Trim(txtBuscar.Text) <> "" Then
                filtro = " WHERE sucursales.Nombre LIKE '%" & txtBuscar.Text & "%' OR sucursales.email LIKE '%" & txtBuscar.Text & "%'"
            End If
            sqlSucursales.SelectCommand = scomando & filtro
            dgvData.DataBind()
        End If
    End Sub

    Protected Sub Editar_Click(sender As Object, e As EventArgs)
        Dim btnEditar As Button = CType(sender, Button)
        Dim fila As GridViewRow = CType(btnEditar.NamingContainer, GridViewRow)
        Dim idSucursal As String = dgvData.DataKeys(fila.RowIndex).Value.ToString()

        ' Redirigir a la página de edición con el ID de la sucursal
        Response.Redirect("Sucursales_ABM.aspx?IdSucursal=" & Convert.ToInt32(idSucursal) & "&TituloForm=MODIFICAR SUCURSAL &Accion=E")

    End Sub

    Protected Sub Borrar_Click(sender As Object, e As EventArgs)
        Dim btnBorrar As Button = CType(sender, Button)
        Dim fila As GridViewRow = CType(btnBorrar.NamingContainer, GridViewRow)
        Dim idSucursal As String = dgvData.DataKeys(fila.RowIndex).Value.ToString()
        Response.Redirect("Sucursales_ABM.aspx?IdSucursal=" & Convert.ToInt32(idSucursal) & "&TituloForm=ELIMINAR SUCURSAL &Accion=B")

    End Sub
End Class