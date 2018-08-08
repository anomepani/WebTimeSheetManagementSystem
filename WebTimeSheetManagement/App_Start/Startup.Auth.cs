namespace WebTimeSheetManagement
{
    using Microsoft.AspNet.Identity;
    using Owin;
    using System;

    /// <summary>
    /// Defines the <see cref="Startup" />
    /// </summary>
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        /// <summary>
        /// The ConfigureAuth
        /// </summary>
        /// <param name="app">The app<see cref="IAppBuilder"/></param>
        public void ConfigureAuth(IAppBuilder app)
        {

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);
        }
    }
}
