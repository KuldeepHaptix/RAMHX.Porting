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

namespace RAMHX.CMS.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class cms_TemplatesController : ApiController
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: api/cms_Templates
        public IQueryable<cms_Templates> Getcms_Templates()
        {
            return db.cms_Templates;
        }

        // GET: api/cms_Templates/5
        [ResponseType(typeof(cms_Templates))]
        public IHttpActionResult Getcms_Templates(Guid id)
        {
            cms_Templates cms_Templates = db.cms_Templates.Find(id);
            if (cms_Templates == null)
            {
                return NotFound();
            }

            return Ok(cms_Templates);
        }

        // PUT: api/cms_Templates/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcms_Templates(Guid id, cms_Templates cms_Templates)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cms_Templates.TemplateId)
            {
                return BadRequest();
            }

            var temp = db.cms_Templates.First(tem => tem.TemplateId == cms_Templates.TemplateId);

            temp.TemplateName = cms_Templates.TemplateName;
            temp.TemplateCode = cms_Templates.TemplateCode;
            temp.Description = cms_Templates.Description;
            temp.ModifiedByUserId = SiteContext.CurrentUser_Guid;
            temp.ModifiedDate = DateTime.Now;

            //db.Entry(cms_Templates).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!cms_TemplatesExists(id))
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

        // POST: api/cms_Templates
        [ResponseType(typeof(cms_Templates))]
        public IHttpActionResult Postcms_Templates(cms_Templates cms_Templates)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            cms_Templates.TemplateId = Guid.NewGuid();
            cms_Templates.CreatedByUserId = SiteContext.CurrentUser_Guid;
            cms_Templates.ModifiedByUserId = SiteContext.CurrentUser_Guid;
            cms_Templates.CreatedDate = DateTime.Now;
            cms_Templates.ModifiedDate = DateTime.Now;

            db.cms_Templates.Add(cms_Templates);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (cms_TemplatesExists(cms_Templates.TemplateId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cms_Templates.TemplateId }, cms_Templates);
        }

        // DELETE: api/cms_Templates/5
        [ResponseType(typeof(cms_Templates))]
        public IHttpActionResult Deletecms_Templates(Guid id)
        {
            cms_Templates cms_Templates = db.cms_Templates.Find(id);

            if (cms_Templates == null)
            {
                return NotFound();
            }

            db.cms_TemplateFields.RemoveRange(db.cms_TemplateFields.Where(tf => tf.TemplateId == id));

            db.cms_Templates.Remove(cms_Templates);
            db.SaveChanges();

            return Ok(cms_Templates);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool cms_TemplatesExists(Guid id)
        {
            return db.cms_Templates.Count(e => e.TemplateId == id) > 0;
        }
    }
}