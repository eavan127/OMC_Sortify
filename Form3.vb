Imports System.Data.OleDb ' Enable Access database support

Public Class frmLogin
    ' When the form loads, clear the email and password fields
    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ClearLoginFields()
    End Sub

    ' Clear both login fields
    Private Sub ClearLoginFields()
        txtEmail2.Clear()
        txtPass2.Clear()
    End Sub

    ' Go back to the sign-up form
    Private Sub BackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BackToolStripMenuItem.Click
        Me.Hide()
        frmSign.Visible = True
    End Sub

    ' Close current form and go back to start page with confirmation
    Private Sub CloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click
        Dim result As MsgBoxResult
        result = MsgBox("Sure to navigate back?", 4 + vbQuestion, "Logout Confirmation")
        If result = MsgBoxResult.Yes Then
            Me.Hide()
            frmStart.Visible = True
        End If
    End Sub

    ' Show contact information
    Private Sub ContactToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ContactToolStripMenuItem.Click
        MsgBox("Any requires : Please contact Sortify (+0105051218)")
    End Sub

    ' Redirect to the Sign-up form if user clicks login link
    Private Sub lnkLogin_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkLogin.LinkClicked
        Me.Hide()
        frmSign.Visible = True
    End Sub

    ' Main login button click handler
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        ' Check if email or password is empty
        If txtEmail2.Text = "" Or txtPass2.Text = "" Then
            MessageBox.Show("Please enter both email and password.")
            Return
        End If

        Try
            DBHelper.OpenConnection() ' Open the database connection

            'Step 1: Check if user exists and fetch ID + Email
            Dim cmd As New OleDbCommand("SELECT [ID], [Email] FROM [user_signup] WHERE [Email]=? AND [Password]=?", DBHelper.conn)
            cmd.Parameters.AddWithValue("?", txtEmail2.Text.Trim())
            cmd.Parameters.AddWithValue("?", txtPass2.Text.Trim())

            Dim reader As OleDbDataReader = cmd.ExecuteReader()

            If reader.Read() Then
                'Store session values
                Session.LoggedInUserID = Convert.ToInt32(reader("ID"))
                Session.LoggedInUserEmail = reader("Email").ToString()
                reader.Close()

                'Step 2: Fetch wallet points
                Dim walletCmd As New OleDbCommand("SELECT WalletPts FROM user_signup WHERE ID = ?", DBHelper.conn)
                walletCmd.Parameters.AddWithValue("?", Session.LoggedInUserID)
                Dim result = walletCmd.ExecuteScalar()
                Dim walletStr As String = If(IsDBNull(result), "0", result.ToString())
                Dim walletVal As Integer = 0
                Integer.TryParse(walletStr, walletVal)
                Session.WalletPts = walletVal

                txtEmail2.Clear()
                txtPass2.Clear()

                'Step 3: Check if address exists
                Dim checkAddressCmd As New OleDbCommand("SELECT COUNT(*) FROM [address] WHERE [User_ID]=?", DBHelper.conn)
                checkAddressCmd.Parameters.AddWithValue("?", Session.LoggedInUserID)
                Dim addressCount As Integer = Convert.ToInt32(checkAddressCmd.ExecuteScalar())

                Me.Hide()

                If addressCount = 0 Then
                    frmAddress.Show() ' No address yet → go to address form
                Else
                    frmMenu.Show() ' Address exists → go to main menu
                End If
            Else
                ' Login failed if no match found
                MessageBox.Show("❌ Login failed. Please check your email or password.", "Login Error")
            End If

        Catch ex As Exception
            ' Handle runtime errors (e.g., DB connection)
            MessageBox.Show("⚠️ Error during login: " & ex.Message, "Error")
        Finally
            DBHelper.CloseConnection() ' Always close the database connection
        End Try
    End Sub

    ' Navigate to password reset form
    Private Sub LinkForgotPass_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkForgotPass.LinkClicked
        Me.Hide()
        Dim forgotForm As New frmReset()
        forgotForm.ShowDialog()
    End Sub
End Class
