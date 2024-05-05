using Core.Dto.PMDb;

namespace Application.Repositories.Bugs
{
    public interface IBugsDA
    {
        FormTaskModel GetDataById(int id, int employeeId);
        IEnumerable<TaskModels> GetListBugsByAssignedEmployee(int EmployeeId);
        IEnumerable<TaskModels> GetListBugsByAssignedEmployeeAndProjects(int EmployeeId, int ProjectId);
        IEnumerable<TaskModels> GetListBugsByEmployeeCreated(int EmployeeId);
        IEnumerable<TaskModels> GetListBugsByProjectId(int ProjectId);
        IEnumerable<TaskModels> GetListBugsForAdmin();
    }
}