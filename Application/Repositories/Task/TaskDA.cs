using Application.Repositories.TaskLog;
using Core.Dto.PMDb;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.Task
{
    public class TaskDA : ITaskDA
    {
        ProjectManagementEntities db = new ProjectManagementEntities();

        private readonly ITaskLogDA _taskLogDA;

        public TaskDA(ITaskLogDA taskLogDA)
        {
            _taskLogDA = taskLogDA;
        }



        public IEnumerable<Core.Dto.PMDb.Task> GetAllData()
        {
            var data = db.Tasks.Where(x => x.DelFlag == false).ToList();
            return data;
        }


        //public IEnumerable<Task> GetTaskByProjectId(int ProjectId)
        //{
        //    var data = db.Tasks.Where(x => x.ProjectId == ProjectId).ToList();
        //    return data;
        //}


        public FormTaskModel GetDataByIdForActivityMonitoring(int id)
        {
            var taskdata = db.Tasks.Where(x => x.Id == id).FirstOrDefault();
            var getattachlist = db.Attachmentfiles.Where(x => x.Taskid == id && x.Istaskdocument == true).ToList();
            var getActivitylog = db.Taskdayactivities.Where(x => x.Taskid == id && x.DelFlag == false).ToList();

            string query = @"SELECT tda.[id] as id
                                                                          ,TaskId] as TaskId
                                                                          ,EmployeeId as EmployeeId
	                                                                      ,CONCAT(emp.FirstName,' ',emp.LastName) as EmployeeName
                                                                          ,Type as Type
                                                                          ,StartDate  as StartDate
                                                                          ,Estimate as Estimate
                                                                          ,Description as Description
                                                                          
                                                                      FROM TaskDayActivity tda
                                                                      inner join Employee emp on tda.EmployeeId = emp.id
                                                                      where TaskId = " + id + " and tda.del_flag is not true ";

            var getActivitiesLog = db.TaskDayActivitieses.FromSqlInterpolated<TaskDayActivities>($@"SELECT tda.id as id
                                                                          ,TaskId as TaskId
                                                                          ,EmployeeId as EmployeeId
                                                                          , CONCAT(emp.FirstName, ' ', emp.LastName) as EmployeeName
                                                                          ,Type as Type
                                                                          ,StartDate as StartDate
                                                                          ,Estimate as Estimate
                                                                          ,Description as Description


                                                                      FROM TaskDayActivity tda
                                                                      inner join Employee emp on tda.EmployeeId = emp.id
                                                                      where TaskId = {id} and tda.del_flag is not true ").ToList();

            if (getActivitylog.Count == 0)
            {
                Taskdayactivity model = new Taskdayactivity();
                getActivitylog.Add(model);
            }
            if (getActivitiesLog.Count == 0)
            {
                TaskDayActivities model = new TaskDayActivities();
                getActivitiesLog.Add(model);
            }

            var data = new FormTaskModel()
            {
                TaskData = taskdata,
                AttachmentList = getattachlist,
                TaskDayActivity = getActivitylog,
                TaskDayActivties = getActivitiesLog
            };
            return data;
        }

        public FormTaskModel GetDataById(int id, int employeeId)
        {
            var taskdata = db.Tasks.Where(x => x.Id == id).FirstOrDefault();
            var getattachlist = db.Attachmentfiles.Where(x => x.Taskid == id && x.Istaskdocument == true).ToList();
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

        public Core.Dto.PMDb.Task GetData(int id)
        {
            var Taskdata = db.Tasks.Where(x => x.Id == id).FirstOrDefault();
            return Taskdata;
        }

        public void CreateData(Core.Dto.PMDb.Task model)
        {

            model.DelFlag = false;
            db.Tasks.Add(model);
            db.SaveChanges();


            Tasklog formlog = new Tasklog();

            //var getdata = GetData(model.id);
            formlog.Taskid = model.Id;
            formlog.Taskgroupid = model.Taskgroupid;
            formlog.Taskname = model.Taskname;
            formlog.Projectid = model.Projectid;
            formlog.Startdate = model.Startdate;
            formlog.Duedate = model.Duedate;
            formlog.Severityid = model.Severityid;
            formlog.Assignto = model.Assignto;
            formlog.Categoryid = model.Categoryid;
            formlog.Descripition = model.Descripition;
            formlog.Createdby = model.Createdby;
            formlog.Createddate = DateTime.Now;
            formlog.Statusid = model.Statusid;
            formlog.Type = model.Type;
            formlog.Reviewby = model.Reviewby;
            formlog.Result = model.Result;

            // TaskLogLogic.CreateData(formlog);
            _taskLogDA.CreateData(formlog);

        }

        public void Update(Core.Dto.PMDb.Task model)
        {
            Tasklog formlog = new Tasklog();

            //var getdata = GetData(model.id);
            formlog.Taskid = model.Id;
            formlog.Taskgroupid = model.Taskgroupid;
            formlog.Taskname = model.Taskname;
            formlog.Projectid = model.Projectid;
            formlog.Startdate = model.Startdate;
            formlog.Duedate = model.Duedate;
            formlog.Severityid = model.Severityid;
            formlog.Assignto = model.Assignto;
            formlog.Categoryid = model.Categoryid;
            formlog.Descripition = model.Descripition;
            formlog.Createdby = model.Createdby;
            formlog.Createddate = DateTime.Now;
            formlog.Statusid = model.Statusid;
            formlog.Type = model.Type;
            formlog.Reviewby = model.Reviewby;
            formlog.Result = model.Result;

            // TaskLogLogic.CreateData(formlog);
            _taskLogDA.CreateData(formlog);



            model.DelFlag = false;
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Core.Dto.PMDb.Task model)
        {
            Core.Dto.PMDb.Task item = db.Tasks.Find(model.Id);
            item.DelFlag = true;
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteRow(Core.Dto.PMDb.Task model)
        {
            Core.Dto.PMDb.Task item = db.Tasks.Find(model.Id);
            db.Tasks.Remove(item);
            db.SaveChanges();
        }


        public IEnumerable<TaskModels> GetListTaskForAdmin()
        {
            var data = (from a in db.Tasks
                        join e in db.Employees on a.Assignto equals e.Id
                        join tg in db.Taskgroups on a.Taskgroupid equals tg.Id
                        join st in db.Statuses on a.Statusid equals st.Id
                        join pr in db.Projects on a.Projectid equals pr.Id
                        join s in db.Severities on a.Severityid equals s.Id
                        where

                        a.DelFlag == false
                        && e.DelFlag == false
                        && tg.DelFlag == false
                        && st.DelFlag == false
                        && pr.DelFlag == false
                        && s.DelFlag == false
                        && a.Type == 1
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


        public IEnumerable<TaskModels> GetListTaskByAssignedEmployeeAndProjects(int EmployeeId, int ProjectId)
        {
            //var data =  (from a in db.Tasks
            //            join e in db.Employees on a.AssignTo equals e.id 
            //            //join f in db.Employees on a.CreatedBy equals f.id
            //            join tg in db.TaskGroups on a.TaskGroupId equals tg.id
            //            join st in db.Status on a.StatusId equals st.id
            //            join pr in db.Projects on a.ProjectId equals pr.id
            //            join s in db.Severities on a.SeverityId equals s.id
            //            where (a.AssignTo == EmployeeId || a.CreatedBy == EmployeeId) && a.ProjectId == ProjectId
            //            && a.del_flag == false
            //            && e.del_flag == false
            //            && tg.del_flag == false
            //            && st.del_flag == false
            //            && pr.del_flag == false
            //            && s.del_flag == false
            //            && a.Type == 1
            //            select new TaskModels
            //            {
            //                AssignedTo = e.FirstName + " " + e.LastName,
            //                Description = a.Descripition,
            //                Id = a.id,
            //                ProjectName = pr.ProjectsName,
            //                TaskName = a.TaskName,
            //                TaskGroup = tg.TaskGroupName,
            //                Severity = s.SeverityName,
            //                StatusName = st.StatusName
            //            }).ToList();

            //var query = @"select
            //                CONCAT(emp.FirstName, ' ', emp.LastName) as AssignedTo,
            //                ta.Descripition as Description,
            //                ta.id as Id,
            //                pr.ProjectsName as ProjectName,
            //                ta.TaskName as TaskName,
            //                tg.TaskGroupName as TaskGroup,
            //                s.SeverityName as Severity,
            //                st.StatusName as StatusName 

            //                from Task ta
            //                left join Employee emp on ta.AssignTo = emp.id 
            //                inner join TaskGroup tg on ta.TaskGroupId = tg.id
            //                left join Status st on ta.StatusId = st.id
            //                inner join Projects pr on pr.id = ta.ProjectId
            //                left join Severity s on s.id = ta.SeverityId
            //                where (ta.AssignTo = " + EmployeeId + "  or ta.AssignTo is null) and tg.IsActive = 1  and ta.ProjectId = " + ProjectId + " and ta.Type = 1 and ta.del_flag = 0";
            // var data = SQLQueryUtil.ExecuteCustomQuery<TaskModels>(query).ToList();


            var lala = $@"select
                            CONCAT(emp.FirstName, ' ', emp.LastName) as AssignedTo,
                            ta.Descripition as Description,
                            ta.id as Id,
                            pr.ProjectsName as ProjectName,
                            ta.TaskName as TaskName,
                            tg.TaskGroupName as TaskGroup,
                            s.SeverityName as Severity,
                            st.StatusName as StatusName,
                            pr.Id as ProjectId,
                            ta.reviewby as ReviewBy,
                            ta.taskgroupid as TaskGroupId,
                            ta.type as Type,
                            '' as TypeName

                    
                            from Task ta
                            left join Employee emp on ta.AssignTo = emp.id 
                            inner join TaskGroup tg on ta.TaskGroupId = tg.id
                            left join Status st on ta.StatusId = st.id
                            inner join Projects pr on pr.id = ta.ProjectId
                            left join Severity s on s.id = ta.SeverityId
                            where (ta.AssignTo = {EmployeeId}  or ta.AssignTo is null) and tg.IsActive is true  and ta.ProjectId = {ProjectId} and ta.Type = 1 and ta.del_flag is false";

            var data = db.TaskModels.FromSqlInterpolated<TaskModels>($@"select
                            CONCAT(emp.FirstName, ' ', emp.LastName) as AssignedTo,
                            ta.Descripition as Description,
                            ta.id as Id,
                            pr.ProjectsName as ProjectName,
                            ta.TaskName as TaskName,
                            tg.TaskGroupName as TaskGroup,
                            s.SeverityName as Severity,
                            st.StatusName as StatusName,
                            pr.Id as ProjectId,
                            '' as ReviewBy,
                            ta.taskgroupid as TaskGroupId,
                            ta.type as Type,
                            '' as TypeName

                    
                            from Task ta
                            left join Employee emp on ta.AssignTo = emp.id 
                            inner join TaskGroup tg on ta.TaskGroupId = tg.id
                            left join Status st on ta.StatusId = st.id
                            inner join Projects pr on pr.id = ta.ProjectId
                            left join Severity s on s.id = ta.SeverityId
                            where (ta.AssignTo = {EmployeeId}  or ta.AssignTo is null) and tg.IsActive is true  and ta.ProjectId = {ProjectId} and ta.Type = 1 and ta.del_flag is false").Select(x => x).ToList();


            return data;

        }

        public IEnumerable<TaskModels> GetListTaskByAssignedEmployee(int EmployeeId)
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
                         && a.Type == 1
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

        public IEnumerable<TaskModels> GetListTaskByEmployeeCreated(int EmployeeId)
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
                         && a.Type == 1
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


        public IEnumerable<TaskModels> GetListBackLogByProjectId(int ProjectId)
        {
            //string query = @"select 
            //                CONCAT(e.FirstName, ' ',e.LastName) as AssignedTo,
            //                a.Descripition as Description,
            //                a.id as Id,
            //                p.ProjectsName as ProjectName,
            //                p.id as ProjectId,
            //                a.TaskName as TaskName,
            //                tg.id as TaskGroupId,
            //                a.Type as Type,
            //                tg.TaskGroupName as TaskGroup,
            //                st.StatusName as StatusName,
            //                s.SeverityName as Severity,
            //                ty.TypeName as TypeName


            //                 from
            //                Task a
            //                left join Employee e on a.AssignTo = e.id
            //                left join TaskGroup tg on a.TaskGroupId = tg.id
            //                left join [Status] st on a.StatusId = st.id
            //                left join Projects p on a.ProjectId = p.id 
            //                left join Severity s on a.SeverityId = s.id
            //                left join [Type] ty on a.Type = ty.id
            //                where a.del_flag = 0 and a.ProjectId = 
            //                " + ProjectId;

            var data = db.TaskModels.FromSqlInterpolated<TaskModels>($@"
                        select 
                            CONCAT(e.FirstName, ' ',e.LastName) as AssignedTo,
                            a.Descripition as Description,
                            a.id as Id,
                            p.ProjectsName as ProjectName,
                            p.id as ProjectId,
                            a.TaskName as TaskName,
                            tg.id as TaskGroupId,
                            a.Type as Type,
                            tg.TaskGroupName as TaskGroup,
                            st.StatusName as StatusName,
                            s.SeverityName as Severity,
                            ty.TypeName as TypeName,
                            a.ReviewBy


                             from
                            Task a
                            left join Employee e on a.AssignTo = e.id
                            left join TaskGroup tg on a.TaskGroupId = tg.id
                            left join Status st on a.StatusId = st.id
                            left join Projects p on a.ProjectId = p.id 
                            left join Severity s on a.SeverityId = s.id
                            left join Type ty on a.Type = ty.id
                            where a.del_flag = false and a.ProjectId = {ProjectId} ;
            ").Select(x => x).ToList();



            return data;
        }

        public IEnumerable<TaskModels> GetListTaskByProjectId(int ProjectId)
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
                         && a.Type == 1
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

        public BackLogModel GetSprintBackLogDataByTaskGroupID(int id)
        {
            var dataTaskGroup = db.Taskgroups.Where(x => x.Id == id).FirstOrDefault();

            var GetDataList = GetListBackLogByProjectId(Convert.ToInt32(dataTaskGroup.Projectid));
            var dataProductBacklog = GetDataList.Where(x => (x.TaskGroupId == dataTaskGroup.Id || x.Type == 3) && x.ProjectId == dataTaskGroup.Projectid).ToList();

            var model = new BackLogModel()
            {
                TaskGroupModel = dataTaskGroup,
                ListTask = dataProductBacklog
            };
            return model;

        }

        public IEnumerable<Core.Dto.PMDb.Task> GetTaskByTaskGroupIdAndProjectId(int id, int ProjectId)
        {
            var data = db.Tasks.Where(x => x.Taskgroupid == id && x.Projectid == ProjectId).ToList();
            return data;
        }


        public IEnumerable<TaskModels> GetListActivityReviewByReviewEmployeeAndProjects(int EmployeeId, int ProjectId)
        {
            //     string query = @"select
            //                     CONCAT(emp.FirstName, ' ', emp.LastName) as ReviewBy,
            //                     ta.Descripition as Description,
            //                     ta.id as Id,
            //                     pr.ProjectsName as ProjectName,
            //                     ta.TaskName as TaskName,
            //                     tg.TaskGroupName as TaskGroup,
            //                     s.SeverityName as Severity,
            //                     st.StatusName as StatusName,
            //case when ta.Type = 1 then 'Task'
            //when ta.Type = 2 then 'Bugs'
            //else ''
            //end as TypeName

            //                      from Task ta
            //                     left join Employee emp on ta.ReviewBy = emp.id
            //                     inner join TaskGroup tg on ta.TaskGroupId = tg.id
            //                     inner join Status st on ta.StatusId = st.id
            //                     inner join Projects pr on pr.id = ta.ProjectId
            //                     inner join Severity s on s.id = ta.SeverityId
            //                     where (ta.ReviewBy = " + EmployeeId + @" or ta.ReviewBy is null)  
            //and ta.ProjectId = " + ProjectId + @" and (ta.StatusId = 5 or ta.StatusId = 7 or ta.StatusId = 9) 
            // and ta.del_flag = 0";
            //     var data = SQLQueryUtil.ExecuteCustomQuery<TaskModels>(query).ToList();

            var data = db.TaskModels.FromSqlInterpolated<TaskModels>($@"select
                            CONCAT(emp.FirstName, ' ', emp.LastName) as ReviewBy,
                            ta.Descripition as Description,
                            ta.id as Id,
                            pr.ProjectsName as ProjectName,
                            ta.TaskName as TaskName,
                            tg.TaskGroupName as TaskGroup,
                            s.SeverityName as Severity,
                            st.StatusName as StatusName,
							case when ta.Type = 1 then 'Task'
							when ta.Type = 2 then 'Bugs'
							else ''
							end as TypeName

                             from Task ta
                            left join Employee emp on ta.ReviewBy = emp.id
                            inner join TaskGroup tg on ta.TaskGroupId = tg.id
                            inner join Status st on ta.StatusId = st.id
                            inner join Projects pr on pr.id = ta.ProjectId
                            inner join Severity s on s.id = ta.SeverityId
                            where (ta.ReviewBy ={EmployeeId} or ta.ReviewBy is null)  
							and ta.ProjectId = {ProjectId} and (ta.StatusId = 5 or ta.StatusId = 7 or ta.StatusId = 9) 
							 and ta.del_flag is false").ToList();

            return data;
        }



    }
}
