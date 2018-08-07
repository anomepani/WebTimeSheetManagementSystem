namespace WebTimeSheetManagement.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using WebTimeSheetManagement.Models;

    /// <summary>
    /// Defines the <see cref="ITimeSheetExport" />
    /// </summary>
    public interface ITimeSheetExport
    {
        /// <summary>
        /// The GetReportofTimeSheet
        /// </summary>
        /// <param name="FromDate">The FromDate<see cref="DateTime?"/></param>
        /// <param name="ToDate">The ToDate<see cref="DateTime?"/></param>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="DataSet"/></returns>
        DataSet GetReportofTimeSheet(DateTime? FromDate, DateTime? ToDate, int UserID);

        /// <summary>
        /// The GetWeekTimeSheetDetails
        /// </summary>
        /// <param name="TimeSheetMasterID">The TimeSheetMasterID<see cref="int"/></param>
        /// <returns>The <see cref="DataSet"/></returns>
        DataSet GetWeekTimeSheetDetails(int TimeSheetMasterID);

        /// <summary>
        /// The ListofEmployees
        /// </summary>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="List{Registration}"/></returns>
        List<Registration> ListofEmployees(int UserID);

        /// <summary>
        /// The GetTimeSheetMasterIDTimeSheet
        /// </summary>
        /// <param name="FromDate">The FromDate<see cref="DateTime?"/></param>
        /// <param name="ToDate">The ToDate<see cref="DateTime?"/></param>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="DataSet"/></returns>
        DataSet GetTimeSheetMasterIDTimeSheet(DateTime? FromDate, DateTime? ToDate, int UserID);

        /// <summary>
        /// The GetUsernamebyRegistrationID
        /// </summary>
        /// <param name="RegistrationID">The RegistrationID<see cref="int"/></param>
        /// <returns>The <see cref="string"/></returns>
        string GetUsernamebyRegistrationID(int RegistrationID);

        /// <summary>
        /// The GetTimeSheetMasterIDTimeSheet
        /// </summary>
        /// <param name="FromDate">The FromDate<see cref="DateTime?"/></param>
        /// <param name="ToDate">The ToDate<see cref="DateTime?"/></param>
        /// <returns>The <see cref="DataSet"/></returns>
        DataSet GetTimeSheetMasterIDTimeSheet(DateTime? FromDate, DateTime? ToDate);
    }
}
