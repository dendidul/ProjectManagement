using Core.Dto.PMDb;

namespace Application.Wrapper.Users
{
    public interface IUsersWrapper
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