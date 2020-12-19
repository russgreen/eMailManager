Imports System.Windows.Forms
Imports System.IO

Public Class frmMain2

    Friend bBatchFile As Boolean = False
    Friend bSentMail As Boolean = False
    Friend sSavePathMsg As String = String.Empty

    Friend m_olMailItem As Outlook.MailItem
    Friend m_olSelection As Outlook.Selection

    Dim QuoteNo As Integer = 0

    Public Sub New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        Me.Text = My.Application.Info.Title & String.Format(" Version {0}", My.Application.Info.Version.ToString) ' My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.MajorRevision & "." & My.Application.Info.Version.MinorRevision
    End Sub

    Private Sub frmMain2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.ToolStripStatusLabel1.Text = String.Empty
        Me.ToolStripProgressBar1.Visible = False
        Me.lblQuote.Text = String.Empty

        cSettings.LoadSettings()

        If bSentMail = True Then
            Me.rdoArchiveDelete.Checked = True
        Else
            Me.rdoArchiveRetain.Checked = True
        End If

        'populate the mail details
        If bBatchFile = False Then
            Me.lblMailFrom.Text = m_olMailItem.SenderName
            Me.lblMailTo.Text = m_olMailItem.To
            Me.lblMailSubject.Text = m_olMailItem.Subject
        Else
            Me.lblMailFrom.Text = "batch file mode"
            Me.lblMailTo.Text = "batch file mode"
            Me.lblMailSubject.Text = "batch file mode"

            Me.lblFilename.Text = "batch file mode"
            Me.txtSubject.Text = "batch file mode"
            Me.txtSubject.Enabled = False
        End If

        If Not cSettings.Form2SavePath.EndsWith("\") Then
            Me.lblSavePath.Text = cSettings.Form2SavePath & "\"
        Else
            Me.lblSavePath.Text = cSettings.Form2SavePath
        End If

        Me.txtSubject.Text = Me.lblMailSubject.Text

        UpdateFileName()

        Me.btnFileMessage.Enabled = False
    End Sub


    Private Sub btnFileMessage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileMessage.Click
        If bBatchFile = True Then
            BatchFile()
            Exit Sub
        End If

        Dim sSaveMsgFilename As String = Me.lblSavePath.Text & Me.lblFilename.Text

        If sSaveMsgFilename.EndsWith(".msg") = False Then
            sSaveMsgFilename = sSaveMsgFilename & ".msg"
        End If

        Try

            'first check the new msg filename dones't already exist
            If My.Computer.FileSystem.FileExists(sSaveMsgFilename) = True Then
                sSaveMsgFilename = sSaveMsgFilename.Replace(".msg", " [filed - " & ShortYear() & ShortMonth1() & ShortDay() & "_" & DateTime.Now.ToShortTimeString.Replace(":", "-") & "].msg")
            End If

            m_olMailItem.SaveAs(sSaveMsgFilename, Microsoft.Office.Interop.Outlook.OlSaveAsType.olMSG)

            If Me.rdoArchiveRetain.Checked = True Then
                m_olMailItem.Categories = cSettings.ArchivedAndRetainedCategory
                m_olMailItem.Save()
            Else
                Try
                    m_olMailItem.Move(emDeletedItems)
                Catch
                    m_olMailItem.Move(olDeletedItems)
                End Try
            End If

            'now save the ADS data.
            If cSettings.SaveADS = True Then
                Try
                    AlternateDataStreams.ADSFile.Write(NullSafeString(m_olMailItem.SenderName), sSaveMsgFilename, "OEMFrom")
                    AlternateDataStreams.ADSFile.Write(NullSafeString(m_olMailItem.To), sSaveMsgFilename, "OEMTo")
                    AlternateDataStreams.ADSFile.Write(NullSafeString(m_olMailItem.Subject), sSaveMsgFilename, "OEMSubject")
                    AlternateDataStreams.ADSFile.Write(m_olMailItem.SentOn.ToShortDateString, sSaveMsgFilename, "OEMDate")
                    AlternateDataStreams.ADSFile.Write(NullSafeString(m_olMailItem.Body.Substring(0, 500)), sSaveMsgFilename, "OEMBody")
                    AlternateDataStreams.ADSFile.Write(NullSafeString(m_olMailItem.CC), sSaveMsgFilename, "OEMCc")
                    AlternateDataStreams.ADSFile.Write(m_olMailItem.Attachments.Count.ToString, sSaveMsgFilename, "OEMAttCount")
                Catch ex As Exception

                End Try
            End If

            Me.Close()

        Catch ex As System.Exception
            MsgBox(ex.Message & vbCrLf & "Please check you have selected the correct location type (File system folder or Outlook folder) for the selected save location. Also check you have write permissions on the target folder and also that you have permission to move emails in Outlook", MsgBoxStyle.Critical)
            'Me.DialogResult = DialogResult.Abort
            'Exit Sub
        End Try
    End Sub

    Private Sub BatchFile()
        Me.ToolStripStatusLabel1.Visible = False
        Me.ToolStripProgressBar1.Visible = True

        Me.ToolStripProgressBar1.Maximum = m_olSelection.Count
        Me.ToolStripProgressBar1.Value = 0

        For Each m_olItem In m_olSelection
            m_olMailItem = m_olItem

            Dim sSubject As String = String.Empty
            If Not m_olMailItem.Subject Is Nothing Then sSubject = m_olMailItem.Subject.ToString

            Dim sSaveMsgFilename As String = Me.lblSavePath.Text & ParseFilename(m_olMailItem.SentOn, m_olMailItem.SenderName, m_olMailItem.Subject, bSentMail, Me.MaskedTextBox1.Text, Me.chkPrivate.Checked)

            If sSaveMsgFilename.EndsWith(".msg") = False Then
                sSaveMsgFilename = sSaveMsgFilename & ".msg"
            End If

            Try
                'first check the new msg filename dones't already exist
                If My.Computer.FileSystem.FileExists(sSaveMsgFilename) = True Then
                    sSaveMsgFilename = sSaveMsgFilename.Replace(".msg", " [filed - " & ShortYear() & ShortMonth1() & ShortDay() & "_" & DateTime.Now.ToShortTimeString.Replace(":", "-") & "].msg")
                End If

                m_olMailItem.SaveAs(sSaveMsgFilename, Microsoft.Office.Interop.Outlook.OlSaveAsType.olMSG)

                If Me.rdoArchiveRetain.Checked = True Then
                    m_olMailItem.Categories = cSettings.ArchivedAndRetainedCategory
                    m_olMailItem.Save()
                Else
                    Try
                        m_olMailItem.Move(emDeletedItems)
                    Catch
                        m_olMailItem.Move(olDeletedItems)
                    End Try
                End If

                'now save the ADS data.
                If cSettings.SaveADS = True Then
                    Try
                        AlternateDataStreams.ADSFile.Write(NullSafeString(m_olMailItem.SenderName), sSaveMsgFilename, "OEMFrom")
                        AlternateDataStreams.ADSFile.Write(NullSafeString(m_olMailItem.To), sSaveMsgFilename, "OEMTo")
                        AlternateDataStreams.ADSFile.Write(NullSafeString(m_olMailItem.Subject), sSaveMsgFilename, "OEMSubject")
                        AlternateDataStreams.ADSFile.Write(m_olMailItem.SentOn.ToShortDateString, sSaveMsgFilename, "OEMDate")
                        AlternateDataStreams.ADSFile.Write(NullSafeString(m_olMailItem.Body.Substring(0, 500)), sSaveMsgFilename, "OEMBody")
                        AlternateDataStreams.ADSFile.Write(NullSafeString(m_olMailItem.CC), sSaveMsgFilename, "OEMCc")
                        AlternateDataStreams.ADSFile.Write(m_olMailItem.Attachments.Count.ToString, sSaveMsgFilename, "OEMAttCount")
                    Catch ex As Exception

                    End Try
                End If

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

    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        Me.MaskedTextBox1.Text = Me.QuoteNo.ToString
        ValidateForm()
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        With Me.FolderBrowserDialog1
            .ShowNewFolderButton = False
            .SelectedPath = cSettings.Form2BrowseQuote
            '.RootFolder = sPath
            .Description = "Select foldername to get quote details"

            If .ShowDialog = DialogResult.OK Then
                Dim dirInfo As DirectoryInfo = New DirectoryInfo(.SelectedPath)
                Me.lblQuote.Text = dirInfo.Name
                cSettings.Form2BrowseQuote = .SelectedPath
                cSettings.SaveSettings()
                Try

                    Dim expression As New Text.RegularExpressions.Regex("(?<QUOTE>\d{6})")
                    Dim matches As Text.RegularExpressions.MatchCollection = expression.Matches(Me.lblQuote.Text)
                    For Each match In matches
                        'Console.WriteLine(match.groups("QUOTE").value)
                        Me.QuoteNo = CInt(match.groups("QUOTE").value)
                        Me.btnSelect.Enabled = True
                    Next

                    'Me.QuoteNo = Me.lblQuote.Text.Substring(4, 6)

                Catch ex As Exception
                    Me.btnSelect.Enabled = False
                End Try
            End If


        End With
    End Sub

#Region "UI Code"
    Private Sub UpdateFileName()
        If bBatchFile = False Then
            Me.lblFilename.Text = ParseFilename(m_olMailItem.SentOn, m_olMailItem.SenderName, Me.txtSubject.Text, bSentMail, Me.MaskedTextBox1.Text, Me.chkPrivate.Checked)
        Else
            Me.lblFilename.Text = "batch file mode"
        End If
    End Sub

    Private Sub ValidateForm()
        Me.btnFileMessage.Enabled = False

        If Trim(Me.txtSubject.Text) <> String.Empty And Me.MaskedTextBox1.MaskCompleted = True Then
            Me.btnFileMessage.Enabled = True
        End If
    End Sub

    Private Sub MaskedTextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MaskedTextBox1.TextChanged
        UpdateFileName()
        ValidateForm()
    End Sub

    Private Sub txtSubject_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSubject.TextChanged
        UpdateFileName()
        ValidateForm()
    End Sub

    Private Sub chkPrivate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPrivate.CheckedChanged
        UpdateFileName()
        ValidateForm()
    End Sub
#End Region

End Class