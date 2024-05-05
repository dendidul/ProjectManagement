using Application.Repositories.NewsFeed;
using Core.Dto.PMDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrapper.NewsFeed
{
    public class NewsFeedWrapper : INewsFeedWrapper
    {
        private readonly INewsFeedDA _newsFeedDA;

        public NewsFeedWrapper(INewsFeedDA newsFeedDA)
        {
            _newsFeedDA = newsFeedDA;
        }

        public void AddNewsFeed(int EmployeeId, EnumAction action, int taskid, int typeid)
        {
            _newsFeedDA.AddNewsFeed(EmployeeId, action, taskid, typeid);
        }
        public void CreateData(Newsfeed model)
        {
            _newsFeedDA.CreateData(model);
        }
        public void Delete(Newsfeed model)
        {
            _newsFeedDA.Delete(model);
        }
        public IEnumerable<NewsFeedModels> GetAllActivityNewsFeedByEmployeeId(int EmployeeId)
        {
            return _newsFeedDA.GetAllActivityNewsFeedByEmployeeId(EmployeeId);
        }
        public IEnumerable<NewsFeedModels> GetAllActivityNewsFeedByProjectId(int ProjectId)
        {
            return _newsFeedDA.GetAllActivityNewsFeedByProjectId(ProjectId);
        }
        public IEnumerable<Newsfeed> GetAllData()
        {
            return _newsFeedDA.GetAllData();
        }
        public Newsfeed GetDataByEmployeeId(int id)
        {
            return _newsFeedDA.GetDataByEmployeeId(id);
        }
        public Newsfeed GetDataById(int id)
        {
            return _newsFeedDA.GetDataById(id);
        }
        public Newsfeed GetDataByProjectId(int id)
        {
            return _newsFeedDA.GetDataByProjectId(id);
        }
        public IEnumerable<NewsFeedModels> GetNewsFeedByEmployeeId(int EmployeeId)
        {
            return _newsFeedDA.GetNewsFeedByEmployeeId(EmployeeId);
        }
        public IEnumerable<EventViewModel> GetNewsFeedByEmployeeIdForCalendar(int EmployeeId)
        {
            return _newsFeedDA.GetNewsFeedByEmployeeIdForCalendar(EmployeeId);
        }
        public IEnumerable<NewsFeedModels> GetNewsFeedByProjectId(int ProjectId)
        {
            return _newsFeedDA.GetNewsFeedByProjectId(ProjectId);
        }
        public IEnumerable<NewsFeedModels> GetRecentActivityByEmployeeId(int EmployeeId)
        {
            return _newsFeedDA.GetRecentActivityByEmployeeId(EmployeeId);
        }
        public void Update(Newsfeed model)
        {
            _newsFeedDA.Update(model);
        }

    }
}
