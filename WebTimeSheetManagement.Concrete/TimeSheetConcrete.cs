namespace WebTimeSheetManagement.Concrete
{
    using Dapper;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Entity;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.SqlServer;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Linq.Dynamic;
    using WebTimeSheetManagement.Interface;
    using WebTimeSheetManagement.Models;

    /// <summary>
    /// Defines the <see cref="TimeSheetConcrete" />
    /// </summary>
    public class TimeSheetConcrete : ITimeSheet
    {
        /// <summary>
        /// The AddTimeSheetMaster
        /// </summary>
        /// <param name="TimeSheetMaster">The TimeSheetMaster<see cref="TimeSheetMaster"/></param>
        /// <returns>The <see cref="int"/></returns>
        public int AddTimeSheetMaster(TimeSheetMaster TimeSheetMaster)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    _context.TimeSheetMaster.Add(TimeSheetMaster);
                    _context.SaveChanges();
                    int id = TimeSheetMaster.TimeSheetMasterID; // Yes it's here
                    return id;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The AddTimeSheetDetail
        /// </summary>
        /// <param name="TimeSheetDetails">The TimeSheetDetails<see cref="TimeSheetDetails"/></param>
        /// <returns>The <see cref="int"/></returns>
        public int AddTimeSheetDetail(TimeSheetDetails TimeSheetDetails)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    _context.TimeSheetDetails.Add(TimeSheetDetails);
                    _context.SaveChanges();
                    int id = TimeSheetDetails.TimeSheetID; // Yes it's here
                    return id;
                }
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
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public bool CheckIsDateAlreadyUsed(DateTime FromDate, int UserID)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    var result = (from timesheetdetails in _context.TimeSheetDetails
                                  where timesheetdetails.Period == FromDate && timesheetdetails.UserID == UserID
                                  select timesheetdetails).Count();

                    return result > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The ShowTimeSheet
        /// </summary>
        /// <param name="sortColumn">The sortColumn<see cref="string"/></param>
        /// <param name="sortColumnDir">The sortColumnDir<see cref="string"/></param>
        /// <param name="Search">The Search<see cref="string"/></param>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="IQueryable{TimeSheetMasterView}"/></returns>
        public IQueryable<TimeSheetMasterView> ShowTimeSheet(string sortColumn, string sortColumnDir, string Search, int UserID)
        {
            var _context = new DatabaseContext();

            var IQueryabletimesheet = (from timesheetmaster in _context.TimeSheetMaster

                                       where timesheetmaster.UserID == UserID
                                       select new TimeSheetMasterView
                                       {
                                           TimeSheetStatus = timesheetmaster.TimeSheetStatus == 1 ? "Submitted" : timesheetmaster.TimeSheetStatus == 2 ? "Approved" : "Rejected",
                                           Comment = timesheetmaster.Comment,
                                           TimeSheetMasterID = timesheetmaster.TimeSheetMasterID,
                                           FromDate = SqlFunctions.DateName("day", timesheetmaster.FromDate).Trim() + "/" +
                   SqlFunctions.StringConvert((double)timesheetmaster.FromDate.Value.Month).TrimStart() + "/" +
                   SqlFunctions.DateName("year", timesheetmaster.FromDate),
                                           ToDate = SqlFunctions.DateName("day", timesheetmaster.ToDate).Trim() + "/" +
                   SqlFunctions.StringConvert((double)timesheetmaster.ToDate.Value.Month).TrimStart() + "/" +
                   SqlFunctions.DateName("year", timesheetmaster.ToDate),

                                           CreatedOn = SqlFunctions.DateName("day", timesheetmaster.CreatedOn).Trim() + "/" +
                   SqlFunctions.StringConvert((double)timesheetmaster.CreatedOn.Value.Month).TrimStart() + "/" +
                   SqlFunctions.DateName("year", timesheetmaster.CreatedOn),
                                           TotalHours = timesheetmaster.TotalHours
                                       });

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                IQueryabletimesheet = IQueryabletimesheet.OrderBy(sortColumn + " " + sortColumnDir);
            }
            if (!string.IsNullOrEmpty(Search))
            {
                IQueryabletimesheet = IQueryabletimesheet.Where(m => m.FromDate == Search);
            }

            return IQueryabletimesheet;
        }

        /// <summary>
        /// The TimesheetDetailsbyTimeSheetMasterID
        /// </summary>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <param name="TimeSheetMasterID">The TimeSheetMasterID<see cref="int"/></param>
        /// <returns>The <see cref="List{TimeSheetDetailsView}"/></returns>
        public List<TimeSheetDetailsView> TimesheetDetailsbyTimeSheetMasterID(int UserID, int TimeSheetMasterID)
        {
            using (var _context = new DatabaseContext())
            {
                var data = (from timesheet in _context.TimeSheetDetails
                            join project in _context.ProjectMaster on timesheet.ProjectID equals project.ProjectID
                            where timesheet.UserID == UserID && timesheet.TimeSheetMasterID == TimeSheetMasterID
                            select new TimeSheetDetailsView
                            {
                                TimeSheetID = timesheet.TimeSheetID,
                                CreatedOn = SqlFunctions.DateName("day", timesheet.CreatedOn).Trim() + "/" +
                    SqlFunctions.StringConvert((double)timesheet.CreatedOn.Value.Month).TrimStart() + "/" +
                    SqlFunctions.DateName("year", timesheet.CreatedOn),
                                Period = SqlFunctions.DateName("day", timesheet.Period).Trim() + "/" +
                    SqlFunctions.StringConvert((double)timesheet.Period.Value.Month).TrimStart() + "/" +
                    SqlFunctions.DateName("year", timesheet.Period),
                                DaysofWeek = timesheet.DaysofWeek,
                                Hours = timesheet.Hours,
                                ProjectName = project.ProjectName,
                                TimeSheetMasterID = timesheet.TimeSheetMasterID

                            }).ToList();

                return data;
            }
        }

        /// <summary>
        /// The TimesheetDetailsbyTimeSheetMasterID
        /// </summary>
        /// <param name="TimeSheetMasterID">The TimeSheetMasterID<see cref="int"/></param>
        /// <returns>The <see cref="List{TimeSheetDetailsView}"/></returns>
        public List<TimeSheetDetailsView> TimesheetDetailsbyTimeSheetMasterID(int TimeSheetMasterID)
        {
            using (var _context = new DatabaseContext())
            {
                var data = (from timesheet in _context.TimeSheetDetails
                            join project in _context.ProjectMaster on timesheet.ProjectID equals project.ProjectID
                            where timesheet.TimeSheetMasterID == TimeSheetMasterID
                            select new TimeSheetDetailsView
                            {
                                TimeSheetID = timesheet.TimeSheetID,
                                CreatedOn = SqlFunctions.DateName("day", timesheet.CreatedOn).Trim() + "/" +
                    SqlFunctions.StringConvert((double)timesheet.CreatedOn.Value.Month).TrimStart() + "/" +
                    SqlFunctions.DateName("year", timesheet.CreatedOn),
                                Period = SqlFunctions.DateName("day", timesheet.Period).Trim() + "/" +
                    SqlFunctions.StringConvert((double)timesheet.Period.Value.Month).TrimStart() + "/" +
                    SqlFunctions.DateName("year", timesheet.Period),
                                DaysofWeek = timesheet.DaysofWeek,
                                Hours = timesheet.Hours,
                                ProjectName = project.ProjectName,
                                TimeSheetMasterID = timesheet.TimeSheetMasterID

                            }).ToList();

                return data;
            }
        }

        /// <summary>
        /// The DeleteTimesheetByTimeSheetMasterID
        /// </summary>
        /// <param name="TimeSheetMasterID">The TimeSheetMasterID<see cref="int"/></param>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="int"/></returns>
        public int DeleteTimesheetByTimeSheetMasterID(int TimeSheetMasterID, int UserID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
                {
                    var param = new DynamicParameters();
                    param.Add("@TimeSheetID", TimeSheetMasterID);
                    param.Add("@UserID", UserID);
                    return con.Execute("Usp_DeleteTimeSheet", param, null, 0, System.Data.CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The ShowAllTimeSheet
        /// </summary>
        /// <param name="sortColumn">The sortColumn<see cref="string"/></param>
        /// <param name="sortColumnDir">The sortColumnDir<see cref="string"/></param>
        /// <param name="Search">The Search<see cref="string"/></param>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="IQueryable{TimeSheetMasterView}"/></returns>
        public IQueryable<TimeSheetMasterView> ShowAllTimeSheet(string sortColumn, string sortColumnDir, string Search, int UserID)
        {
            var _context = new DatabaseContext();

            var IQueryabletimesheet = (from timesheetmaster in _context.TimeSheetMaster
                                       join registration in _context.Registration on timesheetmaster.UserID equals registration.RegistrationID
                                       join AssignedRolesAdmin in _context.AssignedRoles on registration.RegistrationID equals AssignedRolesAdmin.RegistrationID
                                       where AssignedRolesAdmin.AssignToAdmin == UserID
                                       select new TimeSheetMasterView
                                       {
                                           TimeSheetStatus = timesheetmaster.TimeSheetStatus == 1 ? "Submitted" : timesheetmaster.TimeSheetStatus == 2 ? "Approved" : "Rejected",
                                           Comment = timesheetmaster.Comment,
                                           TimeSheetMasterID = timesheetmaster.TimeSheetMasterID,
                                           FromDate =
                (
                     DbFunctions.Right(SqlFunctions.StringConvert((double?)SqlFunctions.DatePart("yyyy", timesheetmaster.FromDate)), 4)

                                            + "-"
                    + DbFunctions.Right(string.Concat(" ", SqlFunctions.StringConvert((double?)SqlFunctions.DatePart("mm", timesheetmaster.FromDate))), 2)
                        + "-"
                        + DbFunctions.Right(string.Concat(" ", SqlFunctions.StringConvert((double?)SqlFunctions.DatePart("dd", timesheetmaster.FromDate))), 2)
                       ).Replace(" ", "0"),

                                           ToDate =
                     (
                     DbFunctions.Right(SqlFunctions.StringConvert((double?)SqlFunctions.DatePart("yyyy", timesheetmaster.ToDate)), 4)

                                            + "-"
                    + DbFunctions.Right(string.Concat(" ", SqlFunctions.StringConvert((double?)SqlFunctions.DatePart("mm", timesheetmaster.ToDate))), 2)
                        + "-"
                        + DbFunctions.Right(string.Concat(" ", SqlFunctions.StringConvert((double?)SqlFunctions.DatePart("dd", timesheetmaster.ToDate))), 2)
                       ).Replace(" ", "0"),

                                           CreatedOn = SqlFunctions.DateName("day", timesheetmaster.CreatedOn).Trim() + "/" +
                   SqlFunctions.StringConvert((double)timesheetmaster.CreatedOn.Value.Month).TrimStart() + "/" +
                   SqlFunctions.DateName("year", timesheetmaster.CreatedOn),
                                           TotalHours = timesheetmaster.TotalHours,
                                           Username = registration.Username,
                                           SubmittedMonth = SqlFunctions.DateName("MONTH", timesheetmaster.ToDate)

                                       });

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                IQueryabletimesheet = IQueryabletimesheet.OrderBy(sortColumn + " " + sortColumnDir);
            }
            if (!string.IsNullOrEmpty(Search))
            {
                IQueryabletimesheet = IQueryabletimesheet.Where(m => m.FromDate == Search || m.Username == Search);
            }

            return IQueryabletimesheet;
        }

        /// <summary>
        /// The GetPeriodsbyTimeSheetMasterID
        /// </summary>
        /// <param name="TimeSheetMasterID">The TimeSheetMasterID<see cref="int"/></param>
        /// <returns>The <see cref="List{GetPeriods}"/></returns>
        public List<GetPeriods> GetPeriodsbyTimeSheetMasterID(int TimeSheetMasterID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
            {
                con.Open();
                try
                {
                    var param = new DynamicParameters();
                    param.Add("@TimeSheetMasterID", TimeSheetMasterID);
                    var result = con.Query<GetPeriods>("Usp_GetPeriodsbyTimeSheetMasterID", param, null, true, 0, System.Data.CommandType.StoredProcedure).ToList();
                    if (result.Count > 0)
                    {
                        return result;
                    }
                    else
                    {
                        return new List<GetPeriods>();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// The GetProjectNamesbyTimeSheetMasterID
        /// </summary>
        /// <param name="TimeSheetMasterID">The TimeSheetMasterID<see cref="int"/></param>
        /// <returns>The <see cref="List{GetProjectNames}"/></returns>
        public List<GetProjectNames> GetProjectNamesbyTimeSheetMasterID(int TimeSheetMasterID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
            {
                con.Open();
                try
                {
                    var param = new DynamicParameters();
                    param.Add("@TimeSheetMasterID", TimeSheetMasterID);
                    var result = con.Query<GetProjectNames>("Usp_GetProjectNamesbyTimeSheetMasterID", param, null, true, 0, System.Data.CommandType.StoredProcedure).ToList();
                    if (result.Count > 0)
                    {
                        return result;
                    }
                    else
                    {
                        return new List<GetProjectNames>();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// The UpdateTimeSheetStatus
        /// </summary>
        /// <param name="timesheetapprovalmodel">The timesheetapprovalmodel<see cref="TimeSheetApproval"/></param>
        /// <param name="Status">The Status<see cref="int"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public bool UpdateTimeSheetStatus(TimeSheetApproval timesheetapprovalmodel, int Status)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
            {
                con.Open();
                SqlTransaction sql = con.BeginTransaction();

                try
                {
                    var param = new DynamicParameters();
                    param.Add("@TimeSheetMasterID", timesheetapprovalmodel.TimeSheetMasterID);
                    param.Add("@Comment", timesheetapprovalmodel.Comment);
                    param.Add("@TimeSheetStatus", Status);
                    var result = con.Execute("Usp_UpdateTimeSheetStatus", param, sql, 0, System.Data.CommandType.StoredProcedure);
                    if (result > 0)
                    {
                        sql.Commit();
                        return true;
                    }
                    else
                    {
                        sql.Rollback();
                        return false;
                    }
                }
                catch (Exception)
                {
                    sql.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// The InsertTimeSheetAuditLog
        /// </summary>
        /// <param name="timesheetaudittb">The timesheetaudittb<see cref="TimeSheetAuditTB"/></param>
        public void InsertTimeSheetAuditLog(TimeSheetAuditTB timesheetaudittb)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    _context.TimeSheetAuditTB.Add(timesheetaudittb);
                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The DeleteTimesheetByOnlyTimeSheetMasterID
        /// </summary>
        /// <param name="TimeSheetMasterID">The TimeSheetMasterID<see cref="int"/></param>
        /// <returns>The <see cref="int"/></returns>
        public int DeleteTimesheetByOnlyTimeSheetMasterID(int TimeSheetMasterID)
        {
            int resultTimeSheetMaster = 0;
            int resultTimeSheetDetails = 0;
            try
            {
                using (var _context = new DatabaseContext())
                {
                    var timesheetcount = (from ex in _context.TimeSheetMaster
                                          where ex.TimeSheetMasterID == TimeSheetMasterID
                                          select ex).Count();

                    if (timesheetcount > 0)
                    {
                        TimeSheetMaster timesheet = (from ex in _context.TimeSheetMaster
                                                     where ex.TimeSheetMasterID == TimeSheetMasterID
                                                     select ex).SingleOrDefault();

                        _context.TimeSheetMaster.Remove(timesheet);
                        resultTimeSheetMaster = _context.SaveChanges();
                    }

                    var timesheetdetailscount = (from ex in _context.TimeSheetDetails
                                                 where ex.TimeSheetMasterID == TimeSheetMasterID
                                                 select ex).Count();

                    if (timesheetdetailscount > 0)
                    {
                        var timesheetdetails = (from ex in _context.TimeSheetDetails
                                                where ex.TimeSheetMasterID == TimeSheetMasterID
                                                select ex).ToList();

                        _context.TimeSheetDetails.RemoveRange(timesheetdetails);
                        resultTimeSheetDetails = _context.SaveChanges();
                    }

                    if (resultTimeSheetMaster > 0 || resultTimeSheetDetails > 0)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The InsertDescription
        /// </summary>
        /// <param name="DescriptionTB">The DescriptionTB<see cref="DescriptionTB"/></param>
        /// <returns>The <see cref="int?"/></returns>
        public int? InsertDescription(DescriptionTB DescriptionTB)
        {
            using (var _context = new DatabaseContext())
            {
                _context.DescriptionTB.Add(DescriptionTB);
                _context.SaveChanges();
                int? id = DescriptionTB.DescriptionID; // Yes it's here
                return id;
            }
        }

        /// <summary>
        /// The GetTimeSheetsCountByAdminID
        /// </summary>
        /// <param name="AdminID">The AdminID<see cref="string"/></param>
        /// <returns>The <see cref="DisplayViewModel"/></returns>
        public DisplayViewModel GetTimeSheetsCountByAdminID(string AdminID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
            {
                var param = new DynamicParameters();
                param.Add("@AdminID", AdminID);
                return con.Query<DisplayViewModel>("Usp_GetTimeSheetsCountByAdminID", param, null, true, 0, System.Data.CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        /// <summary>
        /// The ShowAllApprovedTimeSheet
        /// </summary>
        /// <param name="sortColumn">The sortColumn<see cref="string"/></param>
        /// <param name="sortColumnDir">The sortColumnDir<see cref="string"/></param>
        /// <param name="Search">The Search<see cref="string"/></param>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="IQueryable{TimeSheetMasterView}"/></returns>
        public IQueryable<TimeSheetMasterView> ShowAllApprovedTimeSheet(string sortColumn, string sortColumnDir, string Search, int UserID)
        {
            var _context = new DatabaseContext();

            var IQueryabletimesheet = (from timesheetmaster in _context.TimeSheetMaster
                                       join timeSheetAuditTB in _context.TimeSheetAuditTB on timesheetmaster.TimeSheetMasterID equals timeSheetAuditTB.TimeSheetID
                                       join registration in _context.Registration on timesheetmaster.UserID equals registration.RegistrationID
                                       join AssignedRolesAdmin in _context.AssignedRoles on registration.RegistrationID equals AssignedRolesAdmin.RegistrationID
                                       where AssignedRolesAdmin.AssignToAdmin == UserID && timeSheetAuditTB.Status == 2
                                       select new TimeSheetMasterView
                                       {
                                           TimeSheetStatus = timesheetmaster.TimeSheetStatus == 1 ? "Submitted" : timesheetmaster.TimeSheetStatus == 2 ? "Approved" : "Rejected",
                                           Comment = timesheetmaster.Comment,
                                           TimeSheetMasterID = timesheetmaster.TimeSheetMasterID,
                                           FromDate =
                (
                     DbFunctions.Right(SqlFunctions.StringConvert((double?)SqlFunctions.DatePart("yyyy", timesheetmaster.FromDate)), 4)

                                            + "-"
                    + DbFunctions.Right(string.Concat(" ", SqlFunctions.StringConvert((double?)SqlFunctions.DatePart("mm", timesheetmaster.FromDate))), 2)
                        + "-"
                        + DbFunctions.Right(string.Concat(" ", SqlFunctions.StringConvert((double?)SqlFunctions.DatePart("dd", timesheetmaster.FromDate))), 2)
                       ).Replace(" ", "0"),

                                           ToDate =
                     (
                     DbFunctions.Right(SqlFunctions.StringConvert((double?)SqlFunctions.DatePart("yyyy", timesheetmaster.ToDate)), 4)

                                            + "-"
                    + DbFunctions.Right(string.Concat(" ", SqlFunctions.StringConvert((double?)SqlFunctions.DatePart("mm", timesheetmaster.ToDate))), 2)
                        + "-"
                        + DbFunctions.Right(string.Concat(" ", SqlFunctions.StringConvert((double?)SqlFunctions.DatePart("dd", timesheetmaster.ToDate))), 2)
                       ).Replace(" ", "0"),

                                           CreatedOn = SqlFunctions.DateName("day", timesheetmaster.CreatedOn).Trim() + "/" +
                   SqlFunctions.StringConvert((double)timesheetmaster.CreatedOn.Value.Month).TrimStart() + "/" +
                   SqlFunctions.DateName("year", timesheetmaster.CreatedOn),
                                           TotalHours = timesheetmaster.TotalHours,
                                           Username = registration.Username,
                                           SubmittedMonth = SqlFunctions.DateName("MONTH", timesheetmaster.ToDate)

                                       });

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                IQueryabletimesheet = IQueryabletimesheet.OrderBy(sortColumn + " " + sortColumnDir);
            }
            if (!string.IsNullOrEmpty(Search))
            {
                IQueryabletimesheet = IQueryabletimesheet.Where(m => m.FromDate == Search || m.Username == Search);
            }

            return IQueryabletimesheet;
        }

        /// <summary>
        /// The ShowAllRejectTimeSheet
        /// </summary>
        /// <param name="sortColumn">The sortColumn<see cref="string"/></param>
        /// <param name="sortColumnDir">The sortColumnDir<see cref="string"/></param>
        /// <param name="Search">The Search<see cref="string"/></param>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="IQueryable{TimeSheetMasterView}"/></returns>
        public IQueryable<TimeSheetMasterView> ShowAllRejectTimeSheet(string sortColumn, string sortColumnDir, string Search, int UserID)
        {
            var _context = new DatabaseContext();

            var IQueryabletimesheet = (from timesheetmaster in _context.TimeSheetMaster
                                       join timeSheetAuditTB in _context.TimeSheetAuditTB on timesheetmaster.TimeSheetMasterID equals timeSheetAuditTB.TimeSheetID
                                       join registration in _context.Registration on timesheetmaster.UserID equals registration.RegistrationID
                                       join AssignedRolesAdmin in _context.AssignedRoles on registration.RegistrationID equals AssignedRolesAdmin.RegistrationID
                                       where AssignedRolesAdmin.AssignToAdmin == UserID && timeSheetAuditTB.Status == 3
                                       select new TimeSheetMasterView
                                       {
                                           TimeSheetStatus = timesheetmaster.TimeSheetStatus == 1 ? "Submitted" : timesheetmaster.TimeSheetStatus == 2 ? "Approved" : "Rejected",
                                           Comment = timesheetmaster.Comment,
                                           TimeSheetMasterID = timesheetmaster.TimeSheetMasterID,
                                           FromDate =
                (
                     DbFunctions.Right(SqlFunctions.StringConvert((double?)SqlFunctions.DatePart("yyyy", timesheetmaster.FromDate)), 4)

                                            + "-"
                    + DbFunctions.Right(string.Concat(" ", SqlFunctions.StringConvert((double?)SqlFunctions.DatePart("mm", timesheetmaster.FromDate))), 2)
                        + "-"
                        + DbFunctions.Right(string.Concat(" ", SqlFunctions.StringConvert((double?)SqlFunctions.DatePart("dd", timesheetmaster.FromDate))), 2)
                       ).Replace(" ", "0"),

                                           ToDate =
                     (
                     DbFunctions.Right(SqlFunctions.StringConvert((double?)SqlFunctions.DatePart("yyyy", timesheetmaster.ToDate)), 4)

                                            + "-"
                    + DbFunctions.Right(string.Concat(" ", SqlFunctions.StringConvert((double?)SqlFunctions.DatePart("mm", timesheetmaster.ToDate))), 2)
                        + "-"
                        + DbFunctions.Right(string.Concat(" ", SqlFunctions.StringConvert((double?)SqlFunctions.DatePart("dd", timesheetmaster.ToDate))), 2)
                       ).Replace(" ", "0"),

                                           CreatedOn = SqlFunctions.DateName("day", timesheetmaster.CreatedOn).Trim() + "/" +
                   SqlFunctions.StringConvert((double)timesheetmaster.CreatedOn.Value.Month).TrimStart() + "/" +
                   SqlFunctions.DateName("year", timesheetmaster.CreatedOn),
                                           TotalHours = timesheetmaster.TotalHours,
                                           Username = registration.Username,
                                           SubmittedMonth = SqlFunctions.DateName("MONTH", timesheetmaster.ToDate)

                                       });

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                IQueryabletimesheet = IQueryabletimesheet.OrderBy(sortColumn + " " + sortColumnDir);
            }
            if (!string.IsNullOrEmpty(Search))
            {
                IQueryabletimesheet = IQueryabletimesheet.Where(m => m.FromDate == Search || m.Username == Search);
            }

            return IQueryabletimesheet;
        }

        /// <summary>
        /// The ShowAllSubmittedTimeSheet
        /// </summary>
        /// <param name="sortColumn">The sortColumn<see cref="string"/></param>
        /// <param name="sortColumnDir">The sortColumnDir<see cref="string"/></param>
        /// <param name="Search">The Search<see cref="string"/></param>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="IQueryable{TimeSheetMasterView}"/></returns>
        public IQueryable<TimeSheetMasterView> ShowAllSubmittedTimeSheet(string sortColumn, string sortColumnDir, string Search, int UserID)
        {
            var _context = new DatabaseContext();

            var IQueryabletimesheet = (from timesheetmaster in _context.TimeSheetMaster
                                       join timeSheetAuditTB in _context.TimeSheetAuditTB on timesheetmaster.TimeSheetMasterID equals timeSheetAuditTB.TimeSheetID
                                       join registration in _context.Registration on timesheetmaster.UserID equals registration.RegistrationID
                                       join AssignedRolesAdmin in _context.AssignedRoles on registration.RegistrationID equals AssignedRolesAdmin.RegistrationID
                                       where AssignedRolesAdmin.AssignToAdmin == UserID && timeSheetAuditTB.Status == 1
                                       select new TimeSheetMasterView
                                       {
                                           TimeSheetStatus = timesheetmaster.TimeSheetStatus == 1 ? "Submitted" : timesheetmaster.TimeSheetStatus == 2 ? "Approved" : "Rejected",
                                           Comment = timesheetmaster.Comment,
                                           TimeSheetMasterID = timesheetmaster.TimeSheetMasterID,
                                           FromDate =
                (
                     DbFunctions.Right(SqlFunctions.StringConvert((double?)SqlFunctions.DatePart("yyyy", timesheetmaster.FromDate)), 4)

                                            + "-"
                    + DbFunctions.Right(string.Concat(" ", SqlFunctions.StringConvert((double?)SqlFunctions.DatePart("mm", timesheetmaster.FromDate))), 2)
                        + "-"
                        + DbFunctions.Right(string.Concat(" ", SqlFunctions.StringConvert((double?)SqlFunctions.DatePart("dd", timesheetmaster.FromDate))), 2)
                       ).Replace(" ", "0"),

                                           ToDate =
                     (
                     DbFunctions.Right(SqlFunctions.StringConvert((double?)SqlFunctions.DatePart("yyyy", timesheetmaster.ToDate)), 4)

                                            + "-"
                    + DbFunctions.Right(string.Concat(" ", SqlFunctions.StringConvert((double?)SqlFunctions.DatePart("mm", timesheetmaster.ToDate))), 2)
                        + "-"
                        + DbFunctions.Right(string.Concat(" ", SqlFunctions.StringConvert((double?)SqlFunctions.DatePart("dd", timesheetmaster.ToDate))), 2)
                       ).Replace(" ", "0"),

                                           CreatedOn = SqlFunctions.DateName("day", timesheetmaster.CreatedOn).Trim() + "/" +
                   SqlFunctions.StringConvert((double)timesheetmaster.CreatedOn.Value.Month).TrimStart() + "/" +
                   SqlFunctions.DateName("year", timesheetmaster.CreatedOn),
                                           TotalHours = timesheetmaster.TotalHours,
                                           Username = registration.Username,
                                           SubmittedMonth = SqlFunctions.DateName("MONTH", timesheetmaster.ToDate)

                                       });

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                IQueryabletimesheet = IQueryabletimesheet.OrderBy(sortColumn + " " + sortColumnDir);
            }
            if (!string.IsNullOrEmpty(Search))
            {
                IQueryabletimesheet = IQueryabletimesheet.Where(m => m.FromDate == Search || m.Username == Search);
            }

            return IQueryabletimesheet;
        }

        /// <summary>
        /// The GetTimeSheetsCountByUserID
        /// </summary>
        /// <param name="UserID">The UserID<see cref="string"/></param>
        /// <returns>The <see cref="DisplayViewModel"/></returns>
        public DisplayViewModel GetTimeSheetsCountByUserID(string UserID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
            {
                var param = new DynamicParameters();
                param.Add("@UserID", UserID);
                return con.Query<DisplayViewModel>("Usp_GetTimeSheetsCountByUserID", param, null, true, 0, System.Data.CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        /// <summary>
        /// The ShowTimeSheetStatus
        /// </summary>
        /// <param name="sortColumn">The sortColumn<see cref="string"/></param>
        /// <param name="sortColumnDir">The sortColumnDir<see cref="string"/></param>
        /// <param name="Search">The Search<see cref="string"/></param>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <param name="TimeSheetStatus">The TimeSheetStatus<see cref="int"/></param>
        /// <returns>The <see cref="IQueryable{TimeSheetMasterView}"/></returns>
        public IQueryable<TimeSheetMasterView> ShowTimeSheetStatus(string sortColumn, string sortColumnDir, string Search, int UserID, int TimeSheetStatus)
        {
            var _context = new DatabaseContext();

            var IQueryabletimesheet = (from timesheetmaster in _context.TimeSheetMaster
                                       where timesheetmaster.UserID == UserID && timesheetmaster.TimeSheetStatus == TimeSheetStatus
                                       select new TimeSheetMasterView
                                       {
                                           TimeSheetStatus = timesheetmaster.TimeSheetStatus == 1 ? "Submitted" : timesheetmaster.TimeSheetStatus == 2 ? "Approved" : "Rejected",
                                           Comment = timesheetmaster.Comment,
                                           TimeSheetMasterID = timesheetmaster.TimeSheetMasterID,
                                           FromDate = SqlFunctions.DateName("day", timesheetmaster.FromDate).Trim() + "/" +
                   SqlFunctions.StringConvert((double)timesheetmaster.FromDate.Value.Month).TrimStart() + "/" +
                   SqlFunctions.DateName("year", timesheetmaster.FromDate),
                                           ToDate = SqlFunctions.DateName("day", timesheetmaster.ToDate).Trim() + "/" +
                   SqlFunctions.StringConvert((double)timesheetmaster.ToDate.Value.Month).TrimStart() + "/" +
                   SqlFunctions.DateName("year", timesheetmaster.ToDate),

                                           CreatedOn = SqlFunctions.DateName("day", timesheetmaster.CreatedOn).Trim() + "/" +
                   SqlFunctions.StringConvert((double)timesheetmaster.CreatedOn.Value.Month).TrimStart() + "/" +
                   SqlFunctions.DateName("year", timesheetmaster.CreatedOn),
                                           TotalHours = timesheetmaster.TotalHours
                                       });

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                IQueryabletimesheet = IQueryabletimesheet.OrderBy(sortColumn + " " + sortColumnDir);
            }
            if (!string.IsNullOrEmpty(Search))
            {
                IQueryabletimesheet = IQueryabletimesheet.Where(m => m.FromDate == Search);
            }

            return IQueryabletimesheet;
        }

        /// <summary>
        /// The UpdateTimeSheetAuditStatus
        /// </summary>
        /// <param name="TimeSheetID">The TimeSheetID<see cref="int"/></param>
        /// <param name="Comment">The Comment<see cref="string"/></param>
        /// <param name="Status">The Status<see cref="int"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public bool UpdateTimeSheetAuditStatus(int TimeSheetID, string Comment, int Status)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
            {
                con.Open();
                SqlTransaction sql = con.BeginTransaction();

                try
                {
                    var param = new DynamicParameters();
                    param.Add("@TimeSheetID", TimeSheetID);
                    param.Add("@Comment", Comment);
                    param.Add("@Status", Status);
                    var result = con.Execute("Usp_ChangeTimesheetStatus", param, sql, 0, System.Data.CommandType.StoredProcedure);
                    if (result > 0)
                    {
                        sql.Commit();
                        return true;
                    }
                    else
                    {
                        sql.Rollback();
                        return false;
                    }
                }
                catch (Exception)
                {
                    sql.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// The IsTimesheetALreadyProcessed
        /// </summary>
        /// <param name="TimeSheetID">The TimeSheetID<see cref="int"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public bool IsTimesheetALreadyProcessed(int TimeSheetID)
        {
            using (var _context = new DatabaseContext())
            {
                var data = (from timesheet in _context.TimeSheetAuditTB
                            where timesheet.TimeSheetID == TimeSheetID && timesheet.Status != 1
                            select timesheet).Count();

                return data > 0;
            }
        }
    }
}
