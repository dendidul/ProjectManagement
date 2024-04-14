using Application.Repositories.Document;
using Core.Dto.PMDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrapper.Document
{
    public class DocumentWrapper : IDocumentWrapper
    {
        private readonly IDocumentDA _documentDA;

        public DocumentWrapper(IDocumentDA documentDA)
        {
            _documentDA = documentDA;
        }

        public void CreateData(Core.Dto.PMDb.Document model)
        {
            _documentDA.CreateData(model);
        }
        public void Delete(Core.Dto.PMDb.Document model)
        {
            _documentDA.Delete(model);
        }
        public IEnumerable<ViewDocumentByProject> GetAllDataByProject(int id)
        {
            return _documentDA.GetAllDataByProject(id);
        }
        public IEnumerable<Core.Dto.PMDb.Document> GetAllDataForPublic()
        {
            return _documentDA.GetAllDataForPublic();
        }
        public Core.Dto.PMDb.Document GetDataById(int id)
        {
            return _documentDA.GetDataById(id);
        }
        public DocumentModel GetDocumentProjectById(int id)
        {
            return _documentDA.GetDocumentProjectById(id);
        }
        public DocumentModel GetPublicDocumentById(int id)
        {
            return _documentDA.GetPublicDocumentById(id);
        }
        public void Update(Core.Dto.PMDb.Document model)
        {
            _documentDA.Update(model);
        }


    }
}
