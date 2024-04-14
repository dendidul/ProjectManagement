using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.ProjectGroup
{
    public class ProjectGroupDA : IProjectGroupDA
    {
        ProjectManagementEntities db = new ProjectManagementEntities();

        public IEnumerable<Core.Dto.PMDb.Projectgroup> GetAllData()
        {
            var data = db.Projectgroups.Where(x => x.DelFlag == false).ToList();
            return data;
        }

        public Core.Dto.PMDb.Projectgroup GetDataById(int id)
        {
            var data = db.Projectgroups.Where(x => x.Id == id).FirstOrDefault();
            return data;
        }

        public void CreateData(Core.Dto.PMDb.Projectgroup model)
        {
            model.DelFlag = false;
            db.Projectgroups.Add(model);
            db.SaveChanges();
        }

        public void Update(Core.Dto.PMDb.Projectgroup model)
        {
            model.DelFlag = false;
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Core.Dto.PMDb.Projectgroup model)
        {
            Core.Dto.PMDb.Projectgroup item = db.Projectgroups.Find(model.Id);
            item.DelFlag = true;
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
