using Microsoft.AspNetCore.Mvc;
using Application.Wrapper.RolesMenu;
using Core.Dto.PMDb;
using Application.Wrapper.RolesProjectEmployee;

namespace Web.Controllers
{
    public class EmployeeRolesProjectController : SidebarMenuController
    {
        #region

        private readonly Web.Utils.CookieManager _cookieManager;
        private readonly IRolesMenuWrapper _rolesMenuWrapper;
        private readonly GlobalController _globalController;



        #endregion

        private readonly ILogger<EmployeeRolesProjectController> _logger;


        private readonly IRolesProjectEmployeeWrapper _rolesProjectEmployeeWrapper;


        public EmployeeRolesProjectController(ILogger<EmployeeRolesProjectController> logger,
           IRolesProjectEmployeeWrapper rolesProjectEmployeeWrapper,

           Web.Utils.CookieManager cookieManager,

           IRolesMenuWrapper rolesMenuWrapper,
           GlobalController globalController
      



           ) : base(rolesMenuWrapper, cookieManager,globalController)
        {

            _logger = logger;
            _rolesProjectEmployeeWrapper = rolesProjectEmployeeWrapper;



            #region

            _cookieManager = cookieManager;
            _rolesMenuWrapper = rolesMenuWrapper;
            _globalController = globalController;


            #endregion


        }


        public ActionResult Index()
        {

            int ProjectId = _cookieManager.GetCookie("ProjectID") != null ? Convert.ToInt32(_cookieManager.GetCookie("ProjectID")) : 0;
            var data = _rolesProjectEmployeeWrapper.GetAllData(ProjectId);
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Rolesprojectemployee model)
        {
            _rolesProjectEmployeeWrapper.CreateData(model);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var data = _rolesProjectEmployeeWrapper.GetDataById(id);
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(Rolesprojectemployee model)
        {
            _rolesProjectEmployeeWrapper.Update(model);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var data = _rolesProjectEmployeeWrapper.GetDataById(id);
            return View(data);
        }

        public ActionResult Delete(int id)
        {
            var data = _rolesProjectEmployeeWrapper.GetDataById(id);
            return View(data);
        }

        [HttpPost]
        public ActionResult Delete(Rolesprojectemployee model)
        {
            _rolesProjectEmployeeWrapper.Delete(model);
            return RedirectToAction("Index");
        }
    }
}
