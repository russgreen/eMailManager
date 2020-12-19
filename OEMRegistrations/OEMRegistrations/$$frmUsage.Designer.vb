<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUsage
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
        Me.components = New System.ComponentModel.Container()
        Me.TblUsesOEMDataGridView = New System.Windows.Forms.DataGridView()
        Me.AppVersion = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewGrouper1 = New Subro.Controls.DataGridViewGrouper(Me.components)
        Me.dsUses = New OEMRegistrations.DataSet1()
        Me.dsUsesBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        CType(Me.TblUsesOEMDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dsUses, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dsUsesBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TblUsesOEMDataGridView
        '
        Me.TblUsesOEMDataGridView.AllowUserToAddRows = False
        Me.TblUsesOEMDataGridView.AutoGenerateColumns = False
        Me.TblUsesOEMDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.TblUsesOEMDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.AppVersion})
        Me.TblUsesOEMDataGridView.DataSource = Me.dsUsesBindingSource
        Me.TblUsesOEMDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TblUsesOEMDataGridView.Location = New System.Drawing.Point(0, 0)
        Me.TblUsesOEMDataGridView.Name = "TblUsesOEMDataGridView"
        Me.TblUsesOEMDataGridView.Size = New System.Drawing.Size(698, 450)
        Me.TblUsesOEMDataGridView.TabIndex = 1
        '
        'AppVersion
        '
        Me.AppVersion.DataPropertyName = "AppVersion"
        Me.AppVersion.HeaderText = "AppVersion"
        Me.AppVersion.Name = "AppVersion"
        Me.AppVersion.ReadOnly = True
        '
        'DataGridViewGrouper1
        '
        Me.DataGridViewGrouper1.DataGridView = Me.TblUsesOEMDataGridView
        '
        'dsUses
        '
        Me.dsUses.DataSetName = "dsUses"
        Me.dsUses.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'dsUsesBindingSource
        '
        Me.dsUsesBindingSource.DataSource = Me.dsUses
        Me.dsUsesBindingSource.Position = 0
        '
        'frmUsage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(698, 450)
        Me.Controls.Add(Me.TblUsesOEMDataGridView)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmUsage"
        Me.Text = "frmUsage"
        CType(Me.TblUsesOEMDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dsUses, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dsUsesBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TblUsesOEMDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewGrouper1 As Subro.Controls.DataGridViewGrouper
    Friend WithEvents AppVersion As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dsUsesBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents dsUses As OEMRegistrations.DataSet1
End Class
