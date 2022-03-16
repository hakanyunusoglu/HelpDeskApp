using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using SUPPORTMVC.BLL;
using SUPPORTMVC.BLL.Results;
using SUPPORTMVC.COMMON;
using SUPPORTMVC.ENTITIES.DBT;
using SUPPORTMVC.ENTITIES.DBTO;
using SUPPORTMVC.WEB.Filters;

namespace SUPPORTMVC.WEB.Controllers
{
    public class UserController : Controller  
    {
        private RequestManager request_manager = new RequestManager();
        private UserManager um = new UserManager();

        [Auth]
        public ActionResult Index()
        {
            return View();
        }
        [Auth]
        public ActionResult UserReg()
        {
            int loggeduser = 0;
            if (Session["User"] != null)
            {
                loggeduser = App.Common.GetUserID().Value;
                int userole;
                foreach (Users usr in request_manager.GetReqUser())
                {
                    if (usr.UserID == loggeduser)
                    {
                        userole = usr.RoleID;
                        if (userole < 4)
                        {
                            return RedirectToAction("Index", "Index");
                        }
                    }
                }
            }
            return View();
        }
        [Auth]
        [HttpPost]
        public ActionResult UserReg(UserAddModel model)
        { 
            if(ModelState.IsValid)
            {
                UserManager um = new UserManager();
                ErrorsResults<Users> er = um.RegisterUser(model);

                if (er.Errors.Count > 0)
                {
                    er.Errors.ForEach(x=> ModelState.AddModelError("",x));
                    return View(model);
                }

                return RedirectToAction("UserRegOK");
            }
            return View(model);
        }
        [Auth]
        public ActionResult UserRegOK()
        {
           
            return View("UserReg");
        }

        [Auth]
        public ActionResult UserList()
        {
            int userole = 0;
            int loggeduser = 0;
            if (Session["User"] != null)
            {
                loggeduser = App.Common.GetUserID().Value;
            }
            foreach (Users usr in um.GetUsers())
            {
                if (usr.UserID == loggeduser)
                {
                    userole = usr.RoleID;
                }
            }
            if (userole >= 3)
            {
                return View(request_manager.GetReqUser().OrderByDescending(x => x.UserID));
            }
            return RedirectToAction("Index", "Index");
            
        }

        [Auth]
        public ActionResult UserDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int userole = 0;
            Users userList = um.GetUsers().Find(x=>x.UserID == id);
            int loggeduser = 0;
            if (Session["User"] != null)
            {
                loggeduser = App.Common.GetUserID().Value;
            }

            foreach (Users usr in um.GetUsers())
            {
                if (usr.UserID == loggeduser)
                {
                    userole = usr.RoleID;
                }
            }
            if (userole >= 3)
            {
                if (userList == null)
                {
                    return HttpNotFound();
                }
                return View(userList);
            }
            return RedirectToAction("Index", "Index");
        }

        [Auth]
        public ActionResult ShowUserEditModal(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Users usList = um.GetUsers().Find(x => x.UserID == id);
            if (usList == null)
            {
                return HttpNotFound();
            }

            return PartialView("_UserEdit", usList);
        }

        [Auth]
        [HttpPost]
        public ActionResult EditUser(int? id, UserAddModel data)
        {
            data.UserID = id.Value;
            if (String.IsNullOrEmpty(data.UName))
            {
                return Json(new HttpStatusCodeResult(HttpStatusCode.BadRequest));
            }
            if (String.IsNullOrEmpty(data.USurname))
            {
                return Json(new HttpStatusCodeResult(HttpStatusCode.BadRequest));
            }
            if (String.IsNullOrEmpty(data.Email))
            {
                return Json(new HttpStatusCodeResult(HttpStatusCode.BadRequest));
            }

            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return Json(new HttpStatusCodeResult(HttpStatusCode.BadRequest));
                }

                //ErrorsResults<Requests> er = request_manager.CloseRequest(data);
                ErrorsResults<Users> usedit = um.EditUser(data);

                if (usedit.Errors.Count > 0)
                {
                    usedit.Errors.ForEach(x => ModelState.AddModelError("", x));
                    return Json(new { result = false }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }

        [Auth]
        [HttpPost]
        public ActionResult QuickReqUser(UserAddModel data)
        {
            if (ModelState.IsValid)
            {
                UserManager um = new UserManager();
                ErrorsResults<Users> er = um.QuickNewUserAdd(data);

                if (er.Errors.Count > 0)
                {
                    er.Errors.ForEach(x => ModelState.AddModelError("", x));
                    return Json(new { result = false }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }
    }
}