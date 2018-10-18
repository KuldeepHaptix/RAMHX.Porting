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
    public class CoreModuleBlogCategoriesController : ControllerBase
    {
        private readonly DatabaseEntities _context;

        public CoreModuleBlogCategoriesController(DatabaseEntities context)
        {
            _context = context;
        }

        // GET: api/CoreModuleBlogCategories
        [HttpGet]
        public IEnumerable<CoreModuleBlogCategory> GetCoreModuleBlogCategory()
        {
            return _context.CoreModuleBlogCategory;
        }

        // GET: api/CoreModuleBlogCategories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoreModuleBlogCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coreModuleBlogCategory = await _context.CoreModuleBlogCategory.FindAsync(id);

            if (coreModuleBlogCategory == null)
            {
                return NotFound();
            }

            return Ok(coreModuleBlogCategory);
        }

        // PUT: api/CoreModuleBlogCategories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoreModuleBlogCategory([FromRoute] int id, [FromBody] CoreModuleBlogCategory coreModuleBlogCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != coreModuleBlogCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(coreModuleBlogCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreModuleBlogCategoryExists(id))
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

        // POST: api/CoreModuleBlogCategories
        [HttpPost]
        public async Task<IActionResult> PostCoreModuleBlogCategory([FromBody] CoreModuleBlogCategory coreModuleBlogCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CoreModuleBlogCategory.Add(coreModuleBlogCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoreModuleBlogCategory", new { id = coreModuleBlogCategory.Id }, coreModuleBlogCategory);
        }

        // DELETE: api/CoreModuleBlogCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoreModuleBlogCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coreModuleBlogCategory = await _context.CoreModuleBlogCategory.FindAsync(id);
            if (coreModuleBlogCategory == null)
            {
                return NotFound();
            }

            _context.CoreModuleBlogCategory.Remove(coreModuleBlogCategory);
            await _context.SaveChangesAsync();

            return Ok(coreModuleBlogCategory);
        }

        private bool CoreModuleBlogCategoryExists(int id)
        {
            return _context.CoreModuleBlogCategory.Any(e => e.Id == id);
        }
    }
}