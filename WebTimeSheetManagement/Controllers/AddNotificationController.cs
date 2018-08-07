namespace WebTimeSheetManagement.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using WebTimeSheetManagement.Concrete;
    using WebTimeSheetManagement.Filters;
    using WebTimeSheetManagement.Hubs;
    using WebTimeSheetManagement.Interface;
    using WebTimeSheetManagement.Models;

    /// <summary>
    /// Defines the <see cref="AddNotificationController" />
    /// </summary>
    [ValidateSuperAdminSession]
    public class AddNotificationController : Controller
    {
        /// <summary>
        /// Defines the _INotification
        /// </summary>
        private readonly INotification _INotification;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddNotificationController"/> class.
        /// </summary>
        public AddNotificationController()
        {
            _INotification = new NotificationConcrete();
        }

        /// <summary>
        /// The Add
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpGet]
        // GET: AddNotification
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// The Add
        /// </summary>
        /// <param name="NotificationsTB">The NotificationsTB<see cref="NotificationsTB"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(NotificationsTB NotificationsTB)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(NotificationsTB);
                }

                _INotification.DisableExistingNotifications();

                var Notifications = new NotificationsTB
                {
                    CreatedOn = DateTime.Now,
                    Message = NotificationsTB.Message,
                    NotificationsID = 0,
                    Status = "A",
                    FromDate = NotificationsTB.FromDate,
                    ToDate = NotificationsTB.ToDate
                };

                _INotification.AddNotification(Notifications);

                MyNotificationHub.Send();
                return RedirectToAction("Add", "AddNotification");
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The AllNotification
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpGet]
        public ActionResult AllNotification()
        {
            return View();
        }

        /// <summary>
        /// The LoadNotificationData
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult LoadNotificationData()
        {
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                int recordsTotal = 0;

                var notificationdata = _INotification.ShowNotifications(sortColumn, sortColumnDir, searchValue);
                recordsTotal = notificationdata.Count();
                var data = notificationdata.Skip(skip).Take(pageSize).ToList();

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The DeActivateNotification
        /// </summary>
        /// <param name="NotificationID">The NotificationID<see cref="string"/></param>
        /// <returns>The <see cref="JsonResult"/></returns>
        public JsonResult DeActivateNotification(string NotificationID)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(NotificationID)))
                {
                    return Json("Error", JsonRequestBehavior.AllowGet);
                }

                var result = _INotification.DeActivateNotificationByID(Convert.ToInt32(NotificationID));

                if (result)
                {
                    return Json(data: true, behavior: JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(data: false, behavior: JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
