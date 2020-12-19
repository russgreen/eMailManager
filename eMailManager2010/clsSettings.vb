'Imports Microsoft.Win32
Imports System.ComponentModel

Public Class clsSettings
    Implements INotifyPropertyChanged

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Public paths As New List(Of String)()

    Private m_FilenameFilter As String
    Private m_MessageSaveAsType As Integer
    Private m_MessageFileExt As String
    Private m_RememberLastLocation As Boolean
    Private m_LastLocation As String

    Private m_ArchivedAndRetainedCategory As String
    'Private m_SaveADS As Boolean
    Private m_LeaveCopy As Boolean
    Private m_AlwaysEmbedAttachments As Boolean
    Private m_AlwaysClearDeletedItems As Boolean
    Private m_MonitorSentItems As Boolean

    Private m_AutoArchiveIn As Boolean
    Private m_AutoArchiveOut As Boolean
    Private m_AutoArchiveInPath As String
    Private m_AutoArchiveOutPath As String

    Private m_FormWidth As Integer
    Private m_FormHeight As Integer
    Private m_FormX As Integer
    Private m_FormY As Integer

    Public Property FilenameFilter() As String
        Get
            Return m_FilenameFilter
        End Get
        Set(ByVal value As String)
            m_FilenameFilter = value
        End Set
    End Property
    'OUTLOOK SAVEAS FILE TYPES
    '0	-	olTXT	-	Text format (.txt)
    '1	-	olRTF	-	Rich Text format (.rtf)
    '2	-	olTemplate	-	Microsoft Office Outlook template (.oft)
    '3	-	olMSG	-	Outlook message format (.msg)
    '4	-	olDoc	-	Microsoft Office Word format (.doc)
    '5	-	olHTML	-	HTML format (.html)
    '6	-	olVCard	-	VCard format (.vcf)
    '7	-	olVCal	-	VCal format (.vcs)
    '8	-	olICal	-	iCal format (.ics)
    '9	-	olMSGUnicode	-	Outlook Unicode message format (.msg)
    '10	-	olMHTML	-	MIME HTML format (.mht)
    Public Property MessageSaveAsType() As Integer
        Get
            Return m_MessageSaveAsType
        End Get
        Set(ByVal value As Integer)
            m_MessageSaveAsType = value
            ' Call OnPropertyChanged whenever the property is updated
            OnPropertyChanged("MessageSaveAsType")
        End Set
    End Property
    Public Property MessageFileExt() As String
        Get
            Return m_MessageFileExt
        End Get
        Set(ByVal value As String)
            m_MessageFileExt = value
        End Set
    End Property

    Public Property RememberLastLocation As Boolean
        Get
            Return m_RememberLastLocation
        End Get
        Set(ByVal value As Boolean)
            m_RememberLastLocation = value
        End Set
    End Property
    Public Property LastLocation As String
        Get
            Return m_LastLocation
        End Get
        Set(ByVal value As String)
            m_LastLocation = value
        End Set
    End Property

    Public Property ArchivedAndRetainedCategory() As String
        Get
            Return m_ArchivedAndRetainedCategory
        End Get
        Set(ByVal value As String)
            m_ArchivedAndRetainedCategory = value
        End Set
    End Property
    'Public Property SaveADS As Boolean
    '    Get
    '        Return m_SaveADS
    '    End Get
    '    Set(ByVal value As Boolean)
    '        m_SaveADS = value
    '    End Set
    'End Property
    Public Property LeaveCopy As Boolean
        Get
            Return m_LeaveCopy
        End Get
        Set(ByVal value As Boolean)
            m_LeaveCopy = value
        End Set
    End Property
    Public Property AlwaysEmbedAttachments() As Boolean
        Get
            Return m_AlwaysEmbedAttachments
        End Get
        Set(ByVal value As Boolean)
            m_AlwaysEmbedAttachments = value
        End Set
    End Property
    Public Property AlwaysClearDeletedItems() As Boolean
        Get
            Return m_AlwaysClearDeletedItems
        End Get
        Set(ByVal value As Boolean)
            m_AlwaysClearDeletedItems = value
        End Set
    End Property
    Public Property MonitorSentItems() As Boolean
        Get
            Return m_MonitorSentItems
        End Get
        Set(ByVal value As Boolean)
            m_MonitorSentItems = value
        End Set
    End Property

    Public Property AutoArchiveIn() As Boolean
        Get
            Return m_AutoArchiveIn
        End Get
        Set(ByVal value As Boolean)
            m_AutoArchiveIn = value
        End Set
    End Property
    Public Property AutoArchiveOut() As Boolean
        Get
            Return m_AutoArchiveOut
        End Get
        Set(ByVal value As Boolean)
            m_AutoArchiveOut = value
        End Set
    End Property
    Public Property AutoArchiveInPath() As String
        Get
            Return m_AutoArchiveInPath
        End Get
        Set(ByVal value As String)
            m_AutoArchiveInPath = value
        End Set
    End Property
    Public Property AutoArchiveOutPath() As String
        Get
            Return m_AutoArchiveOutPath
        End Get
        Set(ByVal value As String)
            m_AutoArchiveOutPath = value
        End Set
    End Property

    Public Property FormWidth() As Integer
        Get
            Return m_FormWidth
        End Get
        Set(ByVal value As Integer)
            m_FormWidth = value
        End Set
    End Property
    Public Property FormHeight() As Integer
        Get
            Return m_FormHeight
        End Get
        Set(ByVal value As Integer)
            m_FormHeight = value
        End Set
    End Property
    Public Property FormX() As Integer
        Get
            Return m_FormX
        End Get
        Set(ByVal value As Integer)
            m_FormX = value
        End Set
    End Property
    Public Property FormY() As Integer
        Get
            Return m_FormY
        End Get
        Set(ByVal value As Integer)
            m_FormY = value
        End Set
    End Property

    Public Sub New()
        'use default values
        m_FilenameFilter = "<sent>_<from>_<subject>"
        'm_MainForm = 0
        m_MessageSaveAsType = 3

        m_RememberLastLocation = False
        m_LastLocation = String.Empty

        Dim sMyDocs As String
        Try
            sMyDocs = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        Catch ex As Exception
            sMyDocs = My.Computer.FileSystem.SpecialDirectories.Desktop
        End Try

        m_ArchivedAndRetainedCategory = "eMail Manager Filed"

        'm_SaveADS = True
        m_LeaveCopy = False
        m_AlwaysEmbedAttachments = False
        m_AlwaysClearDeletedItems = False
        m_MonitorSentItems = True

        m_AutoArchiveIn = False
        m_AutoArchiveOut = False
        m_AutoArchiveInPath = sMyDocs
        m_AutoArchiveOutPath = sMyDocs

        m_FormWidth = 704
        m_FormHeight = 520
        m_FormX = 0
        m_FormY = 0
    End Sub

    ' Create the OnPropertyChanged method to raise the event
    Protected Sub OnPropertyChanged(ByVal name As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(name))

        Select Case m_MessageSaveAsType
            Case 3 : m_MessageFileExt = ".msg"
            Case 0 : m_MessageFileExt = ".txt"
        End Select

    End Sub

    Public Sub LoadSettings()
        Try
            'set the registry to warn about unloading for 2010
            My.Computer.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Office\Outlook\AddIns\eMailManager", True).SetValue("RequireShutdownNotification", 1, Microsoft.Win32.RegistryValueKind.DWord)

            If My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings") IsNot Nothing Then

                With My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings")
                    m_FilenameFilter = .GetValue("FilenameFilter", m_FilenameFilter).ToString
                    'm_MainForm = CInt(.GetValue("MainForm", m_MainForm))

                    Select Case Trim(.GetValue("MessageSaveAsType", m_MessageSaveAsType).ToString)
                        Case "3" : m_MessageSaveAsType = 3
                        Case "0" : m_MessageSaveAsType = 0
                        Case "" : m_MessageSaveAsType = 3
                    End Select

                    m_RememberLastLocation = CBool(.GetValue("RememberLastLocation", m_RememberLastLocation))
                    m_LastLocation = .GetValue("LastLocation", m_LastLocation).ToString

                    m_ArchivedAndRetainedCategory = .GetValue("Form2Category", m_ArchivedAndRetainedCategory).ToString
                    m_ArchivedAndRetainedCategory = .GetValue("ArchivedAndRetainedCategory", m_ArchivedAndRetainedCategory).ToString

                    'm_SaveADS = CBool(.GetValue("SaveADS", m_SaveADS))
                    m_LeaveCopy = CBool(.GetValue("LeaveCopy", m_LeaveCopy))
                    m_AlwaysEmbedAttachments = CBool(.GetValue("AlwaysEmbedAttachments", m_AlwaysEmbedAttachments))
                    m_AlwaysClearDeletedItems = CBool(.GetValue("AlwaysClearDeletedItems", m_AlwaysClearDeletedItems))
                    m_MonitorSentItems = CBool(.GetValue("MonitorSentItems", m_MonitorSentItems))

                    m_AutoArchiveIn = CBool(.GetValue("AutoArchiveIn", m_AutoArchiveIn))
                    m_AutoArchiveOut = CBool(.GetValue("AutoArchiveOut", m_AutoArchiveOut))
                    m_AutoArchiveInPath = .GetValue("AutoArchiveInPath", m_AutoArchiveInPath).ToString
                    m_AutoArchiveOutPath = .GetValue("AutoArchiveOutPath", m_AutoArchiveOutPath).ToString

                    m_FormWidth = CInt(.GetValue("FormWidth", m_FormWidth))
                    m_FormHeight = CInt(.GetValue("FormHeight", m_FormHeight))
                    m_FormX = CInt(.GetValue("FormX", m_FormX))
                    m_FormY = CInt(.GetValue("FormY", m_FormY))
                End With

            End If
        Catch ex As Exception
            addToLog(ex)
        Finally
            My.Computer.Registry.CurrentUser.Close()
        End Try
    End Sub

    Public Sub SaveSettings()
        If My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings") Is Nothing Then
            My.Computer.Registry.CurrentUser.CreateSubKey("Software\MailManager\Settings")
        End If

        My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings", True).SetValue("FilenameFilter", m_FilenameFilter, Microsoft.Win32.RegistryValueKind.String)
        My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings", True).SetValue("MessageSaveAsType", m_MessageSaveAsType, Microsoft.Win32.RegistryValueKind.String)

        My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings", True).SetValue("RememberLastLocation", m_RememberLastLocation, Microsoft.Win32.RegistryValueKind.String)
        My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings", True).SetValue("LastLocation", m_LastLocation, Microsoft.Win32.RegistryValueKind.String)

        My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings", True).SetValue("ArchivedAndRetainedCategory", m_ArchivedAndRetainedCategory, Microsoft.Win32.RegistryValueKind.String)

        'My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings", True).SetValue("SaveADS", m_SaveADS, Microsoft.Win32.RegistryValueKind.String)
        My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings", True).SetValue("LeaveCopy", m_LeaveCopy, Microsoft.Win32.RegistryValueKind.String)
        My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings", True).SetValue("AlwaysEmbedAttachments", m_AlwaysEmbedAttachments, Microsoft.Win32.RegistryValueKind.String)
        My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings", True).SetValue("AlwaysClearDeletedItems", m_AlwaysClearDeletedItems, Microsoft.Win32.RegistryValueKind.String)
        My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings", True).SetValue("MonitorSentItems", m_MonitorSentItems, Microsoft.Win32.RegistryValueKind.String)


        My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings", True).SetValue("AutoArchiveIn", m_AutoArchiveIn, Microsoft.Win32.RegistryValueKind.String)
        My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings", True).SetValue("AutoArchiveOut", m_AutoArchiveOut, Microsoft.Win32.RegistryValueKind.String)
        My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings", True).SetValue("AutoArchiveInPath", m_AutoArchiveInPath, Microsoft.Win32.RegistryValueKind.String)
        My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings", True).SetValue("AutoArchiveOutPath", m_AutoArchiveOutPath, Microsoft.Win32.RegistryValueKind.String)

        My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings", True).SetValue("FormWidth", m_FormWidth, Microsoft.Win32.RegistryValueKind.String)
        My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings", True).SetValue("FormHeight", m_FormHeight, Microsoft.Win32.RegistryValueKind.String)
        My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings", True).SetValue("FormX", m_FormX, Microsoft.Win32.RegistryValueKind.String)
        My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\Settings", True).SetValue("FormY", m_FormY, Microsoft.Win32.RegistryValueKind.String)
    End Sub

    Public Sub GetMRU()
        On Error Resume Next
        Me.paths.Clear()

        Dim oReg As Microsoft.Win32.RegistryKey = My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\MRU")
        Dim sValueNames() As String = oReg.GetValueNames

        'Array.Sort(sValueNames)

        For Each sValueName In sValueNames
            Me.paths.Add(oReg.GetValue(sValueName).ToString)
        Next

    End Sub

    Public Sub SaveMRU(ByVal NewItem As String)
        If IsInMRU(NewItem) = True Then
            Exit Sub
        End If

        Dim oReg As Microsoft.Win32.RegistryKey = My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager", True)

        If oReg Is Nothing Then
            My.Computer.Registry.CurrentUser.CreateSubKey("Software\MailManager")
        End If
        oReg = My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\MRU", True)

        If oReg Is Nothing Then
            oReg = My.Computer.Registry.CurrentUser.CreateSubKey("Software\MailManager\MRU")
        Else
            'increment MRU list
            Dim i, j As Integer
            i = oReg.GetValueNames.Length
            If i <> 120 Then i += 1
            For j = i To 2 Step -1
                oReg.SetValue(j.ToString, oReg.GetValue((j - 1).ToString))
            Next
        End If

        oReg.SetValue((1).ToString, NewItem)
    End Sub

    Private Function IsInMRU(ByVal NewItem As String) As Boolean
        Dim retval As Boolean = False

        GetMRU()

        For i As Integer = 0 To Me.paths.Count - 1

            If Me.paths.Item(i).Substring(0, paths.Item(i).IndexOf("|")) = NewItem.Substring(0, NewItem.IndexOf("|")) Then
                retval = True
                Exit For
            End If
        Next i

        Return retval
    End Function

End Class
