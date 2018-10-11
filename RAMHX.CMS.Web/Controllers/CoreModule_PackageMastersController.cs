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
    public class CoreModule_PackageMastersController : ApiController
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: api/CoreModule_PackageMasters
        public IQueryable<CoreModule_PackageMaster> GetCoreModule_PackageMasters()
        {
            return db.CoreModule_PackageMasters.OrderByDescending(pack => pack.DisplayOrder);
        }

        // GET: api/CoreModule_PackageMasters/5
        [ResponseType(typeof(CoreModule_PackageMaster))]
        public IHttpActionResult GetCoreModule_PackageMaster(int id)
        {
            CoreModule_PackageMaster CoreModule_PackageMaster = db.CoreModule_PackageMasters.Find(id);
            if (CoreModule_PackageMaster == null)
            {
                return NotFound();
            }

            return Ok(CoreModule_PackageMaster);
        }

        // PUT: api/CoreModule_PackageMasters/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCoreModule_PackageMaster(int id, CoreModule_PackageMaster CoreModule_PackageMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != CoreModule_PackageMaster.Id)
            {
                return BadRequest();
            }

            db.Entry(CoreModule_PackageMaster).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreModule_PackageMasterExists(id))
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

        // POST: api/CoreModule_PackageMasters
        [ResponseType(typeof(CoreModule_PackageMaster))]
        public IHttpActionResult PostCoreModule_PackageMaster(CoreModule_PackageMaster CoreModule_PackageMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CoreModule_PackageMasters.Add(CoreModule_PackageMaster);
            db.SaveChanges();

            int nextDO = db.CoreModule_PackageMasters.Max(pp => pp.DisplayOrder);
            nextDO++;
            CoreModule_PackageMaster.DisplayOrder = nextDO;
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = CoreModule_PackageMaster.Id }, CoreModule_PackageMaster);
        }

        // DELETE: api/CoreModule_PackageMasters/5
        [ResponseType(typeof(CoreModule_PackageMaster))]
        public IHttpActionResult DeleteCoreModule_PackageMaster(int id)
        {
            CoreModule_PackageMaster CoreModule_PackageMaster = db.CoreModule_PackageMasters.Find(id);
            if (CoreModule_PackageMaster == null)
            {
                return NotFound();
            }

            db.CoreModule_PackageMasters.Remove(CoreModule_PackageMaster);
            db.SaveChanges();

            return Ok(CoreModule_PackageMaster);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CoreModule_PackageMasterExists(int id)
        {
            return db.CoreModule_PackageMasters.Count(e => e.Id == id) > 0;
        }
    }
}