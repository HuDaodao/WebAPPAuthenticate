using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppAuthenticate.Filters
{
    public class HasUserFilter:ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (!Convert.ToBoolean(filterContext.HttpContext.Session["HasUser"]))
            {
                filterContext.Result = new RedirectResult("/LogIn/LogIn");
            }
        }
    }
}