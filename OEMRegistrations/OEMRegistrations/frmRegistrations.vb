Public Class frmRegistrations

    Private Sub frmRegistrations_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'DsRegistrations.oem_registrations' table. You can move, or remove it, as needed.
        Me.Oem_registrationsTableAdapter.Fill(Me.DsRegistrations.oem_registrations)

    End Sub

    Private Sub TblRegistrationsOEMBindingNavigatorSaveItem_Click(sender As System.Object, e As System.EventArgs) Handles TblRegistrationsOEMBindingNavigatorSaveItem.Click
        Me.Validate()
        Me.OemregistrationsBindingSource.EndEdit()
        Me.Oem_registrationsTableAdapter.Update(Me.DsRegistrations)
    End Sub
End Class