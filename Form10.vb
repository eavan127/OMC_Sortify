Imports System.IO

Public Class frmHistory

    ' When the form becomes visible (e.g. user switches to it)
    Private Sub frmHistory_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        If Me.Visible Then
            frmHistory_Load(Nothing, Nothing) ' Reload history list each time it's shown
        End If
    End Sub

    ' When the form is loaded
    Private Sub frmHistory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lvwItems.Items.Clear()           ' Clear previous list
        lvwItems.View = View.Details     ' Set view mode to Details

        ' Only add columns once
        If lvwItems.Columns.Count = 0 Then
            lvwItems.Columns.Add("Item", 120)
            lvwItems.Columns.Add("Quantity", 80)
            lvwItems.Columns.Add("Reward", 80)
            lvwItems.Columns.Add("Time", 180)
        End If

        ' Build path to this user's history file
        Dim userId = Session.LoggedInUserID
        Dim logPath As String = Path.Combine(Application.StartupPath, "fixed", $"HistoryLog_{userId}.txt")

        ' If file exists, read all lines and process each
        If File.Exists(logPath) Then
            Dim lines = File.ReadAllLines(logPath)
            For Each line As String In lines
                Try
                    ' Only process valid history lines
                    If line.Contains("SubmissionID:") AndAlso line.Contains("Item:") AndAlso line.Contains("Qty:") AndAlso line.Contains("Reward:") Then
                        ' Split line using ", " separator
                        Dim parts = line.Split(New String() {", "}, StringSplitOptions.None) 'split sentences into parts by using"," as seperator

                        ' Extract values from specific parts
                        Dim item = parts.FirstOrDefault(Function(p) p.StartsWith("Item:"))?.Replace("Item:", "").Trim() 'remove "Item:" and store in column "Item:"
                        Dim qty = parts.FirstOrDefault(Function(p) p.StartsWith("Qty:"))?.Replace("Qty:", "").Trim() 'same for "Qty:"
                        Dim reward = parts.FirstOrDefault(Function(p) p.StartsWith("Reward:"))?.Replace("Reward:", "").Trim() 'same for "Reward:"
                        Dim time = parts.FirstOrDefault(Function(p) p.StartsWith("Time:"))?.Replace("Time:", "").Trim() 'same for "Time:"

                        ' Only add to list if all parts are valid
                        If Not String.IsNullOrEmpty(item) AndAlso Not String.IsNullOrEmpty(qty) AndAlso Not String.IsNullOrEmpty(reward) AndAlso Not String.IsNullOrEmpty(time) Then 'only not empty and null
                            Dim lvItem As New ListViewItem(item)
                            lvItem.SubItems.Add(qty)
                            lvItem.SubItems.Add(reward)
                            lvItem.SubItems.Add(time)
                            lvwItems.Items.Add(lvItem)
                        End If
                    End If
                Catch ex As Exception
                    ' If any error happens during parsing, skip that line silently
                End Try
            Next
        Else
            MessageBox.Show("⚠️ No history found yet.", "Info")
        End If
    End Sub

    ' Handle bottom Back button click
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Me.Hide()
        frmMenu.Show()
    End Sub

    ' Handle top menu "Back" click
    Private Sub BackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BackToolStripMenuItem.Click
        Me.Hide()
        frmMenu.Show()
    End Sub

    ' Handle top menu "Close" click, logout and return to login screen
    Private Sub CloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click
        If MsgBox("Sure to logout?", vbYesNo + vbQuestion, "Logout Confirmation") = vbYes Then
            Me.Hide()
            frmStart.Show()
        End If
    End Sub

    ' Handle top menu "Contact" click
    Private Sub ContactToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ContactToolStripMenuItem.Click
        MsgBox("📞 Any enquiries: Please contact Sortify (+0105051218)")
    End Sub

End Class
