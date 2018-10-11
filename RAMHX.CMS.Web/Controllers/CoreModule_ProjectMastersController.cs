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
    public class CoreModule_ProjectMastersController : ApiController
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: api/CoreModule_ProjectMasters
        public IQueryable<CoreModule_ProjectMaster> GetCoreModule_ProjectMasters()
        {
            return db.CoreModule_ProjectMasters.OrderByDescending(proj => proj.DisplayOrder);
        }

        // GET: api/CoreModule_ProjectMasters/5
        [ResponseType(typeof(CoreModule_ProjectMaster))]
        public IHttpActionResult GetCoreModule_ProjectMaster(int id)
        {
            CoreModule_ProjectMaster CoreModule_ProjectMaster = db.CoreModule_ProjectMasters.Find(id);
            if (CoreModule_ProjectMaster == null)
            {
                return NotFound();
            }

            return Ok(CoreModule_ProjectMaster);
        }

        // PUT: api/CoreModule_ProjectMasters/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCoreModule_ProjectMaster(int id, CoreModule_ProjectMaster CoreModule_ProjectMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != CoreModule_ProjectMaster.Id)
            {
                return BadRequest();
            }

            db.Entry(CoreModule_ProjectMaster).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreModule_ProjectMasterExists(id))
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

        // POST: api/CoreModule_ProjectMasters
        [ResponseType(typeof(CoreModule_ProjectMaster))]
        public IHttpActionResult PostCoreModule_ProjectMaster(CoreModule_ProjectMaster CoreModule_ProjectMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CoreModule_ProjectMasters.Add(CoreModule_ProjectMaster);
            db.SaveChanges();

            int nextDO = db.CoreModule_ProjectMasters.Max(pp => pp.DisplayOrder);
            nextDO++;
            CoreModule_ProjectMaster.DisplayOrder = nextDO;
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = CoreModule_ProjectMaster.Id }, CoreModule_ProjectMaster);
        }

        // DELETE: api/CoreModule_ProjectMasters/5
        [ResponseType(typeof(CoreModule_ProjectMaster))]
        public IHttpActionResult DeleteCoreModule_ProjectMaster(int id)
        {
            CoreModule_ProjectMaster CoreModule_ProjectMaster = db.CoreModule_ProjectMasters.Find(id);
            if (CoreModule_ProjectMaster == null)
            {
                return NotFound();
            }

            db.CoreModule_ProjectMasters.Remove(CoreModule_ProjectMaster);
            db.SaveChanges();

            return Ok(CoreModule_ProjectMaster);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CoreModule_ProjectMasterExists(int id)
        {
            return db.CoreModule_ProjectMasters.Count(e => e.Id == id) > 0;
        }
    }
}