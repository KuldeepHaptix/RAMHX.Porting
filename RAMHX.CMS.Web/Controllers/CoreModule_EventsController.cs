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
    public class CoreModule_EventsController : ApiController
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: api/CoreModule_Events
        public IQueryable<CoreModule_Event> GetCoreModule_Events()
        {
            return db.CoreModule_Events.OrderByDescending(proj => proj.DisplayOrder);
        }

        // GET: api/CoreModule_Events/5
        [ResponseType(typeof(CoreModule_Event))]
        public IHttpActionResult GetCoreModule_Event(int id)
        {
            CoreModule_Event CoreModule_Event = db.CoreModule_Events.Find(id);
            if (CoreModule_Event == null)
            {
                return NotFound();
            }

            return Ok(CoreModule_Event);
        }

        // PUT: api/CoreModule_Events/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCoreModule_Event(int id, CoreModule_Event CoreModule_Event)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != CoreModule_Event.Id)
            {
                return BadRequest();
            }

            db.Entry(CoreModule_Event).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreModule_EventExists(id))
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

        // POST: api/CoreModule_Events
        [ResponseType(typeof(CoreModule_Event))]
        public IHttpActionResult PostCoreModule_Event(CoreModule_Event CoreModule_Event)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CoreModule_Events.Add(CoreModule_Event);
            db.SaveChanges();

            int nextDO = db.CoreModule_Events.Max(pp => pp.DisplayOrder);
            nextDO++;
            CoreModule_Event.DisplayOrder = nextDO;
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = CoreModule_Event.Id }, CoreModule_Event);
        }

        // DELETE: api/CoreModule_Events/5
        [ResponseType(typeof(CoreModule_Event))]
        public IHttpActionResult DeleteCoreModule_Event(int id)
        {
            CoreModule_Event CoreModule_Event = db.CoreModule_Events.Find(id);
            if (CoreModule_Event == null)
            {
                return NotFound();
            }

            db.CoreModule_Events.Remove(CoreModule_Event);
            db.SaveChanges();

            return Ok(CoreModule_Event);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CoreModule_EventExists(int id)
        {
            return db.CoreModule_Events.Count(e => e.Id == id) > 0;
        }
    }
}