using Owin;
using Microsoft.Owin;

[assembly: OwinStartupAttribute(typeof(WebTimeSheetManagement.Startup))]
namespace WebTimeSheetManagement
{


    /// <summary>
    /// Defines the <see cref="Startup" />
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// The Configuration
        /// </summary>
        /// <param name="app">The app<see cref="IAppBuilder"/></param>
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
