using Application.Repositories.Employee;
using Core.Dto.PMDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrapper.Employee
{
    public class EmployeeWrapper : IEmployeeWrapper
    {
        private readonly IEmployeeDA _employeeDA;

        public EmployeeWrapper(IEmployeeDA employeeDA)
        {
            _employeeDA = employeeDA;
        }

        public void CreateData(Core.Dto.PMDb.Employee model)
        {
            _employeeDA.CreateData(model);
        }
        public void Delete(Core.Dto.PMDb.Employee model)
        {
            _employeeDA.Delete(model);
        }
        public IEnumerable<ViewAllEmployee> GetAllData()
        {
            return _employeeDA.GetAllData();
        }
        public Core.Dto.PMDb.Employee GetDataById(int id)
        {
            return _employeeDA.GetDataById(id);
        }
        public string GetEmployeeFullName(int id)
        {
            return _employeeDA.GetEmployeeFullName(id);
        }
        public void Update(Core.Dto.PMDb.Employee model)
        {
            _employeeDA.Update(model);
        }

    }
}
