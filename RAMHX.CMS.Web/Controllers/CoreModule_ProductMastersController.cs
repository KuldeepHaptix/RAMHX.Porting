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
    public class CoreModule_ProductMastersController : ApiController
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: api/CoreModule_ProductMasters
        public IQueryable<CoreModule_ProductMaster> GetCoreModule_ProductMasters()
        {
            return db.CoreModule_ProductMasters.OrderByDescending(prod => prod.DisplayOrder);
        }

        // GET: api/CoreModule_ProductMasters/5
        [ResponseType(typeof(CoreModule_ProductMaster))]
        public IHttpActionResult GetCoreModule_ProductMaster(int id)
        {
            CoreModule_ProductMaster CoreModule_ProductMaster = db.CoreModule_ProductMasters.Find(id);
            if (CoreModule_ProductMaster == null)
            {
                return NotFound();
            }

            return Ok(CoreModule_ProductMaster);
        }

        // PUT: api/CoreModule_ProductMasters/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCoreModule_ProductMaster(int id, CoreModule_ProductMaster CoreModule_ProductMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != CoreModule_ProductMaster.Id)
            {
                return BadRequest();
            }

            db.Entry(CoreModule_ProductMaster).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreModule_ProductMasterExists(id))
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

        // POST: api/CoreModule_ProductMasters
        [ResponseType(typeof(CoreModule_ProductMaster))]
        public IHttpActionResult PostCoreModule_ProductMaster(CoreModule_ProductMaster CoreModule_ProductMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CoreModule_ProductMasters.Add(CoreModule_ProductMaster);
            db.SaveChanges();

            int nextDO = db.CoreModule_ProductMasters.Max(pp => pp.DisplayOrder);
            nextDO++;
            CoreModule_ProductMaster.DisplayOrder = nextDO;
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = CoreModule_ProductMaster.Id }, CoreModule_ProductMaster);
        }

        // DELETE: api/CoreModule_ProductMasters/5
        [ResponseType(typeof(CoreModule_ProductMaster))]
        public IHttpActionResult DeleteCoreModule_ProductMaster(int id)
        {
            CoreModule_ProductMaster CoreModule_ProductMaster = db.CoreModule_ProductMasters.Find(id);
            if (CoreModule_ProductMaster == null)
            {
                return NotFound();
            }

            db.CoreModule_ProductMasters.Remove(CoreModule_ProductMaster);
            db.SaveChanges();

            return Ok(CoreModule_ProductMaster);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CoreModule_ProductMasterExists(int id)
        {
            return db.CoreModule_ProductMasters.Count(e => e.Id == id) > 0;
        }
    }
}