using Core.Dto.PMDb;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.TaskDayActivity
{
    public class TaskDayActivityDA : ITaskDayActivityDA
    {
        ProjectManagementEntities db = new ProjectManagementEntities();

      
        public IEnumerable<Taskdayactivity> GetDataByTaskId(int id, int EmployeeId)
        {
            var data = db.Taskdayactivities.Where(x => x.Taskid == id && x.Employeeid == EmployeeId && x.DelFlag == false).ToList();
            return data;
        }

        

        public void CreateData(Taskdayactivity model)
        {
            db.Taskdayactivities.Add(model);
            db.SaveChanges();
        }

       

        public void Delete(Taskdayactivity model)
        {
            Taskdayactivity item = db.Taskdayactivities.Find(model.Id);
            db.Taskdayactivities.Remove(item);
            db.SaveChanges();
        }
    }
}
