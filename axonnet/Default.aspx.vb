Imports System.Management
Imports capaEntidad
Imports capaNegocio
Imports axonnet.MisFunciones
Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then

        Else

            If Request.Browser.IsMobileDevice Then
                Session.Add("movil", 1)
                modalMensaje = "Version Movil"
            Else
                Session.Add("movil", 0)
                modalMensaje = "Version PC"
            End If
        End If
    End Sub



End Class