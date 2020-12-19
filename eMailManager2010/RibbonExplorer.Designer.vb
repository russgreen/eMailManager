Partial Class RibbonExplorer
    Inherits Microsoft.Office.Tools.Ribbon.RibbonBase

    <System.Diagnostics.DebuggerNonUserCode()> _
   Public Sub New(ByVal container As System.ComponentModel.IContainer)
        MyClass.New()

        'Required for Windows.Forms Class Composition Designer support
        If (container IsNot Nothing) Then
            container.Add(Me)
        End If

    End Sub

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Sub New()
        MyBase.New(Globals.Factory.GetRibbonFactory())

        'This call is required by the Component Designer.
        InitializeComponent()

    End Sub

    'Component overrides dispose to clean up the component list.
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

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RibbonExplorer))
        Me.Tab1 = Me.Factory.CreateRibbonTab
        Me.Group1 = Me.Factory.CreateRibbonGroup
        Me.btnFileMessage = Me.Factory.CreateRibbonButton
        Me.btnFileFolder = Me.Factory.CreateRibbonButton
        Me.btnSearch = Me.Factory.CreateRibbonButton
        Me.btnUpdate = Me.Factory.CreateRibbonButton
        Me.btnSettings = Me.Factory.CreateRibbonButton
        Me.btnAbout = Me.Factory.CreateRibbonButton
        Me.Tab1.SuspendLayout()
        Me.Group1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Tab1
        '
        Me.Tab1.Groups.Add(Me.Group1)
        resources.ApplyResources(Me.Tab1, "Tab1")
        Me.Tab1.Name = "Tab1"
        '
        'Group1
        '
        Me.Group1.Items.Add(Me.btnFileMessage)
        Me.Group1.Items.Add(Me.btnFileFolder)
        Me.Group1.Items.Add(Me.btnSearch)
        Me.Group1.Items.Add(Me.btnUpdate)
        Me.Group1.Items.Add(Me.btnSettings)
        Me.Group1.Items.Add(Me.btnAbout)
        resources.ApplyResources(Me.Group1, "Group1")
        Me.Group1.Name = "Group1"
        '
        'btnFileMessage
        '
        Me.btnFileMessage.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.btnFileMessage.Image = Global.eMailManager.My.Resources.Resources.file_mail
        resources.ApplyResources(Me.btnFileMessage, "btnFileMessage")
        Me.btnFileMessage.Name = "btnFileMessage"
        Me.btnFileMessage.OfficeImageId = "CreateMailRule"
        Me.btnFileMessage.ShowImage = True
        '
        'btnFileFolder
        '
        Me.btnFileFolder.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.btnFileFolder.Image = Global.eMailManager.My.Resources.Resources.FilingCabinet
        resources.ApplyResources(Me.btnFileFolder, "btnFileFolder")
        Me.btnFileFolder.Name = "btnFileFolder"
        Me.btnFileFolder.ShowImage = True
        '
        'btnSearch
        '
        Me.btnSearch.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.btnSearch.Image = Global.eMailManager.My.Resources.Resources.magnifingglass
        resources.ApplyResources(Me.btnSearch, "btnSearch")
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.ShowImage = True
        '
        'btnUpdate
        '
        resources.ApplyResources(Me.btnUpdate, "btnUpdate")
        Me.btnUpdate.Name = "btnUpdate"
        '
        'btnSettings
        '
        resources.ApplyResources(Me.btnSettings, "btnSettings")
        Me.btnSettings.Name = "btnSettings"
        '
        'btnAbout
        '
        resources.ApplyResources(Me.btnAbout, "btnAbout")
        Me.btnAbout.Name = "btnAbout"
        '
        'RibbonExplorer
        '
        Me.Name = "RibbonExplorer"
        Me.RibbonType = "Microsoft.Outlook.Explorer"
        Me.Tabs.Add(Me.Tab1)
        Me.Tab1.ResumeLayout(False)
        Me.Tab1.PerformLayout()
        Me.Group1.ResumeLayout(False)
        Me.Group1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Tab1 As Microsoft.Office.Tools.Ribbon.RibbonTab
    Friend WithEvents Group1 As Microsoft.Office.Tools.Ribbon.RibbonGroup
    Friend WithEvents btnFileMessage As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btnSearch As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btnAbout As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btnSettings As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btnFileFolder As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btnUpdate As Microsoft.Office.Tools.Ribbon.RibbonButton
End Class

Partial Class ThisRibbonCollection

    <System.Diagnostics.DebuggerNonUserCode()> _
    Friend ReadOnly Property RibbonExplorer() As RibbonExplorer
        Get
            Return Me.GetRibbon(Of RibbonExplorer)()
        End Get
    End Property
End Class
