
namespace eMailManager
{
    partial class FormSettings
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.fileNameFilterBox1 = new eMailManager.Controls.FileNameFilterBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkboxMonitorSentItems = new System.Windows.Forms.CheckBox();
            this.checkboxAlwaysClearDeletedItems = new System.Windows.Forms.CheckBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.checkboxLeaveCopy = new System.Windows.Forms.CheckBox();
            this.OK_Button = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.fileNameFilterBox1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(408, 59);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filename filter";
            // 
            // fileNameFilterBox1
            // 
            this.fileNameFilterBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileNameFilterBox1.DefaultFileNameFilter = "<sent>_<from>_<subject>";
            this.fileNameFilterBox1.FileNameFilter = null;
            this.fileNameFilterBox1.Location = new System.Drawing.Point(7, 20);
            this.fileNameFilterBox1.Name = "fileNameFilterBox1";
            this.fileNameFilterBox1.Size = new System.Drawing.Size(395, 21);
            this.fileNameFilterBox1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.checkboxMonitorSentItems);
            this.groupBox2.Controls.Add(this.checkboxAlwaysClearDeletedItems);
            this.groupBox2.Controls.Add(this.Label4);
            this.groupBox2.Controls.Add(this.cboCategory);
            this.groupBox2.Controls.Add(this.checkboxLeaveCopy);
            this.groupBox2.Location = new System.Drawing.Point(12, 78);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(409, 117);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "General";
            // 
            // checkboxMonitorSentItems
            // 
            this.checkboxMonitorSentItems.AutoSize = true;
            this.checkboxMonitorSentItems.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkboxMonitorSentItems.Location = new System.Drawing.Point(9, 86);
            this.checkboxMonitorSentItems.Name = "checkboxMonitorSentItems";
            this.checkboxMonitorSentItems.Size = new System.Drawing.Size(140, 17);
            this.checkboxMonitorSentItems.TabIndex = 14;
            this.checkboxMonitorSentItems.Text = "Monitor sent items folder";
            this.checkboxMonitorSentItems.UseVisualStyleBackColor = true;
            // 
            // checkboxAlwaysClearDeletedItems
            // 
            this.checkboxAlwaysClearDeletedItems.AutoSize = true;
            this.checkboxAlwaysClearDeletedItems.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkboxAlwaysClearDeletedItems.Location = new System.Drawing.Point(9, 63);
            this.checkboxAlwaysClearDeletedItems.Name = "checkboxAlwaysClearDeletedItems";
            this.checkboxAlwaysClearDeletedItems.Size = new System.Drawing.Size(304, 17);
            this.checkboxAlwaysClearDeletedItems.TabIndex = 15;
            this.checkboxAlwaysClearDeletedItems.Text = "Always empty \'Deleted Items (eMail Manager)\' folder on exit\r\n";
            this.checkboxAlwaysClearDeletedItems.UseVisualStyleBackColor = true;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label4.Location = new System.Drawing.Point(6, 16);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(155, 13);
            this.Label4.TabIndex = 9;
            this.Label4.Text = "Archived and retained category";
            // 
            // cboCategory
            // 
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.FormattingEnabled = true;
            this.cboCategory.Location = new System.Drawing.Point(167, 13);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(236, 21);
            this.cboCategory.TabIndex = 10;
            // 
            // checkboxLeaveCopy
            // 
            this.checkboxLeaveCopy.AutoSize = true;
            this.checkboxLeaveCopy.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkboxLeaveCopy.Location = new System.Drawing.Point(9, 40);
            this.checkboxLeaveCopy.Name = "checkboxLeaveCopy";
            this.checkboxLeaveCopy.Size = new System.Drawing.Size(265, 17);
            this.checkboxLeaveCopy.TabIndex = 13;
            this.checkboxLeaveCopy.Text = "Default value for leave copy of message in mailbox";
            this.checkboxLeaveCopy.UseVisualStyleBackColor = true;
            // 
            // OK_Button
            // 
            this.OK_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OK_Button.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.OK_Button.Location = new System.Drawing.Point(354, 201);
            this.OK_Button.Name = "OK_Button";
            this.OK_Button.Size = new System.Drawing.Size(67, 23);
            this.OK_Button.TabIndex = 0;
            this.OK_Button.Text = "OK";
            this.OK_Button.Click += new System.EventHandler(this.OK_Button_Click);
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 236);
            this.Controls.Add(this.OK_Button);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSettings";
            this.Text = "Settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private Controls.FileNameFilterBox fileNameFilterBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button OK_Button;
        private System.Windows.Forms.CheckBox checkboxMonitorSentItems;
        internal System.Windows.Forms.CheckBox checkboxAlwaysClearDeletedItems;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.ComboBox cboCategory;
        internal System.Windows.Forms.CheckBox checkboxLeaveCopy;
    }
}