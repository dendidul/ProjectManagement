using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.PMDb
{
    public class ViewAllProject
    {
        public string ProjectGroupName { get; set; }
        public Nullable<int> ProjectGroupId { get; set; }
        public string ProjectName { get; set; }
        public Nullable<bool> IsPublic { get; set; }
        public int ProjectId { get; set; }
        public Nullable<bool> del_flag { get; set; }
    }
}
