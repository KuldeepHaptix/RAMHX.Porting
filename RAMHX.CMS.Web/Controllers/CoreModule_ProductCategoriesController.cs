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
    public class CoreModule_ProductCategoriesController : ApiController
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: api/CoreModule_ProductCategories
        public IQueryable<CoreModule_ProductCategory> GetCoreModule_ProductCategories()
        {
            return db.CoreModule_ProductCategories.OrderByDescending(prod => prod.DisplayOrder);
        }

        // GET: api/CoreModule_ProductCategories/5
        [ResponseType(typeof(CoreModule_ProductCategory))]
        public IHttpActionResult GetCoreModule_ProductCategory(int id)
        {
            CoreModule_ProductCategory CoreModule_ProductCategory = db.CoreModule_ProductCategories.Find(id);
            if (CoreModule_ProductCategory == null)
            {
                return NotFound();
            }

            return Ok(CoreModule_ProductCategory);
        }

        // PUT: api/CoreModule_ProductCategories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCoreModule_ProductCategory(int id, CoreModule_ProductCategory CoreModule_ProductCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != CoreModule_ProductCategory.Id)
            {
                return BadRequest();
            }

            db.Entry(CoreModule_ProductCategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreModule_ProductCategoryExists(id))
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

        // POST: api/CoreModule_ProductCategories
        [ResponseType(typeof(CoreModule_ProductCategory))]
        public IHttpActionResult PostCoreModule_ProductCategory(CoreModule_ProductCategory CoreModule_ProductCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CoreModule_ProductCategories.Add(CoreModule_ProductCategory);
            db.SaveChanges();

            int nextDO = db.CoreModule_ProductCategories.Max(pp => pp.DisplayOrder);
            nextDO++;
            CoreModule_ProductCategory.DisplayOrder = nextDO;
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = CoreModule_ProductCategory.Id }, CoreModule_ProductCategory);
        }

        // DELETE: api/CoreModule_ProductCategories/5
        [ResponseType(typeof(CoreModule_ProductCategory))]
        public IHttpActionResult DeleteCoreModule_ProductCategory(int id)
        {
            CoreModule_ProductCategory CoreModule_ProductCategory = db.CoreModule_ProductCategories.Find(id);
            if (CoreModule_ProductCategory == null)
            {
                return NotFound();
            }

            db.CoreModule_ProductCategories.Remove(CoreModule_ProductCategory);
            db.SaveChanges();

            return Ok(CoreModule_ProductCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CoreModule_ProductCategoryExists(int id)
        {
            return db.CoreModule_ProductCategories.Count(e => e.Id == id) > 0;
        }
    }
}