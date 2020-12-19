<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSettings
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSettings))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.FileNameFilterBox1 = New eMailManager.FileNameFilterBox()
        Me.cboFileFormat = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cboCategory = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.chkAlwaysClearDeletedItems = New System.Windows.Forms.CheckBox()
        Me.chkAlwaysEmbedAttachments = New System.Windows.Forms.CheckBox()
        Me.chkLeaveCopy = New System.Windows.Forms.CheckBox()
        Me.chkADS = New System.Windows.Forms.CheckBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.chkMonitorSentItems = New System.Windows.Forms.CheckBox()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.btnAutoArchiveOut = New System.Windows.Forms.Button()
        Me.btnAutoArchiveIn = New System.Windows.Forms.Button()
        Me.txtAutoArchiveOut = New System.Windows.Forms.TextBox()
        Me.txtAutoArchiveIn = New System.Windows.Forms.TextBox()
        Me.chkAutoArchiveOut = New System.Windows.Forms.CheckBox()
        Me.chkAutoArchiveIn = New System.Windows.Forms.CheckBox()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        '
        'OK_Button
        '
        resources.ApplyResources(Me.OK_Button, "OK_Button")
        Me.OK_Button.Name = "OK_Button"
        '
        'Cancel_Button
        '
        resources.ApplyResources(Me.Cancel_Button, "Cancel_Button")
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Name = "Cancel_Button"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.FileNameFilterBox1)
        Me.GroupBox1.Controls.Add(Me.cboFileFormat)
        Me.GroupBox1.Controls.Add(Me.Label3)
        resources.ApplyResources(Me.GroupBox1, "GroupBox1")
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.TabStop = False
        '
        'FileNameFilterBox1
        '
        Me.FileNameFilterBox1.DefaultFileNameFilter = "<sent>_<from>_<subject>"
        Me.FileNameFilterBox1.FileNameFilter = Nothing
        resources.ApplyResources(Me.FileNameFilterBox1, "FileNameFilterBox1")
        Me.FileNameFilterBox1.Name = "FileNameFilterBox1"
        '
        'cboFileFormat
        '
        Me.cboFileFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFileFormat.FormattingEnabled = True
        Me.cboFileFormat.Items.AddRange(New Object() {resources.GetString("cboFileFormat.Items"), resources.GetString("cboFileFormat.Items1")})
        resources.ApplyResources(Me.cboFileFormat, "cboFileFormat")
        Me.cboFileFormat.Name = "cboFileFormat"
        '
        'Label3
        '
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Name = "Label3"
        '
        'ToolTip1
        '
        Me.ToolTip1.AutoPopDelay = 6000
        Me.ToolTip1.InitialDelay = 500
        Me.ToolTip1.ReshowDelay = 100
        '
        'cboCategory
        '
        Me.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCategory.FormattingEnabled = True
        resources.ApplyResources(Me.cboCategory, "cboCategory")
        Me.cboCategory.Name = "cboCategory"
        '
        'Label4
        '
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.Name = "Label4"
        '
        'chkAlwaysClearDeletedItems
        '
        resources.ApplyResources(Me.chkAlwaysClearDeletedItems, "chkAlwaysClearDeletedItems")
        Me.chkAlwaysClearDeletedItems.Name = "chkAlwaysClearDeletedItems"
        Me.chkAlwaysClearDeletedItems.UseVisualStyleBackColor = True
        '
        'chkAlwaysEmbedAttachments
        '
        resources.ApplyResources(Me.chkAlwaysEmbedAttachments, "chkAlwaysEmbedAttachments")
        Me.chkAlwaysEmbedAttachments.Name = "chkAlwaysEmbedAttachments"
        Me.chkAlwaysEmbedAttachments.UseVisualStyleBackColor = True
        '
        'chkLeaveCopy
        '
        resources.ApplyResources(Me.chkLeaveCopy, "chkLeaveCopy")
        Me.chkLeaveCopy.Name = "chkLeaveCopy"
        Me.chkLeaveCopy.UseVisualStyleBackColor = True
        '
        'chkADS
        '
        resources.ApplyResources(Me.chkADS, "chkADS")
        Me.chkADS.Name = "chkADS"
        Me.chkADS.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        resources.ApplyResources(Me.TabControl1, "TabControl1")
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.chkMonitorSentItems)
        Me.TabPage1.Controls.Add(Me.chkAlwaysClearDeletedItems)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.chkAlwaysEmbedAttachments)
        Me.TabPage1.Controls.Add(Me.cboCategory)
        Me.TabPage1.Controls.Add(Me.chkLeaveCopy)
        Me.TabPage1.Controls.Add(Me.chkADS)
        resources.ApplyResources(Me.TabPage1, "TabPage1")
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'chkMonitorSentItems
        '
        resources.ApplyResources(Me.chkMonitorSentItems, "chkMonitorSentItems")
        Me.chkMonitorSentItems.Name = "chkMonitorSentItems"
        Me.chkMonitorSentItems.UseVisualStyleBackColor = True
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.btnAutoArchiveOut)
        Me.TabPage3.Controls.Add(Me.btnAutoArchiveIn)
        Me.TabPage3.Controls.Add(Me.txtAutoArchiveOut)
        Me.TabPage3.Controls.Add(Me.txtAutoArchiveIn)
        Me.TabPage3.Controls.Add(Me.chkAutoArchiveOut)
        Me.TabPage3.Controls.Add(Me.chkAutoArchiveIn)
        resources.ApplyResources(Me.TabPage3, "TabPage3")
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'btnAutoArchiveOut
        '
        resources.ApplyResources(Me.btnAutoArchiveOut, "btnAutoArchiveOut")
        Me.btnAutoArchiveOut.Name = "btnAutoArchiveOut"
        Me.btnAutoArchiveOut.UseVisualStyleBackColor = True
        '
        'btnAutoArchiveIn
        '
        resources.ApplyResources(Me.btnAutoArchiveIn, "btnAutoArchiveIn")
        Me.btnAutoArchiveIn.Name = "btnAutoArchiveIn"
        Me.btnAutoArchiveIn.UseVisualStyleBackColor = True
        '
        'txtAutoArchiveOut
        '
        resources.ApplyResources(Me.txtAutoArchiveOut, "txtAutoArchiveOut")
        Me.txtAutoArchiveOut.Name = "txtAutoArchiveOut"
        '
        'txtAutoArchiveIn
        '
        resources.ApplyResources(Me.txtAutoArchiveIn, "txtAutoArchiveIn")
        Me.txtAutoArchiveIn.Name = "txtAutoArchiveIn"
        '
        'chkAutoArchiveOut
        '
        resources.ApplyResources(Me.chkAutoArchiveOut, "chkAutoArchiveOut")
        Me.chkAutoArchiveOut.Name = "chkAutoArchiveOut"
        Me.chkAutoArchiveOut.UseVisualStyleBackColor = True
        '
        'chkAutoArchiveIn
        '
        resources.ApplyResources(Me.chkAutoArchiveIn, "chkAutoArchiveIn")
        Me.chkAutoArchiveIn.Name = "chkAutoArchiveIn"
        Me.chkAutoArchiveIn.UseVisualStyleBackColor = True
        '
        'frmSettings
        '
        Me.AcceptButton = Me.OK_Button
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSettings"
        Me.ShowInTaskbar = False
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents cboFileFormat As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cboCategory As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents chkADS As System.Windows.Forms.CheckBox
    Friend WithEvents FileNameFilterBox1 As eMailManager.FileNameFilterBox
    Friend WithEvents chkLeaveCopy As System.Windows.Forms.CheckBox
    Friend WithEvents chkAlwaysEmbedAttachments As System.Windows.Forms.CheckBox
    Friend WithEvents chkAlwaysClearDeletedItems As System.Windows.Forms.CheckBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents btnAutoArchiveOut As System.Windows.Forms.Button
    Friend WithEvents btnAutoArchiveIn As System.Windows.Forms.Button
    Friend WithEvents txtAutoArchiveOut As System.Windows.Forms.TextBox
    Friend WithEvents txtAutoArchiveIn As System.Windows.Forms.TextBox
    Friend WithEvents chkAutoArchiveOut As System.Windows.Forms.CheckBox
    Friend WithEvents chkAutoArchiveIn As System.Windows.Forms.CheckBox
    Friend WithEvents chkMonitorSentItems As System.Windows.Forms.CheckBox

End Class
