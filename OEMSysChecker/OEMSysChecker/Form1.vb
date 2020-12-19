Imports System.Net.Mail
Imports Microsoft.Win32
Imports System.IO

Public Class Form1

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If Environment.GetEnvironmentVariable("VSTO_SUPRESSDISPLAYALERTS", EnvironmentVariableTarget.User) = "0" Then
            Me.Button2.Text = "Disable Debug Mode"
        Else
            Me.Button2.Text = "Enable Debug Mode"
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Button1.Enabled = False
        Dim result As String = SysCheck()
        Me.TextBox1.Text = result
        Me.Button3.Enabled = True
    End Sub

    Private Function SysCheck() As String
        Dim sb As New System.Text.StringBuilder

        'check user account
        sb.Append(System.Environment.NewLine)
        sb.Append("User Name: " & My.User.Name & System.Environment.NewLine)
        sb.Append("IsAuthenticated: " & My.User.IsAuthenticated & System.Environment.NewLine)
        sb.Append("VSTO_SUPRESSDISPLAYALERTS: " & Environment.GetEnvironmentVariable("VSTO_SUPRESSDISPLAYALERTS", EnvironmentVariableTarget.User) & System.Environment.NewLine)

        Dim ur As String = "Guest"
        If My.User.IsInRole(ApplicationServices.BuiltInRole.User) Then ur = "User"
        If My.User.IsInRole(ApplicationServices.BuiltInRole.PowerUser) Then ur = "Power User"
        If My.User.IsInRole(ApplicationServices.BuiltInRole.Administrator) Then ur = "Administrator"

        sb.Append("User Role: " & ur & System.Environment.NewLine)

        'check office version
        sb.Append(System.Environment.NewLine)
        sb.Append("Outlook Version: " & GetOutlookVersion() & System.Environment.NewLine)

        'check outlook addins
        sb.Append(System.Environment.NewLine)

        'check special directories
        sb.Append(System.Environment.NewLine)
        Dim sMyDocs As String
        Try
            sMyDocs = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        Catch ex As Exception
            sMyDocs = My.Computer.FileSystem.SpecialDirectories.Desktop
        End Try

        'check the license entry
        If My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager") IsNot Nothing Then
            sb.Append("UserID: " & My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager").GetValue("UserID") & System.Environment.NewLine)
            sb.Append("Key: " & My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager").GetValue("Key") & System.Environment.NewLine)
            sb.Append("Qty: " & My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager").GetValue("Qty") & System.Environment.NewLine)
        End If


        sb.Append(System.Environment.NewLine)

        sb.Append("My Documents: " & sMyDocs & System.Environment.NewLine)
        sb.Append("Exists: " & My.Computer.FileSystem.DirectoryExists(sMyDocs) & System.Environment.NewLine)
        sb.Append("My Desktop: " & sMyDocs & System.Environment.NewLine)
        sb.Append("Exists: " & My.Computer.FileSystem.DirectoryExists(sMyDocs) & System.Environment.NewLine)

        'check email manager resistry settings
        sb.Append(System.Environment.NewLine)
        If My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings") IsNot Nothing Then
            sb.Append("FilenameFilter: " & My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings").GetValue("FilenameFilter") & System.Environment.NewLine)
            sb.Append("MainForm: " & My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings").GetValue("MainForm") & System.Environment.NewLine)

            Select Case My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings").GetValue("MessageSaveAsType")
                Case "3" : sb.Append("MessageFileExt: .msg" & System.Environment.NewLine)
                Case "0" : sb.Append("MessageFileExt: .txt" & System.Environment.NewLine)
            End Select

            sb.Append("RememberLastLocation: " & My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings").GetValue("RememberLastLocation") & System.Environment.NewLine)
            sb.Append("LastLocation: " & My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings").GetValue("LastLocation") & System.Environment.NewLine)

            sb.Append("Form2SavePath: " & My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings").GetValue("Form2SavePath") & System.Environment.NewLine)
            sb.Append("Form2BrowseQuote: " & My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings").GetValue("Form2BrowseQuote") & System.Environment.NewLine)
            sb.Append("Form2Category: " & My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings").GetValue("Form2Category") & System.Environment.NewLine)
            sb.Append("ArchivedAndRetainedCategory: " & My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings").GetValue("ArchivedAndRetainedCategory") & System.Environment.NewLine)

            sb.Append("SaveADS: " & My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings").GetValue("SaveADS") & System.Environment.NewLine)
            sb.Append("LeaveCopy: " & My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings").GetValue("LeaveCopy") & System.Environment.NewLine)
            sb.Append("AlwaysEmbedAttachments: " & My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings").GetValue("AlwaysEmbedAttachments") & System.Environment.NewLine)
            sb.Append("AlwaysClearDeletedItems: " & My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings").GetValue("AlwaysClearDeletedItems") & System.Environment.NewLine)
            sb.Append("MonitorSentItems:" & My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings").GetValue("MonitorSentItems") & System.Environment.NewLine)

            sb.Append("AutoArchiveIn: " & My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings").GetValue("AutoArchiveIn") & System.Environment.NewLine)
            sb.Append("AutoArchiveOut: " & My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings").GetValue("AutoArchiveOut") & System.Environment.NewLine)
            sb.Append("AutoArchiveInPath: " & My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings").GetValue("AutoArchiveInPath") & System.Environment.NewLine)
            sb.Append("AutoArchiveOutPath: " & My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings").GetValue("AutoArchiveOutPath") & System.Environment.NewLine)

            sb.Append("FormWidth: " & My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings").GetValue("FormWidth") & System.Environment.NewLine)
            sb.Append("FormHeight: " & My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings").GetValue("FormHeight") & System.Environment.NewLine)
            sb.Append("FormX: " & My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings").GetValue("FormX") & System.Environment.NewLine)
            sb.Append("FormY: " & My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings").GetValue("FormY") & System.Environment.NewLine)

        Else
            sb.Append("ERROR: Email Manager Settings are not found in the registry. Please configure settings in the application and OK the settings dialog." & System.Environment.NewLine)
        End If

        'check the registry can be written to.
        sb.Append(System.Environment.NewLine)
        If My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings") IsNot Nothing Then
            Try
                My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings", True).SetValue("TestValue", "This is a test entry", Microsoft.Win32.RegistryValueKind.String)
                sb.Append("Test entry created in registry" & System.Environment.NewLine)
            Catch ex As Exception
                sb.Append("ERROR: Test entry could not be created" & System.Environment.NewLine)
            End Try

        End If

        Try
            'check folder permissions
            sb.Append(System.Environment.NewLine)
            sb.Append("Drive Free Space: " & (My.Computer.FileSystem.GetDriveInfo(My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings").GetValue("Form2SavePath")).TotalFreeSpace / 1024) / 1024 & " MB" & System.Environment.NewLine)
        Catch ex As Exception
        End Try

        Try
            Dim folder As String
            If My.Computer.FileSystem.DirectoryExists(My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings").GetValue("LastLocation")) = True Then
                folder = My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings").GetValue("LastLocation")
            Else
                folder = My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings").GetValue("Form2SavePath")
            End If

            If folder.EndsWith("\") Then

            Else
                folder = folder & "\"
            End If

            My.Computer.FileSystem.WriteAllText(folder & "Test.txt", String.Empty, False)
            sb.Append("Write Permission : Can write test file to folder : " & folder & System.Environment.NewLine)
            My.Computer.FileSystem.DeleteFile(folder & "Test.txt")

        Catch ex As Exception
            sb.Append("Write Permission : Cannot write test file to folder" & System.Environment.NewLine)
        End Try

        'check max filename limit
        Dim Result As Boolean
        Dim FileNameLength As Integer = 500
        Dim TestFolder As String = My.Computer.FileSystem.SpecialDirectories.Temp

        Do
            FileNameLength -= 1
            Result = WriteFile(TestFolder, CreateFilename(FileNameLength))
            Application.DoEvents()
        Loop Until Result

        sb.Append("Max filename length: " & FileNameLength + TestFolder.Length - 4 & System.Environment.NewLine)

        'check cumputer information
        sb.Append(System.Environment.NewLine)
        sb.Append("Computer Name: " & My.Computer.Name & System.Environment.NewLine)
        sb.Append("OS Full Name: " & My.Computer.Info.OSFullName & System.Environment.NewLine)
        sb.Append("OS Platform: " & My.Computer.Info.OSPlatform & System.Environment.NewLine)
        sb.Append("OS Version: " & My.Computer.Info.OSVersion & System.Environment.NewLine)
        sb.Append("Physical memory: " & (My.Computer.Info.TotalPhysicalMemory / 1024) / 1024 & System.Environment.NewLine)
        sb.Append("Virtual memory: " & (My.Computer.Info.TotalVirtualMemory / 1024) / 1024 & System.Environment.NewLine)
        sb.Append("Available physical memory: " & (My.Computer.Info.AvailablePhysicalMemory / 1024) / 1024 & System.Environment.NewLine)
        sb.Append("Available virtual memory: " & (My.Computer.Info.AvailableVirtualMemory / 1024) / 1024 & System.Environment.NewLine)

        'check processes
        sb.Append(System.Environment.NewLine)
        Dim processList() As Process
        processList = Process.GetProcesses
        For Each proc In processList
            sb.Append(proc.ProcessName & " PageMem:" & proc.PagedMemorySize / 1024 & "kb SysMem:" & proc.PagedSystemMemorySize / 1024 & "kb" & System.Environment.NewLine)
        Next

        Return sb.ToString
    End Function

    Private Function GetOutlookVersion()
        GetOutlookVersion = 0    ' Get path to outlook from registry    
        Dim sOutlookPath As String = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\OUTLOOK.EXE", "Path", Nothing)
        If Not sOutlookPath Is Nothing Then
            Dim sOutlookVersion As FileVersionInfo = FileVersionInfo.GetVersionInfo(sOutlookPath & "\outlook.exe")
            GetOutlookVersion = sOutlookVersion.FileVersion
        End If
    End Function

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click

        If Me.Button2.Text = "Disable Debug Mode" Then
            Environment.SetEnvironmentVariable("VSTO_SUPRESSDISPLAYALERTS", "1", EnvironmentVariableTarget.User)
            Me.Button2.Text = "Enable Debug Mode"
        ElseIf Me.Button2.Text = "Enable Debug Mode" Then
            Environment.SetEnvironmentVariable("VSTO_SUPRESSDISPLAYALERTS", "0", EnvironmentVariableTarget.User)
            Me.Button2.Text = "Disable Debug Mode"
        End If

    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        Process.Start("mailto:support@archisoft.co.uk?Subject=eMailManagerSysCheck&Body=" & Me.TextBox1.Text.Replace(Environment.NewLine, "%0A"))
    End Sub

    Private Function CreateFilename(ByVal FileNameLength As Integer) As String
        Dim s As String
        For i As Integer = 1 To FileNameLength
            s &= "a"
        Next
        Return s
    End Function

    Public Shared Function WriteFile(ByVal TestFolder As String, ByVal filename As String) As Boolean
        Try
            ' Create an instance of StreamWriter to write text to a file.
            Dim sw As StreamWriter = New StreamWriter(TestFolder & filename)
            ' Add some text to the file.
            sw.Write("This is the ")
            sw.WriteLine("header for the file.")
            sw.WriteLine("-------------------")
            ' Arbitrary objects can also be written to the file.
            sw.Write("The date is: ")
            sw.WriteLine(DateTime.Now)
            sw.Close()
            Return True
        Catch
            Return False
        End Try
    End Function
End Class

