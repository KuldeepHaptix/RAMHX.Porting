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
    public class CoreModule_NewsCategoriesController : ApiController
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: api/CoreModule_NewsCategories
        public IQueryable<CoreModule_NewsCategory> GetCoreModule_NewsCategories()
        {
            return db.CoreModule_NewsCategories.OrderByDescending(nw => nw.DisplayOrder);
        }

        // GET: api/CoreModule_NewsCategories/5
        [ResponseType(typeof(CoreModule_NewsCategory))]
        public IHttpActionResult GetCoreModule_NewsCategory(int id)
        {
            CoreModule_NewsCategory CoreModule_NewsCategory = db.CoreModule_NewsCategories.Find(id);
            if (CoreModule_NewsCategory == null)
            {
                return NotFound();
            }

            return Ok(CoreModule_NewsCategory);
        }

        // PUT: api/CoreModule_NewsCategories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCoreModule_NewsCategory(int id, CoreModule_NewsCategory CoreModule_NewsCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != CoreModule_NewsCategory.Id)
            {
                return BadRequest();
            }

            db.Entry(CoreModule_NewsCategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreModule_NewsCategoryExists(id))
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

        // POST: api/CoreModule_NewsCategories
        [ResponseType(typeof(CoreModule_NewsCategory))]
        public IHttpActionResult PostCoreModule_NewsCategory(CoreModule_NewsCategory CoreModule_NewsCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CoreModule_NewsCategories.Add(CoreModule_NewsCategory);
            db.SaveChanges();

            int nextDO = db.CoreModule_NewsCategories.Max(pp => pp.DisplayOrder);
            nextDO++;
            CoreModule_NewsCategory.DisplayOrder = nextDO;
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = CoreModule_NewsCategory.Id }, CoreModule_NewsCategory);
        }

        // DELETE: api/CoreModule_NewsCategories/5
        [ResponseType(typeof(CoreModule_NewsCategory))]
        public IHttpActionResult DeleteCoreModule_NewsCategory(int id)
        {
            CoreModule_NewsCategory CoreModule_NewsCategory = db.CoreModule_NewsCategories.Find(id);
            if (CoreModule_NewsCategory == null)
            {
                return NotFound();
            }

            db.CoreModule_NewsCategories.Remove(CoreModule_NewsCategory);
            db.SaveChanges();

            return Ok(CoreModule_NewsCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CoreModule_NewsCategoryExists(int id)
        {
            return db.CoreModule_NewsCategories.Count(e => e.Id == id) > 0;
        }
    }
}