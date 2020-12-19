<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.btnFileMessage = New System.Windows.Forms.Button()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.rdoWorkflow02 = New System.Windows.Forms.RadioButton()
        Me.rdoWorkflow01 = New System.Windows.Forms.RadioButton()
        Me.rdoWorkflow03 = New System.Windows.Forms.RadioButton()
        Me.lvwMRU = New System.Windows.Forms.ListView()
        Me.chDesc = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chMRU = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ctxList = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmiEditDesc = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmiRemoveEntry = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmiBackupList = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.rdoFileFolder = New System.Windows.Forms.RadioButton()
        Me.rdoOutlookFolder = New System.Windows.Forms.RadioButton()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.lblMailTo = New System.Windows.Forms.Label()
        Me.lblMailSubject = New System.Windows.Forms.Label()
        Me.lblMailFrom = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripProgressBar1 = New System.Windows.Forms.ToolStripProgressBar()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.gbFileFormat = New System.Windows.Forms.GroupBox()
        Me.cboFileFormat = New System.Windows.Forms.ComboBox()
        Me.chkLeaveCopy = New System.Windows.Forms.CheckBox()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.GroupBox2.SuspendLayout()
        Me.ctxList.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.gbFileFormat.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnFileMessage
        '
        resources.ApplyResources(Me.btnFileMessage, "btnFileMessage")
        Me.btnFileMessage.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnFileMessage.Name = "btnFileMessage"
        '
        'GroupBox2
        '
        resources.ApplyResources(Me.GroupBox2, "GroupBox2")
        Me.GroupBox2.Controls.Add(Me.rdoWorkflow02)
        Me.GroupBox2.Controls.Add(Me.rdoWorkflow01)
        Me.GroupBox2.Controls.Add(Me.rdoWorkflow03)
        Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.TabStop = False
        '
        'rdoWorkflow02
        '
        resources.ApplyResources(Me.rdoWorkflow02, "rdoWorkflow02")
        Me.rdoWorkflow02.BackColor = System.Drawing.SystemColors.Control
        Me.rdoWorkflow02.Checked = True
        Me.rdoWorkflow02.Name = "rdoWorkflow02"
        Me.rdoWorkflow02.TabStop = True
        Me.rdoWorkflow02.UseVisualStyleBackColor = False
        '
        'rdoWorkflow01
        '
        resources.ApplyResources(Me.rdoWorkflow01, "rdoWorkflow01")
        Me.rdoWorkflow01.BackColor = System.Drawing.SystemColors.Control
        Me.rdoWorkflow01.Name = "rdoWorkflow01"
        Me.rdoWorkflow01.UseVisualStyleBackColor = False
        '
        'rdoWorkflow03
        '
        resources.ApplyResources(Me.rdoWorkflow03, "rdoWorkflow03")
        Me.rdoWorkflow03.BackColor = System.Drawing.SystemColors.Control
        Me.rdoWorkflow03.Name = "rdoWorkflow03"
        Me.rdoWorkflow03.UseVisualStyleBackColor = False
        '
        'lvwMRU
        '
        resources.ApplyResources(Me.lvwMRU, "lvwMRU")
        Me.lvwMRU.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chDesc, Me.chMRU, Me.ColumnHeader1})
        Me.lvwMRU.ContextMenuStrip = Me.ctxList
        Me.lvwMRU.FullRowSelect = True
        Me.lvwMRU.GridLines = True
        Me.lvwMRU.HideSelection = False
        Me.lvwMRU.MultiSelect = False
        Me.lvwMRU.Name = "lvwMRU"
        Me.lvwMRU.UseCompatibleStateImageBehavior = False
        Me.lvwMRU.View = System.Windows.Forms.View.Details
        '
        'chDesc
        '
        resources.ApplyResources(Me.chDesc, "chDesc")
        '
        'chMRU
        '
        resources.ApplyResources(Me.chMRU, "chMRU")
        '
        'ColumnHeader1
        '
        resources.ApplyResources(Me.ColumnHeader1, "ColumnHeader1")
        '
        'ctxList
        '
        Me.ctxList.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmiEditDesc, Me.ToolStripSeparator1, Me.cmiRemoveEntry, Me.ToolStripMenuItem1, Me.cmiBackupList})
        Me.ctxList.Name = "ctxList"
        resources.ApplyResources(Me.ctxList, "ctxList")
        '
        'cmiEditDesc
        '
        Me.cmiEditDesc.Name = "cmiEditDesc"
        resources.ApplyResources(Me.cmiEditDesc, "cmiEditDesc")
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        resources.ApplyResources(Me.ToolStripSeparator1, "ToolStripSeparator1")
        '
        'cmiRemoveEntry
        '
        Me.cmiRemoveEntry.Image = Global.eMailManager.My.Resources.Resources.Delete
        Me.cmiRemoveEntry.Name = "cmiRemoveEntry"
        resources.ApplyResources(Me.cmiRemoveEntry, "cmiRemoveEntry")
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        resources.ApplyResources(Me.ToolStripMenuItem1, "ToolStripMenuItem1")
        '
        'cmiBackupList
        '
        Me.cmiBackupList.Name = "cmiBackupList"
        resources.ApplyResources(Me.cmiBackupList, "cmiBackupList")
        '
        'GroupBox1
        '
        resources.ApplyResources(Me.GroupBox1, "GroupBox1")
        Me.GroupBox1.Controls.Add(Me.txtSearch)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.btnBrowse)
        Me.GroupBox1.Controls.Add(Me.rdoFileFolder)
        Me.GroupBox1.Controls.Add(Me.rdoOutlookFolder)
        Me.GroupBox1.Controls.Add(Me.lvwMRU)
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.TabStop = False
        '
        'txtSearch
        '
        resources.ApplyResources(Me.txtSearch, "txtSearch")
        Me.txtSearch.Name = "txtSearch"
        '
        'Label4
        '
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.Name = "Label4"
        '
        'btnBrowse
        '
        resources.ApplyResources(Me.btnBrowse, "btnBrowse")
        Me.btnBrowse.Name = "btnBrowse"
        '
        'rdoFileFolder
        '
        Me.rdoFileFolder.Checked = True
        resources.ApplyResources(Me.rdoFileFolder, "rdoFileFolder")
        Me.rdoFileFolder.Name = "rdoFileFolder"
        Me.rdoFileFolder.TabStop = True
        '
        'rdoOutlookFolder
        '
        resources.ApplyResources(Me.rdoOutlookFolder, "rdoOutlookFolder")
        Me.rdoOutlookFolder.Name = "rdoOutlookFolder"
        '
        'btnCancel
        '
        resources.ApplyResources(Me.btnCancel, "btnCancel")
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Name = "btnCancel"
        '
        'GroupBox3
        '
        resources.ApplyResources(Me.GroupBox3, "GroupBox3")
        Me.GroupBox3.Controls.Add(Me.lblMailTo)
        Me.GroupBox3.Controls.Add(Me.lblMailSubject)
        Me.GroupBox3.Controls.Add(Me.lblMailFrom)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.TabStop = False
        '
        'lblMailTo
        '
        resources.ApplyResources(Me.lblMailTo, "lblMailTo")
        Me.lblMailTo.Name = "lblMailTo"
        '
        'lblMailSubject
        '
        resources.ApplyResources(Me.lblMailSubject, "lblMailSubject")
        Me.lblMailSubject.Name = "lblMailSubject"
        '
        'lblMailFrom
        '
        resources.ApplyResources(Me.lblMailFrom, "lblMailFrom")
        Me.lblMailFrom.Name = "lblMailFrom"
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        '
        'Label3
        '
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Name = "Label3"
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'btnDelete
        '
        resources.ApplyResources(Me.btnDelete, "btnDelete")
        Me.btnDelete.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnDelete.Name = "btnDelete"
        Me.ToolTip1.SetToolTip(Me.btnDelete, resources.GetString("btnDelete.ToolTip"))
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.ToolStripProgressBar1})
        resources.ApplyResources(Me.StatusStrip1, "StatusStrip1")
        Me.StatusStrip1.Name = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        resources.ApplyResources(Me.ToolStripStatusLabel1, "ToolStripStatusLabel1")
        Me.ToolStripStatusLabel1.Spring = True
        '
        'ToolStripProgressBar1
        '
        Me.ToolStripProgressBar1.Name = "ToolStripProgressBar1"
        resources.ApplyResources(Me.ToolStripProgressBar1, "ToolStripProgressBar1")
        Me.ToolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        '
        'gbFileFormat
        '
        resources.ApplyResources(Me.gbFileFormat, "gbFileFormat")
        Me.gbFileFormat.Controls.Add(Me.cboFileFormat)
        Me.gbFileFormat.Name = "gbFileFormat"
        Me.gbFileFormat.TabStop = False
        '
        'cboFileFormat
        '
        Me.cboFileFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFileFormat.FormattingEnabled = True
        Me.cboFileFormat.Items.AddRange(New Object() {resources.GetString("cboFileFormat.Items"), resources.GetString("cboFileFormat.Items1")})
        resources.ApplyResources(Me.cboFileFormat, "cboFileFormat")
        Me.cboFileFormat.Name = "cboFileFormat"
        '
        'chkLeaveCopy
        '
        resources.ApplyResources(Me.chkLeaveCopy, "chkLeaveCopy")
        Me.chkLeaveCopy.Name = "chkLeaveCopy"
        Me.chkLeaveCopy.UseVisualStyleBackColor = True
        '
        'frmMain
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ControlBox = False
        Me.Controls.Add(Me.chkLeaveCopy)
        Me.Controls.Add(Me.gbFileFormat)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.btnFileMessage)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnCancel)
        Me.Name = "frmMain"
        Me.GroupBox2.ResumeLayout(False)
        Me.ctxList.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.gbFileFormat.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnFileMessage As System.Windows.Forms.Button
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents rdoWorkflow02 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoWorkflow01 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoWorkflow03 As System.Windows.Forms.RadioButton
    Friend WithEvents lvwMRU As System.Windows.Forms.ListView
    Friend WithEvents chMRU As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents rdoFileFolder As System.Windows.Forms.RadioButton
    Friend WithEvents rdoOutlookFolder As System.Windows.Forms.RadioButton
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents lblMailTo As System.Windows.Forms.Label
    Friend WithEvents lblMailSubject As System.Windows.Forms.Label
    Friend WithEvents lblMailFrom As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripProgressBar1 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents gbFileFormat As System.Windows.Forms.GroupBox
    Friend WithEvents cboFileFormat As System.Windows.Forms.ComboBox
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents chkLeaveCopy As System.Windows.Forms.CheckBox
    Friend WithEvents chDesc As System.Windows.Forms.ColumnHeader
    Friend WithEvents ctxList As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents cmiEditDesc As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmiRemoveEntry As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmiBackupList As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
End Class
