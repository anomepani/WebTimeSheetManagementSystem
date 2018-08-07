namespace WebTimeSheetManagement.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using WebTimeSheetManagement.Models;

    /// <summary>
    /// Defines the <see cref="ITimeSheet" />
    /// </summary>
    public interface ITimeSheet
    {
        /// <summary>
        /// The AddTimeSheetMaster
        /// </summary>
        /// <param name="TimeSheetMaster">The TimeSheetMaster<see cref="TimeSheetMaster"/></param>
        /// <returns>The <see cref="int"/></returns>
        int AddTimeSheetMaster(TimeSheetMaster TimeSheetMaster);

        /// <summary>
        /// The AddTimeSheetDetail
        /// </summary>
        /// <param name="TimeSheetDetails">The TimeSheetDetails<see cref="TimeSheetDetails"/></param>
        /// <returns>The <see cref="int"/></returns>
        int AddTimeSheetDetail(TimeSheetDetails TimeSheetDetails);

        /// <summary>
        /// The CheckIsDateAlreadyUsed
        /// </summary>
        /// <param name="FromDate">The FromDate<see cref="DateTime"/></param>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="bool"/></returns>
        bool CheckIsDateAlreadyUsed(DateTime FromDate, int UserID);

        /// <summary>
        /// The ShowTimeSheet
        /// </summary>
        /// <param name="sortColumn">The sortColumn<see cref="string"/></param>
        /// <param name="sortColumnDir">The sortColumnDir<see cref="string"/></param>
        /// <param name="Search">The Search<see cref="string"/></param>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="IQueryable{TimeSheetMasterView}"/></returns>
        IQueryable<TimeSheetMasterView> ShowTimeSheet(string sortColumn, string sortColumnDir, string Search, int UserID);

        /// <summary>
        /// The TimesheetDetailsbyTimeSheetMasterID
        /// </summary>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <param name="TimeSheetMasterID">The TimeSheetMasterID<see cref="int"/></param>
        /// <returns>The <see cref="List{TimeSheetDetailsView}"/></returns>
        List<TimeSheetDetailsView> TimesheetDetailsbyTimeSheetMasterID(int UserID, int TimeSheetMasterID);

        /// <summary>
        /// The DeleteTimesheetByTimeSheetMasterID
        /// </summary>
        /// <param name="TimeSheetMasterID">The TimeSheetMasterID<see cref="int"/></param>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="int"/></returns>
        int DeleteTimesheetByTimeSheetMasterID(int TimeSheetMasterID, int UserID);

        /// <summary>
        /// The ShowAllTimeSheet
        /// </summary>
        /// <param name="sortColumn">The sortColumn<see cref="string"/></param>
        /// <param name="sortColumnDir">The sortColumnDir<see cref="string"/></param>
        /// <param name="Search">The Search<see cref="string"/></param>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="IQueryable{TimeSheetMasterView}"/></returns>
        IQueryable<TimeSheetMasterView> ShowAllTimeSheet(string sortColumn, string sortColumnDir, string Search, int UserID);

        /// <summary>
        /// The TimesheetDetailsbyTimeSheetMasterID
        /// </summary>
        /// <param name="TimeSheetMasterID">The TimeSheetMasterID<see cref="int"/></param>
        /// <returns>The <see cref="List{TimeSheetDetailsView}"/></returns>
        List<TimeSheetDetailsView> TimesheetDetailsbyTimeSheetMasterID(int TimeSheetMasterID);

        /// <summary>
        /// The GetPeriodsbyTimeSheetMasterID
        /// </summary>
        /// <param name="TimeSheetMasterID">The TimeSheetMasterID<see cref="int"/></param>
        /// <returns>The <see cref="List{GetPeriods}"/></returns>
        List<GetPeriods> GetPeriodsbyTimeSheetMasterID(int TimeSheetMasterID);

        /// <summary>
        /// The GetProjectNamesbyTimeSheetMasterID
        /// </summary>
        /// <param name="TimeSheetMasterID">The TimeSheetMasterID<see cref="int"/></param>
        /// <returns>The <see cref="List{GetProjectNames}"/></returns>
        List<GetProjectNames> GetProjectNamesbyTimeSheetMasterID(int TimeSheetMasterID);

        /// <summary>
        /// The UpdateTimeSheetStatus
        /// </summary>
        /// <param name="timesheetapprovalmodel">The timesheetapprovalmodel<see cref="TimeSheetApproval"/></param>
        /// <param name="Status">The Status<see cref="int"/></param>
        /// <returns>The <see cref="bool"/></returns>
        bool UpdateTimeSheetStatus(TimeSheetApproval timesheetapprovalmodel, int Status);

        /// <summary>
        /// The InsertTimeSheetAuditLog
        /// </summary>
        /// <param name="timesheetaudittb">The timesheetaudittb<see cref="TimeSheetAuditTB"/></param>
        void InsertTimeSheetAuditLog(TimeSheetAuditTB timesheetaudittb);

        /// <summary>
        /// The DeleteTimesheetByOnlyTimeSheetMasterID
        /// </summary>
        /// <param name="TimeSheetMasterID">The TimeSheetMasterID<see cref="int"/></param>
        /// <returns>The <see cref="int"/></returns>
        int DeleteTimesheetByOnlyTimeSheetMasterID(int TimeSheetMasterID);

        /// <summary>
        /// The InsertDescription
        /// </summary>
        /// <param name="DescriptionTB">The DescriptionTB<see cref="DescriptionTB"/></param>
        /// <returns>The <see cref="int?"/></returns>
        int? InsertDescription(DescriptionTB DescriptionTB);

        /// <summary>
        /// The GetTimeSheetsCountByAdminID
        /// </summary>
        /// <param name="AdminID">The AdminID<see cref="string"/></param>
        /// <returns>The <see cref="DisplayViewModel"/></returns>
        DisplayViewModel GetTimeSheetsCountByAdminID(string AdminID);

        /// <summary>
        /// The ShowAllApprovedTimeSheet
        /// </summary>
        /// <param name="sortColumn">The sortColumn<see cref="string"/></param>
        /// <param name="sortColumnDir">The sortColumnDir<see cref="string"/></param>
        /// <param name="Search">The Search<see cref="string"/></param>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="IQueryable{TimeSheetMasterView}"/></returns>
        IQueryable<TimeSheetMasterView> ShowAllApprovedTimeSheet(string sortColumn, string sortColumnDir, string Search, int UserID);

        /// <summary>
        /// The ShowAllRejectTimeSheet
        /// </summary>
        /// <param name="sortColumn">The sortColumn<see cref="string"/></param>
        /// <param name="sortColumnDir">The sortColumnDir<see cref="string"/></param>
        /// <param name="Search">The Search<see cref="string"/></param>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="IQueryable{TimeSheetMasterView}"/></returns>
        IQueryable<TimeSheetMasterView> ShowAllRejectTimeSheet(string sortColumn, string sortColumnDir, string Search, int UserID);

        /// <summary>
        /// The ShowAllSubmittedTimeSheet
        /// </summary>
        /// <param name="sortColumn">The sortColumn<see cref="string"/></param>
        /// <param name="sortColumnDir">The sortColumnDir<see cref="string"/></param>
        /// <param name="Search">The Search<see cref="string"/></param>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="IQueryable{TimeSheetMasterView}"/></returns>
        IQueryable<TimeSheetMasterView> ShowAllSubmittedTimeSheet(string sortColumn, string sortColumnDir, string Search, int UserID);

        /// <summary>
        /// The GetTimeSheetsCountByUserID
        /// </summary>
        /// <param name="UserID">The UserID<see cref="string"/></param>
        /// <returns>The <see cref="DisplayViewModel"/></returns>
        DisplayViewModel GetTimeSheetsCountByUserID(string UserID);

        /// <summary>
        /// The ShowTimeSheetStatus
        /// </summary>
        /// <param name="sortColumn">The sortColumn<see cref="string"/></param>
        /// <param name="sortColumnDir">The sortColumnDir<see cref="string"/></param>
        /// <param name="Search">The Search<see cref="string"/></param>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <param name="TimeSheetStatus">The TimeSheetStatus<see cref="int"/></param>
        /// <returns>The <see cref="IQueryable{TimeSheetMasterView}"/></returns>
        IQueryable<TimeSheetMasterView> ShowTimeSheetStatus(string sortColumn, string sortColumnDir, string Search, int UserID, int TimeSheetStatus);

        /// <summary>
        /// The UpdateTimeSheetAuditStatus
        /// </summary>
        /// <param name="TimeSheetID">The TimeSheetID<see cref="int"/></param>
        /// <param name="Comment">The Comment<see cref="string"/></param>
        /// <param name="Status">The Status<see cref="int"/></param>
        /// <returns>The <see cref="bool"/></returns>
        bool UpdateTimeSheetAuditStatus(int TimeSheetID, string Comment, int Status);

        /// <summary>
        /// The IsTimesheetALreadyProcessed
        /// </summary>
        /// <param name="TimeSheetID">The TimeSheetID<see cref="int"/></param>
        /// <returns>The <see cref="bool"/></returns>
        bool IsTimesheetALreadyProcessed(int TimeSheetID);
    }
}
