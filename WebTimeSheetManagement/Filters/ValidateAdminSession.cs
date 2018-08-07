﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebTimeSheetManagement.Filters
{
    public class ValidateAdminSession : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session["AdminUser"])))
                {
                    filterContext.Controller.TempData["ErrorMessage"] = "Session has been expired please Login";
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "Error" }));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}