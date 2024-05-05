using Application.Repositories.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrapper.Menu
{
    public class MenuWrapper : IMenuWrapper
    {
        private readonly IMenuDA _menuDA;

        public MenuWrapper(IMenuDA menuDA)
        {
            _menuDA = menuDA;
        }

        public void CreateData(Core.Dto.PMDb.Menu model)
        {
            _menuDA.CreateData(model);
        }
        public void Delete(Core.Dto.PMDb.Menu model)
        {
            _menuDA.Delete(model);
        }
        public IEnumerable<Core.Dto.PMDb.Menu> GetAllData()
        {
            var data = _menuDA.GetAllData();
            return data;
        }
        public Core.Dto.PMDb.Menu GetDataById(int id)
        {
            var data = _menuDA.GetDataById(id);
            return data;
        }
        public IEnumerable<Core.Dto.PMDb.Menu> GetParentMenu()
        {
            var data = _menuDA.GetParentMenu();
            return data;
        }
        public void Update(Core.Dto.PMDb.Menu model)
        {
            _menuDA.Update(model);
        }

    }
}
