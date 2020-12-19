' Interface to generalize the key generation process
' and allow for easier upgrade of algorithms (though must be same b/w client and server)
Public Interface IKeyGenerator
    ' Function that takes a userID and # of seats and returns the serial
    Function KeyGen(ByVal userID As String, ByVal seats As Integer) As String

    ' The array of seeds to set [readonly so assignment of a shorter array doesnt take place,
    '  not to prevent the necessary changing of seeds b/w programs]
    ReadOnly Property Seeds() As Integer()
End Interface
