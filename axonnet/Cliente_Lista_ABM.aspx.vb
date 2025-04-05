Imports System.Data.SqlClient
Imports CapaDatos
Imports capaEntidad
Imports capaNegocio
Public Class Cliente_Lista_ABM
    Inherits System.Web.UI.Page
    Public Property sTitulo As String
    Public Property sAccion As String
    Public Property nIdCliente As Integer = 0
    Dim HayError As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
        Else
            PanelDatos.Enabled = True
            sTitulo = Request.QueryString("TituloForm")
            sAccion = Request.QueryString("Accion")
            nIdCliente = Request.QueryString("idCliente")
            lblTitulo.Text = sTitulo
            Dim i As Integer = 0


            cboEstado.Items.Insert(0, New ListItem("Activo", 1))
            cboEstado.Items.Insert(1, New ListItem("No Activo", 0))
            cboEstado.DataTextField = "Activo"
            cboEstado.DataValueField = 1
            cboEstado.SelectedIndex = 0

            cboMovil.Items.Insert(0, New ListItem("Movil", 1))
            cboMovil.Items.Insert(1, New ListItem("Fijo", 0))
            cboMovil.DataTextField = "Movil"
            cboMovil.DataValueField = 1
            cboMovil.SelectedIndex = 0

            Dim listapais As List(Of Pais) = New CD_Pais().Listar(0)
            For item = 0 To listapais.Count - 1
                cboPais.Items.Insert(item, New ListItem(listapais(item).NombrePais, listapais(item).idPais))
            Next
            cboPais.SelectedIndex = 0



            Dim listResponsable As List(Of tiposresponsables) = New CN_TipoResponsable().Listar()
            For item = 0 To listResponsable.Count - 1
                cboResponsables.Items.Insert(item, New ListItem(listResponsable(item).Nombre, listResponsable(item).idTipoResponsable))
            Next
            cboResponsables.SelectedIndex = 0

            Dim listdocumento As List(Of TipoDocumento) = New CN_Tipodocumentos().Listar()
            For item = 0 To listdocumento.Count - 1
                cboTipoDocumento.Items.Insert(item, New ListItem(listdocumento(item).Nombre, listdocumento(item).idTipoDocumento))
            Next
            cboTipoDocumento.SelectedIndex = 0

            Dim listaemp As List(Of cliente) = New CD_Cliente().Listar(nIdCliente)

            If nIdCliente = 0 Then
                txtidCliente.Value = 0
                txtApellido.Text = ""
                txtNombre.Text = ""
                txtDireccion.Text = ""
                txtCodigoPostal.Text = ""
                txtLocalidad.Text = ""
                txtProvincia.Text = ""
                txtTelefonoPais.Text = ""
                txtTelefonoArea.Text = ""
                txtTelefonoNumero.Text = ""
                txtEmail.Text = ""
                cboResponsables.SelectedIndex = 0
                cboTipoDocumento.SelectedIndex = 0
                txtNumeroDocumento.Text = ""
                txtFechaAlta.Text = String.Format("{0:yyyy-MM-dd}", Date.Today)
                txtSaldo.Text = 0

            Else
                Dim dia As DateTime = Convert.ToDateTime(listaemp(0).fechaAlta)
                Dim valorCelda As Integer
                txtidCliente.Value = listaemp(0).idcliente
                txtApellido.Text = listaemp(0).apellido
                txtNombre.Text = listaemp(0).nombre
                txtDireccion.Text = listaemp(0).Direccion
                txtCodigoPostal.Text = listaemp(0).CodigoPostal
                txtLocalidad.Text = listaemp(0).Localidad
                txtProvincia.Text = listaemp(0).provincia
                txtTelefonoPais.Text = listaemp(0).TelefonoPais
                txtTelefonoArea.Text = listaemp(0).TelefonoArea
                txtTelefonoNumero.Text = listaemp(0).TelefonoNumero
                txtEmail.Text = listaemp(0).correo

                For Each item As ListItem In cboResponsables.Items
                    If Integer.TryParse(listaemp(0).oTipoResponsable.idTipoResponsable, valorCelda) Then
                        ' Compara el valor de la celda con el valor del ítem del ComboBox.
                        If item.Value = valorCelda.ToString() Then
                            txtidResponsable.Value = listaemp(0).Estado
                            cboResponsables.SelectedValue = item.Value
                            Exit For
                        End If
                    End If
                Next
                For Each item As ListItem In cboTipoDocumento.Items
                    If Integer.TryParse(listaemp(0).oTipoDocumento.idTipoDocumento, valorCelda) Then
                        ' Compara el valor de la celda con el valor del ítem del ComboBox.
                        If item.Value = valorCelda.ToString() Then
                            txtIdTipoDocumento.Value = listaemp(0).Estado
                            cboTipoDocumento.SelectedValue = item.Value
                            Exit For
                        End If
                    End If
                Next
                txtNumeroDocumento.Text = listaemp(0).NumeroDocumento
                txtFechaAlta.Text = String.Format("{0:yyyy-MM-dd}", dia)
                txtSaldo.Text = FormatNumber(listaemp(0).Saldo, 2)

                For Each item As ListItem In cboEstado.Items

                    If Integer.TryParse(listaemp(0).Estado, valorCelda) Then
                        ' Compara el valor de la celda con el valor del ítem del ComboBox.
                        If item.Value = valorCelda.ToString() Then
                            txtIdEstado.Value = listaemp(0).Estado
                            cboEstado.SelectedValue = item.Value
                            Exit For
                        End If
                    End If
                Next
                For Each item As ListItem In cboPais.Items

                    If Integer.TryParse(listaemp(0).pais, valorCelda) Then
                        ' Compara el valor de la celda con el valor del ítem del ComboBox.
                        If item.Value = valorCelda.ToString() Then
                            'txtIdEstado.Value = listaemp(0).Estado
                            cboPais.SelectedValue = item.Value
                            Exit For
                        End If
                    End If
                Next
                For Each item As ListItem In cboMovil.Items

                    If Integer.TryParse(listaemp(0).TelefonoMovil, valorCelda) Then
                        ' Compara el valor de la celda con el valor del ítem del ComboBox.
                        If item.Value = valorCelda.ToString() Then
                            'txtIdEstado.Value = listaemp(0).Estado
                            cboMovil.SelectedValue = item.Value
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

    End Sub

    Protected Sub BtnGuardar_Click(sender As Object, e As EventArgs)
        Dim script As String = ""
        HayError = False

        Dim objcliente As cliente = New cliente() With {
           .idcliente = Convert.ToInt32(txtidCliente.Value),
           .apellido = UCase(txtApellido.Text),
           .nombre = UCase(txtNombre.Text),
           .Direccion = txtDireccion.Text,
           .pais = cboPais.SelectedItem.Text,
           .codigopostal = txtCodigoPostal.Text,
           .localidad = txtLocalidad.Text,
           .provincia = txtProvincia.Text,
           .TelefonoPais = txtTelefonoPais.Text,
           .TelefonoArea = txtTelefonoArea.Text,
           .TelefonoNumero = txtTelefonoNumero.Text,
           .correo = txtEmail.Text,
           .oTipoResponsable = New tiposresponsables() With {.idTipoResponsable = Convert.ToInt32(cboResponsables.SelectedValue)},
           .TelefonoMovil = cboMovil.SelectedValue,
           .oTipoDocumento = New TipoDocumento() With {.idTipoDocumento = Convert.ToInt32(cboTipoDocumento.SelectedValue)},
           .NumeroDocumento = txtNumeroDocumento.Text,
           .fechaAlta = FormatDateTime(txtFechaAlta.Text, vbShortDate),
           .Saldo = Convert.ToDecimal(txtSaldo.Text),
           .Estado = Convert.ToInt32(cboEstado.SelectedValue)}
        Dim idgenerado As Integer = 0


        Using conn As New SqlClient.SqlConnection(Conexion.conectar.Cadena)
            Dim query As String = ""
            Dim mensaje As String = ""
            Dim resultado As Boolean = False
            mensaje = ""
            If Convert.ToInt32(txtidCliente.Value) = 0 Then
                idgenerado = New CD_Cliente().Registrar(objcliente, mensaje)
            Else
                resultado = New CD_Cliente().Editar(objcliente, mensaje)
            End If

            If mensaje = "" Then
                Response.Redirect("cliente_lista.aspx")
            Else
                lblMensajeAtencion.Text = mensaje
            End If
            script = "$(function() { showModalAtencion(); }); "
            ScriptManager.RegisterStartupScript(Me, Page.GetType(), "btn1", script, True)
        End Using
    End Sub

End Class