using Application.Repositories.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrapper.Category
{
    public class CategoryWrapper : ICategoryWrapper
    {
        private readonly ICategoryDA _categoryDA;

        public CategoryWrapper(ICategoryDA categoryDA)
        {
            _categoryDA = categoryDA;
        }

        public void CreateData(Core.Dto.PMDb.Category model)
        {
            _categoryDA.CreateData(model);
        }
        public void Delete(Core.Dto.PMDb.Category model)
        {
            _categoryDA.Delete(model);
        }
        public IEnumerable<Core.Dto.PMDb.Category> GetAllData()
        {
            return _categoryDA.GetAllData();
        }
        public Core.Dto.PMDb.Category GetDataById(int id)
        {
            return _categoryDA.GetDataById(id);
        }
        public void Update(Core.Dto.PMDb.Category model)
        {
            _categoryDA.Update(model);
        }
    }
}
