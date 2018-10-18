using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RAMHX.CMS.DataAccessCore;

namespace RAMHX.CMS.WebCore.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CmsTemplateFieldsController : ControllerBase
    {
        private readonly DatabaseEntities _context;

        public CmsTemplateFieldsController(DatabaseEntities context)
        {
            _context = context;
        }

        // GET: api/CmsTemplateFields
        [HttpGet]
        public IEnumerable<CmsTemplateFields> GetCmsTemplateFields()
        {
            return _context.CmsTemplateFields;
        }

        // GET: api/CmsTemplateFields/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCmsTemplateFields([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cmsTemplateFields = await _context.CmsTemplateFields.FindAsync(id);

            if (cmsTemplateFields == null)
            {
                return NotFound();
            }

            return Ok(cmsTemplateFields);
        }

        // PUT: api/CmsTemplateFields/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCmsTemplateFields([FromRoute] Guid id, [FromBody] CmsTemplateFields cmsTemplateFields)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cmsTemplateFields.TemplateFieldId)
            {
                return BadRequest();
            }

            _context.Entry(cmsTemplateFields).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CmsTemplateFieldsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CmsTemplateFields
        [HttpPost]
        public async Task<IActionResult> PostCmsTemplateFields([FromBody] CmsTemplateFields cmsTemplateFields)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CmsTemplateFields.Add(cmsTemplateFields);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CmsTemplateFieldsExists(cmsTemplateFields.TemplateFieldId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCmsTemplateFields", new { id = cmsTemplateFields.TemplateFieldId }, cmsTemplateFields);
        }

        // DELETE: api/CmsTemplateFields/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCmsTemplateFields([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cmsTemplateFields = await _context.CmsTemplateFields.FindAsync(id);
            if (cmsTemplateFields == null)
            {
                return NotFound();
            }

            _context.CmsTemplateFields.Remove(cmsTemplateFields);
            await _context.SaveChangesAsync();

            return Ok(cmsTemplateFields);
        }

        private bool CmsTemplateFieldsExists(Guid id)
        {
            return _context.CmsTemplateFields.Any(e => e.TemplateFieldId == id);
        }
    }
}