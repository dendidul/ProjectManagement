using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.Position
{
    public class PositionDA : IPositionDA
    {
        ProjectManagementEntities db = new ProjectManagementEntities();

        public IEnumerable<Core.Dto.PMDb.Position> GetAllData()
        {
            var data = db.Positions.Where(x => x.DelFlag == false).ToList();
            return data;
        }

        public Core.Dto.PMDb.Position GetDataById(int id)
        {
            var data = db.Positions.Where(x => x.Id == id).FirstOrDefault();
            return data;
        }

        public void CreateData(Core.Dto.PMDb.Position model)
        {
            model.DelFlag = false;
            db.Positions.Add(model);
            db.SaveChanges();
        }

        public void Update(Core.Dto.PMDb.Position model)
        {
            model.DelFlag = false;
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Core.Dto.PMDb.Position model)
        {
            Core.Dto.PMDb.Position item = db.Positions.Find(model.Id);
            item.DelFlag = true;
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
