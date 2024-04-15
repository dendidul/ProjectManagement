using Core.Dto.PMDb;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.RolesProjectEmployee
{
    public class RolesProjectEmployeeDA : IRolesProjectEmployeeDA
    {
        ProjectManagementEntities db = new ProjectManagementEntities();

        public IEnumerable<ViewRolesProjectEmployee> GetAllData(int ProjectId)
        {

            var data = db.ViewRolesProjectEmployees.FromSqlInterpolated<ViewRolesProjectEmployee>($@"select 
                        
                        rpe.ProjectID,
                        p.ProjectsName,
                        Emp.ID as EmployeeId, emp.Email,
                         concat((coalesce (emp.FirstName, '')),' ',(coalesce (emp.LastName, ''))) AS EmployeeName ,
                        r.RolesName,
                        case when emp.PhotoUrl is null then '#'
                        else
                        emp.PhotoUrl
                        end as PhotoUrl
                        ,d.DepartmentName as DepartmentName,
                        pos.PositionName,r.id as RolesId,rpe.id as id
                        from RolesProjectEmployee rpe
                        inner join Employee emp on rpe.EmpID = emp.id
                        inner join Projects p on rpe.ProjectID = p.id
                        inner join Roles r on r.id = rpe.RoleID
                        inner join Department d on d.id = emp.DepartmentId
                        inner join Position pos on pos.id = emp.PositionId
                        where 
                        
                        p.del_flag is not true
                        and rpe.del_flag is not true
                        and emp.del_flag is not true 
                            
                        ").Select(x => x).ToList();
            if (ProjectId != 100)
            {
                data = data.Where(x => x.ProjectID == ProjectId).ToList();

            }



            return data;
        }


        public IEnumerable<UserProjectModel> GetEmployeeByProject(int ProjectId)
        {

            var data = db.UserProjectModels.FromSqlInterpolated<UserProjectModel>($@"select 
                        rpe.ProjectID,
                        p.ProjectsName as ProjectName,
                        Emp.ID as EmployeeId, emp.Email,
                        concat((coalesce (emp.FirstName, '')),' ',(coalesce (emp.LastName, ''))) AS EmployeeName ,
                        r.RolesName,
                        case when emp.PhotoUrl is null then '#'
                        else
                        emp.PhotoUrl
                        end as PhotoUrl
                        ,d.DepartmentName as DepartmentName,
                        pos.PositionName,rpe.id as id
                        from RolesProjectEmployee rpe
                        inner join Employee emp on rpe.EmpID = emp.id
                        inner join Projects p on rpe.ProjectID = p.id
                        inner join Roles r on r.id = rpe.RoleID
                        inner join Department d on d.id = emp.DepartmentId
                        inner join Position pos on pos.id = emp.PositionId
                        where rpe.ProjectID = {ProjectId} 
                        and p.del_flag is not true
                        and rpe.del_flag is not true
                        and emp.del_flag is not true 
                            
                        ").Select(x => x).ToList();
            return data;
        }

        public Rolesprojectemployee GetDataById(int id)
        {
            var data = db.Rolesprojectemployees.Where(x => x.Id == id).FirstOrDefault();
            return data;
        }

        public void CreateData(Rolesprojectemployee model)
        {
            model.DelFlag = false;
            db.Rolesprojectemployees.Add(model);
            db.SaveChanges();
        }


        public Rolesprojectemployee GetRolesByProjectAndEmployeeID(int projectId, int employeeid)
        {
            var data = db.Rolesprojectemployees.Where(x => x.Empid == employeeid && x.Projectid == projectId).FirstOrDefault();
            return data;
        }

        public void Update(Rolesprojectemployee model)
        {
            model.DelFlag = false;
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Rolesprojectemployee model)
        {
            Rolesprojectemployee item = db.Rolesprojectemployees.Find(model.Id);
            item.DelFlag = true;
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }

    }
}
