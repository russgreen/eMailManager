'Imports System.Security.Cryptography
Imports Microsoft.Win32
Imports System.Diagnostics
Imports System.Windows.Forms

Module modCommon

    Public LocRM As New Resources.ResourceManager("eMailManager.ResourceStrings", GetType(ThisAddIn).Assembly)

    'Public IsUnlicensed As Boolean = False

    'Public passPhrase As String = "Pas5pr@se"
    'Public saltValue As String = "s@1tValue"          ' can be any string
    'Public hashAlgorithm As String = "SHA1"           ' can be "MD5"
    'Public passwordIterations As Integer = 2          ' can be any number
    'Public initVector As String = "@1B2c3D4e5F6g7H8"  ' must be 16 bytes
    'Public keySize As Integer = 256                   ' can be 192 or 128


    Public mapiNamespace As Outlook.NameSpace = Globals.ThisAddIn.Application.GetNamespace("MAPI")
    Public sDeletedItems As String = "Deleted Items (eMail Manager)"
    Public emDeletedItems As Outlook.Folder
    'Dim mapiNamespace As Outlook.NameSpace = Globals.ThisAddIn.Application.GetNamespace("MAPI")
    Public olInbox As Outlook.Folder = TryCast(mapiNamespace.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox), Outlook.Folder)
    Public olDeletedItems As Outlook.Folder = TryCast(mapiNamespace.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderDeletedItems), Outlook.Folder)

    Public cSettings As clsSettings

    Public MaxFile As Integer = 255

    Private SaveFolder As Outlook.Folder

    Public m_olLastItem As Outlook.MailItem

    Public Sub ClearDeletedItems() 'ByRef DeletedItemsFolder As Outlook.Folder
        'first we get the inbox and the parent folder
        Dim myRoot As Outlook.Folder
        myRoot = CType(olInbox.Parent, Outlook.Folder)

        'try to get the new deleted items folder
        emDeletedItems = myRoot.Folders(sDeletedItems)
        Dim iCount As Integer = emDeletedItems.Items.Count

        If iCount > 0 Then
            Dim clearitems As Boolean = cSettings.AlwaysClearDeletedItems

            If cSettings.AlwaysClearDeletedItems = False Then
                If MsgBox(LocRM.GetString("Message002"), MsgBoxStyle.YesNo) = Windows.Forms.DialogResult.Yes Then
                    clearitems = True
                End If
            End If

            If clearitems = True Then
                For i As Integer = iCount To 1 Step -1
                    On Error Resume Next
                    Dim em As Outlook.MailItem
                    em = CType(emDeletedItems.Items.Item(i), Outlook.MailItem)
                    em.Save()
                    'em.Move(olDeletedItems)
                    em.Delete()
                Next (i)
            End If
        End If
    End Sub

    Public Sub CallFolderSaveDialog(ByRef Folder As Outlook.Folder)
        SaveFolder = Folder

        Dim workerThread As Threading.Thread = New Threading.Thread(AddressOf bw_DoWork)

        With workerThread
            .CurrentCulture = Threading.Thread.CurrentThread.CurrentCulture
            .CurrentUICulture = Threading.Thread.CurrentThread.CurrentUICulture
            .IsBackground = True
            .SetApartmentState(Threading.ApartmentState.STA)
            .Start()
        End With
    End Sub

    Private Sub bw_DoWork()
        Dim frm As New frmFolderSave
        With frm
            .m_olFolder = SaveFolder
            .ShowDialog()
            .Dispose()
        End With
    End Sub

    Public Sub CallSearchDialog()
        Dim frm1 As New frmSearch
        With frm1
            .Show()
            '.Dispose()
        End With
    End Sub

    Public Sub CallAboutDialog()
        Dim frm As New frmAbout
        With frm
            .ShowDialog()
            .Dispose()
        End With
    End Sub

    Public Sub CallSettingsDialog()
        Dim frm As New frmSettings
        With frm
            .FileNameFilterBox1.FileNameFilter = cSettings.FilenameFilter
            .FileNameFilterBox1.DefaultFileNameFilter = cSettings.FilenameFilter


            If cSettings.MessageSaveAsType = 3 Then
                .cboFileFormat.SelectedIndex = 0
            ElseIf cSettings.MessageSaveAsType = 0 Then
                .cboFileFormat.SelectedIndex = 1
            End If

            '.txtSavePath.Text = cSettings.Form2SavePath

            If .cboCategory.Items.IndexOf(cSettings.ArchivedAndRetainedCategory) = -1 Then
                .cboCategory.SelectedIndex = .cboCategory.Items.IndexOf("Red category")
            Else
                .cboCategory.SelectedIndex = .cboCategory.Items.IndexOf(cSettings.ArchivedAndRetainedCategory)
            End If

            '.chkADS.Checked = cSettings.SaveADS
            .chkLeaveCopy.Checked = cSettings.LeaveCopy
            .chkAlwaysEmbedAttachments.Checked = cSettings.AlwaysEmbedAttachments
            .chkAlwaysClearDeletedItems.Checked = cSettings.AlwaysClearDeletedItems
            .chkMonitorSentItems.Checked = cSettings.MonitorSentItems

            .chkAutoArchiveIn.Checked = cSettings.AutoArchiveIn
            .chkAutoArchiveOut.Checked = cSettings.AutoArchiveOut
            .txtAutoArchiveIn.Text = cSettings.AutoArchiveInPath
            .txtAutoArchiveOut.Text = cSettings.AutoArchiveOutPath

            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                cSettings.FilenameFilter = .FileNameFilterBox1.FileNameFilter

                If .cboFileFormat.SelectedIndex = 0 Then
                    cSettings.MessageSaveAsType = 3
                ElseIf .cboFileFormat.SelectedIndex = 1 Then
                    cSettings.MessageSaveAsType = 0
                End If

                'cSettings.Form2SavePath = .txtSavePath.Text
                cSettings.ArchivedAndRetainedCategory = .cboCategory.SelectedItem.ToString
                'cSettings.SaveADS = .chkADS.Checked
                cSettings.LeaveCopy = .chkLeaveCopy.Checked
                cSettings.AlwaysEmbedAttachments = .chkAlwaysEmbedAttachments.Checked
                cSettings.AlwaysClearDeletedItems = .chkAlwaysClearDeletedItems.Checked
                cSettings.MonitorSentItems = .chkMonitorSentItems.Checked

                cSettings.AutoArchiveIn = .chkAutoArchiveIn.Checked
                cSettings.AutoArchiveOut = .chkAutoArchiveOut.Checked
                cSettings.AutoArchiveInPath = .txtAutoArchiveIn.Text
                cSettings.AutoArchiveOutPath = .txtAutoArchiveOut.Text

                cSettings.SaveSettings()
                cSettings.LoadSettings()

            End If

            .Dispose()
        End With
    End Sub

    Public Function TrimLongFileName(ByRef strValue As String) As String
        Dim retval As String = strValue

        If strValue.Length >= MaxFile Then
            retval = strValue.Substring(0, (MaxFile - 4)) & cSettings.MessageFileExt
        End If

        Return retval
    End Function

    Public Function getImage(ByRef icon As System.Drawing.Icon) As stdole.IPictureDisp
        Dim tempImage As stdole.IPictureDisp = Nothing
        Try
            Dim newIcon As System.Drawing.Icon = icon
            Dim newImageList As New System.Windows.Forms.ImageList
            newImageList.Images.Add(newIcon)
            tempImage = ConvertImage.Convert(newImageList.Images(0))
        Catch ex As Exception
            addToLog(ex)
        End Try
        Return tempImage
    End Function

    Public Function IsGuidWithOptionalBraces(ByRef strValue As String) As Boolean
        If String.IsNullOrEmpty(strValue) Then
            Return False
        End If
        Return System.Text.RegularExpressions.Regex.IsMatch(strValue, "^[\{]?[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}[\}]?$", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
    End Function

    Public Sub Delay(ByVal dblSecs As Double)

        Const OneSec As Double = 1.0# / (1440.0# * 60.0#)
        Dim dblWaitTil As Date
        Now.AddSeconds(OneSec)
        dblWaitTil = Now.AddSeconds(OneSec).AddSeconds(dblSecs)
        Do Until Now > dblWaitTil
            Application.DoEvents()
        Loop

    End Sub


    ''' <summary>
    ''' Called from SCD form.  DO NOT EDIT
    ''' </summary>
    ''' <param name="Sent"></param>
    ''' <param name="From"></param>
    ''' <param name="Subject"></param>
    ''' <param name="SentMail"></param>
    ''' <param name="Ref"></param>
    ''' <param name="Pvt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ParseFilename(ByVal Sent As DateTime, ByVal From As String, ByVal Subject As String, ByVal SentMail As Boolean, Optional ByVal Ref As String = Nothing, Optional ByVal Pvt As Boolean = False) As String
        Dim retval As String
        '<sent>
        '<from>
        '<subject>
        '<ref>
        '<type> - in or out
        '<pvt> - private or open

        Dim Type As String
        If SentMail = True Then
            Type = "out"
        Else
            Type = "in"
        End If

        retval = cSettings.FilenameFilter
        retval = retval.Replace("<sent>", Sent.Year.ToString & "-" & modDateTime.ShortMonth3(Sent.Month.ToString) & "-" & modDateTime.ShortDay1(Sent.Day.ToString) & "_" & RemoveIllegalCharacters(Sent.ToShortTimeString))
        retval = retval.Replace("<from>", From)
        retval = retval.Replace("<subject>", RemoveIllegalCharacters(Subject))
        retval = retval.Replace("<ref>", Ref)
        retval = retval.Replace("<type>", Type)
        retval = retval.Replace("<TYPE>", Type.ToUpper)

        If Pvt = True Then
            retval = retval.Replace("<pvt>", "Private")
        Else
            retval = retval.Replace("<pvt>", "Open")
        End If



        Return RemoveIllegalCharacters(retval)
    End Function

    Public Function ParseFilename(ByVal FilenameFilter As String, ByRef em As Outlook.MailItem, ByVal SentMail As Boolean, _
                               Optional ByVal Ref As String = Nothing, _
                               Optional ByVal Pvt As Boolean = False) As String
        Dim retval As String
        '<sent> - date and time the message was sent in reverse format for chronological saving e.g.  yyyy-mm-dd_hh-mm 
        '<sent_yy> - year the message was sent
        '<sent_yyyy> - year the message was sent
        '<sent_mm> - month the message was sent
        '<sent_dd> - day in the month message was sent
        '<sent_hh-mm> - time the message was sent
        '<sent_hh.mm> - time the message was sent
        '<sent_hh-mm12> - time the message was sent in 12 hour format
        '<sent_hh.mm12> - time the message was sent in 12 hour format
        '<sent_hhmmss> - time the messages was sent in 24 hour hour HHMMSS
        '<from> - who the message was sent by (display name)
        '<from_email> - who the message was sent by (real email address). If the email address is an Exchange email address (sender on same local Exchange server) then the display name is used instead.
        '<from_domain> - domain name of sender.
        '<subject> - subject line of the message 
        '<$[XX]subject> - subject line of the message where nn is the length of the subject line to use in the filename.  nn must be 2 digits, e.g. 09, 20
        '<ref>
        '<type> - direction of the message incoming = in or outgoing = out - NOTE: email manager only detects outgoing messages if they are filed as they are sent.
        '<TYPE> - as above but in uppercase 
        '<type_sr>
        '<pvt> - private or open

        Dim sSubject As String = String.Empty
        If Not em.Subject Is Nothing Then sSubject = em.Subject.ToString

        retval = FilenameFilter

        'handle dates
        retval = retval.Replace("<sent>", em.SentOn.Year.ToString & "-" & modDateTime.ShortMonth3(em.SentOn.Month.ToString) & "-" & modDateTime.ShortDay1(em.SentOn.Day.ToString) & "_" & RemoveIllegalCharacters(em.SentOn.ToShortTimeString))
        retval = retval.Replace("<sent_yy>", modDateTime.ShortYear2(em.SentOn.Year.ToString))
        retval = retval.Replace("<sent_yyyy>", em.SentOn.Year.ToString)
        retval = retval.Replace("<sent_mm>", modDateTime.ShortMonth3(em.SentOn.Month.ToString))
        retval = retval.Replace("<sent_dd>", modDateTime.ShortDay1(em.SentOn.Day.ToString))
        retval = retval.Replace("<sent_hh-mm>", RemoveIllegalCharacters(em.SentOn.ToShortTimeString))
        retval = retval.Replace("<sent_hh.mm>", RemoveIllegalCharacters(em.SentOn.ToShortTimeString).Replace("-", "."))
        retval = retval.Replace("<sent_hh-mm12>", RemoveIllegalCharacters(em.SentOn.ToString("h:mm tt")))
        retval = retval.Replace("<sent_hh.mm12>", RemoveIllegalCharacters(em.SentOn.ToString("h:mm tt")).Replace("-", "."))
        retval = retval.Replace("<sent_hhmmss>", RemoveIllegalCharacters(em.SentOn.ToString("HHmmss"))) 'em.SentOn.Hour & em.SentOn.Minute & em.SentOn.Second)

        'handle subject information
        Dim sSafeSubject As String = Trim(RemoveIllegalCharacters(NullSubject(sSubject)))
        retval = retval.Replace("<subject>", sSafeSubject)

        'does the filename filter contain a short subject variable?
        If FilenameFilter.Contains("<$[") Then
            Dim iStart As Integer = FilenameFilter.IndexOf("<$[")
            Dim iEnd As Integer = FilenameFilter.IndexOf("]subject>")

            Dim sLength As String = FilenameFilter.Substring(iStart + 3, 2)
            Dim iLength As Integer = CInt(sLength)

            Dim iSubjectLength As Integer = sSafeSubject.Length
            If iLength > iSubjectLength Then iLength = iSubjectLength

            Dim sShortSubject As String = sSafeSubject.Substring(0, iLength)

            retval = retval.Replace("<$[" & sLength & "]subject>", sShortSubject)

        End If

        'handle sender information
        retval = retval.Replace("<from>", em.SenderName)

        If em.SenderEmailType = "EX" Then
            'we must have an exchange server address so use SenderName
            Dim recip As Outlook.Recipient = mapiNamespace.CreateRecipient(em.SenderEmailAddress)
            Dim exUser As Outlook.ExchangeUser = recip.AddressEntry.GetExchangeUser()

            Try
                retval = retval.Replace("<from_email>", exUser.PrimarySmtpAddress)
            Catch ex As Exception
                retval = retval.Replace("<from_email>", recip.Name)
            End Try

        Else
            retval = retval.Replace("<from_email>", em.SenderEmailAddress)
        End If


        'handle not automatic fields
        retval = retval.Replace("<ref>", Ref)

        'handle direction of email
        Dim Type, TypeSR As String
        If SentMail = True Then
            Type = "out"
            TypeSR = "S"
        Else
            'check if account email matches mail item sender
            If Globals.ThisAddIn.Application.Session.CurrentUser.Name = em.SenderName Then
                'we match so this must be a sent item
                Type = "out"
                TypeSR = "S"
            Else
                Type = "in"
                TypeSR = "R"
            End If
        End If

        retval = retval.Replace("<from_domain>", GetDomainFromEmail(em))

        retval = retval.Replace("<type>", Type)
        retval = retval.Replace("<TYPE>", Type.ToUpper)
        retval = retval.Replace("<type_sr>", TypeSR)

        If Pvt = True Then
            retval = retval.Replace("<pvt>", "Private")
        Else
            retval = retval.Replace("<pvt>", "Open")
        End If

        Return RemoveIllegalCharacters(retval)
    End Function

    Private Function GetDomainFromEmail(ByRef o_Mail As Outlook.MailItem) As String
        On Error Resume Next

        Dim retval As String = String.Empty
        Dim smtp As String = String.Empty

        If o_Mail.SenderEmailType = "EX" Then
            'we must have an exchange server address so use SenderName
            Dim recip As Outlook.Recipient = mapiNamespace.CreateRecipient(o_Mail.SenderEmailAddress)
            Dim exUser As Outlook.ExchangeUser = recip.AddressEntry.GetExchangeUser()

            smtp = exUser.PrimarySmtpAddress
        Else
            smtp = o_Mail.SenderEmailAddress
        End If

        'extract domain name from senders email address
        retval = smtp.Substring(smtp.IndexOf("@") + 1)

        Return retval
    End Function


    ''' <summary>
    ''' Strip vbCrLf from a text string and return the reformatted string
    ''' </summary>
    Public Function StripVbCrLf(ByVal sStart As String) As String
        'this function will remove vbCrLf 
        'from sString and replace them a |
        Dim sLegalString As String

        sLegalString = Replace(sStart, vbCrLf, " | ")

        StripVbCrLf = sLegalString
    End Function

    ''' <summary>
    ''' Strip spaces from a text string and return the reformatted string
    ''' </summary>
    Public Function StripSpaces(ByVal sStart As String) As String
        'this function will remove vbCrLf 
        'from sString and replace them a |
        Dim sLegalString As String

        sLegalString = Replace(sStart, " ", "")

        StripSpaces = sLegalString
    End Function

    ''' <summary>
    ''' Strip illegal characters from a text string and return the reformatted string
    ''' </summary>
    Public Function RemoveIllegalSourceCharacters(ByVal sStart As String) As String
        'this function will remove these characters \ / : * ? < > | _'
        'from sString and replace them a space
        Dim sLegalString As String

        If (sStart Is DBNull.Value) OrElse (sStart Is Nothing) _
                         OrElse (sStart Is String.Empty) Then
            sLegalString = " "
        Else
            sLegalString = sStart
        End If

        sLegalString = Replace(sLegalString, "\", " ")
        sLegalString = Replace(sLegalString, "/", " ")
        sLegalString = Replace(sLegalString, ":", " ")
        sLegalString = Replace(sLegalString, "*", " ")
        sLegalString = Replace(sLegalString, "?", " ")
        'sLegalString = Replace(sLegalString, """", " ")
        sLegalString = Replace(sLegalString, "<", " ")
        sLegalString = Replace(sLegalString, ">", " ")
        sLegalString = Replace(sLegalString, "|", " ")
        sLegalString = Replace(sLegalString, "_", " ")
        sLegalString = Replace(sLegalString, "'", "")
        'sLegalString = Replace(sLegalString, ".", " ")
        sLegalString = Replace(sLegalString, ".msg", "")

        RemoveIllegalSourceCharacters = sLegalString
    End Function

    Public Function RemoveIllegalCharacters(ByVal sStart As String) As String
        'this function will remove these characters \ / : * ? < > | 
        'from sString and replace them a space
        Dim sLegalString As String = sStart

        sLegalString = Replace(sLegalString, "\", " ")
        sLegalString = Replace(sLegalString, "/", " ")
        sLegalString = Replace(sLegalString, ":", "-")
        sLegalString = Replace(sLegalString, "*", " ")
        sLegalString = Replace(sLegalString, "?", " ")
        sLegalString = Replace(sLegalString, """", " ")
        sLegalString = Replace(sLegalString, "<", " ")
        sLegalString = Replace(sLegalString, ">", " ")
        sLegalString = Replace(sLegalString, "|", " ")

        RemoveIllegalCharacters = sLegalString
    End Function


    'Public Sub WriteADS(ByRef m_olmailitem As Outlook.MailItem, ByVal sSaveMsgFilename As String)
    '    On Error Resume Next

    '    AlternateDataStreams.ADSFile.Write(NullSafeString(m_olmailitem.SenderName), sSaveMsgFilename, "OEMFrom")
    '    AlternateDataStreams.ADSFile.Write(NullSafeString(m_olmailitem.To), sSaveMsgFilename, "OEMTo")
    '    AlternateDataStreams.ADSFile.Write(NullSafeString(NullSubject(m_olmailitem.Subject)), sSaveMsgFilename, "OEMSubject")
    '    AlternateDataStreams.ADSFile.Write(m_olmailitem.SentOn.ToShortDateString, sSaveMsgFilename, "OEMDate")
    '    AlternateDataStreams.ADSFile.Write(NullSafeString(m_olmailitem.Body.Substring(0, 500)), sSaveMsgFilename, "OEMBody")
    '    AlternateDataStreams.ADSFile.Write(NullSafeString(m_olmailitem.CC), sSaveMsgFilename, "OEMCc")
    '    AlternateDataStreams.ADSFile.Write(m_olmailitem.Attachments.Count.ToString, sSaveMsgFilename, "OEMAttCount")

    '    'TODO: write extended data
    '    'http://www.robelle.com/smugbook/ascii.html
    '    'Dim sb As New StringBuilder
    '    'sb.Append(Chr(4) & Chr(0) & Chr(0) & "Å" & Chr(0) & Chr(0) & Chr(0) & "1SPS0ñ%·ïG" & Chr(26) & Chr(16) & "¥ñ" & Chr(2) & "`Œžë¬©" & Chr(0) & Chr(0) & Chr(0))
    '    'AlternateDataStreams.ADSFile.Write(sb.ToString, sSaveMsgFilename, "OECustomProperty")
    'End Sub

    Public Function NullSubject(ByVal subject As String) As String
        Dim retval As String

        If Trim(subject).Length = 0 Then
            retval = "RE"
        Else
            retval = Trim(subject)
        End If

        Return retval
    End Function

#Region "  Error handling  "
    Public Sub addToLog(ByVal err As Exception)
        On Error Resume Next

        Dim errFolder As String = My.Computer.FileSystem.SpecialDirectories.Desktop

        Dim sLogFile As String = errFolder & "\emailmanager.log"
        If My.Computer.FileSystem.FileExists(sLogFile) = False Then
            My.Computer.FileSystem.WriteAllText(sLogFile, String.Empty, False)
        End If

        'build the log text
        Dim sb As New StringBuilder
        sb.Append("Message : " & err.Message & Environment.NewLine)
        sb.Append("Source : " & err.Source & Environment.NewLine)
        sb.Append("StackTrace : " & err.StackTrace & Environment.NewLine)
        'sb.Append("Method : " & err.TargetSit.ToString & Environment.NewLine)
        sb.Append("Date/Time : " & DateTime.Now.ToString & Environment.NewLine)
        sb.Append("================================================" & Environment.NewLine)

        My.Computer.FileSystem.WriteAllText(sLogFile, sb.ToString, True)

        'Process.Start(sLogFile)
    End Sub


    '****************************************************************
    ' NullSafeString
    '****************************************************************
    Public Function NullSafeString(ByVal arg As Object, _
        Optional ByVal returnIfEmpty As String = "") As String

        Dim returnValue As String

        If (arg Is DBNull.Value) OrElse (arg Is Nothing) _
                        OrElse (arg.ToString.Trim Is Nothing) _
                         OrElse (arg.ToString.Trim Is String.Empty) _
                         OrElse (arg.ToString.Trim Is "") Then
            returnValue = returnIfEmpty
        Else
            Try
                returnValue = CStr(arg).Trim
            Catch
                returnValue = returnIfEmpty
            End Try

        End If

        Return returnValue

    End Function

    '****************************************************************
    ' NullSafeInteger
    '****************************************************************
    Public Function NullSafeInteger(ByVal arg As Object, _
      Optional ByVal returnIfEmpty As Integer = 0) As Integer

        Dim returnValue As Integer

        If (arg Is DBNull.Value) OrElse (arg Is Nothing) _
                         OrElse (arg Is String.Empty) Then
            returnValue = returnIfEmpty
        Else
            Try
                returnValue = CInt(arg)
            Catch
                returnValue = returnIfEmpty
            End Try
        End If

        Return returnValue

    End Function

    '****************************************************************
    ' NullSafeLong
    '****************************************************************
    Public Function NullSafeLong(ByVal arg As Object, _
      Optional ByVal returnIfEmpty As Long = 0) As Long

        Dim returnValue As Long

        If (arg Is DBNull.Value) OrElse (arg Is Nothing) _
                         OrElse (arg Is String.Empty) Then
            returnValue = returnIfEmpty
        Else
            Try
                returnValue = CLng(arg)
            Catch
                returnValue = returnIfEmpty
            End Try
        End If

        Return returnValue

    End Function

    '****************************************************************
    '   NullSafeDouble
    '****************************************************************
    Public Function NullSafeDouble(ByVal arg As Object, _
      Optional ByVal returnIfEmpty As Integer = 0) As Double

        Dim returnValue As Double

        If (arg Is DBNull.Value) OrElse (arg Is Nothing) _
                         OrElse (arg Is String.Empty) _
                         OrElse (Trim(arg.ToString) Is ".") Then
            returnValue = returnIfEmpty
        Else
            Try
                returnValue = CDbl(arg)
            Catch
                returnValue = returnIfEmpty
            End Try
        End If

        Return returnValue

    End Function

    '****************************************************************
    ' NullSafeBoolean
    '****************************************************************
    Public Function NullSafeBoolean(ByVal arg As Object, Optional ByVal NullVal As Boolean = False) As Boolean

        Dim returnValue As Boolean

        If (arg Is DBNull.Value) OrElse (arg Is Nothing) OrElse (arg Is String.Empty) Then
            returnValue = NullVal
        Else
            Try
                returnValue = CBool(arg)
            Catch
                returnValue = False
            End Try
        End If

        Return returnValue

    End Function


    '****************************************************************
    ' NullSafeDate
    '****************************************************************
    Public Function NullSafeDate(ByVal arg As Object) As Date

        Dim returnValue As Date

        If (arg Is DBNull.Value) OrElse (arg Is Nothing) _
                         OrElse (arg Is String.Empty) Then
            returnValue = Date.Now
        Else
            Try
                returnValue = CDate(arg)
                If returnValue < CDate("1 / 1 / 1753") Then
                    returnValue = CDate("1 / 1 / 1753")
                End If
            Catch
                returnValue = Date.Now
            End Try
        End If

        Return returnValue

    End Function



#End Region


    '' <summary>
    '' Encrypts specified plaintext using Rijndael symmetric key algorithm
    '' and returns a base64-encoded result.
    '' </summary>
    '' <param name="plainText">
    '' Plaintext value to be encrypted.
    '' </param>
    '' <param name="passPhrase">
    '' Passphrase from which a pseudo-random password will be derived. The 
    '' derived password will be used to generate the encryption key. 
    '' Passphrase can be any string. In this example we assume that this 
    '' passphrase is an ASCII string.
    '' </param>
    '' <param name="saltValue">
    '' Salt value used along with passphrase to generate password. Salt can 
    '' be any string. In this example we assume that salt is an ASCII string.
    '' </param>
    '' <param name="hashAlgorithm">
    '' Hash algorithm used to generate password. Allowed values are: "MD5" and
    '' "SHA1". SHA1 hashes are a bit slower, but more secure than MD5 hashes.
    '' </param>
    '' <param name="passwordIterations">
    '' Number of iterations used to generate password. One or two iterations
    '' should be enough.
    '' </param>
    '' <param name="initVector">
    '' Initialization vector (or IV). This value is required to encrypt the 
    '' first block of plaintext data. For RijndaelManaged class IV must be 
    '' exactly 16 ASCII characters long.
    '' </param>
    '' <param name="keySize">
    '' Size of encryption key in bits. Allowed values are: 128, 192, and 256. 
    '' Longer keys are more secure than shorter keys.
    '' </param>
    '' <returns>
    '' Encrypted value formatted as a base64-encoded string.
    '' </returns>
    'Public Function Encrypt(ByVal plainText As String, _
    '                               ByVal passPhrase As String, _
    '                               ByVal saltValue As String, _
    '                               ByVal hashAlgorithm As String, _
    '                               ByVal passwordIterations As Integer, _
    '                               ByVal initVector As String, _
    '                               ByVal keySize As Integer) _
    '                       As String

    '    ' Convert strings into byte arrays.
    '    ' Let us assume that strings only contain ASCII codes.
    '    ' If strings include Unicode characters, use Unicode, UTF7, or UTF8 
    '    ' encoding.
    '    Dim initVectorBytes As Byte()
    '    initVectorBytes = Encoding.ASCII.GetBytes(initVector)

    '    Dim saltValueBytes As Byte()
    '    saltValueBytes = Encoding.ASCII.GetBytes(saltValue)

    '    ' Convert our plaintext into a byte array.
    '    ' Let us assume that plaintext contains UTF8-encoded characters.
    '    Dim plainTextBytes As Byte()
    '    plainTextBytes = Encoding.UTF8.GetBytes(plainText)

    '    ' First, we must create a password, from which the key will be derived.
    '    ' This password will be generated from the specified passphrase and 
    '    ' salt value. The password will be created using the specified hash 
    '    ' algorithm. Password creation can be done in several iterations.
    '    Dim password As PasswordDeriveBytes
    '    password = New PasswordDeriveBytes(passPhrase, _
    '                                       saltValueBytes, _
    '                                       hashAlgorithm, _
    '                                       passwordIterations)

    '    ' Use the password to generate pseudo-random bytes for the encryption
    '    ' key. Specify the size of the key in bytes (instead of bits).
    '    Dim keyBytes As Byte()
    '    keyBytes = password.GetBytes(CInt(keySize / 8))

    '    ' Create uninitialized Rijndael encryption object.
    '    Dim symmetricKey As RijndaelManaged
    '    symmetricKey = New RijndaelManaged()

    '    ' It is reasonable to set encryption mode to Cipher Block Chaining
    '    ' (CBC). Use default options for other symmetric key parameters.
    '    symmetricKey.Mode = CipherMode.CBC

    '    ' Generate encryptor from the existing key bytes and initialization 
    '    ' vector. Key size will be defined based on the number of the key 
    '    ' bytes.
    '    Dim encryptor As ICryptoTransform
    '    encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes)

    '    ' Define memory stream which will be used to hold encrypted data.
    '    Dim memoryStream As IO.MemoryStream
    '    memoryStream = New IO.MemoryStream()

    '    ' Define cryptographic stream (always use Write mode for encryption).
    '    Dim cryptoStream As CryptoStream
    '    cryptoStream = New CryptoStream(memoryStream, _
    '                                    encryptor, _
    '                                    CryptoStreamMode.Write)
    '    ' Start encrypting.
    '    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length)

    '    ' Finish encrypting.
    '    cryptoStream.FlushFinalBlock()

    '    ' Convert our encrypted data from a memory stream into a byte array.
    '    Dim cipherTextBytes As Byte()
    '    cipherTextBytes = memoryStream.ToArray()

    '    ' Close both streams.
    '    memoryStream.Close()
    '    cryptoStream.Close()

    '    ' Convert encrypted data into a base64-encoded string.
    '    Dim cipherText As String
    '    cipherText = Convert.ToBase64String(cipherTextBytes)

    '    ' Return encrypted string.
    '    Encrypt = cipherText
    'End Function

    '' <summary>
    '' Decrypts specified ciphertext using Rijndael symmetric key algorithm.
    '' </summary>
    '' <param name="cipherText">
    '' Base64-formatted ciphertext value.
    '' </param>
    '' <param name="passPhrase">
    '' Passphrase from which a pseudo-random password will be derived. The 
    '' derived password will be used to generate the encryption key. 
    '' Passphrase can be any string. In this example we assume that this 
    '' passphrase is an ASCII string.
    '' </param>
    '' <param name="saltValue">
    '' Salt value used along with passphrase to generate password. Salt can 
    '' be any string. In this example we assume that salt is an ASCII string.
    '' </param>
    '' <param name="hashAlgorithm">
    '' Hash algorithm used to generate password. Allowed values are: "MD5" and
    '' "SHA1". SHA1 hashes are a bit slower, but more secure than MD5 hashes.
    '' </param>
    '' <param name="passwordIterations">
    '' Number of iterations used to generate password. One or two iterations
    '' should be enough.
    '' </param>
    '' <param name="initVector">
    '' Initialization vector (or IV). This value is required to encrypt the 
    '' first block of plaintext data. For RijndaelManaged class IV must be 
    '' exactly 16 ASCII characters long.
    '' </param>
    '' <param name="keySize">
    '' Size of encryption key in bits. Allowed values are: 128, 192, and 256. 
    '' Longer keys are more secure than shorter keys.
    '' </param>
    '' <returns>
    '' Decrypted string value.
    '' </returns>
    '' <remarks>
    '' Most of the logic in this function is similar to the Encrypt 
    '' logic. In order for decryption to work, all parameters of this function
    '' - except cipherText value - must match the corresponding parameters of 
    '' the Encrypt function which was called to generate the 
    '' ciphertext.
    '' </remarks>
    'Public Function Decrypt(ByVal cipherText As String, _
    '                               ByVal passPhrase As String, _
    '                               ByVal saltValue As String, _
    '                               ByVal hashAlgorithm As String, _
    '                               ByVal passwordIterations As Integer, _
    '                               ByVal initVector As String, _
    '                               ByVal keySize As Integer) _
    '                       As String

    '    ' Convert strings defining encryption key characteristics into byte
    '    ' arrays. Let us assume that strings only contain ASCII codes.
    '    ' If strings include Unicode characters, use Unicode, UTF7, or UTF8
    '    ' encoding.
    '    Dim initVectorBytes As Byte()
    '    initVectorBytes = Encoding.ASCII.GetBytes(initVector)

    '    Dim saltValueBytes As Byte()
    '    saltValueBytes = Encoding.ASCII.GetBytes(saltValue)

    '    ' Convert our ciphertext into a byte array.
    '    Dim cipherTextBytes As Byte()
    '    cipherTextBytes = Convert.FromBase64String(cipherText)

    '    ' First, we must create a password, from which the key will be 
    '    ' derived. This password will be generated from the specified 
    '    ' passphrase and salt value. The password will be created using
    '    ' the specified hash algorithm. Password creation can be done in
    '    ' several iterations.
    '    Dim password As PasswordDeriveBytes
    '    password = New PasswordDeriveBytes(passPhrase, _
    '                                       saltValueBytes, _
    '                                       hashAlgorithm, _
    '                                       passwordIterations)

    '    ' Use the password to generate pseudo-random bytes for the encryption
    '    ' key. Specify the size of the key in bytes (instead of bits).
    '    Dim keyBytes As Byte()
    '    keyBytes = password.GetBytes(CInt(keySize / 8))

    '    ' Create uninitialized Rijndael encryption object.
    '    Dim symmetricKey As RijndaelManaged
    '    symmetricKey = New RijndaelManaged()

    '    ' It is reasonable to set encryption mode to Cipher Block Chaining
    '    ' (CBC). Use default options for other symmetric key parameters.
    '    symmetricKey.Mode = CipherMode.CBC

    '    ' Generate decryptor from the existing key bytes and initialization 
    '    ' vector. Key size will be defined based on the number of the key 
    '    ' bytes.
    '    Dim decryptor As ICryptoTransform
    '    decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes)

    '    ' Define memory stream which will be used to hold encrypted data.
    '    Dim memoryStream As IO.MemoryStream
    '    memoryStream = New IO.MemoryStream(cipherTextBytes)

    '    ' Define memory stream which will be used to hold encrypted data.
    '    Dim cryptoStream As CryptoStream
    '    cryptoStream = New CryptoStream(memoryStream, _
    '                                    decryptor, _
    '                                    CryptoStreamMode.Read)

    '    ' Since at this point we don't know what the size of decrypted data
    '    ' will be, allocate the buffer long enough to hold ciphertext;
    '    ' plaintext is never longer than ciphertext.
    '    Dim plainTextBytes As Byte()
    '    ReDim plainTextBytes(cipherTextBytes.Length)

    '    ' Start decrypting.
    '    Dim decryptedByteCount As Integer
    '    decryptedByteCount = cryptoStream.Read(plainTextBytes, _
    '                                           0, _
    '                                           plainTextBytes.Length)

    '    ' Close both streams.
    '    memoryStream.Close()
    '    cryptoStream.Close()

    '    ' Convert decrypted data into a string. 
    '    ' Let us assume that the original plaintext string was UTF8-encoded.
    '    Dim plainText As String
    '    plainText = Encoding.UTF8.GetString(plainTextBytes, _
    '                                        0, _
    '                                        decryptedByteCount)

    '    ' Return decrypted string.
    '    Decrypt = plainText
    'End Function


End Module
