Imports System.IO
Imports System.Xml.Serialization

''' <summary>
''' Lädt und speichert die Verbindungsliste als XML unter %APPDATA%\RDP-Manager\connections.xml.
''' </summary>
Public Module ConnectionStore
    Public ReadOnly FilePath As String = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "RDP-Manager", "connections.xml")

    Private ReadOnly Serializer As New XmlSerializer(GetType(List(Of Connection)), New XmlRootAttribute("Connections"))

    Public Function Load() As List(Of Connection)
        If Not File.Exists(FilePath) Then Return New List(Of Connection)()

        Using stream = File.OpenRead(FilePath)
            Return DirectCast(Serializer.Deserialize(stream), List(Of Connection))
        End Using
    End Function

    Public Sub Save(connections As IEnumerable(Of Connection))
        Directory.CreateDirectory(Path.GetDirectoryName(FilePath))

        ' Erst in eine temporäre Datei schreiben, damit bei einem Fehler die alte Liste erhalten bleibt.
        Dim tempPath = FilePath & ".tmp"
        Using stream = File.Create(tempPath)
            Serializer.Serialize(stream, connections.ToList())
        End Using

        If File.Exists(FilePath) Then
            File.Replace(tempPath, FilePath, Nothing)
        Else
            File.Move(tempPath, FilePath)
        End If
    End Sub

    ''' <summary>
    ''' Sichert eine nicht lesbare Datei als .bak, damit sie beim nächsten Speichern nicht überschrieben wird.
    ''' </summary>
    Public Function BackupBrokenFile() As String
        Dim backupPath = FilePath & "." & DateTime.Now.ToString("yyyyMMdd-HHmmss") & ".bak"
        File.Copy(FilePath, backupPath)
        Return backupPath
    End Function
End Module
