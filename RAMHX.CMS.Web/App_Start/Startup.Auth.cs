using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Google;
using Owin;
using System;
using RAMHX.CMS.Web.Areas.Admin.Models;
using RAMHX.CMS.Repository;
using System.Configuration;

namespace RAMHX.CMS.Web
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            int timeout = 30;
            if (!string.IsNullOrEmpty(AppConfiguration.GetAppSettings("RAMHX.SessionCookiesTimeOutInMinutes")))
            {
                int.TryParse(AppConfiguration.GetAppSettings("RAMHX.SessionCookiesTimeOutInMinutes"), out timeout);
                if (timeout >= 0)
                {
                    timeout = 30;
                }
            } 
        
            string redirect = "/admin/Account/Login";
            AppConfiguration confg = new AppConfiguration();
            if (RAMHX.CMS.Infra.Common.HasCorrectConnectionString && !string.IsNullOrEmpty(confg.GetGroupItemByItemAndGroupID(2, 1).ItemDesc) && ConfigurationManager.AppSettings["RAMHX.RedirectToCustomLogin"] == "1")
            {
                 redirect = confg.GetGroupItemByItemAndGroupID(2, 1).ItemDesc.ToLower();
                if (!redirect.StartsWith("http") && !redirect.StartsWith("/"))
                {
                    redirect = "/" + redirect;
                }
            }
            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString(redirect),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(timeout),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }
    }
}