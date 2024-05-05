using Core.Dto.PMDb;

namespace Application.Repositories.Global
{
    public interface IGlobalDA
    {
        decimal GetProjectProgress(int id);
        ProgressBarModel ProgressBugsByProject(int id);
        ProgressBarModel ProgressTaskByProject(int id);
    }
}