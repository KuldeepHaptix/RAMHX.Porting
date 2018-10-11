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
    public class CoreModule_EventCategoriesController : ApiController
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: api/CoreModule_EventCategories
        public IQueryable<CoreModule_EventCategory> GetCoreModule_EventCategories()
        {
            return db.CoreModule_EventCategories.OrderByDescending(proj => proj.DisplayOrder);
        }

        // GET: api/CoreModule_EventCategories/5
        [ResponseType(typeof(CoreModule_EventCategory))]
        public IHttpActionResult GetCoreModule_EventCategory(int id)
        {
            CoreModule_EventCategory CoreModule_EventCategory = db.CoreModule_EventCategories.Find(id);
            if (CoreModule_EventCategory == null)
            {
                return NotFound();
            }

            return Ok(CoreModule_EventCategory);
        }

        // PUT: api/CoreModule_EventCategories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCoreModule_EventCategory(int id, CoreModule_EventCategory CoreModule_EventCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != CoreModule_EventCategory.Id)
            {
                return BadRequest();
            }

            db.Entry(CoreModule_EventCategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreModule_EventCategoryExists(id))
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

        // POST: api/CoreModule_EventCategories
        [ResponseType(typeof(CoreModule_EventCategory))]
        public IHttpActionResult PostCoreModule_EventCategory(CoreModule_EventCategory CoreModule_EventCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CoreModule_EventCategories.Add(CoreModule_EventCategory);
            db.SaveChanges();

            int nextDO = db.CoreModule_EventCategories.Max(pp => pp.DisplayOrder);
            nextDO++;
            CoreModule_EventCategory.DisplayOrder = nextDO;
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = CoreModule_EventCategory.Id }, CoreModule_EventCategory);
        }

        // DELETE: api/CoreModule_EventCategories/5
        [ResponseType(typeof(CoreModule_EventCategory))]
        public IHttpActionResult DeleteCoreModule_EventCategory(int id)
        {
            CoreModule_EventCategory CoreModule_EventCategory = db.CoreModule_EventCategories.Find(id);
            if (CoreModule_EventCategory == null)
            {
                return NotFound();
            }

            db.CoreModule_EventCategories.Remove(CoreModule_EventCategory);
            db.SaveChanges();

            return Ok(CoreModule_EventCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CoreModule_EventCategoryExists(int id)
        {
            return db.CoreModule_EventCategories.Count(e => e.Id == id) > 0;
        }
    }
}