using Application.Repositories.TaskGroup;
using Core.Dto.PMDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrapper.TaskGroup
{
    public class TaskGroupWrapper : ITaskGroupWrapper
    {

        private readonly ITaskGroupDA _taskGroupDA;

        public TaskGroupWrapper(ITaskGroupDA taskGroupDA)
        {
            _taskGroupDA = taskGroupDA;
        }


        public void CreateData(Taskgroup model)
        {
            _taskGroupDA.CreateData(model);
        }
        public void Delete(Taskgroup model)
        {
            _taskGroupDA.Delete(model);
        }
        public IEnumerable<Taskgroup> GetActiveTaskGroup(int ProjectId)
        {
            return _taskGroupDA.GetActiveTaskGroup(ProjectId);
        }
        public IEnumerable<Taskgroup> GetAllData()
        {
            return _taskGroupDA.GetAllData();
        }
        public IEnumerable<TaskGroupModel> GetAllDataByEmployee(int id, int Projectid)
        {
            return _taskGroupDA.GetAllDataByEmployee(id, Projectid);
        }
        public IEnumerable<TaskGroupModel> GetAllDataTaskGroupByProjectID(int Projectid)
        {
            return _taskGroupDA.GetAllDataTaskGroupByProjectID(Projectid);
        }
        public Taskgroup GetDataById(int id)
        {
            return _taskGroupDA.GetDataById(id);
        }
        public void Update(Taskgroup model)
        {
            _taskGroupDA.Update(model);
        }

    }
}
