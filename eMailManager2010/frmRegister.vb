Imports System.Windows.Forms
Imports System.Diagnostics

Public Class frmRegister
    Private LocRM As New Resources.ResourceManager("eMailManager2010.ResourceStrings", Me.GetType.Assembly)

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        On Error Resume Next

        Dim wsK As New keygen.keygenSoapClient

        If wsK.KeyGen(Me.txtUserID.Text, CInt(Me.MaskedTextBox1.Text)) = Me.txtKey.Text Then
            'must be OK so enable the OK button
            'Me.OK_Button.Enabled = True
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        Else
            'Me.OK_Button.Enabled = False
            MsgBox(LocRM.GetString("Message007"), MsgBoxStyle.Information, "Error registering software")
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmRegister_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        On Error Resume Next
        Me.OK_Button.Enabled = False

        'populate the textboxes
        Me.txtUserID.Text = My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager").GetValue("UserID").ToString
        Me.txtKey.Text = My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager").GetValue("Key").ToString
        Me.MaskedTextBox1.Text = CInt(My.Computer.Registry.CurrentUser.OpenSubKey("Software\MailManager").GetValue("Qty")).ToString

        m_CheckLic()
    End Sub

    Private Sub checkLic(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUserID.TextChanged, txtKey.TextChanged, MaskedTextBox1.TextChanged
        m_CheckLic()
    End Sub


    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        System.Diagnostics.Process.Start("http://www.shareit.com/product.html?productid=300384396")
    End Sub


    Private Sub m_CheckLic()
        On Error Resume Next

        Dim wsK As New keygen.keygenSoapClient

        If IsGuidWithOptionalBraces(Me.txtUserID.Text) = True Then
            If wsK.KeyGen(Me.txtUserID.Text, CInt(Me.MaskedTextBox1.Text)) = Me.txtKey.Text Then
                'must be OK so enable the OK button
                Me.OK_Button.Enabled = True
            Else
                Me.OK_Button.Enabled = False
            End If
        Else
            Me.OK_Button.Enabled = False
        End If
    End Sub
End Class
