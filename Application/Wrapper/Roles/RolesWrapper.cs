using Application.Repositories.Roles;
using Core.Dto.PMDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrapper.Roles
{
    public class RolesWrapper : IRolesWrapper
    {
        private readonly IRolesDA _rolesDA;

        public RolesWrapper(IRolesDA rolesDA)
        {
            _rolesDA = rolesDA;
        }

        public void CreateData(Role model)
        {
            _rolesDA.CreateData(model);
        }
        public void Delete(Role model)
        {
            _rolesDA.Delete(model);
        }
        public IEnumerable<Role> GetAllData()
        {
            return _rolesDA.GetAllData();
        }
        public Role GetDataById(int id)
        {
            return _rolesDA.GetDataById(id);
        }
        public void Update(Role model)
        {
            _rolesDA.Update(model);
        }
    }
}
