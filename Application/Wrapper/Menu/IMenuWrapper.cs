namespace Application.Wrapper.Menu
{
    public interface IMenuWrapper
    {
        void CreateData(Core.Dto.PMDb.Menu model);
        void Delete(Core.Dto.PMDb.Menu model);
        IEnumerable<Core.Dto.PMDb.Menu> GetAllData();
        Core.Dto.PMDb.Menu GetDataById(int id);
        IEnumerable<Core.Dto.PMDb.Menu> GetParentMenu();
        void Update(Core.Dto.PMDb.Menu model);
    }
}