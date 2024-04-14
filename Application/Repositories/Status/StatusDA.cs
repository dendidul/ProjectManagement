using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.Status
{
    public class StatusDA : IStatusDA
    {
        ProjectManagementEntities db = new ProjectManagementEntities();

        public IEnumerable<Core.Dto.PMDb.Status> GetAllData()
        {
            var data = db.Statuses.Where(x => x.DelFlag == false).ToList();
            return data;
        }

        public IEnumerable<Core.Dto.PMDb.Status> GetStatus()
        {
            var data = db.Statuses.Where(x => x.DelFlag == false && x.Id != 5).ToList();
            return data;
        }

        public Core.Dto.PMDb.Status GetDataById(int id)
        {

            var data = db.Statuses.Where(x => x.Id == id).FirstOrDefault();
            return data;
        }

        public void CreateData(Core.Dto.PMDb.Status model)
        {
            model.DelFlag = false;
            db.Statuses.Add(model);
            db.SaveChanges();
        }

        public void Update(Core.Dto.PMDb.Status model)
        {
            model.DelFlag = false;
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Core.Dto.PMDb.Status model)
        {
            Core.Dto.PMDb.Status item = db.Statuses.Find(model.Id);
            item.DelFlag = true;
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
