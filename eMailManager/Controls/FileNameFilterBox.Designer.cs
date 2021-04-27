using System;
using System.Runtime.CompilerServices;

namespace eMailManager.Controls
{
    public partial class FileNameFilterBox : System.Windows.Forms.UserControl
    {

        // UserControl overrides dispose to clean up the component list.
        [System.Diagnostics.DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        // Required by the Windows Form Designer
        private System.ComponentModel.IContainer components;

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough()]
        private void InitializeComponent()
        {
            _TextBox1 = new System.Windows.Forms.TextBox();
            _TextBox1.TextChanged += new EventHandler(TextBox1_TextChanged);
            _ComboBox1 = new System.Windows.Forms.ComboBox();
            _ComboBox1.SelectionChangeCommitted += new EventHandler(ComboBox1_SelectionChangeCommitted);
            SuspendLayout();
            // 
            // TextBox1
            // 
            _TextBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;

            _TextBox1.Location = new System.Drawing.Point(0, 0);
            _TextBox1.Name = "_TextBox1";
            _TextBox1.Size = new System.Drawing.Size(380, 20);
            _TextBox1.TabIndex = 0;
            // 
            // ComboBox1
            // 
            _ComboBox1.Dock = System.Windows.Forms.DockStyle.Right;
            _ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            _ComboBox1.DropDownWidth = 100;
            _ComboBox1.FormattingEnabled = true;
            _ComboBox1.Items.AddRange(new object[] { "<sent>", "<sent_yy>", "<sent_yyyy>", "<sent_mm>", "<sent_dd>", "<sent_hh-mm>", "<sent_hh.mm>", "<sent_hh-mm12>", "<sent_hh.mm12>", "<sent_hhmmss>", "<from>", "<from_email>", "<from_domain>", "<subject>", "﻿<$[nn]subject>", "<type>", "<TYPE>", "<type_sr>", ".msg" });
            _ComboBox1.Location = new System.Drawing.Point(380, 0);
            _ComboBox1.Name = "_ComboBox1";
            _ComboBox1.Size = new System.Drawing.Size(20, 21);
            _ComboBox1.TabIndex = 1;
            // 
            // FileNameFilterBox
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6.0f, 13.0f);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(_ComboBox1);
            Controls.Add(_TextBox1);
            Name = "FileNameFilterBox";
            Size = new System.Drawing.Size(400, 21);
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.TextBox _TextBox1;

        internal System.Windows.Forms.TextBox TextBox1
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _TextBox1;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_TextBox1 != null)
                {
                    _TextBox1.TextChanged -= TextBox1_TextChanged;
                }

                _TextBox1 = value;
                if (_TextBox1 != null)
                {
                    _TextBox1.TextChanged += TextBox1_TextChanged;
                }
            }
        }

        private System.Windows.Forms.ComboBox _ComboBox1;

        internal System.Windows.Forms.ComboBox ComboBox1
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _ComboBox1;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_ComboBox1 != null)
                {
                    _ComboBox1.SelectionChangeCommitted -= ComboBox1_SelectionChangeCommitted;
                }

                _ComboBox1 = value;
                if (_ComboBox1 != null)
                {
                    _ComboBox1.SelectionChangeCommitted += ComboBox1_SelectionChangeCommitted;
                }
            }
        }
    }
}