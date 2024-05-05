using Core.Dto.PMDb;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.TaskGroup
{
    public class TaskGroupDA : ITaskGroupDA
    {
        ProjectManagementEntities db = new ProjectManagementEntities();

        public IEnumerable<Core.Dto.PMDb.Taskgroup> GetAllData()
        {
            var data = db.Taskgroups.Where(x => x.DelFlag == false).ToList();
            return data;
        }


        public IEnumerable<Core.Dto.PMDb.Taskgroup> GetActiveTaskGroup(int ProjectId)
        {
            var data = db.Taskgroups.Where(x => x.Isactive == true && x.Projectid == ProjectId && x.DelFlag == false).ToList();
            return data;
        }


        public IEnumerable<TaskGroupModel> GetAllDataTaskGroupByProjectID(int Projectid)
        {
            //string query = @"select 
            //                tg.id as Id,
            //                tg.ProjectId as ProjectId,
            //                p.ProjectsName as ProjectName,
            //                tg.TaskGroupName,
            //                tg.del_flag as del_flag,
            //                tg.IsActive as IsActive

            //                 from TaskGroup tg
            //                inner join Projects p on tg.ProjectId = p.id

            //                where tg.del_flag = 0 and ProjectId = " + Projectid;
            // var data = SQLQueryUtil.ExecuteCustomQuery<TaskGroupModel>(query).ToList();


            var data = db.TaskGroupModels.FromSqlInterpolated<TaskGroupModel>($@"select 
                            tg.id as Id,
                            tg.ProjectId as ProjectId,
                            p.ProjectsName as ProjectName,
                            tg.TaskGroupName,
                            tg.del_flag as del_flag,
                            tg.IsActive as IsActive

                             from TaskGroup tg
                            inner join Projects p on tg.ProjectId = p.id
                           
                            where tg.del_flag is false and ProjectId = {Projectid}").Select(x => x).ToList();


            return data;
        }

        public IEnumerable<TaskGroupModel> GetAllDataByEmployee(int id, int Projectid)
        {
            //string query = "";
            if (id == 100)
            {
                //query = @"select 
                //            tg.id as Id,
                //            tg.ProjectId as ProjectId,
                //            p.ProjectsName as ProjectName,
                //            tg.TaskGroupName,
                //            tg.del_flag as del_flag,
                //            tg.IsActive as IsActive


                //             from TaskGroup tg
                //            inner join Projects p on tg.ProjectId = p.id
                //            inner join RolesProjectEmployee rpe on rpe.ProjectID = p.id
                //            inner join Employee emp on rpe.EmpID = emp.id
                //            where tg.del_flag = 0 and rpe.RoleID  = 2 and p.id = " + Projectid;

                var data = db.TaskGroupModels.FromSqlInterpolated<TaskGroupModel>($@"select 
                            tg.id as Id,
                            tg.ProjectId as ProjectId,
                            p.ProjectsName as ProjectName,
                            tg.TaskGroupName,
                            tg.del_flag as del_flag,
                            tg.IsActive as IsActive


                             from TaskGroup tg
                            inner join Projects p on tg.ProjectId = p.id
                            inner join RolesProjectEmployee rpe on rpe.ProjectID = p.id
                            inner join Employee emp on rpe.EmpID = emp.id
                            where tg.del_flag = false and rpe.RoleID  = 2 and p.id = {Projectid}").Select(x => x).ToList();
                return data;

            }
            else
            {
                //query = @"select 
                //            tg.id as Id,
                //            tg.ProjectId as ProjectId,
                //            p.ProjectsName as ProjectName,
                //            tg.TaskGroupName,
                //            tg.del_flag as del_flag,
                //            tg.IsActive as IsActive

                //             from TaskGroup tg
                //            inner join Projects p on tg.ProjectId = p.id
                //            inner join RolesProjectEmployee rpe on rpe.ProjectID = p.id
                //            inner join Employee emp on rpe.EmpID = emp.id
                //            where tg.del_flag = 0 and emp.id = " + id + " and p.id = " + Projectid;

                var data = db.TaskGroupModels.FromSqlInterpolated<TaskGroupModel>($@"select 
                            tg.id as Id,
                            tg.ProjectId as ProjectId,
                            p.ProjectsName as ProjectName,
                            tg.TaskGroupName,
                            tg.del_flag as del_flag,
                            tg.IsActive as IsActive

                             from TaskGroup tg
                            inner join Projects p on tg.ProjectId = p.id
                            inner join RolesProjectEmployee rpe on rpe.ProjectID = p.id
                            inner join Employee emp on rpe.EmpID = emp.id
                            where tg.del_flag = false and emp.id = {id} and p.id = {Projectid}").Select(x => x).ToList();
                return data;
            }

            // var data = SQLQueryUtil.ExecuteCustomQuery<TaskGroupModel>(query).ToList();


        }

        public Core.Dto.PMDb.Taskgroup GetDataById(int id)
        {
            var data = db.Taskgroups.Where(x => x.Id == id).FirstOrDefault();
            return data;
        }

        public void CreateData(Core.Dto.PMDb.Taskgroup model)
        {
            model.DelFlag = false;
            db.Taskgroups.Add(model);
            db.SaveChanges();
        }

        public void Update(Core.Dto.PMDb.Taskgroup model)
        {
            model.DelFlag = false;
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Core.Dto.PMDb.Taskgroup model)
        {
            Core.Dto.PMDb.Taskgroup item = db.Taskgroups.Find(model.Id);
            item.DelFlag = true;
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }

    }
}
