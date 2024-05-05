namespace Application.Repositories.Status
{
    public interface IStatusDA
    {
        void CreateData(Core.Dto.PMDb.Status model);
        void Delete(Core.Dto.PMDb.Status model);
        IEnumerable<Core.Dto.PMDb.Status> GetAllData();
        Core.Dto.PMDb.Status GetDataById(int id);
        IEnumerable<Core.Dto.PMDb.Status> GetStatus();
        void Update(Core.Dto.PMDb.Status model);
    }
}