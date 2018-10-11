using RAMHX.CMS.DataAccess;
using RAMHX.CMS.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RAMHX.CMS.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ConfigurationsController : BaseController
    {
        //public SelectList BindDropdownList { get; set; }

        //private DatabaseEntities db = new DatabaseEntities();
        private AppConfiguration appRepo = new AppConfiguration();
        // GET: Admin/Configurations
        public ActionResult Index(int? id)
        {
            if (!id.HasValue)
                id = 0;
            return View((new AppConfiguration()).GetConfigurationItemsByConfigurationID(id.Value));
        }

        public ActionResult Create(int id)
        {
            return View(new app_Configs() { GroupId = id});
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupId,ItemId,ItemName,ItemDesc,IsActive,ShortDesc,ItemCode")] app_Configs config, FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    (new AppConfiguration()).CreateNewItem(config, config.GroupId);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                SqlException innerException = ex.InnerException.InnerException as SqlException;
                // Exception number 2627 = Violation of %ls constraint '%.*ls'. Cannot insert duplicate key in object '%.*ls'.
                // Exception number 2601 = Cannot insert duplicate key row in object '%.*ls' with unique index '%.*ls'.
                // See http://msdn.microsoft.com/en-us/library/cc645603.aspx for more information and possible exception numbers
                if (innerException != null && (innerException.Number == 2627 || innerException.Number == 2601))
                {
                    ModelState.AddModelError("Duplicate", "Item id already exists");
                }
                else
                {
                    throw;
                }
            }
            catch
            {
                throw;
            }
            return View(config);
        }

        public ActionResult Edit(int gid, int itmid)
        {
            return View((new AppConfiguration()).GetGroupItemByItemAndGroupID(itmid, gid));
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupId,ItemId,ItemName,ItemDesc,IsActive,ShortDesc,ItemCode")] app_Configs config, string gid, string itmid)
        {
            if (ModelState.IsValid)
            {
                (new AppConfiguration()).UpdateItem(config);
                return RedirectToAction("Index");
            }
            return View(config);
        }

        public ActionResult details(int gid, int itmid)
        {
            return View((new AppConfiguration()).GetGroupItemByItemAndGroupID(itmid, gid));
        }

        public ActionResult Delete(int gid, int itmid)
        {
            return View((new AppConfiguration()).GetGroupItemByItemAndGroupID(itmid, gid));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int gid, int itmid)
        {
            (new AppConfiguration()).DeleteItem(new app_Configs() { GroupId = gid, ItemId = itmid });
            return RedirectToAction("Index");
        }
    }
}