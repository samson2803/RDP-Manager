Public Class SettingsForm
    Private Sub SettingsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Theme.StylePrimaryButton(btnSave)
        Theme.StyleSecondaryButton(btnCancel)
        chkMinimizeOnConnect.Checked = My.Settings.MinimizeOnConnect
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        My.Settings.MinimizeOnConnect = chkMinimizeOnConnect.Checked
        My.Settings.Save()
        DialogResult = DialogResult.OK
    End Sub
End Class
