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
    /// Defines the <see cref="ShowAllTimeSheetController" />
    /// </summary>
    [ValidateAdminSession]
    public class ShowAllTimeSheetController : Controller
    {
        /// <summary>
        /// Defines the _IProject
        /// </summary>
        private readonly IProject _IProject;

        /// <summary>
        /// Defines the _IUsers
        /// </summary>
        private readonly IUsers _IUsers;

        /// <summary>
        /// Defines the _ITimeSheet
        /// </summary>
        private readonly ITimeSheet _ITimeSheet;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShowAllTimeSheetController"/> class.
        /// </summary>
        public ShowAllTimeSheetController()
        {
            _IProject = new ProjectConcrete();
            _ITimeSheet = new TimeSheetConcrete();
            _IUsers = new UsersConcrete();
        }

        // GET: ShowAllTimeSheet
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

                var timesheetdata = _ITimeSheet.ShowAllTimeSheet(sortColumn, sortColumnDir, searchValue, Convert.ToInt32(Session["AdminUser"]));
                recordsTotal = timesheetdata.Count();
                var data = timesheetdata.Skip(skip).Take(pageSize).ToList();

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
                    ListTimeSheetDetails = _ITimeSheet.TimesheetDetailsbyTimeSheetMasterID(Convert.ToInt32(id)),
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
            List<string> li = new List<string>();
            li.Add("Sunday");
            li.Add("Monday");
            li.Add("Tuesday");
            li.Add("Wednesday");
            li.Add("Thursday");
            li.Add("Friday");
            li.Add("Saturday");
            li.Add("Total");
            return li;
        }

        /// <summary>
        /// The Approval
        /// </summary>
        /// <param name="TimeSheetApproval">The TimeSheetApproval<see cref="TimeSheetApproval"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Approval(TimeSheetApproval TimeSheetApproval)
        {
            try
            {
                if (TimeSheetApproval.Comment == null)
                {
                    return Json(false);
                }

                if (string.IsNullOrEmpty(Convert.ToString(TimeSheetApproval.TimeSheetMasterID)))
                {
                    return Json(false);
                }

                _ITimeSheet.UpdateTimeSheetStatus(TimeSheetApproval, 2); //Approve

                if (_ITimeSheet.IsTimesheetALreadyProcessed(TimeSheetApproval.TimeSheetMasterID))
                {
                    _ITimeSheet.UpdateTimeSheetAuditStatus(TimeSheetApproval.TimeSheetMasterID, TimeSheetApproval.Comment, 2);
                }
                else
                {
                    _ITimeSheet.InsertTimeSheetAuditLog(InsertTimeSheetAudit(TimeSheetApproval, 2));
                }

                return Json(true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The Rejected
        /// </summary>
        /// <param name="TimeSheetApproval">The TimeSheetApproval<see cref="TimeSheetApproval"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Rejected(TimeSheetApproval TimeSheetApproval)
        {
            try
            {
                if (TimeSheetApproval.Comment == null)
                {
                    return Json(false);
                }

                if (string.IsNullOrEmpty(Convert.ToString(TimeSheetApproval.TimeSheetMasterID)))
                {
                    return Json(false);
                }

                _ITimeSheet.UpdateTimeSheetStatus(TimeSheetApproval, 3); //Reject

                if (_ITimeSheet.IsTimesheetALreadyProcessed(TimeSheetApproval.TimeSheetMasterID))
                {
                    _ITimeSheet.UpdateTimeSheetAuditStatus(TimeSheetApproval.TimeSheetMasterID, TimeSheetApproval.Comment, 3);
                }
                else
                {
                    _ITimeSheet.InsertTimeSheetAuditLog(InsertTimeSheetAudit(TimeSheetApproval, 3));
                }

                return Json(true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The InsertTimeSheetAudit
        /// </summary>
        /// <param name="TimeSheetApproval">The TimeSheetApproval<see cref="TimeSheetApproval"/></param>
        /// <param name="Status">The Status<see cref="int"/></param>
        /// <returns>The <see cref="TimeSheetAuditTB"/></returns>
        private TimeSheetAuditTB InsertTimeSheetAudit(TimeSheetApproval TimeSheetApproval, int Status)
        {
            try
            {
                TimeSheetAuditTB objAuditTB = new TimeSheetAuditTB
                {
                    ApprovalTimeSheetLogID = 0,
                    TimeSheetID = TimeSheetApproval.TimeSheetMasterID,
                    Status = Status,
                    CreatedOn = DateTime.Now,
                    Comment = TimeSheetApproval.Comment,
                    ApprovalUser = Convert.ToInt32(Session["AdminUser"]),
                    ProcessedDate = DateTime.Now,
                    UserID = _IUsers.GetUserIDbyTimesheetID(TimeSheetApproval.TimeSheetMasterID)
                };
                return objAuditTB;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The Delete
        /// </summary>
        /// <param name="TimeSheetMasterID">The TimeSheetMasterID<see cref="int"/></param>
        /// <returns>The <see cref="JsonResult"/></returns>
        public JsonResult Delete(int TimeSheetMasterID)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(TimeSheetMasterID)))
                {
                    return Json("Error", JsonRequestBehavior.AllowGet);
                }

                var data = _ITimeSheet.DeleteTimesheetByOnlyTimeSheetMasterID(TimeSheetMasterID);

                if (data > 0)
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
        /// The LoadSubmittedTData
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult LoadSubmittedTData()
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

                var timesheetdata = _ITimeSheet.ShowAllSubmittedTimeSheet(sortColumn, sortColumnDir, searchValue, Convert.ToInt32(Session["AdminUser"]));
                recordsTotal = timesheetdata.Count();
                var data = timesheetdata.Skip(skip).Take(pageSize).ToList();

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The LoadRejectedData
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult LoadRejectedData()
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

                var timesheetdata = _ITimeSheet.ShowAllRejectTimeSheet(sortColumn, sortColumnDir, searchValue, Convert.ToInt32(Session["AdminUser"]));
                recordsTotal = timesheetdata.Count();
                var data = timesheetdata.Skip(skip).Take(pageSize).ToList();

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The LoadApprovedData
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult LoadApprovedData()
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

                var timesheetdata = _ITimeSheet.ShowAllApprovedTimeSheet(sortColumn, sortColumnDir, searchValue, Convert.ToInt32(Session["AdminUser"]));
                recordsTotal = timesheetdata.Count();
                var data = timesheetdata.Skip(skip).Take(pageSize).ToList();

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
