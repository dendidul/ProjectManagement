namespace Application.Wrapper.Comment
{
    public interface ICommentWrapper
    {
        void CreateData(Core.Dto.PMDb.Comment model);
        void Delete(Core.Dto.PMDb.Comment model);
        IEnumerable<Core.Dto.PMDb.Comment> GetAllData();
        Core.Dto.PMDb.Comment GetDataById(int id);
        void Update(Core.Dto.PMDb.Comment model);
    }
}