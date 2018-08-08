namespace WebTimeSheetManagement.Service
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.Script.Serialization;
    using WebTimeSheetManagement.Hubs;
    using WebTimeSheetManagement.Models;

    /// <summary>
    /// Defines the <see cref="NotificationService" />
    /// </summary>
    public class NotificationService
    {
        /// <summary>
        /// Defines the connString
        /// </summary>
        private static readonly string connString = ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString();

        /// <summary>
        /// Defines the command
        /// </summary>
        internal static SqlCommand command = null;

        /// <summary>
        /// Defines the dependency
        /// </summary>
        internal static SqlDependency dependency = null;

        /// <summary>
        /// Gets the notifications.
        /// </summary>
        /// <returns></returns>
        public static string GetNotification()
        {
            try
            {
                var messages = new List<NotificationsTB>();
                using (var connection = new SqlConnection(connString))
                {
                    connection.Open();
                    using (command = new SqlCommand(@"  
    SELECT [NotificationsID],[Status],[Message]FROM [TimesheetDB].[dbo].[NotificationsTB]
  where [TimesheetDB].[dbo].[NotificationsTB].Status ='A' 
  and GETDATE() between FromDate and ToDate", connection))
                    {
                        command.Notification = null;
                        if (dependency == null)
                        {
                            dependency = new SqlDependency(command);
                            dependency.OnChange += dependency_OnChange;
                        }

                        if (connection.State == ConnectionState.Closed)
                            connection.Open();
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            messages.Add(item: new NotificationsTB
                            {
                                NotificationsID = (int)reader["NotificationsID"],
                                Status = reader["Status"] != DBNull.Value ? (string)reader["Status"] : string.Empty,
                                Message = reader["Message"] != DBNull.Value ? (string)reader["Message"] : string.Empty
                            });
                        }
                    }
                }
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(messages);
                return json;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// The dependency_OnChange
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="SqlNotificationEventArgs"/></param>
        private static void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (dependency != null)
            {
                dependency.OnChange -= dependency_OnChange;
                dependency = null;
            }
            if (e.Type == SqlNotificationType.Change)
            {
                MyNotificationHub.Send();
            }
        }
    }
}
