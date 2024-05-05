using Core.Dto.PMDb;

namespace Application.Wrapper.Global
{
    public interface IGlobalWrapper
    {
        decimal GetProjectProgress(int id);
        ProgressBarModel ProgressBugsByProject(int id);
        ProgressBarModel ProgressTaskByProject(int id);
    }
}