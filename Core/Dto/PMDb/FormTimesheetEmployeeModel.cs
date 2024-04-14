using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.PMDb
{
    public class FormTimesheetEmployeeModel
    {
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ProjectId { get; set; }
    }
}
