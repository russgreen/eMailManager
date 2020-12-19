Imports Microsoft.Office.Tools.Ribbon

Imports Office = Microsoft.Office.Core
Imports System.Diagnostics
Imports System.Windows.Forms

Imports Microsoft.Win32

Public Class RibbonExplorer
    Private LocRM As New Resources.ResourceManager("eMailManager.ResourceStrings", Me.GetType.Assembly)

    Dim m_olMailItem As Outlook.MailItem

    Private Sub RibbonExplorer_Load(ByVal sender As System.Object, ByVal e As RibbonUIEventArgs) Handles MyBase.Load
        Me.btnFileMessage.Label = LocRM.GetString("MenuFileMessage")
        Me.btnFileFolder.Label = LocRM.GetString("MenuFileFolder")
        Me.btnSearch.Label = LocRM.GetString("MenuSearch")
        Me.btnSettings.Label = LocRM.GetString("MenuSettings")
        Me.btnAbout.Label = LocRM.GetString("MenuAbout")
        Me.btnUpdate.Label = LocRM.GetString("MenuUpdate")
    End Sub


    Private Sub btnFileMessage_Click(ByVal sender As System.Object, ByVal e As Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs) Handles btnFileMessage.Click
        Dim mOlSelectedItem As Outlook.MailItem

        If Globals.ThisAddIn.Application.ActiveExplorer.Selection.Count = 0 Then
            MessageBox.Show(LocRM.GetString("Message009")) '"Please select the item that you want to save."
            Exit Sub

        ElseIf Globals.ThisAddIn.Application.ActiveExplorer.Selection.Count = 1 Then
            Try
                mOlSelectedItem = CType(Globals.ThisAddIn.Application.ActiveExplorer.Selection.Item(1), Outlook.MailItem)

                Dim frm As New frmMain()
                With frm
                    .m_olMailItem = mOlSelectedItem
                    .bBatchFile = False
                    .bSentMail = False
                    .ShowDialog()
                    .Dispose()
                End With

            Catch ex As System.Exception

            End Try

        ElseIf Globals.ThisAddIn.Application.ActiveExplorer.Selection.Count > 1 Then
            Try

                Dim frm As New frmMain()
                With frm
                        .m_olSelection = Globals.ThisAddIn.Application.ActiveExplorer.Selection
                        .bBatchFile = True
                        .bSentMail = False
                        .ShowDialog()
                        .Dispose()
                    End With

            Catch ex As System.Exception

            End Try
        End If
    End Sub

    Private Sub btnAbout_Click(ByVal sender As System.Object, ByVal e As Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs) Handles btnAbout.Click
        CallAboutDialog()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs) Handles btnSearch.Click
        CallSearchDialog()
    End Sub

    Private Sub btnSettings_Click(ByVal sender As System.Object, ByVal e As Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs) Handles btnSettings.Click
        CallSettingsDialog()
    End Sub

    Private Sub btnFileFolder_Click(ByVal sender As System.Object, ByVal e As Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs) Handles btnFileFolder.Click
        ' check if we have a mail folder
        If Globals.ThisAddIn.Application.ActiveExplorer.CurrentFolder.DefaultItemType = Outlook.OlItemType.olMailItem Then
            CallFolderSaveDialog(Globals.ThisAddIn.Application.ActiveExplorer.CurrentFolder)
        End If
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As RibbonControlEventArgs) Handles btnUpdate.Click
        Try
            Dim sAppPath As String = CType(My.Computer.Registry.CurrentUser.OpenSubKey("Software\archisoft\eMail Manager", True).GetValue("Path"), String)
            If sAppPath.EndsWith("\") Then
                sAppPath = sAppPath
            Else
                sAppPath = sAppPath & "\"
            End If
            Process.Start(sAppPath & "updater.exe")
        Catch ex As Exception

        End Try

    End Sub
End Class
