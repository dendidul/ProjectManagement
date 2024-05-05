using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.Menu
{
    public class MenuDA : IMenuDA
    {
        ProjectManagementEntities db = new ProjectManagementEntities();
        public IEnumerable<Core.Dto.PMDb.Menu> GetAllData()
        {
            var data = db.Menus.ToList();
            return data;
        }


        public IEnumerable<Core.Dto.PMDb.Menu> GetParentMenu()
        {
            var data = db.Menus.Where(x => x.ParentId == 0).ToList();
            return data;

        }

        public Core.Dto.PMDb.Menu GetDataById(int id)
        {
            var data = db.Menus.Where(x => x.Id == id).FirstOrDefault();
            return data;
        }

        public void CreateData(Core.Dto.PMDb.Menu model)
        {

            db.Menus.Add(model);
            db.SaveChanges();
        }

        public void Update(Core.Dto.PMDb.Menu model)
        {
            //model.del_flag = false;
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Core.Dto.PMDb.Menu model)
        {
            Core.Dto.PMDb.Menu item = db.Menus.Find(model.Id);
            //item.del_flag = true;
            //db.Entry(item).State = EntityState.Modified;
            db.Menus.Remove(item);
            db.SaveChanges();
        }
    }
}
