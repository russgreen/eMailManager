Imports System.Windows.Forms
Imports Microsoft.Win32
Imports System.Diagnostics
Imports System.IO

'Imports Outlook = Microsoft.Office.Interop.Outlook

Public Class frmMain
    Private LocRM As New Resources.ResourceManager("eMailManager.ResourceStrings", Me.GetType.Assembly)

    Friend bBatchFile As Boolean = False
    Friend bSentMail As Boolean = False
    Friend sSavePathMsg As String = String.Empty
    Friend sSavePathAtt As String = String.Empty
    Friend Workflow As String = "Workflow02"

    Friend m_olMailItem As Outlook.MailItem
    Friend m_olSelection As Outlook.Selection

    Public Sub New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        Me.Text = My.Application.Info.Title & String.Format(" Version {0}", My.Application.Info.Version.ToString) 'My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.MajorRevision & "." & My.Application.Info.Version.MinorRevision
    End Sub

    Private Sub FrmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Trim(Me.txtSearch.Text) = String.Empty Then
            ReSaveMRU()
        End If

        cSettings.FormHeight = Me.Height
        cSettings.FormWidth = Me.Width
        cSettings.FormX = Me.Location.X
        cSettings.FormY = Me.Location.Y
        cSettings.SaveSettings()

        My.Computer.Registry.CurrentUser.SetValue("Software\MailManager\useFileSystem", Me.rdoFileFolder.Checked.ToString, RegistryValueKind.String)

    End Sub

    Private Sub FrmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.ToolStripStatusLabel1.Text = String.Empty
        Me.ToolStripProgressBar1.Visible = False

        'Recall saved listview items
        ' GetMRU()
        'Me.lvwMRU.Sort()

        cSettings.LoadSettings()

        Me.Width = cSettings.FormWidth
        Me.Height = cSettings.FormHeight

        If cSettings.FormX = 0 And cSettings.FormY = 0 Then
            'do not move the form
        Else

            Dim Loc As System.Drawing.Point = New System.Drawing.Point(cSettings.FormX, cSettings.FormY)

            If Screen.PrimaryScreen.WorkingArea.Contains(Loc.X + 10, Loc.Y + 10) Then
                Me.Location = Loc

            End If
        End If


        Me.chkLeaveCopy.Checked = cSettings.LeaveCopy

        cSettings.GetMRU()
        loadMRUlist()

        If cSettings.MessageSaveAsType = 3 Then
            Me.cboFileFormat.SelectedIndex = 0
        ElseIf cSettings.MessageSaveAsType = 0 Then
            Me.cboFileFormat.SelectedIndex = 1
        End If

        'populate the mail details
        If bBatchFile = False Then
            Me.lblMailFrom.Text = m_olMailItem.SenderName
            Me.lblMailTo.Text = m_olMailItem.To
            Me.lblMailSubject.Text = m_olMailItem.Subject
        Else
            Me.lblMailFrom.Text = LocRM.GetString("BatchFileMode")
            Me.lblMailTo.Text = LocRM.GetString("BatchFileMode")
            Me.lblMailSubject.Text = LocRM.GetString("BatchFileMode")
        End If

        SetWorkFlowControls()

        'set the default save folder type
        Me.rdoFileFolder.Checked = CBool(My.Computer.Registry.CurrentUser.GetValue("Software\MailManager\useFileSystem", 0))
        If Me.rdoFileFolder.Checked = False Then Me.rdoOutlookFolder.Checked = True Else Me.rdoOutlookFolder.Checked = False

        Me.btnFileMessage.Enabled = False
        Me.cmiEditDesc.Enabled = False
        Me.cmiRemoveEntry.Enabled = False

        If cSettings.RememberLastLocation = True Then
            If Me.lvwMRU.Items.Count > 0 Then
                For Each oItem As ListViewItem In Me.lvwMRU.Items
                    If oItem.SubItems(1).Text = cSettings.LastLocation Then
                        oItem.EnsureVisible()
                        oItem.Selected = True
                        oItem.Focused = True
                        'Me.btnFileMessage.Enabled = True
                    End If
                Next
                Me.lvwMRU.Select()
            End If

        End If


    End Sub

#Region " UI code "

    Private Sub SetWorkFlowControls()
        'start by enabling all workflows and defaults on controls
        Me.rdoWorkflow01.Checked = True
        Me.rdoWorkflow01.Enabled = True
        Me.rdoWorkflow01.Text = LocRM.GetString("WorkFlow01") '"File message (any attachments remain embedded)"
        Me.rdoWorkflow02.Enabled = True
        Me.rdoWorkflow02.Text = LocRM.GetString("WorkFlow02") '"File message and attachments separately (save the attachments in file system)"
        Me.rdoWorkflow03.Enabled = True
        Me.rdoWorkflow03.Text = LocRM.GetString("WorkFlow03") '"File message and attachments separately (keep a copy of attachments in message)"

        If bBatchFile = False Then
            'check if we are saving to an outlook folder or a file system folder
            If rdoOutlookFolder.Checked = True Then
                'saving to an outlook folder - disable file format
                Me.gbFileFormat.Enabled = False

                Me.chkLeaveCopy.Visible = False

            Else
                'saving to a file system folder
                Me.gbFileFormat.Enabled = True

                Me.chkLeaveCopy.Visible = True

                'check for file type
                Select Case cSettings.MessageSaveAsType
                    Case 3 'saving a MSG file
                        Me.rdoWorkflow01.Checked = True

                        Me.rdoWorkflow01.Enabled = True
                        Me.rdoWorkflow02.Enabled = True
                        Me.rdoWorkflow03.Enabled = True

                    Case 0 'saving a TXT file
                        Me.rdoWorkflow02.Checked = True

                        Me.rdoWorkflow01.Enabled = False
                        Me.rdoWorkflow02.Enabled = True
                        Me.rdoWorkflow03.Enabled = False

                End Select
            End If

            'finally handle sent items with attachments
            If bSentMail = True Then
                'file sent messages
                Select Case cSettings.MessageSaveAsType
                    Case 3 'saving a MSG file
                        If cSettings.AlwaysEmbedAttachments = True Then
                            Me.rdoWorkflow01.Checked = True
                            Me.rdoWorkflow03.Text = LocRM.GetString("WorkFlow03") '"File message and attachments separately (keep a copy of attachments in message)"
                        Else
                            If HasNonEmbeddedAttachments() = True Then
                                Me.rdoWorkflow03.Checked = True
                                Me.rdoWorkflow03.Text = LocRM.GetString("WorkFlow03a") '"File message and strip attachments from the message (removes the attachments)"
                            Else
                                Me.rdoWorkflow01.Checked = True
                                Me.rdoWorkflow03.Text = LocRM.GetString("WorkFlow03") '"File message and attachments separately (keep a copy of attachments in message)"
                            End If
                        End If

                    Case 0 'saving a TXT file
                        Me.rdoWorkflow02.Checked = True

                        Me.rdoWorkflow01.Enabled = False
                        Me.rdoWorkflow02.Enabled = True
                        Me.rdoWorkflow03.Enabled = False

                End Select


            End If

        ElseIf bBatchFile = True Then
            'check for file type
            Select Case cSettings.MessageSaveAsType
                Case 3 'saving a MSG file
                    Me.rdoWorkflow01.Checked = True
                    Me.rdoWorkflow02.Enabled = False
                    Me.rdoWorkflow03.Enabled = False

                Case 0 'saving a TXT file
                    Me.rdoWorkflow02.Checked = True
                    Me.rdoWorkflow01.Enabled = False
                    Me.rdoWorkflow03.Enabled = False

            End Select

            If rdoOutlookFolder.Checked = True Then
                Me.gbFileFormat.Enabled = False
                Me.chkLeaveCopy.Visible = False
            Else
                Me.gbFileFormat.Enabled = True
                Me.chkLeaveCopy.Visible = True
            End If

        End If

    End Sub

    Private Sub lvwMRU_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvwMRU.DoubleClick
        EditDescription()
    End Sub

    Private Sub lvwMRU_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvwMRU.SelectedIndexChanged
        If lvwMRU.SelectedItems.Count = 0 Then
            Me.btnFileMessage.Enabled = False
            Me.cmiEditDesc.Enabled = False
            Me.cmiRemoveEntry.Enabled = False
            Me.ToolStripStatusLabel1.Text = String.Empty
        Else
            Me.btnFileMessage.Enabled = True
            Me.cmiEditDesc.Enabled = True
            Me.cmiRemoveEntry.Enabled = True

            Dim selectedpath As String = Me.lvwMRU.SelectedItems.Item(0).Tag.ToString.ToLower
            Me.rdoOutlookFolder.Checked = True

            Select Case True
                Case selectedpath.StartsWith("\\personal folders")
                    'probably be an outlook folder
                    Me.rdoOutlookFolder.Checked = True
                Case selectedpath.StartsWith("\\public folders")
                    'probably be an outlook folder
                    Me.rdoOutlookFolder.Checked = True
                Case selectedpath.StartsWith("\\mailbox")
                    'probably be an outlook folder
                    Me.rdoOutlookFolder.Checked = True
                Case selectedpath.Remove(0, 1).StartsWith(":\")
                    'probably the file system
                    Me.rdoFileFolder.Checked = True
                Case My.Computer.FileSystem.DirectoryExists(selectedpath)
                    'probably the file system
                    Me.rdoFileFolder.Checked = True
                Case Else

            End Select

            sSavePathMsg = Me.lvwMRU.SelectedItems.Item(0).SubItems(1).Text
            Me.ToolStripStatusLabel1.Text = sSavePathMsg

            SetWorkFlowControls()
        End If
    End Sub

    Private Sub rdoWorkflow01_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoWorkflow01.CheckedChanged
        If rdoWorkflow01.Checked = True Then Workflow = "Workflow01"
    End Sub

    Private Sub rdoWorkflow02_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoWorkflow02.CheckedChanged
        If rdoWorkflow02.Checked = True Then Workflow = "Workflow02"
    End Sub

    Private Sub rdoWorkflow03_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoWorkflow03.CheckedChanged
        If rdoWorkflow03.Checked = True Then Workflow = "Workflow03"
    End Sub

    Private Sub rdoOutlookFolder_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoOutlookFolder.CheckedChanged
        SetWorkFlowControls()
    End Sub

    Private Sub cboFileFormat_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboFileFormat.SelectedIndexChanged
        Select Case Me.cboFileFormat.SelectedIndex
            Case 0 : cSettings.MessageSaveAsType = 3 'msg file
            Case 1 : cSettings.MessageSaveAsType = 0 'txt file

        End Select

        SetWorkFlowControls()
    End Sub

#End Region

#Region " Button / menu code "

    Private Sub cmiEditDesc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmiEditDesc.Click
        EditDescription()
    End Sub

    Private Sub cmiRemoveEntry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmiRemoveEntry.Click
        ''clear MRU
        On Error Resume Next
        'Dim oRegCurrentUser As Microsoft.Win32.RegistryKey
        Microsoft.Win32.Registry.CurrentUser.DeleteSubKey("Software\MailManager\MRU")

        Me.lvwMRU.SelectedItems(0).Remove()

        For Each item As ListViewItem In Me.lvwMRU.Items
            cSettings.SaveMRU(item.SubItems(1).Text & "|" & item.Tag.ToString & "|" & item.Text)
        Next
    End Sub

    Private Sub cmiBackupList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmiBackupList.Click
        With Me.SaveFileDialog1
            .InitialDirectory = Environment.SpecialFolder.MyComputer.ToString
            .Filter = "Registry files (*.reg)|*.reg"

            If .ShowDialog = DialogResult.OK Then

                Dim ProcessProperties As New ProcessStartInfo With {
                    .FileName = "regedit",
                    .Arguments = "/e """ & .FileName & """ HKEY_CURRENT_USER\Software\MailManager\MRU",
                    .WindowStyle = ProcessWindowStyle.Maximized
                }
                'ProcessProperties.Arguments = "/e """ & .FileName & """ HKEY_CURRENT_USER\Software\MailManager\MRU"
                'ProcessProperties.WindowStyle = ProcessWindowStyle.Maximized
                Dim myProcess As Process = Process.Start(ProcessProperties)


            End If
        End With

    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        If rdoFileFolder.Checked Then
            Dim strSelectedPath As String = Environment.SpecialFolder.MyComputer.ToString
            If Me.lvwMRU.SelectedItems.Count = 1 Then
                If My.Computer.FileSystem.DirectoryExists(strSelectedPath) = True Then
                    strSelectedPath = Me.lvwMRU.SelectedItems.Item(0).Text.ToLower
                End If
            End If

            With Me.FolderBrowserDialog1
                .ShowNewFolderButton = True
                .RootFolder = Environment.SpecialFolder.MyComputer

                If .ShowDialog = DialogResult.OK Then
                    sSavePathMsg = .SelectedPath
                    If Not sSavePathMsg.EndsWith("\") Then sSavePathMsg &= "\"
                    'Dim oListItem As New ListViewItem
                    'Dim oSubListItem As New ListViewItem.ListViewSubItem(oListItem, "")
                    'oListItem.Text = sSavePathMsg
                    'oListItem.SubItems.Add(oSubListItem)
                    'Me.lvwMRU.Items.Add(oListItem)

                    cSettings.SaveMRU(sSavePathMsg & "|" & sSavePathMsg & "|" & InputBox(LocRM.GetString("LocationDescription"), "Description", sSavePathMsg).Replace("|", ""))
                    cSettings.GetMRU()
                    loadMRUlist(sSavePathMsg)
                End If

            End With
        Else
            'On Error Resume Next
            Dim objNS As Outlook.NameSpace
            Dim objFolder As Outlook.Folder
            objNS = Globals.ThisAddIn.Application.GetNamespace("MAPI")
            objFolder = CType(objNS.PickFolder, Outlook.Folder)
            'Dim oListItem As New ListViewItem
            'Dim oSubListItem As New ListViewItem.ListViewSubItem(oListItem, objFolder.EntryID())
            'oListItem.Text = objFolder.FolderPath
            'oListItem.SubItems.Add(oSubListItem)
            'Me.lvwMRU.Items.Add(oListItem)

            Try
                cSettings.SaveMRU(objFolder.FolderPath & "|" & objFolder.EntryID() & "|" & InputBox(LocRM.GetString("LocationDescription"), "Description", objFolder.FolderPath).Replace("|", ""))
                cSettings.GetMRU()
                loadMRUlist(objFolder.FolderPath)
            Catch ex As Exception
            End Try

        End If
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ''clear MRU
        On Error Resume Next
        'Dim oRegCurrentUser As Microsoft.Win32.RegistryKey
        Microsoft.Win32.Registry.CurrentUser.DeleteSubKey("Software\MailManager\MRU")

        Me.lvwMRU.SelectedItems(0).Remove()

        For Each item As ListViewItem In Me.lvwMRU.Items
            cSettings.SaveMRU(item.SubItems(1).Text & "|" & item.Tag.ToString & "|" & item.Text)
        Next
    End Sub

    Private Sub btnFileMessage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileMessage.Click
        If bBatchFile = True Then
            BatchFile()
            Exit Sub
        End If

        Try
            If rdoFileFolder.Checked = True Then 'FILE INTO FILE SYSTEM
                Dim sMSGFilename As String = ParseFilename(cSettings.FilenameFilter, m_olMailItem, bSentMail)

                If sMSGFilename.EndsWith(cSettings.MessageFileExt) = False Then
                    sMSGFilename &= cSettings.MessageFileExt
                End If

                Dim sTempMsgFilename As String = TrimLongFileName(My.Computer.FileSystem.SpecialDirectories.Temp & "\" & sMSGFilename)
                Dim sSaveMsgFilename As String = TrimLongFileName(lvwMRU.SelectedItems(0).Tag.ToString & sMSGFilename)

                'first check the new msg filename dones't already exist
                If My.Computer.FileSystem.FileExists(sSaveMsgFilename) = True Then
                    'first check the filename length is OK to add 28 characters
                    If sSaveMsgFilename.Length > (MaxFile - 28) Then
                        'the filename is too long to add the current filed date / time
                        sSaveMsgFilename = sSaveMsgFilename.Substring(0, (MaxFile - 28)) & cSettings.MessageFileExt
                    End If

                    sSaveMsgFilename = sSaveMsgFilename.Replace(cSettings.MessageFileExt, " [filed - " & ShortYear() & ShortMonth1() & ShortDay() & "_" & DateTime.Now.ToLongTimeString.Replace(":", "") & "]" & cSettings.MessageFileExt)
                End If

                If rdoWorkflow01.Checked Then 'file message with attachments embedded
                    'm_olMailItem.SaveAs(sSaveMsgFilename, cSettings.MessageSaveAsType)
                    m_olMailItem.SaveAs(sTempMsgFilename, cSettings.MessageSaveAsType)

                    HandleSavedMSG()

                ElseIf rdoWorkflow02.Checked Then 'file message with attachments removed and saved in file system

                    If HasNonEmbeddedAttachments() = True Then

                        If MsgBox(LocRM.GetString("Message004"), MsgBoxStyle.YesNo) = DialogResult.Yes Then
                            ExtractAttachments(False)
                            If cSettings.MessageSaveAsType = 3 Then
                                RemoveAttachments()
                            End If
                        Else
                            'm_olMailItem.SaveAs(sSaveMsg & ".txt", Microsoft.Office.Interop.Outlook.OlSaveAsType.olTXT)
                            If ExtractAttachments(True) = True Then
                                If cSettings.MessageSaveAsType = 3 Then
                                    RemoveAttachments()
                                End If
                            End If
                        End If

                    End If

                    'm_olMailItem.SaveAs(sSaveMsgFilename, cSettings.MessageSaveAsType)
                    m_olMailItem.SaveAs(sTempMsgFilename, cSettings.MessageSaveAsType)

                    HandleSavedMSG()

                ElseIf rdoWorkflow03.Checked Then 'file message with attachments embedded and copy saved in file system
                    If bSentMail = True Then
                        If HasNonEmbeddedAttachments() = True Then
                            RemoveAttachments()
                        End If

                        'm_olMailItem.SaveAs(sSaveMsgFilename, cSettings.MessageSaveAsType)
                        m_olMailItem.SaveAs(sTempMsgFilename, cSettings.MessageSaveAsType)

                        HandleSavedMSG()
                    Else
                        'm_olMailItem.SaveAs(sSaveMsgFilename, cSettings.MessageSaveAsType)
                        m_olMailItem.SaveAs(sTempMsgFilename, cSettings.MessageSaveAsType)

                        If MsgBox(LocRM.GetString("Message004"), MsgBoxStyle.YesNo) = DialogResult.Yes Then
                            ExtractAttachments(False)
                        Else
                            'm_olMailItem.SaveAs(sSaveMsg & ".txt", Microsoft.Office.Interop.Outlook.OlSaveAsType.olTXT)
                            ExtractAttachments(True)
                        End If

                        HandleSavedMSG()
                    End If

                End If

                Try
                    Dim fileinfo As New FileInfo(sTempMsgFilename)
                    With fileinfo
                        .MoveTo(sSaveMsgFilename)
                        .CreationTime = m_olMailItem.SentOn
                    End With
                Catch ex As Exception
                    MsgBox(ex.Message & vbCrLf & LocRM.GetString("Message005"), MsgBoxStyle.Critical, "Error moving temp MSG file")
                    Throw New System.Exception(ex.ToString)
                End Try


            Else 'FILE TO OUTLOOK FOLDER
                If rdoWorkflow01.Checked Then
                    Dim objFolder As Outlook.Folder
                    Dim objNS As Outlook.NameSpace
                    objNS = Globals.ThisAddIn.Application.GetNamespace("MAPI")
                    'objFolder = objNS.GetFolderFromID(lvwMRU.SelectedItems(0).SubItems(1).Text)
                    objFolder = CType(objNS.GetFolderFromID(lvwMRU.SelectedItems(0).Tag.ToString), Outlook.Folder)
                    'objFolder = objNS.GetFolderFromID(lvwMRU.SelectedItems(0).Text)
                    If Not objFolder Is Nothing Then
                        m_olMailItem.Move(objFolder)
                    End If

                ElseIf rdoWorkflow02.Checked Then 'file message with attachments removed and saved in file system
                    Dim objFolder As Outlook.Folder
                    Dim objNS As Outlook.NameSpace
                    objNS = Globals.ThisAddIn.Application.GetNamespace("MAPI")
                    'objFolder = objNS.GetFolderFromID(lvwMRU.SelectedItems(0).SubItems(1).Text)
                    objFolder = CType(objNS.GetFolderFromID(lvwMRU.SelectedItems(0).Tag.ToString), Outlook.Folder)
                    'objFolder = objNS.GetFolderFromID(lvwMRU.SelectedItems(0).Text)
                    If Not objFolder Is Nothing Then
                        If HasNonEmbeddedAttachments() = True Then
                            'now remove attachments from the message.
                            If ExtractAttachments(True) = True Then
                                RemoveAttachments()
                                m_olMailItem.Move(objFolder)
                            End If
                        End If
                    End If

                ElseIf rdoWorkflow03.Checked Then 'file message with attachments embedded and copy saved in file system
                    Dim objFolder As Outlook.Folder
                    Dim objNS As Outlook.NameSpace
                    objNS = Globals.ThisAddIn.Application.GetNamespace("MAPI")
                    'objFolder = objNS.GetFolderFromID(lvwMRU.SelectedItems(0).SubItems(1).Text)
                    objFolder = CType(objNS.GetFolderFromID(lvwMRU.SelectedItems(0).Tag.ToString), Outlook.Folder)
                    'objFolder = objNS.GetFolderFromID(lvwMRU.SelectedItems(0).Text)
                    If Not objFolder Is Nothing Then
                        If bSentMail = True Then
                            If HasNonEmbeddedAttachments() = True Then
                                'now remove attachments from the message.
                                RemoveAttachments()
                            End If

                            m_olMailItem.Move(objFolder)
                        Else
                            If HasNonEmbeddedAttachments() = True Then
                                ExtractAttachments(True)
                            End If

                            m_olMailItem.Move(objFolder)
                        End If



                    End If

                End If

            End If

            If cSettings.RememberLastLocation = True Then
                cSettings.LoadSettings()
                cSettings.LastLocation = Me.ToolStripStatusLabel1.Text
                cSettings.SaveSettings()
            End If

            SaveDefaultSaveType()
            ' Me.Visible = False
            Me.Close()

        Catch ex As System.Exception
            MsgBox(ex.Message & vbCrLf & ex.Source & vbCrLf & LocRM.GetString("Message005"), MsgBoxStyle.Critical)

            Throw New System.Exception(ex.ToString)
        End Try

    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            m_olMailItem.Move(emDeletedItems)
        Catch
            m_olMailItem.Move(olDeletedItems)
        End Try

        SaveDefaultSaveType()
        'Me.Visible = False
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        'Me.Visible = False
        SaveDefaultSaveType()
        Me.Close()
    End Sub

#End Region

    Private Sub BatchFile()
        Me.ToolStripStatusLabel1.Visible = False
        Me.ToolStripProgressBar1.Visible = True

        Me.ToolStripProgressBar1.Maximum = m_olSelection.Count
        Me.ToolStripProgressBar1.Value = 0

        For Each m_olItem In m_olSelection
            m_olMailItem = CType(m_olItem, Outlook.MailItem)

            Dim strMSGFilename As String = ParseFilename(cSettings.FilenameFilter, m_olMailItem, bSentMail)

            If strMSGFilename.EndsWith(cSettings.MessageFileExt) = False Then
                strMSGFilename &= cSettings.MessageFileExt
            End If

            Dim sTempMsgFilename As String = TrimLongFileName(My.Computer.FileSystem.SpecialDirectories.Temp & "\" & strMSGFilename)
            Dim sSaveMsgFilename As String = TrimLongFileName(lvwMRU.SelectedItems(0).Tag.ToString & strMSGFilename)

            Try
                If rdoFileFolder.Checked = True Then
                    'first check the new msg filename dones't already exist
                    If My.Computer.FileSystem.FileExists(sSaveMsgFilename) = True Then
                        'first check the filename length is OK to add 28 characters
                        If sSaveMsgFilename.Length > (MaxFile - 28) Then
                            'the filename is too long to add the current filed date / time
                            sSaveMsgFilename = sSaveMsgFilename.Substring(0, (MaxFile - 28)) & cSettings.MessageFileExt
                        End If

                        sSaveMsgFilename = sSaveMsgFilename.Replace(cSettings.MessageFileExt, " [filed - " & ShortYear() & ShortMonth1() & ShortDay() & "_" & DateTime.Now.ToLongTimeString.Replace(":", "") & "]" & cSettings.MessageFileExt)

                        If My.Computer.FileSystem.FileExists(sSaveMsgFilename) = True Then
                            Delay(1)

                            'first check the filename length is OK to add 28 characters
                            If sSaveMsgFilename.Length > (MaxFile - 28) Then
                                'the filename is too long to add the current filed date / time
                                sSaveMsgFilename = sSaveMsgFilename.Substring(0, (MaxFile - 28)) & cSettings.MessageFileExt
                            End If

                            sSaveMsgFilename = sSaveMsgFilename.Replace(cSettings.MessageFileExt, " [filed - " & ShortYear() & ShortMonth1() & ShortDay() & "_" & DateTime.Now.ToLongTimeString.Replace(":", "") & "]" & cSettings.MessageFileExt)
                        End If

                    End If

                    'file into file system
                    'If rdoWorkflow01.Checked Then 'file message with attachments embedded
                    If cSettings.MessageFileExt = ".txt" Then
                        ExtractAttachments(False)
                    End If

                    'm_olMailItem.SaveAs(sSaveMsgFilename, cSettings.MessageSaveAsType)
                    m_olMailItem.SaveAs(sTempMsgFilename, cSettings.MessageSaveAsType)

                    ''now save the ADS data.
                    'Try
                    '    If cSettings.SaveADS = True Then
                    '        WriteADS(m_olMailItem, sTempMsgFilename)
                    '    End If
                    'Catch ex As System.Exception
                    '    MsgBox(ex.Message & vbCrLf & LocRM.GetString("Message005"), MsgBoxStyle.Critical, "Error setting MSG file properties")
                    '    Throw New System.Exception(ex.ToString)
                    'End Try

                    'move temp MSG file to file location
                    Try
                        Dim fileinfo As New FileInfo(sTempMsgFilename)
                        With fileinfo
                            .MoveTo(sSaveMsgFilename)
                            .CreationTime = m_olMailItem.SentOn
                        End With
                    Catch ex As Exception
                        MsgBox(ex.Message & vbCrLf & LocRM.GetString("Message005"), MsgBoxStyle.Critical, "Error moving temp MSG file")
                        Throw New System.Exception(ex.ToString)
                    End Try

                    HandleSavedMSG()


                Else 'FILE TO OUTLOOK FOLDER
                    If rdoWorkflow01.Checked Then
                        Dim objFolder As Outlook.Folder
                        Dim objNS As Outlook.NameSpace
                        objNS = Globals.ThisAddIn.Application.GetNamespace("MAPI")
                        'objFolder = objNS.GetFolderFromID(lvwMRU.SelectedItems(0).SubItems(1).Text)
                        objFolder = CType(objNS.GetFolderFromID(lvwMRU.SelectedItems(0).Tag.ToString), Outlook.Folder)
                        'objFolder = objNS.GetFolderFromID(lvwMRU.SelectedItems(0).Text)
                        If Not objFolder Is Nothing Then
                            m_olMailItem.Move(objFolder)
                        End If

                    End If

                End If

                SaveDefaultSaveType()
                ' Me.Visible = False
                Me.Close()

            Catch ex As System.Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical)
                'Me.DialogResult = DialogResult.Abort
                'Exit Sub
            End Try

            Me.ToolStripProgressBar1.Value = Me.ToolStripProgressBar1.Value + 1
            Application.DoEvents()
        Next


        Me.ToolStripStatusLabel1.Visible = True
        Me.ToolStripProgressBar1.Visible = False

    End Sub

    Private Function HasNonEmbeddedAttachments() As Boolean
        Dim retval As Boolean = False
        Dim oAttachment As Outlook.Attachment

        If m_olMailItem.Attachments.Count = 0 Then
            retval = False
        Else
            For Each oAttachment In m_olMailItem.Attachments
                If m_olMailItem.HTMLBody.ToLower.Contains("cid:" & oAttachment.FileName) = True Then
                    retval = False
                Else
                    retval = True
                    Exit For
                End If
            Next
        End If

        Return retval
    End Function


    Private Sub RemoveAttachments()
        Dim sAttachmentListFile As String = My.Computer.FileSystem.SpecialDirectories.Temp & "\REMOVED_ATTACHMENTS.txt"
        'Dim sAttachmentsArray As New ArrayList
        Dim sbAttachmentList As New StringBuilder

        'For Each oAttachment As Outlook.Attachment In m_olMailItem.Attachments
        '    sAttachmentsArray.Add(lvwMRU.SelectedItems(0).Text & oAttachment.DisplayName)
        'Next

        sbAttachmentList.Append("ATTACHMENTS REMOVED: " & vbCrLf)
        'm_olMailItem.Subject = m_olMailItem.Subject + " [ATTACHMENTS REMOVED: "

        Dim i As Integer
        For i = m_olMailItem.Attachments.Count To 1 Step -1
            If m_olMailItem.HTMLBody.ToLower.Contains("cid:" & m_olMailItem.Attachments.Item(i).FileName) = True Then
                'must be an embedded attachment so we'll leave it alone
            Else
                sbAttachmentList.Append(m_olMailItem.Attachments.Item(i).FileName.ToString & vbCrLf)
                'm_olMailItem.Subject = m_olMailItem.Subject + m_olMailItem.Attachments.Item(i).FileName.ToString + ";"
                m_olMailItem.Attachments.Remove(i)
            End If
        Next

        'm_olMailItem.Subject = m_olMailItem.Subject + " ]"

        'now save the list of removed attachments to a TXT file and attach it to the message
        My.Computer.FileSystem.WriteAllText(sAttachmentListFile, sbAttachmentList.ToString, False)

        m_olMailItem.Save()
        m_olMailItem.Attachments.Add(sAttachmentListFile, Outlook.OlAttachmentType.olByValue, , "REMOVED_ATTACHMENTS.txt")
        m_olMailItem.Save()
    End Sub

    Private Function ExtractAttachments(Optional ByVal bChooseLocation As Boolean = False) As Boolean
        Dim retval As Boolean = False
        Dim oAttachment As Outlook.Attachment

        If bChooseLocation = False Then
            retval = True
            For Each oAttachment In m_olMailItem.Attachments
                oAttachment.SaveAsFile(lvwMRU.SelectedItems(0).Text & oAttachment.DisplayName)
            Next
        Else
            With Me.FolderBrowserDialog1
                .ShowNewFolderButton = True
                .RootFolder = Environment.SpecialFolder.MyComputer
                .Description = LocRM.GetString("Message006")

                If .ShowDialog = Windows.Forms.DialogResult.OK Then
                    sSavePathAtt = .SelectedPath
                    If Not sSavePathAtt.EndsWith("\") Then sSavePathAtt &= "\"
                    retval = True
                Else
                    sSavePathAtt = My.Computer.FileSystem.SpecialDirectories.MyDocuments
                    If Not sSavePathAtt.EndsWith("\") Then sSavePathAtt &= "\"
                    retval = True
                End If

            End With

            If retval = True Then
                For Each oAttachment In m_olMailItem.Attachments
                    'MsgBox(sSavePathAtt & oAttachment.FileName)
                    oAttachment.SaveAsFile(sSavePathAtt & oAttachment.FileName)
                Next
            End If

        End If

        Return retval
    End Function

    Private Sub SaveDefaultSaveType()
        On Error Resume Next
        My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager", True).SetValue("useFileSystem", Me.rdoFileFolder.Checked.ToString)
        My.Computer.Registry.CurrentUser.Close()
    End Sub

    Private Sub HandleSavedMSG()
        If Me.chkLeaveCopy.Checked = False Then
            Try
                m_olMailItem.Move(emDeletedItems)
            Catch
                m_olMailItem.Move(olDeletedItems)
            End Try
        Else
            With m_olMailItem
                .UserProperties.Add("emfiled", Outlook.OlUserPropertyType.olYesNo)
                .UserProperties("emfiled").Value = True
                .Categories = cSettings.ArchivedAndRetainedCategory
                .Save()
            End With
        End If

    End Sub

#Region "  ListView Sorting  "
    Dim sortColumn As Integer = -1

    Private Sub ListView_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lvwMRU.ColumnClick
        ' Set the ListViewItemSorter property to a new ListViewItemComparer
        ' object.
        ' Determine whether the column is the same as the last column clicked.

        If e.Column <> sortColumn Then
            ' Set the sort column to the new column.
            sortColumn = e.Column
            ' Set the sort order to ascending by default.
            Me.lvwMRU.Sorting = SortOrder.Ascending
        Else
            ' Determine what the last sort order was and change it.
            If Me.lvwMRU.Sorting = SortOrder.Ascending Then
                Me.lvwMRU.Sorting = SortOrder.Descending
            Else
                Me.lvwMRU.Sorting = SortOrder.Ascending
            End If
        End If
        ' Call the sort method to manually sort.
        Me.lvwMRU.Sort()
        ' Set the ListViewItemSorter property to a new ListViewItemComparer
        ' object.

        Me.lvwMRU.ListViewItemSorter = New ListViewItemComparer(e.Column, Me.lvwMRU.Sorting)

    End Sub

    ' Implements the manual sorting of items by column.
    ' Implements the manual sorting of items by columns.
    Class ListViewItemComparer
        Implements System.Collections.IComparer
        Private col As Integer
        Private order As SortOrder

        Public Sub New()
            col = 0
            order = SortOrder.Ascending
        End Sub

        Public Sub New(ByVal column As Integer, ByVal order As SortOrder)
            col = column
            Me.order = order
        End Sub

        Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer _
            Implements System.Collections.IComparer.Compare
            Dim returnVal As Integer = -1
            returnVal = [String].Compare(CType(x,  _
                            ListViewItem).SubItems(col).Text, _
                            CType(y, ListViewItem).SubItems(col).Text)
            ' Determine whether the sort order is descending.
            If order = SortOrder.Descending Then
                ' Invert the value returned by String.Compare.
                returnVal *= -1
            End If

            Return returnVal
        End Function
    End Class
#End Region

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Me.lvwMRU.Items.Clear()

        For i As Integer = 0 To cSettings.paths.Count - 1
            If cSettings.paths.Item(i).ToLower.Contains(Me.txtSearch.Text.ToLower) Then
                'Dim oListItem As New ListViewItem
                'Dim oSubListItem As New ListViewItem.ListViewSubItem(oListItem, cSettings.paths.Item(i).ToString.Substring(cSettings.paths.Item(i).ToString.IndexOf("|") + 1))

                'oListItem.Text = cSettings.paths.Item(i).Substring(0, cSettings.paths.Item(i).ToString.IndexOf("|"))
                'oListItem.SubItems.Add(oSubListItem)

                'Me.lvwMRU.Items.Add(oListItem)
                Dim sDesc, sPath, sTag As String
                sPath = cSettings.paths.Item(i).Substring(0, cSettings.paths.Item(i).ToString.IndexOf("|"))
                Dim tmp As String = cSettings.paths.Item(i).ToString.Substring(cSettings.paths.Item(i).ToString.IndexOf("|") + 1)

                If tmp.Contains("|") = True Then
                    sTag = NullSafeString(tmp.ToString.Substring(0, tmp.ToString.IndexOf("|")), sPath)
                    sDesc = tmp.ToString.Substring(tmp.ToString.IndexOf("|") + 1)
                Else
                    sTag = NullSafeString(tmp, sPath)
                    sDesc = "description"
                End If

                Dim oListItem As New ListViewItem
                'Dim oSubListItem As New ListViewItem.ListViewSubItem(oListItem, cSettings.paths.Item(i).ToString.Substring(cSettings.paths.Item(i).ToString.IndexOf("|") + 1))
                Dim oSubListItem As New ListViewItem.ListViewSubItem(oListItem, sPath)

                oListItem.Tag = sTag
                oListItem.Text = sDesc 'cSettings.paths.Item(i).Substring(0, cSettings.paths.Item(i).ToString.IndexOf("|"))
                oListItem.SubItems.Add(oSubListItem)

                Me.lvwMRU.Items.Add(oListItem)
            End If
        Next

    End Sub

    Private Sub loadMRUlist(Optional ByVal SelectedValue As String = Nothing)
        Me.lvwMRU.Items.Clear()

        If cSettings.paths.Count > 0 Then
            For i As Integer = 0 To cSettings.paths.Count - 1 Step 1
                Dim sDesc, sPath, sTag As String
                sPath = cSettings.paths.Item(i).Substring(0, cSettings.paths.Item(i).ToString.IndexOf("|"))
                Dim tmp As String = cSettings.paths.Item(i).ToString.Substring(cSettings.paths.Item(i).ToString.IndexOf("|") + 1)

                If tmp.Contains("|") = True Then
                    sTag = NullSafeString(tmp.ToString.Substring(0, tmp.ToString.IndexOf("|")), sPath)
                    sDesc = tmp.ToString.Substring(tmp.ToString.IndexOf("|") + 1)
                Else
                    sTag = NullSafeString(tmp, sPath)
                    sDesc = "description"
                End If

                Dim oListItem As New ListViewItem
                'Dim oSubListItem As New ListViewItem.ListViewSubItem(oListItem, cSettings.paths.Item(i).ToString.Substring(cSettings.paths.Item(i).ToString.IndexOf("|") + 1))
                Dim oSubListItem As New ListViewItem.ListViewSubItem(oListItem, sPath)

                oListItem.Tag = sTag
                oListItem.Text = sDesc 'cSettings.paths.Item(i).Substring(0, cSettings.paths.Item(i).ToString.IndexOf("|"))
                oListItem.SubItems.Add(oSubListItem)

                Me.lvwMRU.Items.Add(oListItem)
            Next i
        End If


        If SelectedValue IsNot Nothing Then
            For Each oItem As ListViewItem In Me.lvwMRU.Items
                If oItem.SubItems(1).Text = SelectedValue Then
                    oItem.EnsureVisible()
                    oItem.Selected = True
                    oItem.Focused = True
                    Me.lvwMRU.Focus()
                End If
            Next
        End If

    End Sub

    Private Sub EditDescription()
        Dim sVal As String = InputBox(LocRM.GetString("LocationDescription"), "Description", Me.lvwMRU.SelectedItems(0).Text).Replace("|", "")

        If sVal = String.Empty Then
            'cancel the operation
        Else
            On Error Resume Next
            'Dim oRegCurrentUser As Microsoft.Win32.RegistryKey
            Microsoft.Win32.Registry.CurrentUser.DeleteSubKey("Software\MailManager\MRU")

            Me.lvwMRU.SelectedItems(0).Text = sVal

            For Each item As ListViewItem In Me.lvwMRU.Items
                cSettings.SaveMRU(item.SubItems(1).Text & "|" & item.Tag.ToString & "|" & item.Text)
            Next

            cSettings.GetMRU()
            loadMRUlist()
        End If
    End Sub

    Private Sub ReSaveMRU()
        'delete the current MRU reg values
        On Error Resume Next
        'Dim oRegCurrentUser As Microsoft.Win32.RegistryKey
        Microsoft.Win32.Registry.CurrentUser.DeleteSubKey("Software\MailManager\MRU")

        'Refresh the ListView before writing the data
        Me.lvwMRU.Refresh()

        'now save the curret list
        For i As Integer = Me.lvwMRU.Items.Count - 1 To 0 Step -1
            Dim item As ListViewItem
            item = Me.lvwMRU.Items(i)
            cSettings.SaveMRU(item.SubItems(1).Text & "|" & item.Tag.ToString & "|" & item.Text)
        Next i

    End Sub

End Class