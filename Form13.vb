Imports System.Data.OleDb

Public Class frmRedeem

    Dim walletBalance As Integer

    ' Form load: fetch wallet balance from database
    Private Sub frmRedeem_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            DBHelper.OpenConnection()
            Dim cmd As New OleDbCommand("SELECT WalletPts FROM user_signup WHERE ID = ?", DBHelper.conn)
            cmd.Parameters.AddWithValue("?", Session.LoggedInUserID)
            Dim result = cmd.ExecuteScalar()

            If result IsNot Nothing AndAlso IsNumeric(result) Then
                walletBalance = Convert.ToInt32(result)
            Else
                walletBalance = 0
            End If

        Catch ex As Exception
            MessageBox.Show("❌ Failed to load wallet balance: " & ex.Message)
            walletBalance = 0
        Finally
            DBHelper.CloseConnection()
        End Try

        lblBalance.Text = "Wallet Balance : " & walletBalance.ToString() & " points"
    End Sub

    ' Redeem RM10
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        RedeemPoints(1000, 10D)
    End Sub
    ' Redeem RM8
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        RedeemPoints(800, 8D)
    End Sub
    ' Redeem RM6
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        RedeemPoints(600, 6D)
    End Sub
    ' Redeem RM4
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        RedeemPoints(400, 4D)
    End Sub
    ' Redeem RM2
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        RedeemPoints(200, 2D)
    End Sub
    ' Redeem RM1
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        RedeemPoints(100, 1D)
    End Sub

    ' Shared function to handle redemption logic
    Private Sub RedeemPoints(pointsRequired As Integer, cashValue As Decimal)
        If walletBalance >= pointsRequired Then
            walletBalance -= pointsRequired

            ' Update wallet balance in DB
            Try
                DBHelper.OpenConnection()
                Dim updateWalletCmd As New OleDbCommand("UPDATE user_signup SET WalletPts = ? WHERE ID = ?", DBHelper.conn)
                updateWalletCmd.Parameters.AddWithValue("?", walletBalance)
                updateWalletCmd.Parameters.AddWithValue("?", Session.LoggedInUserID)
                updateWalletCmd.ExecuteNonQuery()
            Catch ex As Exception
                MessageBox.Show("❌ Error updating wallet: " & ex.Message)
            Finally
                DBHelper.CloseConnection()
            End Try

            ' Refresh wallet after deduction
            Try
                DBHelper.OpenConnection()
                Dim cmd As New OleDbCommand("SELECT WalletPts FROM user_signup WHERE ID = ?", DBHelper.conn)
                cmd.Parameters.AddWithValue("?", Session.LoggedInUserID)
                Dim updatedResult = cmd.ExecuteScalar()

                If updatedResult IsNot Nothing AndAlso IsNumeric(updatedResult) Then
                    walletBalance = Convert.ToInt32(updatedResult)
                Else
                    walletBalance = 0
                End If

                lblBalance.Text = "Wallet Balance : " & walletBalance.ToString() & " points"

            Catch ex As Exception
                MessageBox.Show("❌ Error refreshing balance: " & ex.Message)
            Finally
                DBHelper.CloseConnection()
            End Try

            ' Open payment form
            Dim paymentForm As New frmPaymentMethod With {
                .IsRedemption = True,
                .AmountToPay = cashValue,
                .RedeemedCashAmount = cashValue,
                .StartPosition = FormStartPosition.CenterParent
            }

            If paymentForm.ShowDialog(Me) = DialogResult.OK Then
                Dim selectedMethod As String = paymentForm.cboBankWallet.SelectedItem?.ToString()

                If Not String.IsNullOrEmpty(selectedMethod) Then

                    ' ✅ Update wallet again for safety
                    Try
                        DBHelper.OpenConnection()
                        Dim updateWalletCmd As New OleDbCommand("UPDATE user_signup SET WalletPts = ? WHERE ID = ?", DBHelper.conn)
                        updateWalletCmd.Parameters.AddWithValue("?", walletBalance)
                        updateWalletCmd.Parameters.AddWithValue("?", Session.LoggedInUserID)
                        updateWalletCmd.ExecuteNonQuery()
                    Catch ex As Exception
                        MessageBox.Show("❌ Error updating wallet: " & ex.Message)
                    Finally
                        DBHelper.CloseConnection()
                    End Try

                    ' Success confirmation message
                    MessageBox.Show("✅ You redeemed " & pointsRequired.ToString() &
                                " points for RM " & cashValue.ToString("0.00") & "." & vbCrLf &
                                "New Balance: " & walletBalance.ToString() & " points",
                                "Redemption Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("Please select a payment method.")
                    walletBalance += pointsRequired ' Refund points if no selection
                End If
            Else
                MessageBox.Show("Redemption cancelled.")
                walletBalance += pointsRequired ' Refund points on cancel
            End If

            lblBalance.Text = "Wallet Balance : " & walletBalance.ToString() & " points"
        Else
            MessageBox.Show("⚠️ You don’t have enough points to redeem this amount.",
                        "Redemption Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    ' Optional: save redemption history (not called in current version)
    Private Sub SaveRedemptionToDatabase(points As Integer, cash As Decimal, paymentMethod As String)
        Try
            DBHelper.OpenConnection()

            Dim cmd As New OleDbCommand("INSERT INTO redemption_history ([User_ID], [Points_Redeemed], [Cash_Value], [Redeem_Date], [Payment_Method]) VALUES (?, ?, ?, ?, ?)", DBHelper.conn)

            cmd.Parameters.AddWithValue("?", Session.LoggedInUserID)
            cmd.Parameters.AddWithValue("?", points)
            cmd.Parameters.AddWithValue("?", cash)
            cmd.Parameters.AddWithValue("?", DateTime.Now)
            cmd.Parameters.AddWithValue("?", paymentMethod)

            cmd.ExecuteNonQuery()

        Catch ex As Exception
            MessageBox.Show("❌ Error saving redemption: " & ex.Message)
        Finally
            DBHelper.CloseConnection()
        End Try
    End Sub

    ' Back button in menu
    Private Sub BackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BackToolStripMenuItem.Click
        Me.Hide()
        frmMenu.RefreshWalletBalance()
        frmMenu.Visible = True
    End Sub

    ' Optional manual refresh
    Private Sub RefreshWallet()
        Try
            DBHelper.OpenConnection()
            Dim cmd As New OleDbCommand("SELECT WalletPts FROM user_signup WHERE ID = ?", DBHelper.conn)
            cmd.Parameters.AddWithValue("?", Session.LoggedInUserID)
            Dim result = cmd.ExecuteScalar()

            If result IsNot Nothing AndAlso IsNumeric(result) Then
                walletBalance = Convert.ToInt32(result)
            Else
                walletBalance = 0
            End If

            lblBalance.Text = "Wallet Balance : " & walletBalance.ToString() & " points"

        Catch ex As Exception
            MessageBox.Show("❌ Failed to refresh wallet: " & ex.Message)
        Finally
            DBHelper.CloseConnection()
        End Try
    End Sub
End Class
