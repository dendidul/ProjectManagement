using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.PMDb
{
    public class DocumentModel
    {
        public int id { get; set; }
        public string DocumentName { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public List<Attachmentfile> ListAttachmentFiles { get; set; }
    }
}
