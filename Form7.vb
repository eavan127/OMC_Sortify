Imports System.Data.OleDb
Imports System.IO

Public Class frmSell

    ' Stores the selected image file path for database storage
    Private selectedImagePath As String

    ' Upload button, lets user select an image to display and store
    Private Sub btnUpload1_Click(sender As Object, e As EventArgs) Handles btnUpload1.Click
        Dim ofd As New OpenFileDialog()
        ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp" ' Only allow image files

        ' If user selects a file
        If ofd.ShowDialog() = DialogResult.OK Then
            Dim originalPath As String = ofd.FileName
            Dim fileName As String = Path.GetFileName(originalPath)
            Dim destinationPath As String = Path.Combine(Application.StartupPath, "materials", fileName)

            ' Copy to materials folder if not already there
            If Not File.Exists(destinationPath) Then
                File.Copy(originalPath, destinationPath)
            End If

            selectedImagePath = fileName ' Save only filename to database
            picUpload.Image = Image.FromFile(destinationPath)

        Else
            MessageBox.Show("❗ Please select an image.")
        End If
    End Sub

    ' Sell Item button, validates inputs, then saves item to database
    Private Sub btnSellItem_Click(sender As Object, e As EventArgs) Handles btnSellItem.Click
        ' Check if all fields are filled and image is uploaded
        If txtDescription.Text = "" Or txtCondition.Text = "" Or txtPrice.Text = "" Or picUpload.Image Is Nothing Then
            MessageBox.Show("⚠️ Please fill in all the fields and upload an image.")
            Exit Sub
        End If

        ' Check if price is a valid decimal number
        Dim priceValue As Decimal
        If Not Decimal.TryParse(txtPrice.Text, priceValue) Then
            MessageBox.Show("❗ Invalid price entered.")
            Exit Sub
        End If

        ' Try to insert item into the Items table
        Try
            DBHelper.OpenConnection()

            Dim cmd As New OleDbCommand("INSERT INTO Items ([User_ID], [Description], [Price], [Condition], [ImagePath]) VALUES (?, ?, ?, ?, ?)", DBHelper.conn)
            cmd.Parameters.AddWithValue("?", Session.LoggedInUserID)  ' Foreign key to track user
            cmd.Parameters.AddWithValue("?", txtDescription.Text)
            cmd.Parameters.AddWithValue("?", priceValue)
            cmd.Parameters.AddWithValue("?", txtCondition.Text)
            cmd.Parameters.AddWithValue("?", selectedImagePath)

            cmd.ExecuteNonQuery() ' Run the insert query
            MessageBox.Show("✅ Item submitted successfully!")

            ' Reset form fields
            txtDescription.Clear()
            txtCondition.Clear()
            txtPrice.Clear()
            picUpload.Image = Nothing
            selectedImagePath = ""

        Catch ex As Exception
            MessageBox.Show("❌ Error saving to database: " & ex.Message)
        Finally
            DBHelper.CloseConnection()
        End Try
    End Sub

    ' Top menu: Back to Menu
    Private Sub BackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BackToolStripMenuItem.Click
        Me.Hide()
        frmMenu.Show()
    End Sub

    ' Top menu: Logout and return to Start/Login page
    Private Sub CloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click
        Dim result As MsgBoxResult
        result = MsgBox("Sure to logout?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Logout Confirmation")
        If result = MsgBoxResult.Yes Then
            Me.Hide()
            frmStart.Show()
        End If
    End Sub

    ' Top menu: Show contact information
    Private Sub ContactToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ContactToolStripMenuItem.Click
        MessageBox.Show("📞 Any enquiries: Please contact Sortify (+0105051218)", "Contact Us")
    End Sub

    ' Bottom Back button — return to Menu
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Me.Hide()
        frmMenu.Show()
    End Sub

End Class
