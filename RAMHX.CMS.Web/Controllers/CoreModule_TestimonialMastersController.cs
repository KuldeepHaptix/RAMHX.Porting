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
    public class CoreModule_TestimonialMastersController : ApiController
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: api/CoreModule_TestimonialMasters
        public IQueryable<CoreModule_TestimonialMaster> GetCoreModule_TestimonialMasters()
        {
            return db.CoreModule_TestimonialMasters.OrderByDescending(test => test.DisplayOrder);
        }

        // GET: api/CoreModule_TestimonialMasters/5
        [ResponseType(typeof(CoreModule_TestimonialMaster))]
        public IHttpActionResult GetCoreModule_TestimonialMaster(int id)
        {
            CoreModule_TestimonialMaster CoreModule_TestimonialMaster = db.CoreModule_TestimonialMasters.Find(id);
            if (CoreModule_TestimonialMaster == null)
            {
                return NotFound();
            }

            return Ok(CoreModule_TestimonialMaster);
        }

        // PUT: api/CoreModule_TestimonialMasters/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCoreModule_TestimonialMaster(int id, CoreModule_TestimonialMaster CoreModule_TestimonialMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != CoreModule_TestimonialMaster.Id)
            {
                return BadRequest();
            }

            db.Entry(CoreModule_TestimonialMaster).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreModule_TestimonialMasterExists(id))
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

        // POST: api/CoreModule_TestimonialMasters
        [ResponseType(typeof(CoreModule_TestimonialMaster))]
        public IHttpActionResult PostCoreModule_TestimonialMaster(CoreModule_TestimonialMaster CoreModule_TestimonialMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CoreModule_TestimonialMasters.Add(CoreModule_TestimonialMaster);
            db.SaveChanges();

            int nextDO = db.CoreModule_TestimonialMasters.Max(pp => pp.DisplayOrder);
            nextDO++;
            CoreModule_TestimonialMaster.DisplayOrder = nextDO;
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = CoreModule_TestimonialMaster.Id }, CoreModule_TestimonialMaster);
        }

        // DELETE: api/CoreModule_TestimonialMasters/5
        [ResponseType(typeof(CoreModule_TestimonialMaster))]
        public IHttpActionResult DeleteCoreModule_TestimonialMaster(int id)
        {
            CoreModule_TestimonialMaster CoreModule_TestimonialMaster = db.CoreModule_TestimonialMasters.Find(id);
            if (CoreModule_TestimonialMaster == null)
            {
                return NotFound();
            }

            db.CoreModule_TestimonialMasters.Remove(CoreModule_TestimonialMaster);
            db.SaveChanges();

            return Ok(CoreModule_TestimonialMaster);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CoreModule_TestimonialMasterExists(int id)
        {
            return db.CoreModule_TestimonialMasters.Count(e => e.Id == id) > 0;
        }
    }
}