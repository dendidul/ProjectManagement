using Core.Dto.PMDb;

namespace Application.Wrapper.TaskLog
{
    public interface ITaskLogWrapper
    {
        void CreateData(Tasklog model);
    }
}