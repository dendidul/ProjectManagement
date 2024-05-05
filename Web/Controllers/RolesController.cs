using Application.Wrapper.RolesMenu;
using Microsoft.AspNetCore.Mvc;
using Core.Dto.PMDb;
using Application.Wrapper.Roles;

namespace Web.Controllers
{
    public class RolesController  : SidebarMenuController
    {
        #region

        private readonly Web.Utils.CookieManager _cookieManager;
    private readonly IRolesMenuWrapper _rolesMenuWrapper;
        private readonly GlobalController _globalController;



        #endregion

        private readonly ILogger<CategoryController> _logger;
    private readonly IRolesWrapper _rolesWrapper;

    public RolesController(ILogger<CategoryController> logger,
       IRolesWrapper rolesWrapper,

       Web.Utils.CookieManager cookieManager,
       IRolesMenuWrapper rolesMenuWrapper,
       GlobalController globalController

       ) : base(rolesMenuWrapper, cookieManager,globalController)
    {

        _logger = logger;
            _rolesWrapper = rolesWrapper;



        #region

        _cookieManager = cookieManager;
        _rolesMenuWrapper = rolesMenuWrapper;
            _globalController = globalController;


        #endregion


    }

        public ActionResult Index()
        {
            var data = _rolesWrapper.GetAllData();
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Role model)
        {
            _rolesWrapper.CreateData(model);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var data = _rolesWrapper.GetDataById(id);
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(Role model)
        {
            _rolesWrapper.Update(model);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var data = _rolesWrapper.GetDataById(id);
            return View(data);
        }

        public ActionResult Delete(int id)
        {
            var data = _rolesWrapper.GetDataById(id);
            return View(data);
        }

        [HttpPost]
        public ActionResult Delete(Role model)
        {
            _rolesWrapper.Delete(model);
            return RedirectToAction("Index");
        }
    }
}
