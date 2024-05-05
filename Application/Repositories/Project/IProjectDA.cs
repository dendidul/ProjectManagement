using Core.Dto.PMDb;

namespace Application.Repositories.Project
{
    public interface IProjectDA
    {
        void CreateData(Core.Dto.PMDb.Project model);
        void Delete(Core.Dto.PMDb.Project model);
        IEnumerable<ViewAllProject> GetAllData();
        ViewModelsProjectEmployee GetAllProjectByEmployeeId(int id);
        IEnumerable<ViewProjectGroupEmployeeModels> GetAllProjectsByEmployeeId(int id);
        Core.Dto.PMDb.Project GetDataById(int id);
        void Update(Core.Dto.PMDb.Project model);
    }
}