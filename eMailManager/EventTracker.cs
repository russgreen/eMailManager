using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace eMailManager  
{
    /// <summary>
    /// The EventTracker class handles the various Outlook events
    /// and maintains and applies the event filter.
    /// </summary>
    internal sealed class EventTracker
    {
        // Store references to the folder objects so that we can receive their events.
        // If we do not maintain references to these items, then we cannot reliably 
        // receive the add, change, and remove events.
        private Outlook.Items _inboxItems;
        private Outlook.Items _sentItems;

        private Outlook.Explorer _exp;
        private ThisAddIn _app;

        private bool _monitorSentItems = true;
        private bool _askedToMonitor = false;
        private int _sentItemsCancelCount = 0;

        private Outlook.MailItem[] _sentMailStack;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="app">Reference to the main Outlook application object</param>
        internal EventTracker(ThisAddIn app)
        {
            _app = app;
            _exp = _app.Application.ActiveExplorer();
            _exp.SelectionChange += ExplorerSelectionChange;

            // Obtain references to the folder objects that fire the events we are interested in.
            var mapiNamespace = app.Application.GetNamespace("MAPI");
            Outlook.Folder inbox = mapiNamespace.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox) as Outlook.Folder;
            Outlook.Folder sentitems = mapiNamespace.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderSentMail) as Outlook.Folder;

            // Store references to the item collection objects.
            _inboxItems = inbox.Items;
            _sentItems = sentitems.Items;

            // Subscribe to the ItemAdd events.
            // Note that there are cases in which the ItemAdd event is not raised. For example, if multiple
            // items are created or received, then the ItemAdd event may be raised only once.
            // See http://support.microsoft.com/?kbid=249156 for details.
            _inboxItems.ItemAdd += new Outlook.ItemsEvents_ItemAddEventHandler(InboxFolderItemAdded);
            _sentItems.ItemAdd += new Outlook.ItemsEvents_ItemAddEventHandler(SentItemsFolderItemAdded);

            // Subscribe to the ItemChange events.
            _inboxItems.ItemChange += new Outlook.ItemsEvents_ItemChangeEventHandler(InboxItemsItemChange);
            _sentItems.ItemChange += new Outlook.ItemsEvents_ItemChangeEventHandler(SentItemsItemChange);

            // Subscribe to the ItemRemove events.
            _inboxItems.ItemRemove += new Outlook.ItemsEvents_ItemRemoveEventHandler(InboxItemsItemRemove);
            _sentItems.ItemRemove += new Outlook.ItemsEvents_ItemRemoveEventHandler(SentItemsItemRemove);
        }

        private void ExplorerSelectionChange()
        {
            Outlook.Explorer exp;
            exp = _app.Application.ActiveExplorer();
            Outlook.Folder selectedFolder = (Outlook.Folder)exp.CurrentFolder;

            switch (selectedFolder.Name.ToLower() ?? "")
            {
                case "inbox":
                    break;
  
                case "sent items":
                    break;
            }
        }

        #region Items Added
        private void InboxFolderItemAdded(object Item)
        {
            

            if (GlobalConfig.GlobalSettings.AutoArchiveIn == true)
            {
                //TODO: Auto save the message

                try
                {
                    //mailitem added to inbox folder
                    Outlook.MailItem olMailItem = (Outlook.MailItem)Item;
                }
                catch
                {
                    //do nothing if we can't convert item to a mail item
                }
            }
        }

        private void SentItemsFolderItemAdded(object Item)
        {
            // mailitem added to sent items folder
            Outlook.MailItem olMailItem = (Outlook.MailItem)Item;

            List<Outlook.MailItem> olMailItems = new List<Outlook.MailItem>();

            if (GlobalConfig.GlobalSettings.AutoArchiveOut == true)
            {
                //TODO: Auto save the message
            }

            // check if this is a duplicate mailitem or not.  
            // handling a bug from google apps
            if (GlobalConfig.OlLastItem is object)
            {
                if (GlobalConfig.OlLastItem.SenderName  == olMailItem.SenderName & 
                    GlobalConfig.OlLastItem.To  == olMailItem.To & 
                    GlobalConfig.OlLastItem.SentOn == olMailItem.SentOn & 
                    GlobalConfig.OlLastItem.Subject == olMailItem.Subject & 
                    GlobalConfig.OlLastItem.Attachments.Count == olMailItem.Attachments.Count & 
                    GlobalConfig.OlLastItem.Body  == olMailItem.Body)
                {
                    // we seem to have a duplicate message so ingore it
                    return;
                }

                // TODO: perform the same action as the last message
                else
                {
                    // seems to be a unique message so carry on
                }
            }

            // check if we should monitor sent items.
            if (_monitorSentItems == false)
            {
                return;
            }

            //add the mail item to the list to pass to the form
            olMailItems.Add(olMailItem);

            var formMain = new FormMain(olMailItems, true);

            // TODO: check if the form is already visible

            // TODO: form is already visible so add sent item to stack

            //if the user keeps cancelling them maybe they want to stop monitoring
            if (formMain.ShowDialog() == DialogResult.Cancel)
            {
                // check for user wanting to stop monitoring folder
                if (_sentItemsCancelCount + 1 >= 5)
                {
                    if (_monitorSentItems == true & _askedToMonitor == false)
                    {
                        if (MessageBox.Show(GlobalConfig.localResourceManager.GetString("CancelMonitoringMessage"),
                            GlobalConfig.localResourceManager.GetString("CancelMonitoringCaption"),
                           MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            _sentItemsCancelCount++;
                            _monitorSentItems = false;
                        }
                        else
                        {
                            _monitorSentItems = true;
                        }

                        _askedToMonitor = true;
                    }
                }
                else
                {
                    _sentItemsCancelCount++;
                }
            }
            else
            {
                _sentItemsCancelCount = 0;
            }


            formMain.Dispose();
            GlobalConfig.OlLastItem = olMailItem;
        }
        #endregion

        #region Items Changed
        private void InboxItemsItemChange(object Item)
        {
           //nothing to be done
        }

        private void SentItemsItemChange(object Item)
        {
            //nothing to be done
        }
        #endregion

        #region Items Removed
        private void InboxItemsItemRemove()
        {
            //nothing to be done
        }

        private void SentItemsItemRemove()
        {
            //nothing to be done
        }
        #endregion
    }
}