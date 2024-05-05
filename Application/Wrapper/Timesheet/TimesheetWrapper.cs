using Application.Repositories.Timesheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Dto.PMDb; 

namespace Application.Wrapper.Timesheet
{
    public class TimesheetWrapper : ITimesheetWrapper
    {
        private readonly ITimesheetDA _timesheetDA;

        public TimesheetWrapper(ITimesheetDA timesheetDA)
        {
            _timesheetDA = timesheetDA;
        }

        public IEnumerable<TimesheetModel> GetTimesheet(int ProjectId, DateTime StartDate, DateTime DueDate, int EmpId)
        {
            return _timesheetDA.GetTimesheet(ProjectId, StartDate, DueDate, EmpId);
        }
    }
}
