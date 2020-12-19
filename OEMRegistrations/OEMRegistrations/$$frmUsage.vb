Public Class frmUsage

    Private Sub TblUsesOEMBindingNavigatorSaveItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Validate()
        Me.TblUsesOEMBindingSource.EndEdit()
        Me.TableAdapterManager.UpdateAll(Me.DsOEMUses)

    End Sub

    Private Sub frmUsage_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'DataSet1.oem_usage' table. You can move, or remove it, as needed.
        Me.Oem_usageTableAdapter.Fill(Me.DataSet1.oem_usage)
        'TODO: This line of code loads data into the 'DsOEMUses.tblUsesOEM' table. You can move, or remove it, as needed.
        Me.TblUsesOEMTableAdapter.Fill(Me.DsOEMUses.tblUsesOEM)

        Dim grouper = New Subro.Controls.DataGridViewGrouper(Me.TblUsesOEMDataGridView)
        With grouper
            .SetGroupOn("UserID")
            .CollapseAll()
        End With

    End Sub



    Private Sub TblUsesOEMDataGridView_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles TblUsesOEMDataGridView.CellMouseDoubleClick
        Dim iCol, iRow As Integer
        iCol = e.ColumnIndex
        iRow = e.RowIndex

        Select Case iCol
            Case 0
                Process.Start("http://www.geobytes.com/IpLocator.htm?GetLocation&template=json.txt&ipaddress=" & TblUsesOEMDataGridView.Rows(iRow).Cells("DataGridViewTextBoxColumn1").Value)
        End Select
    End Sub
End Class