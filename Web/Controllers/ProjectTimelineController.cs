using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class ProjectTimelineController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
