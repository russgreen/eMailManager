using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;

using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace eMailManager
{
    public partial class ThisAddIn
    {
        // User interface elements.
        private Office.CommandBar _addinToolbar;
        private Office.CommandBarPopup _addinMenu;
        private Office.CommandBarButton _fileItMenuButton;
        private Office.CommandBarButton _folderSaveButton;
        private Office.CommandBarButton _aboutMenuButton;
        private Office.CommandBarButton _searchMenuButton;
        private Office.CommandBarButton _settingsMenuButton;
        private Office.CommandBarButton _clearDeletedItems;
        private Office.CommandBarButton _fileItToolbarButton;
        private Office.CommandBarButton _folderSaveToolbarButton;
        private Office.CommandBarButton _aboutToolbarButton;
        private Office.CommandBarButton _updateToolbarButton;
        private Office.CommandBarButton _searchToolbarButton;
        private Office.CommandBarButton _settingsToolbarButton;

        // Object to filter and report Outlook events.
        private EventTracker _eventTracker;

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            var application = this.Application;
            var inspectors = application.Inspectors;
            var activeInspector = application.ActiveInspector();
            var explorers = application.Explorers;
            var activeExplorer = application.ActiveExplorer();

            while (activeExplorer is null)
            {
                // do nothing
            }

            if (activeExplorer is object)
            {
                StartAddin();
            }
        }

        private void StartAddin()
        {
#if (!DEBUG)
            AppCenter.Start("fbfec405-1cd1-40ba-baef-4be1d91398da",
                   typeof(Analytics), typeof(Crashes));

#endif
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            // Note: Outlook no longer raises this event. If you have code that 
            // must run when Outlook shuts down, see https://go.microsoft.com/fwlink/?LinkId=506785
        }

#region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
#endregion

    }
}
