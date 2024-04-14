using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.Department
{
    public class DepartmentDA : IDepartmentDA
    {
        ProjectManagementEntities db = new ProjectManagementEntities();

        public IEnumerable<Core.Dto.PMDb.Department> GetAllData()
        {
            var data = db.Departments.Where(x => x.DelFlag == false).ToList();
            return data;
        }

        public Core.Dto.PMDb.Department GetDataById(int id)
        {
            var data = db.Departments.Where(x => x.Id == id).FirstOrDefault();
            return data;
        }

        public void CreateData(Core.Dto.PMDb.Department model)
        {
            model.DelFlag = false;
            db.Departments.Add(model);
            db.SaveChanges();
        }

        public void Update(Core.Dto.PMDb.Department model)
        {
            model.DelFlag = false;
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Core.Dto.PMDb.Department model)
        {
            Core.Dto.PMDb.Department item = db.Departments.Find(model.Id);
            item.DelFlag = true;
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
