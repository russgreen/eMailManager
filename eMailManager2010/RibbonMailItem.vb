Imports Microsoft.Office.Tools.Ribbon

'Imports Outlook = Microsoft.Office.Interop.Outlook
Imports Office = Microsoft.Office.Core
Imports System.Diagnostics
Imports System.Windows.Forms

Imports Microsoft.Win32

Public Class RibbonMailItem
    Private LocRM As New Resources.ResourceManager("eMailManager.ResourceStrings", Me.GetType.Assembly)

    'Dim m_olMailItem As Outlook.MailItem

    Private Sub Ribbon1_Load(ByVal sender As System.Object, ByVal e As RibbonUIEventArgs) Handles MyBase.Load
        Me.Button1.Label = LocRM.GetString("MenuFileMessage")
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs) Handles Button1.Click
        Try
            'If cSettings.MainForm = 0 Then
            'use default form
            Dim frm As New frmMain()
                With frm
                .m_olMailItem = CType(Globals.ThisAddIn.Application.ActiveInspector.CurrentItem, Outlook.MailItem)
                .bBatchFile = False
                .bSentMail = False

                    Globals.ThisAddIn.Application.ActiveInspector.Close(Outlook.OlInspectorClose.olDiscard)

                    .ShowDialog()
                    .Dispose()
                End With

            'ElseIf cSettings.MainForm = 1 Then
            '    ''use SCD form
            '    'Dim frm As New frmMain2()
            '    'With frm
            '    '    .m_olMailItem = Globals.ThisAddIn.Application.ActiveInspector.CurrentItem
            '    '    .bBatchFile = False
            '    '    .bSentMail = False

            '    '    Globals.ThisAddIn.Application.ActiveInspector.Close(Outlook.OlInspectorClose.olDiscard)

            '    '    .ShowDialog()
            '    '    .Dispose()
            '    'End With
            'End If

        Catch ex As System.Exception

        End Try
    End Sub

End Class
