using Application.Wrapper.Employee;
using Application.Wrapper.RolesMenu;
using Microsoft.AspNetCore.Mvc;
using Core.Dto.PMDb;

namespace Web.Controllers
{
    public class EmployeeController : SidebarMenuController
    {

        #region

        private readonly Web.Utils.CookieManager _cookieManager;
        private readonly IRolesMenuWrapper _rolesMenuWrapper;
        private readonly GlobalController _globalController;

        #endregion

        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeWrapper _employeeWrapper;

        public EmployeeController(ILogger<EmployeeController> logger,
           IEmployeeWrapper employeeWrapper,

           Web.Utils.CookieManager cookieManager,
           IRolesMenuWrapper rolesMenuWrapper,
            GlobalController globalController

           ) : base(rolesMenuWrapper, cookieManager, globalController)
        {

            _logger = logger;
            _employeeWrapper = employeeWrapper;


            #region

            _cookieManager = cookieManager;
            _rolesMenuWrapper = rolesMenuWrapper;
            _globalController = globalController;

            #endregion


        }

        public ActionResult Index()
        {
            var data = _employeeWrapper.GetAllData();
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Create(Employee model, string IsActive)
        {
            model.IsActive = IsActive != null ? 1 : 0;
            _employeeWrapper.CreateData(model);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var data = _employeeWrapper.GetDataById(id);
            return View(data);
        }

        [HttpPost]

        public ActionResult Edit(Employee model, string IsActive)
        {
            model.IsActive = IsActive != null ? 1 : 0;
            _employeeWrapper.Update(model);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var data = _employeeWrapper.GetDataById(id);
            return View(data);
        }

        public ActionResult Delete(int id)
        {
            var data = _employeeWrapper.GetDataById(id);
            return View(data);
        }

        [HttpPost]

        public ActionResult Delete(Employee model)
        {
            _employeeWrapper.Delete(model);
            return RedirectToAction("Index");
        }
    }
}
