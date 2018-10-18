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
    public class CoreModuleEventsController : ControllerBase
    {
        private readonly DatabaseEntities _context;

        public CoreModuleEventsController(DatabaseEntities context)
        {
            _context = context;
        }

        // GET: api/CoreModuleEvents
        [HttpGet]
        public IEnumerable<CoreModuleEvents> GetCoreModuleEvents()
        {
            return _context.CoreModuleEvents;
        }

        // GET: api/CoreModuleEvents/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoreModuleEvents([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coreModuleEvents = await _context.CoreModuleEvents.FindAsync(id);

            if (coreModuleEvents == null)
            {
                return NotFound();
            }

            return Ok(coreModuleEvents);
        }

        // PUT: api/CoreModuleEvents/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoreModuleEvents([FromRoute] int id, [FromBody] CoreModuleEvents coreModuleEvents)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != coreModuleEvents.Id)
            {
                return BadRequest();
            }

            _context.Entry(coreModuleEvents).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreModuleEventsExists(id))
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

        // POST: api/CoreModuleEvents
        [HttpPost]
        public async Task<IActionResult> PostCoreModuleEvents([FromBody] CoreModuleEvents coreModuleEvents)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CoreModuleEvents.Add(coreModuleEvents);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoreModuleEvents", new { id = coreModuleEvents.Id }, coreModuleEvents);
        }

        // DELETE: api/CoreModuleEvents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoreModuleEvents([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coreModuleEvents = await _context.CoreModuleEvents.FindAsync(id);
            if (coreModuleEvents == null)
            {
                return NotFound();
            }

            _context.CoreModuleEvents.Remove(coreModuleEvents);
            await _context.SaveChangesAsync();

            return Ok(coreModuleEvents);
        }

        private bool CoreModuleEventsExists(int id)
        {
            return _context.CoreModuleEvents.Any(e => e.Id == id);
        }
    }
}