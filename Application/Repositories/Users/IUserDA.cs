using Core.Dto.PMDb;

namespace Application.Repositories.Users
{
    public interface IUserDA
    {
        bool CheckValidateUser(string username, string password);
        void CreateData(User model);
        void Delete(User model);
        IEnumerable<ListUserModels> GetAllData();
        User GetDataById(int id);
        User GetUser(string username, string password);
        void Update(User model);
    }
}