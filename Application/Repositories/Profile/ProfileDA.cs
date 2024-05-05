using Application.Repositories.NewsFeed;
using Core.Dto.PMDb;
using Infrastructure.Context;
using Infrastructure.Helper.Config;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.Profile
{
    public class ProfileDA : IProfileDA
    {
        ProjectManagementEntities db = new ProjectManagementEntities();

        private readonly INewsFeedDA _newsFeedDA;
        private IDataAccessClientRepository _dataAccessClientRepository;
        private IConfigCreatorHelper _configCreatorHelper;
        private string Connection = "";
        private string ServerType = "";


        public ProfileDA(INewsFeedDA newsFeedDA, IDataAccessClientRepository dataAccessClientRepository, IConfigCreatorHelper configCreatorHelper)
        {

            _newsFeedDA = newsFeedDA;
            _dataAccessClientRepository = dataAccessClientRepository;
            _configCreatorHelper = configCreatorHelper;
            Connection = configCreatorHelper.Get("ConnectionStrings:ProjectManagement");
            ServerType = "PostgreeSQL";
        }

        public ProfileModel GetProfileByEmployeeId(int id)
        {
            //var Employeedata = db.ViewAllEmployees.Where(x => x.id == id).FirstOrDefault();
            //var EmployeeProject = db.ViewRolesProjectEmployees.Where(x => x.EmployeeId == id).ToList();


            var sqlEmployeeData = $@"select id from viewallemployee v where id =  {id}";
            var sqlEmployeeProject = $@"

select id,EmployeeName,RolesName,
projectsname as ProjectsName,ProjectID,EmployeeId,RolesId
from viewrolesprojectemployee v where employeeid = {id}

";

            var Employeedata = _dataAccessClientRepository.Get<ViewAllEmployee>(sqlEmployeeData, null, CommandType.Text, Connection, ServerType);

            var EmployeeProject = _dataAccessClientRepository.GetList<ViewRolesProjectEmployee>(sqlEmployeeProject, null, CommandType.Text, Connection, ServerType).ToList();

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
