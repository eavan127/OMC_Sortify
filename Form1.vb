Public Class frmStart

    ' Open user sign-in form
    Private Sub btnStartUser_Click(sender As Object, e As EventArgs) Handles btnStartUser.Click
        Me.Hide()
        frmSign.Visible = True
    End Sub

    ' Admin login with password check
    Private Sub btnStartAdmin_Click(sender As Object, e As EventArgs) Handles btnStartAdmin.Click
        Dim inputPassword As String = InputBox("Enter admin password:", "Admin Access Required")

        If inputPassword = "OMC_G13" Then
            Me.Hide()
            frmApproval.Visible = True
        Else
            MessageBox.Show("Incorrect password. Access denied.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    ' Exit the application
    Private Sub CloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click
        Application.Exit()
    End Sub

End Class
