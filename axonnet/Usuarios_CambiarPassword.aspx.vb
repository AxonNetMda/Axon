Imports capaNegocio
Imports capaEntidad
Imports CapaDatos
Imports capaAdministracion.MisFunciones
Public Class Usuarios_CambiarPassword
    Inherits System.Web.UI.Page
    Dim nIdusuario As Integer = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            nIdusuario = Session("idUsuario")
            Dim listausuario As List(Of usuario) = New CN_Usuario().Listar(nIdusuario)
            If listausuario.Count >= 0 Then
                lblNombreUsuario.Text = listausuario(0).Nombre
            Else
                lblNombreUsuario.Text = "No identificado"
            End If
        End If
    End Sub

    Protected Sub BtnAceptar_Click(sender As Object, e As EventArgs) Handles BtnAceptar.Click
        Dim resultado As Boolean = False
        Dim nidusuario As Integer = 0
        Dim mensaje As String = ""
        Dim sclave As String = Desencriptar(Session("sClaveUsuario"))
        If Trim(txtNueva.Text) = "" Then
            mensaje = "la nueva clave no puede ser vacia"
        Else
            If Trim(txtActual.Text) = sclave Then
                If Trim(txtNueva.Text) = Trim(txtConfirmar.Text) Then
                    nidusuario = Session("idUsuario")
                    Try
                        resultado = New CD_Usuario().CambiarClave(nidusuario, Encriptar(txtNueva.Text), mensaje)
                    Catch ex As Exception
                        mensaje += ex.Message
                    End Try

                Else
                    mensaje = "no se puede cambiar la clave"


                End If
            Else
                mensaje = "la clave actual es distinta a la registrada"
            End If
        End If
        If Not resultado Then
            lblMensajeAtencion.Text = mensaje
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowModal", "$(document).ready(function() { $('#MdlAtencion').modal('show'); });", True)
        Else
            Session("sClaveUsuario") = ""
            Session("rolnombre") = ""
            Session("rol") = 0
            Session("sNombreUsuario") = ""
            Session("Idusuario") = 0
            Response.Redirect("Login.aspx")
        End If
    End Sub
End Class