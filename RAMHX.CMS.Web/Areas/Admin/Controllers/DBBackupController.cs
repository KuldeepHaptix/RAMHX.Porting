using RAMHX.CMS.DataAccess;
using RAMHX.CMS.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RAMHX.CMS.Web.Areas.Admin.Controllers
{
    public class DBBackupController : Controller
    {
        // GET: Admin/DBBackup
        public ActionResult Index()
        {
            return View(GetFiles());
        }

        // GET: Admin/DBBackup
        public void Create()
        {
            SQLDataAccess.TakeAutoDailyBackup(true);
            Response.Redirect("/Admin/DBBackup/Index");
        }

        [HttpPost]
        public JsonResult TakeBackupNow()
        {
            SQLDataAccess.TakeAutoDailyBackup(true);
            return new JsonResult() { Data = "success" };
        }

        private List<DBBackupEntity> GetFiles()
        {
            List<DBBackupEntity> list = new List<DBBackupEntity>();
            var path = (Path.GetDirectoryName(System.Web.HttpContext.Current.Server.MapPath("/")) + "\\dbbackups\\").ToLower();
            foreach (var item in Directory.GetFiles(path).ToList())
            {
                DBBackupEntity ent = new DBBackupEntity();
                ent.FullPath = item.ToLower().Replace(path, string.Empty);
                list.Add(ent);
            }
            return list;
        }
    }
}