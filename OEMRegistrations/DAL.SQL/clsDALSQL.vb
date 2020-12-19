Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient

Public Class clsDAL
    Implements IDisposable

    'Class level variables that are available to classes that instantiate me
    Public SQL As String

    Public Connection As SqlConnection
    Public Command As SqlCommand
    Public DataAdapter As SqlDataAdapter
    Public DataReader As SqlDataReader

    Private disposed As Boolean = False

    Public Sub New(ByVal Conn As String)
        Try
            Connection = New SqlConnection(Conn)
        Catch SQLExceptionErr As SqlException
            Throw New System.Exception(SQLExceptionErr.Message, _
                SQLExceptionErr.InnerException)
        End Try
    End Sub

    ' IDisposable
    Private Overloads Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposed Then
            If disposing Then
 If Not DataReader Is Nothing Then
                    DataReader.Close()
                    DataReader = Nothing
                End If
                If Not DataAdapter Is Nothing Then
                    DataAdapter.Dispose()
                    DataAdapter = Nothing
                End If
                If Not Command Is Nothing Then
                    Command.Dispose()
                    Command = Nothing
                End If
                If Not Connection Is Nothing Then
                    Connection.Close()
                    Connection.Dispose()
                    Connection = Nothing
                End If
            End If

 End If
        Me.disposed = True
    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overrides Sub Finalize()
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(False)
        MyBase.Finalize()
    End Sub
#End Region

    Public Sub OpenConnection()
        Try
            Connection.Open()
        Catch SqlExceptionErr As Common.DbException
            Throw New System.Exception(SqlExceptionErr.Message, _
                SqlExceptionErr.InnerException)
        Catch InvalidOperationExceptionErr As InvalidOperationException
            Throw New System.Exception(InvalidOperationExceptionErr.Message, _
                InvalidOperationExceptionErr.InnerException)
        End Try
    End Sub


    Public Sub CloseConnection()
        Connection.Close()
    End Sub

    Public Sub InitializeCommand()
        If Command Is Nothing Then
            Try
                Command = New SqlCommand(SQL, Connection)
                'See if this is a stored procedure
                If Not SQL.ToUpper.StartsWith("SELECT ") _
                    And Not SQL.ToUpper.StartsWith("INSERT ") _
                    And Not SQL.ToUpper.StartsWith("UPDATE ") _
                    And Not SQL.ToUpper.StartsWith("DELETE ") _
                    And Not SQL.ToUpper.StartsWith("CREATE ") _
                    And Not SQL.ToUpper.StartsWith("ALTER ") Then
                    Command.CommandType = CommandType.StoredProcedure
                End If
            Catch SqlExceptionErr As SqlException
                Throw New System.Exception(SqlExceptionErr.Message, _
                    SqlExceptionErr.InnerException)
            End Try
        End If
    End Sub

    Public Sub AddParameter(ByVal Name As String, ByVal Type As SqlDbType, _
        ByVal Size As Integer, ByVal Value As Object)

        Try
            Command.Parameters.Add(Name, Type, Size).Value = Value
        Catch SqlExceptionErr As SqlException
            Throw New System.Exception(SqlExceptionErr.Message, _
                SqlExceptionErr.InnerException)
        End Try
    End Sub

    Public Sub InitializeDataAdapter()
        Try
            DataAdapter = New SqlDataAdapter
            DataAdapter.SelectCommand = Command
        Catch SqlExceptionErr As SqlException
            Throw New System.Exception(SqlExceptionErr.Message, _
            SqlExceptionErr.InnerException)
        End Try
    End Sub

    Public Sub FillDataSet(ByRef oDataSet As DataSet, ByVal TableName As String)
        Try
            InitializeCommand()
            InitializeDataAdapter()
            DataAdapter.Fill(oDataSet, TableName)
        Catch SqlExceptionErr As SqlException
            Throw New System.Exception(SqlExceptionErr.Message, _
                SqlExceptionErr.InnerException)
        Finally
            Command.Dispose()
            Command = Nothing
            DataAdapter.Dispose()
            DataAdapter = Nothing
        End Try
    End Sub

    Public Sub FillDataTable(ByRef oDataTable As DataTable)
        Try
            InitializeCommand()
            InitializeDataAdapter()
            DataAdapter.Fill(oDataTable)
        Catch SqlExceptionErr As SqlException
            Throw New System.Exception(SqlExceptionErr.Message, _
                SqlExceptionErr.InnerException)
        Finally
            Command.Dispose()
            Command = Nothing
            DataAdapter.Dispose()
            DataAdapter = Nothing
        End Try
    End Sub

    Public Function ExecuteStoredProcedure() As Integer
        Try
            OpenConnection()
            ExecuteStoredProcedure = Command.ExecuteNonQuery()
        Catch ExceptionErr As Exception
            Throw New System.Exception(ExceptionErr.Message, _
                ExceptionErr.InnerException)
        Finally
            CloseConnection()
        End Try
    End Function
End Class
