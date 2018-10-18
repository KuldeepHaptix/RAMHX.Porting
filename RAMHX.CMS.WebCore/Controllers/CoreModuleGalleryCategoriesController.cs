using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RAMHX.CMS.DataAccessCore;

namespace RAMHX.CMS.WebCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoreModuleGalleryCategoriesController : ControllerBase
    {
        private readonly DatabaseEntities _context;

        public CoreModuleGalleryCategoriesController(DatabaseEntities context)
        {
            _context = context;
        }

        // GET: api/CoreModuleGalleryCategories
        [HttpGet]
        public IEnumerable<CoreModuleGalleryCategory> GetCoreModuleGalleryCategory()
        {
            return _context.CoreModuleGalleryCategory;
        }

        // GET: api/CoreModuleGalleryCategories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoreModuleGalleryCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coreModuleGalleryCategory = await _context.CoreModuleGalleryCategory.FindAsync(id);

            if (coreModuleGalleryCategory == null)
            {
                return NotFound();
            }

            return Ok(coreModuleGalleryCategory);
        }

        // PUT: api/CoreModuleGalleryCategories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoreModuleGalleryCategory([FromRoute] int id, [FromBody] CoreModuleGalleryCategory coreModuleGalleryCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != coreModuleGalleryCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(coreModuleGalleryCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreModuleGalleryCategoryExists(id))
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

        // POST: api/CoreModuleGalleryCategories
        [HttpPost]
        public async Task<IActionResult> PostCoreModuleGalleryCategory([FromBody] CoreModuleGalleryCategory coreModuleGalleryCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CoreModuleGalleryCategory.Add(coreModuleGalleryCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoreModuleGalleryCategory", new { id = coreModuleGalleryCategory.Id }, coreModuleGalleryCategory);
        }

        // DELETE: api/CoreModuleGalleryCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoreModuleGalleryCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coreModuleGalleryCategory = await _context.CoreModuleGalleryCategory.FindAsync(id);
            if (coreModuleGalleryCategory == null)
            {
                return NotFound();
            }

            _context.CoreModuleGalleryCategory.Remove(coreModuleGalleryCategory);
            await _context.SaveChangesAsync();

            return Ok(coreModuleGalleryCategory);
        }

        private bool CoreModuleGalleryCategoryExists(int id)
        {
            return _context.CoreModuleGalleryCategory.Any(e => e.Id == id);
        }
    }
}