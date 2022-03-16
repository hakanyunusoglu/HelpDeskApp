using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SUPPORTMVC.BLL;
using SUPPORTMVC.BLL.Results;
using SUPPORTMVC.COMMON.Helpers;
using SUPPORTMVC.ENTITIES.DBT;
using SUPPORTMVC.ENTITIES.DBTO;

namespace SUPPORTMVC.WEB.Controllers
{
    public class LoginController : Controller
    {
        UserManager um = new UserManager();
        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                ErrorsResults<Users> lu = um.LoginUser(model);

                if (lu.Errors.Count > 0)
                {
                    lu.Errors.ForEach(x => ModelState.AddModelError("", x));
                    return View(model);
                }
                FormsAuthentication.SetAuthCookie(lu.Result.Username, true);
                HttpCookie cookieUser = new HttpCookie("User", Convert.ToInt32(lu.Result.UserID).ToString());
                HttpContext.Response.Cookies.Add(cookieUser);

                Session["User"] = lu.Result;
                return RedirectToAction("Index", "Index");
            }
            return View(model);
        }
        public ActionResult Logout()
        {
            var c = new HttpCookie("User");
            c.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(c);
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }
        [HttpPost]
        public ActionResult ForgotPassword(LoginViewModel data)
        {
            ModelState.Remove("Username");
            ModelState.Remove("Password");

            List<Users> usrlist = um.GetUsers();

            if (ModelState.IsValid)
            {
                ErrorsResults<Users> lu = um.ForgotPassword(data);
                if (lu.Errors.Count > 0)
                {
                    return Json(new { result = false }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }
    }
}