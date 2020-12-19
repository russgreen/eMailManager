<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMRU
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lvwMRU = New System.Windows.Forms.ListView()
        Me.chDesc = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chMRU = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SuspendLayout()
        '
        'lvwMRU
        '
        Me.lvwMRU.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chDesc, Me.chMRU, Me.ColumnHeader1})
        Me.lvwMRU.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvwMRU.FullRowSelect = True
        Me.lvwMRU.GridLines = True
        Me.lvwMRU.HideSelection = False
        Me.lvwMRU.Location = New System.Drawing.Point(0, 0)
        Me.lvwMRU.MultiSelect = False
        Me.lvwMRU.Name = "lvwMRU"
        Me.lvwMRU.Size = New System.Drawing.Size(624, 370)
        Me.lvwMRU.TabIndex = 8
        Me.lvwMRU.UseCompatibleStateImageBehavior = False
        Me.lvwMRU.View = System.Windows.Forms.View.Details
        '
        'chDesc
        '
        Me.chDesc.Text = "Description"
        Me.chDesc.Width = 210
        '
        'chMRU
        '
        Me.chMRU.Text = "Message Location"
        Me.chMRU.Width = 647
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Width = 0
        '
        'frmMRU
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(624, 370)
        Me.ControlBox = False
        Me.Controls.Add(Me.lvwMRU)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.Name = "frmMRU"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "frmMRU"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lvwMRU As System.Windows.Forms.ListView
    Friend WithEvents chDesc As System.Windows.Forms.ColumnHeader
    Friend WithEvents chMRU As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
End Class
