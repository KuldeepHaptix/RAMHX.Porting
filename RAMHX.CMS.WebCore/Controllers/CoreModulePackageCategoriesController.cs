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
    public class CoreModulePackageCategoriesController : ControllerBase
    {
        private readonly DatabaseEntities _context;

        public CoreModulePackageCategoriesController(DatabaseEntities context)
        {
            _context = context;
        }

        // GET: api/CoreModulePackageCategories
        [HttpGet]
        public IEnumerable<CoreModulePackageCategory> GetCoreModulePackageCategory()
        {
            return _context.CoreModulePackageCategory;
        }

        // GET: api/CoreModulePackageCategories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoreModulePackageCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coreModulePackageCategory = await _context.CoreModulePackageCategory.FindAsync(id);

            if (coreModulePackageCategory == null)
            {
                return NotFound();
            }

            return Ok(coreModulePackageCategory);
        }

        // PUT: api/CoreModulePackageCategories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoreModulePackageCategory([FromRoute] int id, [FromBody] CoreModulePackageCategory coreModulePackageCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != coreModulePackageCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(coreModulePackageCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreModulePackageCategoryExists(id))
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

        // POST: api/CoreModulePackageCategories
        [HttpPost]
        public async Task<IActionResult> PostCoreModulePackageCategory([FromBody] CoreModulePackageCategory coreModulePackageCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CoreModulePackageCategory.Add(coreModulePackageCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoreModulePackageCategory", new { id = coreModulePackageCategory.Id }, coreModulePackageCategory);
        }

        // DELETE: api/CoreModulePackageCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoreModulePackageCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coreModulePackageCategory = await _context.CoreModulePackageCategory.FindAsync(id);
            if (coreModulePackageCategory == null)
            {
                return NotFound();
            }

            _context.CoreModulePackageCategory.Remove(coreModulePackageCategory);
            await _context.SaveChangesAsync();

            return Ok(coreModulePackageCategory);
        }

        private bool CoreModulePackageCategoryExists(int id)
        {
            return _context.CoreModulePackageCategory.Any(e => e.Id == id);
        }
    }
}