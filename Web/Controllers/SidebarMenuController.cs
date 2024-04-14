using Application.Wrapper.Project;
using Application.Wrapper.RolesMenu;
using Core.Dto.PMDb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Web.Utils;

namespace Web.Controllers
{
    public class SidebarMenuController : Controller
    {

        private readonly Web.Utils.CookieManager _cookieManager;
        private readonly IRolesMenuWrapper _rolesMenuWrapper;
      


        public SidebarMenuController(
             IRolesMenuWrapper rolesMenuWrapper,
             Web.Utils.CookieManager cookieManager
            


            )
        {
            _cookieManager = cookieManager;
            _rolesMenuWrapper = rolesMenuWrapper;
            

           

        }


       


        public override void OnActionExecuting(ActionExecutingContext ctx)
        {
            base.OnActionExecuting(ctx);
            ViewBagItem(ctx);

        }

        protected ActionResult ViewBagItem(ActionExecutingContext ctx)
        {
            ViewBag.Menu = BuildRoleMenu();
            ViewBag.ListProject = BuildListProject().ViewModelProjectGroupList;
            ViewBag.ListSubProject = BuildListProject();
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
