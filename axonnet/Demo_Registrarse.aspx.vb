Imports System.Net.Mail
Imports System.Threading.Tasks

Public Class Demo_Registrarse
    Inherits System.Web.UI.Page
    Private Function EnviarCorreo(destinatario As String, asunto As String, cuerpo As String) As Boolean

        Try
            Dim smtp As New SmtpClient("dtc035.ferozo.com") ' Cambia por tu servidor SMTP
            smtp.Port = 465
            smtp.Credentials = New System.Net.NetworkCredential("javier@axonnet.store", "Cacha1969//")
            smtp.EnableSsl = True
            smtp.Timeout = 10000 ' 30 segundos

            Dim mensaje As New MailMessage()
            mensaje.From = New MailAddress("javier@axonnet.store")
            mensaje.To.Add(destinatario)
            mensaje.Subject = asunto
            mensaje.Body = cuerpo
            mensaje.IsBodyHtml = False

            smtp.Send(mensaje)
            Return True
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "error", "alert('Error: " & ex.Message & "');", True)
            Return False
        End Try
    End Function
    Protected Async Sub btnEnviar_Click(sender As Object, e As EventArgs) Handles btnEnviar.Click
        Try


            Dim enviado As Boolean = EnviarCorreo(txtEmail.Text, "Verificación", "Por favor, confirma tu correo.")

            If enviado Then
                ' Mostrar la ventana modal de éxito
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "modal", "$('#modalExito').modal('show');", True)

                ' Redirigir después de un pequeño retraso
                Await Task.Delay(3000) ' Espera 3 segundos antes de redirigir
                Response.Redirect("index.aspx")
            Else
                ' Mostrar mensaje de error
                lblmensaje.Text = "Error al enviar el correo. Intente nuevamente."
                lblmensaje.CssClass = "text-danger"
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "error", "alert('Error: " & ex.Message & "');", True)

        End Try
    End Sub

    Private Sub Registrarse_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None
    End Sub

End Class