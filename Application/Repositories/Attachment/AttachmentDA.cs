using Core.Dto.PMDb;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.Attachment
{
    public class AttachmentDA:IAttachmentDA
    {
        ProjectManagementEntities db = new ProjectManagementEntities();

        //public IEnumerable<AttachmentFile> GetAllData()
        //{
        //    var data = db.AttachmentFiles.ToList();
        //    return data;
        //}

        //public User GetDataById(int id)
        //{
        //    var data = db.Users.Where(x => x.id == id).FirstOrDefault();
        //    return data;
        //}


        public IEnumerable<Attachmentfile> GetDataByTaskId(int id)
        {
            var data = db.Attachmentfiles.Where(x => x.Taskid == id).ToList();
            return data;
        }

        public IEnumerable<Attachmentfile> GetDataByDocumentId(int id)
        {
            var data = db.Attachmentfiles.Where(x => x.Documentid == id).ToList();
            return data;
        }

        public void CreateData(Attachmentfile model)
        {
            db.Attachmentfiles.Add(model);
            db.SaveChanges();
        }

        //public void Update(User model)
        //{
        //    model.del_flag = false;
        //    db.Entry(model).State = EntityState.Modified;
        //    db.SaveChanges();
        //}

        public void Delete(Attachmentfile model)
        {
            Attachmentfile item = db.Attachmentfiles.Find(model.Id);
            db.Attachmentfiles.Remove(item);
            db.SaveChanges();
        }
    }
}
