using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.PMDb
{
    public class TaskDayActivities
    {
        public int id { get; set; }
        public Nullable<int> TaskId { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<decimal> Estimate { get; set; }
        public string Description { get; set; }

        public string EmployeeName { get; set; }
    }
}
