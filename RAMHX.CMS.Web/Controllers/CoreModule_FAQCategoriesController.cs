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
    public class CoreModule_FAQCategoriesController : ApiController
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: api/CoreModule_FAQCategories
        public IQueryable<CoreModule_FAQCategory> GetCoreModule_FAQCategories()
        {
            return db.CoreModule_FAQCategories.OrderByDescending(faq => faq.DisplayOrder);
        }

        // GET: api/CoreModule_FAQCategories/5
        [ResponseType(typeof(CoreModule_FAQCategory))]
        public IHttpActionResult GetCoreModule_FAQCategory(int id)
        {
            CoreModule_FAQCategory CoreModule_FAQCategory = db.CoreModule_FAQCategories.Find(id);
            if (CoreModule_FAQCategory == null)
            {
                return NotFound();
            }

            return Ok(CoreModule_FAQCategory);
        }

        // PUT: api/CoreModule_FAQCategories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCoreModule_FAQCategory(int id, CoreModule_FAQCategory CoreModule_FAQCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != CoreModule_FAQCategory.Id)
            {
                return BadRequest();
            }

            db.Entry(CoreModule_FAQCategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreModule_FAQCategoryExists(id))
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

        // POST: api/CoreModule_FAQCategories
        [ResponseType(typeof(CoreModule_FAQCategory))]
        public IHttpActionResult PostCoreModule_FAQCategory(CoreModule_FAQCategory CoreModule_FAQCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CoreModule_FAQCategories.Add(CoreModule_FAQCategory);
            db.SaveChanges();

            int nextDO = db.CoreModule_FAQCategories.Max(pp => pp.DisplayOrder);
            nextDO++;
            CoreModule_FAQCategory.DisplayOrder = nextDO;
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = CoreModule_FAQCategory.Id }, CoreModule_FAQCategory);
        }

        // DELETE: api/CoreModule_FAQCategories/5
        [ResponseType(typeof(CoreModule_FAQCategory))]
        public IHttpActionResult DeleteCoreModule_FAQCategory(int id)
        {
            CoreModule_FAQCategory CoreModule_FAQCategory = db.CoreModule_FAQCategories.Find(id);
            if (CoreModule_FAQCategory == null)
            {
                return NotFound();
            }

            db.CoreModule_FAQCategories.Remove(CoreModule_FAQCategory);
            db.SaveChanges();

            return Ok(CoreModule_FAQCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CoreModule_FAQCategoryExists(int id)
        {
            return db.CoreModule_FAQCategories.Count(e => e.Id == id) > 0;
        }
    }
}