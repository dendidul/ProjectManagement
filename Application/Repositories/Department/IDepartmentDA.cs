namespace Application.Repositories.Department
{
    public interface IDepartmentDA
    {
        void CreateData(Core.Dto.PMDb.Department model);
        void Delete(Core.Dto.PMDb.Department model);
        IEnumerable<Core.Dto.PMDb.Department> GetAllData();
        Core.Dto.PMDb.Department GetDataById(int id);
        void Update(Core.Dto.PMDb.Department model);
    }
}