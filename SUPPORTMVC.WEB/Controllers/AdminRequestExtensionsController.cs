using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SUPPORTMVC.BLL;
using SUPPORTMVC.BLL.Results;
using SUPPORTMVC.COMMON;
using SUPPORTMVC.ENTITIES.DBT;
using SUPPORTMVC.ENTITIES.DBTO;
using SUPPORTMVC.WEB.Filters;

namespace SUPPORTMVC.WEB.Controllers
{
    public class AdminRequestExtensionsController : Controller
    {
        RequestManager rm = new RequestManager();
        [Auth]
        public ActionResult AddStatus()
        {
            int loggeduser = 0;
            if (Session["User"] != null)
            {
                loggeduser = App.Common.GetUserID().Value;
                int userole;
                foreach (Users usr in rm.GetReqUser())
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
            return View();
        }
        [Auth]
        [HttpPost]
        public ActionResult AddStatus( RequestStatus model)
        {
            if (ModelState.IsValid)
            {
                RequestStatus rs = new RequestStatus();
                if (model.StatusTitle != null)
                {
                    ErrorsResults<RequestStatus> er = rm.AddStatus(model);

                    if (er.Errors.Count > 0)
                    {
                        er.Errors.ForEach(x => ModelState.AddModelError("", x));
                        return View(model);
                    }

                    return View();

                }

            }
            return View(model);
        }
        [Auth]
        public ActionResult AddPriority()
        {
            int loggeduser = 0;
            if (Session["User"] != null)
            {
                loggeduser = App.Common.GetUserID().Value;
                int userole;
                foreach (Users usr in rm.GetReqUser())
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
            return View();
        }
        [Auth]
        [HttpPost]
        public ActionResult AddPriority(RequestPriority model)
        {
            if (ModelState.IsValid)
            {
                if (model.PriorityTitle != null)
                {
                    ErrorsResults<RequestPriority> er = rm.AddPriority(model);

                    if (er.Errors.Count > 0)
                    {
                        er.Errors.ForEach(x => ModelState.AddModelError("", x));
                        return View(model);
                    }

                    return View();

                }

            }
            return View(model);
        }
        [Auth]
        public ActionResult AddType()
        {
            int loggeduser = 0;
            if (Session["User"] != null)
            {
                loggeduser = App.Common.GetUserID().Value;
                int userole;
                foreach (Users usr in rm.GetReqUser())
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
            return View();
        }
        [Auth]
        [HttpPost]
        public ActionResult AddType(RequestType model)
        {
            if (ModelState.IsValid)
            {
                if (model.RequestTypeTitle != null)
                {
                    ErrorsResults<RequestType> er = rm.AddType(model);

                    if (er.Errors.Count > 0)
                    {
                        er.Errors.ForEach(x => ModelState.AddModelError("", x));
                        return View(model);
                    }

                    return View();

                }

            }
            return View(model);
        }
    }
}