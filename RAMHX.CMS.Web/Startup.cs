using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RAMHX.CMS.Web.Startup))]
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Web.config", Watch = true)]   
namespace RAMHX.CMS.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
