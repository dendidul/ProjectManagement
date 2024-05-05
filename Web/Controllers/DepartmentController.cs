using Application.Wrapper.Department;
using Application.Wrapper.RolesMenu;
using Microsoft.AspNetCore.Mvc;
using Core.Dto.PMDb;

namespace Web.Controllers
{
    public class DepartmentController : SidebarMenuController
    {

        #region

        private readonly Web.Utils.CookieManager _cookieManager;
        private readonly IRolesMenuWrapper _rolesMenuWrapper;
        private readonly GlobalController _globalController;



        #endregion

        private readonly ILogger<DepartmentController> _logger;
        private readonly IDepartmentWrapper _departmentWrapper;

        public DepartmentController(ILogger<DepartmentController> logger,
           IDepartmentWrapper departmentWrapper,

           Web.Utils.CookieManager cookieManager,
           IRolesMenuWrapper rolesMenuWrapper,
           GlobalController globalController
      

           ) : base(rolesMenuWrapper, cookieManager,globalController)
        {

            _logger = logger;
            _departmentWrapper = departmentWrapper;



            #region

            _cookieManager = cookieManager;
            _rolesMenuWrapper = rolesMenuWrapper;
            _globalController = globalController;


            #endregion


        }

        public ActionResult Index()
        {
            var data = _departmentWrapper.GetAllData();
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Department model)
        {
            _departmentWrapper.CreateData(model);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var data = _departmentWrapper.GetDataById(id);
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(Department model)
        {
            _departmentWrapper.Update(model);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var data = _departmentWrapper.GetDataById(id);
            return View(data);
        }

        public ActionResult Delete(int id)
        {
            var data = _departmentWrapper.GetDataById(id);
            return View(data);
        }

        [HttpPost]
        public ActionResult Delete(Department model)
        {
            _departmentWrapper.Delete(model);
            return RedirectToAction("Index");
        }
    }
}
