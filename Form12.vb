Imports System.Data.OleDb
Imports System.IO
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms

Public Class frmApproval
    ' Store submission and user IDs
    Private SubmissionIDs(3) As Integer
    Private UserIDs(3) As Integer
    Private ImagePaths(3) As String
    Private Quantities(3) As Integer
    Private listBoxes() As ListBox
    Private lastLoadedTime As DateTime = DateTime.MinValue

    Private Sub frmApproval_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If (DateTime.Now - lastLoadedTime).TotalSeconds > 1 Then
            LoadPendingSubmissions()
            lastLoadedTime = DateTime.Now
        End If
    End Sub

    Private Sub LoadPendingSubmissions()
        listBoxes = {ListBox1, ListBox2, ListBox3, ListBox4}
        Dim linkLabels = {LinkLabel1, LinkLabel2, LinkLabel3, LinkLabel4}
        Dim txtRewards = {txtReward1, txtReward2, txtReward3, txtReward4}
        Dim buttonsYes = {btnYes1, btnYes2, btnYes3, btnYes4}
        Dim buttonsNo = {btnNo1, btnNo2, btnNo3, btnNo4}

        ' Clear UI
        For i = 0 To 3
            listBoxes(i).Items.Clear()
            linkLabels(i).Text = ""
            txtRewards(i).Text = ""
            buttonsYes(i).Enabled = False
            buttonsNo(i).Enabled = False
        Next

        Try
            DBHelper.OpenConnection()
            Dim cmd As New OleDbCommand("SELECT TOP 4 S.ID, S.User_ID, S.ItemType, S.Quantity, S.ImagePath, U.[Full Name]
                                         FROM Submissions S
                                         INNER JOIN user_signup U ON S.User_ID = U.ID
                                         WHERE S.Status = 'Pending'", DBHelper.conn)
            Dim reader As OleDbDataReader = cmd.ExecuteReader()
            Dim index As Integer = 0

            While reader.Read() AndAlso index < 4
                SubmissionIDs(index) = Convert.ToInt32(reader("ID"))
                UserIDs(index) = Convert.ToInt32(reader("User_ID"))
                ImagePaths(index) = reader("ImagePath").ToString()
                Quantities(index) = Convert.ToInt32(reader("Quantity"))

                listBoxes(index).Items.Add(reader("Full Name").ToString())
                listBoxes(index).Items.Add(reader("ItemType").ToString())
                Debug.WriteLine("Loaded item type: " & reader("ItemType").ToString())
                linkLabels(index).Text = "🖼 View Image"
                txtRewards(index).Text = "0"
                buttonsYes(index).Enabled = True
                buttonsNo(index).Enabled = True

                index += 1
            End While

        Catch ex As Exception
            MessageBox.Show("❌ Error loading approvals: " & ex.Message)
        Finally
            DBHelper.CloseConnection()
        End Try
    End Sub

    ' View image on link click
    Private Sub LinkLabel_Click(sender As Object, e As EventArgs) Handles LinkLabel1.Click, LinkLabel2.Click, LinkLabel3.Click, LinkLabel4.Click
        Dim link = CType(sender, LinkLabel)
        Dim index = Array.IndexOf({LinkLabel1, LinkLabel2, LinkLabel3, LinkLabel4}, link)
        Dim imgPath = ImagePaths(index)
        Dim fullImgPath As String = Path.Combine(Application.StartupPath, "materials", imgPath)

        If File.Exists(fullImgPath) Then
            Dim viewer As New Form With {.Text = "Image Proof", .Size = New Size(400, 400)}
            Dim pic As New PictureBox With {
                .Image = Image.FromFile(fullImgPath),
                .Dock = DockStyle.Fill,
                .SizeMode = PictureBoxSizeMode.Zoom
            }
            viewer.Controls.Add(pic)
            viewer.ShowDialog()
        Else
            MessageBox.Show("⚠️ Image file not found.")
        End If

    End Sub

    ' Approve
    Private Sub btnYes_Click(sender As Object, e As EventArgs) Handles btnYes1.Click, btnYes2.Click, btnYes3.Click, btnYes4.Click
        Dim button As System.Windows.Forms.Button = CType(sender, System.Windows.Forms.Button)
        Dim index = Array.IndexOf({btnYes1, btnYes2, btnYes3, btnYes4}, button)
        Dim rewardBox = {txtReward1, txtReward2, txtReward3, txtReward4}(index)

        Dim reward As Integer
        If Not Integer.TryParse(rewardBox.Text.Trim(), reward) Then
            MessageBox.Show("❗ Invalid reward points.")
            Return
        End If

        Try
            DBHelper.OpenConnection()

            ' 1. Update Submissions table (Reward + Status)
            Dim updateCmd As New OleDbCommand("UPDATE Submissions SET Reward = ?, Status = 'Approved' WHERE ID = ?", DBHelper.conn)
            updateCmd.Parameters.AddWithValue("?", reward)
            updateCmd.Parameters.AddWithValue("?", SubmissionIDs(index))
            updateCmd.ExecuteNonQuery()

            ' 2. Update WalletPts
            Dim walletCmd As New OleDbCommand("UPDATE user_signup SET WalletPts = WalletPts + ? WHERE ID = ?", DBHelper.conn)
            walletCmd.Parameters.AddWithValue("?", reward)
            walletCmd.Parameters.AddWithValue("?", UserIDs(index))
            walletCmd.ExecuteNonQuery()

            ' 3. Confirm WalletPts updated
            Dim checkCmd As New OleDbCommand("SELECT WalletPts FROM user_signup WHERE ID = ?", DBHelper.conn)
            checkCmd.Parameters.AddWithValue("?", UserIDs(index))
            Dim currentPts = checkCmd.ExecuteScalar()
            MessageBox.Show($"🔎 New WalletPts: {currentPts}")

            ' 4. Refresh wallet label
            If Application.OpenForms().OfType(Of frmMenu).Any() Then
                frmMenu.RefreshWalletBalance()
            End If

            ' 5. Get item type from DB (with safety check for connection)
            Dim itemType As String = "Unknown"
            Try
                If DBHelper.conn.State <> ConnectionState.Open Then
                    DBHelper.OpenConnection()
                End If

                Dim itemCmd As New OleDbCommand("SELECT ItemType FROM Submissions WHERE ID = ?", DBHelper.conn)
                itemCmd.Parameters.AddWithValue("?", SubmissionIDs(index))
                Dim result = itemCmd.ExecuteScalar()
                If result IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(result.ToString()) Then
                    itemType = result.ToString().Trim()
                Else
                    MessageBox.Show("❌ Could not fetch ItemType from DB for SubmissionID: " & SubmissionIDs(index))
                    Exit Sub
                End If
            Catch ex As Exception
                MessageBox.Show("❌ Failed to retrieve ItemType: " & ex.Message)
                Exit Sub
            End Try

            ' 6. Write log
            WriteToUserLog(
            UserIDs(index),
            SubmissionIDs(index),
            itemType,
            Quantities(index).ToString(),
            reward,
            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            "Approved"
        )

            ' 7. Refresh
            MessageBox.Show("✅ Submission approved. Wallet updated.")
            LoadPendingSubmissions()
            If Application.OpenForms().OfType(Of frmStatus).Any() Then
                frmStatus.RefreshStatus()
            End If

        Catch ex As Exception
            MessageBox.Show("❌ Error approving: " & ex.Message)
        Finally
            DBHelper.CloseConnection()
        End Try
    End Sub



    ' Disapprove
    Private Sub btnNo_Click(sender As Object, e As EventArgs) Handles btnNo1.Click, btnNo2.Click, btnNo3.Click, btnNo4.Click
        Dim button As System.Windows.Forms.Button = CType(sender, System.Windows.Forms.Button)
        Dim index = Array.IndexOf({btnNo1, btnNo2, btnNo3, btnNo4}, button)

        Try
            DBHelper.OpenConnection()

            ' 1. Update status to Disapproved
            Dim cmd As New OleDbCommand("UPDATE Submissions SET Status = 'Disapproved', Reward = 0 WHERE ID = ?", DBHelper.conn)
            cmd.Parameters.AddWithValue("?", SubmissionIDs(index))
            cmd.ExecuteNonQuery()

            ' 2. Get item type from DB with connection check
            Dim itemType As String = "Unknown"
            Try
                If DBHelper.conn.State <> ConnectionState.Open Then
                    DBHelper.OpenConnection()
                End If

                Dim itemCmd As New OleDbCommand("SELECT ItemType FROM Submissions WHERE ID = ?", DBHelper.conn)
                itemCmd.Parameters.AddWithValue("?", SubmissionIDs(index))
                Dim result = itemCmd.ExecuteScalar()
                If result IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(result.ToString()) Then
                    itemType = result.ToString().Trim()
                Else
                    MessageBox.Show("❌ Could not fetch ItemType from DB for SubmissionID: " & SubmissionIDs(index))
                    Exit Sub
                End If
            Catch ex As Exception
                MessageBox.Show("❌ Failed to retrieve ItemType: " & ex.Message)
                Exit Sub
            End Try

            ' 3. Log
            WriteToUserLog(
            UserIDs(index),
            SubmissionIDs(index),
            itemType,
            Quantities(index).ToString(),
            0,
            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            "Disapproved"
        )

            ' 4. Refresh
            MessageBox.Show("⚠️ Submission disapproved.")
            LoadPendingSubmissions()
            If Application.OpenForms().OfType(Of frmStatus).Any() Then
                frmStatus.RefreshStatus()
            End If

        Catch ex As Exception
            MessageBox.Show("❌ Error disapproving: " & ex.Message)
        Finally
            DBHelper.CloseConnection()
        End Try
    End Sub

    Private Sub WriteToUserLog(userId As Integer, submissionId As Integer, itemType As String, quantity As String, reward As Integer, timestamp As String, status As String)
        Try
            Dim logPath As String = Path.Combine(Application.StartupPath, "fixed", $"HistoryLog_{userId}.txt")
            Dim line As String = $"SubmissionID: {submissionId}, Item: {itemType}, Qty: {quantity}, Reward: {reward} pts, Time: {timestamp}, Status: {status}"

            ' Ensure folder exists
            Dim folderPath As String = Path.GetDirectoryName(logPath)
            If Not Directory.Exists(folderPath) Then
                Directory.CreateDirectory(folderPath)
            End If

            ' Check for existing log lines with the same SubmissionID
            Dim alreadyLogged As Boolean = False
            If File.Exists(logPath) Then
                Dim allLines As String() = File.ReadAllLines(logPath)
                alreadyLogged = allLines.Any(Function(l) l.Trim().StartsWith($"SubmissionID: {submissionId},"))
            End If

            ' Write if not duplicate
            If Not alreadyLogged Then
                Using writer As New StreamWriter(logPath, append:=True)
                    writer.WriteLine(line)
                End Using
            End If

        Catch ex As Exception
            MessageBox.Show("❌ Error writing to user log: " & ex.Message)
        End Try
    End Sub


    ' Menu bar
    Private Sub BackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BackToolStripMenuItem.Click
        Me.Hide()
        frmStart.Show()
    End Sub

    Private Sub CloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub ContactToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ContactToolStripMenuItem.Click
        MsgBox("Any enquiries: Please contact Sortify (+0105051218)")
    End Sub
    Private Sub frmApproval_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadPendingSubmissions()
        lastLoadedTime = DateTime.Now

        ' Disable approve/disapprove buttons briefly to allow UI to populate
        Dim buttons = {btnYes1, btnYes2, btnYes3, btnYes4, btnNo1, btnNo2, btnNo3, btnNo4}
        For Each btn In buttons
            btn.Enabled = False
        Next

        ' Enable after 1 second
        Dim t As New Timer()
        AddHandler t.Tick, Sub()
                               For Each btn In buttons
                                   btn.Enabled = True
                               Next
                               t.Stop()
                           End Sub
        t.Interval = 1000
        t.Start()
    End Sub

End Class
