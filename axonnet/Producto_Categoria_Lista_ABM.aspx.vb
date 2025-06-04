Imports CapaDatos
Imports capaEntidad
Imports capaNegocio
Imports System.Data.SqlClient
Imports System.IO

Public Class Producto_Categoria_ABM
    Inherits System.Web.UI.Page
    Public Property sTitulo As String
    Public Property sAccion As String
	Public Property nIdCategoria As Integer
	Dim HayError As Boolean = False
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If IsPostBack Then
		Else
			PanelDatos.Enabled = True
			sTitulo = Request.QueryString("TituloForm")
			sAccion = Request.QueryString("Accion")
			nIdCategoria = Request.QueryString("idCategoria")


			cboEstado.Items.Insert(0, New ListItem("Activo", 1))
			cboEstado.Items.Insert(1, New ListItem("No Activo", 0))
			cboEstado.DataTextField = "Activo"
			cboEstado.DataValueField = 1
			cboEstado.SelectedIndex = 0

			If sAccion = "N" Then
				txtIdCategoria.Text = 0
				txtNombre.Text = ""

			Else

				Dim idRol As Integer = 0
				Dim listacategoria As List(Of categoria) = New CN_Categoria().Listar(nIdCategoria)
				If listacategoria.Count >= 0 Then
					txtIdCategoria.Text = listacategoria(0).idCategoria
					txtNombre.Text = listacategoria(0).Nombre
					Dim valorCelda As Integer
					For Each item As ListItem In cboEstado.Items

						If Integer.TryParse(listacategoria(0).Estado, valorCelda) Then
							' Compara el valor de la celda con el valor del ítem del ComboBox.
							If item.Value = valorCelda.ToString() Then
								txtIdEstado.Value = listacategoria(0).Estado
								cboEstado.SelectedValue = item.Value
								Exit For
							End If
						End If
					Next
					If sAccion = "B" Then
						PanelDatos.Enabled = False
					End If
				End If
			End If
		End If

	End Sub
	Protected Sub BtnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs)
		Dim script As String = ""
		HayError = False
		If Trim(txtNombre.Text) = "" Then
			lblMensajeAtencion.Text = "El nombre de la categoria no puede ser vacio"
			ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowModal", "$(document).ready(function() { $('#MdlAtencion').modal('show'); });", True)
			HayError = True
			Exit Sub
		End If

		Dim objcategoria As categoria = New categoria() With {
			.idCategoria = Convert.ToInt32(txtIdCategoria.Text),
			.Nombre = UCase(txtNombre.Text),
			.Estado = Convert.ToInt32(cboEstado.SelectedValue)}
		Dim idusuariogenerado As Integer = 0


		Using conn As New SqlClient.SqlConnection(Conexion.conectar.Cadena)
			Dim query As String = ""
			Dim cmd As SqlCommand
			Dim mensaje As String = ""
			Dim resultado As Boolean = False
			sAccion = Request.QueryString("Accion")
			If Convert.ToInt32(txtIdCategoria.Text) = 0 Then
				idusuariogenerado = New CD_Categoria().Registrar(objcategoria, mensaje)
				If mensaje = "" Then
					Response.Redirect("producto_categoria_lista.aspx")
				Else
					lblMensajeAtencion.Text = mensaje
					script = "$(function() { showModalMensaje(); }); "
					ScriptManager.RegisterStartupScript(Me, Page.GetType(), "btn1", script, True)
				End If
			Else
				If sAccion = "E" Then
					resultado = New CD_Categoria().Editar(objcategoria, mensaje)
				ElseIf sAccion = "B" Then


					resultado = New CD_Categoria().Eliminar(objcategoria, mensaje)
				Else
					lblMensajeAtencion.Text = "No hay accion para realizar."
					script = "$(function() { showModalMensaje(); }); "
					ScriptManager.RegisterStartupScript(Me, Page.GetType(), "btn1", script, True)
				End If
				If resultado Then
					Response.Redirect("producto_categoria_lista.aspx")
				Else
					lblMensajeAtencion.Text = mensaje
					script = "$(function() { showModalMensaje(); }); "
					ScriptManager.RegisterStartupScript(Me, Page.GetType(), "btn1", script, True)
				End If
			End If

		End Using
	End Sub

End Class