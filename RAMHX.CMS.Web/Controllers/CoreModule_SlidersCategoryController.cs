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
    public class CoreModule_SlidersCategoryController : ApiController
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: api/CoreModule_SlidersCategory
        public IQueryable<CoreModule_Slider> GetCoreModule_Sliders()
        {
            return db.CoreModule_Sliders.OrderByDescending(proj => proj.DisplayOrder);
        }

        // GET: api/CoreModule_SlidersCategory/5
        [ResponseType(typeof(CoreModule_Slider))]
        public IHttpActionResult GetCoreModule_Slider(int id)
        {
            CoreModule_Slider CoreModule_Slider = db.CoreModule_Sliders.Find(id);
            if (CoreModule_Slider == null)
            {
                return NotFound();
            }

            return Ok(CoreModule_Slider);
        }

        // PUT: api/CoreModule_SlidersCategory/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCoreModule_Slider(int id, CoreModule_Slider CoreModule_Slider)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != CoreModule_Slider.Id)
            {
                return BadRequest();
            }

            db.Entry(CoreModule_Slider).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreModule_SliderExists(id))
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

        // POST: api/CoreModule_SlidersCategory
        [ResponseType(typeof(CoreModule_Slider))]
        public IHttpActionResult PostCoreModule_Slider(CoreModule_Slider CoreModule_Slider)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CoreModule_Sliders.Add(CoreModule_Slider);
            db.SaveChanges();
            int nextDO = db.CoreModule_Sliders.Max(pp => pp.DisplayOrder);
            nextDO++;
            CoreModule_Slider.DisplayOrder = nextDO;
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = CoreModule_Slider.Id }, CoreModule_Slider);
        }

        // DELETE: api/CoreModule_SlidersCategory/5
        [ResponseType(typeof(CoreModule_Slider))]
        public IHttpActionResult DeleteCoreModule_Slider(int id)
        {
            CoreModule_Slider CoreModule_Slider = db.CoreModule_Sliders.Find(id);
            if (CoreModule_Slider == null)
            {
                return NotFound();
            }

            db.CoreModule_Sliders.Remove(CoreModule_Slider);
            db.SaveChanges();

            return Ok(CoreModule_Slider);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CoreModule_SliderExists(int id)
        {
            return db.CoreModule_Sliders.Count(e => e.Id == id) > 0;
        }
    }
}