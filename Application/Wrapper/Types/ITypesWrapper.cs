namespace Application.Wrapper.Types
{
    public interface ITypesWrapper
    {
        void CreateData(Core.Dto.PMDb.Type model);
        void Delete(Core.Dto.PMDb.Type model);
        IEnumerable<Core.Dto.PMDb.Type> GetAllData();
        Core.Dto.PMDb.Type GetDataById(int id);
        void Update(Core.Dto.PMDb.Type model);
    }
}