
namespace eMailManager
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.checkboxLeaveCopy = new System.Windows.Forms.CheckBox();
            this.GroupBoxMessageDetails = new System.Windows.Forms.GroupBox();
            this.labelMailTo = new System.Windows.Forms.Label();
            this.labelMailSubject = new System.Windows.Forms.Label();
            this.labelMailFrom = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.buttonFileMessage = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.GroupBoxWorkflow = new System.Windows.Forms.GroupBox();
            this.rdoWorkflow02 = new System.Windows.Forms.RadioButton();
            this.rdoWorkflow01 = new System.Windows.Forms.RadioButton();
            this.rdoWorkflow03 = new System.Windows.Forms.RadioButton();
            this.GroupBoxLocation = new System.Windows.Forms.GroupBox();
            this.sfDataGrid1 = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.radioOutlookFolder = new System.Windows.Forms.RadioButton();
            this.radioFileFolder = new System.Windows.Forms.RadioButton();
            this.btnCancel = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.FolderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.SaveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.GroupBoxMessageDetails.SuspendLayout();
            this.GroupBoxWorkflow.SuspendLayout();
            this.GroupBoxLocation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sfDataGrid1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkboxLeaveCopy
            // 
            this.checkboxLeaveCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkboxLeaveCopy.AutoSize = true;
            this.checkboxLeaveCopy.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkboxLeaveCopy.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkboxLeaveCopy.Location = new System.Drawing.Point(522, 390);
            this.checkboxLeaveCopy.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.checkboxLeaveCopy.Name = "checkboxLeaveCopy";
            this.checkboxLeaveCopy.Size = new System.Drawing.Size(172, 17);
            this.checkboxLeaveCopy.TabIndex = 21;
            this.checkboxLeaveCopy.Text = "Leave a copy in your mailbox";
            this.checkboxLeaveCopy.UseVisualStyleBackColor = true;
            // 
            // GroupBoxMessageDetails
            // 
            this.GroupBoxMessageDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBoxMessageDetails.Controls.Add(this.labelMailTo);
            this.GroupBoxMessageDetails.Controls.Add(this.labelMailSubject);
            this.GroupBoxMessageDetails.Controls.Add(this.labelMailFrom);
            this.GroupBoxMessageDetails.Controls.Add(this.Label2);
            this.GroupBoxMessageDetails.Controls.Add(this.Label3);
            this.GroupBoxMessageDetails.Controls.Add(this.Label1);
            this.GroupBoxMessageDetails.Location = new System.Drawing.Point(8, 12);
            this.GroupBoxMessageDetails.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.GroupBoxMessageDetails.Name = "GroupBoxMessageDetails";
            this.GroupBoxMessageDetails.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.GroupBoxMessageDetails.Size = new System.Drawing.Size(686, 76);
            this.GroupBoxMessageDetails.TabIndex = 19;
            this.GroupBoxMessageDetails.TabStop = false;
            this.GroupBoxMessageDetails.Text = "Message Details:";
            // 
            // labelMailTo
            // 
            this.labelMailTo.AutoSize = true;
            this.labelMailTo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelMailTo.Location = new System.Drawing.Point(85, 36);
            this.labelMailTo.Margin = new System.Windows.Forms.Padding(0);
            this.labelMailTo.Name = "labelMailTo";
            this.labelMailTo.Size = new System.Drawing.Size(54, 13);
            this.labelMailTo.TabIndex = 1;
            this.labelMailTo.Text = "lblMailTo";
            // 
            // labelMailSubject
            // 
            this.labelMailSubject.AutoSize = true;
            this.labelMailSubject.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelMailSubject.Location = new System.Drawing.Point(85, 54);
            this.labelMailSubject.Margin = new System.Windows.Forms.Padding(0);
            this.labelMailSubject.Name = "labelMailSubject";
            this.labelMailSubject.Size = new System.Drawing.Size(80, 13);
            this.labelMailSubject.TabIndex = 1;
            this.labelMailSubject.Text = "lblMailSubject";
            // 
            // labelMailFrom
            // 
            this.labelMailFrom.AutoSize = true;
            this.labelMailFrom.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelMailFrom.Location = new System.Drawing.Point(85, 18);
            this.labelMailFrom.Margin = new System.Windows.Forms.Padding(0);
            this.labelMailFrom.Name = "labelMailFrom";
            this.labelMailFrom.Size = new System.Drawing.Size(68, 13);
            this.labelMailFrom.TabIndex = 1;
            this.labelMailFrom.Text = "lblMailFrom";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label2.Location = new System.Drawing.Point(8, 36);
            this.Label2.Margin = new System.Windows.Forms.Padding(0);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(22, 13);
            this.Label2.TabIndex = 0;
            this.Label2.Text = "To:";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label3.Location = new System.Drawing.Point(8, 54);
            this.Label3.Margin = new System.Windows.Forms.Padding(0);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(48, 13);
            this.Label3.TabIndex = 0;
            this.Label3.Text = "Subject:";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label1.Location = new System.Drawing.Point(8, 18);
            this.Label1.Margin = new System.Windows.Forms.Padding(0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(36, 13);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "From:";
            // 
            // buttonFileMessage
            // 
            this.buttonFileMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonFileMessage.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonFileMessage.Enabled = false;
            this.buttonFileMessage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFileMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.buttonFileMessage.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonFileMessage.Location = new System.Drawing.Point(579, 419);
            this.buttonFileMessage.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.buttonFileMessage.Name = "buttonFileMessage";
            this.buttonFileMessage.Size = new System.Drawing.Size(117, 73);
            this.buttonFileMessage.TabIndex = 17;
            this.buttonFileMessage.Text = "File Message";
            this.buttonFileMessage.Click += new System.EventHandler(this.buttonFileMessage_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDelete.Location = new System.Drawing.Point(457, 419);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(113, 29);
            this.btnDelete.TabIndex = 15;
            this.btnDelete.Text = "Delete Message";
            this.btnDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // GroupBoxWorkflow
            // 
            this.GroupBoxWorkflow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBoxWorkflow.Controls.Add(this.rdoWorkflow02);
            this.GroupBoxWorkflow.Controls.Add(this.rdoWorkflow01);
            this.GroupBoxWorkflow.Controls.Add(this.rdoWorkflow03);
            this.GroupBoxWorkflow.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.GroupBoxWorkflow.Location = new System.Drawing.Point(9, 388);
            this.GroupBoxWorkflow.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.GroupBoxWorkflow.Name = "GroupBoxWorkflow";
            this.GroupBoxWorkflow.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.GroupBoxWorkflow.Size = new System.Drawing.Size(434, 105);
            this.GroupBoxWorkflow.TabIndex = 18;
            this.GroupBoxWorkflow.TabStop = false;
            this.GroupBoxWorkflow.Text = "Workflow:";
            // 
            // rdoWorkflow02
            // 
            this.rdoWorkflow02.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rdoWorkflow02.BackColor = System.Drawing.SystemColors.Control;
            this.rdoWorkflow02.Checked = true;
            this.rdoWorkflow02.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rdoWorkflow02.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rdoWorkflow02.Location = new System.Drawing.Point(8, 40);
            this.rdoWorkflow02.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.rdoWorkflow02.Name = "rdoWorkflow02";
            this.rdoWorkflow02.Size = new System.Drawing.Size(420, 30);
            this.rdoWorkflow02.TabIndex = 1;
            this.rdoWorkflow02.TabStop = true;
            this.rdoWorkflow02.Text = "File message and attachments separately (save the attachments in file system)";
            this.rdoWorkflow02.UseVisualStyleBackColor = false;
            // 
            // rdoWorkflow01
            // 
            this.rdoWorkflow01.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rdoWorkflow01.BackColor = System.Drawing.SystemColors.Control;
            this.rdoWorkflow01.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rdoWorkflow01.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rdoWorkflow01.Location = new System.Drawing.Point(8, 11);
            this.rdoWorkflow01.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.rdoWorkflow01.Name = "rdoWorkflow01";
            this.rdoWorkflow01.Size = new System.Drawing.Size(420, 34);
            this.rdoWorkflow01.TabIndex = 1;
            this.rdoWorkflow01.Text = "File message (any attachments remain embedded)";
            this.rdoWorkflow01.UseVisualStyleBackColor = false;
            // 
            // rdoWorkflow03
            // 
            this.rdoWorkflow03.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rdoWorkflow03.BackColor = System.Drawing.SystemColors.Control;
            this.rdoWorkflow03.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rdoWorkflow03.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rdoWorkflow03.Location = new System.Drawing.Point(8, 69);
            this.rdoWorkflow03.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.rdoWorkflow03.Name = "rdoWorkflow03";
            this.rdoWorkflow03.Size = new System.Drawing.Size(420, 30);
            this.rdoWorkflow03.TabIndex = 1;
            this.rdoWorkflow03.Text = "File message and attachments separately (keep a copy of attachments in message)";
            this.rdoWorkflow03.UseVisualStyleBackColor = false;
            // 
            // GroupBoxLocation
            // 
            this.GroupBoxLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBoxLocation.Controls.Add(this.sfDataGrid1);
            this.GroupBoxLocation.Controls.Add(this.btnBrowse);
            this.GroupBoxLocation.Controls.Add(this.radioOutlookFolder);
            this.GroupBoxLocation.Controls.Add(this.radioFileFolder);
            this.GroupBoxLocation.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.GroupBoxLocation.Location = new System.Drawing.Point(8, 94);
            this.GroupBoxLocation.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.GroupBoxLocation.Name = "GroupBoxLocation";
            this.GroupBoxLocation.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.GroupBoxLocation.Size = new System.Drawing.Size(686, 290);
            this.GroupBoxLocation.TabIndex = 14;
            this.GroupBoxLocation.TabStop = false;
            this.GroupBoxLocation.Text = "Location:";
            // 
            // sfDataGrid1
            // 
            this.sfDataGrid1.AccessibleName = "Table";
            this.sfDataGrid1.AllowDeleting = true;
            this.sfDataGrid1.AllowFiltering = true;
            this.sfDataGrid1.AllowResizingColumns = true;
            this.sfDataGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sfDataGrid1.AutoGenerateColumns = false;
            this.sfDataGrid1.Location = new System.Drawing.Point(11, 46);
            this.sfDataGrid1.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.sfDataGrid1.Name = "sfDataGrid1";
            this.sfDataGrid1.Size = new System.Drawing.Size(665, 238);
            this.sfDataGrid1.TabIndex = 10;
            this.sfDataGrid1.Text = "sfDataGrid1";
            this.sfDataGrid1.ValidationMode = Syncfusion.WinForms.DataGrid.Enums.GridValidationMode.InEdit;
            this.sfDataGrid1.SelectionChanged += new Syncfusion.WinForms.DataGrid.Events.SelectionChangedEventHandler(this.sfDataGrid1_SelectionChanged);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowse.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnBrowse.Location = new System.Drawing.Point(481, 16);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(195, 24);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "Browse for save location...";
            this.btnBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // radioOutlookFolder
            // 
            this.radioOutlookFolder.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.radioOutlookFolder.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radioOutlookFolder.Location = new System.Drawing.Point(140, 17);
            this.radioOutlookFolder.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.radioOutlookFolder.Name = "radioOutlookFolder";
            this.radioOutlookFolder.Size = new System.Drawing.Size(110, 24);
            this.radioOutlookFolder.TabIndex = 0;
            this.radioOutlookFolder.Text = "Outlook folder";
            this.radioOutlookFolder.CheckedChanged += new System.EventHandler(this.radioOutlookFolder_CheckedChanged);
            // 
            // radioFileFolder
            // 
            this.radioFileFolder.Checked = true;
            this.radioFileFolder.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.radioFileFolder.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radioFileFolder.Location = new System.Drawing.Point(8, 17);
            this.radioFileFolder.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.radioFileFolder.Name = "radioFileFolder";
            this.radioFileFolder.Size = new System.Drawing.Size(110, 24);
            this.radioFileFolder.TabIndex = 0;
            this.radioFileFolder.TabStop = true;
            this.radioFileFolder.Text = "File system folder";
            this.radioFileFolder.CheckedChanged += new System.EventHandler(this.radioFileFolder_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCancel.Location = new System.Drawing.Point(457, 454);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(113, 38);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "Do not file message";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 498);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.statusStrip1.Size = new System.Drawing.Size(705, 22);
            this.statusStrip1.TabIndex = 22;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(0, 16);
            this.toolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.toolStripProgressBar1.Visible = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = global::eMailManager.Properties.Settings.Default.FormBackColor;
            this.ClientSize = new System.Drawing.Size(705, 520);
            this.ControlBox = false;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.checkboxLeaveCopy);
            this.Controls.Add(this.GroupBoxMessageDetails);
            this.Controls.Add(this.buttonFileMessage);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.GroupBoxWorkflow);
            this.Controls.Add(this.GroupBoxLocation);
            this.Controls.Add(this.btnCancel);
            this.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::eMailManager.Properties.Settings.Default, "FormBackColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Font = global::eMailManager.Properties.Settings.Default.FormFont;
            this.ForeColor = global::eMailManager.Properties.Settings.Default.FormForeColour;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.Name = "FormMain";
            this.Text = "eMail Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.GroupBoxMessageDetails.ResumeLayout(false);
            this.GroupBoxMessageDetails.PerformLayout();
            this.GroupBoxWorkflow.ResumeLayout(false);
            this.GroupBoxLocation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sfDataGrid1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.CheckBox checkboxLeaveCopy;
        internal System.Windows.Forms.GroupBox GroupBoxMessageDetails;
        internal System.Windows.Forms.Label labelMailTo;
        internal System.Windows.Forms.Label labelMailSubject;
        internal System.Windows.Forms.Label labelMailFrom;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Button buttonFileMessage;
        private System.Windows.Forms.Button btnDelete;
        internal System.Windows.Forms.GroupBox GroupBoxWorkflow;
        private System.Windows.Forms.RadioButton rdoWorkflow02;
        private System.Windows.Forms.RadioButton rdoWorkflow01;
        private System.Windows.Forms.RadioButton rdoWorkflow03;
        internal System.Windows.Forms.GroupBox GroupBoxLocation;
        private System.Windows.Forms.Button btnBrowse;
        internal System.Windows.Forms.RadioButton radioFileFolder;
        private System.Windows.Forms.RadioButton radioOutlookFolder;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        internal System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog1;
        internal System.Windows.Forms.SaveFileDialog SaveFileDialog1;
        internal System.Windows.Forms.ToolTip ToolTip1;
        private Syncfusion.WinForms.DataGrid.SfDataGrid sfDataGrid1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
    }
}