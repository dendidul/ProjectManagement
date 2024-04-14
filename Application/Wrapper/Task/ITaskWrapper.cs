using Core.Dto.PMDb;

namespace Application.Wrapper.Task
{
    public interface ITaskWrapper
    {
        void CreateData(Core.Dto.PMDb.Task model);
        void Delete(Core.Dto.PMDb.Task model);
        void DeleteRow(Core.Dto.PMDb.Task model);
        IEnumerable<Core.Dto.PMDb.Task> GetAllData();
        Core.Dto.PMDb.Task GetData(int id);
        FormTaskModel GetDataById(int id, int employeeId);
        FormTaskModel GetDataByIdForActivityMonitoring(int id);
        IEnumerable<TaskModels> GetListActivityReviewByReviewEmployeeAndProjects(int EmployeeId, int ProjectId);
        IEnumerable<TaskModels> GetListBackLogByProjectId(int ProjectId);
        IEnumerable<TaskModels> GetListTaskByAssignedEmployee(int EmployeeId);
        IEnumerable<TaskModels> GetListTaskByAssignedEmployeeAndProjects(int EmployeeId, int ProjectId);
        IEnumerable<TaskModels> GetListTaskByEmployeeCreated(int EmployeeId);
        IEnumerable<TaskModels> GetListTaskByProjectId(int ProjectId);
        IEnumerable<TaskModels> GetListTaskForAdmin();
        BackLogModel GetSprintBackLogDataByTaskGroupID(int id);
        IEnumerable<Core.Dto.PMDb.Task> GetTaskByTaskGroupIdAndProjectId(int id, int ProjectId);
        void Update(Core.Dto.PMDb.Task model);
    }
}