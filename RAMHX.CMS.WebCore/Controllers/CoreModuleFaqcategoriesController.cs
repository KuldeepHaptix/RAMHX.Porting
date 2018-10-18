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
    public class CoreModuleFaqcategoriesController : ControllerBase
    {
        private readonly DatabaseEntities _context;

        public CoreModuleFaqcategoriesController(DatabaseEntities context)
        {
            _context = context;
        }

        // GET: api/CoreModuleFaqcategories
        [HttpGet]
        public IEnumerable<CoreModuleFaqcategory> GetCoreModuleFaqcategory()
        {
            return _context.CoreModuleFaqcategory;
        }

        // GET: api/CoreModuleFaqcategories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoreModuleFaqcategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coreModuleFaqcategory = await _context.CoreModuleFaqcategory.FindAsync(id);

            if (coreModuleFaqcategory == null)
            {
                return NotFound();
            }

            return Ok(coreModuleFaqcategory);
        }

        // PUT: api/CoreModuleFaqcategories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoreModuleFaqcategory([FromRoute] int id, [FromBody] CoreModuleFaqcategory coreModuleFaqcategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != coreModuleFaqcategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(coreModuleFaqcategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreModuleFaqcategoryExists(id))
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

        // POST: api/CoreModuleFaqcategories
        [HttpPost]
        public async Task<IActionResult> PostCoreModuleFaqcategory([FromBody] CoreModuleFaqcategory coreModuleFaqcategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CoreModuleFaqcategory.Add(coreModuleFaqcategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoreModuleFaqcategory", new { id = coreModuleFaqcategory.Id }, coreModuleFaqcategory);
        }

        // DELETE: api/CoreModuleFaqcategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoreModuleFaqcategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coreModuleFaqcategory = await _context.CoreModuleFaqcategory.FindAsync(id);
            if (coreModuleFaqcategory == null)
            {
                return NotFound();
            }

            _context.CoreModuleFaqcategory.Remove(coreModuleFaqcategory);
            await _context.SaveChangesAsync();

            return Ok(coreModuleFaqcategory);
        }

        private bool CoreModuleFaqcategoryExists(int id)
        {
            return _context.CoreModuleFaqcategory.Any(e => e.Id == id);
        }
    }
}