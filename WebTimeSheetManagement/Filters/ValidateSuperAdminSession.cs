namespace WebTimeSheetManagement.Filters
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// Defines the <see cref="ValidateSuperAdminSession" />
    /// </summary>
    public class ValidateSuperAdminSession : ActionFilterAttribute
    {
        /// <summary>
        /// The OnActionExecuting
        /// </summary>
        /// <param name="filterContext">The filterContext<see cref="ActionExecutingContext"/></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session["SuperAdmin"])))
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
