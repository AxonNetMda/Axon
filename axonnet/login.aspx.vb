Imports System.Management
Imports capaEntidad
Imports capaNegocio
Imports axonnet.MisFunciones
Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then

        Else
            Dim listasucursales As List(Of sucursal) = New CN_Sucursal().Listar(0)
            cboSucursales.Items.Clear()

            For item = 0 To listasucursales.Count - 1
                cboSucursales.Items.Insert(item, New ListItem(listasucursales(item).Nombre, listasucursales(item).idSucursal))
            Next
            cboSucursales.SelectedIndex = 0

            Session.Add("sNombreSucursal", "")
            Session.Add("email", "")
            Session.Add("idUsuario", 0)
            Session.Add("idSucursal", Convert.ToInt32(cboSucursales.SelectedItem.Value))
            Session.Add("nPuntoVenta", 1)
            Session.Add("sNombreEmpresa", "SIN LIMITES")
            Session.Add("nMoneda", 1)
            Session.Add("sNombreUsuario", "")
            Session.Add("sCodigoAtencion", "")
            sNombreUsuario = ""
            nRol = 0
            sRolNombre = ""
            sCodigoAtencion = ""
            If Request.Browser.IsMobileDevice Then
                Session.Add("movil", 1)
                modalMensaje = "Version Movil"
            Else
                Session.Add("movil", 0)
                modalMensaje = "Version PC"
            End If
        End If
    End Sub

    Protected Sub BtnLogin_Click(sender As Object, e As EventArgs) Handles BtnLogin.Click

        Dim listausuario As List(Of usuario) = New CN_Usuario().Login(txtEmail.Text)
        If listausuario.Count > 0 Then
            Session("email") = txtEmail.Text
            Session("rolnombre") = listausuario.Item(0).oRol.Nombre
            Session("rol") = listausuario.Item(0).oRol.idRol
            Session("sNombreUsuario") = listausuario.Item(0).Nombre
            Session("Idusuario") = listausuario.Item(0).idusuario
            Session("idSucursal") = cboSucursales.SelectedItem.Value
            Session("sNombreSucursal") = cboSucursales.SelectedItem.Text
            lblMensaje.Text = "1"

            If txtPassword.Text = Trim(Desencriptar(listausuario.Item(0).Clave)) Then
                If Session("rolnombre") = "DEMO" Then
                    lblMensaje.Text = "demo"
                    Response.Redirect("Default_Demo.aspx?sNombreUsuario=" & Session("usuario") & "&nRol=" & Session("rol"))
                Else
                    lblMensaje.Text = "no demo"
                    Response.Redirect("DefaultAdmin.aspx?sNombreUsuario=" & Session("sNombreUsuario") & "&nRol=" & Session("rol"))
                End If

            Else
                lblMensaje.Text = "no desencripta clave"
                txtPassword.Text = 0
            End If
        Else
            lblMensaje.Text = "X"
        End If

    End Sub
End Class