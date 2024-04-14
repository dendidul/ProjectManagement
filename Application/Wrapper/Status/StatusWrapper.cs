using Application.Repositories.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrapper.Status
{
    public class StatusWrapper : IStatusWrapper
    {
        private readonly IStatusDA _statusDA;

        public StatusWrapper(IStatusDA statusDA)
        {
            _statusDA = statusDA;
        }

        public void CreateData(Core.Dto.PMDb.Status model)
        {
            _statusDA.CreateData(model);
        }
        public void Delete(Core.Dto.PMDb.Status model)
        {
            _statusDA.Delete(model);
        }
        public IEnumerable<Core.Dto.PMDb.Status> GetAllData()
        {
            return _statusDA.GetAllData();
        }
        public Core.Dto.PMDb.Status GetDataById(int id)
        {
            return _statusDA.GetDataById(id);
        }
        public IEnumerable<Core.Dto.PMDb.Status> GetStatus()
        {
            return _statusDA.GetStatus();
        }
        public void Update(Core.Dto.PMDb.Status model)
        {
            _statusDA.Update(model);
        }
    }
}
