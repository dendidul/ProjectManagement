using Core.Dto.PMDb;

namespace Application.Repositories.TaskDayActivity
{
    public interface ITaskDayActivityDA
    {
        void CreateData(Taskdayactivity model);
        void Delete(Taskdayactivity model);
        IEnumerable<Taskdayactivity> GetDataByTaskId(int id, int EmployeeId);
    }
}