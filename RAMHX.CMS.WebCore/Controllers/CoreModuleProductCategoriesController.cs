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
    public class CoreModuleProductCategoriesController : ControllerBase
    {
        private readonly DatabaseEntities _context;

        public CoreModuleProductCategoriesController(DatabaseEntities context)
        {
            _context = context;
        }

        // GET: api/CoreModuleProductCategories
        [HttpGet]
        public IEnumerable<CoreModuleProductCategory> GetCoreModuleProductCategory()
        {
            return _context.CoreModuleProductCategory;
        }

        // GET: api/CoreModuleProductCategories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoreModuleProductCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coreModuleProductCategory = await _context.CoreModuleProductCategory.FindAsync(id);

            if (coreModuleProductCategory == null)
            {
                return NotFound();
            }

            return Ok(coreModuleProductCategory);
        }

        // PUT: api/CoreModuleProductCategories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoreModuleProductCategory([FromRoute] int id, [FromBody] CoreModuleProductCategory coreModuleProductCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != coreModuleProductCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(coreModuleProductCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreModuleProductCategoryExists(id))
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

        // POST: api/CoreModuleProductCategories
        [HttpPost]
        public async Task<IActionResult> PostCoreModuleProductCategory([FromBody] CoreModuleProductCategory coreModuleProductCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CoreModuleProductCategory.Add(coreModuleProductCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoreModuleProductCategory", new { id = coreModuleProductCategory.Id }, coreModuleProductCategory);
        }

        // DELETE: api/CoreModuleProductCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoreModuleProductCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coreModuleProductCategory = await _context.CoreModuleProductCategory.FindAsync(id);
            if (coreModuleProductCategory == null)
            {
                return NotFound();
            }

            _context.CoreModuleProductCategory.Remove(coreModuleProductCategory);
            await _context.SaveChangesAsync();

            return Ok(coreModuleProductCategory);
        }

        private bool CoreModuleProductCategoryExists(int id)
        {
            return _context.CoreModuleProductCategory.Any(e => e.Id == id);
        }
    }
}