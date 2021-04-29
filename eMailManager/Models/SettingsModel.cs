using eMailManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMailManager.Models
{
    public class SettingsModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        public ObservableCollection<MessageStoreModel> MessageStores = new ObservableCollection<MessageStoreModel>();

        public string FilenameFilter { get; set; } = "<sent>_<from>_<subject>";

        // OUTLOOK SAVEAS FILE TYPES
        // 0	-	olTXT	-	Text format (.txt)
        // 1	-	olRTF	-	Rich Text format (.rtf)
        // 2	-	olTemplate	-	Microsoft Office Outlook template (.oft)
        // 3	-	olMSG	-	Outlook message format (.msg)
        // 4	-	olDoc	-	Microsoft Office Word format (.doc)
        // 5	-	olHTML	-	HTML format (.html)
        // 6	-	olVCard	-	VCard format (.vcf)
        // 7	-	olVCal	-	VCal format (.vcs)
        // 8	-	olICal	-	iCal format (.ics)
        // 9	-	olMSGUnicode	-	Outlook Unicode message format (.msg)
        // 10	-	olMHTML	-	MIME HTML format (.mht)
        public int MessageSaveAsType { get; set; } = 3;        
        public string MessageFileExt { get; set; } = ".msg";
        public bool RememberLastLocation { get; set; } = false;
        public string LastLocation { get; set; } = string.Empty;
        public string ArchivedAndRetainedCategory { get; set; } = "eMail Manager Filed";
        public bool LeaveCopy { get; set; } = false;
        public bool AlwaysClearDeletedItems { get; set; } = false;
        public bool MonitorSentItems { get; set; } = true;
        public bool UseFileSystem { get; set; } = true;
        public int FormWidth { get; set; } = 705;
        public int FormHeight { get; set; } = 520;

    }
}
