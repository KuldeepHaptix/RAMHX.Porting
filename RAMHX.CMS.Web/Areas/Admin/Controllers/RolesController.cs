using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using RAMHX.CMS.DataAccess;
using RAMHX.CMS.Repository;
using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RAMHX.CMS.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private DatabaseEntities db = new DatabaseEntities();
        PageRepository pr = new PageRepository();

        // GET: Admin/Roles
        public ActionResult Index(string pageid, string pagetype)
        {
            Guid pgId = Guid.Empty;
            Guid.TryParse(pageid, out pgId);
            if (pagetype == "page")
            {
                var roleAssigned = pr.GetPageRolesByID(pgId);
                var roles = db.AspNetRoles.ToList();

                var result = roles.Where(p => !roleAssigned.Any(p2 => p2.RoleId == p.Id));
                return View(result.ToList());
            }

            return View(db.AspNetRoles.ToList());
        }

        // GET: Admin/Roles/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetRole aspNetRole = db.AspNetRoles.Find(id);
            if (aspNetRole == null)
            {
                return HttpNotFound();
            }
            return View(aspNetRole);
        }

        // GET: Admin/Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] AspNetRole aspNetRole)
        {
            if (ModelState.IsValid)
            {
                string url = Request.UrlReferrer.ToString();
                var newurl = url.Split('?');

                aspNetRole.Id = Guid.NewGuid().ToString();
                var role = db.AspNetRoles.Where(rid => rid.Name == aspNetRole.Name).FirstOrDefault();
                if (role != null)
                {
                    Response.Redirect("/admin/roles/create" + (newurl.Length > 1 ? "?" + newurl[1] : string.Empty));
                }
                else
                {
                    db.AspNetRoles.Add(aspNetRole);
                    db.SaveChanges();
                    Response.Redirect("/admin/roles/index" + (newurl.Length > 1 ? "?" + newurl[1] : string.Empty));
                }
            }
            return View(aspNetRole);
        }

        // GET: Admin/Roles/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetRole aspNetRole = db.AspNetRoles.Find(id);
            if (aspNetRole == null)
            {
                return HttpNotFound();
            }
            return View(aspNetRole);
        }

        // POST: Admin/Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] AspNetRole aspNetRole)
        {
            if (ModelState.IsValid)
            {
                AspNetRole anRole = db.AspNetRoles.Find(aspNetRole.Id);
                if (anRole.Name.ToLower() == "admin")
                {
                    if (SiteContext.IsDialogPage)
                    {
                        return RedirectToAction("Index", new { @dialog = 1 });
                    }
                    return RedirectToAction("Index");
                }
                anRole.Name = aspNetRole.Name;
                db.SaveChanges();
                if (SiteContext.IsDialogPage)
                {
                    return RedirectToAction("Index", new { @dialog = 1 });
                }
                return RedirectToAction("Index");
            }
            return View(aspNetRole);
        }

        // GET: Admin/Roles/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetRole aspNetRole = db.AspNetRoles.Find(id);
            if (aspNetRole == null)
            {
                return HttpNotFound();
            }
            return View(aspNetRole);
        }

        // POST: Admin/Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AspNetRole aspNetRole = db.AspNetRoles.Find(id);
            if (aspNetRole.Name.ToLower() == "admin")
            {
                if (SiteContext.IsDialogPage)
                {
                    return RedirectToAction("Index", new { @dialog = 1 });
                }

                return RedirectToAction("Index");
            }
            db.AspNetRoles.Remove(aspNetRole);
            db.SaveChanges();
            if (SiteContext.IsDialogPage)
            {
                return RedirectToAction("Index", new { @dialog = 1 });
            }
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

        public JsonResult AssignedRoles(string pageid, string hmids)
        {
            Guid pgId = Guid.Empty;
            Guid.TryParse(pageid, out pgId);
            pr.RemoveRoles(pgId);
            foreach (var item in hmids.Split(','))
            {
                pr.AddPageRoles(pgId, item);
            }
            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AssignedUserRoles(string userid, string hmids)
        {
            //////VIPUL CODE - START
            //////PageRepository pr = new PageRepository();
            //////ApplicationUserManager UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            //////foreach (var item in hmids.Split(','))
            //////{
            //////    var role = pr.GetRoleByRoleID(item);
            //////    var result = await UserManager.RemoveFromRoleAsync(userid, role.Id);
            //////}

            //////foreach (var item in hmids.Split(','))
            //////{
            //////    var role = pr.GetRoleByRoleID(item);
            //////    var resultAdd = await UserManager.AddToRoleAsync(userid, role.Id);
            //////    if (!resultAdd.Succeeded)
            //////    {
            //////        return Json(new { status = "error" }, JsonRequestBehavior.AllowGet);
            //////    }
            //////}
            ///////* Sample code for manger.AddToRole */
            ////////var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new DatabaseEntities()));
            ////////manager.AddToRole(userid, roleid);
            ////////manager.RemoveFromRole(userid, roleid);
            //////VIPUL CODE - END

            PageRepository pr = new PageRepository();
            ApplicationUserManager UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            //var userid = "guid of user";
            //remove existing roles
            var uroles = UserManager.GetRoles(userid);
            foreach (var rname in uroles)
            {
                UserManager.RemoveFromRole(userid, rname);
            }

            //now add new selected roles
            foreach (var rid in hmids.Split(','))
            {
                var role = pr.GetRoleByRoleID(rid);
                UserManager.AddToRole(userid, role.Name);
            }

            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }
    }
}
