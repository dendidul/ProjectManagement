using Core.Dto.PMDb;

namespace Application.Wrapper.Bugs
{
    public interface IBugsWrapper
    {
        FormTaskModel GetDataById(int id, int employeeId);
        IEnumerable<TaskModels> GetListBugsByAssignedEmployee(int EmployeeId);
        IEnumerable<TaskModels> GetListBugsByAssignedEmployeeAndProjects(int EmployeeId, int ProjectId);
        IEnumerable<TaskModels> GetListBugsByEmployeeCreated(int EmployeeId);
        IEnumerable<TaskModels> GetListBugsByProjectId(int ProjectId);
        IEnumerable<TaskModels> GetListBugsForAdmin();
    }
}