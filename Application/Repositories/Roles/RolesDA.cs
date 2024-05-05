using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.Roles
{
    public class RolesDA : IRolesDA
    {
        ProjectManagementEntities db = new ProjectManagementEntities();

        public IEnumerable<Core.Dto.PMDb.Role> GetAllData()
        {
            var data = db.Roles.Where(x => x.DelFlag == false).ToList();
            return data;
        }

        public Core.Dto.PMDb.Role GetDataById(int id)
        {
            var data = db.Roles.Where(x => x.Id == id).FirstOrDefault();
            return data;
        }

        public void CreateData(Core.Dto.PMDb.Role model)
        {
            model.DelFlag = false;
            db.Roles.Add(model);
            db.SaveChanges();
        }

        public void Update(Core.Dto.PMDb.Role model)
        {
            model.DelFlag = false;
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Core.Dto.PMDb.Role model)
        {
            Core.Dto.PMDb.Role item = db.Roles.Find(model.Id);
            item.DelFlag = true;
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
