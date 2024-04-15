using Application.Repositories.Employee;
using Application.Repositories.Project;
using Application.Repositories.RolesProjectEmployee;
using Application.Repositories.Types;
using Core.Dto.PMDb;
using Infrastructure.Context;
using Infrastructure.Helper.Config;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.NewsFeed
{
    public class NewsFeedDA : INewsFeedDA
    {
        //ini harus dirubah

        ProjectManagementEntities db = new ProjectManagementEntities();

        private IDataAccessClientRepository _dataAccessClientRepository;
        private IConfigCreatorHelper _configCreatorHelper;
        private string Connection = "";
        private string ServerType = "";

        private readonly IRolesProjectEmployeeDA _rolesProjectEmployeeDA;
        private readonly IProjectDA _projectDA;
        private readonly ITypesDA _typesDA;
        private readonly IEmployeeDA _employeeDA;

        public NewsFeedDA(IRolesProjectEmployeeDA rolesProjectEmployeeDA, IProjectDA projectDA, ITypesDA typesDA, IEmployeeDA employeeDA, IDataAccessClientRepository dataAccessClientRepository, IConfigCreatorHelper configCreatorHelper)
        {
            _rolesProjectEmployeeDA = rolesProjectEmployeeDA;
            _projectDA = projectDA;
            _typesDA = typesDA;
            _employeeDA = employeeDA;
            _dataAccessClientRepository = dataAccessClientRepository;
            _configCreatorHelper = configCreatorHelper;
            Connection = configCreatorHelper.Get("ConnectionStrings:ProjectManagement");
            ServerType = "PostgreeSQL";
        }


        public IEnumerable<Newsfeed> GetAllData()
        {
            var data = db.Newsfeeds.Where(x => x.DelFlag == false).ToList();
            return data;
        }

        public IEnumerable<NewsFeedModels> GetNewsFeedByProjectId(int ProjectId)
        {
            var query = @"select 
                        CONCAT(emp.FirstName,' ',emp.LastName) as EmployeeName,
                        ta.TaskName as TaskName,
                        nf.Description as StatusName,
                        ta.id as TaskId,
                        emp.PhotoUrl as PhotoUrl,
                        emp.id as EmployeeId,
                        ta.StatusId as StatusId,
                        ta.ProjectId,
                        p.ProjectsName as ProjectName
                        , ty.id as TypeId,
                        ty.TypeName as TypeName 
                        from 
                        NewsFeed nf
                         inner join Task ta on ta.id = nf.TaskId
                         inner join Employee emp on emp.id = nf.EmployeeId
                        inner join Status st on ta.StatusId = st.id
                        inner join Projects p on ta.ProjectId = p.id
                        inner join Type ty on ty.id = ta.Type
                        where ta.ProjectId = " + ProjectId + " order by nf.CreatedDate desc limit 10";
            //  var data = SQLQueryUtil.ExecuteCustomQuery<NewsFeedModels>(query).ToList();

            var data = _dataAccessClientRepository.GetList<NewsFeedModels>(query, null, CommandType.Text, Connection, ServerType);
            return data;

        }

        public IEnumerable<NewsFeedModels> GetAllActivityNewsFeedByProjectId(int ProjectId)
        {
            var query = @"select 
                        CONCAT(emp.FirstName,' ',emp.LastName) as EmployeeName,
                        ta.TaskName as TaskName,
                        nf.Description as StatusName,
                        ta.id as TaskId,
                        emp.PhotoUrl as PhotoUrl,
                        emp.id as EmployeeId,
                        ta.StatusId as StatusId,
                        ta.ProjectId,
                        p.ProjectsName as ProjectName
                        , ty.id as TypeId,
                        ty.TypeName as TypeName 
                        from 
                        NewsFeed nf
                         inner join Task ta on ta.id = nf.TaskId
                         inner join Employee emp on emp.id = nf.EmployeeId
                        inner join Status st on ta.StatusId = st.id
                        inner join Projects p on ta.ProjectId = p.id
                        inner join Type ty on ty.id = ta.Type
                        where ta.ProjectId = " + ProjectId + " order by nf.CreatedDate desc";
            //var data = SQLQueryUtil.ExecuteCustomQuery<NewsFeedModels>(query).ToList();
            //return data;
            var data = _dataAccessClientRepository.GetList<NewsFeedModels>(query, null, CommandType.Text, Connection, ServerType);
            return data;

        }


        public IEnumerable<NewsFeedModels> GetNewsFeedByEmployeeId(int EmployeeId)
        {
            var Selectquery = @"select  
                        CONCAT(emp.FirstName,' ',emp.LastName) as EmployeeName,
                        ta.TaskName as TaskName,
                        nf.Description as StatusName,
                        ta.id as TaskId,
                        emp.id as EmployeeId,
                        ta.StatusId as StatusId,
                        emp.PhotoUrl as PhotoUrl,
                        ta.ProjectId,
                        p.ProjectsName as ProjectName 
                        , ty.id as TypeId,
                        ty.TypeName as TypeName 
                        from 
                        NewsFeed nf
                        inner join Task ta on ta.id = nf.TaskId
                         inner join Employee emp on emp.id = nf.EmployeeId
                        inner join Status st on ta.StatusId = st.id
                        inner join Projects p on ta.ProjectId = p.id
                        inner join Type ty on ty.id = ta.Type  ";
            //where ta.ProjectId = " + ProjectId;

            var getProject = _projectDA.GetAllProjectsByEmployeeId(EmployeeId);

            var ConditionQuery = "";

            if (getProject.ToList().Count > 0)
            {
                ConditionQuery += " where ";
                foreach (var i in getProject)
                {
                    if (getProject.LastOrDefault().ProjectId == i.ProjectId)
                    {
                        ConditionQuery += "ta.Projectid = " + i.ProjectId;
                    }
                    else
                    {
                        ConditionQuery += "ta.Projectid = " + i.ProjectId + " or ";
                    }

                }
            }

            var orderby = " order by nf.CreatedDate desc limit 10";

            var query = Selectquery + ConditionQuery + orderby;

            //var data = SQLQueryUtil.ExecuteCustomQuery<NewsFeedModels>(query).ToList();

            var data = _dataAccessClientRepository.GetList<NewsFeedModels>(query, null, CommandType.Text, Connection, ServerType);
            return data;


        }



        public IEnumerable<NewsFeedModels> GetAllActivityNewsFeedByEmployeeId(int EmployeeId)
        {
            var Selectquery = @"select 
                        CONCAT(emp.FirstName,' ',emp.LastName) as EmployeeName,
                        ta.TaskName as TaskName,
                        nf.Description as StatusName,
                        ta.id as TaskId,
                        emp.id as EmployeeId,
                        ta.StatusId as StatusId,
                        emp.PhotoUrl as PhotoUrl,
                        ta.ProjectId,
                        p.ProjectsName as ProjectName
                        , ty.id as TypeId,
                        ty.TypeName as TypeName 
                        from 
                        NewsFeed nf
                        inner join Task ta on ta.id = nf.TaskId
                         inner join Employee emp on emp.id = nf.EmployeeId
                        inner join Status st on ta.StatusId = st.id
                        inner join Projects p on ta.ProjectId = p.id
                        inner join Type ty on ty.id = ta.Type ";
            //where ta.ProjectId = " + ProjectId;

            var getProject = _projectDA.GetAllProjectsByEmployeeId(EmployeeId);

            var ConditionQuery = "";

            if (getProject.ToList().Count > 0)
            {
                ConditionQuery += " where ";
                foreach (var i in getProject)
                {
                    if (getProject.LastOrDefault().ProjectId == i.ProjectId)
                    {
                        ConditionQuery += "ta.Projectid = " + i.ProjectId;
                    }
                    else
                    {
                        ConditionQuery += "ta.Projectid = " + i.ProjectId + " or ";
                    }

                }
            }

            var orderby = " order by nf.CreatedDate desc ";

            var query = Selectquery + ConditionQuery + orderby;

            var data = _dataAccessClientRepository.GetList<NewsFeedModels>(query, null, CommandType.Text, Connection, ServerType);
            return data;

        }

        public IEnumerable<NewsFeedModels> GetRecentActivityByEmployeeId(int EmployeeId)
        {
            var Selectquery = @"select 
                        CONCAT(emp.FirstName,' ',emp.LastName) as EmployeeName,
                        ta.TaskName as TaskName,
                        nf.Description as StatusName,
                        ta.id as TaskId,
                        emp.id as EmployeeId,
                        ta.StatusId as StatusId,
                        emp.PhotoUrl as PhotoUrl,
                        ta.ProjectId,
                        p.ProjectsName as ProjectName
                        , ty.id as TypeId,
                        ty.TypeName as TypeName 
                        from 
                        NewsFeed nf
                        inner join Task ta on ta.id = nf.TaskId
                        inner join Employee emp on emp.id = nf.EmployeeId
                        inner join Status st on ta.StatusId = st.id
                        inner join Projects p on ta.ProjectId = p.id
                        inner join Type ty on ty.id = ta.Type 
                        where nf.EmployeeId = " + EmployeeId;

            var orderby = " order by nf.CreatedDate desc ";
            var query = Selectquery + orderby;

            var data = _dataAccessClientRepository.GetList<NewsFeedModels>(query, null, CommandType.Text, Connection, ServerType);
            return data;

        }


        public IEnumerable<EventViewModel> GetNewsFeedByEmployeeIdForCalendar(int EmployeeId)
        {
            var Selectquery = @"select 
                        ta.id as id,
                         CONCAT(emp.FirstName,' ',emp.LastName) as EmployeeName,
                        ta.TaskName as title,
                        st.StatusName as StatusName,
                        ta.id as TaskId,
                        emp.id as EmployeeId,
						ta.StartDate as start,
						ta.DueDate as end,
                       -- ta.StatusId as StatusId,
                        --emp.PhotoUrl as PhotoUrl,
                        --ta.ProjectId,
                        p.ProjectsName as ProjectName
                        from 
                        NewsFeed nf
                        right join Task ta on ta.id = nf.TaskId
                        inner join Employee emp on emp.id = ta.AssignTo 
                        --or emp.id = ta.CreatedBy
                        inner join Status st on ta.StatusId = st.id
                        inner join Projects p on ta.ProjectId = p.id
                        inner join Type ty on ty.id = ta.Type ";
            //where ta.ProjectId = " + ProjectId;

            var getProject = _projectDA.GetAllProjectsByEmployeeId(EmployeeId);

            var ConditionQuery = "";

            if (getProject.ToList().Count > 0)
            {
                ConditionQuery += " where ";
                foreach (var i in getProject)
                {
                    if (getProject.LastOrDefault().ProjectId == i.ProjectId)
                    {
                        ConditionQuery += "ta.Projectid = " + i.ProjectId;
                    }
                    else
                    {
                        ConditionQuery += "ta.Projectid = " + i.ProjectId + " or ";
                    }

                }
            }
            var groupby = " group by   ta.id, emp.FirstName,emp.LastName,ta.TaskName,st.StatusName,ta.id ,emp.id ,ta.StartDate	,ta.DueDate	, p.ProjectsName ";
            var query = Selectquery + ConditionQuery + groupby;

            var data = _dataAccessClientRepository.GetList<EventViewModel>(query, null, CommandType.Text, Connection, ServerType);
            return data;

        }





        public Newsfeed GetDataByEmployeeId(int id)
        {
            var data = db.Newsfeeds.Where(x => x.Id == id).FirstOrDefault();
            return data;
        }

        public Newsfeed GetDataByProjectId(int id)
        {
            var data = db.Newsfeeds.Where(x => x.Id == id).FirstOrDefault();
            return data;
        }



        public Newsfeed GetDataById(int id)
        {
            var data = db.Newsfeeds.Where(x => x.Id == id).FirstOrDefault();
            return data;
        }


        public void AddNewsFeed(int EmployeeId, EnumAction action, int taskid, int typeid)
        {
            var employeename = _employeeDA.GetEmployeeFullName(EmployeeId);
            var typename = _typesDA.GetDataById(typeid).Typename;

            Newsfeed form = new Newsfeed();

            switch (action)
            {
                case EnumAction.CreatedNew:


                    form.Createddate = DateTime.Now;
                    form.DelFlag = false;
                    form.Employeeid = EmployeeId;
                    form.Description = employeename + " has Created New " + typename;
                    form.Taskid = taskid;
                    CreateData(form);

                    break;
                case EnumAction.Edited:


                    form.Createddate = DateTime.Now;
                    form.DelFlag = false;
                    form.Employeeid = EmployeeId;
                    form.Description = employeename + " has " + EnumAction.Edited.ToString() + " " + typename;
                    form.Taskid = taskid;
                    CreateData(form);

                    break;

                case EnumAction.Deleted:
                    form.Createddate = DateTime.Now;
                    form.DelFlag = false;
                    form.Employeeid = EmployeeId;
                    form.Description = employeename + " has " + EnumAction.Deleted.ToString() + " " + typename;
                    form.Taskid = taskid;
                    CreateData(form);
                    break;

                case EnumAction.Completed:
                    form.Createddate = DateTime.Now;
                    form.DelFlag = false;
                    form.Employeeid = EmployeeId;
                    form.Description = employeename + " has " + EnumAction.Completed.ToString() + " " + typename;
                    form.Taskid = taskid;
                    CreateData(form);
                    break;

                case EnumAction.InProgress:
                    form.Createddate = DateTime.Now;
                    form.DelFlag = false;
                    form.Employeeid = EmployeeId;
                    form.Description = employeename + " has Marked In Progress " + typename;
                    form.Taskid = taskid;
                    CreateData(form);
                    break;

                case EnumAction.InReview:
                    form.Createddate = DateTime.Now;
                    form.DelFlag = false;
                    form.Employeeid = EmployeeId;
                    form.Description = employeename + " has Marked In Review " + typename;
                    form.Taskid = taskid;
                    CreateData(form);
                    break;

                case EnumAction.NeedRepair:
                    form.Createddate = DateTime.Now;
                    form.DelFlag = false;
                    form.Employeeid = EmployeeId;
                    form.Description = employeename + " has Marked Need Repair " + typename;
                    form.Taskid = taskid;
                    CreateData(form);
                    break;


            }
        }

        public void CreateData(Core.Dto.PMDb.Newsfeed model)
        {
            model.DelFlag = false;
            db.Newsfeeds.Add(model);
            db.SaveChanges();
        }

        public void Update(Core.Dto.PMDb.Newsfeed model)
        {
            model.DelFlag = false;
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Core.Dto.PMDb.Newsfeed model)
        {
            Core.Dto.PMDb.Newsfeed item = db.Newsfeeds.Find(model.Id);
            item.DelFlag = true;
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }





    }
}
