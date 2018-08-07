namespace WebTimeSheetManagement
{
    using System.Web.Mvc;
    using WebTimeSheetManagement.Helpers;

    /// <summary>
    /// Defines the <see cref="FilterConfig" />
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// The RegisterGlobalFilters
        /// </summary>
        /// <param name="filters">The filters<see cref="GlobalFilterCollection"/></param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ErrorLoggerAttribute());
        }
    }
}
