namespace WebTimeSheetManagement.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using WebTimeSheetManagement.Concrete;
    using WebTimeSheetManagement.Filters;
    using WebTimeSheetManagement.Interface;
    using WebTimeSheetManagement.Models;

    /// <summary>
    /// Defines the <see cref="AllTimeSheetController" />
    /// </summary>
    [ValidateUserSession]
    public class AllTimeSheetController : Controller
    {
        /// <summary>
        /// Defines the _IProject
        /// </summary>
        private readonly IProject _IProject;

        /// <summary>
        /// Defines the _ITimeSheet
        /// </summary>
        private readonly ITimeSheet _ITimeSheet;

        /// <summary>
        /// Initializes a new instance of the <see cref="AllTimeSheetController"/> class.
        /// </summary>
        public AllTimeSheetController()
        {
            _IProject = new ProjectConcrete();
            _ITimeSheet = new TimeSheetConcrete();
        }

        // GET: AllTimeSheet
        /// <summary>
        /// The TimeSheet
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult TimeSheet()
        {
            return View();
        }

        /// <summary>
        /// The LoadTimeSheetData
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult LoadTimeSheetData()
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
                var v = _ITimeSheet.ShowTimeSheet(sortColumn, sortColumnDir, searchValue, Convert.ToInt32(Session["UserID"]));
                recordsTotal = v.Count();
                var data = v.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The Details
        /// </summary>
        /// <param name="id">The id<see cref="string"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Details(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return RedirectToAction("TimeSheet", "AllTimeSheet");
                }
                MainTimeSheetView objMT = new MainTimeSheetView
                {
                    ListTimeSheetDetails = _ITimeSheet.TimesheetDetailsbyTimeSheetMasterID(Convert.ToInt32(Session["UserID"]), Convert.ToInt32(id)),
                    ListofProjectNames = _ITimeSheet.GetProjectNamesbyTimeSheetMasterID(Convert.ToInt32(id)),
                    ListofPeriods = _ITimeSheet.GetPeriodsbyTimeSheetMasterID(Convert.ToInt32(id)),
                    ListoDayofWeek = DayofWeek(),
                    TimeSheetMasterID = Convert.ToInt32(id)
                };
                return View(objMT);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The DayofWeek
        /// </summary>
        /// <returns>The <see cref="List{string}"/></returns>
        [NonAction]
        public List<string> DayofWeek()
        {
            List<string> li = new List<string>
            {
                "Sunday",
                "Monday",
                "Tuesday",
                "Wednesday",
                "Thursday",
                "Friday",
                "Saturday",
                "Total"
            };
            return li;
        }

        /// <summary>
        /// The SubmittedTimeSheet
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult SubmittedTimeSheet()
        {
            return View();
        }

        /// <summary>
        /// The ApprovedTimeSheet
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult ApprovedTimeSheet()
        {
            return View();
        }

        /// <summary>
        /// The RejectedTimeSheet
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult RejectedTimeSheet()
        {
            return View();
        }

        /// <summary>
        /// The LoadSubmittedTimeSheetData
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult LoadSubmittedTimeSheetData()
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
                var v = _ITimeSheet.ShowTimeSheetStatus(sortColumn, sortColumnDir, searchValue, Convert.ToInt32(Session["UserID"]), 1);
                recordsTotal = v.Count();
                var data = v.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The LoadRejectedTimeSheetData
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult LoadRejectedTimeSheetData()
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
                var v = _ITimeSheet.ShowTimeSheetStatus(sortColumn, sortColumnDir, searchValue, Convert.ToInt32(Session["UserID"]), 3);
                recordsTotal = v.Count();
                var data = v.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The LoadApprovedTimeSheetData
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult LoadApprovedTimeSheetData()
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
                var v = _ITimeSheet.ShowTimeSheetStatus(sortColumn, sortColumnDir, searchValue, Convert.ToInt32(Session["UserID"]), 2);
                recordsTotal = v.Count();
                var data = v.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
