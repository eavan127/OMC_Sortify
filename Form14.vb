Imports System.Data.OleDb

Public Class frmReset
    ' On form load, focus on the email textbox
    Private Sub frmReset_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtEmail.Focus()
    End Sub

    ' When reset button is clicked
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Dim email As String = txtEmail.Text.Trim()
        Dim newPassword As String = txtNewPass.Text
        Dim confirmPassword As String = txtNewPassCon.Text

        'Validate empty fields
        If email = "" Or newPassword = "" Or confirmPassword = "" Then
            MessageBox.Show("❗ Please fill in all fields.")
            Exit Sub
        End If

        'Validate matching passwords
        If newPassword <> confirmPassword Then
            MessageBox.Show("❗ Passwords do not match.")
            Exit Sub
        End If

        Try
            DBHelper.OpenConnection()

            'Check if email exists
            Dim checkCmd As New OleDbCommand("SELECT COUNT(*) FROM user_signup WHERE Email = ?", DBHelper.conn)
            checkCmd.Parameters.AddWithValue("?", email)
            Dim exists As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())

            If exists = 0 Then
                MessageBox.Show("❌ Email not found. Please enter a registered email.")
                Exit Sub
            End If

            'Update password
            Dim updateCmd As New OleDbCommand("UPDATE user_signup SET [Password] = ? WHERE Email = ?", DBHelper.conn)
            updateCmd.Parameters.AddWithValue("?", newPassword)
            updateCmd.Parameters.AddWithValue("?", email)

            Dim rowsUpdated As Integer = updateCmd.ExecuteNonQuery()

            'Check if update successful
            If rowsUpdated > 0 Then
                MessageBox.Show("✅ Password reset successfully!", "Success")
                Me.Hide()
                frmLogin.Show()
            Else
                MessageBox.Show("⚠️ Password reset failed. Try again.")
            End If

        Catch ex As Exception
            MessageBox.Show("❌ Error: " & ex.Message, "Database Error")
        Finally
            DBHelper.CloseConnection()
        End Try
    End Sub

    'Back button
    Private Sub btnBackFormReset_Click(sender As Object, e As EventArgs) Handles btnBackFormReset.Click
        Me.Hide()
        frmLogin.Show()
    End Sub

    'Menu Navigation
    Private Sub HomeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HomeToolStripMenuItem.Click
        Me.Hide()
        frmStart.Show()
    End Sub

    Private Sub CloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click
        Dim result = MessageBox.Show("Sure to exit the application?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Application.Exit()
        End If
    End Sub

    Private Sub ContactToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ContactToolStripMenuItem.Click
        MessageBox.Show("📞 Any enquiries: Please contact Sortify (+0105051218)", "Contact Us")
    End Sub
End Class
