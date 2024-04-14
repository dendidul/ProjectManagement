using Core.Dto.PMDb;

namespace Application.Repositories.RolesProjectEmployee
{
    public interface IRolesProjectEmployeeDA
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