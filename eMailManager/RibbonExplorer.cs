using Microsoft.Office.Tools.Ribbon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace eMailManager
{
    public partial class RibbonExplorer
    {
        private void RibbonExplorer_Load(object sender, RibbonUIEventArgs e)
        {
            this.buttonFileMessage.Label = GlobalConfig.localResourceManager.GetString("MenuFileMessage");
            this.buttonSearch.Label = GlobalConfig.localResourceManager.GetString("MenuSearch");
            this.buttonSettings.Label = GlobalConfig.localResourceManager.GetString("MenuSettings");
        }

        private void buttonFileMessage_Click(object sender, RibbonControlEventArgs e)
        {
            if (Globals.ThisAddIn.Application.ActiveExplorer().Selection.Count == 0)
            {
                MessageBox.Show(GlobalConfig.localResourceManager.GetString("Message009")); 
                return;
            }
            else if (Globals.ThisAddIn.Application.ActiveExplorer().Selection.Count == 1)
            {
                var mailItems = new List<Outlook.MailItem>();
                mailItems.Add((Outlook.MailItem)Globals.ThisAddIn.Application.ActiveExplorer().Selection[(object)1]);
                
                var frm = new FormMain(mailItems, false);
                frm.ShowDialog();
            }
            else if (Globals.ThisAddIn.Application.ActiveExplorer().Selection.Count > 1)
            {
                var mailItems = new List<Outlook.MailItem>();
                foreach (var item in Globals.ThisAddIn.Application.ActiveExplorer().Selection)
                {
                    mailItems.Add((Outlook.MailItem)item);
                }
                
                var frm = new FormMain(mailItems, false, true);
                frm.ShowDialog();
            }
        }

        private void buttonSearch_Click(object sender, RibbonControlEventArgs e)
        {

        }

        private void buttonSettings_Click(object sender, RibbonControlEventArgs e)
        {

        }


    }
}
