
namespace eMailManager
{
    partial class RibbonExplorer : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public RibbonExplorer()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RibbonExplorer));
            this.tabEmailManager = this.Factory.CreateRibbonTab();
            this.groupEM = this.Factory.CreateRibbonGroup();
            this.buttonFileMessage = this.Factory.CreateRibbonButton();
            this.buttonSearch = this.Factory.CreateRibbonButton();
            this.buttonSettings = this.Factory.CreateRibbonButton();
            this.tabEmailManager.SuspendLayout();
            this.groupEM.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabEmailManager
            // 
            this.tabEmailManager.Groups.Add(this.groupEM);
            resources.ApplyResources(this.tabEmailManager, "tabEmailManager");
            this.tabEmailManager.Name = "tabEmailManager";
            // 
            // groupEM
            // 
            this.groupEM.Items.Add(this.buttonFileMessage);
            this.groupEM.Items.Add(this.buttonSearch);
            this.groupEM.Items.Add(this.buttonSettings);
            resources.ApplyResources(this.groupEM, "groupEM");
            this.groupEM.Name = "groupEM";
            // 
            // buttonFileMessage
            // 
            this.buttonFileMessage.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonFileMessage.Image = global::eMailManager.Properties.Resources.MailArchive;
            resources.ApplyResources(this.buttonFileMessage, "buttonFileMessage");
            this.buttonFileMessage.Name = "buttonFileMessage";
            this.buttonFileMessage.ShowImage = true;
            this.buttonFileMessage.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonFileMessage_Click);
            // 
            // buttonSearch
            // 
            this.buttonSearch.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            resources.ApplyResources(this.buttonSearch, "buttonSearch");
            this.buttonSearch.Image = global::eMailManager.Properties.Resources.Search;
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.ShowImage = true;
            this.buttonSearch.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonSearch_Click);
            // 
            // buttonSettings
            // 
            this.buttonSettings.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            resources.ApplyResources(this.buttonSettings, "buttonSettings");
            this.buttonSettings.Image = global::eMailManager.Properties.Resources.Settings;
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.ShowImage = true;
            this.buttonSettings.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonSettings_Click);
            // 
            // RibbonExplorer
            // 
            this.Name = "RibbonExplorer";
            this.RibbonType = "Microsoft.Outlook.Explorer";
            this.Tabs.Add(this.tabEmailManager);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.RibbonExplorer_Load);
            this.tabEmailManager.ResumeLayout(false);
            this.tabEmailManager.PerformLayout();
            this.groupEM.ResumeLayout(false);
            this.groupEM.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupEM;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonFileMessage;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonSettings;
        internal Microsoft.Office.Tools.Ribbon.RibbonTab tabEmailManager;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonSearch;
    }

    partial class ThisRibbonCollection
    {
        internal RibbonExplorer RibbonExplorer
        {
            get { return this.GetRibbon<RibbonExplorer>(); }
        }
    }
}
