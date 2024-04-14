using Microsoft.AspNetCore.Mvc;
using Application.Wrapper.RolesMenu;
using Core.Dto.PMDb;
using Application.Wrapper.Status;

namespace Web.Controllers
{
    public class StatusController : SidebarMenuController
    {

        #region

        private readonly Web.Utils.CookieManager _cookieManager;
        private readonly IRolesMenuWrapper _rolesMenuWrapper;


        #endregion

        private readonly ILogger<StatusController> _logger;
        private readonly IStatusWrapper _statusWrapper;

        public StatusController(ILogger<StatusController> logger,
           IStatusWrapper statusWrapper,

           Web.Utils.CookieManager cookieManager,
           IRolesMenuWrapper rolesMenuWrapper

           ) : base(rolesMenuWrapper, cookieManager)
        {

            _logger = logger;
            _statusWrapper = statusWrapper;



            #region

            _cookieManager = cookieManager;
            _rolesMenuWrapper = rolesMenuWrapper;


            #endregion


        }

        public ActionResult Index()
        {
            var data = _statusWrapper.GetAllData();
            return View(data);
        }


        public ActionResult CreateForm()
        {
            Status modeldata = new Status();
            return PartialView("Create", modeldata);
        }


        [HttpPost]
        public ActionResult Create(Status model)
        {
            _statusWrapper.CreateData(model);
            return RedirectToAction("Index");
        }

        public ActionResult EditForm(int id)
        {
            var data = _statusWrapper.GetDataById(id);
            return View(data);
            //  return PartialView("Create", modeldata);
        }

        [HttpPost]
        public ActionResult Edit(Status model)
        {
            _statusWrapper.Update(model);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var data = _statusWrapper.GetDataById(id);
            return View(data);
        }

        public ActionResult Delete(int id)
        {
            var data = _statusWrapper.GetDataById(id);
            return View(data);
        }

        [HttpPost]
        public ActionResult Delete(Status model)
        {
            _statusWrapper.Delete(model);
            return RedirectToAction("Index");
        }
    }
}
