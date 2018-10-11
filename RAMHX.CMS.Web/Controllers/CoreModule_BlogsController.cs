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
    public class CoreModule_BlogsController : ApiController
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: api/CoreModule_Blogs
        public IQueryable<CoreModule_Blog> GetCoreModule_Blogs()
        {
            return db.CoreModule_Blogs.OrderByDescending(proj => proj.DisplayOrder);
        }

        // GET: api/CoreModule_Blogs/5
        [ResponseType(typeof(CoreModule_Blog))]
        public IHttpActionResult GetCoreModule_Blog(int id)
        {
            CoreModule_Blog CoreModule_Blog = db.CoreModule_Blogs.Find(id);
            if (CoreModule_Blog == null)
            {
                return NotFound();
            }

            return Ok(CoreModule_Blog);
        }

        // PUT: api/CoreModule_Blogs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCoreModule_Blog(int id, CoreModule_Blog CoreModule_Blog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != CoreModule_Blog.Id)
            {
                return BadRequest();
            }

            db.Entry(CoreModule_Blog).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreModule_BlogExists(id))
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

        // POST: api/CoreModule_Blogs
        [ResponseType(typeof(CoreModule_Blog))]
        public IHttpActionResult PostCoreModule_Blog(CoreModule_Blog CoreModule_Blog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CoreModule_Blogs.Add(CoreModule_Blog);
            db.SaveChanges();

            int nextDO = db.CoreModule_Blogs.Max(pp => pp.DisplayOrder);
            nextDO++;
            CoreModule_Blog.DisplayOrder = nextDO;
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = CoreModule_Blog.Id }, CoreModule_Blog);
        }

        // DELETE: api/CoreModule_Blogs/5
        [ResponseType(typeof(CoreModule_Blog))]
        public IHttpActionResult DeleteCoreModule_Blog(int id)
        {
            CoreModule_Blog CoreModule_Blog = db.CoreModule_Blogs.Find(id);
            if (CoreModule_Blog == null)
            {
                return NotFound();
            }

            db.CoreModule_Blogs.Remove(CoreModule_Blog);
            db.SaveChanges();

            return Ok(CoreModule_Blog);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CoreModule_BlogExists(int id)
        {
            return db.CoreModule_Blogs.Count(e => e.Id == id) > 0;
        }
    }
}