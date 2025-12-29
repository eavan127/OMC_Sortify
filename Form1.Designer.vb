<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStart
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lblSortify = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnStartUser = New System.Windows.Forms.Button()
        Me.btnStartAdmin = New System.Windows.Forms.Button()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.CloseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblSortify
        '
        Me.lblSortify.AutoSize = True
        Me.lblSortify.BackColor = System.Drawing.Color.Transparent
        Me.lblSortify.Font = New System.Drawing.Font("Cooper Black", 72.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSortify.ForeColor = System.Drawing.Color.Green
        Me.lblSortify.Location = New System.Drawing.Point(255, 122)
        Me.lblSortify.Name = "lblSortify"
        Me.lblSortify.Size = New System.Drawing.Size(628, 137)
        Me.lblSortify.TabIndex = 0
        Me.lblSortify.Text = "SORTIFY"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Lucida Bright", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Green
        Me.Label1.Location = New System.Drawing.Point(321, 259)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(466, 68)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "CONNECT YOUR COMMUNITY " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "THROUGH BETTER HABITS"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'btnStartUser
        '
        Me.btnStartUser.BackColor = System.Drawing.Color.Green
        Me.btnStartUser.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnStartUser.FlatAppearance.BorderSize = 0
        Me.btnStartUser.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnStartUser.Font = New System.Drawing.Font("Cooper Black", 28.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnStartUser.ForeColor = System.Drawing.Color.White
        Me.btnStartUser.Location = New System.Drawing.Point(230, 369)
        Me.btnStartUser.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnStartUser.Name = "btnStartUser"
        Me.btnStartUser.Size = New System.Drawing.Size(236, 83)
        Me.btnStartUser.TabIndex = 2
        Me.btnStartUser.Text = "USER"
        Me.btnStartUser.UseVisualStyleBackColor = False
        '
        'btnStartAdmin
        '
        Me.btnStartAdmin.BackColor = System.Drawing.Color.Green
        Me.btnStartAdmin.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnStartAdmin.FlatAppearance.BorderSize = 0
        Me.btnStartAdmin.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnStartAdmin.Font = New System.Drawing.Font("Cooper Black", 28.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnStartAdmin.ForeColor = System.Drawing.Color.White
        Me.btnStartAdmin.Location = New System.Drawing.Point(647, 369)
        Me.btnStartAdmin.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnStartAdmin.Name = "btnStartAdmin"
        Me.btnStartAdmin.Size = New System.Drawing.Size(236, 83)
        Me.btnStartAdmin.TabIndex = 4
        Me.btnStartAdmin.Text = "ADMIN"
        Me.btnStartAdmin.UseVisualStyleBackColor = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CloseToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1120, 33)
        Me.MenuStrip1.TabIndex = 5
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'CloseToolStripMenuItem
        '
        Me.CloseToolStripMenuItem.Font = New System.Drawing.Font("Noto Serif SC", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CloseToolStripMenuItem.Name = "CloseToolStripMenuItem"
        Me.CloseToolStripMenuItem.Size = New System.Drawing.Size(71, 29)
        Me.CloseToolStripMenuItem.Text = "Close"
        '
        'frmStart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.OMC_LASTEST.My.Resources.Resources.background
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1120, 524)
        Me.Controls.Add(Me.btnStartAdmin)
        Me.Controls.Add(Me.btnStartUser)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblSortify)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "frmStart"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "START"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblSortify As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents btnStartUser As Button
    Friend WithEvents btnStartAdmin As Button
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents CloseToolStripMenuItem As ToolStripMenuItem
End Class
