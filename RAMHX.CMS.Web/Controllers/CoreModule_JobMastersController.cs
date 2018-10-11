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
    public class CoreModule_JobMastersController : ApiController
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: api/CoreModule_JobMasters
        public IQueryable<CoreModule_JobMaster> GetCoreModule_JobMasters()
        {
            return db.CoreModule_JobMasters.OrderByDescending(job => job.DisplayOrder);
        }

        // GET: api/CoreModule_JobMasters/5
        [ResponseType(typeof(CoreModule_JobMaster))]
        public IHttpActionResult GetCoreModule_JobMaster(int id)
        {
            CoreModule_JobMaster CoreModule_JobMaster = db.CoreModule_JobMasters.Find(id);
            if (CoreModule_JobMaster == null)
            {
                return NotFound();
            }

            return Ok(CoreModule_JobMaster);
        }

        // PUT: api/CoreModule_JobMasters/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCoreModule_JobMaster(int id, CoreModule_JobMaster CoreModule_JobMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != CoreModule_JobMaster.Id)
            {
                return BadRequest();
            }

            db.Entry(CoreModule_JobMaster).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreModule_JobMasterExists(id))
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

        // POST: api/CoreModule_JobMasters
        [ResponseType(typeof(CoreModule_JobMaster))]
        public IHttpActionResult PostCoreModule_JobMaster(CoreModule_JobMaster CoreModule_JobMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CoreModule_JobMasters.Add(CoreModule_JobMaster);
            db.SaveChanges();

            int nextDO = db.CoreModule_JobMasters.Max(pp => pp.DisplayOrder);
            nextDO++;
            CoreModule_JobMaster.DisplayOrder = nextDO;
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = CoreModule_JobMaster.Id }, CoreModule_JobMaster);
        }

        // DELETE: api/CoreModule_JobMasters/5
        [ResponseType(typeof(CoreModule_JobMaster))]
        public IHttpActionResult DeleteCoreModule_JobMaster(int id)
        {
            CoreModule_JobMaster CoreModule_JobMaster = db.CoreModule_JobMasters.Find(id);
            if (CoreModule_JobMaster == null)
            {
                return NotFound();
            }

            db.CoreModule_JobMasters.Remove(CoreModule_JobMaster);
            db.SaveChanges();

            return Ok(CoreModule_JobMaster);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CoreModule_JobMasterExists(int id)
        {
            return db.CoreModule_JobMasters.Count(e => e.Id == id) > 0;
        }
    }
}