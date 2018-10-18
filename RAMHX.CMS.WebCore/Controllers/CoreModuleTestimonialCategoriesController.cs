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
    public class CoreModuleTestimonialCategoriesController : ControllerBase
    {
        private readonly DatabaseEntities _context;

        public CoreModuleTestimonialCategoriesController(DatabaseEntities context)
        {
            _context = context;
        }

        // GET: api/CoreModuleTestimonialCategories
        [HttpGet]
        public IEnumerable<CoreModuleTestimonialCategory> GetCoreModuleTestimonialCategory()
        {
            return _context.CoreModuleTestimonialCategory;
        }

        // GET: api/CoreModuleTestimonialCategories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoreModuleTestimonialCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coreModuleTestimonialCategory = await _context.CoreModuleTestimonialCategory.FindAsync(id);

            if (coreModuleTestimonialCategory == null)
            {
                return NotFound();
            }

            return Ok(coreModuleTestimonialCategory);
        }

        // PUT: api/CoreModuleTestimonialCategories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoreModuleTestimonialCategory([FromRoute] int id, [FromBody] CoreModuleTestimonialCategory coreModuleTestimonialCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != coreModuleTestimonialCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(coreModuleTestimonialCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreModuleTestimonialCategoryExists(id))
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

        // POST: api/CoreModuleTestimonialCategories
        [HttpPost]
        public async Task<IActionResult> PostCoreModuleTestimonialCategory([FromBody] CoreModuleTestimonialCategory coreModuleTestimonialCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CoreModuleTestimonialCategory.Add(coreModuleTestimonialCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoreModuleTestimonialCategory", new { id = coreModuleTestimonialCategory.Id }, coreModuleTestimonialCategory);
        }

        // DELETE: api/CoreModuleTestimonialCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoreModuleTestimonialCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coreModuleTestimonialCategory = await _context.CoreModuleTestimonialCategory.FindAsync(id);
            if (coreModuleTestimonialCategory == null)
            {
                return NotFound();
            }

            _context.CoreModuleTestimonialCategory.Remove(coreModuleTestimonialCategory);
            await _context.SaveChangesAsync();

            return Ok(coreModuleTestimonialCategory);
        }

        private bool CoreModuleTestimonialCategoryExists(int id)
        {
            return _context.CoreModuleTestimonialCategory.Any(e => e.Id == id);
        }
    }
}