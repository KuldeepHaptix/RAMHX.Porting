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
    public class CoreModule_JobCategoriesController : ApiController
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: api/CoreModule_JobCategories
        public IQueryable<CoreModule_JobCategory> GetCoreModule_JobCategories()
        {
            return db.CoreModule_JobCategories.OrderByDescending(job => job.DisplayOrder);
        }

        // GET: api/CoreModule_JobCategories/5
        [ResponseType(typeof(CoreModule_JobCategory))]
        public IHttpActionResult GetCoreModule_JobCategory(int id)
        {
            CoreModule_JobCategory CoreModule_JobCategory = db.CoreModule_JobCategories.Find(id);
            if (CoreModule_JobCategory == null)
            {
                return NotFound();
            }

            return Ok(CoreModule_JobCategory);
        }

        // PUT: api/CoreModule_JobCategories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCoreModule_JobCategory(int id, CoreModule_JobCategory CoreModule_JobCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != CoreModule_JobCategory.Id)
            {
                return BadRequest();
            }

            db.Entry(CoreModule_JobCategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreModule_JobCategoryExists(id))
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

        // POST: api/CoreModule_JobCategories
        [ResponseType(typeof(CoreModule_JobCategory))]
        public IHttpActionResult PostCoreModule_JobCategory(CoreModule_JobCategory CoreModule_JobCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CoreModule_JobCategories.Add(CoreModule_JobCategory);
            db.SaveChanges();

            int nextDO = db.CoreModule_JobCategories.Max(pp => pp.DisplayOrder);
            nextDO++;
            CoreModule_JobCategory.DisplayOrder = nextDO;
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = CoreModule_JobCategory.Id }, CoreModule_JobCategory);
        }

        // DELETE: api/CoreModule_JobCategories/5
        [ResponseType(typeof(CoreModule_JobCategory))]
        public IHttpActionResult DeleteCoreModule_JobCategory(int id)
        {
            CoreModule_JobCategory CoreModule_JobCategory = db.CoreModule_JobCategories.Find(id);
            if (CoreModule_JobCategory == null)
            {
                return NotFound();
            }

            db.CoreModule_JobCategories.Remove(CoreModule_JobCategory);
            db.SaveChanges();

            return Ok(CoreModule_JobCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CoreModule_JobCategoryExists(int id)
        {
            return db.CoreModule_JobCategories.Count(e => e.Id == id) > 0;
        }
    }
}