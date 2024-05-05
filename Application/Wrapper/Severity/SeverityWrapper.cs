using Application.Repositories.Severity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrapper.Severity
{
    public class SeverityWrapper : ISeverityWrapper
    {
        private readonly ISeverityDA _severityDA;

        public SeverityWrapper(ISeverityDA severityDA)
        {
            _severityDA = severityDA;
        }


        public void CreateData(Core.Dto.PMDb.Severity model)
        {
            _severityDA.CreateData(model);
        }
        public void Delete(Core.Dto.PMDb.Severity model)
        {
            _severityDA.Delete(model);
        }
        public IEnumerable<Core.Dto.PMDb.Severity> GetAllData()
        {
            return _severityDA.GetAllData();
        }
        public Core.Dto.PMDb.Severity GetDataById(int id)
        {
            return _severityDA.GetDataById(id);
        }
        public void Update(Core.Dto.PMDb.Severity model)
        {
            _severityDA.Update(model);
        }

    }
}
