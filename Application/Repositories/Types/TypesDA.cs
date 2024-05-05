using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.Types
{
    public class TypesDA : ITypesDA
    {
        ProjectManagementEntities db = new ProjectManagementEntities();

        public IEnumerable<Core.Dto.PMDb.Type> GetAllData()
        {
            var data = db.Types.Where(x => x.DelFlag == false).ToList();
            return data;
        }

        public Core.Dto.PMDb.Type GetDataById(int id)
        {
            var data = db.Types.Where(x => x.Id == id).FirstOrDefault();
            return data;
        }

        public void CreateData(Core.Dto.PMDb.Type model)
        {
            model.DelFlag = false;
            db.Types.Add(model);
            db.SaveChanges();
        }

        public void Update(Core.Dto.PMDb.Type model)
        {
            model.DelFlag = false;
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Core.Dto.PMDb.Type model)
        {
            Core.Dto.PMDb.Type item = db.Types.Find(model.Id);
            item.DelFlag = true;
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
