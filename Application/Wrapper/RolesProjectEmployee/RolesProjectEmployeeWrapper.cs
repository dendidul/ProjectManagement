using Application.Repositories.RolesProjectEmployee;
using Core.Dto.PMDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrapper.RolesProjectEmployee
{
    public class RolesProjectEmployeeWrapper : IRolesProjectEmployeeWrapper
    {
        private readonly IRolesProjectEmployeeDA _rolesProjectEmployeeDA;

        public RolesProjectEmployeeWrapper(IRolesProjectEmployeeDA rolesProjectEmployeeDA)
        {
            _rolesProjectEmployeeDA = rolesProjectEmployeeDA;
        }

        public void CreateData(Rolesprojectemployee model)
        {
            _rolesProjectEmployeeDA.CreateData(model);
        }
        public void Delete(Rolesprojectemployee model)
        {
            _rolesProjectEmployeeDA.Delete(model);
        }
        public IEnumerable<ViewRolesProjectEmployee> GetAllData(int ProjectId)
        {
            return _rolesProjectEmployeeDA.GetAllData(ProjectId);
        }
        public Rolesprojectemployee GetDataById(int id)
        {
            return _rolesProjectEmployeeDA.GetDataById(id);
        }
        public IEnumerable<UserProjectModel> GetEmployeeByProject(int ProjectId)
        {
            return _rolesProjectEmployeeDA.GetEmployeeByProject(ProjectId);
        }
        public Rolesprojectemployee GetRolesByProjectAndEmployeeID(int projectId, int employeeid)
        {
            return _rolesProjectEmployeeDA.GetRolesByProjectAndEmployeeID(projectId, employeeid);
        }
        public void Update(Rolesprojectemployee model)
        {
            _rolesProjectEmployeeDA.Update(model);
        }
    }
}
