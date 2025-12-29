Imports System.Data.OleDb
Imports OMC_LASTEST.AppForms

Public Class frmMenu

    ' Handle the top menu "Back" click, confirms logout
    Private Sub BackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BackToolStripMenuItem.Click
        Dim result As MsgBoxResult
        result = MsgBox("Sure to logout?", 4 + vbQuestion, "Logout Confirmation")
        If result = MsgBoxResult.Yes Then
            Me.Hide()                     ' Hide current menu
            frmSign.Visible = True       ' Show sign-in form
        End If
    End Sub

    ' Handle the top menu "Close" click, exits application
    Private Sub CloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click
        Application.Exit()
    End Sub

    ' Handle the top menu "Contact" click, shows contact info
    Private Sub ContactToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ContactToolStripMenuItem.Click
        MsgBox("Any requires : Please contact Sortify (+0105051218)")
    End Sub

    ' Handle logout button at the bottom, same as Back menu
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Dim result As MsgBoxResult
        result = MsgBox("Sure to logout?", 4 + vbQuestion, "Logout Confirmation")
        If result = MsgBoxResult.Yes Then
            Me.Hide()
            frmSign.Visible = True
        End If
    End Sub

    ' Navigate to the Sort page
    Private Sub btnSort_Click(sender As Object, e As EventArgs) Handles btnSort.Click
        Me.Hide()
        frmSortInstance.Show()           ' Show sorting form
        frmSortInstance.BringToFront()   ' Ensure it's on top
    End Sub

    ' Navigate to the Sell page
    Private Sub btnSell_Click(sender As Object, e As EventArgs) Handles btnSell.Click
        Me.Hide()
        frmSellInstance.Show()
        frmSellInstance.BringToFront()
    End Sub

    ' Navigate to the Buy page
    Private Sub btnBuy_Click(sender As Object, e As EventArgs) Handles btnBuy.Click
        Me.Hide()
        frmBuyInstance.Show()
        frmBuyInstance.BringToFront()
    End Sub

    ' Navigate to the Status page
    Private Sub btnStatus_Click(sender As Object, e As EventArgs) Handles btnStatus.Click
        Me.Hide()
        frmStatusInstance.Show()
        frmStatusInstance.BringToFront()
    End Sub

    ' When frmMenu loads, fetch and display the current user's WalletPts
    Private Sub frmMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            DBHelper.OpenConnection()

            ' Create SQL command to get wallet points
            Dim cmd As New OleDbCommand("SELECT WalletPts FROM user_signup WHERE ID = ?", DBHelper.conn)
            cmd.Parameters.AddWithValue("?", Session.LoggedInUserID)

            ' Execute and convert result to integer
            Dim rawWallet = cmd.ExecuteScalar()?.ToString()
            Dim walletInt As Integer = 0

            ' If result is valid integer, assign it
            If Not Integer.TryParse(rawWallet, walletInt) Then
                walletInt = 0
            End If

            ' Display on UI
            lblPoints.Text = walletInt.ToString() & " pts"

        Catch ex As Exception
            lblPoints.Text = "0 pts" ' fallback on error
        Finally
            DBHelper.CloseConnection()
        End Try
    End Sub

    ' Navigate to the Redeem page when top menu is clicked
    Private Sub RedeemToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RedeemToolStripMenuItem.Click
        Me.Hide()
        frmRedeem.Show()
    End Sub

    ' Refresh wallet points when user comes back to menu (from another form)
    Public Sub RefreshWalletBalance()
        Try
            DBHelper.OpenConnection()
            Dim cmd As New OleDbCommand("SELECT WalletPts FROM user_signup WHERE ID = ?", DBHelper.conn)
            cmd.Parameters.AddWithValue("?", Session.LoggedInUserID)
            Dim result = cmd.ExecuteScalar()

            ' If valid number, show it
            If result IsNot Nothing AndAlso IsNumeric(result) Then
                lblPoints.Text = $"{Convert.ToInt32(result)} pts"
            Else
                lblPoints.Text = "0 pts"
            End If
        Catch ex As Exception
            lblPoints.Text = "0 pts"
        Finally
            DBHelper.CloseConnection()
        End Try
    End Sub

    ' When form becomes active again (e.g. returning from Sell/Buy/Sort), refresh wallet
    Private Sub frmMenu_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        RefreshWalletBalance()
    End Sub

End Class
