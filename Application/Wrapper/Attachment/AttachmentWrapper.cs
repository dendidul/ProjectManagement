using Application.Repositories.Attachment;
using Core.Dto.PMDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrapper.Attachment
{
    public class AttachmentWrapper : IAttachmentWrapper
    {
        private readonly IAttachmentDA _attachmentDA;


        public AttachmentWrapper(IAttachmentDA attachmentDA)
        {
            _attachmentDA = attachmentDA;
        }


        public IEnumerable<Attachmentfile> GetDataByTaskId(int id)
        {
            var data = _attachmentDA.GetDataByTaskId(id);
            return data;
        }

        public IEnumerable<Attachmentfile> GetDataByDocumentId(int id)
        {
            var data = _attachmentDA.GetDataByDocumentId(id);
            return data;
        }

        public void CreateData(Attachmentfile model)
        {
            _attachmentDA.CreateData(model);
        }

      

        public void Delete(Attachmentfile model)
        {
            _attachmentDA.Delete(model);
        }


    }
}
