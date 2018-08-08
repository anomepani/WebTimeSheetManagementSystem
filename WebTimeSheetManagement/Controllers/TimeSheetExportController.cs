namespace WebTimeSheetManagement.Controllers
{
    using System;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using WebTimeSheetManagement.Concrete;
    using WebTimeSheetManagement.Filters;
    using WebTimeSheetManagement.Helpers;
    using WebTimeSheetManagement.Interface;
    using WebTimeSheetManagement.Models;

    /// <summary>
    /// Defines the <see cref="TimeSheetExportController" />
    /// </summary>
    [ValidateAdminSession]
    public class TimeSheetExportController : Controller
    {
        /// <summary>
        /// Defines the _ITimeSheetExport
        /// </summary>
        private readonly ITimeSheetExport _ITimeSheetExport;

        /// <summary>
        /// Defines the _ITimeSheet
        /// </summary>
        private readonly ITimeSheet _ITimeSheet;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSheetExportController"/> class.
        /// </summary>
        public TimeSheetExportController()
        {
            _ITimeSheetExport = new TimeSheetExportConcrete();
            _ITimeSheet = new TimeSheetConcrete();
        }

        /// <summary>
        /// The Report
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpGet]
        public ActionResult Report()
        {
            return View(new TimeSheetExcelExportModel());
        }

        // GET: TimeSheetExport
        /// <summary>
        /// The ExportToExcel
        /// </summary>
        /// <param name="objtimesheet">The objtimesheet<see cref="TimeSheetExcelExportModel"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpPost]
        public ActionResult ExportToExcel(TimeSheetExcelExportModel objtimesheet)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Report", objtimesheet);
                }

                dt.Columns.Add("TotalHours", typeof(string));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("ProjectName", typeof(string));
                dt.Columns.Add("DaysofWeek", typeof(string));
                dt.Columns.Add("Hours", typeof(string));
                dt.Columns.Add("Period", typeof(string));
                dt.Columns.Add("CreatedOn", typeof(string));

                var singlelist = _ITimeSheetExport.GetReportofTimeSheet(objtimesheet.FromDate, objtimesheet.ToDate, Convert.ToInt32(Session["AdminUser"]));

                if (singlelist != null)
                {
                    if (singlelist.Tables.Count > 0)
                    {
                        if (singlelist.Tables[0].Rows.Count == 0)
                        {
                            TempData["NoExportMessage"] = "No Data to Export";
                            return View("Report");
                        }
                        else
                        {
                            for (int i = 0; i < singlelist.Tables[0].Rows.Count; i++)
                            {
                                int TimeSheetMasterID = Convert.ToInt32(singlelist.Tables[0].Rows[i]["TimeSheetMasterID"]);
                                var multi = _ITimeSheetExport.GetWeekTimeSheetDetails(TimeSheetMasterID);

                                DataRow row = dt.NewRow();
                                row["TotalHours"] = Convert.ToString(singlelist.Tables[0].Rows[i]["TotalHours"]);
                                row["Name"] = Convert.ToString(singlelist.Tables[0].Rows[i]["Name"]);
                                row["ProjectName"] = string.Empty;
                                row["DaysofWeek"] = string.Empty;
                                row["Hours"] = string.Empty;
                                row["Period"] = string.Empty;
                                row["CreatedOn"] = string.Empty;
                                dt.Rows.Add(row);

                                DataRow row1 = dt.NewRow();
                                row1["TotalHours"] = string.Empty;
                                row1["Name"] = string.Empty;
                                row1["ProjectName"] = string.Empty;
                                row1["DaysofWeek"] = string.Empty;
                                row1["Hours"] = string.Empty;
                                row1["Period"] = string.Empty;
                                row1["CreatedOn"] = string.Empty;
                                dt.Rows.Add(row1);

                                for (int j = 0; j < multi.Tables[0].Rows.Count; j++)
                                {
                                    DataRow row2 = dt.NewRow();
                                    row2["TotalHours"] = string.Empty;
                                    row2["Name"] = string.Empty;
                                    row2["ProjectName"] = Convert.ToString(multi.Tables[0].Rows[j]["ProjectName"]);
                                    row2["DaysofWeek"] = Convert.ToString(multi.Tables[0].Rows[j]["DaysofWeek"]);
                                    row2["Hours"] = Convert.ToString(multi.Tables[0].Rows[j]["Hours"]);
                                    row2["Period"] = Convert.ToString(multi.Tables[0].Rows[j]["Period"]);
                                    row2["CreatedOn"] = Convert.ToString(multi.Tables[0].Rows[j]["CreatedOn"]);
                                    dt.Rows.Add(row2);
                                }
                            }

                            ds.Tables.Add(dt);
                            var gv = new GridView
                            {
                                DataSource = ds
                            };
                            gv.DataBind();
                            Response.ClearContent();
                            Response.Buffer = true;
                            Response.AddHeader("content-disposition", "attachment; filename=TimeSheetDetails.xls");
                            Response.ContentType = "application/ms-excel";
                            Response.Charset = string.Empty;
                            StringWriter objStringWriter = new StringWriter();
                            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
                            gv.RenderControl(objHtmlTextWriter);
                            Response.Output.Write(objStringWriter.ToString());
                            Response.Flush();
                            Response.End();
                            return View("Report");
                        }
                    }
                }

                TempData["NoExportMessage"] = "No Data to Export";
                return View("Report");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                ds.Dispose();
            }
        }

        /// <summary>
        /// The TimeSheetReport
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpGet]
        public ActionResult TimeSheetReport()
        {
            return View(new TimeSheetExportUserModel());
        }

        /// <summary>
        /// The TimeSheetReport
        /// </summary>
        /// <param name="objtimesheet">The objtimesheet<see cref="TimeSheetExportUserModel"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpPost]
        public ActionResult TimeSheetReport(TimeSheetExportUserModel objtimesheet)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                dt.Columns.Add("ProjectName", typeof(string));
                dt.Columns.Add("Sunday", typeof(string));
                dt.Columns.Add("Monday", typeof(string));
                dt.Columns.Add("Tuesday", typeof(string));
                dt.Columns.Add("Wednesday", typeof(string));
                dt.Columns.Add("Thursday", typeof(string));
                dt.Columns.Add("Friday", typeof(string));
                dt.Columns.Add("Saturday", typeof(string));
                dt.Columns.Add("Total", typeof(string));
                dt.Columns.Add("Description", typeof(string));
                var filename = _ITimeSheetExport.GetUsernamebyRegistrationID(objtimesheet.RegistrationID);

                var timesheetdata = _ITimeSheetExport.GetTimeSheetMasterIDTimeSheet(objtimesheet.FromDate, objtimesheet.ToDate, objtimesheet.RegistrationID);

                if (timesheetdata != null)
                {
                    if (timesheetdata.Tables.Count > 0)
                    {
                        if (timesheetdata.Tables[0].Rows.Count == 0)
                        {
                            TempData["NoExportMessage"] = "No Data to Export";
                            return View("TimeSheetReport", new TimeSheetExportUserModel());
                        }
                        else
                        {
                            for (int k = 0; k < timesheetdata.Tables[0].Rows.Count; k++)
                            {
                                var timesheetID = Convert.ToInt32(timesheetdata.Tables[0].Rows[k]["TimeSheetMasterID"]);

                                var data = _ITimeSheet.GetPeriodsbyTimeSheetMasterID(Convert.ToInt32(timesheetID));

                                if (data.Count == 0)
                                {
                                    TempData["NoExportMessage"] = "No Data to Export";
                                    return View("TimeSheetReport", new TimeSheetExportUserModel());
                                }

                                DataRow row2 = dt.NewRow();
                                row2["ProjectName"] = string.Empty;
                                row2["Sunday"] = string.Empty;
                                row2["Monday"] = string.Empty;
                                row2["Tuesday"] = string.Empty;
                                row2["Wednesday"] = string.Empty;
                                row2["Thursday"] = string.Empty;
                                row2["Friday"] = string.Empty;
                                row2["Saturday"] = string.Empty;
                                row2["Total"] = string.Empty;
                                row2["Description"] = string.Empty;
                                dt.Rows.Add(row2);

                                DataRow row = dt.NewRow();
                                row["ProjectName"] = string.Empty;
                                row["Sunday"] = data[0].Period;
                                row["Monday"] = data[1].Period;
                                row["Tuesday"] = data[2].Period;
                                row["Wednesday"] = data[3].Period;
                                row["Thursday"] = data[4].Period;
                                row["Friday"] = data[5].Period;
                                row["Saturday"] = data[6].Period;
                                row["Total"] = string.Empty;
                                row["Description"] = string.Empty;
                                dt.Rows.Add(row);

                                var ListofProjectNames = _ITimeSheet.GetProjectNamesbyTimeSheetMasterID(Convert.ToInt32(timesheetID));

                                for (int i = 0; i < ListofProjectNames.Count; i++)
                                {
                                    var ListofHours = MethodonViews.GetHoursbyTimeSheetMasterID(timesheetID, ListofProjectNames[i].ProjectID);
                                    var ListofDescription = WebTimeSheetManagement.Helpers.MethodonViews.GetDescriptionbyTimeSheetMasterID(timesheetID, ListofProjectNames[i].ProjectID);
                                    DataRow row1 = dt.NewRow();
                                    row1["ProjectName"] = ListofProjectNames[i].ProjectName;
                                    row1["Sunday"] = ListofHours[0].Hours;
                                    row1["Monday"] = ListofHours[1].Hours;
                                    row1["Tuesday"] = ListofHours[2].Hours;
                                    row1["Wednesday"] = ListofHours[3].Hours;
                                    row1["Thursday"] = ListofHours[4].Hours;
                                    row1["Friday"] = ListofHours[5].Hours;
                                    row1["Saturday"] = ListofHours[6].Hours;
                                    row1["Total"] = ListofHours[7].Hours;
                                    row1["Description"] = Convert.ToString(ListofDescription);
                                    dt.Rows.Add(row1);
                                }
                            }
                            ds.Tables.Add(dt);
                            var gv = new GridView
                            {
                                DataSource = ds
                            };
                            gv.DataBind();
                            Response.ClearContent();
                            Response.Buffer = true;
                            Response.AddHeader("content-disposition", "attachment; filename=" + filename.Trim() + ".xls");
                            Response.ContentType = "application/ms-excel";
                            Response.Charset = string.Empty;
                            StringWriter objStringWriter = new StringWriter();
                            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
                            gv.RenderControl(objHtmlTextWriter);
                            Response.Output.Write(objStringWriter.ToString());
                            Response.Flush();
                            Response.End();
                        }
                    }
                }
                return View("TimeSheetReport", new TimeSheetExportUserModel());
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                ds.Dispose();
            }
        }

        /// <summary>
        /// The ListofEmployees
        /// </summary>
        /// <returns>The <see cref="JsonResult"/></returns>
        public JsonResult ListofEmployees()
        {
            try
            {
                var ListofEmployees = _ITimeSheetExport.ListofEmployees(Convert.ToInt32(Session["AdminUser"]));
                return Json(ListofEmployees, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
