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
    public class CoreModuleNewsCategoriesController : ControllerBase
    {
        private readonly DatabaseEntities _context;

        public CoreModuleNewsCategoriesController(DatabaseEntities context)
        {
            _context = context;
        }

        // GET: api/CoreModuleNewsCategories
        [HttpGet]
        public IEnumerable<CoreModuleNewsCategory> GetCoreModuleNewsCategory()
        {
            return _context.CoreModuleNewsCategory;
        }

        // GET: api/CoreModuleNewsCategories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoreModuleNewsCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coreModuleNewsCategory = await _context.CoreModuleNewsCategory.FindAsync(id);

            if (coreModuleNewsCategory == null)
            {
                return NotFound();
            }

            return Ok(coreModuleNewsCategory);
        }

        // PUT: api/CoreModuleNewsCategories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoreModuleNewsCategory([FromRoute] int id, [FromBody] CoreModuleNewsCategory coreModuleNewsCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != coreModuleNewsCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(coreModuleNewsCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreModuleNewsCategoryExists(id))
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

        // POST: api/CoreModuleNewsCategories
        [HttpPost]
        public async Task<IActionResult> PostCoreModuleNewsCategory([FromBody] CoreModuleNewsCategory coreModuleNewsCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CoreModuleNewsCategory.Add(coreModuleNewsCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoreModuleNewsCategory", new { id = coreModuleNewsCategory.Id }, coreModuleNewsCategory);
        }

        // DELETE: api/CoreModuleNewsCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoreModuleNewsCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coreModuleNewsCategory = await _context.CoreModuleNewsCategory.FindAsync(id);
            if (coreModuleNewsCategory == null)
            {
                return NotFound();
            }

            _context.CoreModuleNewsCategory.Remove(coreModuleNewsCategory);
            await _context.SaveChangesAsync();

            return Ok(coreModuleNewsCategory);
        }

        private bool CoreModuleNewsCategoryExists(int id)
        {
            return _context.CoreModuleNewsCategory.Any(e => e.Id == id);
        }
    }
}