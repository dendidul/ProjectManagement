using Core.Dto.PMDb;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.Document
{
    public class DocumentDA : IDocumentDA
    {
        ProjectManagementEntities db = new ProjectManagementEntities();

        public IEnumerable<ViewDocumentByProject> GetAllDataByProject(int id)
        {
            //var data = db.ViewDocumentByProjects.Where(x => x.projectid == id).ToList();

            var data = db.ViewDocumentByProjects.FromSqlInterpolated<ViewDocumentByProject>(
                $@"
            select id,documentname as DocumentName 
,projectsname as ProjectsName ,projectid  from viewdocumentbyprojec v where projectid = {id}




                "
                ).Select(x => x).ToList();
            return data;
        }

        public IEnumerable<Core.Dto.PMDb.Document> GetAllDataForPublic()
        {
            var data = db.Documents.Where(x => x.DelFlag == false && x.IsPublic == true).ToList();
            return data;
        }

        public Core.Dto.PMDb.Document GetDataById(int id)
        {
            var data = db.Documents.Where(x => x.Id == id).FirstOrDefault();
            return data;
        }

        public DocumentModel GetPublicDocumentById(int id)
        {
            var getdatadocument = db.Documents.Where(x => x.Id == id && x.IsPublic == true).FirstOrDefault();
            var getdata = db.Attachmentfiles.Where(x => x.Documentid == id).ToList();

            var data = new DocumentModel()
            {
                Description = getdatadocument.Description,
                DocumentName = getdatadocument.Documentname,
                //ProjectId = (int)(getdatadocument.projectid),
                id = getdatadocument.Id,
                ListAttachmentFiles = getdata
            };
            return data;
        }

        public DocumentModel GetDocumentProjectById(int id)
        {
            var getdatadocument = db.Documents.Where(x => x.Id == id).FirstOrDefault();
            var getdata = db.Attachmentfiles.Where(x => x.Documentid == id).ToList();

            var data = new DocumentModel()
            {
                Description = getdatadocument.Description,
                DocumentName = getdatadocument.Documentname,
                ProjectId = (int)(getdatadocument.Projectid),
                id = getdatadocument.Id,
                ListAttachmentFiles = getdata
            };
            return data;
        }


        public void CreateData(Core.Dto.PMDb.Document model)
        {
            model.DelFlag = false;
            db.Documents.Add(model);
            db.SaveChanges();
        }

        public void Update(Core.Dto.PMDb.Document model)
        {

            model.DelFlag = false;
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Core.Dto.PMDb.Document model)
        {
            Core.Dto.PMDb.Document item = db.Documents.Find(model.Id);
            item.DelFlag = true;
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
