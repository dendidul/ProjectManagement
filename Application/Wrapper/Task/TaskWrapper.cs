using Application.Repositories.Status;
using Application.Repositories.Task;
using Core.Dto.PMDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrapper.Task
{
    public class TaskWrapper : ITaskWrapper
    {
        private readonly ITaskDA _taskDA;

        public TaskWrapper(ITaskDA taskDA)
        {
            _taskDA = taskDA;
        }

        public void CreateData(Core.Dto.PMDb.Task model)
        {
            _taskDA.CreateData(model);
        }
        public void Delete(Core.Dto.PMDb.Task model)
        {
            _taskDA.Delete(model);
        }
        public void DeleteRow(Core.Dto.PMDb.Task model)
        {
            _taskDA.DeleteRow(model);
        }
        public IEnumerable<Core.Dto.PMDb.Task> GetAllData()
        {
            return _taskDA.GetAllData();
        }
        public Core.Dto.PMDb.Task GetData(int id)
        {
            return _taskDA.GetData(id);
        }
        public FormTaskModel GetDataById(int id, int employeeId)
        {
            return _taskDA.GetDataById(id, employeeId);
        }
        public FormTaskModel GetDataByIdForActivityMonitoring(int id)
        {
            return _taskDA.GetDataByIdForActivityMonitoring(id);
        }
        public IEnumerable<TaskModels> GetListActivityReviewByReviewEmployeeAndProjects(int EmployeeId, int ProjectId)
        {
            return _taskDA.GetListActivityReviewByReviewEmployeeAndProjects(EmployeeId, ProjectId);
        }
        public IEnumerable<TaskModels> GetListBackLogByProjectId(int ProjectId)
        {
            return _taskDA.GetListBackLogByProjectId(ProjectId);
        }
        public IEnumerable<TaskModels> GetListTaskByAssignedEmployee(int EmployeeId)
        {
            return _taskDA.GetListTaskByAssignedEmployee(EmployeeId);
        }
        public IEnumerable<TaskModels> GetListTaskByAssignedEmployeeAndProjects(int EmployeeId, int ProjectId)
        {
            return _taskDA.GetListTaskByAssignedEmployeeAndProjects(EmployeeId, ProjectId);
        }
        public IEnumerable<TaskModels> GetListTaskByEmployeeCreated(int EmployeeId)
        {
            return _taskDA.GetListTaskByEmployeeCreated(EmployeeId);
        }
        public IEnumerable<TaskModels> GetListTaskByProjectId(int ProjectId)
        {
            return (_taskDA.GetListTaskByProjectId(ProjectId));
        }
        public IEnumerable<TaskModels> GetListTaskForAdmin()
        {
            return _taskDA.GetListTaskForAdmin();
        }
        public BackLogModel GetSprintBackLogDataByTaskGroupID(int id)
        {
            return _taskDA.GetSprintBackLogDataByTaskGroupID(id);
        }
        public IEnumerable<Core.Dto.PMDb.Task> GetTaskByTaskGroupIdAndProjectId(int id, int ProjectId)
        {
            return _taskDA.GetTaskByTaskGroupIdAndProjectId(id, ProjectId);
        }
        public void Update(Core.Dto.PMDb.Task model)
        {
            _taskDA.Update(model);
        }
    }
}
