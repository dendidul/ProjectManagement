using Core.Dto.PMDb;

namespace Application.Repositories.ProjectGroup
{
    public interface IProjectGroupDA
    {
        void CreateData(Projectgroup model);
        void Delete(Projectgroup model);
        IEnumerable<Projectgroup> GetAllData();
        Projectgroup GetDataById(int id);
        void Update(Projectgroup model);
    }
}