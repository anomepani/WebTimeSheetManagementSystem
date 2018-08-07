namespace WebTimeSheetManagement.Controllers
{
    using System;
    using System.Web.Mvc;
    using WebTimeSheetManagement.Filters;
    using WebTimeSheetManagement.Service;

    /// <summary>
    /// Defines the <see cref="NotificationController" />
    /// </summary>
    [ValidateUserSession]
    public class NotificationController : Controller
    {
        /// <summary>
        /// The GetNotification
        /// </summary>
        /// <returns>The <see cref="JsonResult"/></returns>
        public JsonResult GetNotification()
        {
            try
            {
                return Json(NotificationService.GetNotification(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
