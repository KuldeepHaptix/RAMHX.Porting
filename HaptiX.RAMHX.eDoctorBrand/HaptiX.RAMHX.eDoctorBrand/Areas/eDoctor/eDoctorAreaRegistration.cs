using System.Web.Mvc;

namespace HaptiX.RAMHX.eDoctorBrand.Areas.eDoctor
{
    public class eDoctorAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "eDoctor";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "eDoctor_default",
                "eDoctor/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}