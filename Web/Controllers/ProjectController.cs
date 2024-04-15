using Microsoft.AspNetCore.Mvc;
using Core.Dto.PMDb;
using Application.Wrapper.RolesMenu;
using Application.Wrapper.Project;

namespace Web.Controllers
{
    public class ProjectController : SidebarMenuController
    {

        #region

        private readonly Web.Utils.CookieManager _cookieManager;
        private readonly IRolesMenuWrapper _rolesMenuWrapper;
        private readonly GlobalController _globalController;



        #endregion

        private readonly ILogger<ProjectController> _logger;
        private readonly IProjectWrapper _projectWrapper;

        public ProjectController(ILogger<ProjectController> logger,
           IProjectWrapper projectWrapper,

           Web.Utils.CookieManager cookieManager,
           IRolesMenuWrapper rolesMenuWrapper,
           GlobalController globalController

           ) : base(rolesMenuWrapper, cookieManager,globalController)
        {

            _logger = logger;
            _projectWrapper = projectWrapper;


            #region

            _cookieManager = cookieManager;
            _rolesMenuWrapper = rolesMenuWrapper;
            _globalController = globalController;   


            #endregion


        }

        public ActionResult Index()
        {
            var data = _projectWrapper.GetAllData();
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Project model, string IsPublic)
        {
            model.Ispublic = IsPublic != null ? true : false;
            _projectWrapper.CreateData(model);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var data = _projectWrapper.GetDataById(id);
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(Project model, string IsPublic)
        {
            model.Ispublic = IsPublic != null ? true : false;
            _projectWrapper.Update(model);
            return RedirectToAction("Index");
        }


        public ActionResult Details(int id)
        {
            var data = _projectWrapper.GetDataById(id);
            return View(data);
        }

        public ActionResult Delete(int id)
        {
            var data = _projectWrapper.GetDataById(id);
            return View(data);
        }

        [HttpPost]
        public ActionResult Delete(Project model)
        {
            _projectWrapper.Delete(model);
            return RedirectToAction("Index");
        }
    }
}
