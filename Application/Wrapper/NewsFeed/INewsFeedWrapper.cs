using Core.Dto.PMDb;

namespace Application.Wrapper.NewsFeed
{
    public interface INewsFeedWrapper
    {
        void AddNewsFeed(int EmployeeId, EnumAction action, int taskid, int typeid);
        void CreateData(Newsfeed model);
        void Delete(Newsfeed model);
        IEnumerable<NewsFeedModels> GetAllActivityNewsFeedByEmployeeId(int EmployeeId);
        IEnumerable<NewsFeedModels> GetAllActivityNewsFeedByProjectId(int ProjectId);
        IEnumerable<Newsfeed> GetAllData();
        Newsfeed GetDataByEmployeeId(int id);
        Newsfeed GetDataById(int id);
        Newsfeed GetDataByProjectId(int id);
        IEnumerable<NewsFeedModels> GetNewsFeedByEmployeeId(int EmployeeId);
        IEnumerable<EventViewModel> GetNewsFeedByEmployeeIdForCalendar(int EmployeeId);
        IEnumerable<NewsFeedModels> GetNewsFeedByProjectId(int ProjectId);
        IEnumerable<NewsFeedModels> GetRecentActivityByEmployeeId(int EmployeeId);
        void Update(Newsfeed model);
    }
}