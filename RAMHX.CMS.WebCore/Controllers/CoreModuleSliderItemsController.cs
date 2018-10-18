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
    public class CoreModuleSliderItemsController : ControllerBase
    {
        private readonly DatabaseEntities _context;

        public CoreModuleSliderItemsController(DatabaseEntities context)
        {
            _context = context;
        }

        // GET: api/CoreModuleSliderItems
        [HttpGet]
        public IEnumerable<CoreModuleSliderItems> GetCoreModuleSliderItems()
        {
            return _context.CoreModuleSliderItems;
        }

        // GET: api/CoreModuleSliderItems/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoreModuleSliderItems([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coreModuleSliderItems = await _context.CoreModuleSliderItems.FindAsync(id);

            if (coreModuleSliderItems == null)
            {
                return NotFound();
            }

            return Ok(coreModuleSliderItems);
        }

        // PUT: api/CoreModuleSliderItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoreModuleSliderItems([FromRoute] int id, [FromBody] CoreModuleSliderItems coreModuleSliderItems)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != coreModuleSliderItems.Id)
            {
                return BadRequest();
            }

            _context.Entry(coreModuleSliderItems).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreModuleSliderItemsExists(id))
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

        // POST: api/CoreModuleSliderItems
        [HttpPost]
        public async Task<IActionResult> PostCoreModuleSliderItems([FromBody] CoreModuleSliderItems coreModuleSliderItems)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CoreModuleSliderItems.Add(coreModuleSliderItems);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoreModuleSliderItems", new { id = coreModuleSliderItems.Id }, coreModuleSliderItems);
        }

        // DELETE: api/CoreModuleSliderItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoreModuleSliderItems([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coreModuleSliderItems = await _context.CoreModuleSliderItems.FindAsync(id);
            if (coreModuleSliderItems == null)
            {
                return NotFound();
            }

            _context.CoreModuleSliderItems.Remove(coreModuleSliderItems);
            await _context.SaveChangesAsync();

            return Ok(coreModuleSliderItems);
        }

        private bool CoreModuleSliderItemsExists(int id)
        {
            return _context.CoreModuleSliderItems.Any(e => e.Id == id);
        }
    }
}