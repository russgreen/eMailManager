<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAbout
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Friend WithEvents TableLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LogoPictureBox As System.Windows.Forms.PictureBox
    Friend WithEvents LabelProductName As System.Windows.Forms.Label
    Friend WithEvents LabelVersion As System.Windows.Forms.Label
    Friend WithEvents LabelCompanyName As System.Windows.Forms.Label
    Friend WithEvents TextBoxDescription As System.Windows.Forms.TextBox
    Friend WithEvents OKButton As System.Windows.Forms.Button
    Friend WithEvents LabelCopyright As System.Windows.Forms.Label

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAbout))
        Me.TableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.LogoPictureBox = New System.Windows.Forms.PictureBox()
        Me.LabelProductName = New System.Windows.Forms.Label()
        Me.LabelVersion = New System.Windows.Forms.Label()
        Me.LabelCopyright = New System.Windows.Forms.Label()
        Me.LabelCompanyName = New System.Windows.Forms.Label()
        Me.TextBoxDescription = New System.Windows.Forms.TextBox()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.OKButton = New System.Windows.Forms.Button()
        Me.lblMaxPath = New System.Windows.Forms.Label()
        Me.TableLayoutPanel.SuspendLayout()
        CType(Me.LogoPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel
        '
        resources.ApplyResources(Me.TableLayoutPanel, "TableLayoutPanel")
        Me.TableLayoutPanel.Controls.Add(Me.LogoPictureBox, 0, 0)
        Me.TableLayoutPanel.Controls.Add(Me.LabelProductName, 1, 0)
        Me.TableLayoutPanel.Controls.Add(Me.LabelVersion, 1, 1)
        Me.TableLayoutPanel.Controls.Add(Me.LabelCopyright, 1, 2)
        Me.TableLayoutPanel.Controls.Add(Me.LabelCompanyName, 1, 3)
        Me.TableLayoutPanel.Controls.Add(Me.TextBoxDescription, 1, 4)
        Me.TableLayoutPanel.Controls.Add(Me.LinkLabel1, 1, 5)
        Me.TableLayoutPanel.Controls.Add(Me.OKButton, 2, 6)
        Me.TableLayoutPanel.Controls.Add(Me.lblMaxPath, 1, 6)
        Me.TableLayoutPanel.Name = "TableLayoutPanel"
        '
        'LogoPictureBox
        '
        Me.LogoPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        resources.ApplyResources(Me.LogoPictureBox, "LogoPictureBox")
        Me.LogoPictureBox.Name = "LogoPictureBox"
        Me.TableLayoutPanel.SetRowSpan(Me.LogoPictureBox, 7)
        Me.LogoPictureBox.TabStop = False
        '
        'LabelProductName
        '
        Me.TableLayoutPanel.SetColumnSpan(Me.LabelProductName, 2)
        resources.ApplyResources(Me.LabelProductName, "LabelProductName")
        Me.LabelProductName.Name = "LabelProductName"
        '
        'LabelVersion
        '
        Me.TableLayoutPanel.SetColumnSpan(Me.LabelVersion, 2)
        resources.ApplyResources(Me.LabelVersion, "LabelVersion")
        Me.LabelVersion.Name = "LabelVersion"
        '
        'LabelCopyright
        '
        Me.TableLayoutPanel.SetColumnSpan(Me.LabelCopyright, 2)
        resources.ApplyResources(Me.LabelCopyright, "LabelCopyright")
        Me.LabelCopyright.Name = "LabelCopyright"
        '
        'LabelCompanyName
        '
        Me.TableLayoutPanel.SetColumnSpan(Me.LabelCompanyName, 2)
        resources.ApplyResources(Me.LabelCompanyName, "LabelCompanyName")
        Me.LabelCompanyName.Name = "LabelCompanyName"
        '
        'TextBoxDescription
        '
        Me.TableLayoutPanel.SetColumnSpan(Me.TextBoxDescription, 2)
        resources.ApplyResources(Me.TextBoxDescription, "TextBoxDescription")
        Me.TextBoxDescription.Name = "TextBoxDescription"
        Me.TextBoxDescription.ReadOnly = True
        Me.TextBoxDescription.TabStop = False
        '
        'LinkLabel1
        '
        resources.ApplyResources(Me.LinkLabel1, "LinkLabel1")
        Me.TableLayoutPanel.SetColumnSpan(Me.LinkLabel1, 2)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.TabStop = True
        '
        'OKButton
        '
        resources.ApplyResources(Me.OKButton, "OKButton")
        Me.OKButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.OKButton.Name = "OKButton"
        '
        'lblMaxPath
        '
        resources.ApplyResources(Me.lblMaxPath, "lblMaxPath")
        Me.lblMaxPath.Name = "lblMaxPath"
        '
        'frmAbout
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.OKButton
        Me.Controls.Add(Me.TableLayoutPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAbout"
        Me.ShowInTaskbar = False
        Me.TableLayoutPanel.ResumeLayout(False)
        Me.TableLayoutPanel.PerformLayout()
        CType(Me.LogoPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents lblMaxPath As System.Windows.Forms.Label

End Class
