Imports System.Data.SqlClient
Imports CapaDatos
Imports CapaDatos.Conexion
Imports capaEntidad
Imports capaNegocio

Public Class Sucursales_lista
    Inherits System.Web.UI.Page
    Public Property sTitulo As String
    Public Property sAccion As String
    Public Property nIdSucursal As Integer
    Dim HayError As Boolean = False
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("sNombreUsuario") = "" Then
            Response.Redirect("~/login.aspx")
        End If

        If IsPostBack Then
        Else
            PanelDatos.Enabled = True
            sTitulo = Request.QueryString("TituloForm")
            sAccion = Request.QueryString("Accion")
            nIdSucursal = Request.QueryString("IdSucursal")
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
                txtidSucursal.Value = 0
                txtNombre.Text = ""
                txtDireccion.Text = ""
                txtCodigoPostal.Text = ""
                txtLocalidad.Text = ""
                txtProvincia.Text = ""
                txtTelefono.Text = ""
                txtCelular.Text = ""
                txtEmail.Text = ""
                txtInstagram.Text = ""
                chkEsCentral.Checked = False
                chkPuedeComprar.Checked = False
            Else
                Dim listaSuc As List(Of sucursal) = New CN_Sucursal().Listar(nIdSucursal)
                If listaSuc.Count >= 0 Then
                    txtidSucursal.Value = listaSuc(0).idSucursal
                    txtNombre.Text = listaSuc(0).Nombre
                    txtDireccion.Text = listaSuc(0).Direccion
                    txtCodigoPostal.Text = listaSuc(0).CodigoPostal
                    txtLocalidad.Text = listaSuc(0).Localidad
                    txtProvincia.Text = listaSuc(0).Provincia
                    txtTelefono.Text = listaSuc(0).Telefono
                    txtCelular.Text = listaSuc(0).Celular
                    txtEmail.Text = listaSuc(0).Email
                    txtInstagram.Text = listaSuc(0).Instagram
                    chkPuedeComprar.Checked = listaSuc(0).PuedeComprar
                    chkEsCentral.Checked = listaSuc(0).EsCentral
                    ''************************************************************************
                    Dim valorCelda As Integer

                    For Each item As ListItem In cboEstado.Items

                        If Integer.TryParse(listaSuc(0).Estado, valorCelda) Then
                            ' Compara el valor de la celda con el valor del ítem del ComboBox.
                            If item.Value = valorCelda.ToString() Then
                                txtIdEstado.Value = listaSuc(0).Estado
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
        HayError = False
        If Trim(txtNombre.Text) = "" Then
            lblMensajeAtencion.Text = "El nombre del usuario no puede ser vacio"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowModal", "$(document).ready(function() { $('#MdlAtencion').modal('show'); });", True)
            HayError = True
            Exit Sub
        End If
        If Trim(txtDireccion.Text) = "" Then
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


        Dim objSuc As sucursal = New sucursal() With {
            .idSucursal = Convert.ToInt32(txtidSucursal.Value),
            .Nombre = UCase(txtNombre.Text),
            .Direccion = txtDireccion.Text,
            .Localidad = txtLocalidad.Text,
            .Celular = txtCelular.Text,
            .CodigoPostal = txtCodigoPostal.Text,
            .Provincia = txtProvincia.Text,
            .Telefono = txtTelefono.Text,
            .Email = txtEmail.Text,
            .Instagram = txtInstagram.Text,
            .PuedeComprar = chkPuedeComprar.Checked,
            .EsCentral = chkEsCentral.Checked,
            .Estado = Convert.ToInt32(cboEstado.SelectedValue)}
        Dim idgenerado As Integer = 0


        Using conn As New SqlClient.SqlConnection(Conexion.conectar.Cadena)
            Dim query As String = ""
            Dim cmd As SqlCommand
            Dim mensaje As String = ""
            Dim resultado As Boolean = False
            sAccion = Request.QueryString("Accion")
            If Convert.ToInt32(txtidSucursal.Value) = 0 Then
                idgenerado = New CD_Sucursal().Registrar(objSuc, mensaje)
                If mensaje = "" Then
                    Response.Redirect("Suculsales_Lista.aspx")
                Else
                    lblMensajeAtencion.Text = mensaje
                    script = "$(function() { showModalMensaje(); }); "
                    ScriptManager.RegisterStartupScript(Me, Page.GetType(), "btn1", script, True)
                End If
            Else
                If sAccion = "E" Then
                    resultado = New CD_Sucursal().Editar(objSuc, mensaje)
                ElseIf sAccion = "B" Then
                    resultado = New CD_Sucursal().Eliminar(objSuc, mensaje)
                Else
                    lblMensajeAtencion.Text = "No hay accion para realizar."
                    script = "$(function() { showModalMensaje(); }); "
                    ScriptManager.RegisterStartupScript(Me, Page.GetType(), "btn1", script, True)
                End If
                If resultado Then
                    Response.Redirect("Suculsales_Lista.aspx")
                Else
                    lblMensajeAtencion.Text = mensaje
                    script = "$(function() { showModalMensaje(); }); "
                    ScriptManager.RegisterStartupScript(Me, Page.GetType(), "btn1", script, True)
                End If
            End If

        End Using
    End Sub
End Class