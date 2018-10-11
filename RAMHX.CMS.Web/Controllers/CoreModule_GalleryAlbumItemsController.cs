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
    public class CoreModule_GalleryAlbumItemsController : ApiController
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: api/CoreModule_GalleryAlbumItems
        public IQueryable<CoreModule_GalleryAlbumItem> GetCoreModule_GalleryAlbumItems()
        {
            return db.CoreModule_GalleryAlbumItems.OrderByDescending(alb => alb.DisplayOrder);
        }

        public IQueryable<CoreModule_GalleryAlbumItem> GetCoreModule_GalleryAlbumItems(int albumId)
        {
            return db.CoreModule_GalleryAlbumItems.Where(a => a.AlbumId == albumId).OrderByDescending(alb => alb.DisplayOrder);
        }
        // GET: api/CoreModule_GalleryAlbumItems/5
        [ResponseType(typeof(CoreModule_GalleryAlbumItem))]
        public IHttpActionResult GetCoreModule_GalleryAlbumItem(int id)
        {
            CoreModule_GalleryAlbumItem CoreModule_GalleryAlbumItem = db.CoreModule_GalleryAlbumItems.Find(id);
            if (CoreModule_GalleryAlbumItem == null)
            {
                return NotFound();
            }

            return Ok(CoreModule_GalleryAlbumItem);
        }

        // PUT: api/CoreModule_GalleryAlbumItems/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCoreModule_GalleryAlbumItem(int id, CoreModule_GalleryAlbumItem CoreModule_GalleryAlbumItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != CoreModule_GalleryAlbumItem.Id)
            {
                return BadRequest();
            }

            db.Entry(CoreModule_GalleryAlbumItem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreModule_GalleryAlbumItemExists(id))
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

        // POST: api/CoreModule_GalleryAlbumItems
        [ResponseType(typeof(CoreModule_GalleryAlbumItem))]
        public IHttpActionResult PostCoreModule_GalleryAlbumItem(CoreModule_GalleryAlbumItem CoreModule_GalleryAlbumItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CoreModule_GalleryAlbumItems.Add(CoreModule_GalleryAlbumItem);
            db.SaveChanges();

            int nextDO = db.CoreModule_GalleryAlbumItems.Max(pp => pp.DisplayOrder);
            nextDO++;
            CoreModule_GalleryAlbumItem.DisplayOrder = nextDO;
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = CoreModule_GalleryAlbumItem.Id }, CoreModule_GalleryAlbumItem);
        }

        // DELETE: api/CoreModule_GalleryAlbumItems/5
        [ResponseType(typeof(CoreModule_GalleryAlbumItem))]
        public IHttpActionResult DeleteCoreModule_GalleryAlbumItem(int id)
        {
            CoreModule_GalleryAlbumItem CoreModule_GalleryAlbumItem = db.CoreModule_GalleryAlbumItems.Find(id);
            if (CoreModule_GalleryAlbumItem == null)
            {
                return NotFound();
            }

            db.CoreModule_GalleryAlbumItems.Remove(CoreModule_GalleryAlbumItem);
            db.SaveChanges();

            return Ok(CoreModule_GalleryAlbumItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CoreModule_GalleryAlbumItemExists(int id)
        {
            return db.CoreModule_GalleryAlbumItems.Count(e => e.Id == id) > 0;
        }
    }
}