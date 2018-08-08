namespace WebTimeSheetManagement.Helpers
{
    using System;
    using System.Configuration;
    using System.Globalization;
    using System.IO;
    using System.Web.Mvc;

    /// <summary>
    /// Defines the <see cref="ErrorLoggerAttribute" />
    /// </summary>
    public class ErrorLoggerAttribute : HandleErrorAttribute
    {
        /// <summary>
        /// The OnException
        /// </summary>
        /// <param name="filterContext">The filterContext<see cref="ExceptionContext" /></param>
        public override void OnException(ExceptionContext filterContext)
        {
            string strLogText = string.Empty;
            Exception ex = filterContext.Exception;
            filterContext.ExceptionHandled = true;
            var objClass = filterContext;
            strLogText += "Message ---\n{0}" + ex.Message;

            if (ex.Source == ".Net SqlClient Data Provider")
            {
                strLogText += Environment.NewLine + "SqlClient Error ---\n{0}Check Sql Error";
            }
            else if (ex.Source == "System.Web.Mvc")
            {
                strLogText += Environment.NewLine + ".Net Error ---\n{0}Check MVC Code For Error";
            }
            else if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                strLogText += Environment.NewLine + ".Net Error ---\n{0}Check MVC Ajax Code For Error";
            }

            strLogText += Environment.NewLine + "Source ---\n{0}" + ex.Source;
            strLogText += Environment.NewLine + "StackTrace ---\n{0}" + ex.StackTrace;
            strLogText += Environment.NewLine + "TargetSite ---\n{0}" + ex.TargetSite;
            if (ex.InnerException != null)
            {
                strLogText += Environment.NewLine + "Inner Exception is {0}" + ex.InnerException; // error prone
            }
            if (ex.HelpLink != null)
            {
                strLogText += Environment.NewLine + "HelpLink ---\n{0}" + ex.HelpLink; // error prone
            }

            StreamWriter log;

            string timestamp = DateTime.Now.ToString("d-MMMM-yyyy", new CultureInfo("en-GB"));

            string error_folder = ConfigurationManager.AppSettings["ErrorLogPath"];

            if (!System.IO.Directory.Exists(error_folder))
            {
                System.IO.Directory.CreateDirectory(error_folder);
            }

            if (!File.Exists(string.Format(@"{0}\Log_{1}.txt", error_folder, timestamp)))
            {
                log = new StreamWriter(string.Format(@"{0}\Log_{1}.txt", error_folder, timestamp));
            }
            else
            {
                log = File.AppendText(string.Format(@"{0}\Log_{1}.txt", error_folder, timestamp));
            }

            var controllerName = (string)filterContext.RouteData.Values["controller"];
            var actionName = (string)filterContext.RouteData.Values["action"];

            // Write to the file:
            log.WriteLine(Environment.NewLine + DateTime.Now);
            log.WriteLine("------------------------------------------------------------------------------------------------");
            log.WriteLine("Controller Name :- " + controllerName);
            log.WriteLine("Action Method Name :- " + actionName);
            log.WriteLine("------------------------------------------------------------------------------------------------");
            log.WriteLine(objClass);
            log.WriteLine(strLogText);
            log.WriteLine();

            // Close the stream:
            log.Close();
            filterContext.HttpContext.Session.Abandon();
            filterContext.Result = new ViewResult()
            {
                ViewName = "Error"
            };
        }
    }
}
