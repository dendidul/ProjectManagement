using Microsoft.AspNetCore.Mvc;
using Core.Dto.PMDb;
using Application.Wrapper.RolesMenu;
using Application.Wrapper.TaskGroup;


namespace Web.Controllers
{
    public class TaskGroupController : SidebarMenuController
    {

        #region

        private readonly Web.Utils.CookieManager _cookieManager;
        private readonly IRolesMenuWrapper _rolesMenuWrapper;


        #endregion

        private readonly ILogger<TaskGroupController> _logger;
        private readonly ITaskGroupWrapper _taskGroupWrapper;

        public TaskGroupController(ILogger<TaskGroupController> logger,
           ITaskGroupWrapper taskGroupWrapper,

           Web.Utils.CookieManager cookieManager,

           IRolesMenuWrapper rolesMenuWrapper



           ) : base(rolesMenuWrapper, cookieManager)
        {

            _logger = logger;
            _taskGroupWrapper = taskGroupWrapper;




            #region

            _cookieManager = cookieManager;
            _rolesMenuWrapper = rolesMenuWrapper;


            #endregion


        }

        public ActionResult Index()
        {
            int ProjectId = _cookieManager.GetCookie("ProjectID") != null ? Convert.ToInt32(_cookieManager.GetCookie("ProjectID")) : 0;
            var getemployeeid = _cookieManager.GetCookie("EmployeeId") != null ? Convert.ToInt32(_cookieManager.GetCookie("EmployeeId")) : 0;
            var data = _taskGroupWrapper.GetAllDataByEmployee(getemployeeid, ProjectId);
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Taskgroup model, string IsActive)
        {
            model.Isactive = IsActive != null ? true : false;
            _taskGroupWrapper.CreateData(model);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var data = _taskGroupWrapper.GetDataById(id);

            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(Taskgroup model, string IsActive)
        {
            model.Isactive = IsActive != null ? true : false;
            _taskGroupWrapper.Update(model);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var data = _taskGroupWrapper.GetDataById(id);
            return View(data);

        }

        [HttpPost]
        public ActionResult Delete(Taskgroup model)
        {
            _taskGroupWrapper.Delete(model);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var data = _taskGroupWrapper.GetDataById(id);
            return View(data);
        }
    }
}
