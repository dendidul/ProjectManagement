using Application.Repositories.Bugs;
using Core.Dto.PMDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrapper.Bugs
{
    public class BugsWrapper : IBugsWrapper
    {
        private readonly IBugsDA _bugsDA;

        public BugsWrapper(IBugsDA bugsDA)
        {
            _bugsDA = bugsDA;

        }



        public FormTaskModel GetDataById(int id, int employeeId)
        {
            var data = _bugsDA.GetDataById(id, employeeId);
            return data;
        }
        public IEnumerable<TaskModels> GetListBugsByAssignedEmployee(int EmployeeId)
        {
            var data = _bugsDA.GetListBugsByAssignedEmployee(EmployeeId);
            return data;
        }
        public IEnumerable<TaskModels> GetListBugsByAssignedEmployeeAndProjects(int EmployeeId, int ProjectId)
        {
            var data = _bugsDA.GetListBugsByAssignedEmployeeAndProjects(EmployeeId, ProjectId);
            return data;
        }
        public IEnumerable<TaskModels> GetListBugsByEmployeeCreated(int EmployeeId)
        {
            var data = _bugsDA.GetListBugsByEmployeeCreated(EmployeeId);
            return data;
        }
        public IEnumerable<TaskModels> GetListBugsByProjectId(int ProjectId)
        {
            var data = _bugsDA.GetListBugsByProjectId(ProjectId);
            return data;
        }

        public IEnumerable<TaskModels> GetListBugsForAdmin()
        {
            var data = _bugsDA.GetListBugsForAdmin();
            return data;
        }
    }
}
