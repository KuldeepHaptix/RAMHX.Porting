using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using RAMHX.CMS.DataAccess;

namespace RAMHX.CMS.Web.Controllers
{
    public class CoreModule_PackageCategoriesController : ApiController
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: api/CoreModule_PackageCategories
        public IQueryable<CoreModule_PackageCategory> GetCoreModule_PackageCategories()
        {
            return db.CoreModule_PackageCategories.OrderByDescending(pack => pack.DisplayOrder);
        }

        // GET: api/CoreModule_PackageCategories/5
        [ResponseType(typeof(CoreModule_PackageCategory))]
        public IHttpActionResult GetCoreModule_PackageCategory(int id)
        {
            CoreModule_PackageCategory CoreModule_PackageCategory = db.CoreModule_PackageCategories.Find(id);
            if (CoreModule_PackageCategory == null)
            {
                return NotFound();
            }

            return Ok(CoreModule_PackageCategory);
        }

        // PUT: api/CoreModule_PackageCategories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCoreModule_PackageCategory(int id, CoreModule_PackageCategory CoreModule_PackageCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != CoreModule_PackageCategory.Id)
            {
                return BadRequest();
            }

            db.Entry(CoreModule_PackageCategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreModule_PackageCategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/CoreModule_PackageCategories
        [ResponseType(typeof(CoreModule_PackageCategory))]
        public IHttpActionResult PostCoreModule_PackageCategory(CoreModule_PackageCategory CoreModule_PackageCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CoreModule_PackageCategories.Add(CoreModule_PackageCategory);
            db.SaveChanges();

            int nextDO = db.CoreModule_PackageCategories.Max(pp => pp.DisplayOrder);
           
            nextDO++;
            CoreModule_PackageCategory.DisplayOrder = nextDO;
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = CoreModule_PackageCategory.Id }, CoreModule_PackageCategory);
        }

        // DELETE: api/CoreModule_PackageCategories/5
        [ResponseType(typeof(CoreModule_PackageCategory))]
        public IHttpActionResult DeleteCoreModule_PackageCategory(int id)
        {
            CoreModule_PackageCategory CoreModule_PackageCategory = db.CoreModule_PackageCategories.Find(id);
            if (CoreModule_PackageCategory == null)
            {
                return NotFound();
            }

            db.CoreModule_PackageCategories.Remove(CoreModule_PackageCategory);
            db.SaveChanges();

            return Ok(CoreModule_PackageCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CoreModule_PackageCategoryExists(int id)
        {
            return db.CoreModule_PackageCategories.Count(e => e.Id == id) > 0;
        }
    }
}