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
    public class CoreModule_BlogCategoriesController : ApiController
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: api/CoreModule_BlogCategories
        public IQueryable<CoreModule_BlogCategory> GetCoreModule_BlogCategories()
        {
            return db.CoreModule_BlogCategories.OrderByDescending(proj => proj.DisplayOrder);
        }

        // GET: api/CoreModule_BlogCategories/5
        [ResponseType(typeof(CoreModule_BlogCategory))]
        public IHttpActionResult GetCoreModule_BlogCategory(int id)
        {
            CoreModule_BlogCategory CoreModule_BlogCategory = db.CoreModule_BlogCategories.Find(id);
            if (CoreModule_BlogCategory == null)
            {
                return NotFound();
            }

            return Ok(CoreModule_BlogCategory);
        }

        // PUT: api/CoreModule_BlogCategories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCoreModule_BlogCategory(int id, CoreModule_BlogCategory CoreModule_BlogCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != CoreModule_BlogCategory.Id)
            {
                return BadRequest();
            }

            db.Entry(CoreModule_BlogCategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreModule_BlogCategoryExists(id))
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

        // POST: api/CoreModule_BlogCategories
        [ResponseType(typeof(CoreModule_BlogCategory))]
        public IHttpActionResult PostCoreModule_BlogCategory(CoreModule_BlogCategory CoreModule_BlogCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CoreModule_BlogCategories.Add(CoreModule_BlogCategory);
            db.SaveChanges();

            int nextDO = db.CoreModule_BlogCategories.Max(pp => pp.DisplayOrder);
            nextDO++;
            CoreModule_BlogCategory.DisplayOrder = nextDO;
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = CoreModule_BlogCategory.Id }, CoreModule_BlogCategory);
        }

        // DELETE: api/CoreModule_BlogCategories/5
        [ResponseType(typeof(CoreModule_BlogCategory))]
        public IHttpActionResult DeleteCoreModule_BlogCategory(int id)
        {
            CoreModule_BlogCategory CoreModule_BlogCategory = db.CoreModule_BlogCategories.Find(id);
            if (CoreModule_BlogCategory == null)
            {
                return NotFound();
            }

            db.CoreModule_BlogCategories.Remove(CoreModule_BlogCategory);
            db.SaveChanges();

            return Ok(CoreModule_BlogCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CoreModule_BlogCategoryExists(int id)
        {
            return db.CoreModule_BlogCategories.Count(e => e.Id == id) > 0;
        }
    }
}