using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace eMailManager
{
    public partial class FormSettings : Form
    {
        public FormSettings()
        {   
            InitializeComponent();

            //load outlook categories to combo
            foreach (Outlook.Category item in GlobalConfig.Application.Session.Categories)
            {
                this.cboCategory.Items.Add(item.Name);
            }

            this.cboCategory.Text = GlobalConfig.GlobalSettings.ArchivedAndRetainedCategory;

            //setup control bindings to settings
            this.fileNameFilterBox1.DataBindings.Add("FileNameFilter", GlobalConfig.GlobalSettings, "FilenameFilter");

            this.checkboxLeaveCopy.DataBindings.Add("Checked", GlobalConfig.GlobalSettings, "LeaveCopy");
            this.checkboxAlwaysClearDeletedItems.DataBindings.Add("Checked", GlobalConfig.GlobalSettings, "AlwaysClearDeletedItems");
            this.checkboxMonitorSentItems.DataBindings.Add("Checked", GlobalConfig.GlobalSettings, "MonitorSentItems");

        }

        private void OK_Button_Click(object sender, EventArgs e)
        {
            //TODO - investigate how to make custom control with binding better
            GlobalConfig.GlobalSettings.FilenameFilter = this.fileNameFilterBox1.FileNameFilter;

            GlobalConfig.GlobalSettings.ArchivedAndRetainedCategory = this.cboCategory.Text;

            GlobalConfig.SaveSettings();

            this.Close();

        }
    }
}
