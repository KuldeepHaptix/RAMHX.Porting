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
    public class CoreModuleEventCategoriesController : ControllerBase
    {
        private readonly DatabaseEntities _context;

        public CoreModuleEventCategoriesController(DatabaseEntities context)
        {
            _context = context;
        }

        // GET: api/CoreModuleEventCategories
        [HttpGet]
        public IEnumerable<CoreModuleEventCategory> GetCoreModuleEventCategory()
        {
            return _context.CoreModuleEventCategory;
        }

        // GET: api/CoreModuleEventCategories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoreModuleEventCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coreModuleEventCategory = await _context.CoreModuleEventCategory.FindAsync(id);

            if (coreModuleEventCategory == null)
            {
                return NotFound();
            }

            return Ok(coreModuleEventCategory);
        }

        // PUT: api/CoreModuleEventCategories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoreModuleEventCategory([FromRoute] int id, [FromBody] CoreModuleEventCategory coreModuleEventCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != coreModuleEventCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(coreModuleEventCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreModuleEventCategoryExists(id))
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

        // POST: api/CoreModuleEventCategories
        [HttpPost]
        public async Task<IActionResult> PostCoreModuleEventCategory([FromBody] CoreModuleEventCategory coreModuleEventCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CoreModuleEventCategory.Add(coreModuleEventCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoreModuleEventCategory", new { id = coreModuleEventCategory.Id }, coreModuleEventCategory);
        }

        // DELETE: api/CoreModuleEventCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoreModuleEventCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coreModuleEventCategory = await _context.CoreModuleEventCategory.FindAsync(id);
            if (coreModuleEventCategory == null)
            {
                return NotFound();
            }

            _context.CoreModuleEventCategory.Remove(coreModuleEventCategory);
            await _context.SaveChangesAsync();

            return Ok(coreModuleEventCategory);
        }

        private bool CoreModuleEventCategoryExists(int id)
        {
            return _context.CoreModuleEventCategory.Any(e => e.Id == id);
        }
    }
}