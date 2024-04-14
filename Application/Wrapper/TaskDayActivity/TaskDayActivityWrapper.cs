using Application.Repositories.TaskDayActivity;
using Core.Dto.PMDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrapper.TaskDayActivity
{
    public class TaskDayActivityWrapper : ITaskDayActivityWrapper
    {
        private readonly ITaskDayActivityDA _taskDayActivityDA;

        public TaskDayActivityWrapper(ITaskDayActivityDA taskDayActivityDA)
        {
            _taskDayActivityDA = taskDayActivityDA;
        }

        public void CreateData(Taskdayactivity model)
        {
            _taskDayActivityDA.CreateData(model);
        }
        public void Delete(Taskdayactivity model)
        {
            _taskDayActivityDA.Delete(model);
        }
        public IEnumerable<Taskdayactivity> GetDataByTaskId(int id, int EmployeeId)
        {
            return _taskDayActivityDA.GetDataByTaskId(id, EmployeeId);
        }

    }
}
