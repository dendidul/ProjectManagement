using Core.Dto.PMDb;

namespace Application.Wrapper.Employee
{
    public interface IEmployeeWrapper
    {
        void CreateData(Core.Dto.PMDb.Employee model);
        void Delete(Core.Dto.PMDb.Employee model);
        IEnumerable<ViewAllEmployee> GetAllData();
        Core.Dto.PMDb.Employee GetDataById(int id);
        string GetEmployeeFullName(int id);
        void Update(Core.Dto.PMDb.Employee model);
    }
}