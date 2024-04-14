namespace Application.Wrapper.Position
{
    public interface IPositionWrapper
    {
        void CreateData(Core.Dto.PMDb.Position model);
        void Delete(Core.Dto.PMDb.Position model);
        IEnumerable<Core.Dto.PMDb.Position> GetAllData();
        Core.Dto.PMDb.Position GetDataById(int id);
        void Update(Core.Dto.PMDb.Position model);
    }
}