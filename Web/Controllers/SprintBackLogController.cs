using Microsoft.AspNetCore.Mvc;
using Application.Wrapper.RolesMenu;
using Core.Dto.PMDb;
using Application.Wrapper.Task;
using Application.Wrapper.TaskGroup;

namespace Web.Controllers
{
    public class SprintBackLogController : SidebarMenuController
    {
        #region

        private readonly Web.Utils.CookieManager _cookieManager;
        private readonly IRolesMenuWrapper _rolesMenuWrapper;
        private readonly GlobalController _globalController;



        #endregion

        private readonly ILogger<SprintBackLogController> _logger;
        private readonly ITaskWrapper _taskWrapper;
        private readonly ITaskGroupWrapper _taskGroupWrapper;

        public SprintBackLogController(ILogger<SprintBackLogController> logger,
             ITaskWrapper taskWrapper,
             ITaskGroupWrapper taskGroupWrapper,
           Web.Utils.CookieManager cookieManager,

           IRolesMenuWrapper rolesMenuWrapper,
           GlobalController globalController



           ) : base(rolesMenuWrapper, cookieManager,globalController)
        {

            _logger = logger;
            _taskWrapper = taskWrapper;
            _taskGroupWrapper = taskGroupWrapper;



            #region

            _cookieManager = cookieManager;
            _rolesMenuWrapper = rolesMenuWrapper;
            _globalController = globalController;

            #endregion


        }


        public ActionResult Index()
        {
            int ProjectId = _cookieManager.GetCookie("ProjectID") != null ? Convert.ToInt32(_cookieManager.GetCookie("ProjectID")) : 0;
            var data = _taskGroupWrapper.GetAllDataTaskGroupByProjectID(ProjectId);
            return View(data);
        }

        public ActionResult ConfigureTaskGroup(int id)
        {
            var data = _taskWrapper.GetSprintBackLogDataByTaskGroupID(id);
            return View(data);
        }

        [HttpPost]

        public ActionResult ConfigureTaskGroup(BackLogModel model, string[] SelectedTask)
        {



            var data = _taskWrapper.GetSprintBackLogDataByTaskGroupID(model.TaskGroupModel.Id).ListTask.ToList();


            foreach (var j in data)
            {
                Core.Dto.PMDb.Task form = new Core.Dto.PMDb.Task();
                var GetTempTaskdata = _taskWrapper.GetData(j.Id);
                form = GetTempTaskdata;

                form.Taskgroupid = null;
                form.Type = 3;
                form.Statusid = 3;
                _taskWrapper.Update(form);

                //var GetTaskData = TaskLogic.GetData(int.Parse(j.Id));

                GetTempTaskdata.Taskgroupid = model.TaskGroupModel.Id;
                GetTempTaskdata.Type = 1;
                GetTempTaskdata.Statusid = 3;
                _taskWrapper.Update(GetTempTaskdata);


            }


            return RedirectToAction("Index");
        }
    }
}
