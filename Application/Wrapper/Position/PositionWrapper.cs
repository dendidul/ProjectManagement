using Application.Repositories.Position;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrapper.Position
{
    public class PositionWrapper : IPositionWrapper
    {
        private readonly IPositionDA _positionDA;

        public PositionWrapper(IPositionDA positionDA)
        {
            _positionDA = positionDA;
        }

        public void CreateData(Core.Dto.PMDb.Position model)
        {
            _positionDA.CreateData(model);
        }
        public void Delete(Core.Dto.PMDb.Position model)
        {
            _positionDA.Delete(model);
        }
        public IEnumerable<Core.Dto.PMDb.Position> GetAllData()
        {
            return _positionDA.GetAllData();
        }
        public Core.Dto.PMDb.Position GetDataById(int id)
        {
            return _positionDA.GetDataById(id);
        }
        public void Update(Core.Dto.PMDb.Position model)
        {
            _positionDA.Update(model);
        }
    }
}
