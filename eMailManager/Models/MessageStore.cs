using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMailManager.Models
{
    internal class MessageStore
    {
        public string Description { get; set; }

        public string Path { get; set; }

        public string FolderID { get; set; }

        public string StoreID { get; set; }
    }
}
