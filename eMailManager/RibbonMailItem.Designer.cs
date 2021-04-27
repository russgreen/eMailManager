
namespace eMailManager
{
    partial class RibbonMailItem : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public RibbonMailItem()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RibbonMailItem));
            this.tab1 = this.Factory.CreateRibbonTab();
            this.groupEM = this.Factory.CreateRibbonGroup();
            this.buttonFileMessage = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.groupEM.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.ControlId.OfficeId = "TabReadMessage";
            this.tab1.Groups.Add(this.groupEM);
            resources.ApplyResources(this.tab1, "tab1");
            this.tab1.Name = "tab1";
            // 
            // groupEM
            // 
            this.groupEM.Items.Add(this.buttonFileMessage);
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
            // RibbonMailItem
            // 
            this.Name = "RibbonMailItem";
            this.RibbonType = "Microsoft.Outlook.Mail.Read";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.RibbonMailItem_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.groupEM.ResumeLayout(false);
            this.groupEM.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupEM;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonFileMessage;
    }

    partial class ThisRibbonCollection
    {
        internal RibbonMailItem RibbonMailItem
        {
            get { return this.GetRibbon<RibbonMailItem>(); }
        }
    }
}
