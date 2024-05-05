using Core.Dto.PMDb;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.Bugs
{
    public class BugsDA : IBugsDA
    {
        ProjectManagementEntities db = new ProjectManagementEntities();
        public IEnumerable<TaskModels> GetListBugsByAssignedEmployeeAndProjects(int EmployeeId, int ProjectId)
        {
            var data = (from a in db.Tasks
                        join e in db.Employees on a.Assignto equals e.Id
                        //join tg in db.TaskGroups on a.TaskGroupId equals tg.id
                        join st in db.Statuses on a.Statusid equals st.Id
                        join pr in db.Projects on a.Projectid equals pr.Id
                        join s in db.Severities on a.Severityid equals s.Id
                        where (a.Assignto == EmployeeId || a.Createdby == EmployeeId) && a.Projectid == ProjectId
                        && a.DelFlag == false
                        && e.DelFlag == false
                        //&& tg.del_flag == false
                        && st.DelFlag == false
                        && pr.DelFlag == false
                        && s.DelFlag == false
                        && a.Type == 2
                        select new TaskModels
                        {
                            AssignedTo = e.Firstname + " " + e.Lastname,
                            Description = a.Descripition,
                            Id = a.Id,
                            ProjectName = pr.Projectsname,
                            TaskName = a.Taskname,
                            //TaskGroup = tg.TaskGroupName,
                            Severity = s.Severityname,
                            StatusName = st.Statusname
                        }).ToList();
            return data;

        }

        public IEnumerable<TaskModels> GetListBugsForAdmin()
        {
            var data = (from a in db.Tasks
                        join e in db.Employees on a.Assignto equals e.Id
                        //join tg in db.TaskGroups on a.TaskGroupId equals tg.id
                        join st in db.Statuses on a.Statusid equals st.Id
                        join pr in db.Projects on a.Projectid equals pr.Id
                        join s in db.Severities on a.Severityid equals s.Id
                        where

                        a.DelFlag == false
                        && e.DelFlag == false
                        //&& tg.del_flag == false
                        && st.DelFlag == false
                        && pr.DelFlag == false
                        && s.DelFlag == false
                        && a.Type == 2
                        select new TaskModels
                        {
                            AssignedTo = e.Firstname + " " + e.Lastname,
                            Description = a.Descripition,
                            Id = a.Id,
                            ProjectName = pr.Projectsname,
                            TaskName = a.Taskname,
                            //TaskGroup = tg.TaskGroupName,
                            Severity = s.Severityname,
                            StatusName = st.Statusname
                        }).ToList();
            return data;

        }

        public IEnumerable<TaskModels> GetListBugsByAssignedEmployee(int EmployeeId)
        {
            var data = (from a in db.Tasks
                        join e in db.Employees on a.Assignto equals e.Id
                        join tg in db.Taskgroups on a.Taskgroupid equals tg.Id
                        join st in db.Statuses on a.Statusid equals st.Id
                        join pr in db.Projects on a.Projectid equals pr.Id
                        join s in db.Severities on a.Severityid equals s.Id
                        where a.Assignto == EmployeeId
                        && a.DelFlag == false
                        && e.DelFlag == false
                        && tg.DelFlag == false
                        && st.DelFlag == false
                        && pr.DelFlag == false
                        && s.DelFlag == false
                         && a.Type == 2
                        select new TaskModels
                        {
                            AssignedTo = e.Firstname + " " + e.Lastname,
                            Description = a.Descripition,
                            Id = a.Id,
                            ProjectName = pr.Projectsname,
                            TaskName = a.Taskname,
                            TaskGroup = tg.Taskgroupname,
                            Severity = s.Severityname,
                            StatusName = st.Statusname
                        }).ToList();
            return data;

        }

        public IEnumerable<TaskModels> GetListBugsByEmployeeCreated(int EmployeeId)
        {
            var data = (from a in db.Tasks
                        join e in db.Employees on a.Assignto equals e.Id
                        join tg in db.Taskgroups on a.Taskgroupid equals tg.Id
                        join st in db.Statuses on a.Statusid equals st.Id
                        join pr in db.Projects on a.Projectid equals pr.Id
                        join s in db.Severities on a.Severityid equals s.Id
                        where a.Createdby == EmployeeId
                        && a.DelFlag == false
                        && e.DelFlag == false
                        && tg.DelFlag == false
                        && st.DelFlag == false
                        && pr.DelFlag == false
                        && s.DelFlag == false
                         && a.Type == 2
                        select new TaskModels
                        {
                            //AssignedTo = e.FirstName + " " + e.LastName,
                            //Description = a.Descripition,
                            //Id = a.id,
                            //ProjectName = pr.ProjectsName,
                            //TaskName = a.TaskName,
                            //TaskGroup = tg.TaskGroupName,
                            //Severity = s.SeverityName,
                            //StatusName = st.StatusName

                            AssignedTo = e.Firstname + " " + e.Lastname,
                            Description = a.Descripition,
                            Id = a.Id,
                            ProjectName = pr.Projectsname,
                            TaskName = a.Taskname,
                            TaskGroup = tg.Taskgroupname,
                            Severity = s.Severityname,
                            StatusName = st.Statusname
                        }).ToList();
            return data;
        }


        public IEnumerable<TaskModels> GetListBugsByProjectId(int ProjectId)
        {
            var data = (from a in db.Tasks
                        join e in db.Employees on a.Assignto equals e.Id
                        join tg in db.Taskgroups on a.Taskgroupid equals tg.Id
                        join st in db.Statuses on a.Statusid equals st.Id
                        join pr in db.Projects on a.Projectid equals pr.Id
                        join s in db.Severities on a.Severityid equals s.Id
                        where a.Projectid == ProjectId
                        && a.DelFlag == false
                        && e.DelFlag == false
                        && tg.DelFlag == false
                        && st.DelFlag == false
                        && pr.DelFlag == false
                        && s.DelFlag == false
                         && a.Type == 2
                        select new TaskModels
                        {
                            AssignedTo = e.Firstname + " " + e.Lastname,
                            Description = a.Descripition,
                            Id = a.Id,
                            ProjectName = pr.Projectsname,
                            TaskName = a.Taskname,
                            TaskGroup = tg.Taskgroupname,
                            Severity = s.Severityname,
                            StatusName = st.Statusname
                        }).ToList();
            return data;
        }

        public FormTaskModel GetDataById(int id, int employeeId)
        {
            var taskdata = db.Tasks.Where(x => x.Id == id).FirstOrDefault();
            var getattachlist = db.Attachmentfiles.Where(x => x.Taskid == id && x.Isbugdocument == true).ToList();
            var getActivitylog = db.Taskdayactivities.Where(x => x.Taskid == id && x.DelFlag == false && x.Employeeid == employeeId).ToList();

            if (getActivitylog.Count == 0)
            {
                Taskdayactivity model = new Taskdayactivity();
                getActivitylog.Add(model);
            }
            var data = new FormTaskModel()
            {
                TaskData = taskdata,
                AttachmentList = getattachlist,
                TaskDayActivity = getActivitylog
            };
            return data;
        }

    }
}
