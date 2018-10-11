using RAMHX.CMS.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RAMHX.CMS.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FileManagerController : BaseController
    {
        // GET: FileManager
        public string GetFileContent(string filepath)
        {
            return System.IO.File.ReadAllText(filepath);
        }

        public void SaveFileContent(string filepath, string content)
        {

        }

        public ActionResult FileEdit(string filepath)
        {
            FileModel file = new FileModel();
            file.FilePath = filepath;
            file.FileContent = System.IO.File.ReadAllText(Server.MapPath(filepath));
            return View(file);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult FileEdit(FileModel file)
        {
            ViewBag.Saved = "1";
            System.IO.File.WriteAllText(Server.MapPath(file.FilePath), file.FileContent);
            return View(file);
        }
    }
}