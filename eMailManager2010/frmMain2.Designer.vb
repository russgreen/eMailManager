<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain2
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
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.lblMailTo = New System.Windows.Forms.Label()
        Me.lblMailSubject = New System.Windows.Forms.Label()
        Me.lblMailFrom = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripProgressBar1 = New System.Windows.Forms.ToolStripProgressBar()
        Me.btnFileMessage = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rdoArchiveDelete = New System.Windows.Forms.RadioButton()
        Me.rdoArchiveRetain = New System.Windows.Forms.RadioButton()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.lblQuote = New System.Windows.Forms.Label()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.chkPrivate = New System.Windows.Forms.CheckBox()
        Me.txtSubject = New System.Windows.Forms.TextBox()
        Me.MaskedTextBox1 = New System.Windows.Forms.MaskedTextBox()
        Me.lblFilename = New System.Windows.Forms.Label()
        Me.lblSavePath = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.GroupBox3.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.lblMailTo)
        Me.GroupBox3.Controls.Add(Me.lblMailSubject)
        Me.GroupBox3.Controls.Add(Me.lblMailFrom)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(622, 85)
        Me.GroupBox3.TabIndex = 11
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Message Details"
        '
        'lblMailTo
        '
        Me.lblMailTo.AutoSize = True
        Me.lblMailTo.Location = New System.Drawing.Point(58, 44)
        Me.lblMailTo.Name = "lblMailTo"
        Me.lblMailTo.Size = New System.Drawing.Size(49, 13)
        Me.lblMailTo.TabIndex = 1
        Me.lblMailTo.Text = "lblMailTo"
        '
        'lblMailSubject
        '
        Me.lblMailSubject.AutoSize = True
        Me.lblMailSubject.Location = New System.Drawing.Point(58, 62)
        Me.lblMailSubject.Name = "lblMailSubject"
        Me.lblMailSubject.Size = New System.Drawing.Size(72, 13)
        Me.lblMailSubject.TabIndex = 1
        Me.lblMailSubject.Text = "lblMailSubject"
        '
        'lblMailFrom
        '
        Me.lblMailFrom.AutoSize = True
        Me.lblMailFrom.Location = New System.Drawing.Point(58, 25)
        Me.lblMailFrom.Name = "lblMailFrom"
        Me.lblMailFrom.Size = New System.Drawing.Size(59, 13)
        Me.lblMailFrom.TabIndex = 1
        Me.lblMailFrom.Text = "lblMailFrom"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 44)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(23, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "To:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 62)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Subject:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(33, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "From:"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.ToolStripProgressBar1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 332)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(642, 22)
        Me.StatusStrip1.TabIndex = 12
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(627, 17)
        Me.ToolStripStatusLabel1.Spring = True
        Me.ToolStripStatusLabel1.Text = "ToolStripStatusLabel1"
        '
        'ToolStripProgressBar1
        '
        Me.ToolStripProgressBar1.Name = "ToolStripProgressBar1"
        Me.ToolStripProgressBar1.Size = New System.Drawing.Size(600, 16)
        Me.ToolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        '
        'btnFileMessage
        '
        Me.btnFileMessage.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFileMessage.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnFileMessage.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnFileMessage.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFileMessage.Location = New System.Drawing.Point(404, 282)
        Me.btnFileMessage.Name = "btnFileMessage"
        Me.btnFileMessage.Size = New System.Drawing.Size(112, 40)
        Me.btnFileMessage.TabIndex = 13
        Me.btnFileMessage.Text = "File Message"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rdoArchiveDelete)
        Me.GroupBox1.Controls.Add(Me.rdoArchiveRetain)
        Me.GroupBox1.Controls.Add(Me.btnBrowse)
        Me.GroupBox1.Controls.Add(Me.lblQuote)
        Me.GroupBox1.Controls.Add(Me.btnSelect)
        Me.GroupBox1.Controls.Add(Me.chkPrivate)
        Me.GroupBox1.Controls.Add(Me.txtSubject)
        Me.GroupBox1.Controls.Add(Me.MaskedTextBox1)
        Me.GroupBox1.Controls.Add(Me.lblFilename)
        Me.GroupBox1.Controls.Add(Me.lblSavePath)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 103)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(622, 169)
        Me.GroupBox1.TabIndex = 14
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Message File Details"
        '
        'rdoArchiveDelete
        '
        Me.rdoArchiveDelete.AutoSize = True
        Me.rdoArchiveDelete.Location = New System.Drawing.Point(392, 83)
        Me.rdoArchiveDelete.Name = "rdoArchiveDelete"
        Me.rdoArchiveDelete.Size = New System.Drawing.Size(104, 17)
        Me.rdoArchiveDelete.TabIndex = 8
        Me.rdoArchiveDelete.TabStop = True
        Me.rdoArchiveDelete.Text = "Archive + Delete"
        Me.rdoArchiveDelete.UseVisualStyleBackColor = True
        '
        'rdoArchiveRetain
        '
        Me.rdoArchiveRetain.AutoSize = True
        Me.rdoArchiveRetain.Location = New System.Drawing.Point(220, 83)
        Me.rdoArchiveRetain.Name = "rdoArchiveRetain"
        Me.rdoArchiveRetain.Size = New System.Drawing.Size(99, 17)
        Me.rdoArchiveRetain.TabIndex = 7
        Me.rdoArchiveRetain.TabStop = True
        Me.rdoArchiveRetain.Text = "Archive + retain"
        Me.rdoArchiveRetain.UseVisualStyleBackColor = True
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(529, 19)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(78, 23)
        Me.btnBrowse.TabIndex = 6
        Me.btnBrowse.Text = "Browse..."
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'lblQuote
        '
        Me.lblQuote.AutoSize = True
        Me.lblQuote.Location = New System.Drawing.Point(217, 25)
        Me.lblQuote.Name = "lblQuote"
        Me.lblQuote.Size = New System.Drawing.Size(66, 13)
        Me.lblQuote.TabIndex = 5
        Me.lblQuote.Text = "Q10-000000"
        '
        'btnSelect
        '
        Me.btnSelect.Enabled = False
        Me.btnSelect.Location = New System.Drawing.Point(164, 20)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(47, 23)
        Me.btnSelect.TabIndex = 4
        Me.btnSelect.Text = "<<<<<"
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'chkPrivate
        '
        Me.chkPrivate.AutoSize = True
        Me.chkPrivate.Location = New System.Drawing.Point(9, 83)
        Me.chkPrivate.Name = "chkPrivate"
        Me.chkPrivate.Size = New System.Drawing.Size(81, 17)
        Me.chkPrivate.TabIndex = 3
        Me.chkPrivate.Text = "Confidential"
        Me.chkPrivate.UseVisualStyleBackColor = True
        '
        'txtSubject
        '
        Me.txtSubject.Location = New System.Drawing.Point(58, 48)
        Me.txtSubject.Name = "txtSubject"
        Me.txtSubject.Size = New System.Drawing.Size(549, 20)
        Me.txtSubject.TabIndex = 2
        '
        'MaskedTextBox1
        '
        Me.MaskedTextBox1.Location = New System.Drawing.Point(58, 22)
        Me.MaskedTextBox1.Mask = "000000"
        Me.MaskedTextBox1.Name = "MaskedTextBox1"
        Me.MaskedTextBox1.Size = New System.Drawing.Size(100, 20)
        Me.MaskedTextBox1.TabIndex = 1
        '
        'lblFilename
        '
        Me.lblFilename.AutoSize = True
        Me.lblFilename.Location = New System.Drawing.Point(58, 140)
        Me.lblFilename.Name = "lblFilename"
        Me.lblFilename.Size = New System.Drawing.Size(59, 13)
        Me.lblFilename.TabIndex = 1
        Me.lblFilename.Text = "lblFilename"
        '
        'lblSavePath
        '
        Me.lblSavePath.AutoSize = True
        Me.lblSavePath.Location = New System.Drawing.Point(58, 115)
        Me.lblSavePath.Name = "lblSavePath"
        Me.lblSavePath.Size = New System.Drawing.Size(64, 13)
        Me.lblSavePath.TabIndex = 1
        Me.lblSavePath.Text = "lblSavePath"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 25)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(39, 13)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Quote:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 140)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(52, 13)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Filename:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 115)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(47, 13)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Save to:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 51)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(46, 13)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Subject:"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(522, 282)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(112, 40)
        Me.btnCancel.TabIndex = 15
        Me.btnCancel.Text = "Cancel"
        '
        'frmMain2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(642, 354)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnFileMessage)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.GroupBox3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmMain2"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmMain2"
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents lblMailTo As System.Windows.Forms.Label
    Friend WithEvents lblMailSubject As System.Windows.Forms.Label
    Friend WithEvents lblMailFrom As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripProgressBar1 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents btnFileMessage As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chkPrivate As System.Windows.Forms.CheckBox
    Friend WithEvents txtSubject As System.Windows.Forms.TextBox
    Friend WithEvents MaskedTextBox1 As System.Windows.Forms.MaskedTextBox
    Friend WithEvents lblFilename As System.Windows.Forms.Label
    Friend WithEvents lblSavePath As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents lblQuote As System.Windows.Forms.Label
    Friend WithEvents btnSelect As System.Windows.Forms.Button
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents rdoArchiveDelete As System.Windows.Forms.RadioButton
    Friend WithEvents rdoArchiveRetain As System.Windows.Forms.RadioButton
End Class
