using Core.Dto.PMDb;

namespace Application.Repositories.Roles
{
    public interface IRolesDA
    {
        void CreateData(Role model);
        void Delete(Role model);
        IEnumerable<Role> GetAllData();
        Role GetDataById(int id);
        void Update(Role model);
    }
}