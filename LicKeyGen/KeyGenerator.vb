Option Strict Off

Imports Microsoft.VisualBasic

' Default example implementation of the key generator
Public Class KeyGenerator
    Implements IKeyGenerator

    ' The default seeds, useful for the demo apps to ensure the numbers
    ' are the same between client and server
    Private mySeeds() As Integer = {500, 600, 700}

    Public Function KeyGen(ByVal userID As String, ByVal seats As Integer) As String Implements IKeyGenerator.KeyGen
        Dim v1, v2, v3, v4 As Integer
        Dim p1, p2, p3, p4 As String

        ' Acquiring some data from the sUserID
        ' these data will be used to generate a unique serial number
        Dim length As Integer = userID.Length
        v1 = Asc(Left(userID, 1))
        v2 = Asc(Right(userID, 1))
        v3 = Asc(Left(userID, length / 2))
        v4 = Asc(Right(userID, length / 2))

        ' Codes to generate the serial
        ' However, restrictions are being applied that
        ' each serial may not exceed 5 character long
        Try
            p1 = Right(Hex(v1 * v2 + (v3 + v4) * seats * 100), 5)
        Catch ex As System.OverflowException
            Return "TOO MANY SEATS"
        End Try

        p2 = Left(Hex(v2 * v4 + (v1 + v2) * Seeds(0) * 100), 5)
        p3 = Right(Hex(v1 * v3 + (v2 + v4) * Seeds(1) * 100), 5)
        p4 = Left(Hex(v2 * v4 + (v3 + v1) * Seeds(2) * 100), 5)
        ' Pad the values with zeroes if necessary
        FixSerial(p1, 5)
        FixSerial(p2, 5)
        FixSerial(p3, 5)
        FixSerial(p4, 5)


        ' The 4 serials are assembled into the format:
        ' = @@@@@-@@@@@-@@@@@-@@@@@
        ' Returns the serial number in a string format
        Return p1 + "-" + p2 + "-" + p3 + "-" + p4
    End Function
    ' Contact zeroes to the beginning to make sure the lengths are uniform
    Public Shared Sub FixSerial(ByRef str As String, ByVal length As Integer)
        While str.Length < length
            str = String.Concat("0", str)
        End While
    End Sub

    Public ReadOnly Property Seeds() As Integer() Implements IKeyGenerator.Seeds
        Get
            Return mySeeds
        End Get
    End Property

End Class
