<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmCart
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.lblShipping = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblFinalTotal = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblPrice1 = New System.Windows.Forms.Label()
        Me.lblItem1 = New System.Windows.Forms.Label()
        Me.btnPay = New System.Windows.Forms.Button()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.HomeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BackToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CloseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContactToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.lblPrice2 = New System.Windows.Forms.Label()
        Me.lblItem2 = New System.Windows.Forms.Label()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.lblPrice3 = New System.Windows.Forms.Label()
        Me.lblItem3 = New System.Windows.Forms.Label()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblShipping
        '
        Me.lblShipping.AutoSize = True
        Me.lblShipping.BackColor = System.Drawing.Color.Transparent
        Me.lblShipping.Font = New System.Drawing.Font("Noto Serif SC Black", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblShipping.Location = New System.Drawing.Point(927, 345)
        Me.lblShipping.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblShipping.Name = "lblShipping"
        Me.lblShipping.Size = New System.Drawing.Size(91, 29)
        Me.lblShipping.TabIndex = 25
        Me.lblShipping.Text = "RM5.00"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Noto Serif SC Black", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(763, 345)
        Me.Label11.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(160, 29)
        Me.Label11.TabIndex = 24
        Me.Label11.Text = "Shipping Fee :"
        '
        'lblFinalTotal
        '
        Me.lblFinalTotal.BackColor = System.Drawing.Color.White
        Me.lblFinalTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFinalTotal.Font = New System.Drawing.Font("Noto Serif SC Black", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFinalTotal.Location = New System.Drawing.Point(932, 383)
        Me.lblFinalTotal.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblFinalTotal.Name = "lblFinalTotal"
        Me.lblFinalTotal.Size = New System.Drawing.Size(116, 36)
        Me.lblFinalTotal.TabIndex = 23
        Me.lblFinalTotal.Text = "RM30.80"
        Me.lblFinalTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Noto Serif SC Black", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(763, 390)
        Me.Label10.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(78, 29)
        Me.Label10.TabIndex = 22
        Me.Label10.Text = "Total :"
        '
        'lblPrice1
        '
        Me.lblPrice1.AutoSize = True
        Me.lblPrice1.BackColor = System.Drawing.Color.Transparent
        Me.lblPrice1.Font = New System.Drawing.Font("Noto Serif SC", 10.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPrice1.Location = New System.Drawing.Point(280, 185)
        Me.lblPrice1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblPrice1.Name = "lblPrice1"
        Me.lblPrice1.Size = New System.Drawing.Size(88, 26)
        Me.lblPrice1.TabIndex = 35
        Me.lblPrice1.Text = "RM 9.80"
        '
        'lblItem1
        '
        Me.lblItem1.AutoSize = True
        Me.lblItem1.BackColor = System.Drawing.Color.Transparent
        Me.lblItem1.Font = New System.Drawing.Font("Noto Serif SC", 10.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItem1.Location = New System.Drawing.Point(278, 142)
        Me.lblItem1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblItem1.Name = "lblItem1"
        Me.lblItem1.Size = New System.Drawing.Size(61, 26)
        Me.lblItem1.TabIndex = 34
        Me.lblItem1.Text = "Jeans"
        '
        'btnPay
        '
        Me.btnPay.BackColor = System.Drawing.Color.Green
        Me.btnPay.FlatAppearance.BorderSize = 0
        Me.btnPay.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPay.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnPay.Location = New System.Drawing.Point(932, 445)
        Me.btnPay.Name = "btnPay"
        Me.btnPay.Size = New System.Drawing.Size(116, 42)
        Me.btnPay.TabIndex = 47
        Me.btnPay.Text = "PAY"
        Me.btnPay.UseVisualStyleBackColor = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HomeToolStripMenuItem, Me.ContactToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1152, 33)
        Me.MenuStrip1.TabIndex = 48
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'HomeToolStripMenuItem
        '
        Me.HomeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BackToolStripMenuItem, Me.CloseToolStripMenuItem})
        Me.HomeToolStripMenuItem.Font = New System.Drawing.Font("Noto Serif SC", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HomeToolStripMenuItem.Name = "HomeToolStripMenuItem"
        Me.HomeToolStripMenuItem.Size = New System.Drawing.Size(79, 29)
        Me.HomeToolStripMenuItem.Text = "Home"
        '
        'BackToolStripMenuItem
        '
        Me.BackToolStripMenuItem.Name = "BackToolStripMenuItem"
        Me.BackToolStripMenuItem.Size = New System.Drawing.Size(224, 30)
        Me.BackToolStripMenuItem.Text = "Back"
        '
        'CloseToolStripMenuItem
        '
        Me.CloseToolStripMenuItem.Name = "CloseToolStripMenuItem"
        Me.CloseToolStripMenuItem.Size = New System.Drawing.Size(224, 30)
        Me.CloseToolStripMenuItem.Text = "Close"
        '
        'ContactToolStripMenuItem
        '
        Me.ContactToolStripMenuItem.Font = New System.Drawing.Font("Noto Serif SC Black", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ContactToolStripMenuItem.Name = "ContactToolStripMenuItem"
        Me.ContactToolStripMenuItem.Size = New System.Drawing.Size(95, 29)
        Me.ContactToolStripMenuItem.Text = "Contact"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.SeaShell
        Me.Label12.Font = New System.Drawing.Font("Cooper Black", 22.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.DarkOliveGreen
        Me.Label12.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label12.Location = New System.Drawing.Point(441, 53)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(249, 52)
        Me.Label12.TabIndex = 49
        Me.Label12.Text = "MY CART"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(110, 133)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(133, 105)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 53
        Me.PictureBox1.TabStop = False
        '
        'PictureBox4
        '
        Me.PictureBox4.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox4.BackgroundImage = Global.OMC_LASTEST.My.Resources.Resources.logo
        Me.PictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox4.Location = New System.Drawing.Point(696, 53)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(253, 209)
        Me.PictureBox4.TabIndex = 54
        Me.PictureBox4.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Location = New System.Drawing.Point(110, 264)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(133, 105)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 59
        Me.PictureBox2.TabStop = False
        '
        'lblPrice2
        '
        Me.lblPrice2.AutoSize = True
        Me.lblPrice2.BackColor = System.Drawing.Color.Transparent
        Me.lblPrice2.Font = New System.Drawing.Font("Noto Serif SC", 10.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPrice2.Location = New System.Drawing.Point(280, 316)
        Me.lblPrice2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblPrice2.Name = "lblPrice2"
        Me.lblPrice2.Size = New System.Drawing.Size(88, 26)
        Me.lblPrice2.TabIndex = 58
        Me.lblPrice2.Text = "RM 9.80"
        '
        'lblItem2
        '
        Me.lblItem2.AutoSize = True
        Me.lblItem2.BackColor = System.Drawing.Color.Transparent
        Me.lblItem2.Font = New System.Drawing.Font("Noto Serif SC", 10.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItem2.Location = New System.Drawing.Point(278, 273)
        Me.lblItem2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblItem2.Name = "lblItem2"
        Me.lblItem2.Size = New System.Drawing.Size(61, 26)
        Me.lblItem2.TabIndex = 57
        Me.lblItem2.Text = "Jeans"
        '
        'PictureBox3
        '
        Me.PictureBox3.Location = New System.Drawing.Point(110, 393)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(133, 105)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox3.TabIndex = 64
        Me.PictureBox3.TabStop = False
        '
        'lblPrice3
        '
        Me.lblPrice3.AutoSize = True
        Me.lblPrice3.BackColor = System.Drawing.Color.Transparent
        Me.lblPrice3.Font = New System.Drawing.Font("Noto Serif SC", 10.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPrice3.Location = New System.Drawing.Point(280, 445)
        Me.lblPrice3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblPrice3.Name = "lblPrice3"
        Me.lblPrice3.Size = New System.Drawing.Size(88, 26)
        Me.lblPrice3.TabIndex = 63
        Me.lblPrice3.Text = "RM 9.80"
        '
        'lblItem3
        '
        Me.lblItem3.AutoSize = True
        Me.lblItem3.BackColor = System.Drawing.Color.Transparent
        Me.lblItem3.Font = New System.Drawing.Font("Noto Serif SC", 10.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItem3.Location = New System.Drawing.Point(278, 402)
        Me.lblItem3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblItem3.Name = "lblItem3"
        Me.lblItem3.Size = New System.Drawing.Size(61, 26)
        Me.lblItem3.TabIndex = 62
        Me.lblItem3.Text = "Jeans"
        '
        'frmCart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.OMC_LASTEST.My.Resources.Resources.background
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1152, 530)
        Me.Controls.Add(Me.PictureBox3)
        Me.Controls.Add(Me.lblPrice3)
        Me.Controls.Add(Me.lblItem3)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.lblPrice2)
        Me.Controls.Add(Me.lblItem2)
        Me.Controls.Add(Me.PictureBox4)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.btnPay)
        Me.Controls.Add(Me.lblPrice1)
        Me.Controls.Add(Me.lblItem1)
        Me.Controls.Add(Me.lblShipping)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.lblFinalTotal)
        Me.Controls.Add(Me.Label10)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "frmCart"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "BUY ITEMS"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblShipping As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents lblFinalTotal As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents lblPrice1 As Label
    Friend WithEvents lblItem1 As Label
    Friend WithEvents btnPay As Button
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents HomeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BackToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CloseToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ContactToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Label12 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents PictureBox4 As PictureBox
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents lblPrice2 As Label
    Friend WithEvents lblItem2 As Label
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents lblPrice3 As Label
    Friend WithEvents lblItem3 As Label
End Class
