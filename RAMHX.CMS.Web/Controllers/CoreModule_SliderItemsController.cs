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
    public class CoreModule_SliderItemsController : ApiController
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: api/CoreModule_SliderItems
        public IQueryable<CoreModule_SliderItem> GetCoreModule_SliderItems()
        {
            return db.CoreModule_SliderItems.OrderByDescending(proj => proj.DisplayOrder);
        }

        // GET: api/CoreModule_SliderItems/5
        [ResponseType(typeof(CoreModule_SliderItem))]
        public IHttpActionResult GetCoreModule_SliderItem(int id)
        {
            CoreModule_SliderItem CoreModule_SliderItem = db.CoreModule_SliderItems.Find(id);
            if (CoreModule_SliderItem == null)
            {
                return NotFound();
            }

            return Ok(CoreModule_SliderItem);
        }

        // PUT: api/CoreModule_SliderItems/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCoreModule_SliderItem(int id, CoreModule_SliderItem CoreModule_SliderItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != CoreModule_SliderItem.Id)
            {
                return BadRequest();
            }

            db.Entry(CoreModule_SliderItem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreModule_SliderItemExists(id))
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

        // POST: api/CoreModule_SliderItems
        [ResponseType(typeof(CoreModule_SliderItem))]
        public IHttpActionResult PostCoreModule_SliderItem(CoreModule_SliderItem CoreModule_SliderItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CoreModule_SliderItems.Add(CoreModule_SliderItem);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = CoreModule_SliderItem.Id }, CoreModule_SliderItem);
        }

        // DELETE: api/CoreModule_SliderItems/5
        [ResponseType(typeof(CoreModule_SliderItem))]
        public IHttpActionResult DeleteCoreModule_SliderItem(int id)
        {
            CoreModule_SliderItem CoreModule_SliderItem = db.CoreModule_SliderItems.Find(id);
            if (CoreModule_SliderItem == null)
            {
                return NotFound();
            }

            db.CoreModule_SliderItems.Remove(CoreModule_SliderItem);
            db.SaveChanges();

            int nextDO = db.CoreModule_SliderItems.Max(pp => pp.DisplayOrder);
            nextDO++;
            CoreModule_SliderItem.DisplayOrder = nextDO;
            db.SaveChanges();

            return Ok(CoreModule_SliderItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CoreModule_SliderItemExists(int id)
        {
            return db.CoreModule_SliderItems.Count(e => e.Id == id) > 0;
        }
    }
}