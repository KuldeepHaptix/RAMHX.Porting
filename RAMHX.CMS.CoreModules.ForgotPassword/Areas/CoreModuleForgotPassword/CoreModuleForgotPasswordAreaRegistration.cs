using System.Web.Mvc;

namespace RAMHX.CMS.CoreModules.ForgotPassword.Areas.CoreModuleForgotPassword
{
    public class CoreModuleForgotPasswordAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CoreModuleForgotPassword";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CoreModuleForgotPassword_default",
                "CoreModuleForgotPassword/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}