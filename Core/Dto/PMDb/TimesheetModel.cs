using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.PMDb
{
    public class TimesheetModel
    {
        public string ProjectsName { get; set; }
        public string TaskName { get; set; }
        public string SeverityName { get; set; }
        public string TypeName { get; set; }
        public string EmployeeName { get; set; }
        public string Descripition { get; set; }
        public decimal Estimate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
