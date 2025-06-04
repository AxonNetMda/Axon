Imports CapaDatos
Imports capaEntidad
Imports capaNegocio
Imports System.Data.SqlClient
Imports System.IO

Public Class Usuarios_Lista_ABM
	Inherits System.Web.UI.Page
	Public Property sTitulo As String
	Public Property sAccion As String
	Public Property nIdusuario As Integer
	Dim HayError As Boolean = False
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If Session("sNombreUsuario") Is Nothing Then
			Response.Redirect("~/login.aspx")
		End If

		If IsPostBack Then
		Else
			PanelDatos.Enabled = True
			sTitulo = Request.QueryString("TituloForm")
			sAccion = Request.QueryString("Accion")
			nIdusuario = Request.QueryString("idUsuario")
			'imgMostrar.ImageUrl = "Imagenes/no-image.jpg" ' Ruta de la imagen actual

			Dim i As Integer = 0
			Dim listarol As List(Of rol) = New CD_Rol().Listar
			For Each item As rol In listarol
				cboRol.Items.Insert(i, New ListItem(item.Nombre, item.idRol))
				i += 1
			Next

			cboRol.DataTextField = "Nombre"
			cboRol.DataValueField = "idRol"
			cboRol.SelectedIndex = 0

			cboEstado.Items.Insert(0, New ListItem("Activo", 1))
			cboEstado.Items.Insert(1, New ListItem("No Activo", 0))
			cboEstado.DataTextField = "Activo"
			cboEstado.DataValueField = 1
			cboEstado.SelectedIndex = 0
			hNombreImagen.Value = "no-image.jpg"
			If sAccion = "N" Then
				imagen.ImageUrl = "imagenes/no-image.jpg"

			Else

				nIdusuario = Request.QueryString("idUsuario")
				Dim idRol As Integer = 0
				Dim listausuario As List(Of usuario) = New CN_Usuario().Listar(nIdusuario)
				If listausuario.Count >= 0 Then
					txtIdUsuario.Text = listausuario(0).idusuario
					txtNombre.Text = listausuario(0).Nombre
						txtDomicilio.Text = listausuario(0).Domicilio
						txtCodigoPostal.Text = listausuario(0).CodigoPostal
						txtLocalidad.Text = listausuario(0).Localidad
						txtProvincia.Text = listausuario(0).Provincia
						txtCelular.Text = listausuario(0).Celular
						txtEmail.Text = listausuario(0).Email
						hNombreImagen.Value = listausuario(0).FotoNombre

					''************************************************************************
					Dim valorCelda As Integer
						For Each item As ListItem In cboRol.Items

							If Integer.TryParse(listausuario(0).oRol.idRol, valorCelda) Then
								' Compara el valor de la celda con el valor del ítem del ComboBox.
								If item.Value = valorCelda.ToString() Then
									txtIdRol.Value = listausuario(0).oRol.idRol
									cboRol.SelectedValue = item.Value
									Exit For
								End If
							End If
						Next
						For Each item As ListItem In cboEstado.Items

							If Integer.TryParse(listausuario(0).Estado, valorCelda) Then
								' Compara el valor de la celda con el valor del ítem del ComboBox.
								If item.Value = valorCelda.ToString() Then
									txtIdEstado.Value = listausuario(0).Estado
									cboEstado.SelectedValue = item.Value
									Exit For
								End If
							End If
						Next
						Dim simagen As String = "imagenes/no-image.jpg"
						Dim sPathImagen As String = "imagenes/"

						If Not IsDBNull(listausuario(0).FotoNombre) Then
							sPathImagen = listausuario(0).FotoPath
							simagen = listausuario(0).FotoNombre
							hNombreImagen.Value = listausuario(0).FotoNombre
						End If
						imagen.ImageUrl = sPathImagen & listausuario(0).FotoNombre
					If sAccion = "E" Then
					ElseIf sAccion = "B" Then
						PanelDatos.Enabled = False
					End If
				End If
				End If
		End If
	End Sub
	'Protected Sub BtnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs)
	'	Dim script As String = ""

	'	HayError = False
	'	If Trim(txtNombre.Text) = "" Then
	'		lblMensajeAtencion.Text = "El nombre del usuario no puede ser vacio"
	'		ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowModal", "$(document).ready(function() { $('#MdlAtencion').modal('show'); });", True)
	'		HayError = True
	'		Exit Sub
	'	End If
	'	If Trim(txtDomicilio.Text) = "" Then
	'		lblMensajeAtencion.Text = "El domicilio del usuario no puede ser vacio"
	'		ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowModal", "$(document).ready(function() { $('#MdlAtencion').modal('show'); });", True)
	'		HayError = True
	'		Exit Sub
	'	End If
	'	If Trim(txtEmail.Text) = "" Then
	'		lblMensajeAtencion.Text = "El email del usuario no puede ser vacio"
	'		ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowModal", "$(document).ready(function() { $('#MdlAtencion').modal('show'); });", True)
	'		HayError = True
	'		Exit Sub
	'	End If
	'	If hNombreImagen.Value = "" Then
	'		hNombreImagen.Value = "no-image.jpg"
	'	Else


	'	End If
	'	If fileUpload1.HasFile AndAlso hNombreImagen.Value = "no-image.jpg" Then
	'		hNombreImagen.Value = "no-image.jpg"
	'	Else

	'	End If
	'	Dim objusuario As usuario = New usuario() With {
	'		.idusuario = Convert.ToInt32(txtIdUsuario.Text),
	'		.Nombre = UCase(txtNombre.Text),
	'		.Email = txtEmail.Text,
	'		.Clave = "",
	'		.Domicilio = txtDomicilio.Text,
	'		.CodigoPostal = txtCodigoPostal.Text,
	'		.Localidad = txtLocalidad.Text,
	'		.Provincia = txtProvincia.Text,
	'		.Celular = txtCelular.Text,
	'		.FotoNombre = hNombreImagen.Value,
	'		.FotoPath = "\imagenes\",
	'		.oRol = New rol() With {.idRol = Convert.ToInt32(cboRol.SelectedValue)},
	'		.Estado = Convert.ToInt32(cboEstado.SelectedValue)
	'	}
	'	Dim idusuariogenerado As Integer = 0


	'	Using conn As New SqlClient.SqlConnection(Conexion.conectar.Cadena)
	'		Dim query As String = ""
	'		Dim cmd As SqlCommand
	'		Dim mensaje As String = ""
	'		Dim resultado As Boolean = False
	'		sAccion = Request.QueryString("Accion")
	'		If Convert.ToInt32(txtIdUsuario.Text) = 0 Then
	'			idusuariogenerado = New CD_Usuario().Registrar(objusuario, mensaje)
	'			If mensaje = "" Then
	'				Response.Redirect("Usuarios_lista.aspx")
	'			Else
	'				lblMensajeAtencion.Text = mensaje
	'				script = "$(function() { showModalMensaje(); }); "
	'				ScriptManager.RegisterStartupScript(Me, Page.GetType(), "btn1", script, True)
	'			End If
	'		Else
	'			If sAccion = "E" Then
	'				resultado = New CD_Usuario().Editar(objusuario, mensaje)
	'			ElseIf sAccion = "B" Then


	'				resultado = New CD_Usuario().Eliminar(objusuario, mensaje)
	'			Else
	'				lblMensajeAtencion.Text = "No hay accion para realizar."
	'				script = "$(function() { showModalMensaje(); }); "
	'				ScriptManager.RegisterStartupScript(Me, Page.GetType(), "btn1", script, True)
	'			End If
	'			If resultado Then
	'				Response.Redirect("Usuarios_lista.aspx")
	'			Else
	'				lblMensajeAtencion.Text = mensaje
	'				imagen.ImageUrl = "imagenes/no-imagen.jpg"
	'				script = "$(function() { showModalMensaje(); }); "
	'				ScriptManager.RegisterStartupScript(Me, Page.GetType(), "btn1", script, True)
	'			End If
	'		End If

	'	End Using
	'End Sub

	Protected Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click
		Dim idUsuario As Integer = nIdusuario ' Reemplazá con la lógica real
		Dim nombreArchivoFinal As String = "no-image.jpg"

		If fileUpload1.HasFile Then
			Try
				' Obtener la extensión y validar tipo
				Dim extension As String = System.IO.Path.GetExtension(fileUpload1.FileName).ToLower()
				Dim extensionesValidas As String() = {".jpg", ".jpeg", ".png", ".gif"}
				If Not extensionesValidas.Contains(extension) Then
					' Mostrar error si no es válida
					lblMensaje.Text = "Solo se permiten archivos JPG, PNG o GIF."
					Exit Sub
				End If

				' Nombre del archivo: idUsuario + extensión
				nombreArchivoFinal = idUsuario.ToString() & extension

				' Ruta para guardar el archivo
				Dim rutaGuardar As String = Server.MapPath("~/imagenes/" & nombreArchivoFinal)
				fileUpload1.SaveAs(rutaGuardar)

				' Actualizar imagen en pantalla
				imagen.ImageUrl = "~/imagenes/" & nombreArchivoFinal

				' Guardar en HiddenField
				hNombreImagen.Value = nombreArchivoFinal

				' Guardar el nombre en la base de datos (adaptá la lógica a tu estructura)
				GuardarNombreImagenEnBD(idUsuario, nombreArchivoFinal)

			Catch ex As Exception
				lblMensaje.Text = "Error al subir la imagen: " & ex.Message
			End Try
		Else
			lblMensaje.Text = "Debe seleccionar una imagen antes de guardar."
		End If
	End Sub

	Private Sub GuardarNombreImagenEnBD(ByVal nidUsuario As Integer, ByVal archivo As String)
		Dim script As String = ""

		HayError = False
		If Trim(txtNombre.Text) = "" Then
			lblMensajeAtencion.Text = "El nombre del usuario no puede ser vacio"
			ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowModal", "$(document).ready(function() { $('#MdlAtencion').modal('show'); });", True)
			HayError = True
			Exit Sub
		End If
		If Trim(txtDomicilio.Text) = "" Then
			lblMensajeAtencion.Text = "El domicilio del usuario no puede ser vacio"
			ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowModal", "$(document).ready(function() { $('#MdlAtencion').modal('show'); });", True)
			HayError = True
			Exit Sub
		End If
		If Trim(txtEmail.Text) = "" Then
			lblMensajeAtencion.Text = "El email del usuario no puede ser vacio"
			ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowModal", "$(document).ready(function() { $('#MdlAtencion').modal('show'); });", True)
			HayError = True
			Exit Sub
		End If

		If archivo = "no-image.jpg" Then
			hNombreImagen.Value = "no-image.jpg"
		Else

		End If
		Dim objusuario As usuario = New usuario() With {
			.idusuario = Convert.ToInt32(txtIdUsuario.Text),
			.Nombre = UCase(txtNombre.Text),
			.Email = txtEmail.Text,
			.Clave = "",
			.Domicilio = txtDomicilio.Text,
			.CodigoPostal = txtCodigoPostal.Text,
			.Localidad = txtLocalidad.Text,
			.Provincia = txtProvincia.Text,
			.Celular = txtCelular.Text,
			.FotoNombre = archivo,
			.FotoPath = "\imagenes\",
			.oRol = New rol() With {.idRol = Convert.ToInt32(cboRol.SelectedValue)},
			.Estado = Convert.ToInt32(cboEstado.SelectedValue)
		}
		Dim idusuariogenerado As Integer = 0


		Using conn As New SqlClient.SqlConnection(Conexion.conectar.Cadena)
			Dim query As String = ""
			Dim cmd As SqlCommand
			Dim mensaje As String = ""
			Dim resultado As Boolean = False
			sAccion = Request.QueryString("Accion")
			If Convert.ToInt32(txtIdUsuario.Text) = 0 Then
				idusuariogenerado = New CD_Usuario().Registrar(objusuario, mensaje)
				If mensaje = "" Then
					Response.Redirect("Usuarios_lista.aspx")
				Else
					lblMensajeAtencion.Text = mensaje
					Script = "$(function() { showModalMensaje(); }); "
					ScriptManager.RegisterStartupScript(Me, Page.GetType(), "btn1", Script, True)
				End If
			Else
				If sAccion = "E" Then
					resultado = New CD_Usuario().Editar(objusuario, mensaje)
				ElseIf sAccion = "B" Then


					resultado = New CD_Usuario().Eliminar(objusuario, mensaje)
				Else
					lblMensajeAtencion.Text = "No hay accion para realizar."
					Script = "$(function() { showModalMensaje(); }); "
					ScriptManager.RegisterStartupScript(Me, Page.GetType(), "btn1", Script, True)
				End If
				If resultado Then
					Response.Redirect("Usuarios_lista.aspx")
				Else
					lblMensajeAtencion.Text = mensaje
					imagen.ImageUrl = "imagenes/no-imagen.jpg"
					Script = "$(function() { showModalMensaje(); }); "
					ScriptManager.RegisterStartupScript(Me, Page.GetType(), "btn1", Script, True)
				End If
			End If

		End Using

	End Sub

End Class