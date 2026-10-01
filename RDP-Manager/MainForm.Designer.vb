<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
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
        Me.components = New System.ComponentModel.Container()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FileSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConnectionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConnectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConnectionSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.NewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsbConnect = New System.Windows.Forms.ToolStripButton()
        Me.ToolSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbNew = New System.Windows.Forms.ToolStripButton()
        Me.tsbEdit = New System.Windows.Forms.ToolStripButton()
        Me.tsbDelete = New System.Windows.Forms.ToolStripButton()
        Me.tsbSettings = New System.Windows.Forms.ToolStripButton()
        Me.SessionSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbFullScreen = New System.Windows.Forms.ToolStripButton()
        Me.tsbDisconnect = New System.Windows.Forms.ToolStripButton()
        Me.tabMain = New Global.RDP_Manager.FlatTabControl()
        Me.tabList = New System.Windows.Forms.TabPage()
        Me.dgvConnections = New System.Windows.Forms.DataGridView()
        Me.colName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAddress = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colUsername = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cmsConnection = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmiConnect = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmiOpenExternal = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.cmiEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmiDelete = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.MenuStrip1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.tabMain.SuspendLayout()
        Me.tabList.SuspendLayout()
        CType(Me.dgvConnections, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsConnection.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.ConnectionToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(6, 3, 0, 3)
        Me.MenuStrip1.Size = New System.Drawing.Size(640, 25)
        Me.MenuStrip1.TabIndex = 0
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SettingsToolStripMenuItem, Me.FileSeparator, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(46, 19)
        Me.FileToolStripMenuItem.Text = "&Datei"
        '
        'SettingsToolStripMenuItem
        '
        Me.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem"
        Me.SettingsToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.SettingsToolStripMenuItem.Text = "&Einstellungen..."
        '
        'FileSeparator
        '
        Me.FileSeparator.Name = "FileSeparator"
        Me.FileSeparator.Size = New System.Drawing.Size(177, 6)
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.ExitToolStripMenuItem.Text = "&Beenden"
        '
        'ConnectionToolStripMenuItem
        '
        Me.ConnectionToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConnectToolStripMenuItem, Me.ConnectionSeparator, Me.NewToolStripMenuItem, Me.EditToolStripMenuItem, Me.DeleteToolStripMenuItem})
        Me.ConnectionToolStripMenuItem.Name = "ConnectionToolStripMenuItem"
        Me.ConnectionToolStripMenuItem.Size = New System.Drawing.Size(80, 19)
        Me.ConnectionToolStripMenuItem.Text = "&Verbindung"
        '
        'ConnectToolStripMenuItem
        '
        Me.ConnectToolStripMenuItem.Name = "ConnectToolStripMenuItem"
        Me.ConnectToolStripMenuItem.ShortcutKeyDisplayString = "Enter"
        Me.ConnectToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.ConnectToolStripMenuItem.Text = "&Verbinden"
        '
        'ConnectionSeparator
        '
        Me.ConnectionSeparator.Name = "ConnectionSeparator"
        Me.ConnectionSeparator.Size = New System.Drawing.Size(177, 6)
        '
        'NewToolStripMenuItem
        '
        Me.NewToolStripMenuItem.Name = "NewToolStripMenuItem"
        Me.NewToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.NewToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.NewToolStripMenuItem.Text = "&Neu..."
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.EditToolStripMenuItem.Text = "&Bearbeiten..."
        '
        'DeleteToolStripMenuItem
        '
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        Me.DeleteToolStripMenuItem.ShortcutKeyDisplayString = "Entf"
        Me.DeleteToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.DeleteToolStripMenuItem.Text = "&Löschen"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbConnect, Me.ToolSeparator, Me.tsbNew, Me.tsbEdit, Me.tsbDelete, Me.SessionSeparator, Me.tsbFullScreen, Me.tsbDisconnect, Me.tsbSettings})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 25)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(8, 4, 8, 4)
        Me.ToolStrip1.Size = New System.Drawing.Size(640, 38)
        Me.ToolStrip1.TabIndex = 1
        '
        'tsbConnect
        '
        Me.tsbConnect.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!)
        Me.tsbConnect.Name = "tsbConnect"
        Me.tsbConnect.Padding = New System.Windows.Forms.Padding(6, 2, 6, 2)
        Me.tsbConnect.Size = New System.Drawing.Size(90, 27)
        Me.tsbConnect.Text = "Verbinden"
        '
        'ToolSeparator
        '
        Me.ToolSeparator.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ToolSeparator.Name = "ToolSeparator"
        Me.ToolSeparator.Size = New System.Drawing.Size(6, 30)
        '
        'tsbNew
        '
        Me.tsbNew.Name = "tsbNew"
        Me.tsbNew.Padding = New System.Windows.Forms.Padding(6, 2, 6, 2)
        Me.tsbNew.Size = New System.Drawing.Size(60, 27)
        Me.tsbNew.Text = "Neu"
        '
        'tsbEdit
        '
        Me.tsbEdit.Name = "tsbEdit"
        Me.tsbEdit.Padding = New System.Windows.Forms.Padding(6, 2, 6, 2)
        Me.tsbEdit.Size = New System.Drawing.Size(90, 27)
        Me.tsbEdit.Text = "Bearbeiten"
        '
        'tsbDelete
        '
        Me.tsbDelete.Name = "tsbDelete"
        Me.tsbDelete.Padding = New System.Windows.Forms.Padding(6, 2, 6, 2)
        Me.tsbDelete.Size = New System.Drawing.Size(80, 27)
        Me.tsbDelete.Text = "Löschen"
        '
        'tsbSettings
        '
        Me.tsbSettings.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.tsbSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbSettings.Name = "tsbSettings"
        Me.tsbSettings.Padding = New System.Windows.Forms.Padding(6, 2, 6, 2)
        Me.tsbSettings.Size = New System.Drawing.Size(32, 27)
        Me.tsbSettings.Text = "Einstellungen"
        '
        'SessionSeparator
        '
        Me.SessionSeparator.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.SessionSeparator.Name = "SessionSeparator"
        Me.SessionSeparator.Size = New System.Drawing.Size(6, 30)
        '
        'tsbFullScreen
        '
        Me.tsbFullScreen.Name = "tsbFullScreen"
        Me.tsbFullScreen.Padding = New System.Windows.Forms.Padding(6, 2, 6, 2)
        Me.tsbFullScreen.Size = New System.Drawing.Size(80, 27)
        Me.tsbFullScreen.Text = "Vollbild"
        '
        'tsbDisconnect
        '
        Me.tsbDisconnect.Name = "tsbDisconnect"
        Me.tsbDisconnect.Padding = New System.Windows.Forms.Padding(6, 2, 6, 2)
        Me.tsbDisconnect.Size = New System.Drawing.Size(80, 27)
        Me.tsbDisconnect.Text = "Trennen"
        '
        'tabMain
        '
        Me.tabMain.Controls.Add(Me.tabList)
        Me.tabMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabMain.Location = New System.Drawing.Point(0, 63)
        Me.tabMain.Name = "tabMain"
        Me.tabMain.SelectedIndex = 0
        Me.tabMain.Size = New System.Drawing.Size(640, 335)
        Me.tabMain.TabIndex = 2
        '
        'tabList
        '
        Me.tabList.BackColor = System.Drawing.Color.White
        Me.tabList.Controls.Add(Me.dgvConnections)
        Me.tabList.Location = New System.Drawing.Point(4, 32)
        Me.tabList.Name = "tabList"
        Me.tabList.Size = New System.Drawing.Size(632, 299)
        Me.tabList.TabIndex = 0
        Me.tabList.Text = "Verbindungen"
        '
        'dgvConnections
        '
        Me.dgvConnections.AllowUserToAddRows = False
        Me.dgvConnections.AllowUserToDeleteRows = False
        Me.dgvConnections.AllowUserToResizeRows = False
        Me.dgvConnections.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvConnections.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colName, Me.colAddress, Me.colUsername})
        Me.dgvConnections.ContextMenuStrip = Me.cmsConnection
        Me.dgvConnections.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvConnections.Location = New System.Drawing.Point(0, 0)
        Me.dgvConnections.MultiSelect = False
        Me.dgvConnections.Name = "dgvConnections"
        Me.dgvConnections.ReadOnly = True
        Me.dgvConnections.RowHeadersVisible = False
        Me.dgvConnections.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvConnections.Size = New System.Drawing.Size(632, 299)
        Me.dgvConnections.TabIndex = 0
        '
        'colName
        '
        Me.colName.DataPropertyName = "DisplayName"
        Me.colName.FillWeight = 120.0!
        Me.colName.HeaderText = "Name"
        Me.colName.Name = "colName"
        Me.colName.ReadOnly = True
        '
        'colAddress
        '
        Me.colAddress.DataPropertyName = "Address"
        Me.colAddress.HeaderText = "Host"
        Me.colAddress.Name = "colAddress"
        Me.colAddress.ReadOnly = True
        '
        'colUsername
        '
        Me.colUsername.DataPropertyName = "Username"
        Me.colUsername.FillWeight = 80.0!
        Me.colUsername.HeaderText = "Benutzer"
        Me.colUsername.Name = "colUsername"
        Me.colUsername.ReadOnly = True
        '
        'cmsConnection
        '
        Me.cmsConnection.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmiConnect, Me.cmiOpenExternal, Me.ContextSeparator, Me.cmiEdit, Me.cmiDelete})
        Me.cmsConnection.Name = "cmsConnection"
        Me.cmsConnection.Size = New System.Drawing.Size(150, 76)
        '
        'cmiConnect
        '
        Me.cmiConnect.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!)
        Me.cmiConnect.Name = "cmiConnect"
        Me.cmiConnect.Size = New System.Drawing.Size(149, 22)
        Me.cmiConnect.Text = "Verbinden"
        '
        'cmiOpenExternal
        '
        Me.cmiOpenExternal.Name = "cmiOpenExternal"
        Me.cmiOpenExternal.Size = New System.Drawing.Size(149, 22)
        Me.cmiOpenExternal.Text = "Extern öffnen (mstsc)"
        '
        'ContextSeparator
        '
        Me.ContextSeparator.Name = "ContextSeparator"
        Me.ContextSeparator.Size = New System.Drawing.Size(146, 6)
        '
        'cmiEdit
        '
        Me.cmiEdit.Name = "cmiEdit"
        Me.cmiEdit.Size = New System.Drawing.Size(149, 22)
        Me.cmiEdit.Text = "Bearbeiten..."
        '
        'cmiDelete
        '
        Me.cmiDelete.Name = "cmiDelete"
        Me.cmiDelete.Size = New System.Drawing.Size(149, 22)
        Me.cmiDelete.Text = "Löschen"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 398)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(8, 0, 8, 0)
        Me.StatusStrip1.Size = New System.Drawing.Size(640, 22)
        Me.StatusStrip1.SizingGrip = False
        Me.StatusStrip1.TabIndex = 3
        '
        'lblStatus
        '
        Me.lblStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(96, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(96, Byte), Integer))
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(0, 17)
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(640, 420)
        Me.Controls.Add(Me.tabMain)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MinimumSize = New System.Drawing.Size(480, 300)
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "RDP-Manager"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.tabMain.ResumeLayout(False)
        Me.tabList.ResumeLayout(False)
        CType(Me.dgvConnections, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsConnection.ResumeLayout(False)
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SettingsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FileSeparator As ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ConnectionToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ConnectToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ConnectionSeparator As ToolStripSeparator
    Friend WithEvents NewToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DeleteToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents tsbConnect As ToolStripButton
    Friend WithEvents ToolSeparator As ToolStripSeparator
    Friend WithEvents tsbNew As ToolStripButton
    Friend WithEvents tsbEdit As ToolStripButton
    Friend WithEvents tsbDelete As ToolStripButton
    Friend WithEvents tsbSettings As ToolStripButton
    Friend WithEvents SessionSeparator As ToolStripSeparator
    Friend WithEvents tsbFullScreen As ToolStripButton
    Friend WithEvents tsbDisconnect As ToolStripButton
    Friend WithEvents tabMain As FlatTabControl
    Friend WithEvents tabList As TabPage
    Friend WithEvents dgvConnections As DataGridView
    Friend WithEvents colName As DataGridViewTextBoxColumn
    Friend WithEvents colAddress As DataGridViewTextBoxColumn
    Friend WithEvents colUsername As DataGridViewTextBoxColumn
    Friend WithEvents cmsConnection As ContextMenuStrip
    Friend WithEvents cmiConnect As ToolStripMenuItem
    Friend WithEvents cmiOpenExternal As ToolStripMenuItem
    Friend WithEvents ContextSeparator As ToolStripSeparator
    Friend WithEvents cmiEdit As ToolStripMenuItem
    Friend WithEvents cmiDelete As ToolStripMenuItem
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents lblStatus As ToolStripStatusLabel
End Class
