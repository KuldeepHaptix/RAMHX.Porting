using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace RAMHX.CMS.Web.Areas.Admin.Scheduler
{
    public class KeepAlive : IJob
    {
        /// <summary>
        /// Keep Alive
        /// </summary>
        /// <param name="context">schedule job context</param>
        public void Execute(IJobExecutionContext context)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            string invSchedUrl = MvcApplication.WebsiteRoot;
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(invSchedUrl);
            myHttpWebRequest.Method = "GET";
            myHttpWebRequest.GetResponse();
            logger.Info("KeepAlive Request ... !");
        }
    }
}