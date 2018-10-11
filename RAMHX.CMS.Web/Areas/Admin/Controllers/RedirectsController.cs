using RAMHX.CMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RAMHX.CMS.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RedirectsController : Controller
    {
        DatabaseEntities dataContext = new DatabaseEntities();

        // GET: Admin/Redirects
        public ActionResult Index()
        {
            return View(dataContext.cms_301Redirection.ToList());
        }

        // GET: Admin/Redirects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Redirects/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                cms_301Redirection newRed = new cms_301Redirection();
                newRed.rid = Guid.NewGuid();
                newRed.fromUrl = collection["fromUrl"];
                newRed.toUrl = collection["toUrl"];
                newRed.Active = Convert.ToBoolean(collection["Active"].Split(',')[0]);
                dataContext.cms_301Redirection.Add(newRed);
                dataContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Redirects/Edit/5
        public ActionResult Edit(Guid id)
        {
            return View(dataContext.cms_301Redirection.First(r=>r.rid == id));
        }

        // POST: Admin/Redirects/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                cms_301Redirection newRed = dataContext.cms_301Redirection.First(r => r.rid == id);
                newRed.fromUrl = collection["fromUrl"];
                newRed.toUrl = collection["toUrl"];
                newRed.Active = Convert.ToBoolean(collection["Active"].Split(',')[0]);
                dataContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Redirects/Delete/5
        public ActionResult Delete(Guid id)
        {
            return View(dataContext.cms_301Redirection.First(r => r.rid == id));
        }

        // POST: Admin/Redirects/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                cms_301Redirection newRed = dataContext.cms_301Redirection.First(r => r.rid == id);
                dataContext.cms_301Redirection.Remove(newRed);
                dataContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
