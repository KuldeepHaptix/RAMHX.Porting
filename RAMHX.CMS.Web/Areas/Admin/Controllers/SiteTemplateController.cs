using RAMHX.CMS.Repository;
using RAMHX.CMS.Web.Areas.Admin.Models;
using RAMHX.CMS.Web.Areas.Admin.Schedulers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace RAMHX.CMS.Web.Areas.Admin.Controllers
{
    public class SiteTemplateController : Controller
    {
        // GET: Admin/SiteTemplate
        public ActionResult Index()
        {
            if (!string.IsNullOrEmpty(AppConfiguration.GetAppSettings("SiteTemplates")))
            {
                HttpWebRequest req = WebRequest.Create(AppConfiguration.GetAppSettings("SiteTemplates")) as HttpWebRequest;
                XmlDocument xmlDoc = new XmlDocument();
                using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
                {
                    xmlDoc.Load(resp.GetResponseStream());
                }

                List<SiteTemplate> templates = new List<SiteTemplate>();
                string siteUrl = AppConfiguration.GetAppSettings("SiteTemplates").ToLower().Replace("sitetemplates.xml", "");
                foreach (XmlNode item in xmlDoc.SelectNodes("sitetemplates/template"))
                {
                    templates.Add(new SiteTemplate { Name = item.InnerText, ThumbUrl = siteUrl + item.InnerText + "/thumb.png", PackagePath = siteUrl + item.InnerText + "/package.zip" });
                }
                return View(templates);
            }
            else
            {
                return null;
            }
        }

        public ActionResult Install(string stname)
        {
            if (!string.IsNullOrEmpty(AppConfiguration.GetAppSettings("SiteTemplates")))
            {
                string siteUrl = AppConfiguration.GetAppSettings("SiteTemplates").ToLower().Replace("sitetemplates.xml", stname + "/package.zip");

                HttpWebRequest req = WebRequest.Create(siteUrl) as HttpWebRequest;
                XmlDocument xmlDoc = new XmlDocument();
                using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
                {

                    MemoryStream packageStrm = new MemoryStream();
                    using (Stream stream = resp.GetResponseStream())
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            int count = 0;
                            do
                            {
                                byte[] buf = new byte[1024];
                                count = stream.Read(buf, 0, 1024);
                                ms.Write(buf, 0, count);
                            } while (stream.CanRead && count > 0);
                            packageStrm = ms;
                        }
                    }

                    PackagesRepository package = new PackagesRepository();
                    var packageid = package.InstallRequest(packageStrm.ToArray(), stname + "_package.zip", System.Web.HttpContext.Current.User.Identity.Name);
                    //InstallPackageScheduler.InstallPackage(packageid);
                }

                return null;
            }
            else
            {
                return null;
            }
        }
    }
}