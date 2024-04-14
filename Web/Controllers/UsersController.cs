using Application.Wrapper.RolesMenu;
using Application.Wrapper.RolesProjectEmployee;
using Application.Wrapper.Users;
using Core.Dto.PMDb;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class UsersController : SidebarMenuController
    {


        #region

        private readonly Web.Utils.CookieManager _cookieManager;
        private readonly IRolesMenuWrapper _rolesMenuWrapper;


        #endregion


        private readonly ILogger<HomeController> _logger;
        private readonly IUsersWrapper _usersWrapper;
        private readonly IRolesProjectEmployeeWrapper _rolesProjectEmployeeWrapper;
       

        public UsersController(ILogger<HomeController> logger,


           Web.Utils.CookieManager cookieManager,

           IRolesMenuWrapper rolesMenuWrapper,
           IUsersWrapper usersWrapper,
            IRolesProjectEmployeeWrapper rolesProjectEmployeeWrapper




           ) : base(rolesMenuWrapper, cookieManager)
        {

            _logger = logger;



            #region

            _cookieManager = cookieManager;
            _rolesMenuWrapper = rolesMenuWrapper;


            #endregion

            _usersWrapper = usersWrapper;
            _rolesProjectEmployeeWrapper = rolesProjectEmployeeWrapper;
        }


        public ActionResult Index()
        {
            var data = _usersWrapper.GetAllData();
            return View(data);
        }
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(User model)
        {
            _usersWrapper.CreateData(model);
            return RedirectToAction("Index");
        }


        public ActionResult Edit(int id)
        {
            var data = _usersWrapper.GetDataById(id);
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(User model)
        {
            _usersWrapper.Update(model);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var data = _usersWrapper.GetDataById(id);
            return View(data);
        }

        public ActionResult Delete(int id)
        {
            var data = _usersWrapper.GetDataById(id);
            return View(data);
        }

        [HttpPost]
        public ActionResult Delete(User model)
        {
            _usersWrapper.Delete(model);
            return RedirectToAction("Index");
        }


        public ActionResult ProjectUsers()
        {
            int ProjectId = _cookieManager.GetCookie("ProjectID") != null ? Convert.ToInt32(_cookieManager.GetCookie("ProjectID")) : 0;

            var data = _rolesProjectEmployeeWrapper.GetEmployeeByProject(ProjectId).ToList();
            return View(data);
        }
    }
}
