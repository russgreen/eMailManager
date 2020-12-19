Partial Class RibbonMailItem
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RibbonMailItem))
        Me.Tab2 = Me.Factory.CreateRibbonTab
        Me.Group2 = Me.Factory.CreateRibbonGroup
        Me.Button1 = Me.Factory.CreateRibbonButton
        Me.Tab2.SuspendLayout()
        Me.Group2.SuspendLayout()
        '
        'Tab2
        '
        Me.Tab2.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office
        Me.Tab2.ControlId.OfficeId = "TabReadMessage"
        Me.Tab2.Groups.Add(Me.Group2)
        resources.ApplyResources(Me.Tab2, "Tab2")
        Me.Tab2.Name = "Tab2"
        '
        'Group2
        '
        Me.Group2.Items.Add(Me.Button1)
        resources.ApplyResources(Me.Group2, "Group2")
        Me.Group2.Name = "Group2"
        '
        'Button1
        '
        Me.Button1.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        resources.ApplyResources(Me.Button1, "Button1")
        Me.Button1.Name = "Button1"
        Me.Button1.OfficeImageId = "CreateMailRule"
        Me.Button1.ShowImage = True
        '
        'RibbonMailItem
        '
        Me.Name = "RibbonMailItem"
        Me.RibbonType = "Microsoft.Outlook.Mail.Read"
        Me.Tabs.Add(Me.Tab2)
        Me.Tab2.ResumeLayout(False)
        Me.Tab2.PerformLayout()
        Me.Group2.ResumeLayout(False)
        Me.Group2.PerformLayout()

    End Sub

    Friend WithEvents Tab2 As Microsoft.Office.Tools.Ribbon.RibbonTab
    Friend WithEvents Group2 As Microsoft.Office.Tools.Ribbon.RibbonGroup
    Friend WithEvents Button1 As Microsoft.Office.Tools.Ribbon.RibbonButton
End Class

Partial Class ThisRibbonCollection

    <System.Diagnostics.DebuggerNonUserCode()> _
    Friend ReadOnly Property Ribbon1() As RibbonMailItem
        Get
            Return Me.GetRibbon(Of RibbonMailItem)()
        End Get
    End Property
End Class
