using Microsoft.AspNetCore.Mvc;
using Application.Wrapper.RolesMenu;
using Core.Dto.PMDb;
using Application.Wrapper.Profile;
using Application.Wrapper.Employee;

namespace Web.Controllers
{
    public class ProfileController : SidebarMenuController
    {

        #region

        private readonly Web.Utils.CookieManager _cookieManager;
        private readonly IRolesMenuWrapper _rolesMenuWrapper;
        private readonly GlobalController _globalController;



        #endregion

        private readonly ILogger<CategoryController> _logger;
        private readonly IProfileWrapper _profileWrapper;
        private readonly IEmployeeWrapper _employeeWrapper;

        public ProfileController(ILogger<CategoryController> logger,
           IProfileWrapper profileWrapper, IEmployeeWrapper employeeWrapper,

           Web.Utils.CookieManager cookieManager,

           IRolesMenuWrapper rolesMenuWrapper,
           GlobalController globalController



           ) : base(rolesMenuWrapper, cookieManager,globalController)
        {

            _logger = logger;
            _profileWrapper = profileWrapper;
            _employeeWrapper = employeeWrapper;



            #region

            _cookieManager = cookieManager;
            _rolesMenuWrapper = rolesMenuWrapper;
            _globalController = globalController;


            #endregion


        }



        public ActionResult Index()
        {
            var EmployeeId = Convert.ToInt32(_cookieManager.GetCookie("EmployeeId"));
            var data = _profileWrapper.GetProfileByEmployeeId(EmployeeId);
            return View(data);
        }

        public ActionResult ViewProfile(int id)
        {
            //var EmployeeId = Convert.ToInt32(Session["EmployeeId"].ToString());
            var data = _profileWrapper.GetProfileByEmployeeId(id);
            return View(data);
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

    }
}
