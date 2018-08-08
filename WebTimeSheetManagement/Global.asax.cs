namespace WebTimeSheetManagement
{
    using System;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using WebTimeSheetManagement.Filters;

    /// <summary>
    /// Defines the <see cref="MvcApplication" />
    /// </summary>
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// The Application_BeginRequest
        /// </summary>
        protected void Application_BeginRequest()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
        }

        /// <summary>
        /// The Application_Start
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalFilters.Filters.Add(new UserAuditFilter());
            SqlDependency.Start(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString());
        }

        /// <summary>
        /// The Application_Error
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="EventArgs"/></param>
        private void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            if (ex == null || ex.Message.StartsWith("File"))
            {
                return;
            }
            try
            {
                Server.ClearError();
                // Below line execute in infinite loops as below path is not found so updated code
                // Response.Redirect("~/Errorview/Error");

                // Updated code for redirect to Error page in general 
                // need to create separate page for Well known error. e.g. 404 Error
                Response.Redirect("~/Error/Error");
            }
            finally
            {
                ex = null;
            }
        }
    }
}
