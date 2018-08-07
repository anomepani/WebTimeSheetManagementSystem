namespace WebTimeSheetManagement.Interface
{
    using WebTimeSheetManagement.Models;

    /// <summary>
    /// Defines the <see cref="ILogin" />
    /// </summary>
    public interface ILogin
    {
        /// <summary>
        /// The ValidateUser
        /// </summary>
        /// <param name="userName">The userName<see cref="string"/></param>
        /// <param name="passWord">The passWord<see cref="string"/></param>
        /// <returns>The <see cref="Registration"/></returns>
        Registration ValidateUser(string userName, string passWord);

        /// <summary>
        /// The UpdatePassword
        /// </summary>
        /// <param name="NewPassword">The NewPassword<see cref="string"/></param>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="bool"/></returns>
        bool UpdatePassword(string NewPassword, int UserID);

        /// <summary>
        /// The GetPasswordbyUserID
        /// </summary>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="string"/></returns>
        string GetPasswordbyUserID(int UserID);
    }
}
