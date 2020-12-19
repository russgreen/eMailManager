Public Class frmRegistrations_OLD

    Private Sub TblRegistrationsOEMBindingNavigatorSaveItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TblRegistrationsOEMBindingNavigatorSaveItem.Click
        Me.Validate()
        Me.TblRegistrationsOEMBindingSource.EndEdit()
        Me.TableAdapterManager.UpdateAll(Me.DsOEMRegistrations)

    End Sub

    Private Sub frmRegistrations_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'DsOEMRegistrations.tblRegistrationsOEM' table. You can move, or remove it, as needed.
        Me.TblRegistrationsOEMTableAdapter.Fill(Me.DsOEMRegistrations.tblRegistrationsOEM)

    End Sub
End Class