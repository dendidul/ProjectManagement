using Core.Dto.PMDb;

namespace Application.Repositories.TaskLog
{
    public interface ITaskLogDA
    {
        void CreateData(Tasklog model);
    }
}