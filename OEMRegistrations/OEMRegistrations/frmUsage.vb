Public Class frmUsage

    Private Sub frmUsage_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'DataSet1.oem_usage' table. You can move, or remove it, as needed.
        Me.Oem_usageTableAdapter.Fill(Me.DataSet1.oem_usage)

    End Sub
End Class