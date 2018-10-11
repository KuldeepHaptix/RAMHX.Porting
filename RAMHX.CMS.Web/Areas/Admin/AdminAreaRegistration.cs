using System.Web.Mvc;

namespace RAMHX.CMS.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            try
            {
                context.MapRoute(
                                "Admin_default",
                                "Admin/{controller}/{action}/{id}",
                                new { controller = "dashboard", action = "Index", id = UrlParameter.Optional }
                            );
            }
            catch { }
        }
    }
}