using Core.Dto.PMDb;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.Employee
{
    public class EmployeeDA : IEmployeeDA
    {
        ProjectManagementEntities db = new ProjectManagementEntities();

        public IEnumerable<ViewAllEmployee> GetAllData()
        {
            // var data = db.ViewAllEmployees.ToList();
            var data = db.ViewAllEmployees.FromSqlInterpolated<ViewAllEmployee>($@" 
                             SELECT id,
                               employeename as EmployeeName, 
                               departmentname as DepartmentName, 
                               positionname as PositionName, 
                               is_active as Is_Active, 
                               coalesce(photourl,'') as PhotoUrl,
                               email as Email

                                FROM viewallemployee
                            
                        ").Select(x => x).ToList();
           

            return data;
        }


        public string GetEmployeeFullName(int id)
        {

            string EmployeeName = "";
            var data = db.ViewAllEmployees.FromSqlInterpolated<ViewAllEmployee>($@" 
                            select
                            CONCAT(FirstName, ' ', LastName) as EmployeeName
                            from
                            Employee
                            where id = {id}
                            
                        ").Select(x => x).FirstOrDefault();

            if (data != null)
            {
                EmployeeName = data.EmployeeName;
            }

            return EmployeeName;
        }
        public Core.Dto.PMDb.Employee GetDataById(int id)
        {
            var data = db.Employees.Where(x => x.Id == id).FirstOrDefault();
            return data;
        }

        public void CreateData(Core.Dto.PMDb.Employee model)
        {
            model.DelFlag = false;
            db.Employees.Add(model);
            db.SaveChanges();
        }

        public void Update(Core.Dto.PMDb.Employee model)
        {
            model.DelFlag = false;
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Core.Dto.PMDb.Employee model)
        {
            Core.Dto.PMDb.Employee item = db.Employees.Find(model.Id);
            item.DelFlag = true;
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
