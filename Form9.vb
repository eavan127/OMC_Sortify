Imports System.Data.OleDb
Imports System.IO

Public Class frmItem
    Private imagePath As String = "" ' Store the uploaded image path

    ' When the form loads
    Private Sub frmItem_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Hide all bin visuals and labels initially
        picBrownBin.Visible = False
        picBlueBin.Visible = False
        picYellowBin.Visible = False
        lblBinType.Visible = False
        lblDescription.Visible = False

        ' If an item was selected in the previous form, show it here
        If Not String.IsNullOrEmpty(frmSort.SelectedItemName) Then
            cboSortItems.SelectedItem = frmSort.SelectedItemName

            'a dictionary stores items as key and bin as value
            ' Show correct bin and description based on selected item
            'check if key exisiting in dictionary ItemBinMap
            If ItemBinMap.ContainsKey(frmSort.SelectedItemName) Then
                Dim bin = ItemBinMap(frmSort.SelectedItemName) 'get the bin name from the dictionary
                lblBinType.Visible = True
                lblBinType.Text = bin.ToUpper()
                lblDescription.Visible = True

                Select Case bin
                    Case "Blue Bin"
                        picBlueBin.Visible = True
                        lblDescription.Text = "Paper (e.g. books, newspaper, box ,paper)"
                    Case "Yellow Bin"
                        picYellowBin.Visible = True
                        lblDescription.Text = "Plastic (e.g. bottles, containers)"
                    Case "Brown Bin"
                        picBrownBin.Visible = True
                        lblDescription.Text = "Glass (e.g. jars, perfume bottles)"
                End Select
            End If
        End If
    End Sub

    ' Upload image button
    Private Sub btnUploadImage_Click(sender As Object, e As EventArgs) Handles btnUploadImage.Click
        Dim ofd As New OpenFileDialog With {
        .Filter = "Images|*.jpg;*.jpeg;*.png;*.bmp"
    }

        If ofd.ShowDialog() = DialogResult.OK Then
            Dim originalPath As String = ofd.FileName
            Dim fileName As String = Path.GetFileName(originalPath)
            Dim destinationPath As String = Path.Combine(Application.StartupPath, "materials", fileName)

            ' Copy to materials folder if not already there
            If Not File.Exists(destinationPath) Then
                File.Copy(originalPath, destinationPath)
            End If

            imagePath = fileName ' Save only file name
            PictureBox3.Image = Image.FromFile(destinationPath)
        End If
    End Sub


    ' Submit button to upload sorting item
   Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
    ' Check if item is selected and image uploaded
    If String.IsNullOrEmpty(cboSortItems.Text) OrElse PictureBox3.Image Is Nothing Then
        MessageBox.Show("❗ Please select an item and upload a photo.")
        Return
    End If

    If String.IsNullOrEmpty(imagePath) Then
        MessageBox.Show("❗ Image path not found.")
        Return
    End If

    Try
        DBHelper.OpenConnection()

        Dim cmd As New OleDbCommand("
            INSERT INTO Submissions 
            ([User_ID], [ItemType], [Quantity], [TimeStamp], [ImagePath], [Status], [Reward]) 
            VALUES (?, ?, ?, ?, ?, ?, ?)", DBHelper.conn)

        cmd.Parameters.AddWithValue("?", Session.LoggedInUserID)
        cmd.Parameters.AddWithValue("?", cboSortItems.Text.Trim())
        cmd.Parameters.AddWithValue("?", cboQuantity.Text.Trim())
        cmd.Parameters.AddWithValue("?", DateTime.Now)
        cmd.Parameters.AddWithValue("?", imagePath) ' just file name
        cmd.Parameters.AddWithValue("?", "Pending")
        cmd.Parameters.AddWithValue("?", "0")

        cmd.ExecuteNonQuery()

        MessageBox.Show("✅ Item submitted successfully and waiting for approval!", "Success")

        frmStatus.Show()
        frmStatus.BringToFront()
        Me.Hide()

    Catch ex As Exception
        MessageBox.Show("❌ Failed to save data: " & ex.Message)
    Finally
        DBHelper.CloseConnection()
    End Try

    ' Clear form
    PictureBox3.Image = Nothing
    cboSortItems.SelectedIndex = -1
    cboQuantity.Text = ""
    imagePath = ""
End Sub

    ' Menu: contact info
    Private Sub ContactToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ContactToolStripMenuItem.Click
        MsgBox("Any requires : Please contact Sortify (+0105051218)")
    End Sub

    ' Menu: logout
    Private Sub CloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click
        Dim result As MsgBoxResult = MsgBox("Sure to logout?", 4 + vbQuestion, "Logout Confirmation")
        If result = MsgBoxResult.Yes Then
            Me.Hide()
            frmSign.Visible = True
        End If
    End Sub

    ' Menu: go back to main menu
    Private Sub BackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BackToolStripMenuItem.Click
        Me.Hide()
        frmMenu.Visible = True
    End Sub
End Class
