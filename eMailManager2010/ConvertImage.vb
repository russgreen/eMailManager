<Global.System.Security.Permissions.PermissionSetAttribute _
 (Global.System.Security.Permissions.SecurityAction.Demand, Name:="FullTrust")> _
Public Class ConvertImage
    Inherits System.Windows.Forms.AxHost
    Public Sub New()
        MyBase.New("59EE46BA-677D-4d20-BF10-8D8067CB8B32")
    End Sub

    Public Shared Function Convert(ByVal image As System.Drawing.Image) As stdole.IPictureDisp
        Return DirectCast(GetIPictureDispFromPicture(image), stdole.IPictureDisp)
        'Convert = GetIPictureFromPicture(image)
    End Function
End Class

