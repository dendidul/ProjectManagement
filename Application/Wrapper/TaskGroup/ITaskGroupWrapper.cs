using Core.Dto.PMDb;

namespace Application.Wrapper.TaskGroup
{
    public interface ITaskGroupWrapper
    {
        void CreateData(Taskgroup model);
        void Delete(Taskgroup model);
        IEnumerable<Taskgroup> GetActiveTaskGroup(int ProjectId);
        IEnumerable<Taskgroup> GetAllData();
        IEnumerable<TaskGroupModel> GetAllDataByEmployee(int id, int Projectid);
        IEnumerable<TaskGroupModel> GetAllDataTaskGroupByProjectID(int Projectid);
        Taskgroup GetDataById(int id);
        void Update(Taskgroup model);
    }
}