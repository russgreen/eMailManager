Imports System.Windows.Forms
Imports System.Threading

Public Class frmFolderSave

    Private LocRM As New Resources.ResourceManager("eMailManager.ResourceStrings", Me.GetType.Assembly)

    Friend m_olFolder As Outlook.Folder

    Dim bFiled As Boolean = False
    Dim bSentMail As Boolean = False
    Dim m_olUserProp As Outlook.UserProperty
    Dim m_olSubFolder As Outlook.Folder
    Dim sParentFolderPath As String  'stored to ease stripping sub-folder paths
    Dim sThisFolderPath As String
    Dim bStarted As Boolean = False

    Private Sub frmFolderSave_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.btnFileMessage.Enabled = False

        Me.Text = LocRM.GetString("Message003") & m_olFolder.Name
        sParentFolderPath = m_olFolder.FolderPath

        cSettings.LoadSettings()

        If cSettings.MessageSaveAsType = 3 Then
            Me.cboFileFormat.SelectedIndex = 0
        ElseIf cSettings.MessageSaveAsType = 0 Then
            Me.cboFileFormat.SelectedIndex = 1
        End If

        'Me.txtFilenameFilter.Text = cSettings.FilenameFilter
        Me.FileNameFilterBox1.FileNameFilter = cSettings.FilenameFilter
        Me.FileNameFilterBox1.DefaultFileNameFilter = cSettings.FilenameFilter

    End Sub

    Private Sub cboFileFormat_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboFileFormat.SelectedIndexChanged
        Select Case Me.cboFileFormat.SelectedIndex
            Case 0 : cSettings.MessageSaveAsType = 3 'msg file
            Case 1 : cSettings.MessageSaveAsType = 0 'txt file

        End Select

    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        'Dim strSelectedPath As String = Environment.SpecialFolder.MyComputer.ToString

        With Me.FolderBrowserDialog1
            .ShowNewFolderButton = True
            .RootFolder = Environment.SpecialFolder.MyComputer

            If .ShowDialog = DialogResult.OK Then
                Me.txtLocation.Text = .SelectedPath
                If Not Me.txtLocation.Text.EndsWith("\") Then Me.txtLocation.Text = Me.txtLocation.Text & "\"
                'Me.btnFileMessage.Enabled = True
                EnableFileButton()
            End If

        End With
    End Sub

    Private Sub btnFileMessage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileMessage.Click

        If bStarted = False Then
            Me.bStarted = True
            Me.btnFileMessage.Text = "STOP"

            If Not sParentFolderPath.EndsWith("\") Then sParentFolderPath = sParentFolderPath & "\"

            If Me.chkIncludeSubFolders.Checked = True Then
                IterateFolder(CType(m_olFolder, Outlook.Folder), True)
            Else
                IterateFolder(CType(m_olFolder, Outlook.Folder), False)
            End If

            Me.Close()
        Else
            'stop a search a close the form
            Me.bStarted = False
            Me.Close()
        End If

    End Sub

    Private Sub IterateFolder(ByRef m_olThisFolder As Outlook.Folder, Optional ByVal SubFolders As Boolean = False)
        Me.ProgressBar1.Maximum = m_olThisFolder.Items.Count
        Me.ProgressBar1.Value = 0

        sThisFolderPath = m_olThisFolder.FolderPath
        If Not sThisFolderPath.EndsWith("\") Then sThisFolderPath = sThisFolderPath & "\"

        Dim iCount As Integer = m_olThisFolder.Items.Count

        If iCount > 0 Then
            For i As Integer = iCount To 1 Step -1
                'check the process STOP button was pushed and exit out if required.
                If bStarted = False Then Exit Sub

                Dim m_olItem As Outlook.MailItem
                m_olItem = CType(m_olThisFolder.Items.Item(i), Outlook.MailItem)

                'check if the message has already been filed
                If m_olItem.UserProperties.Count = 0 Then
                    SaveMSG(m_olItem)
                Else
                    For Each Prop As Outlook.UserProperty In m_olItem.UserProperties
                        If Prop.Name = "emfiled" Then
                            If CBool(Prop.Value) = True Then
                                'do nothing
                            Else
                                SaveMSG(m_olItem)
                            End If
                        End If
                    Next
                End If

                Me.ProgressBar1.Value = Me.ProgressBar1.Value + 1

                Application.DoEvents()
            Next i
        End If

        If Me.chkIncludeSubFolders.Checked = True Then
            For Each folder As Microsoft.Office.Interop.Outlook.Folder In m_olThisFolder.Folders
                IterateFolder(folder, True)
            Next
        End If
    End Sub

    Private Sub SaveMSG(ByRef m_olMailItem As Outlook.MailItem)
        Dim SaveLocation As String = Me.txtLocation.Text & sThisFolderPath.Replace(sParentFolderPath, "")
        If Not SaveLocation.EndsWith("\") Then SaveLocation = SaveLocation & "\"

        'check the folder exists or create it
        If My.Computer.FileSystem.DirectoryExists(SaveLocation) = False Then
            My.Computer.FileSystem.CreateDirectory(SaveLocation)
        End If

        Dim sSaveMsgFilename As String = TrimLongFileName(SaveLocation & ParseFilename(Me.FileNameFilterBox1.FileNameFilter, m_olMailItem, bSentMail))

        If sSaveMsgFilename.EndsWith(cSettings.MessageFileExt) = False Then
            sSaveMsgFilename = sSaveMsgFilename & cSettings.MessageFileExt
        End If

        'first check the new msg filename dones't already exist
        If My.Computer.FileSystem.FileExists(sSaveMsgFilename) = True Then
            sSaveMsgFilename = sSaveMsgFilename.Replace(cSettings.MessageFileExt, " [filed - " & ShortYear() & ShortMonth1() & ShortDay() & "_" & DateTime.Now.ToShortTimeString.Replace(":", "-") & "]" & cSettings.MessageFileExt)
        End If

        'save the msg file
        m_olMailItem.SaveAs(sSaveMsgFilename, cSettings.MessageSaveAsType)

        ''now save the ADS data.
        'If cSettings.SaveADS = True Then
        '    WriteADS(m_olMailItem, sSaveMsgFilename)
        'End If


        'now delete message or record it has been filed.
        If Me.chkDeleteOnFile.Checked = True Then
            m_olMailItem.Move(emDeletedItems)
        Else
            With m_olMailItem
                .UserProperties.Add("emfiled", Outlook.OlUserPropertyType.olYesNo)
                .UserProperties("emfiled").Value = True
                .Categories = cSettings.ArchivedAndRetainedCategory
                .Save()
            End With

        End If

    End Sub

    Private Sub FileNameFilterBox1_IsValidChanged() Handles FileNameFilterBox1.IsValidChanged
        EnableFileButton()
    End Sub

    Private Sub EnableFileButton()
        If Me.FileNameFilterBox1.IsValid = False Then
            Me.btnFileMessage.Enabled = False
        ElseIf Me.FileNameFilterBox1.IsValid = False And Me.txtLocation.Text = String.Empty Then
            Me.btnFileMessage.Enabled = False
        ElseIf Me.FileNameFilterBox1.IsValid = True And Me.txtLocation.Text <> String.Empty Then
            Me.btnFileMessage.Enabled = True
        End If
    End Sub

End Class