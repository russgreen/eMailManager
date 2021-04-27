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

            //register license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDI5MTU0QDMxMzkyZTMxMmUzMFRYNXJ4UkFXd24waElSUGZwTWszc20yaldkWk0vaHZRdTlpbktHU0dzSTg9");

            //initialize the settings for the addin
            GlobalConfig.GlobalSettings = new Models.SettingsModel();
            GlobalConfig.LoadSettings();


            //set the longest filename we can use
            try
            {
                bool result;
                int fileNameLength = 300;
                string TestFolder = System.IO.Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Temp");
                do
                {
                    fileNameLength -= 1;
                    result = WriteFile(TestFolder, CreateFilename(fileNameLength));
                }
                while (!result);
                GlobalConfig.MaxFile = fileNameLength + TestFolder.Length - 4;
            }
            catch (Exception ex)
            {
                // do nothing and use the default value
            }

            //check if we should monitor sent items
            if(GlobalConfig.GlobalSettings.MonitorSentItems == true)
            {
                _eventTracker = new EventTracker(this);
            }

            //get the parent folder of my inbox
            Outlook.Folder myRoot;
            myRoot = GlobalConfig.OlInbox.Parent;

            //then try and add a deleted items folder
            try
            {
                // try to get the new deleted items folder
                GlobalConfig.DeletedItemsFolder = (Outlook.Folder)myRoot.Folders[GlobalConfig.DeletedItemsFolderName];
            }
            catch
            {
                // if the folder does not exist, add it
                GlobalConfig.DeletedItemsFolder = (Outlook.Folder)myRoot.Folders.Add(GlobalConfig.DeletedItemsFolderName);
                // emDeletedItems.Description = "Email messages in this folder have been filed to the file system. Delete these messages regularly to keep your mailbox small."
                GlobalConfig.DeletedItemsFolder.Description = GlobalConfig.localResourceManager.GetString("DeletedItemsDescription");
            }

            //add event handler for shutdown
            ((Outlook.ApplicationEvents_10_Event)Application).Quit += ThisAddIn_Quit;

        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            // Note: Outlook no longer raises this event. If you have code that 
            // must run when Outlook shuts down, see https://go.microsoft.com/fwlink/?LinkId=506785
        }

        private void ThisAddIn_Quit()
        {
            CommonMethods.ClearDeletedItems();
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

        private string CreateFilename(int fileNameLength)
        {
            string s = string.Empty;
            for (int i = 1, loopTo = fileNameLength; i <= loopTo; i++)
                s += "a";
            return s;
        }

        private bool WriteFile(string testFolder, string filename)
        {
            try
            {
                // Create an instance of StreamWriter to write text to a file.
                var sw = new System.IO.StreamWriter(testFolder + filename);
                // Add some text to the file.
                sw.Write("This is the ");
                sw.WriteLine("header for the file.");
                sw.WriteLine("-------------------");
                // Arbitrary objects can also be written to the file.
                sw.Write("The date is: ");
                sw.WriteLine(DateTime.Now);
                sw.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
