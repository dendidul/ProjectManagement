using Application.Repositories.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrapper.Types
{
    public class TypesWrapper : ITypesWrapper
    {

        private readonly ITypesDA _typesDA;

        public TypesWrapper(ITypesDA typesDA)
        {
            _typesDA = typesDA;
        }

        public void CreateData(Core.Dto.PMDb.Type model)
        {
            _typesDA.CreateData(model);
        }
        public void Delete(Core.Dto.PMDb.Type model)
        {
            _typesDA.Delete(model);
        }
        public IEnumerable<Core.Dto.PMDb.Type> GetAllData()
        {
            return _typesDA.GetAllData();
        }
        public Core.Dto.PMDb.Type GetDataById(int id)
        {
            return _typesDA.GetDataById(id);
        }
        public void Update(Core.Dto.PMDb.Type model)
        {
            _typesDA.Update(model);
        }
    }
}
