using RAMHX.CMS.Infra;
using RAMHX.CMS.Repository;
using RAMHX.CMS.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace RAMHX.CMS.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PackagesController : Controller
    {
        [HttpGet]
        public ActionResult InstallListing(string status = "")
        {
            PackagesRepository packRepo = new PackagesRepository();
            ViewBag.stausList = packRepo.StatusList();
            return PartialView(packRepo.InstallListing(status));
        }

        //[HttpGet]
        //public ActionResult InstallRequest()
        //{
        //    return PartialView();
        //}

        // GET: Admin/Packages
        public ActionResult Index(string status = "", string t = "")
        {
            PackageViewModel model = new PackageViewModel();
            PackagesRepository packRepo = new PackagesRepository();

            model.Pages = SiteContext.Pages.OrderBy(p=>p.FullItemPath).ToList();
            model.HtmlModules = new RenderHtmlRepository().GetAllHtmlModules();
            model.TemplatesFilds = new TemplatesRepository().GetTemplateFields();
            ViewBag.stausList = packRepo.StatusList();
            model.PackageInstallations = packRepo.InstallListing(status);

            model.PublishingTarget = "http://haptixbrd5";

            //PartialView(packRepo.InstallListing(status));

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(Guid Id)
        {
            PackagesRepository repo = new PackagesRepository();
            repo.DeletePackage(Id);
            return Json("Deleted", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ExportXML(string pages, string contents, string templates)
        {
            PackagesRepository packRepo = new PackagesRepository();
            string outputFilePath = "";

            string xml = packRepo.Export(pages.Split(',').ToList().Select(x => !string.IsNullOrEmpty(x) ? Guid.Parse(x) : Guid.Empty).ToList(), contents.Split(',').ToList().Select(x => !string.IsNullOrEmpty(x) ? Guid.Parse(x) : Guid.Empty).ToList(), templates.Split(',').ToList().Select(x => !string.IsNullOrEmpty(x) ? Guid.Parse(x) : Guid.Empty).ToList());

            MemoryStream stream = new MemoryStream();

            using (var archive = new ZipArchive(stream, ZipArchiveMode.Create, true))
            {
                //var demoFile = archive.CreateEntry("package_" + DateTime.Now.ToString("yyyy-MM-dd HH'-'mm'-'ss") + ".xml");
                var demoFile = archive.CreateEntry("data.xml");

                using (var entryStream = demoFile.Open())
                using (var streamWriter = new StreamWriter(entryStream))
                {
                    streamWriter.Write(xml);
                }
            }

            string packagePath = Infra.Common.GetAppSetting(Infra.Common.PackagePath);
            if (string.IsNullOrEmpty(packagePath))
                packagePath = Infra.Common.DEFAULT_PACKAGE_PATH;

            bool exists = System.IO.Directory.Exists(Server.MapPath(packagePath));

            if (!exists)
                System.IO.Directory.CreateDirectory(Server.MapPath(packagePath));

            string fileName = "package_" + DateTime.Now.ToString("yyyy-MM-dd_HHmmss") + ".zip";

            outputFilePath = (Server.MapPath(packagePath + fileName));
            using (var fileStream = new FileStream(outputFilePath, FileMode.Create))
            {
                stream.Seek(0, SeekOrigin.Begin); // <-- must do this after writing the stream!
                stream.CopyTo(fileStream);
            }

            return Json(packagePath + fileName);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult importXML(string packageId)
        {
            PackagesRepository packRepo = new PackagesRepository();
            packRepo.Import(Guid.Parse(packageId));
            return Json("Install succesfullly", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult InstallRequest(FormCollection collection)
        {
            string webconfigkey = new AppConfiguration().GetGroupItemByItemAndGroupID(RAMHX.CMS.Infra.Common.Constants.AppConfigItemCodes.RAMHXPackageAPIKey, Convert.ToInt32(Enums.AppConfigs.PackageAPIKey.AppSettings)).ItemDesc;
            string requestApiKey = collection["apikey"];
            string currentusername = collection["currentusername"];

            if (!string.IsNullOrEmpty(requestApiKey) && webconfigkey == requestApiKey)
            {
                PackagesRepository packRepo = new PackagesRepository();
                HttpPostedFileBase postedPackage = Request.Files[0];
                packRepo.InstallRequest(postedPackage, currentusername);
            }
            return Index();
        }
    }
}
