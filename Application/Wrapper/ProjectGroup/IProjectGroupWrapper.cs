using Core.Dto.PMDb;

namespace Application.Wrapper.ProjectGroup
{
    public interface IProjectGroupWrapper
    {
        void CreateData(Projectgroup model);
        void Delete(Projectgroup model);
        IEnumerable<Projectgroup> GetAllData();
        Projectgroup GetDataById(int id);
        void Update(Projectgroup model);
    }
}