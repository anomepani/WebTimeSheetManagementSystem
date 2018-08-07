namespace WebTimeSheetManagement.Hubs
{
    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;
    using System;

    /// <summary>
    /// Defines the <see cref="MyNotificationHub" />
    /// </summary>
    [HubName("mynotificationHub")]
    public class MyNotificationHub : Hub
    {
        /// <summary>
        /// The Send
        /// </summary>
        public static void Send()
        {
            try
            {
                IHubContext context = GlobalHost.ConnectionManager.GetHubContext<MyNotificationHub>();
                context.Clients.All.displayStatus();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
