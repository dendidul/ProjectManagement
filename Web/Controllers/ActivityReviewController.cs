using Microsoft.AspNetCore.Mvc;
using Application.Wrapper.RolesMenu;
using Core.Dto.PMDb;
using Application.Wrapper.TaskLog;
using Application.Wrapper.Task;
using Application.Wrapper.Attachment;
using Application.Wrapper.NewsFeed;

namespace Web.Controllers
{
    public class ActivityReviewController : SidebarMenuController
    {

        #region

        private readonly Web.Utils.CookieManager _cookieManager;
        private readonly IRolesMenuWrapper _rolesMenuWrapper;
        private readonly GlobalController _globalController;

        #endregion

        private readonly ILogger<CategoryController> _logger;
        private readonly ITaskLogWrapper _taskLogWrapper;
        private readonly ITaskWrapper _taskWrapper;
        private readonly IAttachmentWrapper _attachmentWrapper;
        private readonly INewsFeedWrapper _newsFeedWrapper;

        public ActivityReviewController(ILogger<CategoryController> logger,
          
            ITaskLogWrapper taskLogWrapper,
            ITaskWrapper taskWrapper,
            IAttachmentWrapper attachmentWrapper,
            INewsFeedWrapper newsFeedWrapper,


        Web.Utils.CookieManager cookieManager,

           IRolesMenuWrapper rolesMenuWrapper,


           GlobalController globalController


           ) : base(rolesMenuWrapper, cookieManager,globalController)
        {

            _logger = logger;
            _taskLogWrapper = taskLogWrapper;
            _taskWrapper = taskWrapper;
            _attachmentWrapper = attachmentWrapper;
            _newsFeedWrapper = newsFeedWrapper;

            #region

            _cookieManager = cookieManager;
            _rolesMenuWrapper = rolesMenuWrapper;
            _globalController = globalController;


            #endregion


        }


        public ActionResult Index()
        {
            int ProjectId = _cookieManager.GetCookie("ProjectID") != null ? Convert.ToInt32(_cookieManager.GetCookie("ProjectID")) : 0;
            int EmployeeId = _cookieManager.GetCookie("ProjectID") != null ? Convert.ToInt32(_cookieManager.GetCookie("ProjectID")) : 0;

            if (ProjectId == 100)
            {
                var data = _taskWrapper.GetListTaskForAdmin().ToList();

                return View(data);
            }
            else
            {
                var data = _taskWrapper.GetListActivityReviewByReviewEmployeeAndProjects(EmployeeId, ProjectId).ToList();

                return View(data);
            }

        }

        public ActionResult Edit(int id)
        {

            //var data = TaskLogic.GetDataById(id);
            var employeeid = _cookieManager.GetCookie("EmployeeId") != null ? Convert.ToInt32(_cookieManager.GetCookie("EmployeeId")) : 0;
            var data = _taskWrapper.GetDataById(id, employeeid);
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(FormTaskModel model, string Command)
        {
            Core.Dto.PMDb.Task formdata = new Core.Dto.PMDb.Task();
            formdata.Id = model.TaskData.Id;
            formdata.Taskgroupid = model.TaskData.Taskgroupid;
            formdata.Taskname = model.TaskData.Taskname;
            formdata.Projectid = model.TaskData.Projectid;
            formdata.Startdate = model.TaskData.Startdate;
            formdata.Duedate = model.TaskData.Duedate;
            formdata.Severityid = model.TaskData.Severityid;
            formdata.Descripition = model.TaskData.Descripition;
            formdata.Assignto = model.TaskData.Assignto;
            formdata.Updateby = Convert.ToInt32(_cookieManager.GetCookie("EmployeeId"));
            formdata.Updatedate = DateTime.Now;
            formdata.Createdby = model.TaskData.Createdby;
            formdata.Createddate = model.TaskData.Createddate;
            formdata.Type = model.TaskData.Type;
            //formdata.StatusId = model.TaskData.StatusId;
            formdata.Result = model.TaskData.Result;
            formdata.Categoryid = model.TaskData.Categoryid;
            formdata.Reviewby = model.TaskData.Reviewby;

            if (Command == "Completed")
            {
                formdata.Statusid = 5;

                _newsFeedWrapper.AddNewsFeed(Convert.ToInt32(_cookieManager.GetCookie("EmployeeId")), EnumAction.Completed, (int)formdata.Id, (int)formdata.Type);

            }

            else if (Command == "Need Repair")
            {
                formdata.Statusid = 9;
                _newsFeedWrapper.AddNewsFeed(Convert.ToInt32(_cookieManager.GetCookie("EmployeeId")), EnumAction.NeedRepair, (int)formdata.Id, (int)formdata.Type);
            }

            else
            {
                formdata.Statusid = model.TaskData.Statusid;
                _newsFeedWrapper.AddNewsFeed(Convert.ToInt32(_cookieManager.GetCookie("EmployeeId")), EnumAction.Edited, (int)formdata.Id, (int)formdata.Type);
            }



            _taskWrapper.Update(formdata);

            return RedirectToAction("Index");

        }



    }
}
