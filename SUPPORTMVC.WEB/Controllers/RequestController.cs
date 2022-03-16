using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
    public class RequestController : Controller
    {
        RequestManager rm = new RequestManager();

        [Auth]
        public ActionResult Temizle()
        {
            return RedirectToAction("RequestReg", "Request");
        }
        [Auth]
        public ActionResult Index()
        {
            return View();
        }
        [Auth]
        public ActionResult RequestReg()
        {
           return View();
        }
        [Auth]
        [HttpPost]
        public ActionResult RequestReg(RequestAddModel data, RequestDocumentModel rdm)
        {
            
            if (String.IsNullOrEmpty(data.ReqStatus.ToString()) || data.ReqStatus == 0)
            {
                ModelState.Remove("ReqStatus");
            }
            if (String.IsNullOrEmpty(data.CommentTotalTime.ToString()) || data.CommentTotalTime == 0)
            {
                ModelState.Remove("CommentTotalTime");
            }
            if (String.IsNullOrEmpty(data.CommentText))
            {
                ModelState.Remove("CommentText");
            }
            if (String.IsNullOrEmpty(data.RequestDate) || data.RequestDate == "undefined")
            {
                ModelState.Remove("RequestDate");
            }
            if (String.IsNullOrEmpty(data.UserDatetime))
            {
                ModelState.Remove("UserDatetime");
            }
            ModelState.Remove("RequestNumber");
            if (String.IsNullOrEmpty(data.UserDatetime))
            {
                ModelState.Remove("UserDatetime");
            }

            //var errors = ModelState
            //    .Where(x => x.Value.Errors.Count > 0)
            //    .Select(x => new { x.Key, x.Value.Errors })
            //    .ToArray();

            if (ModelState.IsValid)
            {
                int loggeduser = App.Common.GetUserID().Value;
                List<Requests> reqlst = rm.GetRequestList();

                ErrorsResults<Requests> er = rm.AddRequest(data);

                HttpFileCollectionBase files = Request.Files;

                if (files != null && !String.IsNullOrEmpty(data.RequestDescription))
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        double lengthfile = file.ContentLength;

                        string filecontent = file.FileName.Split('.')[1];
                        foreach (Requests rq in reqlst)
                        {
                            if (rq.RequestID + 1 == data.RequestID)
                            {
                                string filename = $"{rq.RequestNumber}_{loggeduser}_{DateTime.Now.Date}.{filecontent}";

                                Directory.CreateDirectory(Server.MapPath($"~/Content/assets/img/RequestDocuments/RequestNumber_{rq.RequestNumber}/"));
                                string path = Server.MapPath($"~/Content/assets/img/RequestDocuments/RequestNumber_{rq.RequestNumber}/{filename}");

                                file.SaveAs(path);
                                rdm.docReq = data.RequestID;
                                rdm.DocumentTitle = file.FileName;
                                rdm.DocumentTitleChanged = filename;
                                rdm.docUser = loggeduser;
                                rdm.FileSize = lengthfile;
                            }
                        }
                        ErrorsResults<RequestDocuments> erd = rm.NewRequestDocument(rdm);
                    }
                }

                if (er.Errors.Count > 0)
                {
                    er.Errors.ForEach(x => ModelState.AddModelError("", x));
                    return View(data);
                }
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }
        [Auth]
        public JsonResult TakeRequestAttendantUser(int? id, TakeRequestSelectedModel data)
        {
            if (String.IsNullOrEmpty(data.TakeRequestSelectedValue.ToString()) || data.TakeRequestSelectedValue == 0)
            {
                return Json(new HttpStatusCodeResult(HttpStatusCode.BadRequest));
            }
            else
            {
                if (ModelState.IsValid)
                {
                    if (id == null)
                    {
                        return Json(new HttpStatusCodeResult(HttpStatusCode.BadRequest));
                    }

                    if (!String.IsNullOrEmpty(data.TakeRequestSelectedValue.ToString()))
                    {
                        data.ReqsID = id.Value;
                        ErrorsResults<Requests> er = rm.NewRequestAttendantUser(data);

                        if (er.Errors.Count > 0)
                        {
                            er.Errors.ForEach(x => ModelState.AddModelError("", x));
                            return Json(new {result = false}, JsonRequestBehavior.AllowGet);
                        }

                        return Json(new {result = true}, JsonRequestBehavior.AllowGet);
                    }

                    return Json(new {result = false}, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }

        [Auth]
        public ActionResult ShowQuickNewUserModal()
        {
            return PartialView("_QuickNewUser");
        }
    }
}