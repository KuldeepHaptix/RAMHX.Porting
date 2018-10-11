using RAMHX.CMS.DataAccess;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Xml;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

namespace RAMHX.CMS.Web.App_Start
{
    public class Installer
    {
        public static void Run()
        {
            log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            logger.Info("RAMHX - Upgrade Begin");
            ///areas/admin/installer/upgradescript/
            try
            {
                XDocument history = XDocument.Load(HttpContext.Current.Server.MapPath("/Areas/Admin/Installer/History.xml"));

                List<cms_UpgradeHistory> releases = history.Root.Elements("release")
                         .Select(x => new cms_UpgradeHistory
                         {
                             Script = ((string)x.Element("script")).Replace("/", ""),
                             ScriptFullPath = HttpContext.Current.Server.MapPath("/areas/admin/installer/upgradescript/" + ((string)x.Element("script")).Replace("/", "")),
                             ReleasedDate = (string)x.Element("releaseddate"),
                             ReleasedNote = (string)x.Element("releasenote"),
                         })
                         .ToList();

                SQLDataAccess dataAccess = new SQLDataAccess();
                foreach (var release in releases)
                {
                    dataAccess.ExecuteScript(release);
                }

                logger.Info("RAMHX - Upgrade Completed");

            }
            catch (System.Exception ex)
            {
                logger.Error("RAMHX - Upgrade Completed with Error!!", ex);
            }

        }
    }
}