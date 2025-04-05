Imports CapaDatos
Imports capaEntidad
Imports capaNegocio
Imports System.Data.SqlClient
Imports System.IO
Public Class moneda_lista_ABM
    Inherits System.Web.UI.Page
    Public Property sTitulo As String
    Public Property sAccion As String
	Public Property nidMoneda As Integer
	Dim HayError As Boolean = False
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If IsPostBack Then
		Else
			PanelDatos.Enabled = True
			sTitulo = Request.QueryString("TituloForm")
			sAccion = Request.QueryString("Accion")
			nidMoneda = Request.QueryString("idMoneda")
			'imgMostrar.ImageUrl = "Imagenes/no-image.jpg" ' Ruta de la imagen actual
			lblTitulo.Text = sTitulo
			Dim i As Integer = 0
			Dim listarol As List(Of rol) = New CD_Rol().Listar


			cboEstado.Items.Insert(0, New ListItem("Activo", 1))
			cboEstado.Items.Insert(1, New ListItem("No Activo", 0))
			cboEstado.DataTextField = "Activo"
			cboEstado.DataValueField = 1
			cboEstado.SelectedIndex = 0
			If sAccion = "N" Then
				txtidMoneda.Value = 0
				txtNombre.Text = ""
				txtCotizacion.Text = 0
				txtFechaCotizacion.Text = Date.Today()
			Else
				Dim listaMoneda As List(Of moneda) = New CN_Moneda().Listar(nidMoneda)
				If listaMoneda.Count >= 0 Then
					Dim dia As DateTime = Convert.ToDateTime(listaMoneda(0).fechaActualizacion)
					txtFechaCotizacion.Text = String.Format("{0:yyyy-MM-dd}", dia)
					txtidMoneda.Value = listaMoneda(0).idMoneda
					txtNombre.Text = listaMoneda(0).Nombre
					txtCotizacion.Text = listaMoneda(0).Cotizacion
					'txtFechaCotizacion.Text = FormatDateTime(listaMoneda(0).fechaActualizacion, vbShortDate)
					''************************************************************************
					Dim valorCelda As Integer

					For Each item As ListItem In cboEstado.Items

						If Integer.TryParse(listaMoneda(0).Estado, valorCelda) Then
							' Compara el valor de la celda con el valor del ítem del ComboBox.
							If item.Value = valorCelda.ToString() Then
								txtIdEstado.Value = listaMoneda(0).Estado
								cboEstado.SelectedValue = item.Value
								Exit For
							End If
						End If
					Next
					If sAccion = "E" Then
						PanelDatos.Enabled = True
					ElseIf sAccion = "B" Then
						PanelDatos.Enabled = False
					End If
				End If
			End If
		End If

	End Sub
	Protected Sub BtnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs)
		Dim script As String = ""
		'If fileUpload1.PostedFile IsNot Nothing AndAlso fileUpload1.PostedFile.ContentLength > 0 Then
		'    Dim fileName As String = Path.GetFileName(fileUpload1.PostedFile.FileName)
		'    Dim fileType As String = fileUpload1.PostedFile.ContentType
		'    Dim fileContent As Byte() = Nothing

		'    Using binaryReader As New BinaryReader(fileUpload1.PostedFile.InputStream)
		'        fileContent = binaryReader.ReadBytes(fileUpload1.PostedFile.ContentLength)
		'    End Using

		'    GuardarImagenEnBaseDeDatos(Val(txtIdUsuario.Text), fileName, fileType, fileContent)
		'End If
		Dim numero As String = txtCotizacion.Text
		' Aquí puedes manejar el número ingresado como desees
		' Por ejemplo, convertirlo a un número decimal para procesamiento
		Dim numeroDecimal As Decimal
		If Decimal.TryParse(numero, numeroDecimal) Then
			' Hacer algo con el número decimal
		Else
			' Manejar el error si el número no es válido
			lblMensajeAtencion.Text = "El numero ingresado tiene un formato incorrecto. VERIFIQUE debe ser ###,#0"
			ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowModal", "$(document).ready(function() { $('#MdlAtencion').modal('show'); });", True)
			HayError = True
			Exit Sub
		End If
		HayError = False
		If Trim(txtNombre.Text) = "" Then
			lblMensajeAtencion.Text = "El nombre del usuario no puede ser vacio"
			ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowModal", "$(document).ready(function() { $('#MdlAtencion').modal('show'); });", True)
			HayError = True
			Exit Sub
		End If

		Dim objMoneda As moneda = New moneda() With {
			.idMoneda = Convert.ToInt32(txtidMoneda.Value),
			.Nombre = UCase(txtNombre.Text),
			.Cotizacion = txtCotizacion.Text,
			.fechaActualizacion = FormatDateTime(txtFechaCotizacion.Text, vbShortDate),
			.Estado = Convert.ToInt32(cboEstado.SelectedValue)}
		Dim idgenerado As Integer = 0


		Using conn As New SqlClient.SqlConnection(Conexion.conectar.Cadena)
			Dim query As String = ""
			Dim cmd As SqlCommand
			Dim mensaje As String = ""
			Dim resultado As Boolean = False
			sAccion = Request.QueryString("Accion")
			If Convert.ToInt32(txtidMoneda.Value) = 0 Then
				idgenerado = New CD_Moneda().Registrar(objMoneda, mensaje)
				If mensaje = "" Then
					Response.Redirect("monedas_lista.aspx")
				Else
					lblMensajeAtencion.Text = mensaje
					script = "$(function() { showModalMensaje(); }); "
					ScriptManager.RegisterStartupScript(Me, Page.GetType(), "btn1", script, True)
				End If
			Else
				If sAccion = "E" Then
					resultado = New CD_Moneda().Editar(objMoneda, mensaje)
				ElseIf sAccion = "B" Then
					resultado = New CD_Moneda().Eliminar(objMoneda, mensaje)
				Else
					lblMensajeAtencion.Text = "No hay accion para realizar."
					script = "$(function() { showModalMensaje(); }); "
					ScriptManager.RegisterStartupScript(Me, Page.GetType(), "btn1", script, True)
				End If
				If resultado Then
					Response.Redirect("monedas_lista.aspx")
				Else
					lblMensajeAtencion.Text = mensaje
					script = "$(function() { showModalMensaje(); }); "
					ScriptManager.RegisterStartupScript(Me, Page.GetType(), "btn1", script, True)
				End If
			End If

		End Using
	End Sub

End Class