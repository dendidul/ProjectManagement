using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.PMDb
{
    public class NewsFeedModels
    {
        public string EmployeeName { get; set; }
        public string TaskName { get; set; }
        public string StatusName { get; set; }
        public int TaskId { get; set; }
        public int EmployeeId { get; set; }
        public string PhotoUrl { get; set; }
        public int StatusId { get; set; }
        public int ProjectId { get; set; }
        public int TypeId { get; set; }
        public string TypeName { get; set; }
    }
}
