Public Class Default_demo
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Session("Esmovil") = 1 Then
            Label1.Text = "Navega en un movil"

        Else
            Label1.Text = "Navega en un PC 1"

        End If



    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        Response.Redirect("homesefeliz.aspx")
    End Sub
End Class