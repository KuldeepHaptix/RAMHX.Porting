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
    public class CoreModule_NewsController : ApiController
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: api/CoreModule_News
        public IQueryable<CoreModule_New> GetCoreModule_News()
        {
            return db.CoreModule_News.OrderByDescending(nw => nw.DisplayOrder);
        }

        // GET: api/CoreModule_News/5
        [ResponseType(typeof(CoreModule_New))]
        public IHttpActionResult GetCoreModule_New(int id)
        {
            CoreModule_New CoreModule_New = db.CoreModule_News.Find(id);
            if (CoreModule_New == null)
            {
                return NotFound();
            }

            return Ok(CoreModule_New);
        }

        // PUT: api/CoreModule_News/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCoreModule_New(int id, CoreModule_New CoreModule_New)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != CoreModule_New.Id)
            {
                return BadRequest();
            }

            db.Entry(CoreModule_New).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreModule_NewExists(id))
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

        // POST: api/CoreModule_News
        [ResponseType(typeof(CoreModule_New))]
        public IHttpActionResult PostCoreModule_New(CoreModule_New CoreModule_New)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CoreModule_News.Add(CoreModule_New);
            db.SaveChanges();

            int nextDO = db.CoreModule_News.Max(pp => pp.DisplayOrder);
            nextDO++;
            CoreModule_New.DisplayOrder = nextDO;
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = CoreModule_New.Id }, CoreModule_New);
        }

        // DELETE: api/CoreModule_News/5
        [ResponseType(typeof(CoreModule_New))]
        public IHttpActionResult DeleteCoreModule_New(int id)
        {
            CoreModule_New CoreModule_New = db.CoreModule_News.Find(id);
            if (CoreModule_New == null)
            {
                return NotFound();
            }

            db.CoreModule_News.Remove(CoreModule_New);
            db.SaveChanges();

            return Ok(CoreModule_New);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CoreModule_NewExists(int id)
        {
            return db.CoreModule_News.Count(e => e.Id == id) > 0;
        }
    }
}