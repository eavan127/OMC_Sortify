Public Class frmSort

    ' Shared variable to pass the selected item to frmItem
    Public Shared SelectedItemName As String

    ' When the user selects an item from the combo box
    Private Sub cboSortItems_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSortItems.SelectedIndexChanged

        ' Change the visible image depending on the selected item category
        Select Case cboSortItems.SelectedItem.ToString()

            ' For paper-based items, show PictureBox2 only
            Case "Paper", "Newspaper", "Cardboard"
                PictureBox2.Visible = True     ' Show paper image
                PictureBox3.Visible = False    ' Hide glass image
                PictureBox4.Visible = False    ' Hide plastic/metal image

            ' For plastic/metal items, show PictureBox4 only
            Case "Plastic", "Plastic Bottle", "Toothbrushes handles", "CDs/DVDs", "Metal", "Aluminium cans"
                PictureBox2.Visible = False
                PictureBox3.Visible = False
                PictureBox4.Visible = True     ' Show plastic/metal image

            ' For glass items, show PictureBox3 only
            Case "Glass", "Eyeglass lenses", "Perfume jars", "Medicine jars"
                PictureBox2.Visible = False
                PictureBox3.Visible = True     ' Show glass image
                PictureBox4.Visible = False

        End Select
    End Sub

    ' Handle the Sort button click
    Private Sub btnSort_Click(sender As Object, e As EventArgs) Handles btnSort.Click
        ' If no item is selected, show a warning
        If cboSortItems.Text.Trim() = "" Then
            MessageBox.Show("❗ Please select or enter an item to sort.")
            Exit Sub
        End If

        ' Store selected item name for use in frmItem
        SelectedItemName = cboSortItems.Text.Trim()

        ' Open frmItem and hide this form
        frmItem.Show()
        Me.Hide()
    End Sub

    ' Handle "Back" from the top menu — go to main menu
    Private Sub BackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BackToolStripMenuItem.Click
        Me.Hide()
        frmMenu.Visible = True
    End Sub

    ' Handle "Close" from the top menu — confirm logout and go to sign in
    Private Sub CloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click
        Dim result As MsgBoxResult = MsgBox("Sure to logout?", 4 + vbQuestion, "Logout Confirmation")
        If result = MsgBoxResult.Yes Then
            Me.Hide()
            frmSign.Visible = True
        End If
    End Sub

    ' Handle "Contact" from top menu — show contact info
    Private Sub ContactToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ContactToolStripMenuItem.Click
        MsgBox("Any requires : Please contact Sortify (+0105051218)")
    End Sub

    ' Handle Back button (bottom-left corner) — return to menu
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Me.Hide()
        frmMenu.Show()
    End Sub

End Class
