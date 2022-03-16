using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SUPPORTMVC.BLL;
using SUPPORTMVC.BLL.Results;
using SUPPORTMVC.COMMON;
using SUPPORTMVC.ENTITIES.DBT;
using SUPPORTMVC.WEB.Filters;

namespace SUPPORTMVC.WEB.Controllers
{
    public class QuestListController : Controller
    {
        private RequestManager request_manager = new RequestManager();
        [Auth]
        public ActionResult Index()
        {
            return View();
        }
        [Auth]
        public ActionResult QuestList()
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
                        if (userole < 3)
                        {
                            return RedirectToAction("Index", "Index");
                        }
                    }
                }
            }
            return View(request_manager.GetAssignedAttendants().Where(x => x.UserID == loggeduser).OrderByDescending(x => x.RequestID));
        }
        [Auth]
        public ActionResult ActiveQuestList()
        {
            int loggeduser = 0;
            if(Session["User"] != null)
            { 
            loggeduser = App.Common.GetUserID().Value;
            int userole;
            foreach (Users usr in request_manager.GetReqUser())
            {
                if (usr.UserID == loggeduser)
                {
                    userole = usr.RoleID;
                    if (userole < 3)
                    {
                        return RedirectToAction("Index", "Index");
                    }
                }
            }
            }
            return View(request_manager.GetRequestList().Where(x=>x.RequestAttendant == loggeduser).OrderByDescending(x=>x.RequestID));
        }

        [Auth]
        [HttpPost]
        public JsonResult AssignmentAttendant(int? id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return Json(new HttpStatusCodeResult(HttpStatusCode.BadRequest));
                }

                ErrorsResults<AttendantAssignment> erc = request_manager.NewAssignmentAttendant(id);

                if (erc.Errors.Count > 0)
                {
                    erc.Errors.ForEach(x => ModelState.AddModelError("", x));
                    return Json(new { result = false }, JsonRequestBehavior.AllowGet);

                }

                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }

        [Auth]
        [HttpPost]
        public JsonResult AssignmentAttendantCancel(int? id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return Json(new HttpStatusCodeResult(HttpStatusCode.BadRequest));
                }

                ErrorsResults<AttendantAssignment> erc = request_manager.CancelAssignmentAttendant(id);

                if (erc.Errors.Count > 0)
                {
                    erc.Errors.ForEach(x => ModelState.AddModelError("", x));
                    return Json(new { result = false }, JsonRequestBehavior.AllowGet);

                }
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }
    }
}