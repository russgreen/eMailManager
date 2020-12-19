Imports System.Windows.Forms
Imports Microsoft.VisualStudio.Tools.Applications.Runtime

Imports Office = Microsoft.Office.Core
Imports System.Diagnostics
Imports Microsoft.Win32
Imports System.Deployment
Imports System.Deployment.Application

Public Class ThisAddIn

    Private LocRM As New Resources.ResourceManager("eMailManager.ResourceStrings", Me.GetType.Assembly)
    'LocRM.GetString("Message001")

    ' User interface elements.
    Private m_addinToolbar As Office.CommandBar
    Private m_addinMenu As Office.CommandBarPopup

    Private m_FileItMenuButton As Office.CommandBarButton
    Private m_FolderSaveButton As Office.CommandBarButton
    Private m_AboutMenuButton As Office.CommandBarButton
    Private m_SearchMenuButton As Office.CommandBarButton
    Private m_SettingsMenuButton As Office.CommandBarButton
    Private m_ClearDeletedItems As Office.CommandBarButton

    Private m_FileItToolbarButton As Office.CommandBarButton
    Private m_FolderSaveToolbarButton As Office.CommandBarButton
    Private m_AboutToolbarButton As Office.CommandBarButton
    Private m_UpdateToolbarButton As Office.CommandBarButton
    Private m_SearchToolbarButton As Office.CommandBarButton
    Private m_SettingsToolbarButton As Office.CommandBarButton

    ' Object to filter and report Outlook events.
    Private m_eventTracker As EventTracker

    Private Sub ThisAddIn_Startup() Handles Me.Startup
        Dim application As Outlook.Application = Me.Application
        Dim inspectors As Outlook.Inspectors = application.Inspectors
        Dim activeInspector As Outlook.Inspector = application.ActiveInspector()

        If activeInspector IsNot Nothing Then
            ' MessageBox.Show("Active inspector: " & activeInspector.Caption)
        End If

        Dim explorers As Outlook.Explorers = application.Explorers
        Dim activeExplorer As Outlook.Explorer = application.ActiveExplorer()

        Do While activeExplorer Is Nothing
            'do nothing
        Loop

        If activeExplorer IsNot Nothing Then
            StartAddin()
        End If
    End Sub

    Private Sub ThisAddIn_Quit()
        Try
            ClearDeletedItems()

            RemoveMenu()

            m_FileItMenuButton = Nothing
            m_addinMenu = Nothing
            m_eventTracker = Nothing

        Catch ex As Exception
            addToLog(ex)
        End Try
    End Sub

    Private Sub ThisAddIn_Shutdown() Handles Me.Shutdown

    End Sub

    Private Sub Application_Quit() Handles Application.Quit

    End Sub

    Private Sub Create2007Menu()
        ' Get a reference to the active Outlook Explorer window.
        ' If the user has more than one Outlook window open, then for this
        ' sample the UI elements are added only to the currently active window.
        Dim exp As Outlook.Explorer = Me.Application.ActiveExplorer()

        ' Add menu items to the Tools menu.
        Dim toolsMenu As Office.CommandBar = exp.CommandBars("Tools")
        m_addinMenu = TryCast(toolsMenu.Controls.Add(Office.MsoControlType.msoControlPopup, 1, "", 1, True), Office.CommandBarPopup)
        m_addinMenu.Visible = True
        m_addinMenu.Caption = My.Application.Info.Title

        ' add the commandbarbuttons to the menu
        m_FileItMenuButton = TryCast(m_addinMenu.CommandBar.Controls.Add(Office.MsoControlType.msoControlButton, , , , True), Office.CommandBarButton)
        With m_FileItMenuButton
            '.Caption = "File Message..."
            .Caption = LocRM.GetString("MenuFileMessage")
            .Style = Microsoft.Office.Core.MsoButtonStyle.msoButtonIconAndCaption
            .FaceId = 721
            AddHandler .Click, New Office._CommandBarButtonEvents_ClickEventHandler(AddressOf m_FileItButton_Click)
        End With

        m_FolderSaveButton = TryCast(m_addinMenu.Controls.Add(Office.MsoControlType.msoControlButton, , , , True), Office.CommandBarButton)
        With m_FolderSaveButton
            '.Caption = "File Folder"
            .Caption = LocRM.GetString("MenuFileFolder")
            .Style = Microsoft.Office.Core.MsoButtonStyle.msoButtonIconAndCaption
            .Picture = getImage(My.Resources.FilingCabinetICO)
            AddHandler .Click, New Office._CommandBarButtonEvents_ClickEventHandler(AddressOf m_FolderSaveButton_Click)
        End With

        m_SearchMenuButton = TryCast(m_addinMenu.Controls.Add(Office.MsoControlType.msoControlButton, , , , True), Office.CommandBarButton)
        With m_SearchMenuButton
            '.Caption = "Search..."
            .Caption = LocRM.GetString("MenuSearch")
            .Style = Microsoft.Office.Core.MsoButtonStyle.msoButtonIconAndCaption
            .FaceId = 3981
            AddHandler .Click, New Office._CommandBarButtonEvents_ClickEventHandler(AddressOf m_SearchButton_Click)
        End With

        m_SettingsMenuButton = TryCast(m_addinMenu.Controls.Add(Office.MsoControlType.msoControlButton, , , , True), Office.CommandBarButton)
        With m_SettingsMenuButton
            '.Caption = "Settings..."
            .Caption = LocRM.GetString("MenuSettings")
            .Style = Microsoft.Office.Core.MsoButtonStyle.msoButtonCaption
            AddHandler .Click, New Office._CommandBarButtonEvents_ClickEventHandler(AddressOf m_SettingsButton_Click)
        End With

        m_ClearDeletedItems = TryCast(m_addinMenu.Controls.Add(Office.MsoControlType.msoControlButton, , , , True), Office.CommandBarButton)
        With m_ClearDeletedItems
            '.Caption = "Clear Deleted Items"
            .Caption = LocRM.GetString("MenuClearDeletedItems")
            .Style = Microsoft.Office.Core.MsoButtonStyle.msoButtonCaption
            AddHandler .Click, New Office._CommandBarButtonEvents_ClickEventHandler(AddressOf m_ClearDeletedItems_Click)
        End With

        m_AboutMenuButton = TryCast(m_addinMenu.CommandBar.Controls.Add(Office.MsoControlType.msoControlButton, , , , True), Office.CommandBarButton)
        With m_AboutMenuButton
            '.Caption = "About..."
            .Caption = LocRM.GetString("MenuAbout")
            .Style = Office.MsoButtonStyle.msoButtonIconAndCaption
            .FaceId = 984
            AddHandler .Click, New Office._CommandBarButtonEvents_ClickEventHandler(AddressOf m_AboutButton_Click)
        End With


        'create a Mail Manager toolbar 
        m_addinToolbar = exp.CommandBars.Add(My.Application.Info.Title, Office.MsoBarPosition.msoBarTop, False, True)
        m_addinToolbar.Visible = True

        ' add the commandbarbuttons to the toolbar
        m_FileItToolbarButton = TryCast(m_addinToolbar.Controls.Add(Office.MsoControlType.msoControlButton, , , , True), Office.CommandBarButton)
        With m_FileItToolbarButton
            '.Caption = "File Message..."
            .Caption = LocRM.GetString("MenuFileMessage")
            .Style = Microsoft.Office.Core.MsoButtonStyle.msoButtonIconAndCaption
            .FaceId = 721
            AddHandler .Click, New Office._CommandBarButtonEvents_ClickEventHandler(AddressOf m_FileItButton_Click)
        End With

        m_FolderSaveToolbarButton = TryCast(m_addinToolbar.Controls.Add(Office.MsoControlType.msoControlButton, , , , True), Office.CommandBarButton)
        With m_FolderSaveToolbarButton
            '.Caption = "File Folder"
            .Caption = LocRM.GetString("MenuFileFolder")
            .Style = Microsoft.Office.Core.MsoButtonStyle.msoButtonIconAndCaption
            .Picture = getImage(My.Resources.FilingCabinetICO)
            AddHandler .Click, New Office._CommandBarButtonEvents_ClickEventHandler(AddressOf m_FolderSaveButton_Click)
        End With

        m_SearchToolbarButton = TryCast(m_addinToolbar.Controls.Add(Office.MsoControlType.msoControlButton, , , , True), Office.CommandBarButton)
        With m_SearchToolbarButton
            '.Caption = "Search..."
            .Caption = LocRM.GetString("MenuSearch")
            .Style = Microsoft.Office.Core.MsoButtonStyle.msoButtonIconAndCaption
            .FaceId = 3981
            AddHandler .Click, New Office._CommandBarButtonEvents_ClickEventHandler(AddressOf m_SearchButton_Click)
        End With

        m_SettingsToolbarButton = TryCast(m_addinToolbar.Controls.Add(Office.MsoControlType.msoControlButton, , , , True), Office.CommandBarButton)
        With m_SettingsToolbarButton
            '.Caption = "Settings..."
            .Caption = LocRM.GetString("MenuSettings")
            .Style = Microsoft.Office.Core.MsoButtonStyle.msoButtonCaption
            AddHandler .Click, New Office._CommandBarButtonEvents_ClickEventHandler(AddressOf m_SettingsButton_Click)
        End With

        m_UpdateToolbarButton = TryCast(m_addinMenu.Controls.Add(Office.MsoControlType.msoControlButton, , , , True), Office.CommandBarButton)
        With m_UpdateToolbarButton
            '.Caption = "Clear Deleted Items"
            .Caption = "Update" 'LocRM.GetString("MenuUpdate")
            .Style = Microsoft.Office.Core.MsoButtonStyle.msoButtonCaption
            AddHandler .Click, New Office._CommandBarButtonEvents_ClickEventHandler(AddressOf m_UpdateButton_Click)
        End With

        m_AboutToolbarButton = TryCast(m_addinToolbar.Controls.Add(Office.MsoControlType.msoControlButton, , , , True), Office.CommandBarButton)
        With m_AboutToolbarButton
            '.Caption = "About..."
            .Caption = LocRM.GetString("MenuAbout")
            .Style = Office.MsoButtonStyle.msoButtonIcon
            .FaceId = 984
            AddHandler .Click, New Office._CommandBarButtonEvents_ClickEventHandler(AddressOf m_AboutButton_Click)
        End With

    End Sub

    Private Sub StartAddin()
        Try
            cSettings = New clsSettings
            cSettings.LoadSettings()

            'check MaxFile of system
            Try
                Dim Result As Boolean
                Dim FileNameLength As Integer = 300
                Dim TestFolder As String = My.Computer.FileSystem.SpecialDirectories.Temp & "\"

                Do
                    FileNameLength -= 1
                    Result = WriteFile(TestFolder, CreateFilename(FileNameLength))
                Loop Until Result

                MaxFile = FileNameLength + TestFolder.Length - 4
            Catch ex As Exception
                'do nothing and use the default value
            End Try

            If cSettings.MonitorSentItems = True Then m_eventTracker = New EventTracker(Me)

            ' Create our special deleted items folder
            'then we get the parent folder if the inbox
            Dim myRoot As Outlook.Folder
            myRoot = CType(olInbox.Parent, Outlook.Folder)

            'then we add a new folder.
            Try
                'try to get the new deleted items folder
                emDeletedItems = myRoot.Folders(sDeletedItems)
            Catch ex As Exception
                'if the folder does not exist, add it
                emDeletedItems = myRoot.Folders.Add(sDeletedItems)
                'emDeletedItems.Description = "Email messages in this folder have been filed to the file system. Delete these messages regularly to keep your mailbox small."
                emDeletedItems.Description = LocRM.GetString("DeletedItemsDescription")
                'Finally
                '    emDeletedItems.SetCustomIcon(getImage(My.Resources.Recycle_Bin_empty))
            End Try

            'check what outlook version is running
            Dim majorVersionString As String = Globals.ThisAddIn.Application.Version.Split(New Char() {"."c})(0)
            Dim majorVersion As Integer = Convert.ToInt32(majorVersionString)

            If majorVersion < 14 Then
                Create2007Menu()
            End If

            AddHandler(CType(Application, Outlook.ApplicationEvents_10_Event)).Quit, AddressOf ThisAddIn_Quit

            'If IsUnlicensed = False Then RecordUsage()

        Catch ex As Exception
            addToLog(ex)
        End Try
    End Sub

    Private Sub RemoveMenu()
        On Error Resume Next
        ' Delete the parent add-in menu popup item.
        m_addinMenu.Delete(True)
    End Sub

    Private Sub m_FileItButton_Click(ByVal ctrl As Office.CommandBarButton, ByRef cancelDefault As Boolean)
        Dim mOlSelectedItem As Outlook.MailItem

        ' first check if we have a mail item selected
        If Application.ActiveExplorer.Selection.Count = 0 Then
            'MessageBox.Show("Please select the item that you want to save.")
            MessageBox.Show(LocRM.GetString("Message001"))
            Exit Sub

        ElseIf Application.ActiveExplorer.Selection.Count = 1 Then
            Try
                mOlSelectedItem = CType(Application.ActiveExplorer.Selection.Item(1), Outlook.MailItem)

                Dim frm As New frmMain()
                With frm
                    .m_olMailItem = mOlSelectedItem
                    .bBatchFile = False
                        .bSentMail = False
                        .ShowDialog()
                        .Dispose()
                    End With

            Catch ex As System.Exception
                addToLog(ex)
            End Try

        ElseIf Application.ActiveExplorer.Selection.Count > 1 Then
            Try
                Dim frm As New frmMain()
                With frm
                    .m_olSelection = Application.ActiveExplorer.Selection
                    .bBatchFile = True
                    .bSentMail = False
                    .ShowDialog()
                    .Dispose()
                End With

            Catch ex As System.Exception
                addToLog(ex)
            End Try
        End If

    End Sub

    Private Sub m_FolderSaveButton_Click(ByVal ctrl As Office.CommandBarButton, ByRef cancelDefault As Boolean)

        ' check if we have a mail folder
        If Application.ActiveExplorer.CurrentFolder.DefaultItemType = Outlook.OlItemType.olMailItem Then
            CallFolderSaveDialog(Application.ActiveExplorer.CurrentFolder)
        End If
    End Sub

    Private Sub m_SearchButton_Click(ByVal ctrl As Office.CommandBarButton, ByRef cancelDefault As Boolean)
        CallSearchDialog()
    End Sub

    Private Sub m_AboutButton_Click(ByVal ctrl As Office.CommandBarButton, ByRef cancelDefault As Boolean)
        CallAboutDialog()
    End Sub

    Private Sub m_SettingsButton_Click(ByVal ctrl As Office.CommandBarButton, ByRef cancelDefault As Boolean)
        CallSettingsDialog()
    End Sub


    Private Sub m_ClearDeletedItems_Click(ByVal ctrl As Office.CommandBarButton, ByRef cancelDefault As Boolean)
        ClearDeletedItems()
    End Sub

    Private Sub m_UpdateButton_Click(ByVal ctrl As Office.CommandBarButton, ByRef cancelDefault As Boolean)
        Try
            Dim sAppPath As String = CType(My.Computer.Registry.CurrentUser.OpenSubKey("Software\archisoft\eMail Manager", True).GetValue("Path"), String)
            If sAppPath.EndsWith("\") Then
                sAppPath = sAppPath
            Else
                sAppPath &= "\"
            End If
            Process.Start(sAppPath & "updater.exe")
        Catch ex As Exception

        End Try
    End Sub

    Private Function CreateFilename(ByVal FileNameLength As Integer) As String
        Dim s As String = String.Empty
        For i As Integer = 1 To FileNameLength
            s &= "a"
        Next
        Return s
    End Function

    Private Function WriteFile(ByVal TestFolder As String, ByVal filename As String) As Boolean
        Try
            ' Create an instance of StreamWriter to write text to a file.
            Dim sw As IO.StreamWriter = New IO.StreamWriter(TestFolder & filename)
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
