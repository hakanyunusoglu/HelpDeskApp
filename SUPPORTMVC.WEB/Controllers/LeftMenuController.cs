using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SUPPORTMVC.BLL;
using SUPPORTMVC.ENTITIES.DBT;
using SUPPORTMVC.WEB.Filters;

namespace SUPPORTMVC.WEB.Controllers
{
    public class LeftMenuController : Controller
    {
        [Auth]
        public ActionResult SelectedMenuItem(int? id)
        {
            LeftMenuManager lm = new LeftMenuManager();
            LeftMenuItems lmi = lm.GetLeftMenuItemID(id.Value);
            if (lmi.LinkID == 1)
            {
                return RedirectToAction("RequestReg", "Request");
            }
            else if (lmi.LinkID == 2)
            {
                return RedirectToAction("RequestList", "RequestList");
            }
            else if (lmi.LinkID == 3)
            {
                return RedirectToAction("ClosedRequestList", "RequestList");
            }
            else if (lmi.LinkID == 4)
            {
                return RedirectToAction("RequestList", "RequestList");
            }
            else if (lmi.LinkID == 5)
            {
                return RedirectToAction("CompanyReg", "Company");
            }
            else if (lmi.LinkID == 6)
            {
                return RedirectToAction("CompanyList", "Company");
            }
            else if (lmi.LinkID == 7)
            {
                return RedirectToAction("UserReg", "User");
            }
            else if (lmi.LinkID == 8)
            {
                return RedirectToAction("UserList", "User");
            }
            else if (lmi.LinkID == 9)
            {
                return RedirectToAction("UpdateNotes", "Updates");
            }
            else if (lmi.LinkID == 10)
            {
                return RedirectToAction("AddStatus", "AdminRequestExtensions");
            }
            else if (lmi.LinkID == 11)
            {
                return RedirectToAction("AddPriority", "AdminRequestExtensions");
            }
            else if (lmi.LinkID == 12)
            {
                return RedirectToAction("AddType", "AdminRequestExtensions");
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
    }
}