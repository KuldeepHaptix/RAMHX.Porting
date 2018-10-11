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
using System.Web.Http.Results;
using System.Web.Mvc;

namespace RAMHX.CMS.Web.Areas.Admin.Controllers
{
    public class cms_TemplateFieldsController : ApiController
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: api/cms_TemplateFields
        public IQueryable<cms_TemplateFields> Getcms_TemplateFields()
        {
            return db.cms_TemplateFields;
        }

       
        // GET: api/cms_TemplateFields/5
        [ResponseType(typeof(cms_TemplateFields))]
        public IHttpActionResult Getcms_TemplateFields(Guid id)
        {
            cms_TemplateFields cms_TemplateFields = db.cms_TemplateFields.Find(id);
            if (cms_TemplateFields == null)
            {
                return NotFound();
            }

            return Ok(cms_TemplateFields);
        }

        // PUT: api/cms_TemplateFields/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcms_TemplateFields(Guid id, cms_TemplateFields cms_TemplateFields)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cms_TemplateFields.TemplateFieldId)
            {
                return BadRequest();
            }

            var tempField = db.cms_TemplateFields.First(tem => tem.TemplateFieldId == cms_TemplateFields.TemplateFieldId);

            tempField.FieldName = cms_TemplateFields.FieldName;
            tempField.FieldTypeId = cms_TemplateFields.FieldTypeId;
            tempField.FieldDisplayOrder = cms_TemplateFields.FieldDisplayOrder;
            tempField.DefaultValue = cms_TemplateFields.DefaultValue;
            tempField.DisplayName = cms_TemplateFields.DisplayName;
            tempField.Notes = cms_TemplateFields.Notes;

            tempField.CreatedByUserId = cms_TemplateFields.CreatedByUserId;
            tempField.ModifiedByUserId = SiteContext.CurrentUser_Guid;
            tempField.CreatedDate = cms_TemplateFields.CreatedDate;
            tempField.ModifiedDate = DateTime.Now;

            //db.Entry(cms_TemplateFields).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!cms_TemplateFieldsExists(id))
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

        // POST: api/cms_TemplateFields
        [ResponseType(typeof(cms_TemplateFields))]
        public IHttpActionResult Postcms_TemplateFields(cms_TemplateFields cms_TemplateFields)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            cms_TemplateFields.TemplateFieldId = Guid.NewGuid();
            cms_TemplateFields.CreatedByUserId = SiteContext.CurrentUser_Guid;
            cms_TemplateFields.ModifiedByUserId = SiteContext.CurrentUser_Guid;
            cms_TemplateFields.CreatedDate = DateTime.Now;
            cms_TemplateFields.ModifiedDate = DateTime.Now;

            db.cms_TemplateFields.Add(cms_TemplateFields);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (cms_TemplateFieldsExists(cms_TemplateFields.TemplateFieldId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cms_TemplateFields.TemplateFieldId }, cms_TemplateFields);
        }

        // DELETE: api/cms_TemplateFields/5
        [ResponseType(typeof(cms_TemplateFields))]
        public IHttpActionResult Deletecms_TemplateFields(Guid id)
        {
            cms_TemplateFields cms_TemplateFields = db.cms_TemplateFields.Find(id);
            if (cms_TemplateFields == null)
            {
                return NotFound();
            }

            db.cms_TemplateFields.Remove(cms_TemplateFields);
            db.SaveChanges();

            return Ok(cms_TemplateFields);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool cms_TemplateFieldsExists(Guid id)
        {
            return db.cms_TemplateFields.Count(e => e.TemplateFieldId == id) > 0;
        }
    }
}