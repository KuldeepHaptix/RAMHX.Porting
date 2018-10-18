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
    public class CoreModuleFaqmastersController : ControllerBase
    {
        private readonly DatabaseEntities _context;

        public CoreModuleFaqmastersController(DatabaseEntities context)
        {
            _context = context;
        }

        // GET: api/CoreModuleFaqmasters
        [HttpGet]
        public IEnumerable<CoreModuleFaqmaster> GetCoreModuleFaqmaster()
        {
            return _context.CoreModuleFaqmaster;
        }

        // GET: api/CoreModuleFaqmasters/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoreModuleFaqmaster([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coreModuleFaqmaster = await _context.CoreModuleFaqmaster.FindAsync(id);

            if (coreModuleFaqmaster == null)
            {
                return NotFound();
            }

            return Ok(coreModuleFaqmaster);
        }

        // PUT: api/CoreModuleFaqmasters/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoreModuleFaqmaster([FromRoute] int id, [FromBody] CoreModuleFaqmaster coreModuleFaqmaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != coreModuleFaqmaster.Id)
            {
                return BadRequest();
            }

            _context.Entry(coreModuleFaqmaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoreModuleFaqmasterExists(id))
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

        // POST: api/CoreModuleFaqmasters
        [HttpPost]
        public async Task<IActionResult> PostCoreModuleFaqmaster([FromBody] CoreModuleFaqmaster coreModuleFaqmaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CoreModuleFaqmaster.Add(coreModuleFaqmaster);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoreModuleFaqmaster", new { id = coreModuleFaqmaster.Id }, coreModuleFaqmaster);
        }

        // DELETE: api/CoreModuleFaqmasters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoreModuleFaqmaster([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coreModuleFaqmaster = await _context.CoreModuleFaqmaster.FindAsync(id);
            if (coreModuleFaqmaster == null)
            {
                return NotFound();
            }

            _context.CoreModuleFaqmaster.Remove(coreModuleFaqmaster);
            await _context.SaveChangesAsync();

            return Ok(coreModuleFaqmaster);
        }

        private bool CoreModuleFaqmasterExists(int id)
        {
            return _context.CoreModuleFaqmaster.Any(e => e.Id == id);
        }
    }
}