using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SUPPORTMVC.BLL;
using SUPPORTMVC.BLL.Results;
using SUPPORTMVC.ENTITIES;
using SUPPORTMVC.ENTITIES.DBT;
using SUPPORTMVC.WEB.Filters;

namespace SUPPORTMVC.WEB.Controllers
{
    public class ProfileController : Controller
    {
        private UserManager um = new UserManager();
        [Auth]
        public ActionResult Index()
        {
            return View();
        }
        [Auth]
        public ActionResult ProfilePage()
        {
           if(Session["User"] != null)
            { 
            Users currentUser = Session["User"] as Users;
            ErrorsResults<Users> res = um.GetUserById(currentUser.UserID);

            if (res.Errors.Count > 0)
            {
                res.Errors.ForEach(x => ModelState.AddModelError("", x));
                return View();
            }
            return View(res.Result);
            }
           return View();
        }
        [Auth]
        [HttpPost]
        public ActionResult ProfilePage(Users model, HttpPostedFileBase ProfileImageFile)
        {
            if (ModelState.IsValid)
            {

                if (ProfileImageFile != null && (ProfileImageFile.ContentType == "image/jpg" ||
                                                 ProfileImageFile.ContentType == "image/jpeg" ||
                                                 ProfileImageFile.ContentType == "image/png"))
                {
                    string filename = $"user_{ model.UserID}.{ ProfileImageFile.ContentType.Split('/')[1]}";
                    ProfileImageFile.SaveAs(Server.MapPath($"~/Content/assets/img/user_img/{filename}"));
                    model.ProfileImage = filename;
                }
                
                ErrorsResults<Users> er = um.UpdateProfile(model);

                if (er.Errors.Count > 0)
                {
                    er.Errors.ForEach(x => ModelState.AddModelError("", x));
                    return View(model);
                }


                Session["User"] = er.Result;

                return RedirectToAction("ProfilePage");
            }

            return View(model);
        }
        [Auth]
        public ActionResult ProfileUpdateOK()
        {
            return View("ProfilePage");
        }

    }
}