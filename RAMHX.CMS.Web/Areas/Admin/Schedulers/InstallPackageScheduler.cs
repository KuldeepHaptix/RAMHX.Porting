using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace RAMHX.CMS.Web.Areas.Admin.Schedulers
{
    public class InstallPackageScheduler : IJob
    {
        /// <summary>
        /// Keep Alive
        /// </summary>
        /// <param name="context">schedule job context</param>
        public void Execute(IJobExecutionContext context)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            try
            {
                //http://ramhx.brd2/Admin/Packages/importXML?packageId=1659B9D7-57F7-4B3F-9D70-710D9027FC21

                DataAccess.DatabaseEntities dbContext = new DataAccess.DatabaseEntities();
                var InProcPackage = RAMHX.CMS.Infra.Common.HasCorrectConnectionString ? dbContext.cms_PackageInstallations.FirstOrDefault(st => st.Status.ToLower() == "in progress") : null;
                if (InProcPackage == null && RAMHX.CMS.Infra.Common.HasCorrectConnectionString)
                {
                    var package = dbContext.cms_PackageInstallations.Where(st => st.Status.ToUpper() == "PENDING").OrderBy(st => st.CreatedDate).Take(1).FirstOrDefault();
                    if (package != null)
                    {
                        logger.Info("Package Installation Started. PackageId:" + package.PackageId);
                        InstallPackage(package.PackageId);
                        logger.Info("Package Installation Finished. PackageId:" + package.PackageId);
                    }
                }
                else
                {
                    logger.Info("Package Installation is In progress. PackageId:" + InProcPackage.PackageId);
                }
            }
            catch(Exception ex) {
                logger.Error("Error while installing package", ex);
            }
        }

        public static void InstallPackage(Guid packageId)
        {
            string invSchedUrl = MvcApplication.WebsiteRoot + "Admin/Packages/importXML?packageId=" + packageId.ToString().Replace("{", "").Replace("}", "");
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(invSchedUrl);
            myHttpWebRequest.Method = "POST";
            myHttpWebRequest.ContentLength = 0;
            myHttpWebRequest.GetResponse();
        }
    }
}