'***************************************************************************
'
'         Copyright (c) Microsoft Corporation. All rights reserved.
'
'    This code sample is provided "AS IS" without warranty of any kind.
'
'***************************************************************************
Imports System.Windows.Forms
Imports System.Reflection

'Imports Outlook = Microsoft.Office.Interop.Outlook

''' <summary>
''' The EventTracker class handles the various Outlook events
''' and maintains and applies the event filter.
''' </summary>
Friend NotInheritable Class EventTracker

    ' Store references to the folder objects so that we can receive their events.
    ' If we do not maintain references to these items, then we cannot reliably 
    ' receive the add, change, and remove events.
    Private _inboxItems As Outlook.Items
    Private _sentItems As Outlook.Items
    'Private m_contactsItems As Outlook.Items
    'Private m_tasksItems As Outlook.Items
    'Private m_calendarItems As Outlook.Items

    Private _exp As Outlook.Explorer
    Private _app As ThisAddIn

    Private _monitorSentItems As Boolean = True
    Private _askedToMonitor As Boolean = False
    Private _sentItemsCancelCount As Integer = 0

    Private m_SentMailStack() As Outlook.MailItem

    ''' <summary>
    ''' Constructor.
    ''' </summary>
    ''' <param name="app">Reference to the main Outlook application object</param>
    Friend Sub New(ByVal app As ThisAddIn)
        _app = app

        _exp = app.Application.ActiveExplorer
        AddHandler _exp.SelectionChange, AddressOf ExplorerSelectionChange

        ' Obtain references to the folder objects that fire the events we are interested in.
        Dim mapiNamespace As Outlook.NameSpace = app.Application.GetNamespace("MAPI")
        Dim inbox As Outlook.Folder = TryCast(mapiNamespace.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox), Outlook.Folder)
        Dim sentitems As Outlook.Folder = TryCast(mapiNamespace.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderSentMail), Outlook.Folder)

        ' Store references to the item collection objects.
        _inboxItems = inbox.Items
        _sentItems = sentitems.Items

        ' Subscribe to the ItemAdd events.
        ' Note that there are cases in which the ItemAdd event is not raised. For example, if multiple
        ' items are created or received, then the ItemAdd event may be raised only once.
        ' See http://support.microsoft.com/?kbid=249156 for details.
        AddHandler _inboxItems.ItemAdd, New Outlook.ItemsEvents_ItemAddEventHandler(AddressOf InboxFolderItemAdded)
        AddHandler _sentItems.ItemAdd, New Outlook.ItemsEvents_ItemAddEventHandler(AddressOf SentItemsFolderItemAdded)

        ' Subscribe to the ItemChange events.
        AddHandler _inboxItems.ItemChange, New Microsoft.Office.Interop.Outlook.ItemsEvents_ItemChangeEventHandler(AddressOf InboxItemsItemChange)
        AddHandler _sentItems.ItemChange, New Microsoft.Office.Interop.Outlook.ItemsEvents_ItemChangeEventHandler(AddressOf SentItemsItemChange)

        ' Subscribe to the ItemRemove events.
        AddHandler _inboxItems.ItemRemove, New Microsoft.Office.Interop.Outlook.ItemsEvents_ItemRemoveEventHandler(AddressOf InboxItemsItemRemove)
        AddHandler _sentItems.ItemRemove, New Microsoft.Office.Interop.Outlook.ItemsEvents_ItemRemoveEventHandler(AddressOf SentItemsItemRemove)

    End Sub

    Private Sub ExplorerSelectionChange()
        Dim exp As Outlook.Explorer
        exp = _app.Application.ActiveExplorer

        Dim selectedFolder As Outlook.Folder = exp.CurrentFolder

        Select Case selectedFolder.Name.ToLower
            Case "inbox"
                'Try
                '    If exp.Selection.Count > 0 Then
                '        Dim selObject As Object = exp.Selection.Item(1)

                '        If (TypeOf selObject Is Outlook.MailItem) Then
                '            Dim mailItem As Outlook.MailItem = selObject

                '            'If mailItem.UnRead = True Then
                '            '    mailItem.Display(False)

                '            '    Dim frm As New frmMain(New Outlook.Application, mailItem)
                '            '    With frm
                '            '        .bSentMail = False
                '            '        .ShowDialog()
                '            '        .Dispose()
                '            '    End With
                '            'End If

                '        End If


                '    End If

                'Catch ex As Exception
                '    MsgBox(ex.Message)
                'End Try

            Case "sent items"

        End Select

    End Sub


#Region " Items Added "
    Private Sub InboxFolderItemAdded(ByVal addedItem As Object)
        If TypeOf addedItem Is Outlook.MailItem Then
            '  MessageBox.Show("New mail item arrived in inbox." + DirectCast(addedItem, Outlook.MailItem).Subject)
            Dim m_olMailItem As Outlook.MailItem = addedItem

            If cSettings.AutoArchiveIn = False Then
                'do nothing
            Else
                Dim sSaveMsgFilename As String = TrimLongFileName(cSettings.AutoArchiveInPath & ParseFilename(cSettings.FilenameFilter, m_olMailItem, False))

                If sSaveMsgFilename.EndsWith(cSettings.MessageFileExt) = False Then
                    sSaveMsgFilename = sSaveMsgFilename & cSettings.MessageFileExt
                End If

                'first check the new msg filename dones't already exist
                If My.Computer.FileSystem.FileExists(sSaveMsgFilename) = True Then
                    sSaveMsgFilename = sSaveMsgFilename.Replace(cSettings.MessageFileExt, " [filed - " & ShortYear() & ShortMonth1() & ShortDay() & "_" & DateTime.Now.ToShortTimeString.Replace(":", "-") & "]" & cSettings.MessageFileExt)
                End If

                'save the msg and set its file creation time to match the mail sent time
                m_olMailItem.SaveAs(sSaveMsgFilename, cSettings.MessageSaveAsType)

                Dim fileinfo As New IO.FileInfo(sSaveMsgFilename)
                With fileinfo
                    .CreationTime = m_olMailItem.SentOn
                End With

                'now set the archived and retained category
                With m_olMailItem
                    .UserProperties.Add("emfiled", Outlook.OlUserPropertyType.olYesNo)
                    .UserProperties("emfiled").Value = True
                    .Categories = cSettings.ArchivedAndRetainedCategory
                    .Save()
                End With

                ''now save the ADS data.
                'If cSettings.SaveADS = True Then
                '    WriteADS(m_olMailItem, sSaveMsgFilename)
                'End If
            End If
        End If
    End Sub

    Private Sub SentItemsFolderItemAdded(ByVal addedItem As Object)

        If TypeOf addedItem Is Outlook.MailItem Then
            'mailitem added to sent items folder
            Dim m_olMailItem As Outlook.MailItem = addedItem

            'check if we need user interaction
            If cSettings.AutoArchiveOut = False Then

                'check if this is a duplicate mailitem or not.  
                'handling a bug from google apps
                If m_olLastItem IsNot Nothing Then
                    If m_olLastItem.SenderName = m_olMailItem.SenderName And
                        m_olLastItem.To = m_olMailItem.To And
                        m_olLastItem.SentOn = m_olMailItem.SentOn And
                        m_olLastItem.Subject = m_olMailItem.Subject And
                        m_olLastItem.Attachments.Count = m_olMailItem.Attachments.Count And
                        m_olLastItem.Body = m_olMailItem.Body Then
                        'we seem to have a duplicate message so ingore it
                        Exit Sub

                        'TODO: perform the same action as the last message
                    Else
                        'seems to be a unique message so carry on
                    End If
                End If

                'check if we should monitor sent items.
                If _monitorSentItems = False Then
                    Exit Sub
                End If

                Dim frmMain As New frmMain()

                'TODO: check if the form is already visible

                'TODO: form is already visible so add sent item to stack

                With frmMain
                    .m_olMailItem = m_olMailItem
                    .bBatchFile = False
                    .bSentMail = True

                    If .ShowDialog = DialogResult.Cancel Then

                        'check for user wanting to stop monitoring folder
                        If _sentItemsCancelCount + 1 >= 5 Then
                            If _monitorSentItems = True And _askedToMonitor = False Then
                                If MsgBox("You have cancelled saving the last 5 sent messages. Would you like to stop monitoring sent emails until Outlook is restarted?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                    _sentItemsCancelCount = _sentItemsCancelCount + 1
                                    _monitorSentItems = False
                                Else
                                    _monitorSentItems = True
                                End If

                                _askedToMonitor = True
                            End If
                        Else
                            _sentItemsCancelCount = _sentItemsCancelCount + 1
                        End If

                    Else
                        _sentItemsCancelCount = 0
                    End If

                    .Dispose()
                End With


                m_olLastItem = m_olMailItem

            Else
                'autosave the sent item as it is sent
                Dim sSaveMsgFilename As String = TrimLongFileName(cSettings.AutoArchiveInPath & ParseFilename(cSettings.FilenameFilter, m_olMailItem, True))

                If sSaveMsgFilename.EndsWith(cSettings.MessageFileExt) = False Then
                    sSaveMsgFilename = sSaveMsgFilename & cSettings.MessageFileExt
                End If

                'first check the new msg filename dones't already exist
                If My.Computer.FileSystem.FileExists(sSaveMsgFilename) = True Then
                    sSaveMsgFilename = sSaveMsgFilename.Replace(cSettings.MessageFileExt, " [filed - " & ShortYear() & ShortMonth1() & ShortDay() & "_" & DateTime.Now.ToShortTimeString.Replace(":", "-") & "]" & cSettings.MessageFileExt)
                End If

                'save the msg and set its file creation time to match the mail sent time
                m_olMailItem.SaveAs(sSaveMsgFilename, cSettings.MessageSaveAsType)

                Dim fileinfo As New IO.FileInfo(sSaveMsgFilename)
                With fileinfo
                    .CreationTime = m_olMailItem.SentOn
                End With

                'now set the archived and retained category
                With m_olMailItem
                    .UserProperties.Add("emfiled", Outlook.OlUserPropertyType.olYesNo)
                    .UserProperties("emfiled").Value = True
                    .Categories = cSettings.ArchivedAndRetainedCategory
                    .Save()
                End With

                ''now save the ADS data.
                'If cSettings.SaveADS = True Then
                '    WriteADS(m_olMailItem, sSaveMsgFilename)
                'End If
            End If

        End If

    End Sub

#End Region

#Region " Items Changed "

    Private Sub InboxItemsItemChange(ByVal changedItem As Object)

        'Dim m_MailItem As Outlook.MailItem

        'If TypeOf changedItem Is Outlook.MailItem Then
        '    m_MailItem = DirectCast(changedItem, Outlook.MailItem)
        'Else
        '    Exit Sub
        'End If

        'If m_MailItem.UnRead = False And m_MailItem.DownloadState = Outlook.OlDownloadState.olFullItem Then
        '    'MessageBox.Show("Mail item has been changed" + DirectCast(changedItem, Outlook.MailItem).Subject)

        '    Dim frmMain As New frmMain(New Outlook.Application, changedItem)
        '    frmMain.ShowDialog()
        '    frmMain.Dispose()
        'End If

    End Sub

    Private Sub SentItemsItemChange(ByVal changedItem As Object)
        If TypeOf changedItem Is Outlook.MailItem Then
            ' MessageBox.Show("Mail item has been changed" + DirectCast(changedItem, Outlook.MailItem).Subject)
        End If
    End Sub

#End Region

#Region " Items Removed "
    Private Sub InboxItemsItemRemove()
        'MessageBox.Show("A mail item has been removed.")
    End Sub

    Private Sub SentItemsItemRemove()
        'MessageBox.Show("A mail item has been removed.")
    End Sub

#End Region


    Private Sub FileSentItemInStack(ByRef m_olMailItem As Outlook.MailItem)

        'check if this is a duplicate mailitem or not.  
        'handling a bug from google apps
        If m_olLastItem IsNot Nothing Then
            If m_olLastItem.SenderName = m_olMailItem.SenderName And
                m_olLastItem.To = m_olMailItem.To And
                m_olLastItem.SentOn = m_olMailItem.SentOn And
                m_olLastItem.Subject = m_olMailItem.Subject And
                m_olLastItem.Attachments.Count = m_olMailItem.Attachments.Count And
                m_olLastItem.Body = m_olMailItem.Body Then
                'we seem to have a duplicate message so ingore it
                Exit Sub

                'TODO: perform the same action as the last message
            Else
                'seems to be a unique message so carry on
            End If
        End If

        'check if we should monitor sent items.
        If _monitorSentItems = False Then
            Exit Sub
        End If

        Dim frmMain As New frmMain()

        'TODO: check if the form is already visible

        'TODO: form is already visible so add sent item to stack

        With frmMain
            .m_olMailItem = m_olMailItem
            .bBatchFile = False
            .bSentMail = True

            If .ShowDialog = DialogResult.Cancel Then

                'check for user wanting to stop monitoring folder
                If _sentItemsCancelCount + 1 >= 5 Then
                    If _monitorSentItems = True And _askedToMonitor = False Then
                        If MsgBox("You have cancelled saving the last 5 sent messages. Would you like to stop monitoring sent emails until Outlook is restarted?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                            _sentItemsCancelCount = _sentItemsCancelCount + 1
                            _monitorSentItems = False
                        Else
                            _monitorSentItems = True
                        End If

                        _askedToMonitor = True
                    End If
                Else
                    _sentItemsCancelCount = _sentItemsCancelCount + 1
                End If

            Else
                _sentItemsCancelCount = 0
            End If

                .Dispose()
            End With

        m_olLastItem = m_olMailItem
    End Sub
End Class
