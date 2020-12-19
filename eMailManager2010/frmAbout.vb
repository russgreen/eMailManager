Imports System.Windows.Forms
Imports System.Security
Imports System.Security.Permissions
Imports System.Security.Policy
Imports System.Deployment.Application
Imports System.Diagnostics

Public NotInheritable Class frmAbout
    Private WithEvents oAppDeploy As ApplicationDeployment

    Private Sub frmAbout_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Set the title of the form.
        Dim ApplicationTitle As String
        If My.Application.Info.Title <> "" Then
            ApplicationTitle = My.Application.Info.Title
        Else
            ApplicationTitle = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If
        Me.Text = String.Format("About {0}", ApplicationTitle)
        ' Initialize all of the text displayed on the About Box.
        Me.LabelProductName.Text = My.Application.Info.ProductName
        Me.LabelVersion.Text = String.Format("Version {0}", My.Application.Info.Version.ToString)
        Me.LabelCopyright.Text = My.Application.Info.Copyright
        Me.LabelCompanyName.Text = My.Application.Info.CompanyName
        Me.TextBoxDescription.Text = My.Application.Info.Description

        Me.lblMaxPath.Text = "MaxPath = " & MaxFile

        If ApplicationDeployment.IsNetworkDeployed = True Then
            ' Do deployment logic
            'If ApplicationDeployment.CurrentDeployment.CheckForUpdate = True Then
            '    Me.btnUpdate.Visible = True
            'Else
            '    Me.btnUpdate.Visible = False
            'End If
        End If
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If ApplicationDeployment.IsNetworkDeployed = True Then
        '    Me.btnUpdate.Enabled = False

        '    'manaully update addin
        '    Dim ExplorerInstallProc As System.Diagnostics.Process = New System.Diagnostics.Process()
        '    ExplorerInstallProc.StartInfo.Arguments = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Office\Outlook\AddIns\archisoft.eMail Manager", "Manifest", "http://www.archisoft.co.uk/email_manager/eMailManager.vsto").ToString
        '    ExplorerInstallProc.StartInfo.FileName = "explorer.exe"

        '    'MessageBox.Show(ExplorerInstallProc.StartInfo.FileName.ToString & " " & ExplorerInstallProc.StartInfo.Arguments.ToString)
        '    ExplorerInstallProc.Start()
        'End If
    End Sub

    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click
        Me.Close()
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim frm As New frmEULA
        With frm
            .ShowDialog(Me)
            .Dispose()
        End With
    End Sub

    Private Sub LogoPictureBox_Click(sender As System.Object, e As System.EventArgs) Handles LogoPictureBox.Click
        Process.Start("www.archisoft.co.uk")
    End Sub
End Class
