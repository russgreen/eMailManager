Imports System.Collections.ObjectModel
Imports System.IO
Imports System.Windows.Forms
Imports Microsoft.Win32
Imports System.Diagnostics
Imports iwantedue
Imports System.Collections

Public Class frmSearch
    Private LocRM As New Resources.ResourceManager("eMailManager.ResourceStrings", Me.GetType.Assembly)

    Dim files As ReadOnlyCollection(Of String)
    Dim bStarted As Boolean = False
    Dim bLoading As Boolean = False

    Dim objNS As Outlook.NameSpace
    Dim objFolder As Outlook.Folder

    Private LastSelectedCSI As ExpTreeLib.CShItem

    Private ReadOnly Property SearchOption As SearchOption
        Get
            If (Not Me.chkSubdirs.Checked) Then
                Return SearchOption.TopDirectoryOnly
            End If
            Return SearchOption.AllDirectories
        End Get
    End Property

    Private Sub frmSearch_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If Keys.Escape Then
            If bStarted = True Then
                'stop and reset a search
                Me.btnSearch.Text = LocRM.GetString("sSEARCH") '"SEARCH"
                Me.bStarted = False
                Application.DoEvents()
            End If

            If bLoading = True Then
                Me.bLoading = False
                Application.DoEvents()
            End If


        End If
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        If Me.TabControl1.SelectedTab.Text = "Search" Then
            Me.Text = "Search"
        Else
            Me.Text = "Browse for MSG files"
        End If
    End Sub

    Private Sub ExpTree2_ExpTreeNodeSelected(SelPath As String, Item As ExpTreeLib.CShItem) Handles ExpTree2.ExpTreeNodeSelected
        Me.ListView1.Items.Clear()

        Me.ListView1.Columns.Clear()
        Me.ListView1.Columns.Add("msg filename", 300)
        Me.ListView1.Columns.Add("from", 120)
        Me.ListView1.Columns.Add("to", 120)
        Me.ListView1.Columns.Add("cc", 120)
        Me.ListView1.Columns.Add("subject", 250)
        Me.ListView1.Columns.Add("date", 100)
        Me.ListView1.Columns.Add("attachments", 60)

        Dim fileList As New ArrayList()
        LastSelectedCSI = Item

        If Item.DisplayName.Equals(ExpTreeLib.CShItem.strMyComputer) Then
            'do nothing
        Else
            fileList = Item.GetFiles("*.msg")
        End If

        Me.ToolStripProgressBar1.Value = 0
        Me.ToolStripProgressBar1.Maximum = fileList.Count
        Me.ToolStripProgressBar1.Visible = True
        Me.ToolStripStatusLabel1.Text = LocRM.GetString("Message013") '"Searching message files"

        If fileList.Count > 0 Then
            fileList.Sort()
            Me.Text = SelPath

            Me.bLoading = True

            For Each Item In fileList
                On Error Resume Next

                Dim msgFile As String = Item.Path


                'read the information from the msg file
                Dim messageStream As Stream = File.Open(msgFile, FileMode.Open, FileAccess.Read)
                Dim message As New OutlookStorage.Message(messageStream)
                messageStream.Close()

                Dim sTo As String = ""
                Dim sCc As String = ""

                For iR As Integer = 0 To message.Recipients.Count - 1
                    If iR = 0 Then
                        sTo = message.Recipients(iR).DisplayName
                    Else
                        sTo = sTo & "; " & message.Recipients(iR).DisplayName
                    End If
                Next

                Dim sSubject As String = NullSafeString(message.Subject)
                Dim sFrom As String = NullSafeString(message.From, "-")

                Dim sBodyRTF As String = message.BodyRTF

                Dim dDate As Date = NullSafeDate(message.ReceivedOrSentTime)


                'now build the listview item and add it to the listview
                Dim objListViewItem As ListViewItem

                'Create a new ListViewItem
                objListViewItem = New ListViewItem

                'Add the data to the ListViewItem
                objListViewItem.Text = IO.Path.GetFileName(msgFile)
                objListViewItem.Tag = msgFile

                objListViewItem.SubItems.Add(sFrom.ToString)
                objListViewItem.SubItems.Add(sTo.ToString)
                objListViewItem.SubItems.Add(sCc.ToString)
                objListViewItem.SubItems.Add(sSubject.ToString)
                'objListViewItem.SubItems.Add(storage.GetTimeDateProperty.ToShortDateString)
                objListViewItem.SubItems.Add(message.ReceivedOrSentTime.ToShortDateString)
                objListViewItem.SubItems.Add(message.Attachments.Count.ToString)

                Me.ListView1.Items.Add(objListViewItem)
                Me.ListView1.EnsureVisible(Me.ListView1.Items.Count - 1)

                Me.ToolStripProgressBar1.Value = Me.ToolStripProgressBar1.Value + 1

                If Me.bLoading = False Then Exit For
                Application.DoEvents()
            Next
        End If

        Me.ToolStripStatusLabel1.Text = ""
        Me.ToolStripProgressBar1.Visible = False

    End Sub


    Private Sub rdoOutlook_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoOutlook.CheckedChanged
        If Me.rdoOutlook.Checked = True Then
            Me.chkSubdirs.Checked = False
            Me.chkSubdirs.Enabled = False
            Me.SplitContainer1.Panel2.Hide()
            Me.SplitContainer1.SplitterDistance = Me.SplitContainer1.Height

            Me.gbSearchType.Enabled = False
            Me.gbSimple.Enabled = False
            Me.gbDetailed.Enabled = True
        Else
            Me.chkSubdirs.Enabled = True
            Me.SplitContainer1.Panel2.Show()
            Me.SplitContainer1.SplitterDistance = CInt(Me.SplitContainer1.Height / 2)

            Me.gbSearchType.Enabled = True

            If Me.rdoSearchSimple.Checked = True Then
                Me.gbDetailed.Enabled = False
                Me.gbSimple.Enabled = True
            ElseIf Me.rdoSearchDetailedFull.Checked = True Then
                Me.gbDetailed.Enabled = True
                Me.gbSimple.Enabled = False
            End If
        End If
    End Sub

    Private Sub rdoSearchType_CheckChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoSearchSimple.CheckedChanged, rdoSearchDetailedFull.CheckedChanged
        If Me.rdoSearchSimple.Checked = True Then
            Me.gbDetailed.Enabled = False
            Me.gbSimple.Enabled = True

            'ElseIf Me.rdoSearchDetailedADS.Checked = True Then
            '    Me.gbDetailed.Enabled = True
            '    Me.gbSimple.Enabled = False
            '    Me.gbDetailed.Text = LocRM.GetString("SearchTitle001") '"Detailed Search"

        ElseIf Me.rdoSearchDetailedFull.Checked = True Then
            Me.gbDetailed.Enabled = True
            Me.gbSimple.Enabled = False
            Me.gbDetailed.Text = LocRM.GetString("SearchTitle002") '"Detailed Search (slow method)"

        End If

    End Sub

    Private Sub frmSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.ToolStripProgressBar1.Value = 0
        Me.ToolStripProgressBar1.Visible = False
        Me.ToolStripStatusLabel1.Text = Nothing

        'Me.gpSearchDates.Visible = False
        Me.dtpFrom.Value = Date.Now.AddYears(-1)

        Me.txtMessageBody.Dock = DockStyle.Fill
        Me.txtMessageBody.Visible = True
        Me.htmMessageBody.Visible = False
        Me.rtbMessageBody.Visible = False

        Me.ListView1.Columns.Clear()
        Me.ListView1.Columns.Add("msg filename", 600)

        On Error Resume Next
        Dim oRegCurrentUser As Microsoft.Win32.RegistryKey
        oRegCurrentUser = My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager\MRU")

        Dim sValueNames() As String
        Dim sValueName As String

        sValueNames = oRegCurrentUser.GetValueNames
        Array.Sort(sValueNames)

        Dim maxValLen As Integer = 100
        For Each sValueName In sValueNames
            Me.txtLocation.AutoCompleteCustomSource.Add(oRegCurrentUser.GetValue(sValueName).ToString.Substring(0, oRegCurrentUser.GetValue(sValueName).ToString.IndexOf("|")))
            'Me.cboLocation.AutoCompleteCustomSource.Add(oRegCurrentUser.GetValue(sValueName).ToString.Substring(0, oRegCurrentUser.GetValue(sValueName).ToString.IndexOf("|")))

            'Dim strLen As Integer = (oRegCurrentUser.GetValue(sValueName).ToString.Substring(0, oRegCurrentUser.GetValue(sValueName).ToString.IndexOf("|"))).Length
            'If maxValLen < strLen Then
            '    maxValLen = strLen

            '    Me.cboLocation.DropDownWidth = maxValLen
            '    Me.cboLocation.a()

            'End If
        Next

    End Sub

    Private Sub btnBrowse_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        On Error Resume Next
        If Me.rdoFileSystem.Checked = True Then
            With Me.FolderBrowserDialog1
                .Description = LocRM.GetString("SearchTitle003") '"Select a folder to search for *.msg files"

                If .ShowDialog = System.Windows.Forms.DialogResult.OK Then
                    Me.txtLocation.Text = .SelectedPath
                End If
            End With
        Else
            Dim m_olOutlook As New Outlook.Application
            objNS = m_olOutlook.GetNamespace("MAPI")
            objFolder = objNS.PickFolder
            Me.txtLocation.Text = objFolder.FolderPath
        End If

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If bStarted = False Then
            'start a new search
            Me.btnSearch.Text = LocRM.GetString("sSTOP") '"STOP"
            Me.bStarted = True

            DoSearch()

        ElseIf bStarted = True Then
            'stop and reset a search
            Me.btnSearch.Text = LocRM.GetString("sSEARCH") '"SEARCH"
            Me.bStarted = False
            Application.DoEvents()

        End If
    End Sub

    Private Sub btnPopLocations_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPopLocations.Click
        Dim frm As New frmMRU
        Dim loc As New Drawing.Point

        loc.X = Me.Location.X + 30
        loc.Y = Me.Location.Y + 115

        With frm
            .StartPosition = FormStartPosition.Manual
            .Location = loc
            .ShowDialog(Me)

            Dim selectedpath As String = .SelectedSearchPath
            Me.rdoOutlook.Checked = True

            Select Case True
                Case selectedpath.StartsWith("\\personal folders")
                    'probably be an outlook folder
                    Me.rdoOutlook.Checked = True
                Case selectedpath.StartsWith("\\public folders")
                    'probably be an outlook folder
                    Me.rdoOutlook.Checked = True
                Case selectedpath.StartsWith("\\mailbox")
                    'probably be an outlook folder
                    Me.rdoOutlook.Checked = True
                Case selectedpath.Remove(0, 1).StartsWith(":\")
                    'probably the file system
                    Me.rdoFileSystem.Checked = True
                Case My.Computer.FileSystem.DirectoryExists(selectedpath)
                    'probably the file system
                    Me.rdoFileSystem.Checked = True
                Case Else

            End Select

            If Me.rdoOutlook.Checked = True Then
                objNS = Globals.ThisAddIn.Application.GetNamespace("MAPI")
                'objFolder = objNS.GetFolderFromID(lvwMRU.SelectedItems(0).SubItems(1).Text)
                objFolder = objNS.GetFolderFromID(selectedpath)
                'objFolder = objNS.GetFolderFromID(lvwMRU.SelectedItems(0).Text)
                Me.txtLocation.Text = objFolder.FolderPath
            Else
                Me.txtLocation.Text = selectedpath
            End If




        End With
    End Sub

    Private Sub DoSearch()
        If Me.rdoFileSystem.Checked = True Then
            Me.ToolStripProgressBar1.Visible = False
            Me.ToolStripStatusLabel1.Text = LocRM.GetString("Message011") '"Please wait! Gathering all *.MSG files to search"
            Application.DoEvents()

            Dim files As FastDirectoryEnumerator.FileData() = FastDirectoryEnumerator.FastDirectoryEnumerator.GetFiles(Me.txtLocation.Text, "*.msg", SearchOption)

            Me.ToolStripStatusLabel1.Text = LocRM.GetString("Message012") '"Finshed gathering *.MSG files"
            Application.DoEvents()

            Me.ToolStripProgressBar1.Value = 0
            Me.ToolStripProgressBar1.Maximum = files.Count
            Me.ToolStripProgressBar1.Visible = True

            If Me.rdoSearchSimple.Checked = True Then
                SearchFileSystemSimple(files)
                'ElseIf Me.rdoSearchDetailedADS.Checked = True Then
                '    SearchfilesystemADS(files)
            ElseIf Me.rdoSearchDetailedFull.Checked = True Then
                SearchfilesystemDetailed(files)
            End If

        Else
            Searchoutlookfolders()
        End If
    End Sub

    Private Sub Searchoutlookfolders()
        Me.ListView1.Items.Clear()

        Me.ListView1.Columns.Clear()
        Me.ListView1.Columns.Add("from", 120)
        Me.ListView1.Columns.Add("to", 120)
        Me.ListView1.Columns.Add("cc", 120)
        Me.ListView1.Columns.Add("subject", 250)
        Me.ListView1.Columns.Add("date", 100)
        Me.ListView1.Columns.Add("attachments", 60)

        Me.ToolStripProgressBar1.Value = 0
        Me.ToolStripProgressBar1.Visible = True
        Me.ToolStripStatusLabel1.Text = "Searching message files"

        'Dim m_olOutlook As New Outlook.Application
        Dim sCriteria As String = BuildSearchCriteria()
        Dim objItems As Outlook.Items = objFolder.Items

        'restrict the items to search
        Dim objRestrictedItems As Outlook.Items = objItems.Restrict(sCriteria)

        Me.ToolStripProgressBar1.Maximum = objRestrictedItems.Count

        For i As Integer = 1 To objRestrictedItems.Count
            If bStarted = False Then
                Me.ToolStripStatusLabel1.Text = LocRM.GetString("sFINISHED") '"Finished"
                Me.btnSearch.Text = LocRM.GetString("sSEARCH") '"SEARCH"
                Exit Sub
            End If

            Dim objMailItem As Outlook.MailItem = CType(objRestrictedItems.Item(i), Outlook.MailItem)

            'now build the listview item and add it to the listview
            Dim objListViewItem As ListViewItem

            'Create a new ListViewItem
            objListViewItem = New ListViewItem

            'Add the data to the ListViewItem
            objListViewItem.Text = objMailItem.SenderName
            objListViewItem.Tag = objMailItem.EntryID

            objListViewItem.SubItems.Add(objMailItem.To)
            objListViewItem.SubItems.Add(objMailItem.CC)
            objListViewItem.SubItems.Add(objMailItem.Subject)
            objListViewItem.SubItems.Add(objMailItem.SentOn.ToShortDateString)
            objListViewItem.SubItems.Add(objMailItem.Attachments.Count.ToString)

            Me.ListView1.Items.Add(objListViewItem)

            Me.ToolStripProgressBar1.Value = Me.ToolStripProgressBar1.Value + 1

            Application.DoEvents()
        Next

        Me.bStarted = False
        Me.ToolStripStatusLabel1.Text = LocRM.GetString("sFINISHED") '"Finished"
        Me.btnSearch.Text = LocRM.GetString("sSEARCH") '"SEARCH"
    End Sub

    Private Function BuildSearchCriteria() As String
        Dim retval As New StringBuilder

        Dim firstVal As Boolean = True

        retval.Append("@SQL=")

        If Me.txtFrom.Text <> Nothing Then
            retval.Append(Chr(34) & "urn:schemas:httpmail:fromname" & Chr(34) & " LIKE '%" & Me.txtFrom.Text & "%'")
            firstVal = False
        End If

        If Me.txtTo.Text <> Nothing Then
            If firstVal = False Then
                retval.Append(" AND ")
            End If
            retval.Append(Chr(34) & "urn:schemas:httpmail:displayto" & Chr(34) & " LIKE '%" & Me.txtTo.Text & "%'")
            firstVal = False
        End If

        If Me.txtCc.Text <> Nothing Then
            If firstVal = False Then
                retval.Append(" AND ")
            End If
            retval.Append(Chr(34) & "urn:schemas:httpmail:displaycc" & Chr(34) & " LIKE '%" & Me.txtCc.Text & "%'")
            firstVal = False
        End If

        If Me.txtSubject.Text <> Nothing Then
            If firstVal = False Then
                retval.Append(" AND ")
            End If
            retval.Append(Chr(34) & "urn:schemas:httpmail:subject" & Chr(34) & " LIKE '%" & Me.txtSubject.Text & "%'")
            firstVal = False
        End If

        If Me.txtMessageBody.Text <> Nothing Then
            If firstVal = False Then
                retval.Append(" AND ")
            End If
            retval.Append(Chr(34) & "urn:schemas:httpmail:textdescription" & Chr(34) & " LIKE '%" & Me.txtMessageBody.Text & "%'")
            firstVal = False
        End If

        If firstVal = False Then
            retval.Append(" AND ")
        End If
        retval.Append(Chr(34) & "urn:schemas:httpmail:date" & Chr(34) & " >= '" & Me.dtpFrom.Value.ToShortDateString & "'")
        retval.Append(" AND ")
        retval.Append(Chr(34) & "urn:schemas:httpmail:datereceived" & Chr(34) & " <= '" & Me.dtpTo.Value.ToShortDateString & "'")

        Return retval.ToString
    End Function

    Private Sub SearchFileSystemSimple(ByRef files As FastDirectoryEnumerator.FileData())
        Me.ListView1.Items.Clear()
        Me.ListView1.Columns.Clear()
        Me.ListView1.Columns.Add("msg filename", 600)
        Me.ToolStripStatusLabel1.Text = LocRM.GetString("Message008")


        For Each File As FastDirectoryEnumerator.FileData In files
            If File.Name.Contains(Me.txtSimpleSearch.Text) Then
                'now build the listview item and add it to the listview
                Dim objListViewItem As ListViewItem

                'Create a new ListViewItem
                objListViewItem = New ListViewItem

                'Add the data to the ListViewItem
                objListViewItem.Text = IO.Path.GetFileName(File.Name)
                objListViewItem.Tag = File.Path

                Me.ListView1.Items.Add(objListViewItem)
                Me.ListView1.EnsureVisible(Me.ListView1.Items.Count - 1)
            End If

            Me.ToolStripProgressBar1.Value = Me.ToolStripProgressBar1.Value + 1

            If Me.btnSearch.Text = LocRM.GetString("sSEARCH") Then Exit For
            Application.DoEvents()
        Next

        Me.btnSearch.Text = LocRM.GetString("sSEARCH")


    End Sub
    Private Sub SearchFileSystemSimple_old(ByVal files() As String)
        Me.ListView1.Items.Clear()
        Me.ListView1.Columns.Clear()
        Me.ListView1.Columns.Add("msg filename", 600)
        Me.ToolStripStatusLabel1.Text = LocRM.GetString("Message008")

        For i As Integer = 0 To files.Count - 1
            Dim msgfile As String = files(i)

            'search criteria
            Dim MeetsCriteria As Boolean = False

            If Me.txtSimpleSearch.Text = "" Then
                MeetsCriteria = True
            End If

            If MeetsCriteria = False Then
                If SearchWithinString(Me.txtSimpleSearch.Text, IO.Path.GetFileName(msgfile)) = True Then MeetsCriteria = True
            End If

            If MeetsCriteria = True Then
                'now build the listview item and add it to the listview
                Dim objListViewItem As ListViewItem

                'Create a new ListViewItem
                objListViewItem = New ListViewItem

                'Add the data to the ListViewItem
                objListViewItem.Text = IO.Path.GetFileName(msgfile)
                objListViewItem.Tag = msgfile

                Me.ListView1.Items.Add(objListViewItem)
                Me.ListView1.EnsureVisible(Me.ListView1.Items.Count - 1)
            End If

            Me.ToolStripProgressBar1.Value = Me.ToolStripProgressBar1.Value + 1

            If i = files.Count - 1 Then Me.btnSearch.Text = LocRM.GetString("sSEARCH") '"SEARCH"
            If Me.btnSearch.Text = LocRM.GetString("sSEARCH") Then Exit For
            Application.DoEvents()
        Next i

    End Sub

    Private Sub SearchFileSystemDetailed(ByRef files As FastDirectoryEnumerator.FileData())
        Me.ListView1.Items.Clear()

        Me.ListView1.Columns.Clear()
        Me.ListView1.Columns.Add("msg filename", 300)
        Me.ListView1.Columns.Add("from", 120)
        Me.ListView1.Columns.Add("to", 120)
        Me.ListView1.Columns.Add("cc", 120)
        Me.ListView1.Columns.Add("subject", 250)
        Me.ListView1.Columns.Add("date", 100)
        Me.ListView1.Columns.Add("attachments", 60)

        Me.ToolStripProgressBar1.Value = 0
        Me.ToolStripProgressBar1.Visible = True
        Me.ToolStripStatusLabel1.Text = LocRM.GetString("Message008") '"searching message files"

        Me.ToolStripProgressBar1.Maximum = files.Count - 1

        For Each File As FastDirectoryEnumerator.FileData In files
            On Error Resume Next

            Dim messageStream As Stream = System.IO.File.Open(File.Path, FileMode.Open, FileAccess.Read)
            Dim message As New OutlookStorage.Message(messageStream)
            messageStream.Close()

            Dim sTo As String = ""
            Dim sCc As String = ""

            For iR As Integer = 0 To message.Recipients.Count - 1
                If iR = 0 Then
                    sTo = message.Recipients(iR).DisplayName
                Else
                    sTo = sTo & "; " & message.Recipients(iR).DisplayName
                End If
            Next

            Dim sSubject As String = NullSafeString(message.Subject)
            Dim sFrom As String = NullSafeString(message.From, "-")

            'Dim sBodyRTF As String = message.BodyRTF
            Dim sBody As String = message.BodyText

            Dim dDate As Date = NullSafeDate(message.ReceivedOrSentTime)

            'search criteria
            Dim MeetsCriteria As Boolean = False

            If Me.txtFrom.Text = "" And _
            Me.txtTo.Text = "" And _
            Me.txtCc.Text = "" And _
            Me.txtSubject.Text = "" And _
            Me.txtBody.Text = "" And _
            IsBetweenDates(dDate) = True Then
                MeetsCriteria = True
            End If

            If MeetsCriteria = False Then
                If sFrom.ToLower.Contains(NullSafeString(Me.txtFrom.Text, "").ToLower) = True And
                 sTo.ToLower.Contains(NullSafeString(Me.txtTo.Text, "").ToLower) = True And
                sCc.ToLower.Contains(NullSafeString(Me.txtCc.Text, "").ToLower) = True And
                sSubject.ToLower.Contains(NullSafeString(Me.txtSubject.Text, "").ToLower) = True And
                sBody.ToLower.Contains(NullSafeString(Me.txtBody.Text, "").ToLower) = True And
            IsBetweenDates(dDate) = True Then
                    MeetsCriteria = True
                End If

                If SearchWithinString(Me.txtFrom.Text, sFrom) = True And
                 SearchWithinString(Me.txtTo.Text, sTo) = True And
                SearchWithinString(Me.txtCc.Text, sCc) = True And
                SearchWithinString(Me.txtSubject.Text, sSubject) = True And
               SearchWithinString(Me.txtBody.Text, sBody) = True And
            IsBetweenDates(dDate) = True Then
                    MeetsCriteria = True
                End If
            End If

            If MeetsCriteria = True Then
                'now build the listview item and add it to the listview
                Dim objListViewItem As ListViewItem

                'Create a new ListViewItem
                objListViewItem = New ListViewItem

                'Add the data to the ListViewItem
                objListViewItem.Text = File.Name
                objListViewItem.Tag = File.Path

                objListViewItem.SubItems.Add(sFrom.ToString)
                objListViewItem.SubItems.Add(sTo.ToString)
                objListViewItem.SubItems.Add(sCc.ToString)
                objListViewItem.SubItems.Add(sSubject.ToString)
                'objListViewItem.SubItems.Add(storage.GetTimeDateProperty.ToShortDateString)
                objListViewItem.SubItems.Add(message.ReceivedOrSentTime.ToShortDateString)
                objListViewItem.SubItems.Add(message.Attachments.Count.ToString)

                Me.ListView1.Items.Add(objListViewItem)
                Me.ListView1.EnsureVisible(Me.ListView1.Items.Count - 1)
            End If

            'close and clear the storage item
            message.Dispose()

            Me.ToolStripProgressBar1.Value = Me.ToolStripProgressBar1.Value + 1
            If Me.btnSearch.Text = LocRM.GetString("sSEARCH") Then Exit For
            Application.DoEvents()
        Next

        Me.bStarted = False
        Me.ToolStripStatusLabel1.Text = LocRM.GetString("sFINISHED") '"Finished"
        Me.btnSearch.Text = LocRM.GetString("sSEARCH") '"SEARCH"

    End Sub

    Private Function SearchWithinString(ByVal Search As String, ByVal Source As String) As Boolean
        Dim retval As Boolean = False

        Dim m_Separators() As Char = {" "c}
        Dim m_SourceWords() As String
        Dim m_SourceWordsCleaned() As String
        Dim m_FilteredWords As Collections.ArrayList
        Dim m_SearchWords() As String
        Dim m_SearchWordsCleaned() As String

        Dim m_Search As String = Search.ToLower
        Dim m_Source As String = RemoveIllegalSourceCharacters(Source.ToLower)

        If m_Search.StartsWith("""") = True And m_Search.EndsWith("""") = True Then
            'we must want to match the exact string only
            Dim trimarray() As Char = {""""c}
            If m_Source.ToLower.Contains(m_Search.ToLower.TrimEnd(trimarray).TrimStart(trimarray)) = True Then retval = True

        ElseIf m_Search.Contains(" + ") = True Then
            'i think we have a situation where words MUST all exist, 
            'though not necessarily in order

            'split the source into words
            m_SourceWords = m_Source.Split(m_Separators)

            'ensure we only have good words
            m_FilteredWords = New Collections.ArrayList
            For Each Word As String In m_SourceWords
                If Word <> String.Empty Then
                    m_FilteredWords.Add(Word)
                End If
            Next

            m_SourceWordsCleaned = CType(m_FilteredWords.ToArray(GetType(String)), String())
            Array.Sort(m_SourceWordsCleaned)

            'split the search into words
            m_SearchWords = m_Search.Split(m_Separators)

            'ensure we only have good words
            m_FilteredWords = New Collections.ArrayList
            For Each Word As String In m_SearchWords
                If Word <> String.Empty And Word <> "+" Then
                    m_FilteredWords.Add(Word)
                End If
            Next

            m_SearchWordsCleaned = CType(m_FilteredWords.ToArray(GetType(String)), String())
            Array.Sort(m_SearchWordsCleaned)

            'now we have to arrays of words to compare.

            'first loop through the source array and remove strings not found in the search array
            m_FilteredWords = New Collections.ArrayList
            For Each Word As String In m_SourceWordsCleaned
                If m_SearchWordsCleaned.Contains(Word) Then
                    m_FilteredWords.Add(Word)
                End If
            Next

            'now we should have a new array of source words that were found in the search words
            Dim m_SourceWordsForCompare() As String = CType(m_FilteredWords.ToArray(GetType(String)), String())
            Array.Sort(m_SourceWordsForCompare)

            'compare the 2 arrays
            Dim s1, s2 As String
            For Each item As String In m_SourceWordsForCompare
                s1 = s1 + item
            Next
            For Each item As String In m_SearchWordsCleaned
                s2 = s2 + item
            Next

            If s1 = s2 Then
                retval = True
            Else
                retval = False
            End If

        Else
            'split the source into words
            m_SourceWords = m_Source.Split(m_Separators)

            'ensure we only have good words
            m_FilteredWords = New Collections.ArrayList
            For Each Word As String In m_SourceWords
                If Word <> String.Empty Then
                    m_FilteredWords.Add(Word)
                End If
            Next

            m_SourceWordsCleaned = CType(m_FilteredWords.ToArray(GetType(String)), String())

            'split the search into words
            m_SearchWords = m_Search.Split(m_Separators)

            'ensure we only have good words
            m_FilteredWords = New Collections.ArrayList
            For Each Word As String In m_SearchWords
                If Word <> String.Empty And Word <> "+" Then
                    m_FilteredWords.Add(Word)
                End If
            Next

            m_SearchWordsCleaned = CType(m_FilteredWords.ToArray(GetType(String)), String())

            'Do While retval = False
            For Each Word As String In m_SearchWordsCleaned
                If m_Source.Contains(Word) Then
                    retval = True
                    Exit For
                End If

            Next
            'Loop
        End If

        Return retval
    End Function

    Private Function LoadMSGFiles(ByVal searchDirectory As String, Optional ByVal searchsubdirs As Boolean = True) As String()
        Dim fileList As New ArrayList()
        Dim ignoreList As New ArrayList()
        ignoreList.Add(searchDirectory & "Common Files")
        ignoreList.Add(searchDirectory & "$RECYCLE.BIN")
        ignoreList.Add(searchDirectory & "System Volume Information")

        Dim files As String()

        If searchsubdirs = True Then
            Dim subdirectories As String() = System.IO.Directory.GetDirectories(searchDirectory)

            For Each directory As String In subdirectories
                If Not ignoreList.Contains(directory) Then
                    files = LoadMSGFiles(directory)
                    fileList.AddRange(files)
                End If
            Next
        End If

        files = System.IO.Directory.GetFiles(searchDirectory, "*.msg")
        fileList.AddRange(files)

        Return DirectCast(fileList.ToArray(GetType(String)), String())
    End Function


#Region "  ListView Sorting  "
    Dim sortColumn As Integer = -1

    Private Sub ListView_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles ListView1.ColumnClick
        ' Set the ListViewItemSorter property to a new ListViewItemComparer
        ' object.
        ' Determine whether the column is the same as the last column clicked.

        If e.Column <> sortColumn Then
            ' Set the sort column to the new column.
            sortColumn = e.Column
            ' Set the sort order to ascending by default.
            Me.ListView1.Sorting = SortOrder.Ascending
        Else
            ' Determine what the last sort order was and change it.
            If Me.ListView1.Sorting = SortOrder.Ascending Then
                Me.ListView1.Sorting = SortOrder.Descending
            Else
                Me.ListView1.Sorting = SortOrder.Ascending
            End If
        End If
        ' Call the sort method to manually sort.
        Me.ListView1.Sort()
        ' Set the ListViewItemSorter property to a new ListViewItemComparer
        ' object.

        Me.ListView1.ListViewItemSorter = New ListViewItemComparer(e.Column, Me.ListView1.Sorting)
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

    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
        If Me.rdoFileSystem.Checked = True Then
            Process.Start(Me.ListView1.SelectedItems.Item(0).Tag)
        Else
            Dim m_olOutlook As New Outlook.Application
            objNS = m_olOutlook.GetNamespace("MAPI")

            Dim objMailItem As Outlook.MailItem
            objMailItem = CType(objNS.GetItemFromID(ListView1.SelectedItems(0).Tag.ToString), Outlook.MailItem)
            objMailItem.Display()

        End If

    End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged
        On Error Resume Next

        If Me.ListView1.SelectedItems.Count = 0 Then
            Me.lblFrom.Text = String.Empty
            Me.lblTo.Text = String.Empty
            Me.lblCc.Text = String.Empty
            Me.lblSubject.Text = String.Empty
            Me.txtMessageBody.Text = Nothing
            Me.rtbMessageBody.Rtf = Nothing
            Me.htmMessageBody.Document.Body.InnerHtml = Nothing
        Else
            If Me.rdoFileSystem.Checked = True Then
                'read the information from the msg file
                Dim messageStream As Stream = File.Open(Me.ListView1.SelectedItems.Item(0).Tag.ToString, FileMode.Open, FileAccess.Read)
                Dim message As New OutlookStorage.Message(messageStream)
                messageStream.Close()

                Dim sTo As String = ""
                Dim sCc As String = ""

                For iR As Integer = 0 To message.Recipients.Count - 1
                    If iR = 0 Then
                        sTo = message.Recipients(iR).DisplayName
                    Else
                        If message.Recipients(iR).Type = OutlookStorage.RecipientType.To Then
                            sTo = sTo & "; " & message.Recipients(iR).DisplayName

                        ElseIf message.Recipients(iR).Type = OutlookStorage.RecipientType.CC Then
                            If sCc = "" Then
                                sCc = message.Recipients(iR).DisplayName
                            Else
                                sCc = sCc & "; " & message.Recipients(iR).DisplayName
                            End If
                        End If

                    End If
                Next

                Dim sSubject As String = NullSafeString(message.Subject)
                Dim sFrom As String = message.From

                Me.lblFrom.Text = sFrom
                Me.lblTo.Text = sTo
                Me.lblCc.Text = sCc
                Me.lblSubject.Text = sSubject

                Dim sBodyTXT As String = message.BodyText
                Dim sBodyRTF As String = message.BodyRTF
                Dim sBodyHTML As String = message.BodyHTML

                'hide controls
                Me.htmMessageBody.Visible = False
                Me.txtMessageBody.Visible = False
                Me.rtbMessageBody.Visible = False

                If sBodyTXT = Nothing Then
                    If sBodyRTF = Nothing Then
                        If sBodyHTML = Nothing Then
                            'TODO: handle the chaos
                        Else
                            Me.htmMessageBody.Dock = DockStyle.Fill
                            Me.htmMessageBody.Visible = True
                            Me.htmMessageBody.Document.Body.InnerHtml = sBodyHTML
                            Me.htmMessageBody.Refresh()
                        End If
                    Else
                        Me.rtbMessageBody.Dock = DockStyle.Fill
                        Me.rtbMessageBody.Visible = True
                        Me.rtbMessageBody.Rtf = sBodyRTF
                    End If
                Else
                    Me.txtMessageBody.Dock = DockStyle.Fill
                    Me.txtMessageBody.Visible = True
                    Me.txtMessageBody.Text = sBodyTXT
                End If

                Me.lstattachments.Items.Clear()
                For i As Integer = 0 To message.Attachments.Count
                    Me.lstattachments.Items.Add(message.Attachments(i).Filename.ToString)
                Next

                message.Dispose()
            End If
        End If
    End Sub

    Private Sub rtbMessageBody_LinkClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkClickedEventArgs) Handles rtbMessageBody.LinkClicked
        On Error Resume Next
        Process.Start(e.LinkText)
    End Sub

    Private Function IsBetweenDates(ByVal d As Date) As Boolean
        Dim retval As Boolean = False
        Dim dFrom As Date = CDate(Me.dtpFrom.Value.Date.ToShortDateString & " 00:01")
        Dim dTo As Date = CDate(Me.dtpTo.Value.Date.ToShortDateString & " 23:59")

        If d >= dFrom And d <= dTo Then
            retval = True
        End If

        Return retval
    End Function







End Class