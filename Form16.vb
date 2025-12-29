Imports System.Data.OleDb

Public Class frmPaymentMethod
    Public Property IsRedemption As Boolean = False
    Public Property RedeemedCashAmount As Decimal = 0D
    Public Property TotalCartAmount As Decimal = 0
    Public Property AmountToPay As Decimal = 0

    ' Form Load event
    Private Sub frmPaymentMethod_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' Set label depending on whether it's redemption or purchase
        If IsRedemption Then
            lblAmount.Text = "RM " & RedeemedCashAmount.ToString("0.00")
        Else
            lblAmount.Text = "RM " & AmountToPay.ToString("0.00")
        End If

        cboMethod.SelectedIndex = -1
        cboBankWallet.Items.Clear()
        txtAccNum.Clear()
    End Sub

    ' Change options based on selected payment method
    Private Sub cboMethod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboMethod.SelectedIndexChanged
        cboBankWallet.Items.Clear()

        Select Case cboMethod.SelectedItem.ToString()
            Case "e-Wallet"
                cboBankWallet.Items.AddRange({"Touch 'n Go", "Boost", "GrabPay", "ShopeePay", "MAE by Maybank"})
            Case "Online Banking"
                cboBankWallet.Items.AddRange({"Maybank2u", "CIMB Clicks", "RHB Now", "Bank Islam", "Hong Leong Connect", "AmOnline"})
            Case "Credit/Debit Card"
                cboBankWallet.Items.AddRange({"Visa", "Mastercard"})
            Case "Manual Bank Transfer"
                cboBankWallet.Items.AddRange({"Maybank", "CIMB", "RHB", "Bank Islam", "Public Bank", "Others"})
        End Select

        cboBankWallet.SelectedIndex = -1
    End Sub

    ' Confirm button logic
    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        If cboMethod.SelectedIndex = -1 Or cboBankWallet.SelectedIndex = -1 Or txtAccNum.Text.Trim() = "" Then
            MessageBox.Show("❗ Please fill in all fields, including the account number.")
            Exit Sub
        End If

        If Session.LoggedInUserID <= 0 Then
            MessageBox.Show("❌ Invalid or missing user session.")
            Exit Sub
        End If

        btnConfirm.Enabled = False

        Try
            Dim method As String = cboMethod.SelectedItem.ToString().Trim()
            Dim wallet As String = cboBankWallet.SelectedItem.ToString().Trim()
            Dim accNum As String = txtAccNum.Text.Trim()
            Dim payTime As Date = DateTime.Now

            DBHelper.OpenConnection()

            If IsRedemption Then
                ' Insert redemption record
                Dim redeemSQL As String = "INSERT INTO redemption_history (User_ID, Points_Redeemed, Cash_Value, Redeem_Date, Payment_Method, Account) VALUES (?, ?, ?, ?, ?, ?)"
                Using cmd As New OleDbCommand(redeemSQL, DBHelper.conn)
                    cmd.Parameters.AddWithValue("?", Session.LoggedInUserID)
                    cmd.Parameters.AddWithValue("?", RedeemedCashAmount * 100) ' Convert RM to points
                    cmd.Parameters.AddWithValue("?", RedeemedCashAmount)
                    cmd.Parameters.AddWithValue("?", payTime)
                    cmd.Parameters.AddWithValue("?", wallet)
                    cmd.Parameters.AddWithValue("?", accNum)
                    cmd.ExecuteNonQuery()
                End Using

                MessageBox.Show("✅ Redemption of RM " & RedeemedCashAmount.ToString("0.00") & " completed!")

                Me.DialogResult = DialogResult.OK
                Me.Hide()

            Else
                ' Insert purchase payment record
                Dim paySQL As String = "INSERT INTO Payments (User_ID, PaymentMethod, BankOrWallet, AmountPaid, PayToAccount, PaymentTime) VALUES (?, ?, ?, ?, ?, ?)"
                Using cmd As New OleDbCommand(paySQL, DBHelper.conn)
                    cmd.Parameters.AddWithValue("?", Session.LoggedInUserID)
                    cmd.Parameters.AddWithValue("?", method)
                    cmd.Parameters.AddWithValue("?", wallet)
                    cmd.Parameters.AddWithValue("?", "RM " & CStr(AmountToPay))
                    cmd.Parameters.AddWithValue("?", accNum)
                    cmd.Parameters.AddWithValue("?", payTime)
                    cmd.ExecuteNonQuery()
                End Using

                ' Mark items as sold
                For Each item In CartItems
                    Using updateCmd As New OleDbCommand("UPDATE Items SET IsSold = True WHERE ID = ?", DBHelper.conn)
                        updateCmd.Parameters.AddWithValue("?", item.ID)
                        updateCmd.ExecuteNonQuery()
                    End Using
                Next

                ' Clear user's cart
                Using clearCartCmd As New OleDbCommand("DELETE FROM Cart WHERE UserID = ?", DBHelper.conn)
                    clearCartCmd.Parameters.AddWithValue("?", Session.LoggedInUserID)
                    clearCartCmd.ExecuteNonQuery()
                End Using

                CartItems.Clear()

                MessageBox.Show("✅ Purchase payment recorded successfully!")

                Me.DialogResult = DialogResult.OK
                Me.Hide()
            End If

            frmMenu.Visible = True

        Catch ex As Exception
            MessageBox.Show("❌ Error saving transaction: " & ex.Message, "Error")
        Finally
            DBHelper.CloseConnection()
            btnConfirm.Enabled = True
        End Try
    End Sub
End Class
