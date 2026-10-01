Public Class SettingsForm
    Private Sub SettingsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Theme.StylePrimaryButton(btnSave)
        Theme.StyleSecondaryButton(btnCancel)
        chkOpenInTabs.Checked = My.Settings.OpenInTabs
        chkPassStoredPassword.Checked = My.Settings.PassStoredPassword
        chkMinimizeOnConnect.Checked = My.Settings.MinimizeOnConnect
        UpdateEnabledOptions()
    End Sub

    ' Jede Option gilt nur für eine der beiden Verbindungsarten.
    Private Sub UpdateEnabledOptions()
        chkPassStoredPassword.Enabled = chkOpenInTabs.Checked
        chkMinimizeOnConnect.Enabled = Not chkOpenInTabs.Checked
    End Sub

    Private Sub chkOpenInTabs_CheckedChanged(sender As Object, e As EventArgs) Handles chkOpenInTabs.CheckedChanged
        UpdateEnabledOptions()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        My.Settings.OpenInTabs = chkOpenInTabs.Checked
        My.Settings.PassStoredPassword = chkPassStoredPassword.Checked
        My.Settings.MinimizeOnConnect = chkMinimizeOnConnect.Checked
        My.Settings.Save()
        DialogResult = DialogResult.OK
    End Sub
End Class
