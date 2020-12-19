Imports System.Windows.Forms

Public Class frmMRU

    Friend SelectedSearchPath As String = Nothing

    Private Sub frmMRU_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cSettings.LoadSettings()
        cSettings.GetMRU()
        loadMRUlist()

        If Me.lvwMRU.Items.Count = 0 Then
            SelectedSearchPath = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            Me.Close()
        End If
    End Sub

    Private Sub lvwMRU_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvwMRU.MouseClick
        If Me.lvwMRU.SelectedItems.Count > 0 Then
            SelectedSearchPath = lvwMRU.SelectedItems(0).Tag.ToString
            Me.Close()
        End If
    End Sub

    Private Sub lvwMRU_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvwMRU.DoubleClick
        If Me.lvwMRU.SelectedItems.Count > 0 Then
            SelectedSearchPath = lvwMRU.SelectedItems(0).Tag.ToString
            Me.Close()
        End If
    End Sub

    Private Sub loadMRUlist(Optional ByVal SelectedValue As String = Nothing)
        Me.lvwMRU.Items.Clear()

        If cSettings.paths.Count > 0 Then
            For i As Integer = 0 To cSettings.paths.Count - 1 Step 1
                Dim sDesc, sPath, sTag As String
                sPath = cSettings.paths.Item(i).Substring(0, cSettings.paths.Item(i).ToString.IndexOf("|"))
                Dim tmp As String = cSettings.paths.Item(i).ToString.Substring(cSettings.paths.Item(i).ToString.IndexOf("|") + 1)

                If tmp.Contains("|") = True Then
                    sTag = NullSafeString(tmp.ToString.Substring(0, tmp.ToString.IndexOf("|")), sPath)
                    sDesc = tmp.ToString.Substring(tmp.ToString.IndexOf("|") + 1)
                Else
                    sTag = NullSafeString(tmp, sPath)
                    sDesc = "description"
                End If

                Dim oListItem As New ListViewItem
                'Dim oSubListItem As New ListViewItem.ListViewSubItem(oListItem, cSettings.paths.Item(i).ToString.Substring(cSettings.paths.Item(i).ToString.IndexOf("|") + 1))
                Dim oSubListItem As New ListViewItem.ListViewSubItem(oListItem, sPath)

                oListItem.Tag = sTag
                oListItem.Text = sDesc 'cSettings.paths.Item(i).Substring(0, cSettings.paths.Item(i).ToString.IndexOf("|"))
                oListItem.SubItems.Add(oSubListItem)

                Me.lvwMRU.Items.Add(oListItem)
            Next i
        End If


        If SelectedValue IsNot Nothing Then
            For Each oItem As ListViewItem In Me.lvwMRU.Items
                If oItem.SubItems(1).Text = SelectedValue Then
                    oItem.EnsureVisible()
                    oItem.Selected = True
                    oItem.Focused = True
                    Me.lvwMRU.Focus()
                End If
            Next
        End If

    End Sub

#Region "  ListView Sorting  "
    Dim sortColumn As Integer = -1

    Private Sub ListView_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lvwMRU.ColumnClick
        ' Set the ListViewItemSorter property to a new ListViewItemComparer
        ' object.
        ' Determine whether the column is the same as the last column clicked.

        If e.Column <> sortColumn Then
            ' Set the sort column to the new column.
            sortColumn = e.Column
            ' Set the sort order to ascending by default.
            Me.lvwMRU.Sorting = SortOrder.Ascending
        Else
            ' Determine what the last sort order was and change it.
            If Me.lvwMRU.Sorting = SortOrder.Ascending Then
                Me.lvwMRU.Sorting = SortOrder.Descending
            Else
                Me.lvwMRU.Sorting = SortOrder.Ascending
            End If
        End If
        ' Call the sort method to manually sort.
        Me.lvwMRU.Sort()
        ' Set the ListViewItemSorter property to a new ListViewItemComparer
        ' object.

        Me.lvwMRU.ListViewItemSorter = New ListViewItemComparer(e.Column, Me.lvwMRU.Sorting)
    End Sub

    ' Implements the manual sorting of items by column.
    ' Implements the manual sorting of items by columns.
    Class ListViewItemComparer
        Implements System.Collections.IComparer
        Private col As Integer
        Private order As SortOrder

        Public Sub New()
            col = 0
            order = SortOrder.Ascending
        End Sub

        Public Sub New(ByVal column As Integer, ByVal order As SortOrder)
            col = column
            Me.order = order
        End Sub

        Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer _
            Implements System.Collections.IComparer.Compare
            Dim returnVal As Integer = -1
            returnVal = [String].Compare(CType(x,  _
                            ListViewItem).SubItems(col).Text, _
                            CType(y, ListViewItem).SubItems(col).Text)
            ' Determine whether the sort order is descending.
            If order = SortOrder.Descending Then
                ' Invert the value returned by String.Compare.
                returnVal *= -1
            End If

            Return returnVal
        End Function
    End Class
#End Region


End Class