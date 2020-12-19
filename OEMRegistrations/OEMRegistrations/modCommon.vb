Module modCommon
    Public DataSource As String = "Data Source=mssql.archisoft.co.uk;Initial Catalog=archisof_tbm_reg;Persist Security Info=True;User ID=archisoft;Password=Top000Secret"

#Region "  Error handling  "
    '****************************************************************
    ' NullSafeString
    '****************************************************************
    Public Function NullSafeString(ByVal arg As Object, _
        Optional ByVal returnIfEmpty As String = "") As String

        Dim returnValue As String

        If (arg Is DBNull.Value) OrElse (arg Is Nothing) _
                         OrElse (arg Is String.Empty) Then
            returnValue = returnIfEmpty
        Else
            Try
                returnValue = CStr(arg).Trim
            Catch
                returnValue = returnIfEmpty
            End Try

        End If

        Return returnValue

    End Function

    '****************************************************************
    ' NullSafeInteger
    '****************************************************************
    Public Function NullSafeInteger(ByVal arg As Object, _
      Optional ByVal returnIfEmpty As Integer = 0) As Integer

        Dim returnValue As Integer

        If (arg Is DBNull.Value) OrElse (arg Is Nothing) _
                         OrElse (arg Is String.Empty) Then
            returnValue = returnIfEmpty
        Else
            Try
                returnValue = CInt(arg)
            Catch
                returnValue = returnIfEmpty
            End Try
        End If

        Return returnValue

    End Function

    '****************************************************************
    ' NullSafeLong
    '****************************************************************
    Public Function NullSafeLong(ByVal arg As Object, _
      Optional ByVal returnIfEmpty As Long = 0) As Long

        Dim returnValue As Long

        If (arg Is DBNull.Value) OrElse (arg Is Nothing) _
                         OrElse (arg Is String.Empty) Then
            returnValue = returnIfEmpty
        Else
            Try
                returnValue = CLng(arg)
            Catch
                returnValue = returnIfEmpty
            End Try
        End If

        Return returnValue

    End Function

    '****************************************************************
    '   NullSafeDouble
    '****************************************************************
    Public Function NullSafeDouble(ByVal arg As Object, _
      Optional ByVal returnIfEmpty As Integer = 0) As Double

        Dim returnValue As Double

        If (arg Is DBNull.Value) OrElse (arg Is Nothing) _
                         OrElse (arg Is String.Empty) _
                         OrElse (Trim(arg) Is ".") Then
            returnValue = returnIfEmpty
        Else
            Try
                returnValue = CDbl(arg)
            Catch
                returnValue = returnIfEmpty
            End Try
        End If

        Return returnValue

    End Function

    '****************************************************************
    ' NullSafeBoolean
    '****************************************************************
    Public Function NullSafeBoolean(ByVal arg As Object, Optional ByVal NullVal As Boolean = False) As Boolean

        Dim returnValue As Boolean

        If (arg Is DBNull.Value) OrElse (arg Is Nothing) OrElse (arg Is String.Empty) Then
            returnValue = NullVal
        Else
            Try
                returnValue = CBool(arg)
            Catch
                returnValue = False
            End Try
        End If

        Return returnValue

    End Function


    '****************************************************************
    ' NullSafeDate
    '****************************************************************
    Public Function NullSafeDate(ByVal arg As Object) As Date

        Dim returnValue As Date

        If (arg Is DBNull.Value) OrElse (arg Is Nothing) _
                         OrElse (arg Is String.Empty) Then
            returnValue = Date.Now
        Else
            Try
                returnValue = CDate(arg)
                If returnValue < CDate("1 / 1 / 1753") Then
                    returnValue = CDate("1 / 1 / 1753")
                End If
            Catch
                returnValue = Date.Now
            End Try
        End If

        Return returnValue

    End Function


#End Region

End Module
