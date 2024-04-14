using Application.Repositories.Users;
using Core.Dto.PMDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrapper.Users
{
    public class UsersWrapper : IUsersWrapper
    {
        private readonly IUserDA _userDA;


        public UsersWrapper(IUserDA userDA)
        {
            _userDA = userDA;
        }

        public bool CheckValidateUser(string username, string password)
        {

            var data = _userDA.CheckValidateUser(username, password);
            return data;

        }
        public void CreateData(User model)
        {
            _userDA.CreateData(model);
        }
        public void Delete(User model)
        {
            _userDA.Delete(model);
        }
        public IEnumerable<ListUserModels> GetAllData()
        {
            var data = _userDA.GetAllData();
            return data;
        }
        public User GetDataById(int id)
        {
            var data = _userDA.GetDataById(id);
            return data;
        }
        public User GetUser(string username, string password)
        {
            var data = _userDA.GetUser(username, password);
            return data;
        }
        public void Update(User model)
        {
            _userDA.Update(model);
        }
    }
}
