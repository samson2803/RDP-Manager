Imports System.ComponentModel

Public Class MainForm
    Private WithEvents _connections As New BindingList(Of Connection)

    ' Offene RDP-Sitzungen, für die das Fenster minimiert wurde, und der Zustand davor (Normal/Maximiert).
    Private _minimizedSessions As Integer
    Private _windowStateBeforeMinimize As FormWindowState = FormWindowState.Normal

    Private ReadOnly Property SelectedConnection As Connection
        Get
            If dgvConnections.CurrentRow Is Nothing Then Return Nothing
            Return TryCast(dgvConnections.CurrentRow.DataBoundItem, Connection)
        End Get
    End Property

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyTheme()
        dgvConnections.AutoGenerateColumns = False
        LoadConnections()
        dgvConnections.DataSource = _connections
        UpdateButtons()
        UpdateStatus()
    End Sub

    Private Sub ApplyTheme()
        Theme.Initialize()
        Theme.StyleGrid(dgvConnections)
        Icon = Theme.CreateWindowIcon(Theme.Glyph.Monitor, Theme.Accent)

        Dim iconSize = Theme.ScaleForDpi(Me, 16)
        For Each strip As ToolStrip In {MenuStrip1, ToolStrip1, cmsConnection}
            strip.ImageScalingSize = New Size(iconSize, iconSize)
        Next

        Dim makeIcon = Function(glyph As Char) Theme.CreateIcon(glyph, iconSize, Theme.TextPrimary)
        Dim accentIcon = Theme.CreateIcon(Theme.Glyph.Connect, iconSize, Theme.Accent)

        tsbConnect.Image = accentIcon
        tsbNew.Image = makeIcon(Theme.Glyph.Add)
        tsbEdit.Image = makeIcon(Theme.Glyph.Edit)
        tsbDelete.Image = makeIcon(Theme.Glyph.Delete)
        tsbSettings.Image = makeIcon(Theme.Glyph.Settings)

        ConnectToolStripMenuItem.Image = accentIcon
        NewToolStripMenuItem.Image = tsbNew.Image
        EditToolStripMenuItem.Image = tsbEdit.Image
        DeleteToolStripMenuItem.Image = tsbDelete.Image
        SettingsToolStripMenuItem.Image = tsbSettings.Image
        ExitToolStripMenuItem.Image = makeIcon(Theme.Glyph.Power)

        cmiConnect.Image = accentIcon
        cmiOpenExternal.Image = makeIcon(Theme.Glyph.OpenExternal)
        cmiEdit.Image = tsbEdit.Image
        cmiDelete.Image = tsbDelete.Image

        tsbFullScreen.Image = makeIcon(Theme.Glyph.FullScreen)
        tsbDisconnect.Image = makeIcon(Theme.Glyph.Disconnect)
    End Sub

#Region "Laden / Speichern"

    Private Sub LoadConnections()
        Try
            For Each conn In ConnectionStore.Load()
                _connections.Add(conn)
            Next
        Catch ex As InvalidOperationException
            ' XML ist beschädigt: sichern, damit beim nächsten Speichern nichts verloren geht.
            Dim backupPath = ConnectionStore.BackupBrokenFile()
            MessageBox.Show(Me,
                "Die Verbindungsliste konnte nicht gelesen werden:" & vbCrLf & ex.Message & vbCrLf & vbCrLf &
                "Eine Kopie wurde gesichert unter:" & vbCrLf & backupPath,
                "Fehler beim Laden", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Catch ex As Exception When TypeOf ex Is IO.IOException OrElse TypeOf ex Is UnauthorizedAccessException
            MessageBox.Show(Me, "Die Verbindungsliste konnte nicht gelesen werden:" & vbCrLf & ex.Message,
                            "Fehler beim Laden", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub SaveConnections()
        Try
            ConnectionStore.Save(_connections)
        Catch ex As Exception When TypeOf ex Is IO.IOException OrElse TypeOf ex Is UnauthorizedAccessException
            MessageBox.Show(Me, "Die Verbindungsliste konnte nicht gespeichert werden:" & vbCrLf & ex.Message,
                            "Fehler beim Speichern", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "Aktionen"

    Private Sub ConnectSelected()
        Dim conn = SelectedConnection
        If conn Is Nothing Then Return

        If My.Settings.OpenInTabs Then
            OpenInTab(conn)
        Else
            OpenExternal(conn)
        End If
    End Sub

    Private Sub OpenInTab(conn As Connection)
        Dim page As New RdpSessionPage(conn)
        AddHandler page.Closed, AddressOf SessionPage_Closed
        tabMain.TabPages.Add(page)
        tabMain.SelectedTab = page

        Try
            page.Connect()
        Catch ex As Exception When TypeOf ex Is Runtime.InteropServices.COMException OrElse
                                   TypeOf ex Is Win32Exception OrElse
                                   TypeOf ex Is AxHost.InvalidActiveXStateException
            tabMain.TabPages.Remove(page)
            page.Dispose()
            MessageBox.Show(Me, "Die eingebettete RDP-Sitzung konnte nicht gestartet werden:" & vbCrLf & ex.Message,
                            "Verbinden", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        UpdateButtons()
    End Sub

    Private Sub OpenExternal(conn As Connection)
        Dim mstsc As Process
        Try
            mstsc = Process.Start("mstsc.exe", "/v:" & conn.Address)
        Catch ex As Win32Exception
            MessageBox.Show(Me, "mstsc.exe konnte nicht gestartet werden:" & vbCrLf & ex.Message,
                            "Verbinden", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

        If Not My.Settings.MinimizeOnConnect OrElse mstsc Is Nothing Then
            mstsc?.Dispose()
            Return
        End If

        ' Beim Beenden von mstsc das Fenster wiederherstellen. SynchronizingObject sorgt dafür,
        ' dass Exited im UI-Thread ausgelöst wird.
        _minimizedSessions += 1
        mstsc.SynchronizingObject = Me
        mstsc.EnableRaisingEvents = True
        AddHandler mstsc.Exited, AddressOf Mstsc_Exited

        If WindowState <> FormWindowState.Minimized Then _windowStateBeforeMinimize = WindowState
        WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Mstsc_Exited(sender As Object, e As EventArgs)
        DirectCast(sender, Process).Dispose()
        _minimizedSessions -= 1

        ' Erst zurückholen, wenn die letzte Sitzung geschlossen wurde.
        If _minimizedSessions > 0 OrElse IsDisposed Then Return

        WindowState = _windowStateBeforeMinimize
        Activate()
    End Sub

    Private Sub AddConnection()
        Using dlg As New ConnectionDialog()
            If dlg.ShowDialog(Me) <> DialogResult.OK Then Return

            Dim conn = dlg.Result
            If dlg.Password <> "" AndAlso Not TrySavePassword(conn, dlg.Password) Then Return

            _connections.Add(conn)
            SaveConnections()
            SelectConnection(conn)
        End Using
    End Sub

    Private Sub EditSelected()
        Dim original = SelectedConnection
        If original Is Nothing Then Return

        Using dlg As New ConnectionDialog(original)
            If dlg.ShowDialog(Me) <> DialogResult.OK Then Return

            Dim updated = dlg.Result
            Dim hostChanged = Not String.Equals(original.Host, updated.Host, StringComparison.OrdinalIgnoreCase)
            Dim userChanged = original.Username <> updated.Username

            If dlg.Password <> "" Then
                If Not TrySavePassword(updated, dlg.Password) Then Return
                If hostChanged Then TryDeletePasswordIfUnused(original.Host, original)
            ElseIf hostChanged OrElse userChanged Then
                ' Das alte Passwort gehört zu altem Host/Benutzer und passt nicht mehr –
                ' mstsc fragt beim nächsten Verbinden nach.
                TryDeletePasswordIfUnused(original.Host, original)
            End If

            _connections(_connections.IndexOf(original)) = updated
            SaveConnections()
            SelectConnection(updated)
        End Using
    End Sub

    Private Sub DeleteSelected()
        Dim conn = SelectedConnection
        If conn Is Nothing Then Return

        If MessageBox.Show(Me, $"Verbindung ""{conn.DisplayName}"" wirklich löschen?", "Löschen",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return

        _connections.Remove(conn)
        TryDeletePasswordIfUnused(conn.Host, conn)
        SaveConnections()
    End Sub

    Private Function TrySavePassword(conn As Connection, password As String) As Boolean
        Try
            CredentialStore.SavePassword(conn.Host, conn.Username, password)
            Return True
        Catch ex As Win32Exception
            MessageBox.Show(Me, "Das Passwort konnte nicht gespeichert werden:" & vbCrLf & ex.Message,
                            "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Windows speichert ein Passwort pro Host. Es wird nur gelöscht, wenn keine andere Verbindung
    ''' denselben Host verwendet.
    ''' </summary>
    Private Sub TryDeletePasswordIfUnused(host As String, ignore As Connection)
        Dim stillUsed = _connections.Any(Function(c) c IsNot ignore AndAlso
                                             String.Equals(c.Host, host, StringComparison.OrdinalIgnoreCase))
        If stillUsed Then Return

        Try
            CredentialStore.DeletePassword(host)
        Catch ex As Win32Exception
            MessageBox.Show(Me, "Das gespeicherte Passwort konnte nicht entfernt werden:" & vbCrLf & ex.Message,
                            "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub SelectConnection(conn As Connection)
        Dim index = _connections.IndexOf(conn)
        If index < 0 Then Return
        dgvConnections.CurrentCell = dgvConnections.Rows(index).Cells(0)
    End Sub

    Private Sub UpdateButtons()
        Dim hasSelection = SelectedConnection IsNot Nothing
        For Each item As ToolStripItem In {tsbConnect, tsbEdit, tsbDelete,
                                          ConnectToolStripMenuItem, EditToolStripMenuItem, DeleteToolStripMenuItem,
                                          cmiConnect, cmiOpenExternal, cmiEdit, cmiDelete}
            item.Enabled = hasSelection
        Next

        Dim hasSession = ActiveSession IsNot Nothing
        tsbFullScreen.Enabled = hasSession
        tsbDisconnect.Enabled = hasSession
    End Sub

    Private Sub UpdateStatus()
        lblStatus.Text = If(_connections.Count = 1, "1 Verbindung", $"{_connections.Count} Verbindungen")
    End Sub

#End Region

#Region "Ereignisse"

    Private Sub Connect_Click(sender As Object, e As EventArgs) Handles tsbConnect.Click, ConnectToolStripMenuItem.Click, cmiConnect.Click
        ConnectSelected()
    End Sub

    Private Sub New_Click(sender As Object, e As EventArgs) Handles tsbNew.Click, NewToolStripMenuItem.Click
        AddConnection()
    End Sub

    Private Sub Edit_Click(sender As Object, e As EventArgs) Handles tsbEdit.Click, EditToolStripMenuItem.Click, cmiEdit.Click
        EditSelected()
    End Sub

    Private Sub Delete_Click(sender As Object, e As EventArgs) Handles tsbDelete.Click, DeleteToolStripMenuItem.Click, cmiDelete.Click
        DeleteSelected()
    End Sub

    Private Sub Settings_Click(sender As Object, e As EventArgs) Handles tsbSettings.Click, SettingsToolStripMenuItem.Click
        Using dlg As New SettingsForm()
            dlg.ShowDialog(Me)
        End Using
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Close()
    End Sub

    Private Sub dgvConnections_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvConnections.CellDoubleClick
        If e.RowIndex >= 0 Then ConnectSelected()
    End Sub

    Private Sub dgvConnections_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvConnections.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
                ConnectSelected()
                e.SuppressKeyPress = True
            Case Keys.Delete
                DeleteSelected()
                e.SuppressKeyPress = True
        End Select
    End Sub

    Private Sub dgvConnections_SelectionChanged(sender As Object, e As EventArgs) Handles dgvConnections.SelectionChanged
        UpdateButtons()
    End Sub

    ' Rechtsklick wählt die Zeile aus, damit sich das Kontextmenü auf sie bezieht.
    Private Sub dgvConnections_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvConnections.CellMouseDown
        If e.Button = MouseButtons.Right AndAlso e.RowIndex >= 0 Then
            dgvConnections.CurrentCell = dgvConnections.Rows(e.RowIndex).Cells(Math.Max(e.ColumnIndex, 0))
        End If
    End Sub

    ' Hinweis anzeigen, solange die Liste leer ist.
    Private Sub dgvConnections_Paint(sender As Object, e As PaintEventArgs) Handles dgvConnections.Paint
        If _connections.Count > 0 Then Return

        Dim area = dgvConnections.ClientRectangle
        area.Y += dgvConnections.ColumnHeadersHeight
        area.Height -= dgvConnections.ColumnHeadersHeight
        TextRenderer.DrawText(e.Graphics,
                              "Noch keine Verbindungen." & vbCrLf & "Über ""Neu"" eine Verbindung anlegen.",
                              dgvConnections.Font, area, Theme.TextSecondary,
                              TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter Or TextFormatFlags.WordBreak)
    End Sub

    Private Sub Connections_ListChanged(sender As Object, e As ListChangedEventArgs) Handles _connections.ListChanged
        UpdateStatus()
        dgvConnections.Invalidate()
    End Sub

#End Region

#Region "Eingebettete Sitzungen (Tabs)"

    ' True, während beim Beenden auf das Trennen der offenen Sitzungen gewartet wird.
    Private _closingAfterSessions As Boolean

    Private ReadOnly Property ActiveSession As RdpSessionPage
        Get
            Return TryCast(tabMain.SelectedTab, RdpSessionPage)
        End Get
    End Property

    Private ReadOnly Property OpenSessions As List(Of RdpSessionPage)
        Get
            Return tabMain.TabPages.OfType(Of RdpSessionPage)().ToList()
        End Get
    End Property

    Private Sub OpenExternal_Click(sender As Object, e As EventArgs) Handles cmiOpenExternal.Click
        Dim conn = SelectedConnection
        If conn IsNot Nothing Then OpenExternal(conn)
    End Sub

    Private Sub FullScreen_Click(sender As Object, e As EventArgs) Handles tsbFullScreen.Click
        ActiveSession?.EnterFullScreen()
    End Sub

    Private Sub Disconnect_Click(sender As Object, e As EventArgs) Handles tsbDisconnect.Click
        ActiveSession?.CloseSession()
    End Sub

    Private Sub tabMain_TabCloseRequested(sender As Object, page As TabPage) Handles tabMain.TabCloseRequested
        TryCast(page, RdpSessionPage)?.CloseSession()
    End Sub

    Private Sub tabMain_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tabMain.SelectedIndexChanged
        UpdateButtons()
        ActiveSession?.FocusSession()
    End Sub

    Private Sub SessionPage_Closed(sender As Object, e As EventArgs)
        UpdateButtons()
        If _closingAfterSessions AndAlso OpenSessions.Count = 0 Then BeginInvoke(New Action(AddressOf Close))
    End Sub

    ' Offene Sitzungen erst sauber trennen, dann beenden.
    Private Sub MainForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Dim sessions = OpenSessions
        If sessions.Count = 0 Then Return

        e.Cancel = True
        If _closingAfterSessions Then Return

        Dim text = If(sessions.Count = 1, "Es ist noch 1 Sitzung geöffnet.", $"Es sind noch {sessions.Count} Sitzungen geöffnet.")
        If MessageBox.Show(Me, text & " Trennen und beenden?", "Beenden",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return

        _closingAfterSessions = True
        For Each session In sessions
            session.CloseSession()
        Next
    End Sub

#End Region
End Class
