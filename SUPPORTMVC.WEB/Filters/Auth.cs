using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SUPPORTMVC.WEB.Filters
{
    public class Auth:FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (HttpContext.Current.Session["User"] == null)
            {
               filterContext.HttpContext.Response.Redirect("/Login/Login");
            }
            
        }
    }
}