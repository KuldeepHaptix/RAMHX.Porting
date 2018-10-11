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
    public class CoreModule_GalleryCategoriesController : ApiController
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: api/CoreModule_GalleryCategories
        public IQueryable<CoreModule_GalleryCategory> GetCoreModule_GalleryCategories()
        {
            return db.CoreModule_GalleryCategories.OrderByDescending(cat => cat.DisplayOrder);
        }

        // GET: api/CoreModule_GalleryCategories/5
        [ResponseType(typeof(CoreModule_GalleryCategory))]
        public IHttpActionResult GetCoreModule_GalleryCategory(int id)
        {
            CoreModule_GalleryCategory CoreModule_GalleryCategory = db.CoreModule_GalleryCategories.Find(id);
            if (CoreModule_GalleryCategory == null)
            {
                return NotFound();
            }

            return Ok(CoreModule_GalleryCategory);
        }

        // PUT: api/CoreModule_GalleryCategories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCoreModule_GalleryCategory(int id, CoreModule_GalleryCategory CoreModule_GalleryCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != CoreModule_GalleryCategory.Id)
            {
                return BadRequest();
            }

            db.Entry(CoreModule_GalleryCategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreModule_GalleryCategoryExists(id))
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

        // POST: api/CoreModule_GalleryCategories
        [ResponseType(typeof(CoreModule_GalleryCategory))]
        public IHttpActionResult PostCoreModule_GalleryCategory(CoreModule_GalleryCategory CoreModule_GalleryCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CoreModule_GalleryCategories.Add(CoreModule_GalleryCategory);
            db.SaveChanges();

            int nextDO = db.CoreModule_GalleryCategories.Max(pp => pp.DisplayOrder);
            nextDO++;
            CoreModule_GalleryCategory.DisplayOrder = nextDO;
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = CoreModule_GalleryCategory.Id }, CoreModule_GalleryCategory);
        }

        // DELETE: api/CoreModule_GalleryCategories/5
        [ResponseType(typeof(CoreModule_GalleryCategory))]
        public IHttpActionResult DeleteCoreModule_GalleryCategory(int id)
        {
            CoreModule_GalleryCategory CoreModule_GalleryCategory = db.CoreModule_GalleryCategories.Find(id);
            if (CoreModule_GalleryCategory == null)
            {
                return NotFound();
            }

            db.CoreModule_GalleryCategories.Remove(CoreModule_GalleryCategory);
            db.SaveChanges();

            return Ok(CoreModule_GalleryCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CoreModule_GalleryCategoryExists(int id)
        {
            return db.CoreModule_GalleryCategories.Count(e => e.Id == id) > 0;
        }
    }
}