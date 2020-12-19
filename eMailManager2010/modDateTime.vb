Module modDateTime
    Public Function MonthShort() As String
        Dim TodayDate As String = ShortDay() & ShortMonth() & ShortYear()
        Return (TodayDate)
    End Function

    Function ShortDay() As String
        Return Format(Now, "dd")
        'Select Case Now.Day
        '    Case 1 : ShortDay = "01"
        '    Case 2 : ShortDay = "02"
        '    Case 3 : ShortDay = "03"
        '    Case 4 : ShortDay = "04"
        '    Case 5 : ShortDay = "05"
        '    Case 6 : ShortDay = "06"
        '    Case 7 : ShortDay = "07"
        '    Case 8 : ShortDay = "08"
        '    Case 9 : ShortDay = "09"
        '    Case 10 : ShortDay = "10"
        '    Case 11 : ShortDay = "11"
        '    Case 12 : ShortDay = "12"
        '    Case 13 : ShortDay = "13"
        '    Case 14 : ShortDay = "14"
        '    Case 15 : ShortDay = "15"
        '    Case 16 : ShortDay = "16"
        '    Case 17 : ShortDay = "17"
        '    Case 18 : ShortDay = "18"
        '    Case 19 : ShortDay = "19"
        '    Case 20 : ShortDay = "20"
        '    Case 21 : ShortDay = "21"
        '    Case 22 : ShortDay = "22"
        '    Case 23 : ShortDay = "23"
        '    Case 24 : ShortDay = "24"
        '    Case 25 : ShortDay = "25"
        '    Case 26 : ShortDay = "26"
        '    Case 27 : ShortDay = "27"
        '    Case 28 : ShortDay = "28"
        '    Case 29 : ShortDay = "29"
        '    Case 30 : ShortDay = "30"
        '    Case 31 : ShortDay = "31"

        'End Select
    End Function

    Function ShortMonth() As String
        'set new month values
        Return Format(Now, "MMM").ToUpper
        'Select Case Now.Month
        '    Case 1 : ShortMonth = "JAN"
        '    Case 2 : ShortMonth = "FEB"
        '    Case 3 : ShortMonth = "MAR"
        '    Case 4 : ShortMonth = "APR"
        '    Case 5 : ShortMonth = "MAY"
        '    Case 6 : ShortMonth = "JUN"
        '    Case 7 : ShortMonth = "JUL"
        '    Case 8 : ShortMonth = "AUG"
        '    Case 9 : ShortMonth = "SEP"
        '    Case 10 : ShortMonth = "OCT"
        '    Case 11 : ShortMonth = "NOV"
        '    Case 12 : ShortMonth = "DEC"
        'End Select
    End Function

    Function ShortYear() As String
        Dim sYear As String = CStr(Now.Year())
        Return (sYear.Substring(2, 2))
    End Function

    Public Function ShortDay1(ByVal sDay As String) As String
        Select Case sDay
            Case "1" : ShortDay1 = "01"
            Case "2" : ShortDay1 = "02"
            Case "3" : ShortDay1 = "03"
            Case "4" : ShortDay1 = "04"
            Case "5" : ShortDay1 = "05"
            Case "6" : ShortDay1 = "06"
            Case "7" : ShortDay1 = "07"
            Case "8" : ShortDay1 = "08"
            Case "9" : ShortDay1 = "09"
            Case "10" : ShortDay1 = "10"
            Case "11" : ShortDay1 = "11"
            Case "12" : ShortDay1 = "12"
            Case "13" : ShortDay1 = "13"
            Case "14" : ShortDay1 = "14"
            Case "15" : ShortDay1 = "15"
            Case "16" : ShortDay1 = "16"
            Case "17" : ShortDay1 = "17"
            Case "18" : ShortDay1 = "18"
            Case "19" : ShortDay1 = "19"
            Case "20" : ShortDay1 = "20"
            Case "21" : ShortDay1 = "21"
            Case "22" : ShortDay1 = "22"
            Case "23" : ShortDay1 = "23"
            Case "24" : ShortDay1 = "24"
            Case "25" : ShortDay1 = "25"
            Case "26" : ShortDay1 = "26"
            Case "27" : ShortDay1 = "27"
            Case "28" : ShortDay1 = "28"
            Case "29" : ShortDay1 = "29"
            Case "30" : ShortDay1 = "30"
            Case "31" : ShortDay1 = "31"

        End Select
    End Function

    Public Function ShortMonth1() As String
        'set new month values
        Return Format(Now, "MM")
        'Select Case Now.Month
        '    Case 1 : ShortMonth1 = "01"
        '    Case 2 : ShortMonth1 = "02"
        '    Case 3 : ShortMonth1 = "03"
        '    Case 4 : ShortMonth1 = "04"
        '    Case 5 : ShortMonth1 = "05"
        '    Case 6 : ShortMonth1 = "06"
        '    Case 7 : ShortMonth1 = "07"
        '    Case 8 : ShortMonth1 = "08"
        '    Case 9 : ShortMonth1 = "09"
        '    Case 10 : ShortMonth1 = "10"
        '    Case 11 : ShortMonth1 = "11"
        '    Case 12 : ShortMonth1 = "12"
        'End Select
    End Function

    Public Function ShortMonth2(ByVal sDate As String) As String
        Select Case sDate.Substring(3, 3)
            Case "JAN" : ShortMonth2 = "01"
            Case "FEB" : ShortMonth2 = "02"
            Case "MAR" : ShortMonth2 = "03"
            Case "APR" : ShortMonth2 = "04"
            Case "MAY" : ShortMonth2 = "05"
            Case "JUN" : ShortMonth2 = "06"
            Case "JUL" : ShortMonth2 = "07"
            Case "AUG" : ShortMonth2 = "08"
            Case "SEP" : ShortMonth2 = "09"
            Case "OCT" : ShortMonth2 = "10"
            Case "NOV" : ShortMonth2 = "11"
            Case "DEC" : ShortMonth2 = "12"
        End Select
    End Function

    Public Function ShortMonth3(ByVal sMonth As String) As String
        'set new month values
        Select Case sMonth
            Case "1" : ShortMonth3 = "01"
            Case "2" : ShortMonth3 = "02"
            Case "3" : ShortMonth3 = "03"
            Case "4" : ShortMonth3 = "04"
            Case "5" : ShortMonth3 = "05"
            Case "6" : ShortMonth3 = "06"
            Case "7" : ShortMonth3 = "07"
            Case "8" : ShortMonth3 = "08"
            Case "9" : ShortMonth3 = "09"
            Case "10" : ShortMonth3 = "10"
            Case "11" : ShortMonth3 = "11"
            Case "12" : ShortMonth3 = "12"
        End Select
    End Function

    Public Function ShortYear2(ByVal sYear As String) As String
        Return (sYear.Substring(2, 2))
    End Function
End Module
