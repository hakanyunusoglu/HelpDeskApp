using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using SUPPORTMVC.COMMON;
using SUPPORTMVC.ENTITIES.DBT;

namespace SUPPORTMVC.WEB.Init
{
    public class WebCommon : ICommon
    {
        public int? GetUserID()
        {
            if (HttpContext.Current.Session["User"] != null)
            {
                Users user = HttpContext.Current.Session["User"] as Users;
                return user.UserID;
            }
            return null;
        }
    }
}