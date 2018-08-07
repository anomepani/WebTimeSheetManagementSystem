namespace WebTimeSheetManagement.Interface
{
    using System.Linq;
    using WebTimeSheetManagement.Models;

    /// <summary>
    /// Defines the <see cref="INotification" />
    /// </summary>
    public interface INotification
    {
        /// <summary>
        /// The AddNotification
        /// </summary>
        /// <param name="entity">The entity<see cref="NotificationsTB"/></param>
        /// <returns>The <see cref="int"/></returns>
        int AddNotification(NotificationsTB entity);

        /// <summary>
        /// The DisableExistingNotifications
        /// </summary>
        void DisableExistingNotifications();

        /// <summary>
        /// The ShowNotifications
        /// </summary>
        /// <param name="sortColumn">The sortColumn<see cref="string"/></param>
        /// <param name="sortColumnDir">The sortColumnDir<see cref="string"/></param>
        /// <param name="Search">The Search<see cref="string"/></param>
        /// <returns>The <see cref="IQueryable{NotificationsTB_ViewModel}"/></returns>
        IQueryable<NotificationsTB_ViewModel> ShowNotifications(string sortColumn, string sortColumnDir, string Search);

        /// <summary>
        /// The DeActivateNotificationByID
        /// </summary>
        /// <param name="NotificationID">The NotificationID<see cref="int"/></param>
        /// <returns>The <see cref="bool"/></returns>
        bool DeActivateNotificationByID(int NotificationID);
    }
}
