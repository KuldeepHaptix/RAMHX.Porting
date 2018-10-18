using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RAMHX.CMS.DataAccessCore;
using RAMHX.CMS.RepositoryCore;

namespace RAMHX.CMS.WebCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ConfigurationsController : Controller
    {
        private AppConfiguration appRepo = new AppConfiguration();

        public IActionResult Index(int? id)
        {
            if (!id.HasValue)
                id = 0;
            return View((new AppConfiguration()).GetConfigurationItemsByConfigurationID(id.Value));
        }

        public IActionResult Create(int id)
        {
            return View(new AppConfigs() { GroupId = id });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind(include:"GroupId,ItemId,ItemName,ItemDesc,IsActive,ShortDesc,ItemCode")] AppConfigs config, FormCollection collection)
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

        public IActionResult Edit(int gid, int itmid)
        {
            return View((new AppConfiguration()).GetGroupItemByItemAndGroupID(itmid, gid));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind(include:"GroupId,ItemId,ItemName,ItemDesc,IsActive,ShortDesc,ItemCode")] AppConfigs config, string gid, string itmid)
        {
            if (ModelState.IsValid)
            {
                (new AppConfiguration()).UpdateItem(config);
                return RedirectToAction("Index");
            }
            return View(config);
        }

        public IActionResult details(int gid, int itmid)
        {
            return View((new AppConfiguration()).GetGroupItemByItemAndGroupID(itmid, gid));
        }

        public IActionResult Delete(int gid, int itmid)
        {
            return View((new AppConfiguration()).GetGroupItemByItemAndGroupID(itmid, gid));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(int gid, int itmid)
        {
            (new AppConfiguration()).DeleteItem(new AppConfigs() { GroupId = gid, ItemId = itmid });
            return RedirectToAction("Index");
        }
    }
}