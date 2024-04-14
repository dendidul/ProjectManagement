using Application.Wrapper.Menu;
using Application.Wrapper.NewsFeed;
using Application.Wrapper.Project;
using Application.Wrapper.Roles;
using Application.Wrapper.RolesMenu;
using Application.Wrapper.RolesProjectEmployee;
using Application.Wrapper.Task;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using Web.Models;
using Web.Utils;

namespace Web.Controllers
{
    public class HomeController : SidebarMenuController
    {
        #region

        private readonly Web.Utils.CookieManager _cookieManager;
        private readonly IRolesMenuWrapper _rolesMenuWrapper;
             

        #endregion

        private readonly ILogger<HomeController> _logger;
        private readonly INewsFeedWrapper _newsFeedWrapper;
        private readonly ITaskWrapper _taskWrapper;

        private readonly GlobalController _globalController;



        public HomeController(ILogger<HomeController> logger, 
            INewsFeedWrapper newsFeedWrapper,          
            
            Web.Utils.CookieManager cookieManager,         
           
            IRolesMenuWrapper rolesMenuWrapper, ITaskWrapper taskWrapper, GlobalController globalController



            ) : base(rolesMenuWrapper, cookieManager)
        {          

            _logger = logger;
            _newsFeedWrapper = newsFeedWrapper;
            _taskWrapper = taskWrapper;
            _globalController = globalController;



            #region

            _cookieManager = cookieManager;
            _rolesMenuWrapper = rolesMenuWrapper;
         

            #endregion





        }

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [ServiceFilter(typeof(CMSAuthorize))]
        public ActionResult Dashboard()
        {
            int EmployeeId = _cookieManager.GetCookie("EmployeeId") != null ? Convert.ToInt32(_cookieManager.GetCookie("EmployeeId").ToString()) : 0;
            int ProjectId = _cookieManager.GetCookie("ProjectID") != null ? Convert.ToInt32(_cookieManager.GetCookie("ProjectID").ToString()) : 0;
            if (ProjectId == 0 || ProjectId == 100)
            {
                var data = _newsFeedWrapper.GetNewsFeedByEmployeeId(EmployeeId);
                return View(data);
            }
            else
            {
                var data = _newsFeedWrapper.GetNewsFeedByProjectId(ProjectId);
                return View(data);
            }




        }

        public ActionResult AllActivity()
        {
            int EmployeeId = _cookieManager.GetCookie("EmployeeId")!= null ? Convert.ToInt32(_cookieManager.GetCookie("EmployeeId").ToString()) : 0;
            int ProjectId = _cookieManager.GetCookie("ProjectID") != null ? Convert.ToInt32(_cookieManager.GetCookie("ProjectID")) : 0;
            if (ProjectId == 0 || ProjectId == 100)
            {
                var data = _newsFeedWrapper.GetAllActivityNewsFeedByEmployeeId(EmployeeId);
                return View(data);
            }
            else
            {
                var data = _newsFeedWrapper.GetAllActivityNewsFeedByProjectId(ProjectId);
                return View(data);
            }




        }

        public ActionResult Details(int id)
        {
            int ProjectId = _cookieManager.GetCookie("ProjectID") != null ? Convert.ToInt32(_cookieManager.GetCookie("ProjectID")) : 0;

            ViewBag.TaskGroup = new SelectList(_globalController.GetAllTaskGroup(), "id", "TaskGroupName", 0);
            ViewBag.Project = new SelectList(_globalController.GetAllProject(), "ProjectId", "ProjectName", 0);
            ViewBag.Severity = new SelectList(_globalController.GetAllSeverity(), "id", "SeverityName", 0);

            ViewBag.Category = new SelectList(_globalController.GetAllCategory(), "id", "CategoryName", 0);

            ViewBag.EmployeeByProject = new SelectList(_globalController.GetAllEmployeeByProject(ProjectId), "EmployeeId", "EmployeeName", 0);

            ViewBag.Status = new SelectList(_globalController.GetStatus(), "id", "StatusName", 0);

            ViewBag.Type = new SelectList(_globalController.GetAllType(), "Id", "TypeName", 0);

            var employeeid = _cookieManager.GetCookie("EmployeeId") != null ? Convert.ToInt32(_cookieManager.GetCookie("EmployeeId")) : 0;
            var data = _taskWrapper.GetDataByIdForActivityMonitoring(id);
            return View(data);
        }

    }
}