Imports System.Data.OleDb

Public Class frmAddress

    ' Handle Submit button click — validate input and insert address into database
    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click

        'Step 1: Validate all input fields are filled
        If txtAddress.Text = "" Or txtHomePhoneNum.Text = "" Or txtPostcode.Text = "" Or txtState.Text = "" Then
            MessageBox.Show("❗ Please fill in all fields.")
            Return
        End If

        Try
            DBHelper.OpenConnection()

            'Step 2: Check if an address already exists for the logged-in user
            Dim checkCmd As New OleDbCommand("SELECT COUNT(*) FROM [address] WHERE [User_ID] = ?", DBHelper.conn)
            checkCmd.Parameters.AddWithValue("?", Session.LoggedInUserID)
            Dim exists = Convert.ToInt32(checkCmd.ExecuteScalar()) 'return integer 

            If exists > 0 Then
                MessageBox.Show("⚠️ Address already exists. You may edit it later.")
                Return ' Prevent duplicate address entry
            End If

            'Step 3: Insert new address if not exists
            Dim cmd As New OleDbCommand("
            INSERT INTO [address] 
            ([User_ID], [Address], [Home_Phone_Number], [Postcode], [State]) 
            VALUES (?, ?, ?, ?, ?)", DBHelper.conn)

            cmd.Parameters.AddWithValue("?", Session.LoggedInUserID)           ' Link address to current user
            cmd.Parameters.AddWithValue("?", txtAddress.Text.Trim())           ' Address line
            cmd.Parameters.AddWithValue("?", txtHomePhoneNum.Text.Trim())      ' Home phone
            cmd.Parameters.AddWithValue("?", txtPostcode.Text.Trim())          ' Postcode
            cmd.Parameters.AddWithValue("?", txtState.Text.Trim())             ' State

            cmd.ExecuteNonQuery() ' Execute the INSERT command

            MessageBox.Show("✅ Address saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            'After success: hide current form and return to menu
            Me.Hide()
            frmMenu.Show()

        Catch ex As Exception
            ' If anything goes wrong, show error message
            MessageBox.Show("❌ Error saving address: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            DBHelper.CloseConnection() ' Always close the DB connection
        End Try
    End Sub

    ' Handle Clear button, resets all input fields
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtAddress.Clear()
        txtHomePhoneNum.Clear()
        txtPostcode.Clear()
        txtState.Clear()
    End Sub

    ' Top menu "Back",  navigate back to main menu
    Private Sub BackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BackToolStripMenuItem.Click
        Me.Hide()
        frmMenu.Show()
    End Sub

    ' Top menu "Close",  logout confirmation, return to login page
    Private Sub CloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click
        Dim result As MsgBoxResult = MsgBox("Sure to logout?", 4 + vbQuestion, "Logout Confirmation")
        If result = MsgBoxResult.Yes Then
            Me.Hide()
            frmStart.Visible = True
        End If
    End Sub

    ' Top menu "Contact",  show customer support message
    Private Sub ContactToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ContactToolStripMenuItem.Click
        MsgBox("Any requires : Please contact Sortify (+0105051218)")
    End Sub

End Class
