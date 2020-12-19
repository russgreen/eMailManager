Imports DAL.SQL

Public Class frmSummary

    Dim LicLimit As Integer = 1

    Private Sub frmSummary_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadRegistrationsToList()

    End Sub

    Private Sub lvwRegistrations_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lvwRegistrations.SelectedIndexChanged
        On Error Resume Next
        LoadRegDetails()
        LoadUsageSummaryToList()


    End Sub

    Private Sub LoadRegDetails()
        'Initialize a new instance of the data access base class
        Using objData As New clsDAL(DataSource)

            objData.SQL = "SELECT  * FROM tblRegistrationsOEM WHERE UserID LIKE @UserID"

            objData.InitializeCommand()

            'Add the Parameters to the Parameters collection
            objData.AddParameter("@UserID", Data.SqlDbType.NText, Me.lvwRegistrations.SelectedItems(0).Tag.ToString.Length, Me.lvwRegistrations.SelectedItems(0).Tag)

            objData.OpenConnection()
            objData.DataReader = objData.Command.ExecuteReader

            'See if any data exists before continuing
            If objData.DataReader.HasRows Then

                'Process all rows
                While objData.DataReader.Read()

                    Me.txtUserID.Text = objData.DataReader.Item("UserID")
                    Me.txtRegKey.Text = objData.DataReader.Item("RegKey")
                    Me.txtQty.Text = objData.DataReader.Item("QTY")
                    LicLimit = CInt(Me.txtQty.Text)

                End While

            End If

            objData.DataReader.Close()
        End Using
    End Sub

    Private Sub LoadUsageSummaryToList()
        'Clear previous list
        lvwUsageSummary.Items.Clear()

        'clear the listview
        lvwUsageSummary.Clear()

        'create columns in listview
        lvwUsageSummary.Columns.Add("Usage Count", 60, HorizontalAlignment.Left)
        lvwUsageSummary.Columns.Add("Year", 60, HorizontalAlignment.Left)
        lvwUsageSummary.Columns.Add("Month", 60, HorizontalAlignment.Left)
        lvwUsageSummary.Columns.Add("Day", 60, HorizontalAlignment.Left)

        'Declare variables
        Dim objListViewItem As ListViewItem

        'Initialize a new instance of the data access base class
        Using objData As New clsDAL(DataSource)

            objData.SQL = "SELECT  Count(*) AS UsageCount,  DATEPART(Yy, DateChecked) AS YearDate, DATEPART(Mm, DateChecked) AS MonthDate, DATEPART(Dy, DateChecked) As DayNumber FROM tblUsesOEM WHERE UserID LIKE @UserID GROUP BY DATEPART(Yy, DateChecked),DATEPART(Mm, DateChecked),DATEPART(Dy, DateChecked) ORDER BY DATEPART(Yy, DateChecked),DATEPART(Mm, DateChecked), DATEPART(Dy, DateChecked) "

            objData.InitializeCommand()

            'Add the Parameters to the Parameters collection
            objData.AddParameter("@UserID", Data.SqlDbType.NText, Me.lvwRegistrations.SelectedItems(0).Tag.ToString.Length, Me.lvwRegistrations.SelectedItems(0).Tag)

            objData.OpenConnection()
            objData.DataReader = objData.Command.ExecuteReader

            'See if any data exists before continuing
            If objData.DataReader.HasRows Then

                'Process all rows
                While objData.DataReader.Read()

                    'Create a new ListViewItem
                    objListViewItem = New ListViewItem

                    'format the listviewitem
                    If objData.DataReader.Item("UsageCount") > LicLimit Then
                        objListViewItem.ForeColor = Color.Red
                    End If

                    'Add the data to the ListViewItem
                    objListViewItem.Text = objData.DataReader.Item("UsageCount")

                    'Add the sub items to the listview item
                    objListViewItem.SubItems.Add(objData.DataReader.Item("YearDate"))
                    objListViewItem.SubItems.Add(objData.DataReader.Item("MonthDate"))
                    objListViewItem.SubItems.Add(objData.DataReader.Item("DayNumber"))

                    'Add the ListViewItem to the ListView control
                    lvwUsageSummary.Items.Add(objListViewItem)

                End While

            End If

            objData.DataReader.Close()
        End Using
    End Sub


    Private Sub LoadRegistrationsToList()
        'Clear previous list
        lvwRegistrations.Items.Clear()

        'clear the listview
        lvwRegistrations.Clear()

        'create columns
        lvwRegistrations.Columns.Add("License Holder", 300, HorizontalAlignment.Left)

        'Declare variables
        Dim objListViewItem As ListViewItem

        'Initialize a new instance of the data access base class
        Using objData As New clsDAL(DataSource)
            objData.SQL = "SELECT ID, Company, UserID, RTRIM(LTRIM(CAST(Company as nvarchar))) + ' - (' + CAST(UserID as nvarchar) + ')' AS DisMem FROM tblRegistrationsOEM ORDER BY Company"

            objData.InitializeCommand()

            objData.OpenConnection()
            objData.DataReader = objData.Command.ExecuteReader

            'See if any data exists before continuing
            If objData.DataReader.HasRows Then

                'Process all rows
                While objData.DataReader.Read()
                    'Create a new ListViewItem
                    objListViewItem = New ListViewItem

                    'Add the data to the ListViewItem
                    objListViewItem.Text = NullSafeString(objData.DataReader.Item("DisMem"))
                    objListViewItem.Tag = objData.DataReader.Item("UserID")

                    'Add the sub items to the listview item
                    'objListViewItem.SubItems.Add(objData.DataReader.Item("ProjectNumber"))

                    'Add the ListViewItem to the ListView control
                    lvwRegistrations.Items.Add(objListViewItem)
                End While

            End If

            objData.DataReader.Close()
        End Using
    End Sub


End Class