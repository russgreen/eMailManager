Public Class frmMain

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'LoadFormIntoPanel(New frmSummary, Me.pnlMain)
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub



    Private Sub LoadFormIntoPanel(ByRef frm As Form, ByRef pnl As Panel)
        pnl.Controls.Clear()

        frm.TopLevel = False
        frm.Dock = DockStyle.Fill

        pnl.Controls.Add(frm)
        frm.Show()
    End Sub


    Private Sub UsageToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UsageToolStripMenuItem.Click
        LoadFormIntoPanel(New frmUsage, Me.pnlMain)
    End Sub

    Private Sub RegistrationsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RegistrationsToolStripMenuItem.Click
        LoadFormIntoPanel(New frmRegistrations, Me.pnlMain)
    End Sub

    Private Sub SummaryToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SummaryToolStripMenuItem.Click
        LoadFormIntoPanel(New frmSummary, Me.pnlMain)
    End Sub
End Class
