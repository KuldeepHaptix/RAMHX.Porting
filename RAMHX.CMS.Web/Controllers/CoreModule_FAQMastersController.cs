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
    public class CoreModule_FAQMastersController : ApiController
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: api/CoreModule_FAQMasters
        public IQueryable<CoreModule_FAQMaster> GetCoreModule_FAQMasters()
        {
            return db.CoreModule_FAQMasters.OrderByDescending(faq => faq.DisplayOrder);
        }

        // GET: api/CoreModule_FAQMasters/5
        [ResponseType(typeof(CoreModule_FAQMaster))]
        public IHttpActionResult GetCoreModule_FAQMaster(int id)
        {
            CoreModule_FAQMaster CoreModule_FAQMaster = db.CoreModule_FAQMasters.Find(id);
            if (CoreModule_FAQMaster == null)
            {
                return NotFound();
            }

            return Ok(CoreModule_FAQMaster);
        }

        // PUT: api/CoreModule_FAQMasters/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCoreModule_FAQMaster(int id, CoreModule_FAQMaster CoreModule_FAQMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != CoreModule_FAQMaster.Id)
            {
                return BadRequest();
            }

            db.Entry(CoreModule_FAQMaster).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreModule_FAQMasterExists(id))
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

        // POST: api/CoreModule_FAQMasters
        [ResponseType(typeof(CoreModule_FAQMaster))]
        public IHttpActionResult PostCoreModule_FAQMaster(CoreModule_FAQMaster CoreModule_FAQMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CoreModule_FAQMasters.Add(CoreModule_FAQMaster);
            db.SaveChanges();

            int nextDO = db.CoreModule_FAQMasters.Max(pp => pp.DisplayOrder);
            nextDO++;
            CoreModule_FAQMaster.DisplayOrder = nextDO;
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = CoreModule_FAQMaster.Id }, CoreModule_FAQMaster);
        }

        // DELETE: api/CoreModule_FAQMasters/5
        [ResponseType(typeof(CoreModule_FAQMaster))]
        public IHttpActionResult DeleteCoreModule_FAQMaster(int id)
        {
            CoreModule_FAQMaster CoreModule_FAQMaster = db.CoreModule_FAQMasters.Find(id);
            if (CoreModule_FAQMaster == null)
            {
                return NotFound();
            }

            db.CoreModule_FAQMasters.Remove(CoreModule_FAQMaster);
            db.SaveChanges();

            return Ok(CoreModule_FAQMaster);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CoreModule_FAQMasterExists(int id)
        {
            return db.CoreModule_FAQMasters.Count(e => e.Id == id) > 0;
        }
    }
}