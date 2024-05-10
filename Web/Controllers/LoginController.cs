using Application.Wrapper.Employee;
using Application.Wrapper.Users;
using Core.Dto.PMDb;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class LoginController : Controller
    {

        private readonly IUsersWrapper _usersWrapper;
        private readonly Web.Utils.CookieManager _cookieManager;
        private readonly IEmployeeWrapper _employeeWrapper;


        public LoginController(IUsersWrapper usersWrapper, Web.Utils.CookieManager cookieManager, IEmployeeWrapper employeeWrapper)
        {
            _usersWrapper = usersWrapper;
            _cookieManager = cookieManager;
            _employeeWrapper = employeeWrapper;
        }


        public IActionResult Logout()
        {
            _cookieManager.RemoveCookie("EmployeeId");
            _cookieManager.RemoveCookie("ProjectID");
            _cookieManager.RemoveCookie("ImageProfile");
            _cookieManager.RemoveCookie("Role_ID");
            _cookieManager.RemoveCookie("Employee");

            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            _cookieManager.RemoveCookie("EmployeeId");
            _cookieManager.RemoveCookie("ProjectID");
            _cookieManager.RemoveCookie("ImageProfile");
            _cookieManager.RemoveCookie("Role_ID");
            _cookieManager.RemoveCookie("Employee");

            return View();
        }

        public IActionResult Test()
        {
            _cookieManager.RemoveCookie("EmployeeId");
            _cookieManager.RemoveCookie("ProjectID");
            _cookieManager.RemoveCookie("ImageProfile");
            _cookieManager.RemoveCookie("Role_ID");
            _cookieManager.RemoveCookie("Employee");

            return View();
        }


        [HttpPost]

        public ActionResult Login(User model)
        {
            
            

            var data = _usersWrapper.CheckValidateUser(model.Username, model.Password);
            string Message = "";

            if (data == true)
            {
                Message = "Valid";
                var getRolesId = _usersWrapper.GetUser(model.Username, model.Password) != null ? _usersWrapper.GetUser(model.Username, model.Password).Rolesid : 0;
                if (getRolesId == 1)
                {
                    _cookieManager.SetCookie("EmployeeId", "100");
                    _cookieManager.SetCookie("ProjectID", "100");
                    _cookieManager.SetCookie("ImageProfile", "/Images/photo.jpg");
                  
                    //Session["EmployeeId"] = 100;
                    //Session["ProjectID"] = 100;
                    //Session["ImageProfile"] = "/Images/photo.jpg";
                }
                else
                {
                    var getEmployeeId = _usersWrapper.GetUser(model.Username, model.Password) != null ? (_usersWrapper.GetUser(model.Username, model.Password).Employeeid).ToString() : "0";

                    var imageProfile = _employeeWrapper.GetDataById((Convert.ToInt32(
                    getEmployeeId))).Photourl != null ? _employeeWrapper.GetDataById((Convert.ToInt32(
                    getEmployeeId))).Photourl : ""
                    ;



                    _cookieManager.SetCookie("EmployeeId", getEmployeeId);
                    //_cookieManager.SetCookie("ProjectID", "0");
                    _cookieManager.SetCookie("ProjectID", "1008");

                    _cookieManager.SetCookie("ImageProfile", imageProfile);
                  
                    //Session["EmployeeId"] = UserLogic.GetUser(model.UserName, model.Password) != null ? UserLogic.GetUser(model.UserName, model.Password).EmployeeId : 0;
                    //Session["ProjectID"] = 0;
                    //Session["ImageProfile"] = EmployeeLogic.GetDataById((Convert.ToInt32(Session["EmployeeId"].ToString()))).PhotoUrl;
                }

                //Session["Role_ID"] = getRolesId;
                //Session["Employee"] = model.UserName;

                //_cookieManager.SetCookie("ProjectName","Select Project");

                _cookieManager.SetCookie("ProjectName", "FAS");

                _cookieManager.SetCookie("Role_ID", getRolesId.Value.ToString());

                _cookieManager.SetCookie("Employee", model.Username);
                return Json(new { isValid = Message });

                //  return RedirectToAction("Dashboard", "Home");
            }
            else
            {
                Message = "Not Valid";
                return Json(new { isValid = Message });
                //return RedirectToAction("Login");
            }
        }


    }
}
