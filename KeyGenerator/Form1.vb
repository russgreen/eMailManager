Public Class Form1

    Dim wsK As New ServiceReference1.keygenSoapClient

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            TextBox2.Text = wsK.KeyGen(txtUserID.Text, Me.NumericUpDown1.Value)
        Catch
            MsgBox("Error Number " & Err.Number & vbCrLf & Err.Description)
            txtUserID.Text = txtUserID.Text + "1"
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.txtUserID.Text = wsK.NewGUID
    End Sub
End Class
