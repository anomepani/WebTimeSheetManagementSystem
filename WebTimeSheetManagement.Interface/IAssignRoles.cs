namespace WebTimeSheetManagement.Interface
{
    using System.Collections.Generic;
    using System.Linq;
    using WebTimeSheetManagement.Models;

    /// <summary>
    /// Defines the <see cref="IAssignRoles" />
    /// </summary>
    public interface IAssignRoles
    {
        /// <summary>
        /// The ListofAdmins
        /// </summary>
        /// <returns>The <see cref="List{AdminModel}"/></returns>
        List<AdminModel> ListofAdmins();

        /// <summary>
        /// The ListofUser
        /// </summary>
        /// <returns>The <see cref="List{UserModel}"/></returns>
        List<UserModel> ListofUser();

        /// <summary>
        /// The UpdateAssigntoAdmin
        /// </summary>
        /// <param name="AssignToAdminID">The AssignToAdminID<see cref="string"/></param>
        /// <param name="UserID">The UserID<see cref="string"/></param>
        /// <returns>The <see cref="int"/></returns>
        int UpdateAssigntoAdmin(string AssignToAdminID, string UserID);

        /// <summary>
        /// The ShowallRoles
        /// </summary>
        /// <param name="sortColumn">The sortColumn<see cref="string"/></param>
        /// <param name="sortColumnDir">The sortColumnDir<see cref="string"/></param>
        /// <param name="Search">The Search<see cref="string"/></param>
        /// <returns>The <see cref="IQueryable{UserModel}"/></returns>
        IQueryable<UserModel> ShowallRoles(string sortColumn, string sortColumnDir, string Search);

        /// <summary>
        /// The RemovefromUserRole
        /// </summary>
        /// <param name="RegistrationID">The RegistrationID<see cref="string"/></param>
        /// <returns>The <see cref="bool"/></returns>
        bool RemovefromUserRole(string RegistrationID);

        /// <summary>
        /// The GetListofUnAssignedUsers
        /// </summary>
        /// <returns>The <see cref="List{UserModel}"/></returns>
        List<UserModel> GetListofUnAssignedUsers();

        /// <summary>
        /// The SaveAssignedRoles
        /// </summary>
        /// <param name="AssignRolesModel">The AssignRolesModel<see cref="AssignRolesModel"/></param>
        /// <returns>The <see cref="bool"/></returns>
        bool SaveAssignedRoles(AssignRolesModel AssignRolesModel);

        /// <summary>
        /// The CheckIsUserAssignedRole
        /// </summary>
        /// <param name="RegistrationID">The RegistrationID<see cref="int"/></param>
        /// <returns>The <see cref="bool"/></returns>
        bool CheckIsUserAssignedRole(int RegistrationID);
    }
}
