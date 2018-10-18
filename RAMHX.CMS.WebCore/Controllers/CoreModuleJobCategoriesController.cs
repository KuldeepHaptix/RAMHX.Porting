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
    public class CoreModuleJobCategoriesController : ControllerBase
    {
        private readonly DatabaseEntities _context;

        public CoreModuleJobCategoriesController(DatabaseEntities context)
        {
            _context = context;
        }

        // GET: api/CoreModuleJobCategories
        [HttpGet]
        public IEnumerable<CoreModuleJobCategory> GetCoreModuleJobCategory()
        {
            return _context.CoreModuleJobCategory;
        }

        // GET: api/CoreModuleJobCategories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoreModuleJobCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coreModuleJobCategory = await _context.CoreModuleJobCategory.FindAsync(id);

            if (coreModuleJobCategory == null)
            {
                return NotFound();
            }

            return Ok(coreModuleJobCategory);
        }

        // PUT: api/CoreModuleJobCategories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoreModuleJobCategory([FromRoute] int id, [FromBody] CoreModuleJobCategory coreModuleJobCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != coreModuleJobCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(coreModuleJobCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreModuleJobCategoryExists(id))
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

        // POST: api/CoreModuleJobCategories
        [HttpPost]
        public async Task<IActionResult> PostCoreModuleJobCategory([FromBody] CoreModuleJobCategory coreModuleJobCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CoreModuleJobCategory.Add(coreModuleJobCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoreModuleJobCategory", new { id = coreModuleJobCategory.Id }, coreModuleJobCategory);
        }

        // DELETE: api/CoreModuleJobCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoreModuleJobCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coreModuleJobCategory = await _context.CoreModuleJobCategory.FindAsync(id);
            if (coreModuleJobCategory == null)
            {
                return NotFound();
            }

            _context.CoreModuleJobCategory.Remove(coreModuleJobCategory);
            await _context.SaveChangesAsync();

            return Ok(coreModuleJobCategory);
        }

        private bool CoreModuleJobCategoryExists(int id)
        {
            return _context.CoreModuleJobCategory.Any(e => e.Id == id);
        }
    }
}