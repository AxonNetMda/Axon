Imports System.Data.SqlClient
Imports capaEntidad
Imports capaNegocio
Imports CapaDatos
Imports CapaDatos.Conexion
Public Class monedas_lista
    Inherits System.Web.UI.Page
    Dim sqlQuery As String
    Dim cnn As New SqlConnection(conectar.Cadena)
    Dim scomando As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("sNombreUsuario") Is Nothing Then
            Response.Redirect("~/login.aspx")
        End If
        If IsPostBack Then
        Else

        End If
        CargarInicioGrilla()
    End Sub
    Private Sub CargarInicioGrilla()
        scomando = "SELECT * FROM moneda ORDER BY Nombre"
        sqlMonedas.SelectCommand = scomando

    End Sub

    Private Sub dgvData_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvData.RowCommand

        If e.CommandName = "editar" Then
            Dim indice As Integer
            indice = Convert.ToInt32(e.CommandArgument)
            Response.Redirect("moneda_lista_ABM.aspx?Idmoneda=" & Convert.ToInt32(dgvData.Rows(indice).Cells(0).Text) & "&TituloForm=MODIFICAR MONEDA &Accion=E")

        ElseIf e.CommandName = "borrar" Then
            'Dim Script As String = ""
            'modalMensaje = "Error al eliminar este registro"
            'Script = "$(function() { showModalAtencion(); }); "
            'ScriptManager.RegisterStartupScript(Me, Page.GetType(), "btn1", Script, True)
            Dim indice As Integer
            indice = Convert.ToInt32(e.CommandArgument)
            Response.Redirect("moneda_lista_ABM.aspx?idMoneda=" & Convert.ToInt32(dgvData.Rows(indice).Cells(0).Text) & "&TituloForm=ELIMINAR MONEDA &Accion=B")
        End If
    End Sub

    Protected Sub BtnNuevo_Click(sender As Object, e As EventArgs) Handles BtnNuevo.Click
        Response.Redirect("moneda_lista_ABM.aspx?idmoneda=0&TituloForm=MONEDA NUEVA&Accion=N")
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
                filtro = " WHERE moneda.Nombre LIKE '%" & txtBuscar.Text & "%'"
            End If
            sqlMonedas.SelectCommand = scomando & filtro
            dgvData.DataBind()
        End If
    End Sub
End Class