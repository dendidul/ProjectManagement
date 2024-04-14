using Application.Repositories.NewsFeed;
using Core.Dto.PMDb;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.Profile
{
    public class ProfileDA : IProfileDA
    {
        ProjectManagementEntities db = new ProjectManagementEntities();

        private readonly INewsFeedDA _newsFeedDA;

        public ProfileDA(INewsFeedDA newsFeedDA)
        {

            _newsFeedDA = newsFeedDA;
        }

        public ProfileModel GetProfileByEmployeeId(int id)
        {
            var Employeedata = db.ViewAllEmployees.Where(x => x.id == id).FirstOrDefault();
            var EmployeeProject = db.ViewRolesProjectEmployees.Where(x => x.EmployeeId == id).ToList();

            var recentActivity = _newsFeedDA.GetRecentActivityByEmployeeId(id).ToList();


            var data = new ProfileModel()
            {
                UserData = Employeedata,
                RolesProjectEmployee = EmployeeProject,
                ListRecentActivityModel = recentActivity
            };

            return data;

        }
    }
}
