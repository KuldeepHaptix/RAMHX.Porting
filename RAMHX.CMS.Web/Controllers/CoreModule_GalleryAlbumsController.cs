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
    public class CoreModule_GalleryAlbumsController : ApiController
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: api/CoreModule_GalleryAlbums
        public IQueryable<CoreModule_GalleryAlbum> GetCoreModule_GalleryAlbums()
        {
            return db.CoreModule_GalleryAlbums.OrderByDescending(alb => alb.DisplayOrder);
        }

        // GET: api/CoreModule_GalleryAlbums/5
        [ResponseType(typeof(CoreModule_GalleryAlbum))]
        public IHttpActionResult GetCoreModule_GalleryAlbum(int id)
        {
            CoreModule_GalleryAlbum CoreModule_GalleryAlbum = db.CoreModule_GalleryAlbums.Find(id);
            if (CoreModule_GalleryAlbum == null)
            {
                return NotFound();
            }

            return Ok(CoreModule_GalleryAlbum);
        }

        // PUT: api/CoreModule_GalleryAlbums/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCoreModule_GalleryAlbum(int id, CoreModule_GalleryAlbum CoreModule_GalleryAlbum)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != CoreModule_GalleryAlbum.Id)
            {
                return BadRequest();
            }

            db.Entry(CoreModule_GalleryAlbum).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreModule_GalleryAlbumExists(id))
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

        // POST: api/CoreModule_GalleryAlbums
        [ResponseType(typeof(CoreModule_GalleryAlbum))]
        public IHttpActionResult PostCoreModule_GalleryAlbum(CoreModule_GalleryAlbum CoreModule_GalleryAlbum)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CoreModule_GalleryAlbums.Add(CoreModule_GalleryAlbum);
            db.SaveChanges();

            int nextDO = db.CoreModule_GalleryAlbums.Max(pp => pp.DisplayOrder);
            nextDO++;
            CoreModule_GalleryAlbum.DisplayOrder = nextDO;
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = CoreModule_GalleryAlbum.Id }, CoreModule_GalleryAlbum);
        }

        // DELETE: api/CoreModule_GalleryAlbums/5
        [ResponseType(typeof(CoreModule_GalleryAlbum))]
        public IHttpActionResult DeleteCoreModule_GalleryAlbum(int id)
        {
            CoreModule_GalleryAlbum CoreModule_GalleryAlbum = db.CoreModule_GalleryAlbums.Find(id);
            if (CoreModule_GalleryAlbum == null)
            {
                return NotFound();
            }

            db.CoreModule_GalleryAlbums.Remove(CoreModule_GalleryAlbum);
            db.SaveChanges();

            return Ok(CoreModule_GalleryAlbum);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CoreModule_GalleryAlbumExists(int id)
        {
            return db.CoreModule_GalleryAlbums.Count(e => e.Id == id) > 0;
        }
    }
}