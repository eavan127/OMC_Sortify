Imports System.Data.OleDb
Imports System.IO

Public Class frmSign
    Private Sub frmSign_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        If Me.Visible Then
            ClearFormFields()
        End If
    End Sub
    ' When the form loads, clear all input fields
    Private Sub frmSign_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ClearFormFields() ' Clear textboxes
    End Sub

    ' Clear all text fields (Name, Email, Phone, Password)
    Private Sub ClearFormFields()
        txtName.Text = ""
        txtEmail.Text = ""
        txtPhone.Text = ""
        txtPass.Text = ""
    End Sub

    ' When user clicks "Sign Up" button
    Private Sub btnSignUp_Click(sender As Object, e As EventArgs) Handles btnSignUp.Click

        ' Check if any field is empty
        If txtName.Text = "" Or txtEmail.Text = "" Or txtPhone.Text = "" Or txtPass.Text = "" Then
            MessageBox.Show("❗ Please fill in all fields.")
            Return ' Stop if any field is empty
        End If

        Try
            DBHelper.OpenConnection() ' Open database connection

            ' Check if email already exists
            Dim checkCmd As New OleDbCommand("SELECT COUNT(*) FROM user_signup WHERE [Email]=?", DBHelper.conn)
            checkCmd.Parameters.AddWithValue("?", txtEmail.Text.Trim())
            Dim exists As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())

            If exists > 0 Then
                MessageBox.Show("❗ This email is already registered.")
                Return ' Stop if email already used
            End If

            ' Insert new user into database
            Dim insertCmd As New OleDbCommand("INSERT INTO user_signup ([Full Name], [Email], [Phone Number], [Password], [WalletPts]) VALUES (?, ?, ?, ?, ?)", DBHelper.conn)
            ' Insert new user into database
            insertCmd.Parameters.AddWithValue("?", txtName.Text.Trim())
            insertCmd.Parameters.AddWithValue("?", txtEmail.Text.Trim())
            insertCmd.Parameters.AddWithValue("?", txtPhone.Text.Trim())
            insertCmd.Parameters.AddWithValue("?", txtPass.Text.Trim())
            insertCmd.Parameters.AddWithValue("?", 0) ' Default WalletPts to 0
            insertCmd.ExecuteNonQuery()

            ' Get the new user's ID (just inserted)
            Dim getIdCmd As New OleDbCommand("SELECT MAX(ID) FROM user_signup", DBHelper.conn)
            Dim newUserId As Integer = Convert.ToInt32(getIdCmd.ExecuteScalar())

            ' Create a blank history log file for this user
            Dim logPath As String = Path.Combine(Application.StartupPath, "fixed", $"HistoryLog_{newUserId}.txt")
            If Not File.Exists(logPath) Then
                File.WriteAllText(logPath, "") ' Create empty file
            End If

            MessageBox.Show("✅ Registration successful!") ' Show success message
            Me.Hide()
            frmLogin.Show() ' Go to login form

        Catch ex As Exception
            MessageBox.Show("❌ Error during signup: " & ex.Message) ' Handle error
        Finally
            DBHelper.CloseConnection() ' Always close DB connection
        End Try
    End Sub

    ' Exit to start screen with confirmation
    Private Sub CloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click
        Dim result As MsgBoxResult = MsgBox("Sure to navigate back?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Logout Confirmation")
        If result = MsgBoxResult.Yes Then
            Me.Hide()
            frmStart.Visible = True ' Return to start screen
        End If
    End Sub

    ' Show contact info when clicked
    Private Sub ContactToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ContactToolStripMenuItem.Click
        MsgBox("📞 For enquiries: Please contact Sortify (+010-505-1218)", MsgBoxStyle.Information)
    End Sub

    ' Navigate to login form if user already has account
    Private Sub lnkLogin_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkLogin.LinkClicked
        Me.Hide()
        frmLogin.Visible = True
    End Sub
End Class
