using Core.Dto.PMDb;

namespace Application.Wrapper.Timesheet
{
    public interface ITimesheetWrapper
    {
        IEnumerable<TimesheetModel> GetTimesheet(int ProjectId, DateTime StartDate, DateTime DueDate, int EmpId);
    }
}