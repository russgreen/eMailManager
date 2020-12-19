
Imports System.Data.Objects
Imports System.Data

Class MainWindow
    Dim dataEntities As archisof_tbm_regEntities = New archisof_tbm_regEntities
    Dim Registrations As ObjectQuery(Of tblRegistrationsOEM)
    Dim Uses As ObjectQuery(Of tblUsesOEM)

    Private Sub Window_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        Registrations = dataEntities.tblRegistrationsOEMs

        Dim query = _
            From Registration In Registrations _
            Select Registration.Company, Registration.UserID

        Me.dgRegistrations.ItemsSource = query.ToList

    End Sub

    Private Sub dgRegistrations_SelectionChanged(sender As System.Object, e As System.Windows.Controls.SelectionChangedEventArgs) Handles dgRegistrations.SelectionChanged

        Dim myRow As DataRowView = DirectCast(Me.dgRegistrations.CurrentCell.Item, DataRowView)
        Dim myvalue As String = Convert.ToInt32(linha.Row.ItemArray(0).ToString())


        'Dim query = _
        '    From Registration In dataEntities.tblRegistrationsOEMs
        '    Where Registration.UserID = Me.dgRegistrations.SelectedValue
        '    Select Registration.Company, Registration.FName, Registration.LName
    End Sub


End Class