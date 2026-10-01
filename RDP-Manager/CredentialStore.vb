Imports System.ComponentModel
Imports System.Runtime.InteropServices

''' <summary>
''' Legt RDP-Passwörter in der Windows-Anmeldeinformationsverwaltung ab (wie "cmdkey /generic:TERMSRV/host"),
''' aber direkt über die Windows-API – das Passwort taucht dadurch nie in einer Kommandozeile auf.
''' mstsc.exe findet die Anmeldedaten über das Ziel "TERMSRV/&lt;host&gt;" automatisch.
''' </summary>
Public Module CredentialStore
    Private Const CRED_TYPE_GENERIC As Integer = 1
    Private Const CRED_PERSIST_LOCAL_MACHINE As Integer = 2
    Private Const ERROR_NOT_FOUND As Integer = 1168

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)>
    Private Structure CREDENTIAL
        Public Flags As Integer
        Public Type As Integer
        Public TargetName As String
        Public Comment As String
        Public LastWritten As ComTypes.FILETIME
        Public CredentialBlobSize As Integer
        Public CredentialBlob As IntPtr
        Public Persist As Integer
        Public AttributeCount As Integer
        Public Attributes As IntPtr
        Public TargetAlias As String
        Public UserName As String
    End Structure

    <DllImport("advapi32.dll", EntryPoint:="CredWriteW", CharSet:=CharSet.Unicode, SetLastError:=True)>
    Private Function CredWrite(ByRef credential As CREDENTIAL, flags As Integer) As Boolean
    End Function

    <DllImport("advapi32.dll", EntryPoint:="CredDeleteW", CharSet:=CharSet.Unicode, SetLastError:=True)>
    Private Function CredDelete(target As String, type As Integer, flags As Integer) As Boolean
    End Function

    Private Function TargetFor(host As String) As String
        Return "TERMSRV/" & host
    End Function

    Public Sub SavePassword(host As String, username As String, password As String)
        Dim blob = Marshal.StringToCoTaskMemUni(password)
        Try
            Dim cred As New CREDENTIAL With {
                .Type = CRED_TYPE_GENERIC,
                .TargetName = TargetFor(host),
                .UserName = username,
                .CredentialBlob = blob,
                .CredentialBlobSize = password.Length * 2,
                .Persist = CRED_PERSIST_LOCAL_MACHINE
            }
            If Not CredWrite(cred, 0) Then Throw New Win32Exception(Marshal.GetLastWin32Error())
        Finally
            Marshal.ZeroFreeCoTaskMemUnicode(blob)
        End Try
    End Sub

    ''' <summary>Entfernt das gespeicherte Passwort. Ist keins vorhanden, passiert nichts.</summary>
    Public Sub DeletePassword(host As String)
        If CredDelete(TargetFor(host), CRED_TYPE_GENERIC, 0) Then Return

        Dim errorCode = Marshal.GetLastWin32Error()
        If errorCode <> ERROR_NOT_FOUND Then Throw New Win32Exception(errorCode)
    End Sub
End Module
