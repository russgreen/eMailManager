using Microsoft.Office.Tools.Ribbon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace eMailManager
{
    public partial class RibbonMailItem
    {
        private void RibbonMailItem_Load(object sender, RibbonUIEventArgs e)
        {
            this.buttonFileMessage.Label = GlobalConfig.localResourceManager.GetString("MenuFileMessage");
        }

        private void buttonFileMessage_Click(object sender, RibbonControlEventArgs e)
        {
            var mailItems = new List<Outlook.MailItem>();
            mailItems.Add((Outlook.MailItem)Globals.ThisAddIn.Application.ActiveInspector().CurrentItem);

            Globals.ThisAddIn.Application.ActiveInspector().Close(Outlook.OlInspectorClose.olDiscard);

            var frm = new FormMain(mailItems, false);
            frm.ShowDialog();
        }
    }
}
