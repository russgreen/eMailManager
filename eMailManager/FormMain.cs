using eMailManager.Models;
using Syncfusion.WinForms.DataGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Outlook = Microsoft.Office.Interop.Outlook;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace eMailManager
{
    public partial class FormMain : Form
    {
        private List<Outlook.MailItem> _mailItems;
        private Outlook.MailItem _mailItem;
        private bool _sentMail;
        private bool _batchFile;

        private MessageStoreModel _selectedPath;

        public FormMain()
        {
            InitializeComponent();
            BuildDataGrid();
        }

        public FormMain(List<Outlook.MailItem> mailItems, bool sentMail, bool batchFile = false)
        {
            InitializeComponent();

            var informationVersion = Assembly.GetExecutingAssembly().GetName().Version;
            this.Text = $"eMail Manager {informationVersion}";

            BuildDataGrid();

            _mailItems = mailItems;
            _sentMail = sentMail;
            _batchFile = batchFile;

            if(batchFile == false)
            {
                _mailItem = mailItems.First();
            }

            //setup control bindings to settings
            this.checkboxLeaveCopy.DataBindings.Add("Checked", GlobalConfig.GlobalSettings, "LeaveCopy");
            this.radioFileFolder.DataBindings.Add("Checked", GlobalConfig.GlobalSettings, "UseFileSystem");
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // populate the mail details
            if (_batchFile == false)
            {
                labelMailFrom.Text = _mailItem.SenderName;
                labelMailTo.Text = _mailItem.To;
                labelMailSubject.Text = _mailItem.Subject;
            }
            else
            {
                labelMailFrom.Text = GlobalConfig.localResourceManager.GetString("BatchFileMode");
                labelMailTo.Text = GlobalConfig.localResourceManager.GetString("BatchFileMode");
                labelMailSubject.Text = GlobalConfig.localResourceManager.GetString("BatchFileMode");
            }

            //configure from settings
            this.Width = GlobalConfig.GlobalSettings.FormWidth;
            this.Height = GlobalConfig.GlobalSettings.FormHeight;

            //get the list of saved folder locations
            GlobalConfig.LoadMRU();

            //setup the workflow controls
            SetWorkFlowControls();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalConfig.SaveMRU();

            GlobalConfig.GlobalSettings.FormHeight = this.Height;
            GlobalConfig.GlobalSettings.FormWidth = this.Width;

            GlobalConfig.SaveSettings();
        }

        #region UI code
        private void BuildDataGrid()
        {
            this.sfDataGrid1.Columns.Clear();
            this.sfDataGrid1.Columns.Add(new GridTextColumn() { MappingName = "Description", HeaderText = "Description", AllowEditing = true, MinimumWidth = 200 });
            this.sfDataGrid1.Columns.Add(new GridTextColumn() { MappingName = "Path", HeaderText = "Path", AllowEditing = false });
            this.sfDataGrid1.Columns.Add(new GridTextColumn() { MappingName = "FolderID", HeaderText = "Folder ID", AllowEditing = false, Visible = false });
            this.sfDataGrid1.Columns.Add(new GridTextColumn() { MappingName = "StoreID", HeaderText = "Store ID", AllowEditing = false, Visible = false });

            this.sfDataGrid1.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.LastColumnFill;

            //sort the description column
            SortColumnDescription sortColumnDescription = new SortColumnDescription();
            sortColumnDescription.ColumnName = "Description";
            sortColumnDescription.SortDirection = ListSortDirection.Ascending;

            this.sfDataGrid1.SortColumnDescriptions.Add(sortColumnDescription);

            this.sfDataGrid1.DataSource = GlobalConfig.GlobalSettings.MessageStores;
        }

        private void SetWorkFlowControls()
        {
            //start by enabling all workflows and defaults on controls
            rdoWorkflow01.Checked = true;
            rdoWorkflow01.Enabled = true;
            rdoWorkflow01.Text = GlobalConfig.localResourceManager.GetString("WorkFlow01"); // "File message (any attachments remain embedded)"
            rdoWorkflow02.Enabled = true;
            rdoWorkflow02.Text = GlobalConfig.localResourceManager.GetString("WorkFlow02"); // "File message and attachments separately (keep a copy of attachments in message)"
            rdoWorkflow03.Enabled = true;
            rdoWorkflow03.Text = GlobalConfig.localResourceManager.GetString("WorkFlow03"); // "File message and strip attachments from the message (removes and saves the attachments in file systes)"

            if (_batchFile == false)
            {
                //check if we are saving to an outlook folder or a file system folder
                if (radioOutlookFolder.Checked == true)
                {
                    //saving to an outlook folder
                    checkboxLeaveCopy.Visible = false;
                }
                else
                {
                    //saving to a file system folder
                    checkboxLeaveCopy.Visible = true;
                }
            }
            else
            {
                //disable workflows that won't play nice with batch file
                rdoWorkflow02.Enabled = false;
                rdoWorkflow03.Enabled = false;

                if (radioOutlookFolder.Checked == true)
                {
                    checkboxLeaveCopy.Visible = false;
                }
                else
                {
                    checkboxLeaveCopy.Visible = true;
                }
            }
        }

        private void sfDataGrid1_SelectionChanged(object sender, Syncfusion.WinForms.DataGrid.Events.SelectionChangedEventArgs e)
        {
            _selectedPath = (MessageStoreModel)this.sfDataGrid1.SelectedItem;

            //check we have an object
            if(_selectedPath != null)
            {
                this.radioOutlookFolder.Checked = _selectedPath.IsOutlookPath;

                this.buttonFileMessage.Enabled = true;
            }
        }

        private void radioOutlookFolder_CheckedChanged(object sender, EventArgs e)
        {
            if (radioOutlookFolder.Checked == false)
            {
                radioFileFolder.Checked = true;
            }

            GlobalConfig.GlobalSettings.UseFileSystem = this.radioFileFolder.Checked;

            SetWorkFlowControls();
        }

        //TODO - debug why radio buttons are not working in the group when set in code
        private void radioFileFolder_CheckedChanged(object sender, EventArgs e)
        {
            if (radioFileFolder.Checked == false)
            {
                radioOutlookFolder.Checked = true;
            }
        }
        #endregion

        #region Button code
        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            if (radioFileFolder.Checked)
            {
                var folderBrowser = FolderBrowserDialog1;

                folderBrowser.ShowNewFolderButton = true;
                folderBrowser.RootFolder = Environment.SpecialFolder.MyComputer;

                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    var path = folderBrowser.SelectedPath;
                    if (!path.EndsWith(@"\"))
                        path += @"\";

                    var newPath = new MessageStoreModel
                    {
                        Path = path,
                        Description = path,
                        FolderID = path,
                        StoreID = path
                    };

                    GlobalConfig.GlobalSettings.MessageStores.Add(newPath);
                }
            }
            else
            {
                Outlook.NameSpace objNS;
                Outlook.Folder objFolder;
                objNS = Globals.ThisAddIn.Application.GetNamespace("MAPI");
                objFolder = (Outlook.Folder)objNS.PickFolder();

                var newPath = new MessageStoreModel
                {
                    Path = objFolder.FolderPath,
                    Description = objFolder.FolderPath,
                    FolderID = objFolder.EntryID,
                    StoreID = objFolder.StoreID
                };

                GlobalConfig.GlobalSettings.MessageStores.Add(newPath);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                _mailItem.Move(GlobalConfig.DeletedItemsFolder);
            }
            catch
            {
                _mailItem.Move(GlobalConfig.OlDeletedItems);
            }

            this.Close();
        }

        private void buttonFileMessage_Click(object sender, EventArgs e)
        {
            if (_batchFile == true)
            {
                BatchFile();
            }
            else
            {
                FileMessage(_mailItem);
            }

            this.Close();
        }
        #endregion

        private bool HasNonEmbeddedAttachments()
        {
            bool retval = false;
            if (_mailItem.Attachments.Count == 0)
            {
                retval = false;
            }
            else
            {
                foreach (Outlook.Attachment attachment in _mailItem.Attachments)
                {
                    if (_mailItem.HTMLBody.ToLower().Contains("cid:" + attachment.FileName) == true)
                    {
                        retval = false;
                    }
                    else
                    {
                        retval = true;
                        break;
                    }
                }
            }

            return retval;
        }

        /// <summary>
        /// Extracts and saves attachments into the file system
        /// </summary>
        /// <param name="chooseLocation"></param>
        /// <returns></returns>
        private bool ExtractAttachments(bool chooseLocation = false)
        {
            bool retval = false;
            string path = string.Empty;
            Outlook.Attachment attachment;

            if(chooseLocation == false)
            {
                //just save the attachement to the same place as the email.
                retval = true;
                foreach (Outlook.Attachment currentOAttachment in _mailItem.Attachments)
                {
                    attachment = currentOAttachment;
                    attachment.SaveAsFile(Path.Combine(_selectedPath.Path, attachment.DisplayName));
                }
            }
            else
            {
                var folderBrowser = FolderBrowserDialog1;
                folderBrowser.ShowNewFolderButton = true;
                folderBrowser.RootFolder = Environment.SpecialFolder.MyComputer;
                folderBrowser.Description = GlobalConfig.localResourceManager.GetString("Message006");

                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    path = folderBrowser.SelectedPath;
                    if (!path.EndsWith(@"\"))
                    {
                        path += @"\";
                        retval = true;
                    }
                }
                else
                {
                    //just use my documents as a fallback location
                    path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    if (!path.EndsWith(@"\"))
                        path += @"\";
                    retval = true;
                }

                if (retval == true)
                {
                    foreach (Outlook.Attachment currentOAttachment1 in _mailItem.Attachments)
                    {
                        attachment = currentOAttachment1;
                        // MsgBox(sSavePathAtt & oAttachment.FileName)
                        attachment.SaveAsFile(Path.Combine(path, attachment.DisplayName));
                    }
                }
            }

            return retval;
        }

        /// <summary>
        /// Removes attachements and replaces them with a textfile comprising a list of the removed attachments
        /// </summary>
        private void RemoveAttachments()
        {
            var attachmentListFile = $@"{ Path.GetTempPath() }\REMOVED_ATTACHMENTS.txt";

            //if the file exists delete it
            if(File.Exists(attachmentListFile) == true)
            {
                File.Delete(attachmentListFile);
            }

            //use a string builder for the text file contents
            var sbAttachmentList = new StringBuilder();
            sbAttachmentList.Append($"ATTACHMENTS REMOVED: {Environment.NewLine}");

            int i;
            for (i = _mailItem.Attachments.Count; i >= 1; i -= 1)
            {
                if (_mailItem.HTMLBody.ToLower().Contains("cid:" + _mailItem.Attachments[i].FileName) == true)
                {
                }
                // must be an embedded attachment so we'll leave it alone
                else
                {
                    sbAttachmentList.Append($"{_mailItem.Attachments[i].FileName.ToString()}{Environment.NewLine}");
                    _mailItem.Attachments.Remove(i);
                }
            }

            // now save the list of removed attachments to a TXT file and attach it to the message
            File.WriteAllText(attachmentListFile, sbAttachmentList.ToString());

            //now attach the text file to the mail item
            _mailItem.Save();
            _mailItem.Attachments.Add(attachmentListFile, Outlook.OlAttachmentType.olByValue, DisplayName: "REMOVED_ATTACHMENTS.txt");
            _mailItem.Save();
        }

        private void HandleSavedMSG(Outlook.MailItem mailItem)
        {
            if (GlobalConfig.GlobalSettings.LeaveCopy == false)
            {
                try
                {
                    mailItem.Move(GlobalConfig.DeletedItemsFolder);
                }
                catch
                {
                    mailItem.Move(GlobalConfig.OlDeletedItems);
                }
            }
            else
            {
                {
                    mailItem.UserProperties.Add("emfiled", Outlook.OlUserPropertyType.olYesNo);
                    mailItem.UserProperties["emfiled"].Value = true;
                    mailItem.Categories = GlobalConfig.GlobalSettings.ArchivedAndRetainedCategory;
                    mailItem.Save();
                }
            }
        }

        private void FileMessage(Outlook.MailItem mailItem)
        {
            if(this.radioFileFolder.Checked == true) // FILE INTO FILE SYSTEM
            {
                //parse the filename
                var msgFilename = CommonMethods.ParseFilename(GlobalConfig.GlobalSettings.FilenameFilter, mailItem, _sentMail);
                if (msgFilename.EndsWith(GlobalConfig.GlobalSettings.MessageFileExt) == false)
                {
                    msgFilename += GlobalConfig.GlobalSettings.MessageFileExt;
                }

                //build a temp filepath and name 
                //System.IO.Path.GetTempPath()
                string argstrValue = Path.Combine(System.IO.Path.GetTempPath(), msgFilename);
                string tempMsgFilename = CommonMethods.TrimLongFileName(ref argstrValue);
                string argstrValue1 = Path.Combine(_selectedPath.Path, msgFilename);
                string saveMsgFilename = CommonMethods.TrimLongFileName(ref argstrValue1);

                //check the path exists
                if (Directory.Exists(_selectedPath.Path) == false)
                {
                    MessageBox.Show(GlobalConfig.localResourceManager.GetString("Message007"));
                    return;
                }

                //check of the file already exists, and append current date/time if necessary
                if(File.Exists(saveMsgFilename) == true)
                {
                    // first check the filename length is OK to add 28 characters
                    if (saveMsgFilename.Length > GlobalConfig.MaxFile - 28)
                    {
                        // the filename is too long to add the current filed date / time
                        saveMsgFilename = saveMsgFilename.Substring(0, GlobalConfig.MaxFile - 28) + GlobalConfig.GlobalSettings.MessageFileExt;
                    }

                    saveMsgFilename = saveMsgFilename.Replace(GlobalConfig.GlobalSettings.MessageFileExt, 
                        $" [filed - {CommonMethods.ShortYear(DateTime.Now.Year.ToString())}" +
                        $"{CommonMethods.ShortMonth(DateTime.Now.Month.ToString())}{CommonMethods.ShortDay(DateTime.Now.Day.ToString())}_" +
                        $"{DateTime.Now.ToLongTimeString().Replace(":", "")}]{GlobalConfig.GlobalSettings.MessageFileExt}");
                }

                if (this.rdoWorkflow01.Checked) // file message with attachments embedded
                {
                    mailItem.SaveAs(tempMsgFilename, GlobalConfig.GlobalSettings.MessageSaveAsType);
                    HandleSavedMSG(mailItem);
                }
                else if (rdoWorkflow02.Checked) // file message with attachments embedded and saved in file system
                {
                    if (HasNonEmbeddedAttachments() == true)
                    {
                        if (MessageBox.Show(GlobalConfig.localResourceManager.GetString("Message004"), "Save Attachments", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            ExtractAttachments(false);
                            //if (GlobalConfig.GlobalSettings.MessageSaveAsType == 3)
                            //{
                            //    RemoveAttachments();
                            //}
                        }
                        else
                        {
                            ExtractAttachments(true);
                            //if (ExtractAttachments(true) == true)
                            //{
                            //    if (GlobalConfig.GlobalSettings.MessageSaveAsType == 3)
                            //    {
                            //        RemoveAttachments();
                            //    }
                            //}
                        }
                    }

                    // m_olMailItem.SaveAs(sSaveMsgFilename, cSettings.MessageSaveAsType)
                    mailItem.SaveAs(tempMsgFilename, GlobalConfig.GlobalSettings.MessageSaveAsType);
                    HandleSavedMSG(mailItem);
                }
                else if (rdoWorkflow03.Checked) // file message with attachments removed and saved in file system
                {
                    if(_sentMail == true)
                    {
                        if(HasNonEmbeddedAttachments() == true)
                        {
                            RemoveAttachments();
                        }

                        mailItem.SaveAs(tempMsgFilename, GlobalConfig.GlobalSettings.MessageSaveAsType);
                        HandleSavedMSG(mailItem);
                    }
                    else
                    {
                        if (HasNonEmbeddedAttachments() == true)
                        {
                            if (MessageBox.Show(GlobalConfig.localResourceManager.GetString("Message004"), "Save Attachments", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                ExtractAttachments(false);
                            }
                            else
                            {
                                ExtractAttachments(true);
                            }

                            RemoveAttachments();
                        }
                        
                        mailItem.SaveAs(tempMsgFilename, GlobalConfig.GlobalSettings.MessageSaveAsType);
                        HandleSavedMSG(mailItem);
                    }
                }

                //move and rename the temp filename to its final location
                try
                {
                    var fileinfo = new FileInfo(tempMsgFilename);
                    fileinfo.MoveTo(saveMsgFilename);
                    fileinfo.CreationTime = mailItem.SentOn;
                }
                catch (Exception ex)
                {

                    MessageBox.Show($"{ex.Message}{Environment.NewLine}{GlobalConfig.localResourceManager.GetString("Message005")}", "Error moving temp MSG file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Crashes.TrackError(ex);
                }

            }
            else // FILE TO OUTLOOK FOLDER
            {
                if (this.rdoWorkflow01.Checked) // file message with attachments embedded
                {
                    Outlook.Folder objFolder;
                    Outlook.NameSpace objNS;
                    objNS = Globals.ThisAddIn.Application.GetNamespace("MAPI");

                    //check if StoreID existing
                    if(_selectedPath.StoreID != string.Empty)
                    {
                        objFolder = (Outlook.Folder)objNS.GetFolderFromID(_selectedPath.FolderID, _selectedPath.StoreID);
                    }
                    else
                    {
                        objFolder = (Outlook.Folder)objNS.GetFolderFromID(_selectedPath.FolderID );
                    }
                    
                    if (objFolder is object)
                    {
                        mailItem.Move(objFolder);
                    }
                }
                else if (rdoWorkflow02.Checked) // file message with attachments embedded and saved in file system
                {
                    Outlook.Folder objFolder;
                    Outlook.NameSpace objNS;
                    objNS = Globals.ThisAddIn.Application.GetNamespace("MAPI");

                    //check if StoreID existing
                    if (_selectedPath.StoreID != string.Empty)
                    {
                        objFolder = (Outlook.Folder)objNS.GetFolderFromID(_selectedPath.FolderID, _selectedPath.StoreID);
                    }
                    else
                    {
                        objFolder = (Outlook.Folder)objNS.GetFolderFromID(_selectedPath.FolderID);
                    }

                    if (objFolder is object)
                    {
                        if (HasNonEmbeddedAttachments() == true)
                        {
                            ExtractAttachments(true);
                            mailItem.Move(objFolder);

                            //// now remove attachments from the message.
                            //if (ExtractAttachments(true) == true)
                            //{
                            //    RemoveAttachments();
                            //    mailItem.Move(objFolder);
                            //}
                        }
                    }
                }
                else if (rdoWorkflow03.Checked) // file message with attachments removed and saved in file system
                {
                    Outlook.Folder objFolder;
                    Outlook.NameSpace objNS;
                    objNS = Globals.ThisAddIn.Application.GetNamespace("MAPI");

                    //check if StoreID existing
                    if (_selectedPath.StoreID != string.Empty)
                    {
                        objFolder = (Outlook.Folder)objNS.GetFolderFromID(_selectedPath.FolderID, _selectedPath.StoreID);
                    }
                    else
                    {
                        objFolder = (Outlook.Folder)objNS.GetFolderFromID(_selectedPath.FolderID);
                    }

                    if (objFolder is object)
                    {
                        if (_sentMail == true)
                        {
                            if (HasNonEmbeddedAttachments() == true)
                            {
                                // now remove attachments from the message.
                                RemoveAttachments();
                            }

                            mailItem.Move(objFolder);
                        }
                        else
                        {
                            if (HasNonEmbeddedAttachments() == true)
                            {
                                if (MessageBox.Show(GlobalConfig.localResourceManager.GetString("Message004"), "Save Attachments", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    ExtractAttachments(false);
                                }
                                else
                                {
                                    ExtractAttachments(true);
                                }

                                RemoveAttachments();
                            }

                            mailItem.Move(objFolder);
                        }
                    }
                }
            }
        }

        private void BatchFile()
        {
            this.toolStripProgressBar1.Visible = true;
            this.toolStripProgressBar1.Maximum = _mailItems.Count;
            this.toolStripProgressBar1.Value = 0;

            foreach (var item in _mailItems)
            {
                FileMessage(item);
                this.toolStripProgressBar1.Value++;
            }

            this.toolStripProgressBar1.Visible = false;
        }
    }
}
