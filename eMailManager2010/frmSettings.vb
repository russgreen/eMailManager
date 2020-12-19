Imports System.Windows.Forms
Imports Microsoft.Win32

Public Class frmSettings

    Dim ChangedMonitoring As Boolean = False

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        'load categories
        With mapiNamespace.Categories
            For i = 1 To .Count
                Me.cboCategory.Items.Add(.Item(i).Name)
                'lstCategories.AddItem.Item(i)
            Next i
        End With

    End Sub


    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If ChangedMonitoring = True Then MsgBox(LocRM.GetString("Message014"), MsgBoxStyle.Information)
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnAutoArchiveIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAutoArchiveIn.Click
        With Me.FolderBrowserDialog1
            .ShowDialog()
            .ShowNewFolderButton = True
            .RootFolder = Environment.SpecialFolder.MyComputer
            Me.txtAutoArchiveIn.Text = .SelectedPath
            If Not Me.txtAutoArchiveIn.Text.EndsWith("\") Then Me.txtAutoArchiveIn.Text = Me.txtAutoArchiveIn.Text & "\"
        End With
    End Sub

    Private Sub btnAutoArchiveOut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAutoArchiveOut.Click
        With Me.FolderBrowserDialog1
            .ShowDialog()
            .ShowNewFolderButton = True
            .RootFolder = Environment.SpecialFolder.MyComputer
            Me.txtAutoArchiveOut.Text = .SelectedPath
            If Not Me.txtAutoArchiveOut.Text.EndsWith("\") Then Me.txtAutoArchiveOut.Text = Me.txtAutoArchiveOut.Text & "\"
        End With
    End Sub

    Private Sub chkAutoArchiveIn_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAutoArchiveIn.CheckedChanged
        If Me.chkAutoArchiveIn.Checked = True Then
            Me.btnAutoArchiveIn.Enabled = True
            Me.txtAutoArchiveIn.Enabled = True
        Else
            Me.btnAutoArchiveIn.Enabled = False
            Me.txtAutoArchiveIn.Enabled = False
        End If
    End Sub

    Private Sub chkAutoArchiveOut_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAutoArchiveOut.CheckedChanged
        If Me.chkAutoArchiveOut.Checked = True Then
            Me.btnAutoArchiveOut.Enabled = True
            Me.txtAutoArchiveOut.Enabled = True
        Else
            Me.btnAutoArchiveOut.Enabled = False
            Me.txtAutoArchiveOut.Enabled = False
        End If
    End Sub

    Private Sub chkMonitorSentItems_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkMonitorSentItems.CheckedChanged
        ChangedMonitoring = True
    End Sub


End Class
