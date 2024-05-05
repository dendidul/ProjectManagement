using Application.Wrapper.RolesMenu;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Utils
{
    public class CMSAuthorize : ActionFilterAttribute
    {

        private readonly Web.Utils.CookieManager _cookieManager;
        private readonly IRolesMenuWrapper _rolesMenuWrapper;

        public CMSAuthorize(Web.Utils.CookieManager cookieManager, IRolesMenuWrapper rolesMenuWrapper)
        {
            _cookieManager = cookieManager;
            _rolesMenuWrapper = rolesMenuWrapper;
        }

        public List<string> ControllerList
        {
            get
            {
                List<string> listItems = new List<string>();
                listItems.Add("Menu");
                listItems.Add("Home");
                listItems.Add("Profile");
                listItems.Add("Global");

                return listItems;
            }
        }

        public override void OnActionExecuting(ActionExecutingContext ctx)
        {
            //base.OnActionExecuting(ctx);
            //ValidateUserRole(ctx);
            ////ViewBagItem(ctx);


            if (_cookieManager.GetCookie("Role_ID") != null)
            {
                var session_role_id = _cookieManager.GetCookie("Role_ID").ToString();
                if (string.IsNullOrEmpty(session_role_id))
                {
                    //ctx.Result = new RedirectResult(Url.Action("Login", "Login"));
                    ctx.Result = new RedirectResult("Login/Login");
                    // return RedirectToAction("Login", "Login");
                }

                var role_id = Convert.ToInt32(session_role_id);

                //  var URL = ctx.HttpContext.Request.GetDisplayUrl();
                var fullUrl = ctx.HttpContext.Request.Path.Value;

                //  var method = ctx.HttpContext.Request.;
                var relativeURL = ctx.HttpContext.Request.Path.Value;

                var questionMarkIndex = fullUrl.IndexOf('?');
                string queryString = null;
                string url = fullUrl;
                if (questionMarkIndex != -1) // There is a QueryString
                {
                    url = fullUrl.Substring(0, questionMarkIndex);
                    queryString = fullUrl.Substring(questionMarkIndex + 1);
                }

                //var request = new HttpRequest(null, url, queryString);
                //var response = new HttpResponse(new StringWriter());
                //var httpContext = new HttpContext(request, response);
                // var routeData = ControllerContext.ActionDescriptor.ControllerName; ;

                string controller = ctx.HttpContext.Request.RouteValues["controller"].ToString();

                var routeData = ctx.HttpContext.Request.RouteValues["controller"].ToString();

                // var controllerName = routeData.Values["controller"] == null ? null : routeData.Values["controller"].ToString();

                var AccessingController = ControllerList.ToList();



                var count = AccessingController.Where(x => x.ToLower() == routeData.ToString().ToLower()).Count();

                if (count < 1)
                {
                    var dataCount = _rolesMenuWrapper.CheckMenuForRoles(role_id, routeData);
                    if (dataCount == false)
                    {
                        // ctx.Result = new RedirectResult(Url.Action("Index", "NoAccess"));
                        //return RedirectToAction("Index", "NoAccess");
                        ctx.Result = new RedirectResult("NoAccess/Index");
                    }
                    else
                    {
                        //  return null;
                    }
                }
                else
                {
                    // return null;
                }

            }
            else
            {
                // return RedirectToAction("Login", "Login");
                ctx.Result = new RedirectResult("Login/Login");
            }///

        }

       
    }
}
