using Application.Wrapper.Menu;
using Application.Wrapper.Project;
using Application.Wrapper.Roles;
using Application.Wrapper.RolesMenu;
using Application.Wrapper.RolesProjectEmployee;
using Core.Dto.PMDb;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Utils;

namespace Web.Controllers
{
    public class MenuController : SidebarMenuController
    {

        #region

        private readonly Web.Utils.CookieManager _cookieManager;
        private readonly IRolesMenuWrapper _rolesMenuWrapper;


        #endregion

        private readonly IMenuWrapper _menuWrapper;
        private readonly IRolesWrapper _rolesWrapper;       
        private readonly CMSAuthorize _byPassRoute;

      

        private readonly ILogger<MenuController> _logger;


        public MenuController(
            IRolesMenuWrapper rolesMenuWrapper,
            IMenuWrapper menuWrapper,
            IRolesWrapper rolesWrapper,
         
        
            Web.Utils.CookieManager cookieManager,
            CMSAuthorize byPassRoute,
            ILogger<MenuController> logger

            ) : base(rolesMenuWrapper, cookieManager)
        {
            _rolesMenuWrapper = rolesMenuWrapper;
            _menuWrapper = menuWrapper;
            _rolesWrapper = rolesWrapper;           
          
            _cookieManager = cookieManager;
            _byPassRoute = byPassRoute;
            _logger = logger;
          

        }

     


        public ActionResult IndexMenu()
        {
            var data = _menuWrapper.GetAllData();
            return View(data);
        }

        public ActionResult AuthorityAccessMenu(int id)
        {
            var data = _rolesMenuWrapper.ReadRoleMenuByRoleID(id);
            return View(data);
        }

        [HttpPost]

        public ActionResult AuthorityAccessMenu(AuthorityMenuModel model, string[] SelectedMenu, string[] SelectedChildMenu)
        {

            var getdata = _rolesMenuWrapper.GetRoleMenuByRolesId(model.Role.Id);
            foreach (var i in getdata)
            {
                _rolesMenuWrapper.Delete(i);
            }

            if (SelectedMenu != null)
            {
                foreach (var j in SelectedMenu)
                {
                    Rolesmenu form = new Rolesmenu();
                    form.Menuid = int.Parse(j);
                    form.Rolesid = model.Role.Id;
                    _rolesMenuWrapper.CreateData(form);
                }
            }


            if (SelectedChildMenu != null)
            {
                foreach (var k in SelectedChildMenu)
                {
                    Rolesmenu form = new Rolesmenu();
                    form.Menuid = int.Parse(k);
                    form.Rolesid = model.Role.Id;
                    _rolesMenuWrapper.CreateData(form);
                }
            }



            //var data = RolesMenuLogic.ReadRoleMenuByRoleID(id);
            //return View(data);
            return RedirectToAction("ListRoles");
        }


        public ActionResult ListRoles()
        {
            var data = _rolesWrapper.GetAllData();
            return View(data);
        }

        public ActionResult CreateMenu()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateMenu(Menu model)
        {
            _menuWrapper.CreateData(model);
            return RedirectToAction("IndexMenu");
        }

        public ActionResult EditMenu(int id)
        {
            var data = _menuWrapper.GetDataById(id);
            return View(data);
        }

        [HttpPost]
        public ActionResult EditMenu(Menu model)
        {
            _menuWrapper.Update(model);
            return RedirectToAction("IndexMenu");
        }

        public ActionResult DetailsMenu(int id)
        {
            var data = _menuWrapper.GetDataById(id);
            return View(data);
        }

        public ActionResult DeleteMenu(int id)
        {
            var data = _menuWrapper.GetDataById(id);
            return View(data);
        }
        [HttpPost]
        public ActionResult DeleteMenu(Menu model)
        {
            _menuWrapper.Delete(model);
            return RedirectToAction("IndexMenu");
        }

    }
}
