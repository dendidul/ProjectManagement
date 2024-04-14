using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.Severity
{
    public class SeverityDA : ISeverityDA
    {
        ProjectManagementEntities db = new ProjectManagementEntities();

        public IEnumerable<Core.Dto.PMDb.Severity> GetAllData()
        {
            var data = db.Severities.Where(x => x.DelFlag == false).ToList();
            return data;
        }

        public Core.Dto.PMDb.Severity GetDataById(int id)
        {
            var data = db.Severities.Where(x => x.Id == id).FirstOrDefault();
            return data;
        }

        public void CreateData(Core.Dto.PMDb.Severity model)
        {
            model.DelFlag = false;
            db.Severities.Add(model);
            db.SaveChanges();
        }

        public void Update(Core.Dto.PMDb.Severity model)
        {
            model.DelFlag = false;
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Core.Dto.PMDb.Severity model)
        {
            Core.Dto.PMDb.Severity item = db.Severities.Find(model.Id);
            item.DelFlag = true;
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
