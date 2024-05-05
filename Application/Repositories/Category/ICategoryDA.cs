namespace Application.Repositories.Category
{
    public interface ICategoryDA
    {
        void CreateData(Core.Dto.PMDb.Category model);
        void Delete(Core.Dto.PMDb.Category model);
        IEnumerable<Core.Dto.PMDb.Category> GetAllData();
        Core.Dto.PMDb.Category GetDataById(int id);
        void Update(Core.Dto.PMDb.Category model);
    }
}