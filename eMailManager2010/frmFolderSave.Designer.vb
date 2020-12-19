<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFolderSave
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFolderSave))
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtLocation = New System.Windows.Forms.TextBox()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.gbFileFormat = New System.Windows.Forms.GroupBox()
        Me.FileNameFilterBox1 = New eMailManager.FileNameFilterBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cboFileFormat = New System.Windows.Forms.ComboBox()
        Me.btnFileMessage = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.ToolStripProgressBar1 = New System.Windows.Forms.ToolStripProgressBar()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.chkDeleteOnFile = New System.Windows.Forms.CheckBox()
        Me.chkIncludeSubFolders = New System.Windows.Forms.CheckBox()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.GroupBox1.SuspendLayout()
        Me.gbFileFormat.SuspendLayout()
        Me.SuspendLayout()
        '
        'FolderBrowserDialog1
        '
        resources.ApplyResources(Me.FolderBrowserDialog1, "FolderBrowserDialog1")
        '
        'GroupBox1
        '
        resources.ApplyResources(Me.GroupBox1, "GroupBox1")
        Me.GroupBox1.Controls.Add(Me.txtLocation)
        Me.GroupBox1.Controls.Add(Me.btnBrowse)
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GroupBox1, resources.GetString("GroupBox1.ToolTip"))
        '
        'txtLocation
        '
        resources.ApplyResources(Me.txtLocation, "txtLocation")
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtLocation, resources.GetString("txtLocation.ToolTip"))
        '
        'btnBrowse
        '
        resources.ApplyResources(Me.btnBrowse, "btnBrowse")
        Me.btnBrowse.Name = "btnBrowse"
        Me.ToolTip1.SetToolTip(Me.btnBrowse, resources.GetString("btnBrowse.ToolTip"))
        '
        'gbFileFormat
        '
        resources.ApplyResources(Me.gbFileFormat, "gbFileFormat")
        Me.gbFileFormat.Controls.Add(Me.FileNameFilterBox1)
        Me.gbFileFormat.Controls.Add(Me.Label3)
        Me.gbFileFormat.Controls.Add(Me.cboFileFormat)
        Me.gbFileFormat.Name = "gbFileFormat"
        Me.gbFileFormat.TabStop = False
        Me.ToolTip1.SetToolTip(Me.gbFileFormat, resources.GetString("gbFileFormat.ToolTip"))
        '
        'FileNameFilterBox1
        '
        resources.ApplyResources(Me.FileNameFilterBox1, "FileNameFilterBox1")
        Me.FileNameFilterBox1.DefaultFileNameFilter = "<sent>_<from>_<subject>"
        Me.FileNameFilterBox1.FileNameFilter = Nothing
        Me.FileNameFilterBox1.Name = "FileNameFilterBox1"
        Me.ToolTip1.SetToolTip(Me.FileNameFilterBox1, resources.GetString("FileNameFilterBox1.ToolTip"))
        '
        'Label3
        '
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Name = "Label3"
        Me.ToolTip1.SetToolTip(Me.Label3, resources.GetString("Label3.ToolTip"))
        '
        'cboFileFormat
        '
        resources.ApplyResources(Me.cboFileFormat, "cboFileFormat")
        Me.cboFileFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFileFormat.FormattingEnabled = True
        Me.cboFileFormat.Items.AddRange(New Object() {resources.GetString("cboFileFormat.Items"), resources.GetString("cboFileFormat.Items1")})
        Me.cboFileFormat.Name = "cboFileFormat"
        Me.ToolTip1.SetToolTip(Me.cboFileFormat, resources.GetString("cboFileFormat.ToolTip"))
        '
        'btnFileMessage
        '
        resources.ApplyResources(Me.btnFileMessage, "btnFileMessage")
        Me.btnFileMessage.Name = "btnFileMessage"
        Me.ToolTip1.SetToolTip(Me.btnFileMessage, resources.GetString("btnFileMessage.ToolTip"))
        '
        'btnCancel
        '
        resources.ApplyResources(Me.btnCancel, "btnCancel")
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Name = "btnCancel"
        Me.ToolTip1.SetToolTip(Me.btnCancel, resources.GetString("btnCancel.ToolTip"))
        '
        'ToolStripProgressBar1
        '
        resources.ApplyResources(Me.ToolStripProgressBar1, "ToolStripProgressBar1")
        Me.ToolStripProgressBar1.Name = "ToolStripProgressBar1"
        '
        'ToolTip1
        '
        Me.ToolTip1.AutoPopDelay = 6000
        Me.ToolTip1.InitialDelay = 500
        Me.ToolTip1.ReshowDelay = 100
        '
        'chkDeleteOnFile
        '
        resources.ApplyResources(Me.chkDeleteOnFile, "chkDeleteOnFile")
        Me.chkDeleteOnFile.Name = "chkDeleteOnFile"
        Me.ToolTip1.SetToolTip(Me.chkDeleteOnFile, resources.GetString("chkDeleteOnFile.ToolTip"))
        Me.chkDeleteOnFile.UseVisualStyleBackColor = True
        '
        'chkIncludeSubFolders
        '
        resources.ApplyResources(Me.chkIncludeSubFolders, "chkIncludeSubFolders")
        Me.chkIncludeSubFolders.Checked = True
        Me.chkIncludeSubFolders.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkIncludeSubFolders.Name = "chkIncludeSubFolders"
        Me.ToolTip1.SetToolTip(Me.chkIncludeSubFolders, resources.GetString("chkIncludeSubFolders.ToolTip"))
        Me.chkIncludeSubFolders.UseVisualStyleBackColor = True
        '
        'ProgressBar1
        '
        resources.ApplyResources(Me.ProgressBar1, "ProgressBar1")
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ToolTip1.SetToolTip(Me.ProgressBar1, resources.GetString("ProgressBar1.ToolTip"))
        '
        'frmFolderSave
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ControlBox = False
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.chkIncludeSubFolders)
        Me.Controls.Add(Me.chkDeleteOnFile)
        Me.Controls.Add(Me.gbFileFormat)
        Me.Controls.Add(Me.btnFileMessage)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmFolderSave"
        Me.ToolTip1.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.gbFileFormat.ResumeLayout(False)
        Me.gbFileFormat.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtLocation As System.Windows.Forms.TextBox
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents gbFileFormat As System.Windows.Forms.GroupBox
    Friend WithEvents cboFileFormat As System.Windows.Forms.ComboBox
    Friend WithEvents btnFileMessage As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents ToolStripProgressBar1 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents chkDeleteOnFile As System.Windows.Forms.CheckBox
    Friend WithEvents chkIncludeSubFolders As System.Windows.Forms.CheckBox
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents FileNameFilterBox1 As eMailManager.FileNameFilterBox

End Class
