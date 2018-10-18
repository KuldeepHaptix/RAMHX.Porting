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
    public class CoreModuleProjectCategoriesController : ControllerBase
    {
        private readonly DatabaseEntities _context;

        public CoreModuleProjectCategoriesController(DatabaseEntities context)
        {
            _context = context;
        }

        // GET: api/CoreModuleProjectCategories
        [HttpGet]
        public IEnumerable<CoreModuleProjectCategory> GetCoreModuleProjectCategory()
        {
            return _context.CoreModuleProjectCategory;
        }

        // GET: api/CoreModuleProjectCategories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoreModuleProjectCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coreModuleProjectCategory = await _context.CoreModuleProjectCategory.FindAsync(id);

            if (coreModuleProjectCategory == null)
            {
                return NotFound();
            }

            return Ok(coreModuleProjectCategory);
        }

        // PUT: api/CoreModuleProjectCategories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoreModuleProjectCategory([FromRoute] int id, [FromBody] CoreModuleProjectCategory coreModuleProjectCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != coreModuleProjectCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(coreModuleProjectCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreModuleProjectCategoryExists(id))
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

        // POST: api/CoreModuleProjectCategories
        [HttpPost]
        public async Task<IActionResult> PostCoreModuleProjectCategory([FromBody] CoreModuleProjectCategory coreModuleProjectCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CoreModuleProjectCategory.Add(coreModuleProjectCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoreModuleProjectCategory", new { id = coreModuleProjectCategory.Id }, coreModuleProjectCategory);
        }

        // DELETE: api/CoreModuleProjectCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoreModuleProjectCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coreModuleProjectCategory = await _context.CoreModuleProjectCategory.FindAsync(id);
            if (coreModuleProjectCategory == null)
            {
                return NotFound();
            }

            _context.CoreModuleProjectCategory.Remove(coreModuleProjectCategory);
            await _context.SaveChangesAsync();

            return Ok(coreModuleProjectCategory);
        }

        private bool CoreModuleProjectCategoryExists(int id)
        {
            return _context.CoreModuleProjectCategory.Any(e => e.Id == id);
        }
    }
}