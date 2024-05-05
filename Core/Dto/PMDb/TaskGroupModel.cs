using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.PMDb
{
    public  class TaskGroupModel
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string TaskGroupName { get; set; }
        public bool IsActive { get; set; }
        public bool del_flag { get; set; }
    }
}
