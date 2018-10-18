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
    public class CmsTemplatesController : ControllerBase
    {
        private readonly DatabaseEntities _context;

        public CmsTemplatesController(DatabaseEntities context)
        {
            _context = context;
        }

        // GET: api/CmsTemplates
        [HttpGet]
        public IEnumerable<CmsTemplates> GetCmsTemplates()
        {
            return _context.CmsTemplates;
        }

        // GET: api/CmsTemplates/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCmsTemplates([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cmsTemplates = await _context.CmsTemplates.FindAsync(id);

            if (cmsTemplates == null)
            {
                return NotFound();
            }

            return Ok(cmsTemplates);
        }

        // PUT: api/CmsTemplates/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCmsTemplates([FromRoute] Guid id, [FromBody] CmsTemplates cmsTemplates)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cmsTemplates.TemplateId)
            {
                return BadRequest();
            }

            _context.Entry(cmsTemplates).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CmsTemplatesExists(id))
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

        // POST: api/CmsTemplates
        [HttpPost]
        public async Task<IActionResult> PostCmsTemplates([FromBody] CmsTemplates cmsTemplates)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CmsTemplates.Add(cmsTemplates);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CmsTemplatesExists(cmsTemplates.TemplateId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCmsTemplates", new { id = cmsTemplates.TemplateId }, cmsTemplates);
        }

        // DELETE: api/CmsTemplates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCmsTemplates([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cmsTemplates = await _context.CmsTemplates.FindAsync(id);
            if (cmsTemplates == null)
            {
                return NotFound();
            }

            _context.CmsTemplates.Remove(cmsTemplates);
            await _context.SaveChangesAsync();

            return Ok(cmsTemplates);
        }

        private bool CmsTemplatesExists(Guid id)
        {
            return _context.CmsTemplates.Any(e => e.TemplateId == id);
        }
    }
}