using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.Comment
{
    public class CommentDA : ICommentDA
    {
        ProjectManagementEntities db = new ProjectManagementEntities();

        public IEnumerable<Core.Dto.PMDb.Comment> GetAllData()
        {
            var data = db.Comments.Where(x => x.DelFlag == false).ToList();
            return data;
        }

        public Core.Dto.PMDb.Comment GetDataById(int id)
        {
            var data = db.Comments.Where(x => x.Id == id).FirstOrDefault();
            return data;
        }

        public void CreateData(Core.Dto.PMDb.Comment model)
        {
            model.DelFlag = false;
            db.Comments.Add(model);
            db.SaveChanges();
        }

        public void Update(Core.Dto.PMDb.Comment model)
        {
            model.DelFlag = false;
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Core.Dto.PMDb.Comment model)
        {
            Core.Dto.PMDb.Comment item = db.Comments.Find(model.Id);
            item.DelFlag = true;
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
