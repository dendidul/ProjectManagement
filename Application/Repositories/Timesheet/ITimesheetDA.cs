using Core.Dto.PMDb;

namespace Application.Repositories.Timesheet
{
    public interface ITimesheetDA
    {
        IEnumerable<TimesheetModel> GetTimesheet(int ProjectId, DateTime StartDate, DateTime DueDate, int EmpId);
    }
}