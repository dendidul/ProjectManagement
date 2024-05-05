using Core.Dto.PMDb;

namespace Application.Wrapper.TaskDayActivity
{
    public interface ITaskDayActivityWrapper
    {
        void CreateData(Taskdayactivity model);
        void Delete(Taskdayactivity model);
        IEnumerable<Taskdayactivity> GetDataByTaskId(int id, int EmployeeId);
    }
}