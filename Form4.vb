Imports System.Data.OleDb ' For Access database connection
Imports System.IO ' For reading/writing log files

Public Class frmStatus
    Private hasShownNoDataMessage As Boolean = False
    ' Triggered when the form becomes active → refreshes data

    Private isFirstOpen As Boolean = True
    Private Sub frmStatus_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        LoadLatestSubmission() ' Always refresh
    End Sub


    ' Load latest submissions that are not yet approved
    Private Sub LoadLatestSubmission()
        FlowLayoutPanel1.Controls.Clear() ' Clear old cards before loading new

        Try
            DBHelper.OpenConnection() ' Open DB connection

            ' Query: Get user's pending submissions (latest first)
            Dim cmd As New OleDbCommand("SELECT * FROM Submissions WHERE User_ID = ? AND UCASE(TRIM(Status)) = 'PENDING'", DBHelper.conn)
            cmd.Parameters.AddWithValue("?", Session.LoggedInUserID)

            Using reader As OleDbDataReader = cmd.ExecuteReader()
                Dim found As Boolean = False

                ' Fonts for label formatting
                Dim fontLabel As New Font("Noto Serif Black", 10, FontStyle.Bold)
                Dim fontValue As New Font("Segoe UI", 10, FontStyle.Regular)

                While reader.Read()
                    found = True

                    ' Create a card-like panel for each submission
                    Dim panel As New Panel With {
                        .Width = FlowLayoutPanel1.ClientSize.Width - 25,
                        .Height = 150,
                        .BorderStyle = BorderStyle.FixedSingle,
                        .Padding = New Padding(10),
                        .BackColor = Color.White
                    }

                    ' --- Add image if available
                    Dim pic As New PictureBox With {
                        .Size = New Size(120, 120),
                        .Location = New Point(10, 10),
                        .SizeMode = PictureBoxSizeMode.Zoom,
                        .BorderStyle = BorderStyle.FixedSingle
                    }

                    ' Load image from path if it exists
                    If Not IsDBNull(reader("ImagePath")) Then
                        Dim fileName As String = reader("ImagePath").ToString()
                        Dim fullPath As String = Path.Combine(Application.StartupPath, "materials", fileName)

                        If File.Exists(fullPath) Then
                            Using fs As New FileStream(fullPath, FileMode.Open, FileAccess.Read)
                                pic.Image = Image.FromStream(fs)
                            End Using
                        End If

                    End If
                    panel.Controls.Add(pic)

                    ' --- Add data labels beside image
                    Dim startX As Integer = 150
                    Dim startY As Integer = 10
                    Dim spacing As Integer = 25

                    ' Key-value pairs to display
                    Dim lbls = {
                        ("Items :", reader("ItemType").ToString()),
                        ("Quantity :", reader("Quantity").ToString()),
                        ("Status :", reader("Status").ToString()),
                        ("Time Uploaded :", reader("TimeStamp").ToString()),
                        ("Reward :", $"{reader("Reward")} pts")
                    }
                    '.Item1            '.Item2    'default in VB
                    ' Create label pairs dynamically
                    For i = 0 To lbls.Length - 1
                        Dim lblKey As New Label With {
                            .Text = lbls(i).Item1,
                            .Font = fontLabel,
                            .ForeColor = Color.Green,
                            .Location = New Point(startX, startY + spacing * i),
                            .AutoSize = True
                        }


                        Dim extraShift As Integer = If(lbls(i).Item1.Contains("Time"), 140, 100)
                        Dim lblVal As New Label With {
                            .Text = lbls(i).Item2,
                            .Font = fontValue,
                            .Location = New Point(startX + extraShift, startY + spacing * i),
                            .AutoSize = True
                        }

                        panel.Controls.Add(lblKey)
                        panel.Controls.Add(lblVal)
                    Next

                    ' Add panel to FlowLayoutPanel
                    FlowLayoutPanel1.Controls.Add(panel)
                End While

                ' If no pending submission found, inform user (but only once)
                If Not found AndAlso Not hasShownNoDataMessage AndAlso Me.Visible Then
                    hasShownNoDataMessage = True
                    MessageBox.Show("ℹ️ No pending submission found. Please check your History.", "No Data")
                End If
            End Using

        Catch ex As Exception
            MessageBox.Show("❌ Error loading status: " & ex.Message)
        Finally
            DBHelper.CloseConnection()
        End Try
    End Sub

    ' --- Menu Navigation Buttons

    ' Back to main menu
    Private Sub BackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BackToolStripMenuItem.Click
        Me.Hide()
        frmMenu.Show()
    End Sub

    ' Confirm logout and return to Sign In page
    Private Sub CloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click
        If MsgBox("Sure to logout?", vbYesNo + vbQuestion, "Logout Confirmation") = vbYes Then
            Me.Hide()
            frmSign.Show()
        End If
    End Sub

    ' Show contact information
    Private Sub ContactToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ContactToolStripMenuItem.Click
        MsgBox("📞 Any enquiries: Please contact Sortify (+0105051218)")
    End Sub

    ' Open the full submission history form
    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Me.Hide()
        frmHistory.Show()
    End Sub

    ' Allow other forms to refresh this form externally
    Public Sub RefreshStatus()
        LoadLatestSubmission()
    End Sub
    Private Sub frmStatus_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        isFirstOpen = True
    End Sub
End Class
