using Microsoft.AspNetCore.Mvc;
using Application.Wrapper.RolesMenu;
using Core.Dto.PMDb;
using Application.Wrapper.Position;

namespace Web.Controllers
{
    public class PositionController : SidebarMenuController
    {
        #region

        private readonly Web.Utils.CookieManager _cookieManager;
        private readonly IRolesMenuWrapper _rolesMenuWrapper;


        #endregion

        private readonly ILogger<PositionController> _logger;
        private readonly IPositionWrapper _positionWrapper;

        public PositionController(ILogger<PositionController> logger,
           IPositionWrapper positionWrapper,

           Web.Utils.CookieManager cookieManager,
           IRolesMenuWrapper rolesMenuWrapper

           ) : base(rolesMenuWrapper, cookieManager)
        {

            _logger = logger;
            _positionWrapper = positionWrapper;


            #region

            _cookieManager = cookieManager;
            _rolesMenuWrapper = rolesMenuWrapper;


            #endregion


        }

        public ActionResult Index()
        {
            var data = _positionWrapper.GetAllData();
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Position model)
        {
            _positionWrapper.CreateData(model);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var data = _positionWrapper.GetDataById(id);
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(Position model)
        {
            _positionWrapper.Update(model);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var data = _positionWrapper.GetDataById(id);
            return View(data);
        }

        public ActionResult Delete(int id)
        {
            var data = _positionWrapper.GetDataById(id);
            return View(data);
        }

        [HttpPost]
        public ActionResult Delete(Position model)
        {
            _positionWrapper.Delete(model);
            return RedirectToAction("Index");
        }

    }
}
