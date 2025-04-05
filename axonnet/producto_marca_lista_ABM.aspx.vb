
Imports CapaDatos
Imports capaEntidad
Imports capaNegocio
Imports System.Data.SqlClient
Imports System.IO
Public Class producto_marca_lista_ABM
    Inherits System.Web.UI.Page
    Public Property sTitulo As String
    Public Property sAccion As String
	Public Property nIdMarca As Integer
	Dim HayError As Boolean = False
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If IsPostBack Then
		Else
			PanelDatos.Enabled = True
			sTitulo = Request.QueryString("TituloForm")
			sAccion = Request.QueryString("Accion")
			nIdMarca = Request.QueryString("idMarca")


			cboEstado.Items.Insert(0, New ListItem("Activo", 1))
			cboEstado.Items.Insert(1, New ListItem("No Activo", 0))
			cboEstado.DataTextField = "Activo"
			cboEstado.DataValueField = 1
			cboEstado.SelectedIndex = 0

			If sAccion = "N" Then
				txtIdMarca.Text = 0
				txtNombre.Text = ""

			Else

				Dim idRol As Integer = 0
				Dim listacategoria As List(Of marca) = New CN_Marca().Listar(nIdMarca)
				If listacategoria.Count >= 0 Then
					txtIdMarca.Text = listacategoria(0).idMarca
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
			lblMensajeAtencion.Text = "El nombre de la marca no puede ser vacio"
			ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowModal", "$(document).ready(function() { $('#MdlAtencion').modal('show'); });", True)
			HayError = True
			Exit Sub
		End If

		Dim objMarca As marca = New marca() With {
			.idMarca = Convert.ToInt32(txtIdMarca.Text),
			.Nombre = UCase(txtNombre.Text),
			.Estado = Convert.ToInt32(cboEstado.SelectedValue)}
		Dim idgenerado As Integer = 0


		Using conn As New SqlClient.SqlConnection(Conexion.conectar.Cadena)
			Dim query As String = ""
			Dim cmd As SqlCommand
			Dim mensaje As String = ""
			Dim resultado As Boolean = False
			sAccion = Request.QueryString("Accion")
			If Convert.ToInt32(txtIdMarca.Text) = 0 Then
				idgenerado = New CD_Marca().Registrar(objMarca, mensaje)
				If mensaje = "" Then
					Response.Redirect("producto_marca_lista.aspx")
				Else
					lblMensajeAtencion.Text = mensaje
					script = "$(function() { showModalMensaje(); }); "
					ScriptManager.RegisterStartupScript(Me, Page.GetType(), "btn1", script, True)
				End If
			Else
				If sAccion = "E" Then
					resultado = New CD_Marca().Editar(objMarca, mensaje)
				ElseIf sAccion = "B" Then

					resultado = New CD_Marca().Eliminar(objMarca, mensaje)
				Else
					lblMensajeAtencion.Text = "No hay accion para realizar."
					script = "$(function() { showModalMensaje(); }); "
					ScriptManager.RegisterStartupScript(Me, Page.GetType(), "btn1", script, True)
				End If
				If resultado Then
					Response.Redirect("producto_marca_lista.aspx")
				Else
					lblMensajeAtencion.Text = mensaje
					script = "$(function() { showModalMensaje(); }); "
					ScriptManager.RegisterStartupScript(Me, Page.GetType(), "btn1", script, True)
				End If
			End If

		End Using
	End Sub
End Class