namespace WebTimeSheetManagement.Interface
{
    using System.Linq;
    using WebTimeSheetManagement.Models;

    /// <summary>
    /// Defines the <see cref="IUsers" />
    /// </summary>
    public interface IUsers
    {
        /// <summary>
        /// The ShowallUsers
        /// </summary>
        /// <param name="sortColumn">The sortColumn<see cref="string"/></param>
        /// <param name="sortColumnDir">The sortColumnDir<see cref="string"/></param>
        /// <param name="Search">The Search<see cref="string"/></param>
        /// <returns>The <see cref="IQueryable{RegistrationViewSummaryModel}"/></returns>
        IQueryable<RegistrationViewSummaryModel> ShowallUsers(string sortColumn, string sortColumnDir, string Search);

        /// <summary>
        /// The GetUserDetailsByRegistrationID
        /// </summary>
        /// <param name="RegistrationID">The RegistrationID<see cref="int?"/></param>
        /// <returns>The <see cref="RegistrationViewDetailsModel"/></returns>
        RegistrationViewDetailsModel GetUserDetailsByRegistrationID(int? RegistrationID);

        /// <summary>
        /// The ShowallAdmin
        /// </summary>
        /// <param name="sortColumn">The sortColumn<see cref="string"/></param>
        /// <param name="sortColumnDir">The sortColumnDir<see cref="string"/></param>
        /// <param name="Search">The Search<see cref="string"/></param>
        /// <returns>The <see cref="IQueryable{RegistrationViewSummaryModel}"/></returns>
        IQueryable<RegistrationViewSummaryModel> ShowallAdmin(string sortColumn, string sortColumnDir, string Search);

        /// <summary>
        /// The GetAdminDetailsByRegistrationID
        /// </summary>
        /// <param name="RegistrationID">The RegistrationID<see cref="int?"/></param>
        /// <returns>The <see cref="RegistrationViewDetailsModel"/></returns>
        RegistrationViewDetailsModel GetAdminDetailsByRegistrationID(int? RegistrationID);

        /// <summary>
        /// The ShowallUsersUnderAdmin
        /// </summary>
        /// <param name="sortColumn">The sortColumn<see cref="string"/></param>
        /// <param name="sortColumnDir">The sortColumnDir<see cref="string"/></param>
        /// <param name="Search">The Search<see cref="string"/></param>
        /// <param name="RegistrationID">The RegistrationID<see cref="int?"/></param>
        /// <returns>The <see cref="IQueryable{RegistrationViewSummaryModel}"/></returns>
        IQueryable<RegistrationViewSummaryModel> ShowallUsersUnderAdmin(string sortColumn, string sortColumnDir, string Search, int? RegistrationID);

        /// <summary>
        /// The GetTotalAdminsCount
        /// </summary>
        /// <returns>The <see cref="int"/></returns>
        int GetTotalAdminsCount();

        /// <summary>
        /// The GetTotalUsersCount
        /// </summary>
        /// <returns>The <see cref="int"/></returns>
        int GetTotalUsersCount();

        /// <summary>
        /// The GetUserIDbyTimesheetID
        /// </summary>
        /// <param name="TimeSheetMasterID">The TimeSheetMasterID<see cref="int"/></param>
        /// <returns>The <see cref="int"/></returns>
        int GetUserIDbyTimesheetID(int TimeSheetMasterID);

        /// <summary>
        /// The GetUserIDbyExpenseID
        /// </summary>
        /// <param name="ExpenseID">The ExpenseID<see cref="int"/></param>
        /// <returns>The <see cref="int"/></returns>
        int GetUserIDbyExpenseID(int ExpenseID);

        /// <summary>
        /// The GetAdminIDbyUserID
        /// </summary>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="int"/></returns>
        int GetAdminIDbyUserID(int UserID);
    }
}
