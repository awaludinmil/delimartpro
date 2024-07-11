<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_Pulsa
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.txt_no_hp = New Guna.UI2.WinForms.Guna2TextBox()
        Me.txt_provider = New Guna.UI2.WinForms.Guna2TextBox()
        Me.Guna2HtmlLabel1 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.Guna2HtmlLabel2 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.btn_batal = New Guna.UI2.WinForms.Guna2Button()
        Me.Guna2GroupBox1 = New Guna.UI2.WinForms.Guna2GroupBox()
        Me.dg_pulsa = New Guna.UI2.WinForms.Guna2DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Guna2ControlBox1 = New Guna.UI2.WinForms.Guna2ControlBox()
        Me.Guna2GroupBox1.SuspendLayout()
        CType(Me.dg_pulsa, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txt_no_hp
        '
        Me.txt_no_hp.BorderColor = System.Drawing.Color.Black
        Me.txt_no_hp.BorderRadius = 5
        Me.txt_no_hp.BorderThickness = 2
        Me.txt_no_hp.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txt_no_hp.DefaultText = ""
        Me.txt_no_hp.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.txt_no_hp.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.txt_no_hp.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txt_no_hp.DisabledState.Parent = Me.txt_no_hp
        Me.txt_no_hp.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txt_no_hp.FocusedState.BorderColor = System.Drawing.Color.Black
        Me.txt_no_hp.FocusedState.Parent = Me.txt_no_hp
        Me.txt_no_hp.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_no_hp.ForeColor = System.Drawing.Color.Black
        Me.txt_no_hp.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txt_no_hp.HoverState.Parent = Me.txt_no_hp
        Me.txt_no_hp.Location = New System.Drawing.Point(103, 36)
        Me.txt_no_hp.Margin = New System.Windows.Forms.Padding(4)
        Me.txt_no_hp.Name = "txt_no_hp"
        Me.txt_no_hp.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txt_no_hp.PlaceholderText = ""
        Me.txt_no_hp.SelectedText = ""
        Me.txt_no_hp.ShadowDecoration.Parent = Me.txt_no_hp
        Me.txt_no_hp.Size = New System.Drawing.Size(283, 34)
        Me.txt_no_hp.TabIndex = 0
        '
        'txt_provider
        '
        Me.txt_provider.BorderColor = System.Drawing.Color.Black
        Me.txt_provider.BorderRadius = 5
        Me.txt_provider.BorderThickness = 2
        Me.txt_provider.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txt_provider.DefaultText = ""
        Me.txt_provider.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.txt_provider.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.txt_provider.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txt_provider.DisabledState.Parent = Me.txt_provider
        Me.txt_provider.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txt_provider.Enabled = False
        Me.txt_provider.FocusedState.BorderColor = System.Drawing.Color.Black
        Me.txt_provider.FocusedState.Parent = Me.txt_provider
        Me.txt_provider.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_provider.ForeColor = System.Drawing.Color.Black
        Me.txt_provider.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txt_provider.HoverState.Parent = Me.txt_provider
        Me.txt_provider.Location = New System.Drawing.Point(103, 86)
        Me.txt_provider.Margin = New System.Windows.Forms.Padding(4)
        Me.txt_provider.Name = "txt_provider"
        Me.txt_provider.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txt_provider.PlaceholderText = ""
        Me.txt_provider.SelectedText = ""
        Me.txt_provider.ShadowDecoration.Parent = Me.txt_provider
        Me.txt_provider.Size = New System.Drawing.Size(283, 36)
        Me.txt_provider.TabIndex = 1
        Me.txt_provider.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Guna2HtmlLabel1
        '
        Me.Guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel1.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel1.Location = New System.Drawing.Point(12, 41)
        Me.Guna2HtmlLabel1.Name = "Guna2HtmlLabel1"
        Me.Guna2HtmlLabel1.Size = New System.Drawing.Size(85, 21)
        Me.Guna2HtmlLabel1.TabIndex = 3
        Me.Guna2HtmlLabel1.Text = "Nomor HP"
        '
        'Guna2HtmlLabel2
        '
        Me.Guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel2.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel2.Location = New System.Drawing.Point(12, 93)
        Me.Guna2HtmlLabel2.Name = "Guna2HtmlLabel2"
        Me.Guna2HtmlLabel2.Size = New System.Drawing.Size(72, 21)
        Me.Guna2HtmlLabel2.TabIndex = 5
        Me.Guna2HtmlLabel2.Text = "Provider"
        '
        'btn_batal
        '
        Me.btn_batal.BorderRadius = 5
        Me.btn_batal.BorderThickness = 2
        Me.btn_batal.CheckedState.Parent = Me.btn_batal
        Me.btn_batal.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_batal.CustomImages.Parent = Me.btn_batal
        Me.btn_batal.FillColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.btn_batal.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_batal.ForeColor = System.Drawing.Color.White
        Me.btn_batal.HoverState.Parent = Me.btn_batal
        Me.btn_batal.Location = New System.Drawing.Point(12, 334)
        Me.btn_batal.Name = "btn_batal"
        Me.btn_batal.ShadowDecoration.Parent = Me.btn_batal
        Me.btn_batal.Size = New System.Drawing.Size(377, 45)
        Me.btn_batal.TabIndex = 6
        Me.btn_batal.Text = "Batal"
        '
        'Guna2GroupBox1
        '
        Me.Guna2GroupBox1.BorderColor = System.Drawing.Color.Black
        Me.Guna2GroupBox1.BorderRadius = 5
        Me.Guna2GroupBox1.BorderThickness = 2
        Me.Guna2GroupBox1.Controls.Add(Me.dg_pulsa)
        Me.Guna2GroupBox1.CustomBorderColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.Guna2GroupBox1.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2GroupBox1.ForeColor = System.Drawing.Color.White
        Me.Guna2GroupBox1.Location = New System.Drawing.Point(12, 128)
        Me.Guna2GroupBox1.Name = "Guna2GroupBox1"
        Me.Guna2GroupBox1.ShadowDecoration.Parent = Me.Guna2GroupBox1
        Me.Guna2GroupBox1.Size = New System.Drawing.Size(377, 200)
        Me.Guna2GroupBox1.TabIndex = 7
        Me.Guna2GroupBox1.Text = "List Produk Pulsa"
        '
        'dg_pulsa
        '
        Me.dg_pulsa.AllowUserToAddRows = False
        Me.dg_pulsa.AllowUserToDeleteRows = False
        Me.dg_pulsa.AllowUserToResizeColumns = False
        Me.dg_pulsa.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightSkyBlue
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black
        Me.dg_pulsa.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dg_pulsa.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dg_pulsa.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dg_pulsa.BackgroundColor = System.Drawing.Color.Gainsboro
        Me.dg_pulsa.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dg_pulsa.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical
        Me.dg_pulsa.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.Gainsboro
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dg_pulsa.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dg_pulsa.ColumnHeadersHeight = 30
        Me.dg_pulsa.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dg_pulsa.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.Column6, Me.Column3, Me.Column1})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(74, Byte), Integer))
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightSkyBlue
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dg_pulsa.DefaultCellStyle = DataGridViewCellStyle3
        Me.dg_pulsa.EnableHeadersVisualStyles = False
        Me.dg_pulsa.GridColor = System.Drawing.Color.Black
        Me.dg_pulsa.Location = New System.Drawing.Point(3, 39)
        Me.dg_pulsa.Name = "dg_pulsa"
        Me.dg_pulsa.ReadOnly = True
        Me.dg_pulsa.RowHeadersVisible = False
        Me.dg_pulsa.RowTemplate.Height = 23
        Me.dg_pulsa.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dg_pulsa.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dg_pulsa.Size = New System.Drawing.Size(371, 158)
        Me.dg_pulsa.TabIndex = 5
        Me.dg_pulsa.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.[Default]
        Me.dg_pulsa.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White
        Me.dg_pulsa.ThemeStyle.AlternatingRowsStyle.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dg_pulsa.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Black
        Me.dg_pulsa.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.LightSkyBlue
        Me.dg_pulsa.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.dg_pulsa.ThemeStyle.BackColor = System.Drawing.Color.Gainsboro
        Me.dg_pulsa.ThemeStyle.GridColor = System.Drawing.Color.Black
        Me.dg_pulsa.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.Gainsboro
        Me.dg_pulsa.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        Me.dg_pulsa.ThemeStyle.HeaderStyle.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dg_pulsa.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.Black
        Me.dg_pulsa.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dg_pulsa.ThemeStyle.HeaderStyle.Height = 30
        Me.dg_pulsa.ThemeStyle.ReadOnly = True
        Me.dg_pulsa.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.dg_pulsa.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical
        Me.dg_pulsa.ThemeStyle.RowsStyle.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dg_pulsa.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.White
        Me.dg_pulsa.ThemeStyle.RowsStyle.Height = 23
        Me.dg_pulsa.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.LightSkyBlue
        Me.dg_pulsa.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.Black
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "No"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        '
        'Column6
        '
        Me.Column6.HeaderText = "Nama Produk"
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        '
        'Column3
        '
        Me.Column3.HeaderText = "Harga"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        '
        'Column1
        '
        Me.Column1.HeaderText = "kd_pulsa"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Visible = False
        '
        'Guna2ControlBox1
        '
        Me.Guna2ControlBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Guna2ControlBox1.FillColor = System.Drawing.Color.Red
        Me.Guna2ControlBox1.HoverState.Parent = Me.Guna2ControlBox1
        Me.Guna2ControlBox1.IconColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.Guna2ControlBox1.Location = New System.Drawing.Point(356, -1)
        Me.Guna2ControlBox1.Name = "Guna2ControlBox1"
        Me.Guna2ControlBox1.ShadowDecoration.Parent = Me.Guna2ControlBox1
        Me.Guna2ControlBox1.Size = New System.Drawing.Size(45, 29)
        Me.Guna2ControlBox1.TabIndex = 8
        '
        'Form_Pulsa
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(401, 391)
        Me.ControlBox = False
        Me.Controls.Add(Me.Guna2ControlBox1)
        Me.Controls.Add(Me.Guna2GroupBox1)
        Me.Controls.Add(Me.btn_batal)
        Me.Controls.Add(Me.Guna2HtmlLabel2)
        Me.Controls.Add(Me.Guna2HtmlLabel1)
        Me.Controls.Add(Me.txt_provider)
        Me.Controls.Add(Me.txt_no_hp)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Form_Pulsa"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form_Pulsa"
        Me.Guna2GroupBox1.ResumeLayout(False)
        CType(Me.dg_pulsa, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txt_no_hp As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents txt_provider As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents Guna2HtmlLabel1 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents Guna2HtmlLabel2 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents btn_batal As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents Guna2GroupBox1 As Guna.UI2.WinForms.Guna2GroupBox
    Friend WithEvents Guna2ControlBox1 As Guna.UI2.WinForms.Guna2ControlBox
    Friend WithEvents dg_pulsa As Guna.UI2.WinForms.Guna2DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
