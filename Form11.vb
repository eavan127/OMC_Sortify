Imports System.Data.OleDb
Imports System.IO

Public Class frmCart
    Private finalTotal As Decimal ' Stores total amount including shipping

    ' On form load, fetch cart items for the current user
    Private Sub frmCart_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CartItems.Clear() ' Clear existing items from memory

        DBHelper.OpenConnection()

        ' Load cart items belonging to the logged-in user
        Dim cmd As New OleDbCommand("SELECT * FROM Cart WHERE UserID = ?", DBHelper.conn)
        cmd.Parameters.AddWithValue("?", Session.LoggedInUserID)
        Dim reader As OleDbDataReader = cmd.ExecuteReader()

        ' Loop through the results and add them to CartItems list
        While reader.Read()
            Dim item As New CartItem()
            item.ID = Convert.ToInt32(reader("ItemID"))
            item.Name = reader("Description").ToString()
            item.Price = Convert.ToDecimal(reader("Price"))
            item.ImagePath = reader("ImagePath").ToString()
            CartItems.Add(item)
        End While

        DBHelper.CloseConnection()

        ' Display items in the form and calculate total
        DisplayCartItems()
    End Sub

    ' Display cart items in the UI
    Private Sub DisplayCartItems()
        ' Hide all UI elements before displaying
        lblItem1.Visible = False
        lblItem2.Visible = False
        lblItem3.Visible = False
        lblPrice1.Visible = False
        lblPrice2.Visible = False
        lblPrice3.Visible = False
        PictureBox1.Visible = False
        PictureBox2.Visible = False
        PictureBox3.Visible = False

        ' Clear previous data
        lblItem1.Text = ""
        lblItem2.Text = ""
        lblItem3.Text = ""
        lblPrice1.Text = ""
        lblPrice2.Text = ""
        lblPrice3.Text = ""
        PictureBox1.Image = Nothing
        PictureBox2.Image = Nothing
        PictureBox3.Image = Nothing

        Dim total As Decimal = 0

        ' Loop through items and update UI
        For i As Integer = 0 To CartItems.Count - 1
            Dim item = CartItems(i)
            total += item.Price
            total = Math.Round(CDbl(Val(total)), 2) ' Round total to 2 decimal places

            Select Case i
                Case 0
                    lblItem1.Text = item.Name
                    lblPrice1.Text = "RM " & item.Price.ToString("0.00")
                    Dim fullImagePath As String = Path.Combine(Application.StartupPath, "materials", item.ImagePath)
                    If File.Exists(fullImagePath) Then PictureBox1.Image = Image.FromFile(fullImagePath)
                    lblItem1.Visible = True
                    lblPrice1.Visible = True
                    PictureBox1.Visible = True

                Case 1
                    lblItem2.Text = item.Name
                    lblPrice2.Text = "RM " & item.Price.ToString("0.00")
                    Dim fullImagePath As String = Path.Combine(Application.StartupPath, "materials", item.ImagePath)
                    If File.Exists(fullImagePath) Then PictureBox2.Image = Image.FromFile(fullImagePath)
                    lblItem2.Visible = True
                    lblPrice2.Visible = True
                    PictureBox2.Visible = True

                Case 2
                    lblItem3.Text = item.Name
                    lblPrice3.Text = "RM " & item.Price.ToString("0.00")
                    Dim fullImagePath As String = Path.Combine(Application.StartupPath, "materials", item.ImagePath)
                    If File.Exists(fullImagePath) Then PictureBox3.Image = Image.FromFile(fullImagePath)
                    lblItem3.Visible = True
                    lblPrice3.Visible = True
                    PictureBox3.Visible = True
            End Select
        Next

        ' Show total price with RM5 shipping if there are items
        If CartItems.Count > 0 Then
            finalTotal = Math.Round(CDbl(Val(total)), 2) + 5D
            lblFinalTotal.Text = "RM " & CStr(finalTotal)
        Else
            MessageBox.Show(Me, "❗ Your cart is empty.", "Cart", MessageBoxButtons.OK, MessageBoxIcon.Information)
            lblFinalTotal.Text = "RM 0.00"
        End If
    End Sub

    ' Pay button — open payment form and pass total
    Private Sub btnPay_Click(sender As Object, e As EventArgs) Handles btnPay.Click
        If finalTotal <= 0 Then
            MessageBox.Show(Me, "❗ Your cart is empty.", "Cart", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        ' Create and open the payment form
        Dim paymentForm As New frmPaymentMethod()
        paymentForm.AmountToPay = CStr(Val((finalTotal))) ' Pass total amount
        paymentForm.ShowDialog()

        Me.Hide()
    End Sub

    ' Top menu: Back to main menu
    Private Sub BackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BackToolStripMenuItem.Click
        Me.Hide()
        frmMenu.Show()
    End Sub

    ' Top menu: Logout and return to login page
    Private Sub CloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click
        If MessageBox.Show("Are you sure you want to log out?", "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Me.Hide()
            frmStart.Show()
        End If
    End Sub

    ' Top menu: Show contact info
    Private Sub ContactToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ContactToolStripMenuItem.Click
        MessageBox.Show("📞 For enquiries, please contact Sortify at +6010-505-1218", "Contact Us", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
End Class
