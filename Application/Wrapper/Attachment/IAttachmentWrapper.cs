using Core.Dto.PMDb;

namespace Application.Wrapper.Attachment
{
    public interface IAttachmentWrapper
    {
        void CreateData(Attachmentfile model);
        void Delete(Attachmentfile model);
        IEnumerable<Attachmentfile> GetDataByDocumentId(int id);
        IEnumerable<Attachmentfile> GetDataByTaskId(int id);
    }
}