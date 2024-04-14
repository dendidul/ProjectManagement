using Core.Dto.PMDb;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.Users
{
    public class UserDA : IUserDA
    {
        ProjectManagementEntities db = new ProjectManagementEntities();

        public IEnumerable<ListUserModels> GetAllData()
        {
            //var data = db.Users.Where(x => x.del_flag == false).ToList();
            var data = (from a in db.Users
                        join b in db.Employees on a.Employeeid equals b.Id
                        join c in db.Roles on a.Rolesid equals c.Id
                        where a.DelFlag == false
                        select new ListUserModels
                        {
                            id = a.Id,
                            EmployeeName = b.Firstname + " " + b.Lastname,
                            Username = a.Username,
                            Roles = c.Rolesname

                        }).ToList();
            return data;
        }

        public User GetDataById(int id)
        {
            var data = db.Users.Where(x => x.Id == id).FirstOrDefault();
            return data;
        }

        public User GetUser(string username, string password)
        {

            var data = db.Users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password == password).FirstOrDefault();
            return data;
        }



        public bool CheckValidateUser(string username, string password)
        {
            var data = db.Users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password == password).Any();
            return data;
        }

        public void CreateData(User model)
        {
            model.DelFlag = false;
            db.Users.Add(model);
            db.SaveChanges();
        }

        public void Update(User model)
        {
            model.DelFlag = false;
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(User model)
        {
            User item = db.Users.Find(model.Id);
            item.DelFlag = true;
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
