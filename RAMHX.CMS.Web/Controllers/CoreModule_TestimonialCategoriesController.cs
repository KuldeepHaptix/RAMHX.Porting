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
    public class CoreModule_TestimonialCategoriesController : ApiController
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: api/CoreModule_TestimonialCategories
        public IQueryable<CoreModule_TestimonialCategory> GetCoreModule_TestimonialCategories()
        {
            return db.CoreModule_TestimonialCategories.OrderByDescending(test => test.DisplayOrder);
        }

        // GET: api/CoreModule_TestimonialCategories/5
        [ResponseType(typeof(CoreModule_TestimonialCategory))]
        public IHttpActionResult GetCoreModule_TestimonialCategory(int id)
        {
            CoreModule_TestimonialCategory CoreModule_TestimonialCategory = db.CoreModule_TestimonialCategories.Find(id);
            if (CoreModule_TestimonialCategory == null)
            {
                return NotFound();
            }

            return Ok(CoreModule_TestimonialCategory);
        }

        // PUT: api/CoreModule_TestimonialCategories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCoreModule_TestimonialCategory(int id, CoreModule_TestimonialCategory CoreModule_TestimonialCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != CoreModule_TestimonialCategory.Id)
            {
                return BadRequest();
            }

            db.Entry(CoreModule_TestimonialCategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreModule_TestimonialCategoryExists(id))
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

        // POST: api/CoreModule_TestimonialCategories
        [ResponseType(typeof(CoreModule_TestimonialCategory))]
        public IHttpActionResult PostCoreModule_TestimonialCategory(CoreModule_TestimonialCategory CoreModule_TestimonialCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CoreModule_TestimonialCategories.Add(CoreModule_TestimonialCategory);
            db.SaveChanges();

            int nextDO = db.CoreModule_TestimonialCategories.Max(pp => pp.DisplayOrder);
            nextDO++;
            CoreModule_TestimonialCategory.DisplayOrder = nextDO;
            db.SaveChanges();


            return CreatedAtRoute("DefaultApi", new { id = CoreModule_TestimonialCategory.Id }, CoreModule_TestimonialCategory);
        }

        // DELETE: api/CoreModule_TestimonialCategories/5
        [ResponseType(typeof(CoreModule_TestimonialCategory))]
        public IHttpActionResult DeleteCoreModule_TestimonialCategory(int id)
        {
            CoreModule_TestimonialCategory CoreModule_TestimonialCategory = db.CoreModule_TestimonialCategories.Find(id);
            if (CoreModule_TestimonialCategory == null)
            {
                return NotFound();
            }

            db.CoreModule_TestimonialCategories.Remove(CoreModule_TestimonialCategory);
            db.SaveChanges();

            return Ok(CoreModule_TestimonialCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CoreModule_TestimonialCategoryExists(int id)
        {
            return db.CoreModule_TestimonialCategories.Count(e => e.Id == id) > 0;
        }
    }
}