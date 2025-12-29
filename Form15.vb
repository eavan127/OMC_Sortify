Imports System.IO
Imports System.Data.OleDb

Public Class frmBuy

    ' Back to main menu
    Private Sub BackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BackToolStripMenuItem.Click
        Me.Hide()
        frmMenu.Visible = True
    End Sub

    ' Logout and go to start form
    Private Sub CloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click
        Dim result As MsgBoxResult
        result = MsgBox("Sure to navigate back?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Logout Confirmation")
        If result = MsgBoxResult.Yes Then
            Me.Hide()
            frmStart.Visible = True
        End If
    End Sub

    ' Contact message
    Private Sub ContactToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ContactToolStripMenuItem.Click
        MsgBox("Any requires: Please contact Sortify (+0105051218)")
    End Sub

    ' Search button logic
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        FlowLayoutPanel1.Controls.Clear()

        If txtSearch.Text.Trim() = "" Then
            MessageBox.Show("Please enter an item name to search.")
            Exit Sub
        End If

        Try
            DBHelper.OpenConnection()

            ' Search unsold items that match keyword
            Dim cmd As New OleDbCommand("SELECT * FROM Items WHERE Description LIKE ? AND IsSold = False", DBHelper.conn)
            cmd.Parameters.AddWithValue("?", "%" & txtSearch.Text.Trim() & "%")

            Dim reader As OleDbDataReader = cmd.ExecuteReader()

            If Not reader.HasRows Then
                MessageBox.Show("No matching unsold items found.")
                Exit Sub
            End If

            ' For each matched item, dynamically create a panel
            While reader.Read()
                Dim itemID As Integer = Convert.ToInt32(reader("ID"))
                Dim description As String = reader("Description").ToString()
                Dim price As String = Convert.ToDecimal(reader("Price")).ToString("0.00")
                Dim condition As String = reader("Condition").ToString()
                Dim imgPath As String = reader("ImagePath").ToString()

                ' Create panel
                Dim panel As New Panel With {
                    .Width = FlowLayoutPanel1.ClientSize.Width - 30,
                    .Height = 150,
                    .BorderStyle = BorderStyle.FixedSingle,
                    .BackColor = Color.White,
                    .Margin = New Padding(5)
                }

                Dim fontCustom As New Font("Noto Serif Black", 12, FontStyle.Bold)

                ' Image
                Dim pic As New PictureBox With {
                    .Size = New Size(120, 120),
                    .Location = New Point(10, 15),
                    .SizeMode = PictureBoxSizeMode.Zoom,
                    .BorderStyle = BorderStyle.FixedSingle
                }
                Dim fullImgPath As String = Path.Combine(Application.StartupPath, "materials", imgPath)
                If File.Exists(fullImgPath) Then
                    Using fs As New FileStream(fullImgPath, FileMode.Open, FileAccess.Read)
                        pic.Image = Image.FromStream(fs)
                    End Using
                End If
                panel.Controls.Add(pic)

                ' Description label
                Dim lblDesc As New Label With {
                    .Text = $"Description : {description}",
                    .Font = fontCustom,
                    .AutoSize = True,
                    .Location = New Point(150, 20)
                }
                panel.Controls.Add(lblDesc)

                ' Price label
                Dim lblPrice As New Label With {
                    .Text = $" Price : RM {price}",
                    .Font = fontCustom,
                    .AutoSize = True,
                    .Location = New Point(150, 55)
                }
                panel.Controls.Add(lblPrice)

                ' Condition label
                Dim lblCond As New Label With {
                    .Text = $"Condition/10 : {condition}",
                    .Font = fontCustom,
                    .AutoSize = True,
                    .Location = New Point(150, 90)
                }
                panel.Controls.Add(lblCond)

                ' Add to cart button
                Dim btnAdd As New Button With {
                    .Text = "ADD TO CART",
                    .Size = New Size(120, 35),
                    .Font = New Font("Segoe UI", 10, FontStyle.Bold),
                    .BackColor = Color.Green,
                    .ForeColor = Color.White,
                    .FlatStyle = FlatStyle.Flat,
                    .Location = New Point(panel.Width - 140, panel.Height - 45)
                }
                btnAdd.FlatAppearance.BorderSize = 0

                ' Add click handler
                AddHandler btnAdd.Click, Sub(sender2, e2)
                                             AddItemToCart(itemID, description, price, imgPath)
                                         End Sub
                panel.Controls.Add(btnAdd)

                ' Add panel to UI
                FlowLayoutPanel1.Controls.Add(panel)
            End While

            reader.Close()

        Catch ex As Exception
            MessageBox.Show("❌ Error during search: " & ex.Message)
        Finally
            DBHelper.CloseConnection()
        End Try
    End Sub

    ' Add item to cart
    Private Sub AddItemToCart(itemID As Integer, description As String, price As String, imagePath As String)
        Try
            DBHelper.OpenConnection()

            ' Check if item is already in cart
            Dim checkCmd As New OleDbCommand("SELECT COUNT(*) FROM Cart WHERE UserID = ? AND ItemID = ?", DBHelper.conn)
            checkCmd.Parameters.AddWithValue("?", Session.LoggedInUserID)
            checkCmd.Parameters.AddWithValue("?", itemID)
            If Convert.ToInt32(checkCmd.ExecuteScalar()) > 0 Then
                MessageBox.Show("❗ Item already in cart.")
                Exit Sub
            End If

            ' Limit to 3 items max
            Dim countCmd As New OleDbCommand("SELECT COUNT(*) FROM Cart WHERE UserID = ?", DBHelper.conn)
            countCmd.Parameters.AddWithValue("?", Session.LoggedInUserID)
            If Convert.ToInt32(countCmd.ExecuteScalar()) >= 3 Then
                MessageBox.Show("❗ Your cart is full (3 items max).")
                Exit Sub
            End If

            ' Insert into cart table
            Dim insertCmd As New OleDbCommand("INSERT INTO Cart (UserID, ItemID, Description, Price, ImagePath) VALUES (?, ?, ?, ?, ?)", DBHelper.conn)
            insertCmd.Parameters.AddWithValue("?", Session.LoggedInUserID)
            insertCmd.Parameters.AddWithValue("?", itemID)
            insertCmd.Parameters.AddWithValue("?", description)
            insertCmd.Parameters.AddWithValue("?", Decimal.Parse(price))
            insertCmd.Parameters.AddWithValue("?", imagePath)
            insertCmd.ExecuteNonQuery()

            MessageBox.Show("✅ Added to cart.")

        Catch ex As Exception
            MessageBox.Show("❌ Error adding to cart: " & ex.Message)
        Finally
            DBHelper.CloseConnection()
        End Try
    End Sub

    ' View cart form
    Private Sub btnViewCart_Click(sender As Object, e As EventArgs) Handles btnViewCart.Click
        Me.Hide()
        frmCart.Show()
    End Sub

End Class
