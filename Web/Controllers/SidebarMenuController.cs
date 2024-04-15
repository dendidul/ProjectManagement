using Application.Wrapper.Project;
using Application.Wrapper.RolesMenu;
using Core.Dto.PMDb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Utils;

namespace Web.Controllers
{
    public class SidebarMenuController : Controller
    {

        private readonly Web.Utils.CookieManager _cookieManager;
        private readonly IRolesMenuWrapper _rolesMenuWrapper;
        private readonly GlobalController _globalController;
      


        public SidebarMenuController(
             IRolesMenuWrapper rolesMenuWrapper,
             Web.Utils.CookieManager cookieManager,
             GlobalController globalController


            )
        {
            _cookieManager = cookieManager;
            _rolesMenuWrapper = rolesMenuWrapper;
            _globalController = globalController;
        }





        public override void OnActionExecuting(ActionExecutingContext ctx)
        {
            base.OnActionExecuting(ctx);
            ViewBagItem(ctx);

        }

        protected ActionResult ViewBagItem(ActionExecutingContext ctx)
        {

           // GlobalController _globalController = new GlobalController();

            ViewBag.Menu = BuildRoleMenu();
            ViewBag.ListProject = BuildListProject().ViewModelProjectGroupList;
            ViewBag.ListSubProject = BuildListProject();

            ViewBag.ParentMenu = new SelectList(_globalController.GetParentMenu(), "id", "Menuname", 0);
            ViewBag.ProjectGroup = new SelectList(_globalController.GetAllProjectGroup(), "Id", "Projectgroupname", 0);
            ViewBag.Roles = new SelectList(_globalController.GetAllRoles(), "Id", "Rolesname", 0);
            ViewBag.Employee = new SelectList(_globalController.GetAllEmployee(), "id", "EmployeeName", 0);
            ViewBag.Department = new SelectList(_globalController.GetAllDepartment(), "Id", "Departmentname", 0);
            ViewBag.Position = new SelectList(_globalController.GetAllPosition(), "Id", "Positionname", 0);
            ViewBag.Project = new SelectList(_globalController.GetAllProject(), "ProjectId", "ProjectName", 0);
            ViewBag.ActiveTaskGroup = new SelectList(_globalController.GetActiveTaskGroup(), "Id", "Taskgroupname", 0);
            ViewBag.TaskGroup = new SelectList(_globalController.GetAllTaskGroup(), "Id", "Taskgroupname", 0);
            ViewBag.Status = new SelectList(_globalController.GetStatus(), "Id", "Statusname", 0);
            ViewBag.AllStatus = new SelectList(_globalController.GetAllStatus(), "Id", "Statusname", 0);
            ViewBag.Severity = new SelectList(_globalController.GetAllSeverity(), "Id", "Severityname", 0);
            ViewBag.Type = new SelectList(_globalController.GetAllType(), "Id", "Typename", 0);

            ViewBag.Category = new SelectList(_globalController.GetAllCategory(), "Id", "Categoryname", 0);

            int ProjectId = _cookieManager.GetCookie("ProjectID") != null ? Convert.ToInt32(_cookieManager.GetCookie("ProjectID")) : 0; 

            ViewBag.EmployeeByProject = new SelectList(_globalController.GetAllEmployeeByProject(ProjectId), "EmployeeId", "EmployeeName", 0);

            ViewBag.ProjectByEmployeeId = new SelectList(_globalController.GetAllProjectEmployeeID(), "ProjectId", "ProjectsName");

            return null;
        }

        private IList<RolesMenuViewModel> BuildRoleMenu()
        {
            var data = _cookieManager.GetCookie("Role_ID");
            var RolesID = Convert.ToInt32(data);
            var datalist = _rolesMenuWrapper.BuildRoleMenu(RolesID);
            return datalist;
        }

        private ViewModelsProjectEmployee BuildListProject()
        {
            var EmployeeId = _cookieManager.GetCookie("EmployeeId") != null ? Convert.ToInt32(_cookieManager.GetCookie("EmployeeId").ToString()) : 0;
            var data = _rolesMenuWrapper.GetAllProjectByEmployeeId(EmployeeId);

            return data;
        }
    }
}
