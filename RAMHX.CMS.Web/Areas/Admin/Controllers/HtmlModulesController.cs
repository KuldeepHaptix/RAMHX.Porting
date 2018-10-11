using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RAMHX.CMS.DataAccess;
using RAMHX.CMS.Repository;
using RAMHX.CMS.Infra;
using System.Data.SqlClient;
using System.Data.Entity.Core;

namespace RAMHX.CMS.Web.Areas.Admin.Controllers
{

    [Authorize(Roles = "Admin")]
    public class HtmlModulesController : BaseController
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: Admin/HtmlModules

        //public ActionResult Index()
        //{
        //    return View(db.HtmlModules.ToList());
        //}

        public ActionResult Index(Guid? pageid)
        {
            if (pageid.HasValue && pageid.Value != Guid.Empty)
            {
                var phm = db.cms_PageHTMLModules.Where(hm => hm.PageID == pageid).Select(hm => hm.HTMLModuleId).ToList();
                var hms = from hm in db.HtmlModules
                          where !phm.Contains(hm.HTMLModuleId)
                          select hm;
                return View(hms.ToList());
            }

            return View(db.HtmlModules.ToList());
        }

        public JsonResult addNewModule(string pageid, string hmids)
        {
            PageRepository pr = new PageRepository();
            foreach (var item in hmids.Split(','))
            {
                pr.AddPageHtmlModule(Guid.Parse(pageid), Guid.Parse(item));
            }

            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HtmlModule htmlModule = db.HtmlModules.Find(id);
            if (htmlModule == null)
            {
                return HttpNotFound();
            }
            return View(htmlModule);
        }

        public ActionResult Create()
        {
            //BindDocsDropdown();
            return View();
        }

        // POST: Admin/HtmlModules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Include = "HtmlModuleCode,HtmlModuleName,HtmlModuleDescription,HtmlModuleHTML,PageName,CreatedDate,ModifiedDate,CreatedUserName,ModifiedUserName,ViewControlPath")] HtmlModule htmlModule, FormCollection collection)
        {
            try
            {
                htmlModule.HtmlModuleHTML = collection["editor1"];
                if (ModelState.IsValid)
                {
                    //HtmlModule hm = new PageRepository().GetHtmlModule(htmlModule.HtmlModuleCode);
                    //if (hm != null)


                    htmlModule.CreatedDate = DateTime.Now;
                    htmlModule.CreatedUserName = User.Identity.Name;
                    htmlModule.HTMLModuleId = Guid.NewGuid();
                    db.HtmlModules.Add(htmlModule);
                    db.SaveChanges();
                    string url = Request.UrlReferrer.ToString();
                    var newurl = url.Split('?');
                    Response.Redirect("/admin/HtmlModules/Index" + (newurl.Length > 1 ? "?" + newurl[1] : string.Empty));
                    //return RedirectToAction("Index");
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
                    ModelState.AddModelError("Duplicate", "Code already exists");
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

            return View(htmlModule);
        }

        // GET: Admin/HtmlModules/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HtmlModule htmlModule = db.HtmlModules.Find(id);
            if (htmlModule == null)
            {
                return HttpNotFound();
            }
            return View(htmlModule);
        }

        // POST: Admin/HtmlModules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HtmlModuleCode,HtmlModuleName,HtmlModuleDescription,HtmlModuleHTML,PageName,CreatedDate,ModifiedDate,CreatedUserName,ModifiedUserName,ViewControlPath,HTMLModuleId")] HtmlModule htmlModule, string dialog, string pageid, string Command)
        {
            bool IsEmptyHtmlModuleName = string.IsNullOrEmpty(htmlModule.HtmlModuleName) || string.IsNullOrWhiteSpace(htmlModule.HtmlModuleName);
            bool IsEmptyHtmlModuleDescription = string.IsNullOrEmpty(htmlModule.HtmlModuleDescription) || string.IsNullOrWhiteSpace(htmlModule.HtmlModuleDescription);
            bool IsEmptyHtmlModuleHTML = string.IsNullOrEmpty(htmlModule.HtmlModuleHTML) || string.IsNullOrWhiteSpace(htmlModule.HtmlModuleHTML);
           // bool IsEmptyHtmlPageName = string.IsNullOrEmpty(htmlModule.PageName) || string.IsNullOrWhiteSpace(htmlModule.PageName);

            if (ModelState.IsValid && !IsEmptyHtmlModuleName && !IsEmptyHtmlModuleDescription && !IsEmptyHtmlModuleHTML)
            {
                htmlModule.ModifiedDate = DateTime.Now;
                htmlModule.ModifiedUserName = User.Identity.Name;
                db.Entry(htmlModule).State = EntityState.Modified;
                db.SaveChanges();
                CacheRepository.Clear(htmlModule.HTMLModuleId.ToString(), CacheTypes.HtmlModule);
                if (Command == "Save")
                {
                    return Redirect(Request.Url.PathAndQuery);
                }
                if (string.IsNullOrEmpty(dialog) && string.IsNullOrEmpty(pageid))
                    return RedirectToAction("Index");
                else
                    return Redirect("/admin/HtmlModules/Index?dialog=1&pageid=" + pageid);

            }

            if (IsEmptyHtmlModuleName)
                ModelState.AddModelError("HtmlModuleName", ValidationMessages.REQUIRED_FIELD);
            if (IsEmptyHtmlModuleDescription)
                ModelState.AddModelError("HtmlModuleDescription", ValidationMessages.REQUIRED_FIELD);
            if (IsEmptyHtmlModuleHTML)
                ModelState.AddModelError("HtmlModuleHTML", ValidationMessages.REQUIRED_FIELD);
            //if (IsEmptyHtmlPageName)
            //    ModelState.AddModelError("PageName", ValidationMessages.REQUIRED_FIELD);
            return View(htmlModule);
        }

        // GET: Admin/HtmlModules/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HtmlModule htmlModule = db.HtmlModules.Find(id);
            if (htmlModule == null)
            {
                return HttpNotFound();
            }
            return View(htmlModule);
        }

        // POST: Admin/HtmlModules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var mid = Infra.Common.GetGuidValue(id);
            HtmlModule htmlModule = db.HtmlModules.Find(mid);
            if (htmlModule != null)
                db.HtmlModules.Remove(htmlModule);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // POST: Admin/HtmlModules/SaveHtmlContent/5
        [HttpPost]
        public ActionResult SaveHtmlContent(string htmlmoduleid, string htmldata)
        {
            Guid mid = Infra.Common.GetGuidValue(htmlmoduleid);
            if (mid != Guid.Empty)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HtmlModule htmlModule = db.HtmlModules.FirstOrDefault(m => m.HTMLModuleId == mid);
            if (htmlModule == null)
            {

                return HttpNotFound();
            }
            htmlModule.ModifiedDate = DateTime.Now;
            htmlModule.ModifiedUserName = User.Identity.Name;
            htmlModule.HtmlModuleHTML = htmldata;
            db.Entry(htmlModule).State = EntityState.Modified;
            db.SaveChanges();
            return View(htmlModule);
        }
    }
}
