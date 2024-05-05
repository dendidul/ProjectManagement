using Core.Dto.PMDb;

namespace Application.Wrapper.Document
{
    public interface IDocumentWrapper
    {
        void CreateData(Core.Dto.PMDb.Document model);
        void Delete(Core.Dto.PMDb.Document model);
        IEnumerable<ViewDocumentByProject> GetAllDataByProject(int id);
        IEnumerable<Core.Dto.PMDb.Document> GetAllDataForPublic();
        Core.Dto.PMDb.Document GetDataById(int id);
        DocumentModel GetDocumentProjectById(int id);
        DocumentModel GetPublicDocumentById(int id);
        void Update(Core.Dto.PMDb.Document model);
    }
}