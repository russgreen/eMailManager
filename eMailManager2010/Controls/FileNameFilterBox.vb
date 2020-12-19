Imports System.ComponentModel

Public Class FileNameFilterBox
    Implements INotifyPropertyChanged

    Private m_FileNameFilter As String
    Private m_DefaultFileNameFilter As String = "<sent>_<from>_<subject>"
    Private m_IsValid As Boolean = True

    Public Property FileNameFilter() As String
        Get
            Return m_FileNameFilter
        End Get
        Set(ByVal value As String)
            m_FileNameFilter = value
            ' Call OnPropertyChanged whenever the property is updated
            OnPropertyChanged("FileNameFilter")
        End Set
    End Property
    Public Property DefaultFileNameFilter() As String
        Get
            Return m_DefaultFileNameFilter
        End Get
        Set(ByVal value As String)
            m_DefaultFileNameFilter = value
        End Set
    End Property
    Public ReadOnly Property IsValid() As Boolean
        Get
            Return m_IsValid
        End Get
    End Property

    Public Event IsValidChanged()
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    ' Create the OnPropertyChanged method to raise the event
    Protected Sub OnPropertyChanged(ByVal name As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(name))

        Select Case name
            Case "FileNameFilter"
                Me.TextBox1.Text = m_FileNameFilter
        End Select

    End Sub

    Private Sub ComboBox1_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectionChangeCommitted
        Me.TextBox1.Text = Me.TextBox1.Text & Me.ComboBox1.SelectedItem.ToString
        m_FileNameFilter = Me.TextBox1.Text

        Me.TextBox1.Focus()
        Me.TextBox1.Select(Me.TextBox1.Text.Length, Me.TextBox1.Text.Length)
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        If Me.TextBox1.Text = String.Empty Then
            m_IsValid = False
            Me.TextBox1.BackColor = Drawing.Color.Red
            'Me.TextBox1.Text = m_DefaultFileNameFilter
            RaiseEvent IsValidChanged()
        Else
            m_IsValid = True
            Me.TextBox1.BackColor = Drawing.Color.White
            RaiseEvent IsValidChanged()
        End If

        m_FileNameFilter = Me.TextBox1.Text
    End Sub
End Class
