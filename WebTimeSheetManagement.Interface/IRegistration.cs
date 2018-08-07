namespace WebTimeSheetManagement.Interface
{
    using System.Linq;
    using WebTimeSheetManagement.Models;

    /// <summary>
    /// Defines the <see cref="IRegistration" />
    /// </summary>
    public interface IRegistration
    {
        /// <summary>
        /// The CheckUserNameExists
        /// </summary>
        /// <param name="Username">The Username<see cref="string"/></param>
        /// <returns>The <see cref="bool"/></returns>
        bool CheckUserNameExists(string Username);

        /// <summary>
        /// The AddUser
        /// </summary>
        /// <param name="entity">The entity<see cref="Registration"/></param>
        /// <returns>The <see cref="int"/></returns>
        int AddUser(Registration entity);

        /// <summary>
        /// The ListofRegisteredUser
        /// </summary>
        /// <param name="sortColumn">The sortColumn<see cref="string"/></param>
        /// <param name="sortColumnDir">The sortColumnDir<see cref="string"/></param>
        /// <param name="Search">The Search<see cref="string"/></param>
        /// <returns>The <see cref="IQueryable{Registration}"/></returns>
        IQueryable<Registration> ListofRegisteredUser(string sortColumn, string sortColumnDir, string Search);

        /// <summary>
        /// The UpdatePassword
        /// </summary>
        /// <param name="RegistrationID">The RegistrationID<see cref="string"/></param>
        /// <param name="Password">The Password<see cref="string"/></param>
        /// <returns>The <see cref="bool"/></returns>
        bool UpdatePassword(string RegistrationID, string Password);
    }
}
