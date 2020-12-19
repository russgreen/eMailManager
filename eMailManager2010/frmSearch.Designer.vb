<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSearch
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSearch))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.htmMessageBody = New System.Windows.Forms.WebBrowser()
        Me.txtMessageBody = New System.Windows.Forms.TextBox()
        Me.rtbMessageBody = New System.Windows.Forms.RichTextBox()
        Me.lstattachments = New System.Windows.Forms.ListBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblSubject = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblCc = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnPopLocations = New System.Windows.Forms.Button()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.rdoFileSystem = New System.Windows.Forms.RadioButton()
        Me.chkSubdirs = New System.Windows.Forms.CheckBox()
        Me.txtLocation = New System.Windows.Forms.TextBox()
        Me.rdoOutlook = New System.Windows.Forms.RadioButton()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.gbSimple = New System.Windows.Forms.GroupBox()
        Me.txtSimpleSearch = New System.Windows.Forms.TextBox()
        Me.gbDetailed = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtCc = New System.Windows.Forms.TextBox()
        Me.gpSearchDates = New System.Windows.Forms.GroupBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtBody = New System.Windows.Forms.TextBox()
        Me.txtTo = New System.Windows.Forms.TextBox()
        Me.txtSubject = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtFrom = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.gbSearchType = New System.Windows.Forms.GroupBox()
        Me.rdoSearchDetailedFull = New System.Windows.Forms.RadioButton()
        Me.rdoSearchSimple = New System.Windows.Forms.RadioButton()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.ExpTree2 = New ExpTreeLib.ExpTree()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripProgressBar1 = New System.Windows.Forms.ToolStripProgressBar()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ListView1 = New eMailManager.ffListView()
        Me.chMsg = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chFrom = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chTo = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chCc = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chSubject = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chDate = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chAtts = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.gbSimple.SuspendLayout()
        Me.gbDetailed.SuspendLayout()
        Me.gpSearchDates.SuspendLayout()
        Me.gbSearchType.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        resources.ApplyResources(Me.SplitContainer1, "SplitContainer1")
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.ListView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.htmMessageBody)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtMessageBody)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rtbMessageBody)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lstattachments)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Panel2)
        '
        'htmMessageBody
        '
        resources.ApplyResources(Me.htmMessageBody, "htmMessageBody")
        Me.htmMessageBody.Name = "htmMessageBody"
        '
        'txtMessageBody
        '
        resources.ApplyResources(Me.txtMessageBody, "txtMessageBody")
        Me.txtMessageBody.Name = "txtMessageBody"
        Me.txtMessageBody.TabStop = False
        '
        'rtbMessageBody
        '
        resources.ApplyResources(Me.rtbMessageBody, "rtbMessageBody")
        Me.rtbMessageBody.Name = "rtbMessageBody"
        Me.rtbMessageBody.TabStop = False
        '
        'lstattachments
        '
        resources.ApplyResources(Me.lstattachments, "lstattachments")
        Me.lstattachments.FormattingEnabled = True
        Me.lstattachments.Name = "lstattachments"
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.lblSubject)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.lblCc)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.lblTo)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.lblFrom)
        resources.ApplyResources(Me.Panel2, "Panel2")
        Me.Panel2.Name = "Panel2"
        '
        'Label9
        '
        resources.ApplyResources(Me.Label9, "Label9")
        Me.Label9.Name = "Label9"
        '
        'lblSubject
        '
        resources.ApplyResources(Me.lblSubject, "lblSubject")
        Me.lblSubject.Name = "lblSubject"
        '
        'Label8
        '
        resources.ApplyResources(Me.Label8, "Label8")
        Me.Label8.Name = "Label8"
        '
        'lblCc
        '
        resources.ApplyResources(Me.lblCc, "lblCc")
        Me.lblCc.Name = "lblCc"
        '
        'Label7
        '
        resources.ApplyResources(Me.Label7, "Label7")
        Me.Label7.Name = "Label7"
        '
        'lblTo
        '
        resources.ApplyResources(Me.lblTo, "lblTo")
        Me.lblTo.Name = "lblTo"
        '
        'Label6
        '
        resources.ApplyResources(Me.Label6, "Label6")
        Me.Label6.Name = "Label6"
        '
        'lblFrom
        '
        resources.ApplyResources(Me.lblFrom, "lblFrom")
        Me.lblFrom.Name = "lblFrom"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.TabControl1)
        resources.ApplyResources(Me.Panel1, "Panel1")
        Me.Panel1.Name = "Panel1"
        '
        'TabControl1
        '
        resources.ApplyResources(Me.TabControl1, "TabControl1")
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Controls.Add(Me.btnSearch)
        Me.TabPage1.Controls.Add(Me.gbSimple)
        Me.TabPage1.Controls.Add(Me.gbDetailed)
        Me.TabPage1.Controls.Add(Me.gbSearchType)
        resources.ApplyResources(Me.TabPage1, "TabPage1")
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnPopLocations)
        Me.GroupBox1.Controls.Add(Me.btnBrowse)
        Me.GroupBox1.Controls.Add(Me.rdoFileSystem)
        Me.GroupBox1.Controls.Add(Me.chkSubdirs)
        Me.GroupBox1.Controls.Add(Me.txtLocation)
        Me.GroupBox1.Controls.Add(Me.rdoOutlook)
        resources.ApplyResources(Me.GroupBox1, "GroupBox1")
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.TabStop = False
        '
        'btnPopLocations
        '
        resources.ApplyResources(Me.btnPopLocations, "btnPopLocations")
        Me.btnPopLocations.Name = "btnPopLocations"
        Me.btnPopLocations.UseVisualStyleBackColor = True
        '
        'btnBrowse
        '
        resources.ApplyResources(Me.btnBrowse, "btnBrowse")
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'rdoFileSystem
        '
        resources.ApplyResources(Me.rdoFileSystem, "rdoFileSystem")
        Me.rdoFileSystem.Checked = True
        Me.rdoFileSystem.Name = "rdoFileSystem"
        Me.rdoFileSystem.TabStop = True
        Me.rdoFileSystem.UseVisualStyleBackColor = True
        '
        'chkSubdirs
        '
        resources.ApplyResources(Me.chkSubdirs, "chkSubdirs")
        Me.chkSubdirs.Name = "chkSubdirs"
        Me.chkSubdirs.UseVisualStyleBackColor = True
        '
        'txtLocation
        '
        Me.txtLocation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtLocation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        resources.ApplyResources(Me.txtLocation, "txtLocation")
        Me.txtLocation.Name = "txtLocation"
        '
        'rdoOutlook
        '
        resources.ApplyResources(Me.rdoOutlook, "rdoOutlook")
        Me.rdoOutlook.Name = "rdoOutlook"
        Me.rdoOutlook.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        resources.ApplyResources(Me.btnSearch, "btnSearch")
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'gbSimple
        '
        Me.gbSimple.Controls.Add(Me.txtSimpleSearch)
        resources.ApplyResources(Me.gbSimple, "gbSimple")
        Me.gbSimple.Name = "gbSimple"
        Me.gbSimple.TabStop = False
        '
        'txtSimpleSearch
        '
        resources.ApplyResources(Me.txtSimpleSearch, "txtSimpleSearch")
        Me.txtSimpleSearch.Name = "txtSimpleSearch"
        '
        'gbDetailed
        '
        Me.gbDetailed.Controls.Add(Me.Label2)
        Me.gbDetailed.Controls.Add(Me.txtCc)
        Me.gbDetailed.Controls.Add(Me.gpSearchDates)
        Me.gbDetailed.Controls.Add(Me.Label1)
        Me.gbDetailed.Controls.Add(Me.Label5)
        Me.gbDetailed.Controls.Add(Me.txtBody)
        Me.gbDetailed.Controls.Add(Me.txtTo)
        Me.gbDetailed.Controls.Add(Me.txtSubject)
        Me.gbDetailed.Controls.Add(Me.Label4)
        Me.gbDetailed.Controls.Add(Me.txtFrom)
        Me.gbDetailed.Controls.Add(Me.Label3)
        resources.ApplyResources(Me.gbDetailed, "gbDetailed")
        Me.gbDetailed.Name = "gbDetailed"
        Me.gbDetailed.TabStop = False
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        '
        'txtCc
        '
        resources.ApplyResources(Me.txtCc, "txtCc")
        Me.txtCc.Name = "txtCc"
        '
        'gpSearchDates
        '
        Me.gpSearchDates.Controls.Add(Me.Label11)
        Me.gpSearchDates.Controls.Add(Me.Label10)
        Me.gpSearchDates.Controls.Add(Me.dtpTo)
        Me.gpSearchDates.Controls.Add(Me.dtpFrom)
        resources.ApplyResources(Me.gpSearchDates, "gpSearchDates")
        Me.gpSearchDates.Name = "gpSearchDates"
        Me.gpSearchDates.TabStop = False
        '
        'Label11
        '
        resources.ApplyResources(Me.Label11, "Label11")
        Me.Label11.Name = "Label11"
        '
        'Label10
        '
        resources.ApplyResources(Me.Label10, "Label10")
        Me.Label10.Name = "Label10"
        '
        'dtpTo
        '
        resources.ApplyResources(Me.dtpTo, "dtpTo")
        Me.dtpTo.Name = "dtpTo"
        '
        'dtpFrom
        '
        resources.ApplyResources(Me.dtpFrom, "dtpFrom")
        Me.dtpFrom.Name = "dtpFrom"
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'Label5
        '
        resources.ApplyResources(Me.Label5, "Label5")
        Me.Label5.Name = "Label5"
        '
        'txtBody
        '
        resources.ApplyResources(Me.txtBody, "txtBody")
        Me.txtBody.Name = "txtBody"
        '
        'txtTo
        '
        resources.ApplyResources(Me.txtTo, "txtTo")
        Me.txtTo.Name = "txtTo"
        '
        'txtSubject
        '
        resources.ApplyResources(Me.txtSubject, "txtSubject")
        Me.txtSubject.Name = "txtSubject"
        '
        'Label4
        '
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.Name = "Label4"
        '
        'txtFrom
        '
        resources.ApplyResources(Me.txtFrom, "txtFrom")
        Me.txtFrom.Name = "txtFrom"
        '
        'Label3
        '
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Name = "Label3"
        '
        'gbSearchType
        '
        Me.gbSearchType.Controls.Add(Me.rdoSearchDetailedFull)
        Me.gbSearchType.Controls.Add(Me.rdoSearchSimple)
        resources.ApplyResources(Me.gbSearchType, "gbSearchType")
        Me.gbSearchType.Name = "gbSearchType"
        Me.gbSearchType.TabStop = False
        '
        'rdoSearchDetailedFull
        '
        resources.ApplyResources(Me.rdoSearchDetailedFull, "rdoSearchDetailedFull")
        Me.rdoSearchDetailedFull.Checked = True
        Me.rdoSearchDetailedFull.Name = "rdoSearchDetailedFull"
        Me.rdoSearchDetailedFull.TabStop = True
        Me.rdoSearchDetailedFull.UseVisualStyleBackColor = True
        '
        'rdoSearchSimple
        '
        resources.ApplyResources(Me.rdoSearchSimple, "rdoSearchSimple")
        Me.rdoSearchSimple.Name = "rdoSearchSimple"
        Me.rdoSearchSimple.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.ExpTree2)
        resources.ApplyResources(Me.TabPage2, "TabPage2")
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'ExpTree2
        '
        Me.ExpTree2.AllowFolderRename = False
        Me.ExpTree2.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.ExpTree2, "ExpTree2")
        Me.ExpTree2.Name = "ExpTree2"
        Me.ExpTree2.ShowRootLines = False
        Me.ExpTree2.StartUpDirectory = ExpTreeLib.ExpTree.StartDir.Desktop
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripProgressBar1, Me.ToolStripStatusLabel1})
        resources.ApplyResources(Me.StatusStrip1, "StatusStrip1")
        Me.StatusStrip1.Name = "StatusStrip1"
        '
        'ToolStripProgressBar1
        '
        Me.ToolStripProgressBar1.Name = "ToolStripProgressBar1"
        resources.ApplyResources(Me.ToolStripProgressBar1, "ToolStripProgressBar1")
        Me.ToolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        resources.ApplyResources(Me.ToolStripStatusLabel1, "ToolStripStatusLabel1")
        '
        'ListView1
        '
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chMsg, Me.chFrom, Me.chTo, Me.chCc, Me.chSubject, Me.chDate, Me.chAtts})
        resources.ApplyResources(Me.ListView1, "ListView1")
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        Me.ListView1.HideSelection = False
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.TabStop = False
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'chMsg
        '
        resources.ApplyResources(Me.chMsg, "chMsg")
        '
        'chFrom
        '
        resources.ApplyResources(Me.chFrom, "chFrom")
        '
        'chTo
        '
        resources.ApplyResources(Me.chTo, "chTo")
        '
        'chCc
        '
        resources.ApplyResources(Me.chCc, "chCc")
        '
        'chSubject
        '
        resources.ApplyResources(Me.chSubject, "chSubject")
        '
        'chDate
        '
        resources.ApplyResources(Me.chDate, "chDate")
        '
        'chAtts
        '
        resources.ApplyResources(Me.chAtts, "chAtts")
        '
        'frmSearch
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.KeyPreview = True
        Me.Name = "frmSearch"
        Me.ShowIcon = False
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.gbSimple.ResumeLayout(False)
        Me.gbSimple.PerformLayout()
        Me.gbDetailed.ResumeLayout(False)
        Me.gbDetailed.PerformLayout()
        Me.gpSearchDates.ResumeLayout(False)
        Me.gpSearchDates.PerformLayout()
        Me.gbSearchType.ResumeLayout(False)
        Me.gbSearchType.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtBody As System.Windows.Forms.TextBox
    Friend WithEvents txtSubject As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents gbDetailed As System.Windows.Forms.GroupBox
    Friend WithEvents txtCc As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtTo As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtFrom As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents txtLocation As System.Windows.Forms.TextBox
    Friend WithEvents chMsg As System.Windows.Forms.ColumnHeader
    Friend WithEvents chkSubdirs As System.Windows.Forms.CheckBox
    Friend WithEvents chFrom As System.Windows.Forms.ColumnHeader
    Friend WithEvents chTo As System.Windows.Forms.ColumnHeader
    Friend WithEvents chSubject As System.Windows.Forms.ColumnHeader
    Friend WithEvents chDate As System.Windows.Forms.ColumnHeader
    Friend WithEvents rdoOutlook As System.Windows.Forms.RadioButton
    Friend WithEvents rdoFileSystem As System.Windows.Forms.RadioButton
    Friend WithEvents rtbMessageBody As System.Windows.Forms.RichTextBox
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents chAtts As System.Windows.Forms.ColumnHeader
    Friend WithEvents chCc As System.Windows.Forms.ColumnHeader
    Friend WithEvents lstattachments As System.Windows.Forms.ListBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents lblSubject As System.Windows.Forms.Label
    Friend WithEvents lblCc As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripProgressBar1 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents htmMessageBody As System.Windows.Forms.WebBrowser
    Friend WithEvents txtMessageBody As System.Windows.Forms.TextBox
    Friend WithEvents gpSearchDates As System.Windows.Forms.GroupBox
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents gbSearchType As System.Windows.Forms.GroupBox
    Friend WithEvents rdoSearchDetailedFull As System.Windows.Forms.RadioButton
    Friend WithEvents rdoSearchSimple As System.Windows.Forms.RadioButton
    Friend WithEvents gbSimple As System.Windows.Forms.GroupBox
    Friend WithEvents txtSimpleSearch As System.Windows.Forms.TextBox
    Friend WithEvents ListView1 As eMailManager.ffListView
    Friend WithEvents btnPopLocations As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents ExpTree2 As ExpTreeLib.ExpTree
End Class
