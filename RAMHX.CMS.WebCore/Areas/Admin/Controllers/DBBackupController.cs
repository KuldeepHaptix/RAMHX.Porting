using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RAMHX.CMS.DataAccessCore;
using RAMHX.CMS.WebCore.Areas.Admin.Models;

namespace RAMHX.CMS.WebCore.Areas.Admin.Controllers
{
    public class DBBackupController : Controller
    {
        public IActionResult Index()
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
            return new JsonResult(new { Data = "success" });
        }


        private List<DBBackupEntity> GetFiles()
        {
            List<DBBackupEntity> list = new List<DBBackupEntity>();
            var path = (Path.GetDirectoryName(Path.Combine("/")) + "\\dbbackups\\").ToLower();
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