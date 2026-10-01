<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ConnectionDialog
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.tlpFields = New System.Windows.Forms.TableLayoutPanel()
        Me.lblName = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.lblHost = New System.Windows.Forms.Label()
        Me.txtHost = New System.Windows.Forms.TextBox()
        Me.lblPort = New System.Windows.Forms.Label()
        Me.numPort = New System.Windows.Forms.NumericUpDown()
        Me.lblUsername = New System.Windows.Forms.Label()
        Me.txtUsername = New System.Windows.Forms.TextBox()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.lblPasswordHint = New System.Windows.Forms.Label()
        Me.pnlButtons = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.tlpFields.SuspendLayout()
        CType(Me.numPort, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'tlpFields
        '
        Me.tlpFields.ColumnCount = 2
        Me.tlpFields.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110.0!))
        Me.tlpFields.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpFields.Controls.Add(Me.lblName, 0, 0)
        Me.tlpFields.Controls.Add(Me.txtName, 1, 0)
        Me.tlpFields.Controls.Add(Me.lblHost, 0, 1)
        Me.tlpFields.Controls.Add(Me.txtHost, 1, 1)
        Me.tlpFields.Controls.Add(Me.lblPort, 0, 2)
        Me.tlpFields.Controls.Add(Me.numPort, 1, 2)
        Me.tlpFields.Controls.Add(Me.lblUsername, 0, 3)
        Me.tlpFields.Controls.Add(Me.txtUsername, 1, 3)
        Me.tlpFields.Controls.Add(Me.lblPassword, 0, 4)
        Me.tlpFields.Controls.Add(Me.txtPassword, 1, 4)
        Me.tlpFields.Controls.Add(Me.lblPasswordHint, 1, 5)
        Me.tlpFields.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpFields.Location = New System.Drawing.Point(0, 0)
        Me.tlpFields.Name = "tlpFields"
        Me.tlpFields.Padding = New System.Windows.Forms.Padding(16, 16, 16, 8)
        Me.tlpFields.RowCount = 7
        Me.tlpFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlpFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlpFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlpFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlpFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlpFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlpFields.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpFields.Size = New System.Drawing.Size(400, 226)
        Me.tlpFields.TabIndex = 0
        '
        'lblName
        '
        Me.lblName.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblName.AutoSize = True
        Me.lblName.Name = "lblName"
        Me.lblName.TabIndex = 0
        Me.lblName.Text = "&Name"
        '
        'txtName
        '
        Me.txtName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtName.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.txtName.Name = "txtName"
        Me.txtName.TabIndex = 1
        '
        'lblHost
        '
        Me.lblHost.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblHost.AutoSize = True
        Me.lblHost.Name = "lblHost"
        Me.lblHost.TabIndex = 2
        Me.lblHost.Text = "&PC-Name / IP"
        '
        'txtHost
        '
        Me.txtHost.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtHost.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.txtHost.Name = "txtHost"
        Me.txtHost.TabIndex = 3
        '
        'lblPort
        '
        Me.lblPort.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblPort.AutoSize = True
        Me.lblPort.Name = "lblPort"
        Me.lblPort.TabIndex = 4
        Me.lblPort.Text = "P&ort"
        '
        'numPort
        '
        Me.numPort.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.numPort.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.numPort.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.numPort.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numPort.Name = "numPort"
        Me.numPort.Size = New System.Drawing.Size(90, 23)
        Me.numPort.TabIndex = 5
        Me.numPort.Value = New Decimal(New Integer() {3389, 0, 0, 0})
        '
        'lblUsername
        '
        Me.lblUsername.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblUsername.AutoSize = True
        Me.lblUsername.Name = "lblUsername"
        Me.lblUsername.TabIndex = 6
        Me.lblUsername.Text = "&Benutzername"
        '
        'txtUsername
        '
        Me.txtUsername.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtUsername.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.TabIndex = 7
        '
        'lblPassword
        '
        Me.lblPassword.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.TabIndex = 8
        Me.lblPassword.Text = "Pass&wort"
        '
        'txtPassword
        '
        Me.txtPassword.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtPassword.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.TabIndex = 9
        Me.txtPassword.UseSystemPasswordChar = True
        '
        'lblPasswordHint
        '
        Me.lblPasswordHint.AutoSize = True
        Me.lblPasswordHint.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblPasswordHint.ForeColor = System.Drawing.Color.FromArgb(CType(CType(96, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(96, Byte), Integer))
        Me.lblPasswordHint.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.lblPasswordHint.Name = "lblPasswordHint"
        Me.lblPasswordHint.TabIndex = 10
        Me.lblPasswordHint.Text = "Hinweis"
        '
        'pnlButtons
        '
        Me.pnlButtons.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.pnlButtons.Controls.Add(Me.btnCancel)
        Me.pnlButtons.Controls.Add(Me.btnOk)
        Me.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft
        Me.pnlButtons.Location = New System.Drawing.Point(0, 226)
        Me.pnlButtons.Name = "pnlButtons"
        Me.pnlButtons.Padding = New System.Windows.Forms.Padding(13, 12, 13, 12)
        Me.pnlButtons.Size = New System.Drawing.Size(400, 58)
        Me.pnlButtons.TabIndex = 1
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(100, 32)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Abbrechen"
        '
        'btnOk
        '
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(100, 32)
        Me.btnOk.TabIndex = 0
        Me.btnOk.Text = "Speichern"
        '
        'ConnectionDialog
        '
        Me.AcceptButton = Me.btnOk
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(400, 284)
        Me.Controls.Add(Me.tlpFields)
        Me.Controls.Add(Me.pnlButtons)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ConnectionDialog"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Verbindung"
        Me.tlpFields.ResumeLayout(False)
        Me.tlpFields.PerformLayout()
        CType(Me.numPort, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlButtons.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tlpFields As TableLayoutPanel
    Friend WithEvents lblName As Label
    Friend WithEvents txtName As TextBox
    Friend WithEvents lblHost As Label
    Friend WithEvents txtHost As TextBox
    Friend WithEvents lblPort As Label
    Friend WithEvents numPort As NumericUpDown
    Friend WithEvents lblUsername As Label
    Friend WithEvents txtUsername As TextBox
    Friend WithEvents lblPassword As Label
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents lblPasswordHint As Label
    Friend WithEvents pnlButtons As FlowLayoutPanel
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnOk As Button
End Class
