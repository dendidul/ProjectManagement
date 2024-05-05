using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.TaskLog
{
    public class TaskLogDA : ITaskLogDA
    {
        ProjectManagementEntities db = new ProjectManagementEntities();
        public void CreateData(Core.Dto.PMDb.Tasklog model)
        {
            model.DelFlag = false;
            db.Tasklogs.Add(model);
            db.SaveChanges();
        }
    }
}
