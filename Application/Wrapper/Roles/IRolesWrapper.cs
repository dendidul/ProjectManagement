using Core.Dto.PMDb;

namespace Application.Wrapper.Roles
{
    public interface IRolesWrapper
    {
        void CreateData(Role model);
        void Delete(Role model);
        IEnumerable<Role> GetAllData();
        Role GetDataById(int id);
        void Update(Role model);
    }
}