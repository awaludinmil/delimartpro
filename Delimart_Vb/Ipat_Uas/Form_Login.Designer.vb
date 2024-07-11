<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_Login
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
        Me.Guna2ControlBox1 = New Guna.UI2.WinForms.Guna2ControlBox()
        Me.Guna2ShadowPanel1 = New Guna.UI2.WinForms.Guna2ShadowPanel()
        Me.msg_eror_pw = New System.Windows.Forms.Label()
        Me.msg_eror_username = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btn_cancel = New Guna.UI2.WinForms.Guna2Button()
        Me.txt_user = New Guna.UI2.WinForms.Guna2TextBox()
        Me.btn_login = New Guna.UI2.WinForms.Guna2Button()
        Me.Guna2HtmlLabel1 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.txt_pw = New Guna.UI2.WinForms.Guna2TextBox()
        Me.Guna2ShadowPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Guna2ControlBox1
        '
        Me.Guna2ControlBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Guna2ControlBox1.FillColor = System.Drawing.Color.FromArgb(CType(CType(139, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.Guna2ControlBox1.HoverState.Parent = Me.Guna2ControlBox1
        Me.Guna2ControlBox1.IconColor = System.Drawing.Color.White
        Me.Guna2ControlBox1.Location = New System.Drawing.Point(681, 0)
        Me.Guna2ControlBox1.Name = "Guna2ControlBox1"
        Me.Guna2ControlBox1.ShadowDecoration.Parent = Me.Guna2ControlBox1
        Me.Guna2ControlBox1.Size = New System.Drawing.Size(45, 29)
        Me.Guna2ControlBox1.TabIndex = 2
        '
        'Guna2ShadowPanel1
        '
        Me.Guna2ShadowPanel1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Guna2ShadowPanel1.BackColor = System.Drawing.Color.Transparent
        Me.Guna2ShadowPanel1.Controls.Add(Me.msg_eror_pw)
        Me.Guna2ShadowPanel1.Controls.Add(Me.msg_eror_username)
        Me.Guna2ShadowPanel1.Controls.Add(Me.Label1)
        Me.Guna2ShadowPanel1.Controls.Add(Me.btn_cancel)
        Me.Guna2ShadowPanel1.Controls.Add(Me.txt_user)
        Me.Guna2ShadowPanel1.Controls.Add(Me.btn_login)
        Me.Guna2ShadowPanel1.Controls.Add(Me.Guna2HtmlLabel1)
        Me.Guna2ShadowPanel1.Controls.Add(Me.txt_pw)
        Me.Guna2ShadowPanel1.FillColor = System.Drawing.Color.White
        Me.Guna2ShadowPanel1.Location = New System.Drawing.Point(150, 88)
        Me.Guna2ShadowPanel1.Name = "Guna2ShadowPanel1"
        Me.Guna2ShadowPanel1.ShadowColor = System.Drawing.Color.Black
        Me.Guna2ShadowPanel1.ShadowShift = 8
        Me.Guna2ShadowPanel1.Size = New System.Drawing.Size(435, 402)
        Me.Guna2ShadowPanel1.TabIndex = 8
        '
        'msg_eror_pw
        '
        Me.msg_eror_pw.AutoSize = True
        Me.msg_eror_pw.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.msg_eror_pw.ForeColor = System.Drawing.Color.Red
        Me.msg_eror_pw.Location = New System.Drawing.Point(84, 239)
        Me.msg_eror_pw.Name = "msg_eror_pw"
        Me.msg_eror_pw.Size = New System.Drawing.Size(82, 17)
        Me.msg_eror_pw.TabIndex = 13
        Me.msg_eror_pw.Text = "Isi Password"
        Me.msg_eror_pw.Visible = False
        '
        'msg_eror_username
        '
        Me.msg_eror_username.AutoSize = True
        Me.msg_eror_username.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.msg_eror_username.ForeColor = System.Drawing.Color.Red
        Me.msg_eror_username.Location = New System.Drawing.Point(84, 170)
        Me.msg_eror_username.Name = "msg_eror_username"
        Me.msg_eror_username.Size = New System.Drawing.Size(85, 17)
        Me.msg_eror_username.TabIndex = 12
        Me.msg_eror_username.Text = "Isi Username"
        Me.msg_eror_username.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 16.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(82, 73)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(290, 27)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Please Login To Continue"
        '
        'btn_cancel
        '
        Me.btn_cancel.Animated = True
        Me.btn_cancel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.btn_cancel.BorderRadius = 20
        Me.btn_cancel.BorderThickness = 2
        Me.btn_cancel.CheckedState.Parent = Me.btn_cancel
        Me.btn_cancel.CustomImages.Parent = Me.btn_cancel
        Me.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_cancel.FillColor = System.Drawing.Color.White
        Me.btn_cancel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_cancel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.btn_cancel.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.btn_cancel.HoverState.ForeColor = System.Drawing.Color.White
        Me.btn_cancel.HoverState.Parent = Me.btn_cancel
        Me.btn_cancel.Location = New System.Drawing.Point(74, 325)
        Me.btn_cancel.Name = "btn_cancel"
        Me.btn_cancel.ShadowDecoration.Parent = Me.btn_cancel
        Me.btn_cancel.Size = New System.Drawing.Size(304, 45)
        Me.btn_cancel.TabIndex = 8
        Me.btn_cancel.Text = "Cancel"
        '
        'txt_user
        '
        Me.txt_user.Animated = True
        Me.txt_user.BorderColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.txt_user.BorderRadius = 10
        Me.txt_user.BorderThickness = 2
        Me.txt_user.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txt_user.DefaultText = ""
        Me.txt_user.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.txt_user.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.txt_user.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txt_user.DisabledState.Parent = Me.txt_user
        Me.txt_user.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txt_user.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.txt_user.FocusedState.Parent = Me.txt_user
        Me.txt_user.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_user.ForeColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.txt_user.HoverState.BorderColor = System.Drawing.Color.Transparent
        Me.txt_user.HoverState.Parent = Me.txt_user
        Me.txt_user.IconLeft = Global.Ipat_Uas.My.Resources.Resources.profile_user__1_
        Me.txt_user.IconLeftOffset = New System.Drawing.Point(5, 0)
        Me.txt_user.IconLeftSize = New System.Drawing.Size(26, 26)
        Me.txt_user.Location = New System.Drawing.Point(74, 122)
        Me.txt_user.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txt_user.Name = "txt_user"
        Me.txt_user.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txt_user.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.txt_user.PlaceholderText = "Username"
        Me.txt_user.SelectedText = ""
        Me.txt_user.ShadowDecoration.Parent = Me.txt_user
        Me.txt_user.Size = New System.Drawing.Size(304, 45)
        Me.txt_user.TabIndex = 4
        Me.txt_user.TextOffset = New System.Drawing.Point(3, 0)
        '
        'btn_login
        '
        Me.btn_login.Animated = True
        Me.btn_login.BorderRadius = 20
        Me.btn_login.CheckedState.Parent = Me.btn_login
        Me.btn_login.CustomImages.Parent = Me.btn_login
        Me.btn_login.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_login.FillColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.btn_login.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_login.ForeColor = System.Drawing.Color.White
        Me.btn_login.HoverState.Parent = Me.btn_login
        Me.btn_login.Location = New System.Drawing.Point(74, 269)
        Me.btn_login.Name = "btn_login"
        Me.btn_login.ShadowDecoration.Parent = Me.btn_login
        Me.btn_login.Size = New System.Drawing.Size(304, 45)
        Me.btn_login.TabIndex = 0
        Me.btn_login.Text = "Login"
        '
        'Guna2HtmlLabel1
        '
        Me.Guna2HtmlLabel1.BackColor = System.Drawing.Color.White
        Me.Guna2HtmlLabel1.Font = New System.Drawing.Font("Tahoma", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.Guna2HtmlLabel1.Location = New System.Drawing.Point(152, 35)
        Me.Guna2HtmlLabel1.Name = "Guna2HtmlLabel1"
        Me.Guna2HtmlLabel1.Size = New System.Drawing.Size(143, 35)
        Me.Guna2HtmlLabel1.TabIndex = 6
        Me.Guna2HtmlLabel1.Text = "WELCOME"
        '
        'txt_pw
        '
        Me.txt_pw.Animated = True
        Me.txt_pw.BorderColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.txt_pw.BorderRadius = 10
        Me.txt_pw.BorderThickness = 2
        Me.txt_pw.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txt_pw.DefaultText = ""
        Me.txt_pw.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.txt_pw.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.txt_pw.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txt_pw.DisabledState.Parent = Me.txt_pw
        Me.txt_pw.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txt_pw.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.txt_pw.FocusedState.Parent = Me.txt_pw
        Me.txt_pw.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_pw.ForeColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.txt_pw.HoverState.BorderColor = System.Drawing.Color.Transparent
        Me.txt_pw.HoverState.Parent = Me.txt_pw
        Me.txt_pw.IconLeft = Global.Ipat_Uas.My.Resources.Resources.padlock__1_
        Me.txt_pw.IconLeftOffset = New System.Drawing.Point(5, 0)
        Me.txt_pw.IconLeftSize = New System.Drawing.Size(26, 26)
        Me.txt_pw.IconRight = Global.Ipat_Uas.My.Resources.Resources.eye__1_1
        Me.txt_pw.IconRightCursor = System.Windows.Forms.Cursors.Hand
        Me.txt_pw.IconRightOffset = New System.Drawing.Point(5, 0)
        Me.txt_pw.IconRightSize = New System.Drawing.Size(30, 30)
        Me.txt_pw.Location = New System.Drawing.Point(74, 191)
        Me.txt_pw.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txt_pw.Name = "txt_pw"
        Me.txt_pw.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txt_pw.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.txt_pw.PlaceholderText = "Password"
        Me.txt_pw.SelectedText = ""
        Me.txt_pw.ShadowDecoration.Parent = Me.txt_pw
        Me.txt_pw.Size = New System.Drawing.Size(304, 45)
        Me.txt_pw.TabIndex = 5
        Me.txt_pw.TextOffset = New System.Drawing.Point(3, 0)
        Me.txt_pw.UseSystemPasswordChar = True
        '
        'Form_Login
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(726, 526)
        Me.ControlBox = False
        Me.Controls.Add(Me.Guna2ShadowPanel1)
        Me.Controls.Add(Me.Guna2ControlBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form_Login"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Guna2ShadowPanel1.ResumeLayout(False)
        Me.Guna2ShadowPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Guna2ControlBox1 As Guna.UI2.WinForms.Guna2ControlBox
    Friend WithEvents Guna2ShadowPanel1 As Guna.UI2.WinForms.Guna2ShadowPanel
    Friend WithEvents btn_cancel As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents txt_user As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents btn_login As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents Guna2HtmlLabel1 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents txt_pw As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents msg_eror_username As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents msg_eror_pw As System.Windows.Forms.Label

End Class
