using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMailManager.Models
{
    public class MessageStoreModel
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public string Path { get; set; }

        public string FolderID { get; set; }

        public string StoreID { get; set; }

        public bool IsOutlookPath 
        {
            get
            {
                //if the path = folder id then its not an outlook folder
                if (Path == FolderID)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
