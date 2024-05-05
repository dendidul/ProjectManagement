using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.PMDb
{
    public class FormAttachmentFilesModel
    {
        public string GUID { get; set; }
        public string Path { get; set; }
        public string filetype { get; set; }
        public bool delflag { get; set; }

        public int id { get; set; }
        public int TransactionID { get; set; }

    }
}
