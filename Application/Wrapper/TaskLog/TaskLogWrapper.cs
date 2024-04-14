using Application.Repositories.TaskLog;
using Core.Dto.PMDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrapper.TaskLog
{
    public class TaskLogWrapper : ITaskLogWrapper
    {
        private readonly ITaskLogDA _taskLogDA;

        public TaskLogWrapper(ITaskLogDA taskLogDA)
        {
            _taskLogDA = taskLogDA;
        }

        public void CreateData(Tasklog model)
        {
            _taskLogDA.CreateData(model);
        }

    }
}
