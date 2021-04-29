using eMailManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace eMailManager
{
    public static class GlobalConfig
    {
        public static System.Resources.ResourceManager localResourceManager = new System.Resources.ResourceManager("eMailManager.ResourceStrings", typeof(ThisAddIn).Assembly);
        
        public static SettingsModel GlobalSettings;

        public static Outlook.Application Application;
        public static Outlook.NameSpace MapiNamespace = Globals.ThisAddIn.Application.GetNamespace("MAPI");
        public static string DeletedItemsFolderName = "Deleted Items (eMail Manager)";
        public static Outlook.Folder DeletedItemsFolder;
        public static Outlook.Folder OlInbox = MapiNamespace.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox) as Outlook.Folder;
        public static Outlook.Folder OlDeletedItems = MapiNamespace.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderDeletedItems) as Outlook.Folder;
        public static Outlook.MailItem OlLastItem;

        public static int MaxFile = 255;

        public static void LoadSettings()
        {
            using (var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\MailManager\Settings", false))
            {
                if (key != null)
                {
                    GlobalSettings.AlwaysClearDeletedItems = bool.Parse(key.GetValue("AlwaysClearDeletedItems", GlobalSettings.AlwaysClearDeletedItems).ToString()); 
                    GlobalSettings.ArchivedAndRetainedCategory = key.GetValue("ArchivedAndRetainedCategory", GlobalSettings.ArchivedAndRetainedCategory).ToString();
                    GlobalSettings.FilenameFilter = key.GetValue("FilenameFilter", GlobalSettings.FilenameFilter).ToString();
                    GlobalSettings.FormHeight = int.Parse(key.GetValue("FormHeight", GlobalSettings.FormHeight).ToString());
                    GlobalSettings.FormWidth = int.Parse(key.GetValue("FormWidth", GlobalSettings.FormWidth).ToString());
                    GlobalSettings.LastLocation = key.GetValue("LastLocation", GlobalSettings.LastLocation).ToString();
                    GlobalSettings.LeaveCopy = bool.Parse(key.GetValue("LeaveCopy", GlobalSettings.LeaveCopy).ToString());
                    GlobalSettings.MessageFileExt = key.GetValue("MessageFileExt", GlobalSettings.MessageFileExt).ToString();
                    GlobalSettings.MonitorSentItems = bool.Parse(key.GetValue("MonitorSentItems", GlobalSettings.MonitorSentItems).ToString());
                    GlobalSettings.RememberLastLocation = bool.Parse(key.GetValue("RememberLastLocation", GlobalSettings.RememberLastLocation).ToString());
                    GlobalSettings.UseFileSystem = bool.Parse(key.GetValue("UseFileSystem", GlobalSettings.UseFileSystem).ToString());
                }
            }

            if (CategoryExists(GlobalSettings.ArchivedAndRetainedCategory) == false)
            {
                Outlook.Categories categories = GlobalConfig.Application.Session.Categories;

                categories.Add(GlobalSettings.ArchivedAndRetainedCategory,
                    Outlook.OlCategoryColor.olCategoryColorDarkRed);
            }
        }

        public static void SaveSettings()
        {
            var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\MailManager\Settings", true);

            if (key == null)
            {
                key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"SOFTWARE\MailManager\Settings", true);
            }

            key.SetValue("AlwaysClearDeletedItems", GlobalSettings.AlwaysClearDeletedItems); 
            key.SetValue("ArchivedAndRetainedCategory", GlobalSettings.ArchivedAndRetainedCategory);
            key.SetValue("FilenameFilter", GlobalSettings.FilenameFilter);
            key.SetValue("FormHeight", GlobalSettings.FormHeight);
            key.SetValue("FormWidth", GlobalSettings.FormWidth);
            key.SetValue("LastLocation", GlobalSettings.LastLocation);
            key.SetValue("LeaveCopy", GlobalSettings.LeaveCopy);
            key.SetValue("MessageFileExt", GlobalSettings.MessageFileExt);
            key.SetValue("MonitorSentItems", GlobalSettings.MonitorSentItems);
            key.SetValue("RememberLastLocation", GlobalSettings.RememberLastLocation);
            key.SetValue("UseFileSystem", GlobalSettings.UseFileSystem);
        }

        //MRU string in the format  <FolderPath>|<EntryID>|<Description>|<StoreID>

        public static void LoadMRU()
        {
            //first clear any values as we'll load all again
            GlobalSettings.MessageStores.Clear();

            //get the MRU out of the registry
            using (Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\MailManager\MRU", false))
            {
                if (key != null)
                {
                    //first get an array of values
                    var valueNames = key.GetValueNames();

                    List<string> values = new List<string>();

                    foreach(var name in valueNames)
                    {
                        values.Add(key.GetValue(name).ToString());
                    }

                    //now loop through the array
                    foreach (var value in values)
                    {
                        //split this string into an array of its parts
                        var valueParts = value.Split('|');

                        var messageStore = new MessageStoreModel
                        {
                            Path = valueParts[0],
                            FolderID = valueParts[1],
                            Description = valueParts[2]
                        };

                        if (valueParts.Length > 3)
                            messageStore.StoreID = valueParts[3];

                        GlobalConfig.GlobalSettings.MessageStores.Add(messageStore);
                    }
                }
            }
        }

        public static void SaveMRU()
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\MailManager", true);

            if (key == null)
            {
                //we don't have a key yet so create one
                key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"SOFTWARE\MailManager", true);
            }

            //clear all the currently saved registry items out
            key.DeleteSubKey("MRU", false);
            Microsoft.Win32.RegistryKey mruKey = key.CreateSubKey("MRU", true);

            if (mruKey != null)
            {
                    //loop through the current mru and save to registry
                    var id = 1;
                    foreach (var item in GlobalConfig.GlobalSettings.MessageStores)
                    {
                        string newValue;

                        if (item.IsOutlookPath)
                        {
                            //<FolderPath>|<EntryID>|<Description>|<StoreID>
                            newValue = $"{item.Path}|{item.FolderID}|{item.Description}|{item.StoreID}";
                        }
                        else
                        {
                            //not an outlook folder so we duplicate path and description
                            newValue = $"{item.Path}|{item.Path}|{item.Description}|{item.Description}";
                        }

                        mruKey.SetValue(id.ToString(), newValue, Microsoft.Win32.RegistryValueKind.String);

                        id++;
                    }
            }

        }


        private static bool CategoryExists(string categoryName)
        {
            try
            {
                Outlook.Category category =
                    GlobalConfig.Application.Session.Categories[categoryName];
                if (category != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch { return false; }
        }
    }
}
