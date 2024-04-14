using Application.Wrapper.Category;
using Application.Wrapper.RolesMenu;
using Core.Dto.PMDb;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class CategoryController : SidebarMenuController
    {

        #region

        private readonly Web.Utils.CookieManager _cookieManager;
        private readonly IRolesMenuWrapper _rolesMenuWrapper;


        #endregion

        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryWrapper _categoryWrapper;

        public CategoryController(ILogger<CategoryController> logger,
           ICategoryWrapper categoryWrapper,

           Web.Utils.CookieManager cookieManager,

           IRolesMenuWrapper rolesMenuWrapper



           ) : base(rolesMenuWrapper, cookieManager)
        {

            _logger = logger;
            _categoryWrapper = categoryWrapper;



            #region

            _cookieManager = cookieManager;
            _rolesMenuWrapper = rolesMenuWrapper;


            #endregion


        }



        public IActionResult Index()
        {
            var data = _categoryWrapper.GetAllData();
            return View(data);
        }

        [HttpPost]
        public ActionResult Create(Category model)
        {
            _categoryWrapper.CreateData(model);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var data = _categoryWrapper.GetDataById(id);
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(Category model)
        {
            _categoryWrapper.Update(model);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var data = _categoryWrapper.GetDataById(id);
            return View(data);
        }

        public ActionResult Delete(int id)
        {
            var data = _categoryWrapper.GetDataById(id);
            return View(data);
        }

        [HttpPost]
        public ActionResult Delete(Category model)
        {
            _categoryWrapper.Delete(model);
            return RedirectToAction("Index");
        }



    }
}
