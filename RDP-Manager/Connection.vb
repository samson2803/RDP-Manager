''' <summary>
''' Eine gespeicherte RDP-Verbindung.
''' Das Passwort steht bewusst NICHT hier, sondern in der Windows-Anmeldeinformationsverwaltung
''' (siehe <see cref="CredentialStore"/>).
''' </summary>
Public Class Connection
    Public Const DefaultPort As Integer = 3389

    Public Property Name As String = ""
    Public Property Host As String = ""
    Public Property Port As Integer = DefaultPort
    Public Property Username As String = ""

    ''' <summary>Adresse für mstsc /v: – Host, bei abweichendem Port mit ":Port".</summary>
    Public ReadOnly Property Address As String
        Get
            If Port = DefaultPort Then Return Host
            Return Host & ":" & Port
        End Get
    End Property

    ''' <summary>Anzeigename; fällt auf den Host zurück, wenn kein Name vergeben ist.</summary>
    Public ReadOnly Property DisplayName As String
        Get
            If String.IsNullOrWhiteSpace(Name) Then Return Host
            Return Name
        End Get
    End Property
End Class
