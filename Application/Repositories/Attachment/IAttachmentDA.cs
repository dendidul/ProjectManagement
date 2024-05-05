using Core.Dto.PMDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.Attachment
{
    public interface IAttachmentDA
    {
        IEnumerable<Attachmentfile> GetDataByTaskId(int id);
        IEnumerable<Attachmentfile> GetDataByDocumentId(int id);
        void CreateData(Attachmentfile model);
        void Delete(Attachmentfile model);
    }
}
