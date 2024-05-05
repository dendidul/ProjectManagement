using Core.Dto.PMDb;

namespace Application.Repositories.Employee
{
    public interface IEmployeeDA
    {
        void CreateData(Core.Dto.PMDb.Employee model);
        void Delete(Core.Dto.PMDb.Employee model);
        IEnumerable<ViewAllEmployee> GetAllData();
        Core.Dto.PMDb.Employee GetDataById(int id);
        string GetEmployeeFullName(int id);
        void Update(Core.Dto.PMDb.Employee model);
    }
}