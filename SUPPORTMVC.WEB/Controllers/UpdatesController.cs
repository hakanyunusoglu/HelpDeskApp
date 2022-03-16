using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SUPPORTMVC.WEB.Filters;

namespace SUPPORTMVC.WEB.Controllers
{
    public class UpdatesController : Controller
    {
        [Auth]
        public ActionResult UpdateNotes()
        {
            return View();
        }
    }
}