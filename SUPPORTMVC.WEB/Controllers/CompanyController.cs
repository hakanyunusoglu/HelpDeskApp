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
using SUPPORTMVC.ENTITIES.DBTO;
using SUPPORTMVC.WEB.Filters;

namespace SUPPORTMVC.WEB.Controllers
{
    public class CompanyController : Controller
    {
        private RequestManager request_manager = new RequestManager();
        private CompanyManager cm = new CompanyManager();
        [Auth]
        public ActionResult Index()
        {
            return View();
        }
        [Auth]
        public ActionResult CompanyReg()
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
        public ActionResult CompanyReg(CompanyAddModel model)
        {
            if (ModelState.IsValid)
            {
                CompanyManager cm = new CompanyManager();
                ErrorsResults<Companies> er = cm.RegisterCompany(model);

                if (er.Errors.Count > 0)
                {
                    er.Errors.ForEach(x => ModelState.AddModelError("", x));
                    return View(model);
                }

                return RedirectToAction("CompanyRegOK");
            }
            return View(model);
        }
        [Auth]
        public ActionResult CompanyRegOK()
        {
            return View("CompanyReg");
        }

        [Auth]
        public ActionResult CompanyList()
        {
            int userole = 0;
            int loggeduser = 0;
            if (Session["User"] != null)
            {
                loggeduser = App.Common.GetUserID().Value;
            }
            foreach (Users usr in request_manager.GetReqUser())
            {
                if (usr.UserID == loggeduser)
                {
                    userole = usr.RoleID;
                }
            }
            if (userole >= 3)
            {
                return View(request_manager.GetReqComp().OrderByDescending(x => x.CompanyID));
            }
            return RedirectToAction("Index", "Index");
        }
        [Auth]
        public ActionResult CompanyDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int userole = 0;
            Companies companyListFound = request_manager.GetReqComp().Find(x => x.CompanyID == id);
            int loggeduser = 0;
            if (Session["User"] != null)
            {
                loggeduser = App.Common.GetUserID().Value;
            }

            foreach (Users usr in request_manager.GetReqUser())
            {
                if (usr.UserID == loggeduser)
                {
                    userole = usr.RoleID;
                }
            }
            if (userole >= 3)
            {
                if (companyListFound == null)
                {
                    return HttpNotFound();
                }
                return View(companyListFound);
            }
            return RedirectToAction("Index", "Index");
        }

        [Auth]
        public ActionResult ShowCompanyEditModal(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Companies usList = request_manager.GetReqComp().Find(x => x.CompanyID == id);
            if (usList == null)
            {
                return HttpNotFound();
            }

            return PartialView("_CompanyEdit", usList);
        }
        [Auth]
        [HttpPost]
        public ActionResult EditCompany(int? id, CompanyAddModel data)
        {
           data.CompanyID = id.Value;
            if (String.IsNullOrEmpty(data.CompanyCode))
            {
                return Json(new HttpStatusCodeResult(HttpStatusCode.BadRequest));
            }
            if (String.IsNullOrEmpty(data.CompanyName))
            {
                return Json(new HttpStatusCodeResult(HttpStatusCode.BadRequest));
            }
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return Json(new HttpStatusCodeResult(HttpStatusCode.BadRequest));
                }

                ErrorsResults<Companies> usedit = cm.EditCompany(data);

                if (usedit.Errors.Count > 0)
                {
                    usedit.Errors.ForEach(x => ModelState.AddModelError("", x));
                    return Json(new { result = false }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }
    }
}