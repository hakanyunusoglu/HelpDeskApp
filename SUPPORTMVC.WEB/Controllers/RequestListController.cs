using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using SUPPORTMVC.BLL;
using SUPPORTMVC.BLL.Results;
using SUPPORTMVC.COMMON;
using SUPPORTMVC.ENTITIES.DBT;
using SUPPORTMVC.ENTITIES.DBTO;
using SUPPORTMVC.WEB.Filters;

namespace SUPPORTMVC.WEB.Controllers
{

    public class RequestListController : Controller
    {
        private RequestManager request_manager = new RequestManager();

        [Auth]
        public ActionResult RequestList()
        {
            return View(request_manager.GetRequestList().OrderByDescending(x => x.RequestID));
        }

        [Auth]
        public ActionResult ClosedRequestList()
        {
            int loggeduser = 0;
            if (Session["User"] != null)
            {
                loggeduser = App.Common.GetUserID().Value;
            }
            return View(request_manager.GetRequestList().OrderByDescending(x => x.RequestDate));
        }
        [Auth]
        public ActionResult RequestJsonList()
        {
            var data = request_manager.GetRequestList().OrderByDescending(x => x.RequestDate).ToList();
            return Json(new {data = data}, JsonRequestBehavior.AllowGet);
        }

        [Auth]
        public JsonResult RequestListele()
        {
            var obj = new {data = request_manager.GetRequestList().OrderByDescending(x => x.RequestID)};
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        [Auth]
        public ActionResult RequestDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int userole = 0;
            Requests reqList = request_manager.Find(x => x.RequestID == id.Value);
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
            if((reqList.RequestAttendant == 2 && userole >= 3 ) || (reqList.reqUser.UserID == loggeduser || reqList.RequestAttendant == loggeduser || userole >= 4))
            { 
            if (reqList == null)
            {
                return HttpNotFound();
            }
            return View(reqList);
            }
            return RedirectToAction("Index", "Index");
        }

        [Auth]
        [HttpPost]
        public ActionResult Edit(int? id, NewCommentModel data)
        {
            data.comReq = id.Value;
            if (String.IsNullOrEmpty(data.CommentTotalTime.ToString()) || data.CommentTotalTime == 0)
            {
                ModelState.Remove("CommentTotalTime");
            }
            if (String.IsNullOrEmpty(data.CommentText))
            {
                ModelState.Remove("CommentText");
            }
            if (String.IsNullOrEmpty(data.reqPriority.ToString()) || data.reqPriority == 0)
            {
                ModelState.Remove("reqPriority");
            }
            if (String.IsNullOrEmpty(data.ReqStatus.ToString()) || data.ReqStatus == 0)
            {
                ModelState.Remove("ReqStatus");
            }

            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return Json(new HttpStatusCodeResult(HttpStatusCode.BadRequest));
                }

                //ErrorsResults<Requests> er = request_manager.CloseRequest(data);
                ErrorsResults<RequestComments> erc = request_manager.CloseRequestComment(data);

                if (erc.Errors.Count > 0)
                {
                    erc.Errors.ForEach(x => ModelState.AddModelError("", x));
                    return Json(new {result = false}, JsonRequestBehavior.AllowGet);

                }

                return Json(new {result = true}, JsonRequestBehavior.AllowGet);
            }

            return Json(new {result = false}, JsonRequestBehavior.AllowGet);
        }

        [Auth]
        public ActionResult ShowRequestCloseModal(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Requests reqList = request_manager.Find(x => x.RequestID == id);
            if (reqList == null)
            {
                return HttpNotFound();
            }

            return PartialView("_RequestClose", reqList);
        }

        [Auth]
        public ActionResult ShowRequestCommentModal(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return PartialView("_RequestComment");
        }

        [Auth]
        public JsonResult FilterOwner(string id)
        {
            int loggeduser = App.Common.GetUserID().Value;
            int userole;
            int compid;
            List<Companies> complist = request_manager.GetReqComp();
            List<Users> userlist = request_manager.GetReqUser();
            List<SelectListItem> filterList = new List<SelectListItem>();

            if (!String.IsNullOrEmpty(id) && id != Convert.ToInt32(0).ToString())
            {
                foreach (Users usr in userlist)
                {
                    if (usr.UserID == loggeduser)
                    {
                        userole = usr.RoleID;
                        compid = usr.CompanyID;
                        string loggedname = usr.UName + " " + usr.USurname;
                        int loggedval = usr.UserID;

                        if (userole == 1)
                        {
                            if (compid == Convert.ToInt32(id) && userole == 1)
                            {
                                filterList.Add(new SelectListItem {Text = loggedname, Value = loggedval.ToString()});
                            }
                        }
                        else if (userole == 2)
                        {
                            foreach (Users usrl in userlist)
                            {
                                if (usrl.CompanyID == Convert.ToInt32(id) && usrl.RoleID <= 2)
                                {
                                    filterList.Add(new SelectListItem
                                        {Text = usrl.UName + " " + usrl.USurname, Value = usrl.UserID.ToString()});
                                }
                            }
                        }
                        else
                        {
                            foreach (Users usrall in userlist)
                            {
                                if (usrall.CompanyID == Convert.ToInt32(id))
                                {
                                    filterList.Add(new SelectListItem
                                    {
                                        Text = usrall.UName + " " + usrall.USurname, Value = usrall.UserID.ToString()
                                    });
                                }
                            }
                        }
                    }
                }
            }

            return Json(new SelectList(filterList, "Value", "Text"));
        }

        [Auth]
        public JsonResult FilterCompany()
        {
            int loggeduser = App.Common.GetUserID().Value;
            int userole;
            int compid;
            List<Companies> complist = request_manager.GetReqComp();
            List<Users> userlist = request_manager.GetReqUser();
            List<SelectListItem> filterCompany = new List<SelectListItem>();

            foreach (Users usr in userlist)
            {
                if (loggeduser == usr.UserID)
                {
                    userole = usr.RoleID;
                    compid = usr.CompanyID;
                    foreach (Companies cmp in complist)
                    {
                        if (userole <= 2)
                        {
                            if (compid == cmp.CompanyID)
                            {
                                filterCompany.Add(new SelectListItem
                                    {Text = cmp.CompanyName, Value = cmp.CompanyID.ToString()});
                            }
                        }

                        if (userole >= 3)
                        {
                            filterCompany.Add(new SelectListItem
                                {Text = cmp.CompanyName, Value = cmp.CompanyID.ToString()});
                        }
                    }
                }
            }

            return Json(new SelectList(filterCompany, "Value", "Text"));
        }

        [Auth]
        public JsonResult FilterAttendant()
        {
            List<Users> userlist = request_manager.GetReqUser();
            List<SelectListItem> filterReqAttendant = new List<SelectListItem>();

            foreach (Users usr in userlist)
            {
                if (usr.RoleID >= 3)
                {
                    filterReqAttendant.Add(new SelectListItem
                        {Text = usr.UName + " " + usr.USurname, Value = usr.UserID.ToString()});
                }

            }

            return Json(new SelectList(filterReqAttendant, "Value", "Text"));
        }

        [Auth]
        [HttpPost]
        public ActionResult NewComment(int? id, NewCommentModel data, RequestDocumentModel rdm)
        {
           if (String.IsNullOrEmpty(data.ReqStatus.ToString()) || data.ReqStatus == 0)
            {
                ModelState.Remove("ReqStatus");
            }
            if (String.IsNullOrEmpty(data.CommentTotalTime.ToString()) || data.CommentTotalTime == 0)
            {
                ModelState.Remove("CommentTotalTime");
            }
            if (String.IsNullOrEmpty(data.comUser.ToString()) || data.comUser == 0)
            {
                data.comUser = App.Common.GetUserID().Value;
            }
            if (String.IsNullOrEmpty(data.CommentText))
            {
                if (data.comStat == 4)
                {
                    foreach (CommentStatus cs in request_manager.GetCommentStatus())
                    {
                        if (cs.CommentStatusID == 4)
                        {
                            data.CommentText = "Sorun giderilmiş olup, talep durumu "+ cs.CommentStatusTitle.ToUpper() + " olarak güncellenmiştir. Desteğiniz için teşekkürler!";
                        }
                    }
                }
            }

            //var errors = ModelState
            //    .Where(x => x.Value.Errors.Count > 0)
            //    .Select(x => new { x.Key, x.Value.Errors })
            //    .ToArray();

            data.ModifiedUser = App.Common.GetUserID().Value;

            if (ModelState.IsValid)
            {
                List<Requests> reqlst = request_manager.GetRequestList();
                if (id == null)
                {
                    return Json(new HttpStatusCodeResult(HttpStatusCode.BadRequest));
                }

                HttpFileCollectionBase files = Request.Files;
                if (files != null && !String.IsNullOrEmpty(data.CommentText))
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        double lengthfile = file.ContentLength;

                        string filecontent = file.FileName.Split('.')[1];
                        foreach (Requests rq in reqlst)
                        { 
                            if(rq.RequestID == id)
                            { 
                        string filename = $"{rq.RequestNumber}_{data.comUser}_{DateTime.Now.Date}.{filecontent}";
                         
                        Directory.CreateDirectory(Server.MapPath($"~/Content/assets/img/RequestDocuments/RequestNumber_{rq.RequestNumber}/"));
                        string path = Server.MapPath($"~/Content/assets/img/RequestDocuments/RequestNumber_{rq.RequestNumber}/{filename}");
                          
                        file.SaveAs(path);
                        rdm.docReq = id.Value;
                        rdm.DocumentTitle = file.FileName;
                        rdm.DocumentTitleChanged = filename;
                        rdm.docUser = App.Common.GetUserID().Value;
                        rdm.FileSize = lengthfile;
                            }
                        }
                        ErrorsResults<RequestDocuments> erd = request_manager.NewRequestDocument(rdm);
                    }
                }

                if (!String.IsNullOrEmpty(data.CommentText))
                {
                    data.comReq = id.Value;
                    
                    ErrorsResults<RequestComments> er = request_manager.NewCommentAdd(data);

                    if (er.Errors.Count > 0)
                    {
                        er.Errors.ForEach(x => ModelState.AddModelError("", x));
                        return Json(new {result = false}, JsonRequestBehavior.AllowGet);
                    }

                    return Json(new {result = true}, JsonRequestBehavior.AllowGet);
                }

                return Json(new {result = false}, JsonRequestBehavior.AllowGet);
            }

            return Json(new {result = false}, JsonRequestBehavior.AllowGet);
        }

        [Auth]
        [HttpPost]
        public ActionResult NewCommentAnswer(int? id, NewCommentModel data)
        {
            ModelState.Remove("CommentTotalTime");
            ModelState.Remove("ReqStatus");
            ModelState.Remove("reqPriority");

            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return Json(new HttpStatusCodeResult(HttpStatusCode.BadRequest));
                }

                if (!String.IsNullOrEmpty(data.CommentText))
                {
                    foreach (RequestComments rc in request_manager.GetRequestComments())
                    {
                        if (rc.CommentID == id)
                        {
                            data.comReq = rc.comReq.RequestID;
                            data.comUser = App.Common.GetUserID().Value;
                            data.ModifiedUser = App.Common.GetUserID().Value;
                            data.comStat = rc.comStat.CommentStatusID;
                            data.ParentID = id.Value;
                        }
                    }

                    ErrorsResults<RequestComments> rcd = request_manager.CommentAnswerNew(data);

                    if (rcd.Errors.Count > 0)
                    {
                        rcd.Errors.ForEach(x => ModelState.AddModelError("", x));
                        return Json(new {result = false}, JsonRequestBehavior.AllowGet);
                    }

                    return Json(new {result = true}, JsonRequestBehavior.AllowGet);
                }

                return Json(new {result = false}, JsonRequestBehavior.AllowGet);
            }

            return Json(new {result = false}, JsonRequestBehavior.AllowGet);
        }

        [Auth]
        public JsonResult FilterCommentStatus(string id)
        {
            List<Requests> reqList = request_manager.GetRequestList();
            List<CommentStatus> comStatList = request_manager.GetCommentStatus();
            List<SelectListItem> FilterCommentStatus = new List<SelectListItem>();

            if (!String.IsNullOrEmpty(id) && id != Convert.ToInt32(0).ToString())
            {
                Requests reqFound = reqList.Find(x => x.RequestID == Convert.ToInt32(id));

                if (reqFound != null)
                {

                    if (reqFound.reqStatus.StatusID == 1 || reqFound.reqStatus.StatusID == 3 ||
                        reqFound.reqStatus.StatusID == 4)
                    {
                        foreach (CommentStatus cs in comStatList)
                        {
                            if (cs.CommentStatusID == 2 || cs.CommentStatusID == 5)
                            {
                                FilterCommentStatus.Add(new SelectListItem
                                    {Text = cs.CommentStatusTitle, Value = cs.CommentStatusID.ToString()});
                            }
                        }
                    }

                    if (reqFound.reqStatus.StatusID == 2)
                    {
                        foreach (CommentStatus cs in comStatList)
                        {
                            if (cs.CommentStatusID == 2 || cs.CommentStatusID == 5)
                            {
                                FilterCommentStatus.Add(new SelectListItem
                                    {Text = cs.CommentStatusTitle, Value = cs.CommentStatusID.ToString()});
                            }
                        }
                    }

                    if (reqFound.reqStatus.StatusID == 5)
                    {
                        foreach (CommentStatus cs in comStatList)
                        {
                            if (cs.CommentStatusID == 3 || cs.CommentStatusID == 4)
                            {
                                FilterCommentStatus.Add(new SelectListItem
                                    {Text = cs.CommentStatusTitle, Value = cs.CommentStatusID.ToString()});
                            }
                        }
                    }

                    if (reqFound.reqStatus.StatusID == 6)
                    {
                        foreach (CommentStatus cs in comStatList)
                        {
                            if (cs.CommentStatusID == 3)
                            {
                                FilterCommentStatus.Add(new SelectListItem
                                    {Text = cs.CommentStatusTitle, Value = cs.CommentStatusID.ToString()});
                            }
                        }
                    }
                }
            }

            return Json(new SelectList(FilterCommentStatus, "Value", "Text"));
        }

        [Auth]
        public JsonResult FilterRequestStatus(string id)
        {
            List<Requests> reqList = request_manager.GetRequestList();
            List<RequestStatus> reqStatList = request_manager.GetReqStatus();
            List<SelectListItem> FilterRequestStatus = new List<SelectListItem>();

            if (!String.IsNullOrEmpty(id) && id != Convert.ToInt32(0).ToString())
            {
                Requests reqFound = reqList.Find(x => x.RequestID == Convert.ToInt32(id));

                if (reqFound != null)
                {
                    if (reqFound.CommentStatusID == 1)
                    {
                        foreach (RequestStatus rs in reqStatList)
                        {
                            if (rs.StatusID != 1 && rs.StatusID != 2)
                            {
                                FilterRequestStatus.Add(new SelectListItem
                                    {Text = rs.StatusTitle, Value = rs.StatusID.ToString()});
                            }
                        }
                    }

                    if (reqFound.CommentStatusID == 2)
                    {
                        foreach (RequestStatus rs in reqStatList)
                        {
                            if (rs.StatusID == 3 || rs.StatusID == 4 || rs.StatusID == 5 || rs.StatusID == 6)
                            {
                                FilterRequestStatus.Add(new SelectListItem
                                    {Text = rs.StatusTitle, Value = rs.StatusID.ToString()});
                            }
                        }
                    }

                    if (reqFound.CommentStatusID == 3)
                    {
                        foreach (RequestStatus rs in reqStatList)
                        {
                            if (rs.StatusID == 4 || rs.StatusID == 5)
                            {
                                FilterRequestStatus.Add(new SelectListItem
                                    {Text = rs.StatusTitle, Value = rs.StatusID.ToString()});
                            }
                        }
                    }

                    if (reqFound.CommentStatusID == 4)
                    {
                        foreach (RequestStatus rs in reqStatList)
                        {
                            if (rs.StatusID == 7)
                            {
                                FilterRequestStatus.Add(new SelectListItem
                                    {Text = rs.StatusTitle, Value = rs.StatusID.ToString()});
                            }
                        }
                    }

                    if (reqFound.CommentStatusID == 5)
                    {
                        foreach (RequestStatus rs in reqStatList)
                        {
                            if (rs.StatusID == 6)
                            {
                                FilterRequestStatus.Add(new SelectListItem
                                    {Text = rs.StatusTitle, Value = rs.StatusID.ToString()});
                            }
                        }
                    }
                }
            }

            return Json(new SelectList(FilterRequestStatus, "Value", "Text"));
        }

        [Auth]
        public JsonResult FilterRequestCloseStatus(string id)
        {
            List<Requests> reqList = request_manager.GetRequestList();
            List<RequestStatus> reqStatList = request_manager.GetReqStatus();
            List<SelectListItem> selectedRequestStatus = new List<SelectListItem>();

            if (!String.IsNullOrEmpty(id) && id != Convert.ToInt32(0).ToString())
            {
                Requests reqFound = reqList.Find(x => x.RequestID == Convert.ToInt32(id));

                if (reqFound != null)
                {
                    if (reqFound.reqStatus.StatusID == 1 || reqFound.reqStatus.StatusID == 2)
                    {
                        foreach (RequestStatus rs in reqStatList)
                        {
                            if (rs.StatusID == 3 || rs.StatusID == 4 || rs.StatusID == 5)
                            {
                                selectedRequestStatus.Add(new SelectListItem
                                    {Text = rs.StatusTitle, Value = rs.StatusID.ToString()});
                            }
                        }
                    }

                    if (reqFound.reqStatus.StatusID == 3)
                    {
                        foreach (RequestStatus rs in reqStatList)
                        {
                            if (rs.StatusID == 4 || rs.StatusID == 5)
                            {
                                selectedRequestStatus.Add(new SelectListItem
                                    {Text = rs.StatusTitle, Value = rs.StatusID.ToString()});
                            }
                        }
                    }

                    if (reqFound.reqStatus.StatusID == 4)
                    {
                        foreach (RequestStatus rs in reqStatList)
                        {
                            if (rs.StatusID == 5)
                            {
                                selectedRequestStatus.Add(new SelectListItem
                                    {Text = rs.StatusTitle, Value = rs.StatusID.ToString()});
                            }
                        }
                    }
                }
            }

            return Json(new SelectList(selectedRequestStatus, "Value", "Text"));
        }

        [Auth]
        public ActionResult ShowTakeRequestModal(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Requests reqList = request_manager.Find(x => x.RequestID == id);
            if (reqList == null)
            {
                return HttpNotFound();
            }

            return PartialView("_RequestAttendant", reqList);
        }

        public ActionResult ShowAnswerModal(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            RequestComments reqComList = request_manager.GetRequestComments().Find(x => x.CommentID == id);
            if (reqComList == null)
            {
                return HttpNotFound();
            }
            return PartialView("_CommentAnswer", reqComList);
        }

        [Auth]
        public JsonResult FilterTakeRequestAttendant()
        {
            List<Users> userlist = request_manager.GetReqUser();
            List<SelectListItem> filterReqAttendant = new List<SelectListItem>();
            int loggeduser = App.Common.GetUserID().Value;
            //filterReqAttendant.Add(new SelectListItem { Text = "Seçiniz", Value = "0" });
            foreach (Users usr in userlist)
            {
                if (usr.RoleID >= 3 && usr.UserID != loggeduser)
                {
                    filterReqAttendant.Add(new SelectListItem
                        {Text = usr.UName + " " + usr.USurname, Value = usr.UserID.ToString()});

                }
            }

            return Json(new SelectList(filterReqAttendant, "Value", "Text"));
        }

        [Auth]
        public JsonResult FilterRequestStatusOwner(string id)
        {
            List<Requests> reqList = request_manager.GetRequestList();
            List<SelectListItem> FilterRequestStatusOwner = new List<SelectListItem>();
            string reqOwner = " ";

            if (!String.IsNullOrEmpty(id) && id != Convert.ToInt32(0).ToString())
            {
                foreach (Requests rq in reqList)
                {
                    if (rq.RequestID == Convert.ToInt32(id))
                    {
                        reqOwner = rq.reqUser.UName + " " + rq.reqUser.USurname;
                        FilterRequestStatusOwner.Add(new SelectListItem
                            {Text = reqOwner, Value = rq.reqUser.UserID.ToString()});
                    }
                }
            }
            return Json(new SelectList(FilterRequestStatusOwner, "Value", "Text"));
        }
    }
}
