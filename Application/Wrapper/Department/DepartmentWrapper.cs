using Application.Repositories.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrapper.Department
{
    public class DepartmentWrapper : IDepartmentWrapper
    {
        private readonly IDepartmentDA _departmentDA;

        public DepartmentWrapper(IDepartmentDA departmentDA)
        {
            _departmentDA = departmentDA;
        }


        public void CreateData(Core.Dto.PMDb.Department model)
        {
            _departmentDA.CreateData(model);
        }
        public void Delete(Core.Dto.PMDb.Department model)
        {
            _departmentDA.Delete(model);
        }
        public IEnumerable<Core.Dto.PMDb.Department> GetAllData()
        {
            return _departmentDA.GetAllData();
        }
        public Core.Dto.PMDb.Department GetDataById(int id)
        {

            return _departmentDA.GetDataById(id);
        }
        public void Update(Core.Dto.PMDb.Department model)
        {
            _departmentDA.Update(model);
        }
    }
}
