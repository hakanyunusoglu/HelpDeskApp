using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.DynamicData;
using System.Web.Helpers;
using System.Web.Mvc;
using SUPPORTMVC.BLL;
using SUPPORTMVC.COMMON;
using SUPPORTMVC.ENTITIES;
using SUPPORTMVC.ENTITIES.DBT;
using SUPPORTMVC.WEB.Filters;

namespace SUPPORTMVC.WEB.Controllers
{
    
    public class IndexController : Controller
    {
        RequestManager rm = new RequestManager();
        [Auth]
        public ActionResult Index()
        {
           return View();
        }
        //[Auth]
        //public JsonResult StatusChart()
        //{
        //    int loggeduser = App.Common.GetUserID().Value;
        //    int userole;
        //    int compid;

        //    List<Users> userList = rm.GetReqUser();
        //    List<Requests> reqList = rm.GetRequestList();
        //    List<RequestStatus> reqStatList = rm.GetReqStatus();
        //    List<object> iData = new List<object>();
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("Title", System.Type.GetType("System.String"));
        //    dt.Columns.Add("Value", System.Type.GetType("System.Int32"));
        //    DataRow dr = dt.NewRow();
        //    string title =" ";
        //    foreach (Users usr in userList)
        //    {
        //        if (usr.UserID == loggeduser)
        //        {
        //            userole = usr.RoleID;
        //            compid = usr.CompanyID;

        //            foreach (Requests rq in reqList)
        //            {
        //                if (userole == 1)
        //                {

        //                    if (rq.reqCompany.CompanyID == compid && rq.reqUser.UserID == loggeduser)
        //                    {
        //                        foreach (RequestStatus rs in reqStatList)
        //                        {
        //                            if (rq.reqStatus.StatusID == rs.StatusID)
        //                            {
        //                                var count = reqList.Where(x => x.reqUser.UserID == loggeduser)
        //                                    .Count(a => a.reqStatus.StatusID == rs.StatusID);

        //                                dr = dt.NewRow();
        //                                dr["Title"] = rs.StatusTitle;
        //                                if (dr["Title"] != title)
        //                                {
        //                                    title = rs.StatusTitle;
        //                                    dr["Title"] = title;
        //                                    dr["Value"] = count;
        //                                    dt.Rows.Add(dr);
        //                                }
                                        
        //                            }
        //                        }
                               
        //                    }
        //                }

        //                if (userole == 2)
        //                {
        //                    if (rq.reqCompany.CompanyID == compid)
        //                    {
        //                        foreach (RequestStatus rs in reqStatList)
        //                        {
        //                            if (rq.reqStatus.StatusID == rs.StatusID)
        //                            {
        //                                var count = reqList.Where(x => x.reqCompany.CompanyID == compid)
        //                                    .Count(a => a.reqStatus.StatusID == rs.StatusID);

        //                                dr = dt.NewRow();
        //                                dr["Title"] = rs.StatusTitle;
        //                                if (dr["Title"] != title)
        //                                {
        //                                    title = rs.StatusTitle;
        //                                    dr["Title"] = title;
        //                                    dr["Value"] = count;
        //                                    dt.Rows.Add(dr);
        //                                }
        //                            }
        //                        }
        //                    }
        //                }

        //                if (userole == 3)
        //                {
        //                    if (rq.RequestAttendant == loggeduser)
        //                    {
        //                        foreach (RequestStatus rs in reqStatList)
        //                        {
        //                            var count = reqList.Where(x => x.RequestAttendant == loggeduser)
        //                                .Count(a => a.reqStatus.StatusID == rs.StatusID);

        //                            dr = dt.NewRow();
        //                            dr["Title"] = rs.StatusTitle;
        //                            if (dr["Title"] != title)
        //                            {
        //                                title = rs.StatusTitle;
        //                                dr["Title"] = title;
        //                                dr["Value"] = count;
        //                                dt.Rows.Add(dr);
        //                            }
        //                        }
        //                    }
        //                }
        //            }

        //            if (usr.RoleID >= 4)
        //            {
        //                foreach (Requests rqs in reqList)
        //                {
        //                    foreach (RequestStatus rs in reqStatList)
        //                    {
        //                        if (rqs.reqStatus.StatusID == rs.StatusID)
        //                        {
        //                            var count = reqList.Count(a => a.reqStatus.StatusID == rs.StatusID);

        //                            dr = dt.NewRow();
        //                            dr["Title"] = rs.StatusTitle;
        //                            if (dr["Title"] != title)
        //                            {
        //                                title = rs.StatusTitle;
        //                                dr["Title"] = title;
        //                                dr["Value"] = count;
        //                                dt.Rows.Add(dr);
        //                            }

        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    foreach (DataColumn dc in dt.Columns)
        //    {
        //        List<object> x = new List<object>();
        //        x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
        //        iData.Add(x);
        //    }
        //    return Json(iData, JsonRequestBehavior.AllowGet);
        //}
        //[Auth]
        //public JsonResult RequestOwnerChart()
        //{
        //    int loggeduser = App.Common.GetUserID().Value;
        //    int compid;
        //    string userName;
        //    List<Users> userList = rm.GetReqUser();
        //    List<Requests> reqList = rm.GetRequestList();
        //    List<RequestStatus> reqStatList = rm.GetReqStatus();
        //    List<object> iData = new List<object>();
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("Title", System.Type.GetType("System.String"));
        //    dt.Columns.Add("Value", System.Type.GetType("System.Int32"));
        //    DataRow dr = dt.NewRow();
        //    string title = "";
        //    foreach (Requests rq in reqList)
        //    {
        //        foreach (Users usr in userList)
        //        {
        //            if (usr.UserID == loggeduser)
        //            {
        //                compid = usr.CompanyID;
        //                foreach (Users usrl in userList)
        //                {
        //                    if (rq.reqCompany.CompanyID == compid)
        //                    {
        //                        if (usrl.CompanyID == compid && usrl.UserID == loggeduser)
        //                        {
        //                            int count = reqList.Where(x => x.reqCompany.CompanyID == compid && x.reqUser.UserID == usrl.UserID)
        //                                .Count(a => a.reqUser.UserID == usrl.UserID);
        //                            userName = usrl.UName + " " + usrl.USurname;
        //                            dr = dt.NewRow();
        //                            if (userName != title)
        //                            {
        //                                title = userName;
        //                                dr["Title"] = title;
        //                                dr["Value"] = count;
        //                                dt.Rows.Add(dr);

        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    foreach (DataColumn dc in dt.Columns)
        //    {
        //        List<object> x = new List<object>();
        //        x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
        //        iData.Add(x);
        //    }
        //    return Json(iData, JsonRequestBehavior.AllowGet);
        //}
    }
}