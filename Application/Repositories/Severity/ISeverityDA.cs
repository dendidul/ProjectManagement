namespace Application.Repositories.Severity
{
    public interface ISeverityDA
    {
        void CreateData(Core.Dto.PMDb.Severity model);
        void Delete(Core.Dto.PMDb.Severity model);
        IEnumerable<Core.Dto.PMDb.Severity> GetAllData();
        Core.Dto.PMDb.Severity GetDataById(int id);
        void Update(Core.Dto.PMDb.Severity model);
    }
}