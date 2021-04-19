using System;
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
            throw new NotImplementedException();
        }

        private void SentItemsFolderItemAdded(object Item)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Items Changed
        private void InboxItemsItemChange(object Item)
        {
            throw new NotImplementedException();
        }

        private void SentItemsItemChange(object Item)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Items Removed
        private void InboxItemsItemRemove()
        {
            throw new NotImplementedException();
        }

        private void SentItemsItemRemove()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}