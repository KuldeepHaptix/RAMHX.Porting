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
    public class CoreModule_ProjectCategoriesController : ApiController
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: api/CoreModule_ProjectCategories
        public IQueryable<CoreModule_ProjectCategory> GetCoreModule_ProjectCategories()
        {
            return db.CoreModule_ProjectCategories.OrderByDescending(proj => proj.DisplayOrder);
        }

        // GET: api/CoreModule_ProjectCategories/5
        [ResponseType(typeof(CoreModule_ProjectCategory))]
        public IHttpActionResult GetCoreModule_ProjectCategory(int id)
        {
            CoreModule_ProjectCategory CoreModule_ProjectCategory = db.CoreModule_ProjectCategories.Find(id);
            if (CoreModule_ProjectCategory == null)
            {
                return NotFound();
            }

            return Ok(CoreModule_ProjectCategory);
        }

        // PUT: api/CoreModule_ProjectCategories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCoreModule_ProjectCategory(int id, CoreModule_ProjectCategory CoreModule_ProjectCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != CoreModule_ProjectCategory.Id)
            {
                return BadRequest();
            }

            db.Entry(CoreModule_ProjectCategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreModule_ProjectCategoryExists(id))
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

        // POST: api/CoreModule_ProjectCategories
        [ResponseType(typeof(CoreModule_ProjectCategory))]
        public IHttpActionResult PostCoreModule_ProjectCategory(CoreModule_ProjectCategory CoreModule_ProjectCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CoreModule_ProjectCategories.Add(CoreModule_ProjectCategory);
            db.SaveChanges();

            int nextDO = db.CoreModule_ProductCategories.Max(pp => pp.DisplayOrder);
            nextDO++;
            CoreModule_ProjectCategory.DisplayOrder = nextDO;
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = CoreModule_ProjectCategory.Id }, CoreModule_ProjectCategory);
        }

        // DELETE: api/CoreModule_ProjectCategories/5
        [ResponseType(typeof(CoreModule_ProjectCategory))]
        public IHttpActionResult DeleteCoreModule_ProjectCategory(int id)
        {
            CoreModule_ProjectCategory CoreModule_ProjectCategory = db.CoreModule_ProjectCategories.Find(id);
            if (CoreModule_ProjectCategory == null)
            {
                return NotFound();
            }

            db.CoreModule_ProjectCategories.Remove(CoreModule_ProjectCategory);
            db.SaveChanges();

            return Ok(CoreModule_ProjectCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CoreModule_ProjectCategoryExists(int id)
        {
            return db.CoreModule_ProjectCategories.Count(e => e.Id == id) > 0;
        }
    }
}