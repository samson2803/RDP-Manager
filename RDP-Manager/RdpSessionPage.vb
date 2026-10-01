Imports System.Runtime.InteropServices
Imports AxMSTSCLib
Imports MSTSCLib

''' <summary>
''' Ein Tab mit einer eingebetteten RDP-Sitzung (Microsoft RDP Client Control aus mstscax.dll).
''' Ablauf: Seite erzeugen → in ein TabControl einfügen → <see cref="Connect"/> aufrufen.
''' </summary>
Public Class RdpSessionPage
    Inherits TabPage

    ' Gründe, bei denen der Benutzer die Sitzung selbst beendet hat – dann wird der Tab einfach geschlossen.
    Private Const DiscReasonLocal As Integer = 1
    Private Const ExtReasonDisconnectByUser As Integer = 11
    Private Const ExtReasonLogoffByUser As Integer = 12

    Public ReadOnly Property Connection As Connection

    ''' <summary>Wird ausgelöst, nachdem der Tab entfernt wurde.</summary>
    Public Event Closed As EventHandler

    Private WithEvents _rdp As AxMsRdpClient9NotSafeForScripting
    Private WithEvents _resizeTimer As New Timer With {.Interval = 400}
    Private ReadOnly _overlay As New TableLayoutPanel()
    Private ReadOnly _lblMessage As New Label()
    Private WithEvents _btnReconnect As New Button()
    Private WithEvents _btnClose As New Button()

    Private _closeRequested As Boolean
    Private _loggedIn As Boolean

    Public Sub New(connection As Connection)
        Me.Connection = connection
        Text = connection.DisplayName
        BackColor = Color.Black
        UseVisualStyleBackColor = False

        _rdp = New AxMsRdpClient9NotSafeForScripting() With {.Dock = DockStyle.Fill}
        Controls.Add(_rdp)

        BuildOverlay()
        Controls.Add(_overlay)
        _overlay.BringToFront()
    End Sub

    Public ReadOnly Property IsActive As Boolean
        Get
            Return _rdp.Connected <> 0
        End Get
    End Property

#Region "Öffentliche Aktionen"

    Public Sub Connect()
        _overlay.Visible = False
        _loggedIn = False
        _closeRequested = False
        If Not _rdp.Created Then _rdp.CreateControl()

        Dim size = ClampDesktopSize(_rdp.ClientSize)
        _rdp.Server = Connection.Host
        _rdp.UserName = Connection.Username
        _rdp.DesktopWidth = size.Width
        _rdp.DesktopHeight = size.Height
        _rdp.ColorDepth = 32
        _rdp.FullScreenTitle = Connection.DisplayName & " – RDP-Manager"

        With _rdp.AdvancedSettings9
            .RDPPort = Connection.Port
            .EnableCredSspSupport = True
            .AuthenticationLevel = 2            ' Warnen, wenn sich der Server nicht ausweisen kann
            .RedirectClipboard = True
            .EnableAutoReconnect = True
            .BandwidthDetection = True
            .NetworkConnectionType = 7          ' automatisch erkennen
            .SmartSizing = False
        End With

        Dim nonScriptable = DirectCast(_rdp.GetOcx(), IMsRdpClientNonScriptable5)
        Dim password = If(My.Settings.PassStoredPassword, CredentialStore.ReadPassword(Connection.Host), Nothing)
        If password IsNot Nothing Then
            _rdp.AdvancedSettings9.ClearTextPassword = password
        Else
            ' Kein Passwort übergeben: Windows-Anmeldedialog anzeigen.
            nonScriptable.AllowPromptingForCredentials = True
            nonScriptable.PromptForCredsOnClient = True
        End If

        _rdp.Connect()
    End Sub

    ''' <summary>Trennt die Sitzung (falls aktiv) und schließt danach den Tab.</summary>
    Public Sub CloseSession()
        _closeRequested = True
        If IsActive Then
            _rdp.Disconnect()   ' Rest erledigt OnDisconnected
        Else
            RemoveTab()
        End If
    End Sub

    Public Sub EnterFullScreen()
        If _loggedIn Then _rdp.FullScreen = True
    End Sub

    Public Sub FocusSession()
        If _rdp.Visible AndAlso Not _overlay.Visible Then _rdp.Focus()
    End Sub

#End Region

#Region "RDP-Ereignisse"

    Private Sub Rdp_OnLoginComplete(sender As Object, e As EventArgs) Handles _rdp.OnLoginComplete
        _loggedIn = True
        UpdateDisplaySize(_rdp.ClientSize)
    End Sub

    Private Sub Rdp_OnDisconnected(sender As Object, e As IMsTscAxEvents_OnDisconnectedEvent) Handles _rdp.OnDisconnected
        _loggedIn = False
        Dim extended = CInt(_rdp.ExtendedDisconnectReason)

        If _closeRequested OrElse e.discReason = DiscReasonLocal OrElse
           extended = ExtReasonDisconnectByUser OrElse extended = ExtReasonLogoffByUser Then
            RemoveTab()
            Return
        End If

        ShowMessage("Die Verbindung wurde getrennt." & vbCrLf & vbCrLf &
                    _rdp.GetErrorDescription(CUInt(e.discReason), CUInt(extended)))
    End Sub

    Private Sub Rdp_OnFatalError(sender As Object, e As IMsTscAxEvents_OnFatalErrorEvent) Handles _rdp.OnFatalError
        ShowMessage($"Schwerer Fehler im RDP-Client (Code {e.errorCode}).")
    End Sub

    Private Sub Rdp_OnEnterFullScreenMode(sender As Object, e As EventArgs) Handles _rdp.OnEnterFullScreenMode
        UpdateDisplaySize(Screen.FromControl(Me).Bounds.Size)
    End Sub

    Private Sub Rdp_OnLeaveFullScreenMode(sender As Object, e As EventArgs) Handles _rdp.OnLeaveFullScreenMode
        UpdateDisplaySize(_rdp.ClientSize)
    End Sub

#End Region

#Region "Größe anpassen"

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        ' Erst nach dem Ziehen anpassen, nicht bei jedem Pixel.
        _resizeTimer.Stop()
        _resizeTimer.Start()
    End Sub

    Private Sub ResizeTimer_Tick(sender As Object, e As EventArgs) Handles _resizeTimer.Tick
        _resizeTimer.Stop()
        If Not _rdp.FullScreen Then UpdateDisplaySize(_rdp.ClientSize)
    End Sub

    ''' <summary>Passt die Auflösung der Sitzung an. Ältere Server können das nicht – dann wird skaliert.</summary>
    Private Sub UpdateDisplaySize(pixels As Size)
        If Not _loggedIn OrElse pixels.Width = 0 OrElse pixels.Height = 0 Then Return

        Dim size = ClampDesktopSize(pixels)
        Dim scale = CUInt(Math.Max(100, Math.Round(DeviceDpi * 100 / 96.0)))
        Try
            _rdp.UpdateSessionDisplaySettings(CUInt(size.Width), CUInt(size.Height),
                                              CUInt(size.Width * 25.4 / DeviceDpi), CUInt(size.Height * 25.4 / DeviceDpi),
                                              0, scale, 100)
        Catch ex As Exception When TypeOf ex Is COMException OrElse TypeOf ex Is ArgumentException
            _rdp.AdvancedSettings9.SmartSizing = True
        End Try
    End Sub

    ''' <summary>RDP erlaubt 200–8192 Pixel; gerade Breite vermeidet Darstellungsfehler.</summary>
    Private Shared Function ClampDesktopSize(size As Size) As Size
        Dim width = Math.Min(8192, Math.Max(200, size.Width)) And Not 1
        Dim height = Math.Min(8192, Math.Max(200, size.Height))
        Return New Size(width, height)
    End Function

#End Region

#Region "Meldung bei Verbindungsabbruch"

    Private Sub BuildOverlay()
        _overlay.Dock = DockStyle.Fill
        _overlay.BackColor = Theme.Surface
        _overlay.Visible = False
        _overlay.ColumnCount = 3
        _overlay.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50))
        _overlay.ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))
        _overlay.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50))
        _overlay.RowCount = 4
        _overlay.RowStyles.Add(New RowStyle(SizeType.Percent, 50))
        _overlay.RowStyles.Add(New RowStyle(SizeType.AutoSize))
        _overlay.RowStyles.Add(New RowStyle(SizeType.AutoSize))
        _overlay.RowStyles.Add(New RowStyle(SizeType.Percent, 50))

        _lblMessage.AutoSize = True
        _lblMessage.MaximumSize = New Size(Theme.ScaleForDpi(Me, 460), 0)
        _lblMessage.ForeColor = Theme.TextPrimary
        _lblMessage.TextAlign = ContentAlignment.MiddleCenter
        _lblMessage.Anchor = AnchorStyles.None
        _lblMessage.Margin = New Padding(0, 0, 0, Theme.ScaleForDpi(Me, 16))

        Dim buttons As New FlowLayoutPanel With {.AutoSize = True, .Anchor = AnchorStyles.None, .WrapContents = False}
        For Each btn In {_btnReconnect, _btnClose}
            btn.Size = New Size(Theme.ScaleForDpi(Me, 130), Theme.ScaleForDpi(Me, 32))
            buttons.Controls.Add(btn)
        Next
        _btnReconnect.Text = "Erneut verbinden"
        _btnClose.Text = "Tab schließen"
        Theme.StylePrimaryButton(_btnReconnect)
        Theme.StyleSecondaryButton(_btnClose)

        _overlay.Controls.Add(_lblMessage, 1, 1)
        _overlay.Controls.Add(buttons, 1, 2)
    End Sub

    Private Sub ShowMessage(message As String)
        _lblMessage.Text = message
        _overlay.Visible = True
    End Sub

    Private Sub BtnReconnect_Click(sender As Object, e As EventArgs) Handles _btnReconnect.Click
        Connect()
    End Sub

    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles _btnClose.Click
        CloseSession()
    End Sub

#End Region

    Private Sub RemoveTab()
        Dim tabs = TryCast(Parent, TabControl)
        tabs?.TabPages.Remove(Me)
        RaiseEvent Closed(Me, EventArgs.Empty)
        ' Erst nach Abschluss des aktuellen RDP-Ereignisses freigeben.
        BeginInvokeDispose(tabs)
    End Sub

    Private Sub BeginInvokeDispose(owner As Control)
        If owner IsNot Nothing AndAlso owner.IsHandleCreated Then
            owner.BeginInvoke(New Action(AddressOf Dispose))
        Else
            Dispose()
        End If
    End Sub

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing Then _resizeTimer.Dispose()
        MyBase.Dispose(disposing)
    End Sub
End Class
