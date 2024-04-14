using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.PMDb
{
    public  class ViewDocumentByProject
    {
        public int id { get; set; }
        public string DocumentName { get; set; }
        public string ProjectsName { get; set; }
        public int projectid { get; set; }
    }
}
