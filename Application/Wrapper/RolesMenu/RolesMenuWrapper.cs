using Application.Repositories.RolesMenu;
using Core.Dto.PMDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrapper.RolesMenu
{
    public class RolesMenuWrapper : IRolesMenuWrapper
    {
        private readonly IRolesMenuDA _rolesMenuDA;

        public RolesMenuWrapper(IRolesMenuDA rolesMenuDA)
        {
            _rolesMenuDA = rolesMenuDA;
        }

        public ViewModelsProjectEmployee GetAllProjectByEmployeeId(int id)
        {
            return _rolesMenuDA.GetAllProjectByEmployeeId(id);
        }

        public IList<RolesMenuViewModel> BuildRoleMenu(int roleid)
        {
            return _rolesMenuDA.BuildRoleMenu(roleid);
        }
        public bool CheckMenuForRoles(int RolesID, string Controller)
        {
            return _rolesMenuDA.CheckMenuForRoles(RolesID, Controller);
        }
        public void CreateData(Rolesmenu model)
        {
            _rolesMenuDA.CreateData(model);
        }

        public void Delete(Rolesmenu model)
        {
            _rolesMenuDA.Delete(model);
        }
        public IEnumerable<Rolesmenu> GetAllData()
        {
            return _rolesMenuDA.GetAllData();
        }
        public Rolesmenu GetDataById(int id)
        {
            return _rolesMenuDA.GetDataById(id);
        }
        public IEnumerable<Rolesmenu> GetRoleMenuByRolesId(int id)
        {
            return _rolesMenuDA.GetRoleMenuByRolesId(id);
        }
        public AuthorityMenuModel ReadRoleMenuByRoleID(int roleID)
        {
            return _rolesMenuDA.ReadRoleMenuByRoleID(roleID);
        }
        public void Update(Rolesmenu model)
        {
            _rolesMenuDA.Update(model);
        }
    }
}
