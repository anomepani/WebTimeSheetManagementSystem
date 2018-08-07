namespace WebTimeSheetManagement.Controllers
{
    using System;
    using System.Web.Mvc;
    using WebTimeSheetManagement.Concrete;
    using WebTimeSheetManagement.Filters;
    using WebTimeSheetManagement.Interface;
    using WebTimeSheetManagement.Models;

    /// <summary>
    /// Defines the <see cref="TimeSheetController" />
    /// </summary>
    [ValidateUserSession]
    public class TimeSheetController : Controller
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
        /// Defines the _IUsers
        /// </summary>
        private readonly IUsers _IUsers;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSheetController"/> class.
        /// </summary>
        public TimeSheetController()
        {
            _IProject = new ProjectConcrete();
            _ITimeSheet = new TimeSheetConcrete();
            _IUsers = new UsersConcrete();
        }

        // GET: TimeSheet
        /// <summary>
        /// The Add
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// The Add
        /// </summary>
        /// <param name="timesheetmodel">The timesheetmodel<see cref="TimeSheetModel"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(TimeSheetModel timesheetmodel)
        {
            try
            {
                if (timesheetmodel == null)
                {
                    ModelState.AddModelError("", "Values Posted Are Not Accurate");
                    return View();
                }

                TimeSheetMaster objtimesheetmaster = new TimeSheetMaster
                {
                    TimeSheetMasterID = 0,
                    UserID = Convert.ToInt32(Session["UserID"]),
                    CreatedOn = DateTime.Now,
                    FromDate = timesheetmodel.hdtext1,
                    ToDate = timesheetmodel.hdtext7,
                    TotalHours = CalculateTotalHours(timesheetmodel),
                    TimeSheetStatus = 1
                };
                int TimeSheetMasterID = _ITimeSheet.AddTimeSheetMaster(objtimesheetmaster);

                var count = ProjectSelectCount(timesheetmodel);

                if (TimeSheetMasterID > 0)
                {
                    Save(timesheetmodel, TimeSheetMasterID);
                    SaveDescription(timesheetmodel, TimeSheetMasterID);
                    _ITimeSheet.InsertTimeSheetAuditLog(InsertTimeSheetAudit(TimeSheetMasterID, 1));
                }

                TempData["TimeCardMessage"] = "Data Saved Successfully";

                return RedirectToAction("Add", "TimeSheet");
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The ListofProjects
        /// </summary>
        /// <returns>The <see cref="JsonResult"/></returns>
        public JsonResult ListofProjects()
        {
            try
            {
                var listofProjects = _IProject.GetListofProjects();
                return Json(listofProjects, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The CalculateTotalHours
        /// </summary>
        /// <param name="TimeSheetModel">The TimeSheetModel<see cref="TimeSheetModel"/></param>
        /// <returns>The <see cref="int?"/></returns>
        [NonAction]
        private int? CalculateTotalHours(TimeSheetModel TimeSheetModel)
        {
            try
            {
                int? Total = 0;
                var val1 = TimeSheetModel.texttotal_p1 ?? 0;
                var val2 = TimeSheetModel.texttotal_p2 ?? 0;
                var val3 = TimeSheetModel.texttotal_p3 ?? 0;
                var val4 = TimeSheetModel.texttotal_p4 ?? 0;
                var val5 = TimeSheetModel.texttotal_p5 ?? 0;
                var val6 = TimeSheetModel.texttotal_p6 ?? 0;
                Total = val1 + val2 + val3 + val4 + val5 + val6;
                return Total;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The SaveTimeSheetDetail
        /// </summary>
        /// <param name="DaysofWeek">The DaysofWeek<see cref="string"/></param>
        /// <param name="Hours">The Hours<see cref="int?"/></param>
        /// <param name="Period">The Period<see cref="DateTime?"/></param>
        /// <param name="ProjectID">The ProjectID<see cref="int?"/></param>
        /// <param name="TimeSheetMasterID">The TimeSheetMasterID<see cref="int"/></param>
        [NonAction]
        private void SaveTimeSheetDetail(string DaysofWeek, int? Hours, DateTime? Period, int? ProjectID, int TimeSheetMasterID)
        {
            try
            {
                TimeSheetDetails objtimesheetdetails = new TimeSheetDetails
                {
                    TimeSheetID = 0,
                    DaysofWeek = DaysofWeek,
                    Hours = Hours ?? 0,
                    Period = Period,
                    ProjectID = ProjectID,
                    UserID = Convert.ToInt32(Session["UserID"]),
                    CreatedOn = DateTime.Now,
                    TimeSheetMasterID = TimeSheetMasterID
                };
                int TimeSheetID = _ITimeSheet.AddTimeSheetDetail(objtimesheetdetails);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The CheckIsDateAlreadyUsed
        /// </summary>
        /// <param name="FromDate">The FromDate<see cref="DateTime"/></param>
        /// <returns>The <see cref="JsonResult"/></returns>
        public JsonResult CheckIsDateAlreadyUsed(DateTime FromDate)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(FromDate)))
                {
                    return Json("Error", JsonRequestBehavior.AllowGet);
                }

                var data = _ITimeSheet.CheckIsDateAlreadyUsed(FromDate, Convert.ToInt32(Session["UserID"]));
                return Json(data, JsonRequestBehavior.AllowGet);
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

                var data = _ITimeSheet.DeleteTimesheetByTimeSheetMasterID(TimeSheetMasterID, Convert.ToInt32(Session["UserID"]));

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
        /// The ProjectSelectCount
        /// </summary>
        /// <param name="timesheetmodel">The timesheetmodel<see cref="TimeSheetModel"/></param>
        /// <returns>The <see cref="int"/></returns>
        private int ProjectSelectCount(TimeSheetModel timesheetmodel)
        {
            try
            {
                int count = 0;
                if (timesheetmodel.ProjectID1 != null && (timesheetmodel.texttotal_p1 != null && timesheetmodel.texttotal_p1 != 0))
                {
                    count++;
                }

                if (timesheetmodel.ProjectID2 != null && (timesheetmodel.texttotal_p2 != null && timesheetmodel.texttotal_p2 != 0))
                {
                    count++;
                }

                if (timesheetmodel.ProjectID3 != null && (timesheetmodel.texttotal_p3 != null && timesheetmodel.texttotal_p3 != 0))
                {
                    count++;
                }

                if (timesheetmodel.ProjectID3 != null && (timesheetmodel.texttotal_p3 != null && timesheetmodel.texttotal_p3 != 0))
                {
                    count++;
                }

                if (timesheetmodel.ProjectID4 != null && (timesheetmodel.texttotal_p4 != null && timesheetmodel.texttotal_p4 != 0))
                {
                    count++;
                }

                if (timesheetmodel.ProjectID5 != null && (timesheetmodel.texttotal_p5 != null && timesheetmodel.texttotal_p5 != 0))
                {
                    count++;
                }

                if (timesheetmodel.ProjectID6 != null && (timesheetmodel.texttotal_p6 != null && timesheetmodel.texttotal_p6 != 0))
                {
                    count++;
                }

                return count;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The Save
        /// </summary>
        /// <param name="timesheetmodel">The timesheetmodel<see cref="TimeSheetModel"/></param>
        /// <param name="TimeSheetMasterID">The TimeSheetMasterID<see cref="int"/></param>
        public void Save(TimeSheetModel timesheetmodel, int TimeSheetMasterID)
        {
            try
            {
                if (timesheetmodel.ProjectID1 != null && (timesheetmodel.texttotal_p1 != null && timesheetmodel.texttotal_p1 != 0))
                {
                    var date1 = timesheetmodel.hdtext1;
                    var DaysofWeek1 = timesheetmodel.DaysofWeek1;
                    var value1 = timesheetmodel.text1_p1;
                    SaveTimeSheetDetail(DaysofWeek1, value1, date1, timesheetmodel.ProjectID1, TimeSheetMasterID);

                    var date2 = timesheetmodel.hdtext2;
                    var DaysofWeek2 = timesheetmodel.DaysofWeek2;
                    var value2 = timesheetmodel.text2_p1;
                    SaveTimeSheetDetail(DaysofWeek2, value2, date2, timesheetmodel.ProjectID1, TimeSheetMasterID);

                    var date3 = timesheetmodel.hdtext3;
                    var DaysofWeek3 = timesheetmodel.DaysofWeek3;
                    var value3 = timesheetmodel.text3_p1;
                    SaveTimeSheetDetail(DaysofWeek3, value3, date3, timesheetmodel.ProjectID1, TimeSheetMasterID);

                    var date4 = timesheetmodel.hdtext4;
                    var DaysofWeek4 = timesheetmodel.DaysofWeek4;
                    var value4 = timesheetmodel.text4_p1;
                    SaveTimeSheetDetail(DaysofWeek4, value4, date4, timesheetmodel.ProjectID1, TimeSheetMasterID);

                    var date5 = timesheetmodel.hdtext5;
                    var DaysofWeek5 = timesheetmodel.DaysofWeek5;
                    var value5 = timesheetmodel.text5_p1;
                    SaveTimeSheetDetail(DaysofWeek5, value5, date5, timesheetmodel.ProjectID1, TimeSheetMasterID);

                    var date6 = timesheetmodel.hdtext6;
                    var DaysofWeek6 = timesheetmodel.DaysofWeek6;
                    var value6 = timesheetmodel.text6_p1;
                    SaveTimeSheetDetail(DaysofWeek6, value6, date6, timesheetmodel.ProjectID1, TimeSheetMasterID);

                    var date7 = timesheetmodel.hdtext7;
                    var DaysofWeek7 = timesheetmodel.DaysofWeek7;
                    var value7 = timesheetmodel.text7_p1;
                    SaveTimeSheetDetail(DaysofWeek7, value7, date7, timesheetmodel.ProjectID1, TimeSheetMasterID);

                }

                if (timesheetmodel.ProjectID2 != null && (timesheetmodel.texttotal_p2 != null && timesheetmodel.texttotal_p2 != 0))
                {
                    var date1 = timesheetmodel.hdtext1;
                    var DaysofWeek1 = timesheetmodel.DaysofWeek1;
                    var value1 = timesheetmodel.text1_p2;
                    SaveTimeSheetDetail(DaysofWeek1, value1, date1, timesheetmodel.ProjectID2, TimeSheetMasterID);

                    var date2 = timesheetmodel.hdtext2;
                    var DaysofWeek2 = timesheetmodel.DaysofWeek2;
                    var value2 = timesheetmodel.text2_p2;
                    SaveTimeSheetDetail(DaysofWeek2, value2, date2, timesheetmodel.ProjectID2, TimeSheetMasterID);

                    var date3 = timesheetmodel.hdtext3;
                    var DaysofWeek3 = timesheetmodel.DaysofWeek3;
                    var value3 = timesheetmodel.text3_p2;
                    SaveTimeSheetDetail(DaysofWeek3, value3, date3, timesheetmodel.ProjectID2, TimeSheetMasterID);

                    var date4 = timesheetmodel.hdtext4;
                    var DaysofWeek4 = timesheetmodel.DaysofWeek4;
                    var value4 = timesheetmodel.text4_p2;
                    SaveTimeSheetDetail(DaysofWeek4, value4, date4, timesheetmodel.ProjectID2, TimeSheetMasterID);

                    var date5 = timesheetmodel.hdtext5;
                    var DaysofWeek5 = timesheetmodel.DaysofWeek5;
                    var value5 = timesheetmodel.text5_p2;
                    SaveTimeSheetDetail(DaysofWeek5, value5, date5, timesheetmodel.ProjectID2, TimeSheetMasterID);

                    var date6 = timesheetmodel.hdtext6;
                    var DaysofWeek6 = timesheetmodel.DaysofWeek6;
                    var value6 = timesheetmodel.text6_p2;
                    SaveTimeSheetDetail(DaysofWeek6, value6, date6, timesheetmodel.ProjectID2, TimeSheetMasterID);

                    var date7 = timesheetmodel.hdtext7;
                    var DaysofWeek7 = timesheetmodel.DaysofWeek7;
                    var value7 = timesheetmodel.text7_p2;
                    SaveTimeSheetDetail(DaysofWeek7, value7, date7, timesheetmodel.ProjectID2, TimeSheetMasterID);

                }

                if (timesheetmodel.ProjectID3 != null && (timesheetmodel.texttotal_p3 != null && timesheetmodel.texttotal_p3 != 0))
                {
                    var date1 = timesheetmodel.hdtext1;
                    var DaysofWeek1 = timesheetmodel.DaysofWeek1;
                    var value1 = timesheetmodel.text1_p3;
                    SaveTimeSheetDetail(DaysofWeek1, value1, date1, timesheetmodel.ProjectID3, TimeSheetMasterID);

                    var date2 = timesheetmodel.hdtext2;
                    var DaysofWeek2 = timesheetmodel.DaysofWeek2;
                    var value2 = timesheetmodel.text2_p3;
                    SaveTimeSheetDetail(DaysofWeek2, value2, date2, timesheetmodel.ProjectID3, TimeSheetMasterID);

                    var date3 = timesheetmodel.hdtext3;
                    var DaysofWeek3 = timesheetmodel.DaysofWeek3;
                    var value3 = timesheetmodel.text3_p3;
                    SaveTimeSheetDetail(DaysofWeek3, value3, date3, timesheetmodel.ProjectID3, TimeSheetMasterID);

                    var date4 = timesheetmodel.hdtext4;
                    var DaysofWeek4 = timesheetmodel.DaysofWeek4;
                    var value4 = timesheetmodel.text4_p3;
                    SaveTimeSheetDetail(DaysofWeek4, value4, date4, timesheetmodel.ProjectID3, TimeSheetMasterID);

                    var date5 = timesheetmodel.hdtext5;
                    var DaysofWeek5 = timesheetmodel.DaysofWeek5;
                    var value5 = timesheetmodel.text5_p3;
                    SaveTimeSheetDetail(DaysofWeek5, value5, date5, timesheetmodel.ProjectID3, TimeSheetMasterID);

                    var date6 = timesheetmodel.hdtext6;
                    var DaysofWeek6 = timesheetmodel.DaysofWeek6;
                    var value6 = timesheetmodel.text6_p3;
                    SaveTimeSheetDetail(DaysofWeek6, value6, date6, timesheetmodel.ProjectID3, TimeSheetMasterID);

                    var date7 = timesheetmodel.hdtext7;
                    var DaysofWeek7 = timesheetmodel.DaysofWeek7;
                    var value7 = timesheetmodel.text7_p3;
                    SaveTimeSheetDetail(DaysofWeek7, value7, date7, timesheetmodel.ProjectID3, TimeSheetMasterID);

                }

                if (timesheetmodel.ProjectID4 != null && (timesheetmodel.texttotal_p4 != null && timesheetmodel.texttotal_p4 != 0))
                {
                    var date1 = timesheetmodel.hdtext1;
                    var DaysofWeek1 = timesheetmodel.DaysofWeek1;
                    var value1 = timesheetmodel.text1_p4;
                    SaveTimeSheetDetail(DaysofWeek1, value1, date1, timesheetmodel.ProjectID4, TimeSheetMasterID);

                    var date2 = timesheetmodel.hdtext2;
                    var DaysofWeek2 = timesheetmodel.DaysofWeek2;
                    var value2 = timesheetmodel.text2_p4;
                    SaveTimeSheetDetail(DaysofWeek2, value2, date2, timesheetmodel.ProjectID4, TimeSheetMasterID);

                    var date3 = timesheetmodel.hdtext3;
                    var DaysofWeek3 = timesheetmodel.DaysofWeek3;
                    var value3 = timesheetmodel.text3_p4;
                    SaveTimeSheetDetail(DaysofWeek3, value3, date3, timesheetmodel.ProjectID4, TimeSheetMasterID);

                    var date4 = timesheetmodel.hdtext4;
                    var DaysofWeek4 = timesheetmodel.DaysofWeek4;
                    var value4 = timesheetmodel.text4_p4;
                    SaveTimeSheetDetail(DaysofWeek4, value4, date4, timesheetmodel.ProjectID4, TimeSheetMasterID);

                    var date5 = timesheetmodel.hdtext5;
                    var DaysofWeek5 = timesheetmodel.DaysofWeek5;
                    var value5 = timesheetmodel.text5_p4;
                    SaveTimeSheetDetail(DaysofWeek5, value5, date5, timesheetmodel.ProjectID4, TimeSheetMasterID);

                    var date6 = timesheetmodel.hdtext6;
                    var DaysofWeek6 = timesheetmodel.DaysofWeek6;
                    var value6 = timesheetmodel.text6_p4;
                    SaveTimeSheetDetail(DaysofWeek6, value6, date6, timesheetmodel.ProjectID4, TimeSheetMasterID);

                    var date7 = timesheetmodel.hdtext7;
                    var DaysofWeek7 = timesheetmodel.DaysofWeek7;
                    var value7 = timesheetmodel.text7_p4;
                    SaveTimeSheetDetail(DaysofWeek7, value7, date7, timesheetmodel.ProjectID4, TimeSheetMasterID);

                }

                if (timesheetmodel.ProjectID5 != null && (timesheetmodel.texttotal_p5 != null && timesheetmodel.texttotal_p5 != 0))
                {
                    var date1 = timesheetmodel.hdtext1;
                    var DaysofWeek1 = timesheetmodel.DaysofWeek1;
                    var value1 = timesheetmodel.text1_p5;
                    SaveTimeSheetDetail(DaysofWeek1, value1, date1, timesheetmodel.ProjectID5, TimeSheetMasterID);

                    var date2 = timesheetmodel.hdtext2;
                    var DaysofWeek2 = timesheetmodel.DaysofWeek2;
                    var value2 = timesheetmodel.text2_p5;
                    SaveTimeSheetDetail(DaysofWeek2, value2, date2, timesheetmodel.ProjectID5, TimeSheetMasterID);

                    var date3 = timesheetmodel.hdtext3;
                    var DaysofWeek3 = timesheetmodel.DaysofWeek3;
                    var value3 = timesheetmodel.text3_p5;
                    SaveTimeSheetDetail(DaysofWeek3, value3, date3, timesheetmodel.ProjectID5, TimeSheetMasterID);

                    var date4 = timesheetmodel.hdtext4;
                    var DaysofWeek4 = timesheetmodel.DaysofWeek4;
                    var value4 = timesheetmodel.text4_p5;
                    SaveTimeSheetDetail(DaysofWeek4, value4, date4, timesheetmodel.ProjectID5, TimeSheetMasterID);

                    var date5 = timesheetmodel.hdtext5;
                    var DaysofWeek5 = timesheetmodel.DaysofWeek5;
                    var value5 = timesheetmodel.text5_p5;
                    SaveTimeSheetDetail(DaysofWeek5, value5, date5, timesheetmodel.ProjectID5, TimeSheetMasterID);

                    var date6 = timesheetmodel.hdtext6;
                    var DaysofWeek6 = timesheetmodel.DaysofWeek6;
                    var value6 = timesheetmodel.text6_p5;
                    SaveTimeSheetDetail(DaysofWeek6, value6, date6, timesheetmodel.ProjectID5, TimeSheetMasterID);

                    var date7 = timesheetmodel.hdtext7;
                    var DaysofWeek7 = timesheetmodel.DaysofWeek7;
                    var value7 = timesheetmodel.text7_p5;
                    SaveTimeSheetDetail(DaysofWeek7, value7, date7, timesheetmodel.ProjectID5, TimeSheetMasterID);

                }

                if (timesheetmodel.ProjectID6 != null && (timesheetmodel.texttotal_p6 != null && timesheetmodel.texttotal_p6 != 0))
                {
                    var date1 = timesheetmodel.hdtext1;
                    var DaysofWeek1 = timesheetmodel.DaysofWeek1;
                    var value1 = timesheetmodel.text1_p6;
                    SaveTimeSheetDetail(DaysofWeek1, value1, date1, timesheetmodel.ProjectID6, TimeSheetMasterID);

                    var date2 = timesheetmodel.hdtext2;
                    var DaysofWeek2 = timesheetmodel.DaysofWeek2;
                    var value2 = timesheetmodel.text2_p6;
                    SaveTimeSheetDetail(DaysofWeek2, value2, date2, timesheetmodel.ProjectID6, TimeSheetMasterID);

                    var date3 = timesheetmodel.hdtext3;
                    var DaysofWeek3 = timesheetmodel.DaysofWeek3;
                    var value3 = timesheetmodel.text3_p6;
                    SaveTimeSheetDetail(DaysofWeek3, value3, date3, timesheetmodel.ProjectID6, TimeSheetMasterID);

                    var date4 = timesheetmodel.hdtext4;
                    var DaysofWeek4 = timesheetmodel.DaysofWeek4;
                    var value4 = timesheetmodel.text4_p6;
                    SaveTimeSheetDetail(DaysofWeek4, value4, date4, timesheetmodel.ProjectID6, TimeSheetMasterID);

                    var date5 = timesheetmodel.hdtext5;
                    var DaysofWeek5 = timesheetmodel.DaysofWeek5;
                    var value5 = timesheetmodel.text5_p6;
                    SaveTimeSheetDetail(DaysofWeek5, value5, date5, timesheetmodel.ProjectID6, TimeSheetMasterID);

                    var date6 = timesheetmodel.hdtext6;
                    var DaysofWeek6 = timesheetmodel.DaysofWeek6;
                    var value6 = timesheetmodel.text6_p6;
                    SaveTimeSheetDetail(DaysofWeek6, value6, date6, timesheetmodel.ProjectID6, TimeSheetMasterID);

                    var date7 = timesheetmodel.hdtext7;
                    var DaysofWeek7 = timesheetmodel.DaysofWeek7;
                    var value7 = timesheetmodel.text7_p6;
                    SaveTimeSheetDetail(DaysofWeek7, value7, date7, timesheetmodel.ProjectID6, TimeSheetMasterID);

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The SaveDescription
        /// </summary>
        /// <param name="timesheetmodel">The timesheetmodel<see cref="TimeSheetModel"/></param>
        /// <param name="TimeSheetMasterID">The TimeSheetMasterID<see cref="int"/></param>
        public void SaveDescription(TimeSheetModel timesheetmodel, int TimeSheetMasterID)
        {
            try
            {
                if (timesheetmodel.ProjectID1 != null)
                {
                    InsertDescriptionDetail(timesheetmodel.ProjectID1, TimeSheetMasterID, timesheetmodel.Description_p1);
                }

                if (timesheetmodel.ProjectID2 != null)
                {
                    InsertDescriptionDetail(timesheetmodel.ProjectID2, TimeSheetMasterID, timesheetmodel.Description_p2);
                }

                if (timesheetmodel.ProjectID3 != null)
                {
                    InsertDescriptionDetail(timesheetmodel.ProjectID3, TimeSheetMasterID, timesheetmodel.Description_p3);
                }

                if (timesheetmodel.ProjectID4 != null)
                {
                    InsertDescriptionDetail(timesheetmodel.ProjectID4, TimeSheetMasterID, timesheetmodel.Description_p4);
                }

                if (timesheetmodel.ProjectID5 != null)
                {
                    InsertDescriptionDetail(timesheetmodel.ProjectID5, TimeSheetMasterID, timesheetmodel.Description_p5);
                }

                if (timesheetmodel.ProjectID6 != null)
                {
                    InsertDescriptionDetail(timesheetmodel.ProjectID6, TimeSheetMasterID, timesheetmodel.Description_p6);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The InsertTimeSheetAudit
        /// </summary>
        /// <param name="TimeSheetMasterID">The TimeSheetMasterID<see cref="int"/></param>
        /// <param name="Status">The Status<see cref="int"/></param>
        /// <returns>The <see cref="TimeSheetAuditTB"/></returns>
        private TimeSheetAuditTB InsertTimeSheetAudit(int TimeSheetMasterID, int Status)
        {
            try
            {
                TimeSheetAuditTB objAuditTB = new TimeSheetAuditTB
                {
                    ApprovalTimeSheetLogID = 0,
                    TimeSheetID = TimeSheetMasterID,
                    Status = Status,
                    CreatedOn = DateTime.Now,
                    Comment = string.Empty,
                    ApprovalUser = _IUsers.GetAdminIDbyUserID(Convert.ToInt32(Session["UserID"])),
                    ProcessedDate = DateTime.Now,
                    UserID = Convert.ToInt32(Session["UserID"])
                };
                return objAuditTB;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The InsertDescriptionDetail
        /// </summary>
        /// <param name="ProjectID">The ProjectID<see cref="int?"/></param>
        /// <param name="TimeSheetMasterID">The TimeSheetMasterID<see cref="int"/></param>
        /// <param name="Description">The Description<see cref="string"/></param>
        [NonAction]
        private void InsertDescriptionDetail(int? ProjectID, int TimeSheetMasterID, string Description)
        {
            try
            {
                DescriptionTB objtimesheetdetails = new DescriptionTB
                {
                    DescriptionID = 0,
                    ProjectID = ProjectID,
                    UserID = Convert.ToInt32(Session["UserID"]),
                    CreatedOn = DateTime.Now,
                    TimeSheetMasterID = TimeSheetMasterID,
                    Description = Description
                };
                int? TimeSheetID = _ITimeSheet.InsertDescription(objtimesheetdetails);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
