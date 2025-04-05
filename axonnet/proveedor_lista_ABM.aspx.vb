Imports System.Data.SqlClient
Imports CapaDatos
Imports capaEntidad
Imports capaNegocio
Public Class proveedor_lista_ABM
    Inherits System.Web.UI.Page
    Public Property sTitulo As String
    Public Property sAccion As String
    Public Property nidProveedor As Integer = 0
    Dim HayError As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
        Else
            PanelDatos.Enabled = True
            sTitulo = Request.QueryString("TituloForm")
            sAccion = Request.QueryString("Accion")
            nidProveedor = Request.QueryString("idProveedor")
            lblTitulo.Text = sTitulo
            Dim i As Integer = 0


            cboEstado.Items.Insert(0, New ListItem("Activo", 1))
            cboEstado.Items.Insert(1, New ListItem("No Activo", 0))
            cboEstado.DataTextField = "Activo"
            cboEstado.DataValueField = 1
            cboEstado.SelectedIndex = 0


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

            Dim listaemp As List(Of proveedor) = New CN_Proveedor().Listar(nidProveedor)

            If nidProveedor = 0 Then
                txtidProveedor.Value = 0
                txtRazonSocial.Text = ""
                txtNombreComercial.Text = ""
                txtDireccion.Text = ""
                txtCodigoPostal.Text = ""
                txtLocalidad.Text = ""
                txtProvincia.Text = ""
                txtTelefono.Text = ""
                txtCelular.Text = ""
                txtWhatsApp.Text = ""
                txtEmail.Text = ""
                cboResponsables.SelectedIndex = 0
                cboTipoDocumento.SelectedIndex = 0
                txtNumeroDocumento.Text = ""
                txtFechaAlta.Text = String.Format("{0:yyyy-MM-dd}", Date.Today)
                txtSaldo.Text = 0

            Else
                Dim dia As DateTime = Convert.ToDateTime(listaemp(0).FechaAlta)
                Dim valorCelda As Integer
                txtidProveedor.Value = listaemp(0).idProveedor
                txtRazonSocial.Text = listaemp(0).RazonSocial
                txtNombreComercial.Text = listaemp(0).NombreComercial
                txtDireccion.Text = listaemp(0).Direccion
                txtCodigoPostal.Text = listaemp(0).CodigoPostal
                txtLocalidad.Text = listaemp(0).Localidad
                txtProvincia.Text = listaemp(0).Provincia
                txtTelefono.Text = listaemp(0).Telefono
                txtCelular.Text = listaemp(0).Celular
                txtWhatsApp.Text = listaemp(0).WhatsApp
                txtEmail.Text = listaemp(0).Email

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

        Dim objproveedor As proveedor = New proveedor() With {
           .idProveedor = Convert.ToInt32(txtidProveedor.Value),
           .RazonSocial = UCase(txtRazonSocial.Text),
           .NombreComercial = UCase(txtNombreComercial.Text),
           .Direccion = txtDireccion.Text,
           .CodigoPostal = txtCodigoPostal.Text,
           .Localidad = txtLocalidad.Text,
           .Provincia = txtProvincia.Text,
           .Telefono = txtTelefono.Text,
           .celular = txtCelular.Text,
           .WhatsApp = txtWhatsApp.Text,
           .Email = txtEmail.Text,
           .oTipoResponsable = New tiposresponsables() With {.idTipoResponsable = Convert.ToInt32(cboResponsables.SelectedValue)},
           .oTipoDocumento = New TipoDocumento() With {.idTipoDocumento = Convert.ToInt32(cboTipoDocumento.SelectedValue)},
           .NumeroDocumento = txtNumeroDocumento.Text,
           .FechaAlta = FormatDateTime(txtFechaAlta.Text, vbShortDate),
           .Saldo = Convert.ToDecimal(txtSaldo.Text),
           .estado = Convert.ToInt32(cboEstado.SelectedValue)}
        Dim idgenerado As Integer = 0


        Using conn As New SqlClient.SqlConnection(Conexion.conectar.Cadena)
            Dim query As String = ""
            Dim cmd As SqlCommand
            Dim mensaje As String = ""
            Dim resultado As Boolean = False
            mensaje = ""
            If Convert.ToInt32(txtidProveedor.Value) = 0 Then
                idgenerado = New CD_Proveedor().Registrar(objproveedor, mensaje)
            Else
                resultado = New CD_Proveedor().Editar(objproveedor, mensaje)
            End If

            If mensaje = "" Then
                Response.Redirect("proveedor_lista.aspx")
            Else
                lblMensajeAtencion.Text = mensaje
            End If
            script = "$(function() { showModalMensaje(); }); "
            ScriptManager.RegisterStartupScript(Me, Page.GetType(), "btn1", script, True)
        End Using
    End Sub
End Class