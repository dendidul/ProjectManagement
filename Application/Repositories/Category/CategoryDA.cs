using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Dto.PMDb;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories.Category
{
    public class CategoryDA : ICategoryDA
    {
        ProjectManagementEntities db = new ProjectManagementEntities();

        public IEnumerable<Core.Dto.PMDb.Category> GetAllData()
        {
            var data = db.Categories.Where(x => x.DelFlag == false).ToList();
            return data;
        }

        public Core.Dto.PMDb.Category GetDataById(int id)
        {
            var data = db.Categories.Where(x => x.Id == id).FirstOrDefault();
            return data;
        }

        public void CreateData(Core.Dto.PMDb.Category model)
        {
            model.DelFlag = false;
            db.Categories.Add(model);
            db.SaveChanges();
        }

        public void Update(Core.Dto.PMDb.Category model)
        {
            model.DelFlag = false;
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Core.Dto.PMDb.Category model)
        {
            Core.Dto.PMDb.Category item = db.Categories.Find(model.Id);
            item.DelFlag = true;
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }

    }
}
