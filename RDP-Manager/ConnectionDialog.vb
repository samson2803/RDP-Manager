''' <summary>
''' Dialog zum Anlegen oder Bearbeiten einer Verbindung.
''' Nach DialogResult.OK stehen die Daten in <see cref="Result"/> und <see cref="Password"/>.
''' </summary>
Public Class ConnectionDialog
    Public Property Result As Connection

    ''' <summary>Leer bedeutet: kein neues Passwort (neu: beim Verbinden fragen, bearbeiten: unverändert).</summary>
    Public ReadOnly Property Password As String
        Get
            Return txtPassword.Text
        End Get
    End Property

    Public Sub New(Optional existing As Connection = Nothing)
        InitializeComponent()
        Theme.StylePrimaryButton(btnOk)
        Theme.StyleSecondaryButton(btnCancel)

        If existing Is Nothing Then
            Text = "Neue Verbindung"
            numPort.Value = Connection.DefaultPort
            lblPasswordHint.Text = "Leer lassen = beim Verbinden fragen"
        Else
            Text = "Verbindung bearbeiten"
            txtName.Text = existing.Name
            txtHost.Text = existing.Host
            numPort.Value = existing.Port
            txtUsername.Text = existing.Username
            lblPasswordHint.Text = "Leer lassen = gespeichertes Passwort behalten"
        End If
    End Sub

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        Dim host = txtHost.Text.Trim()
        If host = "" OrElse host.Any(Function(c) Char.IsWhiteSpace(c) OrElse c = """"c OrElse c = ":"c) Then
            MessageBox.Show(Me, "Bitte einen gültigen Hostnamen oder eine IP-Adresse eingeben." & vbCrLf &
                            "Der Port wird im eigenen Feld angegeben.",
                            Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtHost.Focus()
            Return
        End If

        Result = New Connection With {
            .Name = txtName.Text.Trim(),
            .Host = host,
            .Port = CInt(numPort.Value),
            .Username = txtUsername.Text.Trim()
        }
        DialogResult = DialogResult.OK
    End Sub
End Class
