Imports System.Data.OleDb

Public Class DBHelper
    Public Shared conn As OleDbConnection

    ' ✅ Updated to use relative path from bin\Debug\fixed\
    Private Shared ReadOnly connectionString As String =
    $"Provider=Microsoft.ACE.OLEDB.16.0;Data Source={System.IO.Path.Combine(Application.StartupPath, "fixed", "signin_fixed.accdb")};Persist Security Info=False;"


    ' Open connection
    Public Shared Sub OpenConnection()
        Try
            If conn Is Nothing Then
                conn = New OleDbConnection(connectionString)
            End If

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
        Catch ex As Exception
            MessageBox.Show("❌ Failed to open database connection: " & ex.Message,
                            "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Close connection
    Public Shared Sub CloseConnection()
        Try
            If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        Catch ex As Exception
            MessageBox.Show("⚠️ Failed to close database connection: " & ex.Message,
                            "Database Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub
End Class
