using Microsoft.AspNetCore.Mvc;
using Application.Wrapper.RolesMenu;
using Core.Dto.PMDb;
using Application.Wrapper.Severity;

namespace Web.Controllers
{
    public class SeverityController : SidebarMenuController
    {
        #region

        private readonly Web.Utils.CookieManager _cookieManager;
        private readonly IRolesMenuWrapper _rolesMenuWrapper;
        private readonly GlobalController _globalController;




        #endregion

        private readonly ILogger<CategoryController> _logger;
        private readonly ISeverityWrapper _severityWrapper;


        public SeverityController(ILogger<CategoryController> logger,
           ISeverityWrapper severityWrapper,

           Web.Utils.CookieManager cookieManager,
           IRolesMenuWrapper rolesMenuWrapper,
           GlobalController globalController

           ) : base(rolesMenuWrapper, cookieManager,globalController)
        {

            _logger = logger;
            _severityWrapper = severityWrapper;



            #region

            _cookieManager = cookieManager;
            _rolesMenuWrapper = rolesMenuWrapper;
            _globalController = globalController;   


            #endregion


        }
        public ActionResult Index()
        {
            var data = _severityWrapper.GetAllData();
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Severity model)
        {
            _severityWrapper.CreateData(model);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var data = _severityWrapper.GetDataById(id);
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(Severity model)
        {
            _severityWrapper.Update(model);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var data = _severityWrapper.GetDataById(id);
            return View(data);
        }

        public ActionResult Delete(int id)
        {
            var data = _severityWrapper.GetDataById(id);
            return View(data);
        }

        [HttpPost]
        public ActionResult Delete(Severity model)
        {
            _severityWrapper.Delete(model);
            return RedirectToAction("Index");
        }
    }
}
