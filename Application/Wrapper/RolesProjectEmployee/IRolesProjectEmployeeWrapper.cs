using Core.Dto.PMDb;

namespace Application.Wrapper.RolesProjectEmployee
{
    public interface IRolesProjectEmployeeWrapper
    {
        void CreateData(Rolesprojectemployee model);
        void Delete(Rolesprojectemployee model);
        IEnumerable<ViewRolesProjectEmployee> GetAllData(int ProjectId);
        Rolesprojectemployee GetDataById(int id);
        IEnumerable<UserProjectModel> GetEmployeeByProject(int ProjectId);
        Rolesprojectemployee GetRolesByProjectAndEmployeeID(int projectId, int employeeid);
        void Update(Rolesprojectemployee model);
    }
}