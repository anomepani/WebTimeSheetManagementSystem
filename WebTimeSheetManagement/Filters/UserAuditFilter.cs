namespace WebTimeSheetManagement.Filters
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using WebTimeSheetManagement.Concrete;
    using WebTimeSheetManagement.Interface;
    using WebTimeSheetManagement.Models;

    /// <summary>
    /// Defines the <see cref="UserAuditFilter" />
    /// </summary>
    public class UserAuditFilter : ActionFilterAttribute
    {
        /// <summary>
        /// Defines the _IAudit
        /// </summary>
        private readonly IAudit _IAudit;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAuditFilter"/> class.
        /// </summary>
        public UserAuditFilter()
        {
            _IAudit = new AuditConcrete();
        }

        /// <summary>
        /// The OnActionExecuting
        /// </summary>
        /// <param name="filterContext">The filterContext<see cref="ActionExecutingContext"/></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            AuditTB objaudit = new AuditTB(); // Getting Action Name 

            string actionName = filterContext.ActionDescriptor.ActionName; //Getting Controller Name 
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var request = filterContext.HttpContext.Request;

            if (HttpContext.Current.Session["UserID"] != null)
            {
                objaudit.UserID = Convert.ToString(HttpContext.Current.Session["UserID"]);
            }
            else if (HttpContext.Current.Session["AdminUser"] != null)
            {
                objaudit.UserID = Convert.ToString(HttpContext.Current.Session["AdminUser"]);
            }
            else
            {
                objaudit.UserID = "";
            }

            objaudit.SessionID = HttpContext.Current.Session.SessionID; // Application SessionID // User IPAddress 
            objaudit.IPAddress = request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.UserHostAddress;
            objaudit.PageAccessed = Convert.ToString(filterContext.HttpContext.Request.Url); // URL User Requested 
            objaudit.LoggedInAt = DateTime.Now; // Time User Logged In || And time User Request Method 

            if (actionName == "LogOff")
            {
                objaudit.LoggedOutAt = DateTime.Now; // Time User Logged OUT 
            }

            objaudit.LoginStatus = "A";
            objaudit.ControllerName = controllerName; // ControllerName 
            objaudit.ActionName = actionName;

            Uri myReferrer = request.UrlReferrer;

            if (myReferrer != null)
            {
                string actual = myReferrer.ToString();

                if (actual != null)
                {
                    objaudit.UrlReferrer = request.UrlReferrer.AbsolutePath;
                }
            }

            _IAudit.InsertAuditData(objaudit);
        }
    }
}
